using KOF.Core;
using KOF.Core.Enums;
using KOF.Core.Handlers;
using KOF.Core.Models;
using KOF.Data;
using KOF.Data.Models;
using KOF.Database;
using KOF.Database.Models;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Text.Json;
using Control = System.Windows.Forms.Control;

namespace KOF.UI.Forms;

public partial class MapController : Form
{
    public Client Client { get; set; } = default!;
    private Character Character { get { return Client.Character; } }
    private CharacterHandler CharacterHandler { get { return Client.CharacterHandler; } }
    private Controller Controller { get { return CharacterHandler.Controller; } }
    private BindingList<Route> RouteList { get; set; } = new();
    private List<RouteData> RouteData { get; set; } = new();
    public MapController(Client client)
    {
        Client = client;

        InitializeComponent();
    }

    private void InitializeControl()
    {
        Control control = GetNextControl(this, true);

        do
        {
            control = GetNextControl(control, true);

            if (control != null)
            {
                if (control.GetType() == typeof(CheckBox))
                {
                    CheckBox checkBox = ((CheckBox)control);
                    bool value = Controller.GetControl(checkBox.Name, checkBox.Checked);

                    if (value != checkBox.Checked)
                        checkBox.Checked = value;
                }
                else if (control.GetType() == typeof(NumericUpDown))
                {
                    NumericUpDown numericUpDown = ((NumericUpDown)control);
                    decimal value = Controller.GetControl(numericUpDown.Name, numericUpDown.Value);

                    if (value != numericUpDown.Value)
                        numericUpDown.Value = value;
                }
                else if (control.GetType() == typeof(ComboBox))
                {
                    ComboBox comboBox = ((ComboBox)control);

                    if (comboBox.Name == "SelectedRouteComboBox")
                        continue;

                    string value = Controller.GetControl(comboBox.Name, comboBox.SelectedText);

                    if (value != comboBox.SelectedText)
                        comboBox.SelectedItem = value;
                }
            }
        }
        while (control != null);

        foreach (Route route in SelectedRouteComboBox.Items)
        {
            if (route.Id == Controller.GetControl("SelectedRoute", 0))
            {
                SelectedRouteComboBox.SelectedItem = route;

                SelectedRouteComboBox_SelectionChangeCommitted(SelectedRouteComboBox, EventArgs.Empty);
            }
        }
    }
    private void RoutePlanner_Load(object sender, EventArgs e)
    {
        RouteList = new BindingList<Route>(SQLiteHandler.Table<Route>().Where(x => x.Zone == Character.Zone).ToList());

        SelectedRouteComboBox.DataSource = RouteList;
        SelectedRouteComboBox.DisplayMember = "Name";

        var selectedTargetIds = JsonSerializer.Deserialize<List<int>>(Controller.GetControl("SelectedTargetList", "[]"))!;

        TargetCheckedListBox.DataSource = TableHandler.GetMonsterList()
            .FindAll(x => CharacterHandler.GetNpcList().Any(y => y.ProtoId == x.Id) || selectedTargetIds.Contains(x.Id))
            .GroupBy(p => p.Id)
            .Select(g => g.First())
            .ToList();

        TargetCheckedListBox.DisplayMember = "Name";

        for (int i = 0; i <= TargetCheckedListBox.Items.Count - 1; i++)
            TargetCheckedListBox.SetItemCheckState(i, CheckState.Checked);

        PlayerListBox.DataSource = CharacterHandler.GetPlayerList();
        PlayerListBox.DisplayMember = "Name";

        InitializeControl();
    }

