using KOF.Core;
using KOF.Core.Handlers;
using KOF.Database.Models;
using KOF.Core.Models;
using System.Text.Json;
using KOF.Cryptography;
using KOF.Core.Enums;
using System.Reflection;
using System.Diagnostics;
using KOF.Database;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Text;
using AutoUpdaterDotNET;
using System.Net;
using System.Windows.Forms;

namespace KOF.UI.Forms;

public partial class Main : Form
{
    private static List<ClientController> ControllerList { get; set; } = new();

    private static List<Client> FollowableClientList { get { return new List<Client>(ClientHandler.ClientList); } }

    public Main()
    {
        InitializeComponent();

        typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.SetProperty, null,
        AccountDataGrid, new object[] { true });

        typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.SetProperty, null,
        ClientListDataGrid, new object[] { true });
    }

    private void ApplicationLoad()
    {
        ServerListComboBox.DataSource = ClientHandler.ServerList;
        ServerListComboBox.DisplayMember = "Name";

        ClientListDataGrid.DataSource = ClientHandler.ClientList;

        AccountDataGrid.DataSource = ClientHandler.AccountList;

        DataGridViewComboBoxColumn CharacterColumn = new DataGridViewComboBoxColumn();

        CharacterColumn.Name = "Character";
        CharacterColumn.Width = 150;
        CharacterColumn.DisplayMember = "Name";
        CharacterColumn.FlatStyle = FlatStyle.Flat;

        AccountDataGrid.Columns.Add(CharacterColumn);

        AccountDataGrid.Columns[0].Visible = false;
        AccountDataGrid.Columns[0].ReadOnly = true;
        AccountDataGrid.Columns[1].Width = 109;
        AccountDataGrid.Columns[1].ReadOnly = true;
        AccountDataGrid.Columns[2].ReadOnly = true;
        AccountDataGrid.Columns[2].Width = 109;
        AccountDataGrid.Columns[3].ReadOnly = true;
        AccountDataGrid.Columns[3].Width = 75;
        AccountDataGrid.Columns[4].ReadOnly = true;
        AccountDataGrid.Columns[4].Width = 50;
        AccountDataGrid.Columns[5].ReadOnly = true;
        AccountDataGrid.Columns[5].Width = 75;
        AccountDataGrid.Columns[6].ReadOnly = true;

        foreach (DataGridViewRow row in AccountDataGrid.Rows)
        {
            var account = (Account)row.DataBoundItem;

            DataGridViewComboBoxCell selectCharacter = (row.Cells[7] as DataGridViewComboBoxCell)!;

            var characterData = JsonSerializer.Deserialize<List<Lobby>>(account.CharacterData)!;

            selectCharacter.DataSource = characterData;

            if (characterData.Any(x => x.Name == account.Character))
                selectCharacter.Value = account.Character;
        }

        ClientListDataGrid.Columns[0].Width = 100;
        ClientListDataGrid.Columns[1].Width = 50;
        ClientListDataGrid.Columns[2].Width = 50;
        ClientListDataGrid.Columns[3].Width = 50;
        ClientListDataGrid.Columns[4].Width = 100;
        ClientListDataGrid.Columns[5].Width = 100;
        ClientListDataGrid.Columns[6].Width = 100;
        ClientListDataGrid.Columns[7].Width = 100;

        ClientListDataGrid.Columns[8].Width = 50;
        ClientListDataGrid.Columns[9].Width = 50;
        ClientListDataGrid.Columns[10].Width = 50;

        FollowSelect.DataSource = FollowableClientList;
        FollowSelect.DisplayMember = "Name";

        var nationList = new List<KeyValuePair<byte, string>>
            {
                new KeyValuePair<byte, string>(byte.MaxValue, "Select nation"),
                new KeyValuePair<byte, string>(1, "Karus"),
                new KeyValuePair<byte, string>(2, "El Morad"),
            };

        NationComboBox.DataSource = nationList;
        NationComboBox.DisplayMember = "Value";
        NationComboBox.ValueMember = "Key";
        NationComboBox.SelectedValue = byte.MaxValue;
    }

    private void MainModern_Load(object sender, EventArgs e)
    {
        AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;

        AutoUpdater.Synchronous = true;
        AutoUpdater.Mandatory = true;
        AutoUpdater.UpdateMode = Mode.Forced;
        //AutoUpdater.DownloadPath = $"{Application.StartupPath}/updates";
        AutoUpdater.Start("https://download.kofbot.com/clientless/manifest.xml");

        ApplicationLoad();
    }

    private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
    {
        if (args.Error == null)
        {
            if (args.IsUpdateAvailable)
            {
                this.Hide();

                DialogResult dialogResult;
                if (args.Mandatory.Value)
                {
                    dialogResult =
                        MessageBox.Show(
                            $@"There is new version {args.CurrentVersion} available. You are using version {args.InstalledVersion}. This is required update. Press Ok to begin updating the application.", @"Update Available",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                }
                else
                {
                    dialogResult =
                        MessageBox.Show(
                            $@"There is new version {args.CurrentVersion} available. You are using version {args.InstalledVersion}. Do you want to update the application now?", @"Update Available",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);
                }

                // Uncomment the following line if you want to show standard update dialog instead.
                // AutoUpdater.ShowUpdateForm(args);

                if (dialogResult.Equals(DialogResult.Yes) || dialogResult.Equals(DialogResult.OK))
                {
                    try
                    {
                        if (AutoUpdater.DownloadUpdate(args))
                        {
                            Application.Exit();
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }
        else
        {
            if (args.Error is WebException)
            {
                MessageBox.Show(
                    @"There is a problem reaching update server. Please check your internet connection and try again later.",
                    @"Update Check Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(args.Error.Message,
                    args.Error.GetType().ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void UpdateTimer_Tick(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in AccountDataGrid.Rows)
        {
            var account = (Account)row.DataBoundItem;

            if (account == null)
                continue;

            var characterData = JsonSerializer.Deserialize<List<Lobby>>(account.CharacterData)!;

            if (characterData == null)
                continue;

            Color RowColor = ColorTranslator.FromHtml(account.GroupColor);

            row.DefaultCellStyle.BackColor = RowColor;

            if (account.Character != "")
            {
                var character = characterData.FirstOrDefault(x => x.Name == account.Character);

                if(character != null)
                {
                    row.Cells[4].Value = character.Level;
                    row.Cells[5].Value = Character.GetRepresentClassName(character.Class);
                }  
            }

            if (row.Cells[7].GetType() == typeof(DataGridViewComboBoxCell))
            {
                DataGridViewComboBoxCell selectCharacter = (DataGridViewComboBoxCell)row.Cells[7];

                if(selectCharacter != null)
                {
                    if (selectCharacter.Value == null)
                    {
                        selectCharacter.DataSource = characterData;

                        if (characterData.Any(x => x.Name == account.Character))
                            selectCharacter.Value = account.Character;
                    }
                    else
                    {
                        if (characterData.Any(x => x.Name == account.Character) &&
                            selectCharacter.Value.ToString() != account.Character)
                        {
                            selectCharacter.Value = account.Character;
                            selectCharacter.DataSource = characterData;
                        }
                    }

                    selectCharacter.Style.BackColor = RowColor;
                }
            }

            var client = ClientHandler.ClientList.FirstOrDefault(x => x != null && x.Account == account);

            if(client != null)
            {
                if(client.CharacterHandler.GetGameState() == GameState.GAME_STATE_INGAME)
                {
                    row.Cells[6].Style.ForeColor = Color.Black;
                    row.Cells[6].Style.BackColor = Color.LimeGreen;
                    row.Cells[6].Value = "Online";
                }
                else if (client.CharacterHandler.GetGameState() == GameState.GAME_STATE_CONNECTED)
                {
                    row.Cells[6].Style.ForeColor = Color.White;
                    row.Cells[6].Style.BackColor = Color.Blue;
                    row.Cells[6].Value = "Loading";
                }
                else
                {
                    row.Cells[6].Style.ForeColor = Color.Black;
                    row.Cells[6].Style.BackColor = Color.Orange;
                    row.Cells[6].Value = "Connecting";
                }
            }
            else
            {
                row.Cells[6].Style.ForeColor = Color.White;
                row.Cells[6].Style.BackColor = Color.Gray;
                row.Cells[6].Value = "Offline";
            }
        }

        foreach (DataGridViewRow row in ClientListDataGrid.Rows)
        {
            var client = (Client)row.DataBoundItem;

            if (client == null) continue;
            if (client.CharacterHandler.Controller == null) continue;

            Color Color = ColorTranslator.FromHtml(client.CharacterHandler.Controller.GetControl("GroupColor", "#FFFFFF"));

            row.DefaultCellStyle.BackColor = Color;
        }

        if (FollowSelect.Items.Count != FollowableClientList.Count)
            FollowSelect.DataSource = FollowableClientList;

        AccountDataGrid.Refresh();
        ClientListDataGrid.Refresh();

       /* if(ClientHandler.ClientList.Count == 0)
        {
            DiscordRpcClient.SetPresence(new RichPresence()
            {
                Details = "Idle",
                State = $"Waiting a start",
                Assets = new Assets()
                {
                    LargeImageKey = "kof1",
                    LargeImageText = "Waiting a start",
                    SmallImageKey = "kof1"
                }
            });
        }
        else
        {
            DiscordRpcClient.SetPresence(new RichPresence()
            {
                Details = "Online",
                State = $"Working",
                Assets = new Assets()
                {
                    LargeImageKey = "kof1",
                    LargeImageText = $"Working with {ClientHandler.ClientList.Count} client",
                    SmallImageKey = "kof1"
                }
            });
        }*/
    }

    private void AccountDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        if (((DataGridView)sender).CurrentCell.ColumnIndex == 4) 
        {
            ComboBox? cb = e.Control as ComboBox;

            if (cb != null)
            {
                cb.SelectionChangeCommitted -= new EventHandler(ServerSelectComboBox_SelectedIndexChanged!);
                cb.SelectionChangeCommitted += new EventHandler(ServerSelectComboBox_SelectedIndexChanged!);
            }
        }

        if (((DataGridView)sender).CurrentCell.ColumnIndex == 7)
        {
            ComboBox? cb = e.Control as ComboBox;

            if (cb != null)
            {
                //cb.MouseDown -= new MouseEventHandler(CharacterSelectComboBox_MouseDown!);
                //cb.MouseDown += new MouseEventHandler(CharacterSelectComboBox_MouseDown!);

                cb.SelectionChangeCommitted -= new EventHandler(CharacterSelectComboBox_SelectedIndexChanged!);
                cb.SelectionChangeCommitted += new EventHandler(CharacterSelectComboBox_SelectedIndexChanged!);
            }
        }
    }

    private void ServerSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        var currentcell = AccountDataGrid.CurrentCellAddress;

        var sendingCB = sender as DataGridViewComboBoxEditingControl;

        var account = (Account)AccountDataGrid.Rows[currentcell.Y].DataBoundItem;

        if (account == null) return;

        account.Server = sendingCB?.EditingControlFormattedValue.ToString()!;

        SQLiteHandler.Update(account);
    }

    private void CharacterSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        var currentcell = AccountDataGrid.CurrentCellAddress;

        var sendingCB = sender as DataGridViewComboBoxEditingControl;

        var account = (Account)AccountDataGrid.Rows[currentcell.Y].DataBoundItem;

        if (account == null) return;

        account.Character = sendingCB?.EditingControlFormattedValue.ToString()!;

        SQLiteHandler.Update(account);
    }

    /*private void CharacterSelectComboBox_MouseDown(object sender, MouseEventArgs e)
    {
        var currentcell = AccountDataGrid.CurrentCellAddress;

        var sendingCB = sender as DataGridViewComboBoxEditingControl;

        var account = (Account)AccountDataGrid.Rows[currentcell.Y].DataBoundItem;

        if (account == null) return;

        switch (e.Button)
        {

            case MouseButtons.Left:
                // Left click
                break;

            case MouseButtons.Right:
                Debug.WriteLine(account.Login);

                var characterCreate = new CharacterCreate();
                characterCreate.Show();
                break;

        }
    }*/

    private void LoginServerButton_Click(object sender, EventArgs e)
    {

        foreach (DataGridViewRow row in AccountDataGrid.SelectedRows)
        {
            var account = (Account)row.DataBoundItem;
            var server = ClientHandler.ServerList.FirstOrDefault(x => x.Name == account.Server);

            if (server == null) return;

            if(!ClientHandler.Ready)
            {
                MessageBox.Show("Application is loading, please wait.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ClientHandler.ClientList.Any(x => x != null && x.Account.Id == account.Id))
                continue;

            var client = ClientHandler.Start(server, account);

            var controller = new ClientController(client);

            ControllerList.Add(controller);
        }
    }

    private void DisconnectButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in AccountDataGrid.SelectedRows)
        {
            var account = (Account)row.DataBoundItem;

            var client = ClientHandler.ClientList.FirstOrDefault(x => x != null && x.Account.Id == account.Id);

            if (client == null) return;

            ClientHandler.Close(client);
        }
    }

    private void ClientListDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1) return;

        var client =  (Client)ClientListDataGrid.Rows[e.RowIndex].DataBoundItem;

        if (client == null) return;

        var character = client.CharacterHandler;

        if (character.GetGameState() != GameState.GAME_STATE_INGAME)
        {
            MessageBox.Show("Character is loading, please wait.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var controller = ControllerList.FirstOrDefault(x => x.Client == client);

        if (controller != null)
            controller.Show();
    }

    private void AddAccountButton_Click(object sender, EventArgs e)
    {
        if (AccountIdTextBox.Text == "" || PasswordTextBox.Text == "" || NationComboBox.SelectedIndex == byte.MaxValue || ServerListComboBox.SelectedIndex == -1) return;

        var server = (Server)ServerListComboBox.SelectedItem;
        if (server == null) return;

        var account = new Account()
        {
            Login = AccountIdTextBox.Text,
            Password = PasswordTextBox.Text.PasswordHash(),
            Server = server.Name,
            NationId = (byte)NationComboBox.SelectedValue
        };

        account.Id = SQLiteHandler.Insert(account);

        ClientHandler.AccountList.Add(account);
    }

    private void DeleteAccountButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in AccountDataGrid.SelectedRows)
        {
            var account = (Account)row.DataBoundItem;

            if (account == null) return;

            SQLiteHandler.Delete(account);
            ClientHandler.AccountList.Remove(account);
        }
    }

    private void CloseClientButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in ClientListDataGrid.SelectedRows)
        {
            var client = (Client)row.DataBoundItem;

            if (client == null) return;

            ClientHandler.Close(client);
        }
    }

    private void SetFollowButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in ClientListDataGrid.SelectedRows)
        {
            var client = (Client)row.DataBoundItem;

            if (client == null) return;

            var character = client.CharacterHandler;

            if (character.GetGameState() != GameState.GAME_STATE_INGAME)
                continue;

            var followSellect = (Client)FollowSelect.SelectedItem;

            if(followSellect != null)
            {
                if(client.Character.Name != followSellect.Character.Name)
                    client.CharacterHandler.Controller.SetControl("Follow", followSellect.Name);
                else
                    client.CharacterHandler.Controller.SetControl("Follow", "");
            }
        }
    }

    private void RunRouteButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in ClientListDataGrid.SelectedRows)
        {
            var client = (Client)row.DataBoundItem;

            if (client == null) return;

            var characterHandler = client.CharacterHandler;

            if (characterHandler.GetGameState() != GameState.GAME_STATE_INGAME)
                continue;

            characterHandler.RouteQueue.Clear();

            var selectedRoute = SQLiteHandler.Table<Route>().SingleOrDefault(x => x.Id == client.CharacterHandler.Controller.GetControl("SelectedRoute", 0));

            if (selectedRoute != null && selectedRoute.Zone == client.Character.Zone)
                client.CharacterHandler.Route(JsonSerializer.Deserialize<List<RouteData>>(selectedRoute.Data)!);
        }
    }

    private void AccountDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1) return;

        var account = (Account)AccountDataGrid.Rows[e.RowIndex].DataBoundItem;

        if (account == null) return;

        var characterData = JsonSerializer.Deserialize<List<Lobby>>(account.CharacterData)!;

        if (characterData.Count == 0)
        {
            MessageBox.Show("You must login first to create a character", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;          
        }

        if (characterData.Any(x => x.Name == ""))
        {
            CharacterCreate characterCreateWindow = new CharacterCreate(account.Id);
            characterCreateWindow.ShowDialog();
        }
        else
        {
            MessageBox.Show("All character slots are full", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
    }

    private void TownButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in ClientListDataGrid.SelectedRows)
        {
            var client = (Client)row.DataBoundItem;

            if (client == null) return;

            var character = client.CharacterHandler;

            if (character.GetGameState() != GameState.GAME_STATE_INGAME)
                continue;

            if (client.CharacterHandler.IsRouting())
                continue;

            client.CharacterHandler.Town();
        }
    }

    private void ResetCharacterButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in AccountDataGrid.SelectedRows)
        {
            var account = (Account)row.DataBoundItem;

            if (account == null) return;

            account.Character = "";
            account.CharacterData = "[]";

            SQLiteHandler.Update(account);

            var client = ClientHandler.ClientList.FirstOrDefault(x => x != null && x.Account.Id == account.Id);

            if (client != null)
                ClientHandler.Close(client);
        }

        ClientHandler.AccountList.ResetBindings();
    }

    private void ClearFollowButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in ClientListDataGrid.SelectedRows)
        {
            var client = (Client)row.DataBoundItem;

            if (client == null) return;

            var character = client.CharacterHandler;

            if (character.GetGameState() != GameState.GAME_STATE_INGAME)
                continue;

            client.CharacterHandler.Controller.SetControl("Follow", "");
        }
    }

    private void SetGroupColorButton_Click(object sender, EventArgs e)
    {
        if (GroupColorDialog.ShowDialog() == DialogResult.OK)
        {
            foreach (DataGridViewRow row in ClientListDataGrid.SelectedRows)
            {
                var client = (Client)row.DataBoundItem;

                if (client == null) return;

                if (client.CharacterHandler.Controller == null) return;

                string colorCode = "#" + GroupColorDialog.Color.R.ToString("X2") + GroupColorDialog.Color.G.ToString("X2") + GroupColorDialog.Color.B.ToString("X2");

                client.CharacterHandler.Controller.SetControl("GroupColor", colorCode);

                var account = ClientHandler.AccountList.FirstOrDefault(x => x.Id == client.Account.Id)!;

                if(account != null)
                {
                    account.GroupColor = colorCode;
                    SQLiteHandler.Update(account);
                }
            }
        }
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason != CloseReason.UserClosing)
            return;

        if (MessageBox.Show("Do you want to close this application?", "Exit", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
        {
            e.Cancel = true;
        }
    }

    private void Main_Resize(object sender, EventArgs e)
    {
        //if the form is minimized  
        //hide it from the task bar  
        //and show the system tray icon (represented by the NotifyIcon control)  
        if (this.WindowState == FormWindowState.Minimized)
        {
            Hide();
            SystemTrayNotify.Visible = true;
        }
    }

    private void SystemTrayNotify_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        Show();
        this.WindowState = FormWindowState.Normal;
        SystemTrayNotify.Visible = false;
    }

    private void StartClientButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in AccountDataGrid.SelectedRows)
        {
            var account = (Account)row.DataBoundItem;
            var server = ClientHandler.ServerList.FirstOrDefault(x => x.Name == account.Server);

            if (server == null) return;

            if (!ClientHandler.Ready)
            {
                //MessageBox.Show("Application is loading, please wait.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }

            if (ClientHandler.ClientList.Any(x => x != null && x.Account.Id == account.Id))
                continue;

            var client = ClientHandler.Inject(server, account);

            var controller = new ClientController(client);
            
            ControllerList.Add(controller);
        }
        
    }
}
