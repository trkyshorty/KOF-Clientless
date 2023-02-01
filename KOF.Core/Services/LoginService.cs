using KOF.Core.Handlers;
using KOF.Core.Models;
using KOF.Core.Enums;
using KOF.Core.Extensions;
using KOF.Core.Communications;
using System.Numerics;
using System.Text.Json;
using System.Diagnostics;
using KOF.Database;

namespace KOF.Core.Services;

public partial class LoginService
{
    [MessageHandler(MessageID.LS_CRYPTION)]
    public Task MsgRecv_Cryption(Session session, Message msg)
    {
        if (!session.Ready)
            return Task.CompletedTask;

        return session.SendAsync(MessageBuilder.MsgSend_AccountLoginRequest(session.Account.Login, session.Account.Password));
    }

    [MessageHandler(MessageID.LS_LOGIN_REQ)]
    public Task MsgRecv_LoginREQ(Session session, Message msg)
    {
        _ = msg.Read<short>(); // unknown
        LoginError result = (LoginError)msg.Read<byte>();

        string authMessage = result switch
        {
            LoginError.AUTH_SUCCESS => "SUCCESS",
            LoginError.AUTH_NOT_FOUND => "NOT FOUND",
            LoginError.AUTH_INVALID => "INVALID",
            LoginError.AUTH_BANNED => "BANNED",
            LoginError.AUTH_IN_GAME => "IN GAME",
            LoginError.AUTH_ERROR => "ERROR",
            LoginError.AUTH_AGREEMENT => "USER AGREEMENT",
            LoginError.AUTH_FAILED => "FAILED",
            _ => $"Unknown: {result}",
        };

        switch (result)
        {
            case LoginError.AUTH_BANNED:
                Debug.WriteLine($"{session.Account.Login} Gateway login '{authMessage}'");
                return session.DisconnectAsync();

            case LoginError.AUTH_SUCCESS:
                short premium = msg.Read<short>();
                string accountId = msg.Read(false);
                Debug.WriteLine($"{session.Account.Login} Gateway login '{authMessage}' - premium type: {(PremiumType)premium}");
                return session.DisconnectAsync();

            case LoginError.AUTH_IN_GAME:
                string serverIp = msg.Read(true).Trim();
                ushort serverPort = msg.Read<ushort>();
                string accountID = msg.Read(true);

                Debug.WriteLine($"{SpanExtensions.GetTimeStamp()} {session.Account.Login} Gateway status '{authMessage}'.");

                //TODO : account icine kontrol koymaliyiz otomatik tekrar girsin mi tarzinda auto relogin.
                var clientKickout = Task.Run(async () =>
                {
                    using Session sessionKickout = new(session.Account, session.Client);
                    await sessionKickout.ConnectAsync(serverIp, serverPort);
                    await sessionKickout.SendAsync(MessageBuilder.MsgSend_KickOut(accountID));
                    await Task.Delay(TimeSpan.FromMilliseconds(300));
                    await sessionKickout.DisconnectAsync();
                });

                return session.DisconnectAsync();

            default:
                Debug.WriteLine(authMessage);
                break;
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_VERSION_CHECK)]
    public Task MsgRecv_VersionCheck(Session session, Message msg)
    {
        if (!session.Ready)
            return Task.CompletedTask;

        return session.SendAsync(MessageBuilder.MsgSend_AccountLogin(session.Account.Login, session.Account.Password));
    }

    [MessageHandler(MessageID.WIZ_LOGIN)]
    public Task MsgRecv_Login(Session session, Message msg)
    {
        //ushort commandType = msg.Read<ushort>();
        byte commandType = msg.Read<byte>();

        switch (commandType)
        {
            case 0:
            case 1:
            case 2:
                return session.SendAsync(MessageBuilder.MsgSend_HackTool(6, "f7c65b0e"));

            case 17: // not signed in -> last login check.
                return session.DisconnectAsync();

            default:
                Console.WriteLine($"MsgRecv_Login:: {msg.AsDataSpan().ToHexString()}");
                return session.DisconnectAsync();
        }
    }

    [MessageHandler(MessageID.WIZ_HACKTOOL)]
    public Task MsgRecv_HackTool(Session session, Message msg)
    {
        byte commandType = msg.Read<byte>();
        short reason = msg.Read<short>();

        if (commandType == 6 && reason != -1)
            return session.SendAsync(MessageBuilder.MsgSend_LoadingLogin());

        return session.DisconnectAsync();
    }

