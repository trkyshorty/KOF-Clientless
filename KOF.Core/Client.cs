using KOF.Core.Models;
using System.Net.Sockets;
using KOF.Core.Extensions;
using KOF.Core.Services;
using KOF.Core.Communications;
using KOF.Core.Exceptions;
using KOF.Database.Models;
using KOF.Core.Handlers;
using System.ComponentModel;
using System.Text.Json;
using System.Diagnostics;
using KOF.Database;

namespace KOF.Core;

public class Client
{
    [Browsable(false)]
    public CharacterHandler CharacterHandler { get; set; } = new();
    [Browsable(false)]
    public Character Character { get { return CharacterHandler.MySelf; } }
    [Browsable(false)]
    public Server Server { get; private set; } = default!;
    [Browsable(false)]
    public Account Account { get; private set; } = default!;
    [Browsable(false)]
    public Session Session { get; private set; } = default!;
    [Browsable(false)]
    private bool Exited { get; set; } = false;
    [Browsable(false)]
    public long StartTime { get; private set; } = Stopwatch.GetTimestamp();
    public string Name
    {
        get
        {
            return Character.Name != "" ? Character.Name : Account.Character;
        }
    }
    public string Hp
    {
        get
        {
            return Character.Hp != 0 ? $"{Math.Ceiling((Character.Hp * 100) / (float)Character.MaxHp)}%" : $"{0}%";
        }
    }
    public string Mp
    {
        get
        {
            return Character.Hp != 0 ? $"{Math.Ceiling((Character.Mp * 100) / (float)Character.MaxMp)}%" : $"{0}%";
        }
    }
    public byte Level
    {
        get
        {
            if (Character.Level != 0)
                return Character.Level;

            var characterData = JsonSerializer.Deserialize<List<Lobby>>(Account.CharacterData)!;

            if (characterData.Count > 0)
                return characterData.FirstOrDefault(x => x.Name == Account.Character)!.Level;

            return 0;
        }
    }
    public string Job
    {
        get
        {
            if (Character.Job != "")
                return Character.Job;

            var characterData = JsonSerializer.Deserialize<List<Lobby>>(Account.CharacterData)!;

            if (characterData.Count > 0)
                return Character.GetRepresentClassName(characterData.FirstOrDefault(x => x.Name == Account.Character)!.Class);

            return "";
        }
    }
    public string Follow
    {
        get
        {
            if (CharacterHandler.GetGameState() != Enums.GameState.GAME_STATE_INGAME)
                return "<none>";

            if (CharacterHandler.Controller != null)
            {
                var follow = CharacterHandler.Controller.GetControl("Follow", "");

                if (follow != "")
                    return follow;
            }

            return "<none>";
        }
    }
    public string Route
    {
        get
        {
            if (CharacterHandler.GetGameState() != Enums.GameState.GAME_STATE_INGAME)
                return "<none>";

            if (CharacterHandler.Controller != null)
            {
                var route = SQLiteHandler.Table<Route>().SingleOrDefault(x => x.Id == CharacterHandler.Controller.GetControl("SelectedRoute", 0));

                if (route != null)
                    return route.Name;
            }

            return "<none>";
        }
    }
    public string Area { get { return Character.Area; } }
    public bool Bot
    {
        get
        {
            if (CharacterHandler.GetGameState() != Enums.GameState.GAME_STATE_INGAME)
                return false;

            if (CharacterHandler.Controller != null)
                return CharacterHandler.Controller.GetControl("Bot", false);

            return false;
        }
        set
        {
            if (CharacterHandler.Controller != null)
                CharacterHandler.Controller.SetControl("Bot", value);
        }
    }
    public bool Attack
    {
        get
        {
            if (CharacterHandler.GetGameState() != Enums.GameState.GAME_STATE_INGAME)
                return false;

            if (CharacterHandler.Controller != null)
                return CharacterHandler.Controller.GetControl("Attack", false);

            return false;
        }
        set
        {
            if (CharacterHandler.Controller != null)
                CharacterHandler.Controller.SetControl("Attack", value);

        }
    }
    public bool Self
    {
        get
        {
            if (CharacterHandler.GetGameState() != Enums.GameState.GAME_STATE_INGAME)
                return false;

            if (CharacterHandler.Controller != null)
                return CharacterHandler.Controller.GetControl("SelfSkill", false);

            return false;
        }

        set
        {
            if (CharacterHandler.Controller != null)
                CharacterHandler.Controller.SetControl("SelfSkill", value);
        }
    }

    public Client(Server server, Account account)
    {
        Server = server;
        Account = account;

        ConnectToLoginServer();
    }

    public void Close()
    {
        Exited = true;

        Session.DisconnectAsync();

        Session.Dispose();
        CharacterHandler.Dispose();
    }

    public void Send(Message msg)
    {
        Session.SendAsync(msg).ConfigureAwait(false);
    }

    private void ConnectToLoginServer()
    {
        CreateNewSessionInstance();

        var clientGatewayTask = Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            try
            {
                Random rnd = new Random();

                byte randomPort = (byte)rnd.Next(1, 9);
                
                await Session.ConnectAsync(Server.GatewayIp, 15101 + randomPort); // TODO: Get values from database
                await Session.SendAsync(MessageBuilder.MsgSend_Cryption());
                await Session.RunAsync();
            }
            catch (Exception ex) when (ex is RemoteDisconnectedException || ex is SocketException)
            {
                Debug.WriteLine($"{SpanExtensions.GetTimeStamp()} '{Account.Login}' {ex.Message}");
            }
        });

        clientGatewayTask.ContinueWith((completedTask) =>
        {
            if(completedTask.Exception != null)
            {
                if (Exited == false)
                    ConnectToLoginServer();
            }
            else
            {
                if (Exited == false)
                    ConnectToGameServer();
            }
        });
    }

    private void ConnectToGameServer()
    {
        CreateNewSessionInstance();

        var clientAgentTask = Task.Run(async () =>
        {
            try
            {
                await Session.ConnectAsync(Server.AgentIp, Server.AgentPort);
                await Session.SendAsync(MessageBuilder.MsgSend_VersionCheck());
                await Session.RunAsync();
            }
            catch (Exception ex) when (ex is RemoteDisconnectedException || ex is SocketException)
            {
                Debug.WriteLine($"{SpanExtensions.GetTimeStamp()} '{Account.Login}' {ex.Message}");
            }
        });

        clientAgentTask.ContinueWith((completedTask) =>
        {
            if (Exited == false)
                ConnectToLoginServer();
        });
    }

    private void CreateNewSessionInstance()
    {
        StartTime = Environment.TickCount;

        if (Session != null)
        {
            Session.DisconnectAsync();
            Session.Dispose();

            CharacterHandler.Dispose();
        }

        Session = new(Account, this);

        Session.RegisterService(new HandshakeService());
        Session.RegisterService(new LoginService());
        Session.RegisterService(new GameService());
    }
}
