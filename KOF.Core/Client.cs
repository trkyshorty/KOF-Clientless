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
using KOF.Core.Win32;
using System.Text;
using KOF.Cryptography;

namespace KOF.Core;

public class Client
{
    [Browsable(false)]
    public CharacterHandler CharacterHandler { get; set; } = new();
    [Browsable(false)]
    public Character Character { get { return CharacterHandler.MySelf; } }
    [Browsable(false)]
    public Process ClientProcess { get; private set; } = default!;
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
    
    [Browsable(false)]
    public IntPtr MailslotRecvPtr { get; private set; }
    [Browsable(false)]
    public IntPtr MailslotRecvFuncPtr { get; private set; }
    [Browsable(false)]
    public IntPtr MailslotRecvHookPtr { get; private set; }
    [Browsable(false)]
    public IntPtr MailslotSendHookPtr { get; private set; }
    [Browsable(false)]
    public long LastConnectionLostTime { get; set; } = Environment.TickCount;

    [Browsable(false)]
    public IntPtr MailslotSendPtr { get; private set; }

    public Client(Server server, Account account)
    {
        Server = server;
        Account = account;
        
        ConnectToLoginServer();
    }

    public Client(Process process, Account account)
    {
        ClientProcess = process;
        Account = account;

        ConnectToClientProcess();
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
            try
            {
                Random rnd = new Random();

                byte randomPort = (byte)rnd.Next(0, 9);
                
                await Session.ConnectAsync(Server.GatewayIp, 15100 + randomPort);
                await Session.SendAsync(MessageBuilder.MsgSend_AccountLoginRequest(Account.Login, Account.Password));
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
        StartTime = Stopwatch.GetTimestamp();

        if (Session != null)
        {
            Session.DisconnectAsync();
            Session.Dispose();

            CharacterHandler.Dispose();
        }

        Session = new(Account, this);
       
        Session.RegisterService(new GameService());

        if(ClientProcess != null)
        {
            Task.Run(async () =>
            {
                while (!ClientProcess.HasExited && CharacterHandler.GetGameState() != Enums.GameState.GAME_STATE_INGAME)
                {
                    if (Win32Api.Read4Byte(ClientProcess.Handle, GetRecvHookPointer()) != MailslotRecvHookPtr.ToInt32())
                    {
                        await PatchRecvMailslot();
                        await PatchSendMailslot();
                    }
                        
                }

                bool threadSuspend = false;

                while(!ClientProcess.HasExited)
                {
                    foreach (ProcessThread thd in ClientProcess.Threads)
                    {
                        IntPtr threadHandle = Win32Api.OpenThread(Win32Api.ThreadAccess.SUSPEND_RESUME, false, (uint)thd.Id);

                        if(thd.StartAddress == new IntPtr(0x00000000))
                        {
                            if(threadSuspend)
                            {
                                Win32Api.ResumeThread(threadHandle);
                                threadSuspend = false;
                            }
                            else
                            {
                                Win32Api.SuspendThread(threadHandle);
                                threadSuspend = true;
                            }
                        }
                    }
                    await Task.Delay(1);
                }
            });
        }
        else
        {
            Session.RegisterService(new LoginService());
            Session.RegisterService(new HandshakeService());
          
        }
    }

    private void ConnectToClientProcess()
    {
        CreateNewSessionInstance();

        var clientAgentTask = Task.Run(async () =>
        {
            try
            {
                await HandleClientProcess();
                await Session.RunAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{SpanExtensions.GetTimeStamp()} '{Account.Login}' {ex.Message}");
            }
        });

        clientAgentTask.ContinueWith((completedTask) =>
        {
            ClientHandler.Inject(Server, Account);
        });
    }

    private async Task IntroSkip()
    {
        //while (Win32Api.Read4Byte(ClientProcess.Handle, 0xF5F2D8) == 0)
        //  await Task.Delay(100);

        //await Task.Delay(3000);

        //ExecuteRemoteCode("60" +
        //      "C6810C01000001" +
        //      "FF35" + Win32Api.AlignDWORD(0xF7E344) +  //KO_PTR_LOGIN
        //      "BF" + Win32Api.AlignDWORD(0x6A29C0) +    //KO_DIK_ENTER_FNC
        //      "FFD7" +
        //      "83C404" +
        //      "B001" +
        //      "61" +
        //      "C20400"
        //      );

        await Task.Delay(1000);
    }

    private async Task Login()
    {
        Thread.Sleep(5000);


        var CUILoginIntro = Win32Api.Read4Byte(ClientProcess.Handle, 0xF7E344) + 0x2C;

        // while (Win32Api.Read4Byte(ClientProcess.Handle, CUILoginIntro) == 0)
        //   await Task.Delay(100);

        var CLoginIDN3UIEdit = Win32Api.Read4Byte(ClientProcess.Handle, CUILoginIntro) + 0x10C;
        var CLoginID = Win32Api.Read4Byte(ClientProcess.Handle, CLoginIDN3UIEdit) + 0x140;
        var CLoginIDLength = Win32Api.Read4Byte(ClientProcess.Handle, CLoginIDN3UIEdit) + 0x150;

        var CLoginPWN3UIEdit = Win32Api.Read4Byte(ClientProcess.Handle, CUILoginIntro) + 0x110;
        var CLoginPW = Win32Api.Read4Byte(ClientProcess.Handle, CLoginPWN3UIEdit) + 0x128;
        var CLoginPWLength = Win32Api.Read4Byte(ClientProcess.Handle, CLoginPWN3UIEdit) + 0x138;

        Win32Api.WriteString(ClientProcess.Handle, new IntPtr(CLoginID), Account.Login);
        Win32Api.Write4Byte(ClientProcess.Handle, new IntPtr(CLoginIDLength), Account.Login.Length);

        Win32Api.WriteString(ClientProcess.Handle, new IntPtr(CLoginPW), "ttCnkoSh1993");
        Win32Api.Write4Byte(ClientProcess.Handle, new IntPtr(CLoginPWLength), 12);

        //string CodeString = "60" +
        //    "8B0D" + Win32Api.AlignDWORD(0xF7E344) +        //KO_PTR_LOGIN
        //    "BF" + Win32Api.AlignDWORD(int.Parse("006B0BB0", System.Globalization.NumberStyles.HexNumber)) +
        //    "FFD7" +
        //    "8B0D" + Win32Api.AlignDWORD(0xF7E344) +        //KO_PTR_LOGIN
        //    "BF" + Win32Api.AlignDWORD(int.Parse("006AE6E0", System.Globalization.NumberStyles.HexNumber)) +
        //    "FFD7" +
        //    "61" +
        //    "C3";

        return;


        string CodeString = 
            "8B0D" + Win32Api.AlignDWORD(0xF7E344)
            + "BF" + Win32Api.AlignDWORD(0x6B0BB0)
            + "FFD7"
            + "8B0D44E3F700"
            + "BF" + Win32Api.AlignDWORD(0x6AE6E0)
            + "FFD7"
            + "61" 
            + "C3";

        ExecuteRemoteCode(CodeString);

        await Task.Delay(1000);
    }

    private async Task ServerSelect()
    {
        var loginStartTime = Environment.TickCount;

        var CUILoginIntro = Win32Api.Read4Byte(ClientProcess.Handle, 0xF7E344) + 0x2C;

        while (Win32Api.Read4Byte(ClientProcess.Handle, Win32Api.Read4Byte(ClientProcess.Handle, CUILoginIntro) + 0x268) != 1)
        {
            if (Environment.TickCount - loginStartTime >= 15000)
            {
                loginStartTime = Environment.TickCount;
                await Login();
            }

            await Task.Delay(100);
        }

        ExecuteRemoteCode("60" +
            "A1" + Win32Api.AlignDWORD(0xF7E344) +                         //KO_PTR_DLG
            "8B402C" +
            "C780" + Win32Api.AlignDWORD(0x410) + Win32Api.AlignDWORD(1) + //KO_OFF_SERVER_INDEX
            "8B01" +
            "8B0D" + Win32Api.AlignDWORD(0xF7E344) +                       //KO_PTR_DLG
            "BF" + Win32Api.AlignDWORD(0x6B14A0) +                         //KO_PTR_SELECT_SERVER
            "FFD7" +
            "61" +
            "C3");

        await Task.Delay(1000);
    }

    private async Task CharacterSelect()
    {
        //while (Win32Api.Read4Byte(ClientProcess.Handle, Win32Api.Read4Byte(ClientProcess.Handle, 0xF5F2C4) + 0x3C) == 0)
          //  await Task.Delay(100);

        await Task.Delay(3000);

        ExecuteRemoteCode("60" +
             "8B0D" + Win32Api.AlignDWORD(0xF7E350) + //CGameProcIntroChrSelect
             "BF" + Win32Api.AlignDWORD(0x6B2890) + //Select Character Function
             "FFD7" +
             "61" +
             "C3");
    }
    
    private async Task HandleClientProcess()
    {
        await IntroSkip();
        await Login();
        await ServerSelect();
        await CharacterSelect();
    }

    public String ByteToHex(byte[] pByte)
    {
        return BitConverter.ToString(pByte).Replace("-", "");
    }

    public IntPtr GetRecvPointer()
    {
        return new IntPtr(0xF7E36C); //KO_PTR_DLG
    }
    
    public IntPtr GetRecvHookPointer()
    {
        return new IntPtr(Win32Api.Read4Byte(ClientProcess.Handle, Win32Api.Read4Byte(ClientProcess.Handle, GetRecvPointer())) + 0x8);
    }

    public void ExecuteRemoteCode(String Code)
    {
        byte[] CodeByte = Win32Api.StringToByte(Code);
        IntPtr CodePtr = Win32Api.VirtualAllocEx(ClientProcess.Handle, IntPtr.Zero, CodeByte.Length, Win32Api.MEM_COMMIT, Win32Api.PAGE_EXECUTE_READWRITE);

        if (CodePtr != IntPtr.Zero)
        {
            Win32Api.WriteProcessMemory(ClientProcess.Handle, CodePtr, CodeByte, CodeByte.Length, 0);
            IntPtr Thread = Win32Api.CreateRemoteThread(ClientProcess.Handle, IntPtr.Zero, 0, CodePtr, IntPtr.Zero, 0, IntPtr.Zero);

            if (Thread != IntPtr.Zero)
                Win32Api.WaitForSingleObject(Thread, uint.MaxValue);

            Win32Api.CloseHandle(Thread);
        }

        Debug.WriteLine(CodePtr);

        //Win32Api.VirtualFreeEx(ClientProcess.Handle, CodePtr, 0, Win32Api.MEM_RELEASE);
    }

    public Task PatchRecvMailslot()
    {
        if (MailslotRecvFuncPtr == IntPtr.Zero)
        {
            UnicodeEncoding UnicodeEncoding = new UnicodeEncoding();

            IntPtr CreateFilePtr = Win32Api.GetProcAddress(Win32Api.GetModuleHandle("kernel32.dll"), "CreateFileW");
            IntPtr WriteFilePtr = Win32Api.GetProcAddress(Win32Api.GetModuleHandle("kernel32.dll"), "WriteFile");
            IntPtr CloseFilePtr = Win32Api.GetProcAddress(Win32Api.GetModuleHandle("kernel32.dll"), "CloseHandle");

            String MailslotRecvName = @"\\.\mailslot\KOF_RECV\" + ClientProcess.Id;

            if (MailslotRecvPtr == IntPtr.Zero)
                MailslotRecvPtr = Win32Api.CreateMailslot(MailslotRecvName, 0, 0, IntPtr.Zero);

            MailslotRecvFuncPtr = Win32Api.VirtualAllocEx(ClientProcess.Handle, IntPtr.Zero, 1, Win32Api.MEM_COMMIT, Win32Api.PAGE_EXECUTE_READWRITE);

            byte[] MailslotRecvNameByte = UnicodeEncoding.GetBytes(MailslotRecvName);

            Win32Api.WriteProcessMemory(ClientProcess.Handle, MailslotRecvFuncPtr + 0x400, MailslotRecvNameByte, MailslotRecvNameByte.Length, 0);

            Win32Api.Patch(ClientProcess.Handle, MailslotRecvFuncPtr,
                "55" +
                "8BEC" +
                "83C4F4" +
                "33C0" +
                "8945FC" +
                "33D2" +
                "8955F8" +
                "6A00" +
                "6880000000" +
                "6A03" +
                "6A00" +
                "6A01" +
                "6800000040" +
                "68" + Win32Api.AlignDWORD(MailslotRecvFuncPtr + 0x400) +
                "E8" + Win32Api.AlignDWORD(Win32Api.AddressDistance(MailslotRecvFuncPtr + 0x27, CreateFilePtr)) +
                "8945F8" +
                "6A00" +
                "8D4DFC" +
                "51" +
                "FF750C" +
                "FF7508" +
                "FF75F8" +
                "E8" + Win32Api.AlignDWORD(Win32Api.AddressDistance(MailslotRecvFuncPtr + 0x3E, WriteFilePtr)) +
                "8945F4" +
                "FF75F8" +
                "E8" + Win32Api.AlignDWORD(Win32Api.AddressDistance(MailslotRecvFuncPtr + 0x49, CloseFilePtr)) +
                "8BE5" +
                "5D" +
                "C3");
        }

        Debug.WriteLine(MailslotRecvFuncPtr);

        if(MailslotRecvHookPtr != IntPtr.Zero)
            Win32Api.VirtualFreeEx(ClientProcess.Handle, MailslotRecvHookPtr, 0, Win32Api.MEM_RELEASE);

        MailslotRecvHookPtr = Win32Api.VirtualAllocEx(ClientProcess.Handle, IntPtr.Zero, 1, Win32Api.MEM_COMMIT, Win32Api.PAGE_EXECUTE_READWRITE);

        Win32Api.Patch(ClientProcess.Handle, MailslotRecvHookPtr,
            "55" +
            "8BEC" +
            "83C4F8" +
            "53" +
            "8B4508" +
            "83C004" +
            "8B10" +
            "8955FC" +
            "8B4D08" +
            "83C108" +
            "8B01" +
            "8945F8" +
            "FF75FC" +
            "FF75F8" +
            "E8" + Win32Api.AlignDWORD(Win32Api.AddressDistance(MailslotRecvHookPtr + 0x23, MailslotRecvFuncPtr)) +
            "83C408" +
            "8B0D" + Win32Api.AlignDWORD(GetRecvPointer()) +
            "FF750C" +
            "FF7508" +
            "B8" + Win32Api.AlignDWORD(Win32Api.Read4Byte(ClientProcess.Handle, GetRecvHookPointer())) +
            "FFD0" +
            "5B" +
            "59" +
            "59" +
            "5D" +
            "C20800");

        uint MemoryProtection;
        Win32Api.VirtualProtectEx(ClientProcess.Handle, GetRecvHookPointer(), 1, Win32Api.PAGE_EXECUTE_READWRITE, out MemoryProtection);
        Win32Api.Write4Byte(ClientProcess.Handle, GetRecvHookPointer(), MailslotRecvHookPtr.ToInt32());
        Win32Api.VirtualProtectEx(ClientProcess.Handle, GetRecvHookPointer(), 1, MemoryProtection, out MemoryProtection);

        Debug.WriteLine("Recv packet hooked. Address: {0}", MailslotRecvHookPtr);

        return Task.CompletedTask;
    }

    public Task PatchSendMailslot()
    {
        UnicodeEncoding UnicodeEncoding = new UnicodeEncoding();

        IntPtr CreateFilePtr = Win32Api.GetProcAddress(Win32Api.GetModuleHandle("kernel32.dll"), "CreateFileW");
        IntPtr WriteFilePtr = Win32Api.GetProcAddress(Win32Api.GetModuleHandle("kernel32.dll"), "WriteFile");
        IntPtr CloseFilePtr = Win32Api.GetProcAddress(Win32Api.GetModuleHandle("kernel32.dll"), "CloseHandle");

        String MailslotSendName = @"\\.\mailslot\KOF_SEND\" + ClientProcess.Id;

        if (MailslotSendPtr == IntPtr.Zero)
            MailslotSendPtr = Win32Api.CreateMailslot(MailslotSendName, 0, 0, IntPtr.Zero);

        MailslotSendHookPtr = Win32Api.VirtualAllocEx(ClientProcess.Handle, MailslotSendHookPtr, 1, Win32Api.MEM_COMMIT, Win32Api.PAGE_EXECUTE_READWRITE);

        byte[] MailslotSendNameByte = UnicodeEncoding.GetBytes(MailslotSendName);

        Win32Api.WriteProcessMemory(ClientProcess.Handle, MailslotSendHookPtr + 0x400, MailslotSendNameByte, MailslotSendNameByte.Length, 0);

        Win32Api.Patch(ClientProcess.Handle, MailslotSendHookPtr, "608B4424248905" +
        Win32Api.AlignDWORD(MailslotSendHookPtr + 0x100) + "8B4424288905" +
        Win32Api.AlignDWORD(MailslotSendHookPtr + 0x104) + "3D004000007D3D6A0068800000006A036A006A01680000004068" +
        Win32Api.AlignDWORD(MailslotSendHookPtr + 0x400) + "E8" +
        Win32Api.AlignDWORD(Win32Api.AddressDistance(MailslotSendHookPtr + 0x33, CreateFilePtr)) + "83F8FF741C6A005490FF35" +
        Win32Api.AlignDWORD(MailslotSendHookPtr + 0x104) + "FF35" +
        Win32Api.AlignDWORD(MailslotSendHookPtr + 0x100) + "50E8" +
        Win32Api.AlignDWORD(Win32Api.AddressDistance(MailslotSendHookPtr + 0x4E, WriteFilePtr)) + "50E8" +
        Win32Api.AlignDWORD(Win32Api.AddressDistance(MailslotSendHookPtr + 0x54, CloseFilePtr)) + "61558BEC6AFF68" +
        Win32Api.AlignDWORD(Win32Api.Read4Byte(ClientProcess.Handle, 0x5ED620 + 0x4)) + "E9" +
        Win32Api.AlignDWORD(Win32Api.AddressDistance(MailslotSendHookPtr + 0x61, new IntPtr(0x5ED620 + 0x7))));

        Win32Api.Patch(ClientProcess.Handle, new IntPtr(0x5ED620), "E9" + Win32Api.AlignDWORD(Win32Api.AddressDistance(new IntPtr(0x5ED620), MailslotSendHookPtr)));

        Debug.WriteLine("Send packet hooked. Address: {0}", MailslotRecvHookPtr);

        return Task.CompletedTask;
    }

    public bool IsClientConnectionLost()
    {
        //KO_PTR_PKT
        return Win32Api.Read4Byte(ClientProcess.Handle, Win32Api.Read4Byte(ClientProcess.Handle, 0xF7E32C) + 0xA0) == 0 ? true : false;
    }


}