    private void RenderTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (CharacterHandler == null || Character == null) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME) return;

            var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Character.Zone)!;
            if (zoneData == null) return;

            string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\image";

            Bitmap image = new Bitmap($"{dataDirectory}\\{zoneData.GetMinimapBigImage()}");

            using (Graphics graphic = Graphics.FromImage(image))
            {
                if (CharacterHandler != null && CharacterHandler.RouteQueue.Count > 0)
                {
                    var moveList = CharacterHandler.RouteQueue.ToList();

                    Vector3 prevPosition = GetWorldPositionToMinimap(Map, Character.X, Character.Y);

                    moveList.ForEach(x =>
                    {
                        Pen linePen = new Pen(Brushes.Orange);
                        linePen.Width = 3;
                        Vector3 position = GetWorldPositionToMinimap(Map, x.X, x.Y);
                        graphic.DrawLine(linePen, prevPosition.X, prevPosition.Y, position.X, position.Y);
                        prevPosition = new Vector3(position.X, position.Y, 0.0f);
                    });

                }

                Vector3 characterPosition = GetWorldPositionToMinimap(Map, Character.GetPosition().X, Character.GetPosition().Y);

                if (Character.GetMovePosition() != Vector3.Zero)
                {
                    Vector3 movePosition = GetWorldPositionToMinimap(Map, Character.GetMovePosition().X, Character.GetMovePosition().Y);

                    Pen linePen = new Pen(Brushes.Red);
                    linePen.Width = 3;
                    graphic.DrawLine(linePen, characterPosition.X, characterPosition.Y, movePosition.X, movePosition.Y);
                }

                CharacterHandler?.GetPlayerList().ForEach(x =>
                {
                    if (x == null) return;
                    Vector3 otherPlayerPosition = GetWorldPositionToMinimap(Map, x.GetPosition().X, x.GetPosition().Y);
                    graphic.FillRectangle(Brushes.DeepSkyBlue, otherPlayerPosition.X, otherPlayerPosition.Y, 4, 4);
                });

                CharacterHandler?.GetNpcList().ForEach(x =>
                {
                    if (x?.MonsterOrNpc == 1)
                    {
                        var monColor = Brushes.Red;

                        if (x.IsDead())
                            monColor = Brushes.Orange;

                        Vector3 otherMonsterPosition = GetWorldPositionToMinimap(Map, x.GetPosition().X, x.GetPosition().Y);
                        graphic.FillRectangle(monColor, otherMonsterPosition.X, otherMonsterPosition.Y, 4, 4);
                    }
                    else
                    {
                        if (x != null)
                        {
                            Vector3 otherNpcPosition = GetWorldPositionToMinimap(Map, x.GetPosition().X, x.GetPosition().Y);
                            graphic.FillRectangle(Brushes.DarkBlue, otherNpcPosition.X, otherNpcPosition.Y, 4, 4);
                        }
                    }
                });

                var attackRange = (int)(Controller?.GetControl("AttackRange", 65) / 2)! - 5;

                graphic.FillRectangle(Brushes.SpringGreen, characterPosition.X, characterPosition.Y, 4, 4);
                graphic.FillEllipse(new SolidBrush(Color.FromArgb(75, 0, 0, 255)), characterPosition.X - attackRange, characterPosition.Y - attackRange, attackRange * 2, attackRange * 2);

                if (Map.Image != null)
                    Map.Image.Dispose();

                Map.Image = image;
            }

            if (Controller != null && CharacterHandler != null)
            {
                var selectedTargetIds = JsonSerializer.Deserialize<List<int>>(Controller.GetControl("SelectedTargetList", "[]"))!;

                var monsterList = TableHandler.GetMonsterList()
                    .FindAll(x => CharacterHandler.GetNpcList().Any(y => y?.ProtoId == x.Id) || selectedTargetIds.Contains(x.Id))
                    .GroupBy(p => p.Id)
                    .Select(g => g.First())
                    .ToList();

                if (TargetCheckedListBox.Items.Count != monsterList.Count)
                {
                    TargetCheckedListBox.DataSource = monsterList;
                    TargetCheckedListBox.DisplayMember = "Name";
                }

                for (int i = 0; i <= TargetCheckedListBox.Items.Count - 1; i++)
                {
                    var target = (Monster)TargetCheckedListBox.Items[i];

                    if (selectedTargetIds.Contains(target.Id))
                        TargetCheckedListBox.SetItemCheckState(i, CheckState.Checked);
                    else
                        TargetCheckedListBox.SetItemCheckState(i, CheckState.Unchecked);
                }

                var playerList = CharacterHandler.GetPlayerList();

                if (PlayerListBox.Items.Count != playerList.Count)
                {
                    PlayerListBox.DataSource = playerList;
                    PlayerListBox.DisplayMember = "Name";
                }

            }


        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    public Vector3 GetMiniMapPositionToWorld(PictureBox Picture, float X, float Y)
    {
        if (Character == null) return new Vector3();

        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Character.Zone)!;
        if (zoneData == null) return new Vector3();

        float fWidth = (Picture.Right - Picture.Left);
        float fHeight = (Picture.Bottom - Picture.Top);

        Vector3 coordinate = new Vector3(
                (float)Math.Ceiling(X * (float)(zoneData.MapLength / fWidth)),
                (float)Math.Ceiling((fHeight - Y) * (float)(zoneData.MapLength / fHeight)),
                0.0f);

        coordinate.Z = zoneData.GetHeightBy2DPos(coordinate.X, coordinate.Y);

        return coordinate;
    }

    public Vector3 GetWorldPositionToMinimap(PictureBox Picture, float X, float Y)
    {
        if (Character == null) return new Vector3();

        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Character.Zone)!;

        if (zoneData == null) return new Vector3();

        float fWidth = (Picture.Right - Picture.Left) + 4;
        float fHeight = (Picture.Bottom - Picture.Top)  + 4;

        Vector3 coordinate = new Vector3(
                (float)Math.Ceiling(X / (float)(zoneData.MapLength / fWidth)),
                (float)Math.Ceiling(fHeight - (Y / (float)(zoneData.MapLength / fHeight))),
                0.0f);

        coordinate.Z = zoneData.GetHeightBy2DPos(X, Y);

        return coordinate;
    }

    private void Map_MouseDown(object sender, MouseEventArgs e)
    {
        if (Character == null) return;
        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Character.Zone)!;
        if (zoneData == null) return;

        CharacterHandler.RouteQueue.Clear();
        CharacterHandler.SelectTarget(-1);

        Vector3 mapPosition = GetMiniMapPositionToWorld(Map, e.X, e.Y);
        Character.SetMovePosition(mapPosition);
    }

    private void RoutePlanner_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.Z)
        {
            if (RouteData.Count > 0)
                RouteData.RemoveAt(RouteData.Count - 1);
        }

        if (e.KeyCode == Keys.Escape)
            RouteData.Clear();
    }

    private void RunRouteButton_Click(object sender, EventArgs e)
    {
        CharacterHandler.RouteQueue.Clear();

        CharacterHandler.Route(RouteData);
    }

    private void SelectedRouteComboBox_SelectionChangeCommitted(object sender, EventArgs e)
    {
        var route = (Route)SelectedRouteComboBox.SelectedItem;

        if (route == null)
        {
            RouteData.Clear();
            return;
        }

        RouteData = JsonSerializer.Deserialize<List<RouteData>>(route.Data)!;
    }

    private void SelectedRouteComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void MapController_VisibleChanged(object sender, EventArgs e)
    {
        if (Visible)
            RenderTimer.Start();
        else
            RenderTimer.Stop();
    }

    private void TargetCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        //if (Controller == null) return;

        //Monster selectedTarget = (Monster)TargetCheckedListBox.Items[e.Index];

        //if (e.NewValue == CheckState.Checked)
        //{
        //    if (!Character.SelectedTargetList.Any(x => x.Id == selectedTarget.Id))
        //        Character.SelectedTargetList.Add(selectedTarget);
        //}
        //else
        //    Character.SelectedTargetList.RemoveAll(x => x.Id == selectedTarget.Id);

        //Controller.SetControl("SelectedTargetList", JsonSerializer.Serialize(Character.SelectedTargetList.Select(e => e.Id)));
    }
}