    [MessageHandler(MessageID.WIZ_LOADING_LOGIN)]
    public Task MsgRecv_LoadingLogin(Session session, Message msg)
    {
        byte commandType = msg.Read<byte>();
        uint queueCount = msg.Read<uint>();

        if (commandType == 1)
        {
            if (queueCount > 0)
            {
                Debug.WriteLine($"{session.Account.Login} waiting queue: {queueCount}");
                return session.SendAsync(MessageBuilder.MsgSend_SpeedCheck(session.Client.StartTime));
            }
            else if (queueCount == 0)
                return session.SendAsync(MessageBuilder.MsgSend_AllCharacterInfoRequest());
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_ALLCHAR_INFO_REQ)]
    public Task MsgRecv_AllCharacterInfo(Session session, Message msg)
    {
        bool messageCheck = msg.AsDataSpan()[^3..].ToArray().SequenceEqual(new byte[] { 0x0c, 0x02, 0x00 });

        if (messageCheck)
        {
            var characterData = JsonSerializer.Deserialize<List<Lobby>>(session.Account.CharacterData)!;

            if (session.Account.Character != "" && characterData.Any(x => x.Name == session.Account.Character))
            {
                var selectedCharacter = characterData.FirstOrDefault(x => x.Name == session.Account.Character)!;

                session.Client.Character.Name = selectedCharacter.Name;
                session.Client.Character.Level = selectedCharacter.Level;
                session.Client.Character.Race = selectedCharacter.Race;
                session.Client.Character.Zone = selectedCharacter.Zone;

                return session.SendAsync(MessageBuilder.MsgSend_SelectedCharacter(session.Account.Login, session.Account.Character, session.Client.Character.Zone));
            }

            else
            {
                var selectableCharacter = characterData.FirstOrDefault(x => x.Name != "");

                if (selectableCharacter != null)
                {
                    session.Account.Character = selectableCharacter.Name;

                    session.Client.Character.Name = selectableCharacter.Name;
                    session.Client.Character.Level = selectableCharacter.Level;
                    session.Client.Character.Race = selectableCharacter.Race;
                    session.Client.Character.Zone = selectableCharacter.Zone;

                    SQLiteHandler.Update(session.Account);

                    return session.SendAsync(MessageBuilder.MsgSend_SelectedCharacter(session.Account.Login, session.Account.Character, session.Client.Character.Zone));
                }
            }
        }
        else
        {
            List<Lobby> characterList = JsonSerializer.Deserialize<List<Lobby>>(session.Account.CharacterData)!;

            byte commandType = msg.Read<byte>();

            if (commandType == 0x01)
            {
                _ = msg.Read<byte>(); // unknown

                for (byte i = 0; i < 4; i++)
                {
                    var _character = new Lobby()
                    {
                        Slot = i,
                        Name = msg.Read(true, "gb2312"),
                        Race = msg.Read<byte>(),
                        Class = msg.Read<ushort>(),
                        Level = msg.Read<byte>(),
                        Face = msg.Read<byte>(),
                        Hair = msg.Read<byte>(),
                        R = msg.Read<byte>(),
                        G = msg.Read<byte>(),
                        B = msg.Read<byte>(),
                        Zone = msg.Read<byte>(),
                        Unknown1 = msg.Read<byte>(),
                        VisibleEquipment = VisibleEquipment(msg)
                    };

                    if (characterList.Count == 4)
                    {
                        if (_character.Name == "" && _character.Name != characterList[i].Name)
                        {
                            session.SendAsync(MessageBuilder.MsgSend_SelectNation(session.Account.NationId)).ConfigureAwait(false);
                            session.SendAsync(MessageBuilder.MsgSend_NewCharacterCreate(characterList[i])).ConfigureAwait(false);
                        }
                        else
                            characterList[i] = _character;
                    }
                    else
                        characterList.Add(_character);
                }

                session.Account.CharacterData = JsonSerializer.Serialize(characterList);

                SQLiteHandler.Update(session.Account);

                static Inventory[] VisibleEquipment(Message msg)
                {
                    var _itemArray = new Inventory[8];
                    for (byte j = 0; j < 8; j++)
                    {
                        var pItem = new Inventory()
                        {
                            Pos = j,
                            ItemID = msg.Read<uint>(),
                            Durability = msg.Read<ushort>()
                        };
                        _itemArray[j] = pItem;
                    }
                    return _itemArray;
                }
            }
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_SEL_CHAR)]
    public Task MsgRecv_SelectedCharacter(Session session, Message msg)
    {
        var commandType = msg.Read<bool>();

        if (commandType)
        {
            session.Client.Character.Zone = msg.Read<byte>();

            session.Client.Character.X = msg.Read<ushort>() / 10.0f;
            session.Client.Character.Y = msg.Read<ushort>() / 10.0f;
            session.Client.Character.Z = msg.Read<ushort>() / 10.0f;

            _ = msg.Read<byte>(); //VictoryNation

            session.Client.Character.SetPosition(session.Client.Character.GetPosition());

            session.SendAsync(MessageBuilder.MsgSend_ShoppingMall((byte)ShoppingMallType.STORE_CLOSE)).ConfigureAwait(false);
            session.SendAsync(MessageBuilder.MsgSend_BufferSize()).ConfigureAwait(false);
            //session.SendAsync(MessageBuilder.MsgSend_Rental()).ConfigureAwait(false);

            session.SendAsync(MessageBuilder.MsgSend_SpeedCheck(session.Client.StartTime, true)).ConfigureAwait(false);

            session.Client.Character.Zone = (byte)Character.GetRepresentZone(session.Client.Character.Zone);

            ClientHandler.LoadZone(session.Client.Character.Zone);

            return session.SendAsync(MessageBuilder.MsgSend_ServerIndex());
        }
        else
            return session.DisconnectAsync();
    }

    [MessageHandler(MessageID.WIZ_SERVER_INDEX)]
    public Task MsgRecv_ServerIndex(Session session, Message msg)
    {
        session.Client.Character.GameState = GameState.GAME_STATE_CONNECTED;
        return session.SendAsync(MessageBuilder.MsgSend_GameStart((byte)session.Client.Character.GameState, session.Account.Character));
    }
}





