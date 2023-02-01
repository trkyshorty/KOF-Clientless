using KOF.Core.Models;
using KOF.Data;
using KOF.Database;
using KOF.Database.Models;
using KOF.Zone;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace KOF.Core.Handlers;

public class C3DMap
{
    public int ZoneNumber { get; set; } = 0;

    public int MapLength { get; set; } = 0;

    public int Image { get; set; } = 0;

    public int ImageBig { get; set; } = 0;

    public object ObjectEventArray { get; set; } = 0;

    public int GetHeight(float x, float y) { return 1; }
}

public static class ClientHandler
{
    public static BindingList<Account> AccountList { get; set; } = new();

    public static BindingList<Server> ServerList { get; internal set; } = new();

    public static BindingList<Client> ClientList { get; internal set; } = new();

    public static List<CGameServerMap> ZoneList { get; internal set; } = new();

    public static bool Ready { get; internal set; } = false;

    static ClientHandler()
    {
        TableHandler.Load().ContinueWith((task) =>
        {
            Ready = true;
        });

        string localAppFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        SQLiteHandler.Connect($"{localAppFolder}\\kof.db");

        ServerList = new BindingList<Server>(SQLiteHandler.Table<Server>().ToList());
        AccountList = new BindingList<Account>(SQLiteHandler.Table<Account>().ToList());
    }

    public static Client Start(Server server, Account account)
    {
        var client = new Client(server, account);

        lock (ClientList)
            ClientList.Add(client);

        return client;
    }

    public static Task LoadZone(byte zoneId)
    {
        var zoneData = SQLiteHandler.Table<KOF.Database.Models.Zone>().FirstOrDefault(x => x.Id == zoneId)!;

        if (zoneData != null && !ZoneList.Any(x => x.GetZoneIndex() == zoneId))
        {
            var gameServerMap = new CGameServerMap();

            var opdPath = Directory.GetCurrentDirectory() + $"\\data\\map\\{zoneData.Opd}";
            var gtdPath = Directory.GetCurrentDirectory() + $"\\data\\map\\{zoneData.Gtd}";

            gameServerMap = gameServerMap.Load(opdPath, gtdPath);

            if(gameServerMap != null)
            {
                gameServerMap.SetMinimapImage(zoneData.Image, zoneData.ImageBig);
                gameServerMap.SetZoneIndex(zoneId);

                ZoneList.Add(gameServerMap);
            }
                
        }

        /*if (zoneData != null && !ZoneList.Any(x => x.ZoneNumber == zoneId))
        {
            var data = new C3DMap();

            data.Initialize(new Zone.Structs.ZoneInfo
            {
                Name = zoneData.Name,
                ZoneNumber = zoneId,
                SmdName = zoneData.Smd,
                Image = zoneData.Image,
                ImageBig = zoneData.ImageBig
            });

            ZoneList.Add(data);
        }*/

        return Task.CompletedTask;
    }

    public static void Close(Client client)
    {
        client.Close();

        lock (ClientList)
            ClientList.Remove(client);
    }

    public static Client Inject(Server server, Account account)
    {
        FileInfo fileInfo = new FileInfo("C:\\Users\\trkys\\OneDrive\\Masaüstü\\CNKO\\KnightOnLine.exe");
        ProcessStartInfo startInfo = new ProcessStartInfo(fileInfo.Name);

        startInfo.WorkingDirectory = fileInfo.DirectoryName;
        startInfo.Arguments = Process.GetCurrentProcess().Id.ToString();
        startInfo.UseShellExecute = true;

        Process? clientProcess = Process.Start(startInfo);

        if (clientProcess != null)
        {
            var client = new Client(clientProcess, account);

            lock (ClientList)
                ClientList.Add(client);

            return client;
        }

        return default!;
    }

}
