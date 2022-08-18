using System.ComponentModel;
using System.Data;
using System.Numerics;
using System.Text.Json;
using KOF.Core;
using KOF.Database.Models;
using KOF.Core.Models;
using KOF.Core.Handlers;
using KOF.Core.Enums;
using Control = System.Windows.Forms.Control;
using System.Diagnostics;
using KOF.Data;
using KOF.Database;

namespace KOF.UI.Forms;

public partial class RoutePlanner : Form
{
    public Client Client { get; set; } = default!;
    private Character Character { get { return Client.Character; } }
    private CharacterHandler CharacterHandler { get { return Client.CharacterHandler; } }
    private Controller Controller { get { return CharacterHandler.Controller; } }
    private BindingList<Route> RouteList { get; set; } = new();
    private List<RouteData> RouteData { get; set; } = new();
    public RoutePlanner(Client client)
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
            if(route.Id == Controller.GetControl("SelectedRoute", 0))
            {
                SelectedRouteComboBox.SelectedItem = route;
                RouteName.Text = route.Name;

                SelectedRouteComboBox_SelectionChangeCommitted(SelectedRouteComboBox, EventArgs.Empty);
            }
        }
    }
    private void RoutePlanner_Load(object sender, EventArgs e)
    {
        RouteList = new BindingList<Route>(SQLiteHandler.Table<Route>().Where(x => x.Zone == Character.Zone).ToList());

        SelectedRouteComboBox.DataSource = RouteList;
        SelectedRouteComboBox.DisplayMember = "Name";

        InitializeControl();
    }

    private void RenderTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Character == null) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME) return;

            var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Character.Zone)!;
            if (zoneData == null) return;

            string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\image";

            Bitmap image = new Bitmap($"{dataDirectory}\\{zoneData.GetMinimapBigImage()}");

            using (Graphics graphic = Graphics.FromImage(image))
            {
                Vector3 characterPosition = GetWorldPositionToMinimap(Map, Character.GetPosition().X, Character.GetPosition().Y);

                graphic.FillRectangle(Brushes.SpringGreen, characterPosition.X, characterPosition.Y, 4, 4);
                graphic.DrawString("Now Here", this.Font, Brushes.Yellow, characterPosition.X - 25, characterPosition.Y);

                for (int i = 0; i < RouteData.Count; i++)
                {
                    var routeData = RouteData[i];                

                    Vector3 position = GetWorldPositionToMinimap(Map, routeData.X, routeData.Y);

                    if(i == 0)
                    {
                        if (routeData.Action == RouteActionType.TOWN)
                        {
                            graphic.FillRectangle(Brushes.Blue, position.X, position.Y, 4, 4);
                            graphic.DrawString($"Town ({i})", this.Font, Brushes.Blue, position.X - 15, position.Y);
                        }
                        else if (routeData.Action == RouteActionType.SUPPLY)
                        {
                            graphic.FillRectangle(Brushes.Purple, position.X, position.Y, 4, 4);
                            graphic.DrawString($"Supply ({i})", this.Font, Brushes.Purple, position.X - 15, position.Y);
                        }
                        else if (routeData.Action == RouteActionType.INN)
                        {
                            graphic.FillRectangle(Brushes.White, position.X, position.Y, 4, 4);
                            graphic.DrawString($"Inn Hostess ({i})", this.Font, Brushes.White, position.X - 25, position.Y);
                        }
                        else
                        {
                            graphic.FillRectangle(Brushes.Red, position.X, position.Y, 4, 4);
                            graphic.DrawString($"Move ({i})", this.Font, Brushes.Red, position.X - 15, position.Y);
                        }
                    }
                    else 
                    {
                        var prevRouteData = RouteData[i - 1];

                        Vector3 prevPosition = GetWorldPositionToMinimap(Map, prevRouteData.X, prevRouteData.Y);

                        if (i != RouteData.Count - 1)
                        {
                            if(prevRouteData.Action != RouteActionType.TOWN)
                            {
                                Pen linePen = new Pen(Brushes.Orange);

                                linePen.Width = 3;
                                graphic.DrawLine(linePen, prevPosition.X, prevPosition.Y, position.X, position.Y);
                            }

                            if(routeData.Action == RouteActionType.TOWN)
                            {
                                graphic.FillRectangle(Brushes.Blue, position.X, position.Y, 4, 4);
                                graphic.DrawString($"Town ({i})", this.Font, Brushes.Blue, position.X - 15, position.Y);
                            }
                            else if(routeData.Action == RouteActionType.SUPPLY)
                            {
                                graphic.FillRectangle(Brushes.Purple, position.X, position.Y, 4, 4);
                                graphic.DrawString($"Supply ({i})", this.Font, Brushes.Purple, position.X - 15, position.Y);
                            }
                            else if (routeData.Action == RouteActionType.INN)
                            {
                                graphic.FillRectangle(Brushes.White, position.X, position.Y, 4, 4);
                                graphic.DrawString($"Inn Hostess ({i})", this.Font, Brushes.White, position.X - 25, position.Y);
                            }
                            else
                            {
                                graphic.FillRectangle(Brushes.Red, position.X, position.Y, 4, 4);
                                graphic.DrawString($"Move ({i})", this.Font, Brushes.Red, position.X - 15, position.Y);
                            }
                        }
                        else
                        {
                            if (routeData.Action == RouteActionType.TOWN)
                            {
                                Pen linePen = new Pen(Brushes.Orange);

                                linePen.Width = 3;
                                graphic.DrawLine(linePen, prevPosition.X, prevPosition.Y, position.X, position.Y);

                                graphic.FillRectangle(Brushes.Blue, position.X, position.Y, 4, 4);
                                graphic.DrawString($"Town ({i})", this.Font, Brushes.Blue, position.X - 15, position.Y);
                            }
                            else if (routeData.Action == RouteActionType.SUPPLY)
                            {
                                if (prevRouteData.Action != RouteActionType.TOWN)
                                {
                                    Pen linePen = new Pen(Brushes.Orange);

                                    linePen.Width = 3;
                                    graphic.DrawLine(linePen, prevPosition.X, prevPosition.Y, position.X, position.Y);
                                }

                                graphic.FillRectangle(Brushes.Purple, position.X, position.Y, 4, 4);
                                graphic.DrawString($"Supply ({i})", this.Font, Brushes.Purple, position.X - 15, position.Y);
                            }
                            else if (routeData.Action == RouteActionType.INN)
                            {
                                if (prevRouteData.Action != RouteActionType.TOWN)
                                {
                                    Pen linePen = new Pen(Brushes.Orange);

                                    linePen.Width = 3;
                                    graphic.DrawLine(linePen, prevPosition.X, prevPosition.Y, position.X, position.Y);
                                }

                                graphic.FillRectangle(Brushes.White, position.X, position.Y, 4, 4);
                                graphic.DrawString($"Inn Hostess ({i})", this.Font, Brushes.White, position.X - 15, position.Y);
                            }
                            else
                            {
                                if (prevRouteData.Action != RouteActionType.TOWN)
                                {
                                    Pen linePen = new Pen(Brushes.Orange);

                                    linePen.Width = 3;
                                    graphic.DrawLine(linePen, prevPosition.X, prevPosition.Y, position.X, position.Y);
                                }

                                graphic.FillRectangle(Brushes.Red, position.X, position.Y, 4, 4);
                                graphic.DrawString($"End ({i})", this.Font, Brushes.Red, position.X - 15, position.Y);
                            }
                        }
                    }
                }

                if (Map.Image != null)
                    Map.Image.Dispose();

                Map.Image = image;
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

        float fWidth = (Picture.Right - Picture.Left);
        float fHeight = (Picture.Bottom - Picture.Top);

        Vector3 coordinate = new Vector3(
                (float)Math.Ceiling(X / (float)(zoneData.MapLength / fWidth)),
                (float)Math.Ceiling(fHeight - (Y / (float)(zoneData.MapLength / fHeight))),
                0.0f);

        coordinate.Z = zoneData.GetHeightBy2DPos(X, Y);

        return coordinate;
    }

    private void Map_MouseDown(object sender, MouseEventArgs e)
    {
        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Character.Zone)!;
        if (zoneData == null) return;

        Vector3 mapPosition = GetMiniMapPositionToWorld(Map, e.X, e.Y);

        var routeData = new RouteData();

        routeData.Action = RouteActionType.MOVE;
        routeData.X = mapPosition.X;
        routeData.Y = mapPosition.Y;
        routeData.Z = mapPosition.Z;

        RouteData.Add(routeData);
    }

    private void RoutePlanner_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.Z)
        {
            if(RouteData.Count > 0)
                RouteData.RemoveAt(RouteData.Count - 1);
        }

        if (e.KeyCode == Keys.Escape)
            RouteData.Clear();
    }

    private void TownActionButton_Click(object sender, EventArgs e)
    {
        if(RouteData.Count > 0)
        {
            var routeData = RouteData[RouteData.Count - 1];

            var newRouteData = new RouteData();

            newRouteData.Action = RouteActionType.TOWN;
            newRouteData.X = routeData.X;
            newRouteData.Y = routeData.Y;
            newRouteData.Z = routeData.Z;

            RouteData[RouteData.Count - 1] = newRouteData;
        }
        else
        {
            var routeData = new RouteData();

            routeData.Action = RouteActionType.TOWN;
            routeData.X = Character.GetPosition().X;
            routeData.Y = Character.GetPosition().Y;
            routeData.Z = Character.GetPosition().Z;

            RouteData.Add(routeData);
        }
    }

    private void SupplyAreaActionButton_Click(object sender, EventArgs e)
    {
        if (RouteData.Count > 0)
        {
            var routeData = RouteData[RouteData.Count - 1];

            var newRouteData = new RouteData();

            newRouteData.Action = RouteActionType.SUPPLY;
            newRouteData.X = routeData.X;
            newRouteData.Y = routeData.Y;
            newRouteData.Z = routeData.Z;

            RouteData[RouteData.Count - 1] = newRouteData;
        }
        else
        {
            var routeData = new RouteData();

            routeData.Action = RouteActionType.SUPPLY;
            routeData.X = Character.GetPosition().X;
            routeData.Y = Character.GetPosition().Y;
            routeData.Z = Character.GetPosition().Z;

            RouteData.Add(routeData);
        }
    }

    private void InnHostessAreaButton_Click(object sender, EventArgs e)
    {
        if (RouteData.Count > 0)
        {
            var routeData = RouteData[RouteData.Count - 1];

            var newRouteData = new RouteData();

            newRouteData.Action = RouteActionType.INN;
            newRouteData.X = routeData.X;
            newRouteData.Y = routeData.Y;
            newRouteData.Z = routeData.Z;

            RouteData[RouteData.Count - 1] = newRouteData;
        }
        else
        {
            var routeData = new RouteData();

            routeData.Action = RouteActionType.INN;
            routeData.X = Character.GetPosition().X;
            routeData.Y = Character.GetPosition().Y;
            routeData.Z = Character.GetPosition().Z;

            RouteData.Add(routeData);
        }
    }

    private void SaveRouteButton_Click(object sender, EventArgs e)
    {
        if(RouteName.Text == "")
        {
            MessageBox.Show("Please type a route name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var route = RouteList.SingleOrDefault(x => x.Name == RouteName.Text);

        if (route == null)
        {
            route = new Route();

            route.Name = RouteName.Text;
            route.Zone = Character.Zone;
            route.Data = JsonSerializer.Serialize(RouteData);

            route.Id = (int)SQLiteHandler.Insert(route);

            RouteList.Add(route);
        }
        else
        {
            route.Data = JsonSerializer.Serialize(RouteData);

            SQLiteHandler.Update(route);
        }

        RouteList.ResetBindings();
    }

    private void DeleteRouteButton_Click(object sender, EventArgs e)
    {
        var route = (Route)SelectedRouteComboBox.SelectedItem;

        if (route == null) return;

        SQLiteHandler.Delete(route);

        RouteList.Remove(route);
        RouteList.ResetBindings();

        SelectedRouteComboBox_SelectionChangeCommitted(SelectedRouteComboBox, EventArgs.Empty);
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

        Controller.SetControl("SelectedRoute", route.Id);

        RouteName.Text = route.Name;

        RouteData = JsonSerializer.Deserialize<List<RouteData>>(route.Data)!;
    }

    private void SelectedRouteComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void RoutePlanner_VisibleChanged(object sender, EventArgs e)
    {
        if (Visible)
            RenderTimer.Start();
        else
            RenderTimer.Stop();
    }

    private void ResetRouteButton_Click(object sender, EventArgs e)
    {
        RouteData.Clear();

        RouteName.Text = "";

        Controller.SetControl("SelectedRoute", 0);
    }
}
