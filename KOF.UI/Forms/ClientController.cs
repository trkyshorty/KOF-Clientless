using System.ComponentModel;
using System.Data;
using System.Numerics;
using System.Text.Json;
using KOF.Core;
using KOF.Core.Handlers;
using KOF.Data.Models;
using System.Diagnostics;
using KOF.Core.Models;
using KOF.Database.Models;

using Control = System.Windows.Forms.Control;
using KOF.Core.Enums;
using KOF.Data;
using KOF.Database;
using AStar;
using AStar.Options;
using System.Reflection;

namespace KOF.UI.Forms;

public partial class ClientController : Form
{
    public Client Client { get; set; } = default!;
    private Character Character { get { return Client.Character; } }
    private CharacterHandler CharacterHandler { get { return Client.CharacterHandler; } }
    private Controller Controller { get { return CharacterHandler.Controller; } }
    private BindingList<Supply> SupplyItemList { get; set; } = new();

    private CancellationTokenSource sendPacketCancelationToken { get; set; }
    CancellationToken sendPacketCancelationTokenCt { get; set; }

    public ClientController(Client client)
    {
        Client = client;

        InitializeComponent();

        typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.SetProperty, null,
        PartyListDataGrid, new object[] { true });

        typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.SetProperty, null,
        NearbyPlayerListDataGrid, new object[] { true });

        typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.SetProperty, null,
        GateListDataGrid, new object[] { true });

        typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.SetProperty, null,
        QuestNpcList, new object[] { true });

        typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.SetProperty, null,
        NpcShopDataList, new object[] { true });

        Visible = false;

        sendPacketCancelationToken = new CancellationTokenSource();
        sendPacketCancelationTokenCt = sendPacketCancelationToken.Token;

    }

    private void ClientController_Load(object sender, EventArgs e)
    {
        Text = $"{Character.Name}";

        FollowSelect.DataSource = ClientHandler.ClientList;
        FollowSelect.DisplayMember = "Name";

        InitializeSkillList();
        InitializeControl();
    }

    private void ClientController_VisibleChanged(object sender, EventArgs e)
    {
        if (Visible)
        {
            MiniMapTimer.Start();
            StatusTimer.Start();

            InitializeSkillList();
            InitializeControl();
        }
        else
        {
            MiniMapTimer.Stop();
            StatusTimer.Stop();
            StatusTimer.Stop();
        }
    }

    private void ClientController_FormClosing(object sender, FormClosingEventArgs e)
    {
        Visible = false;
        e.Cancel = true;
    }

    private void InitializeSkillList()
    {
        AttackSkillCheckedListBox.DataSource = Character.SkillList
            .Where(x =>
            {
                return x.Id.ToString().Substring(0, 3) == Character.Class.ToString() &&
                        x.Point <= Character.Level
                        && (x.Mastery == 0 || x.Point <= Character.Skills[5 + x.Mastery])
                        && (x.TargetType == (int)SkillMagicTargetType.TARGET_ENEMY_ONLY || x.TargetType == (int)SkillMagicTargetType.TARGET_AREA_ENEMY);
            })
            .ToList();

        AttackSkillCheckedListBox.DisplayMember = "Name";

        SelfSkillCheckedListBox.DataSource = Character.SkillList
            .Where(x =>
            {
                return x.Id.ToString().Substring(0, 3) == Character.Class.ToString() &&
                        x.Point <= Character.Level &&
                        (x.Mastery == 0 || x.Point <= Character.Skills[5 + x.Mastery]) &&
                        (x.TargetType == (int)SkillMagicTargetType.TARGET_SELF || x.TargetType == (int)SkillMagicTargetType.TARGET_PARTY_ALL || x.TargetType == (int)SkillMagicTargetType.TARGET_FRIEND_WITHME);
            })
            .ToList();

        SelfSkillCheckedListBox.DisplayMember = "Name";
    }

    private void InitializeControl()
    {
        if (Controller == null) return;

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
                else if (control.GetType() == typeof(TextBox))
                {
                    TextBox textBox = ((TextBox)control);
                    string value = Controller.GetControl(textBox.Name, textBox.Text);

                    if (value != textBox.Text)
                        textBox.Text = value;
                }
                else if (control.GetType() == typeof(ComboBox))
                {
                    ComboBox comboBox = ((ComboBox)control);

                    string value = Controller.GetControl(comboBox.Name, comboBox.SelectedText);

                    if (value != comboBox.SelectedText)
                        comboBox.SelectedItem = value;
                }
            }
        }
        while (control != null);

        var selectedSkillIds = JsonSerializer.Deserialize<List<int>>(Controller.GetControl("SelectedSkillList", "[]"))!;

        for (int i = 0; i <= AttackSkillCheckedListBox.Items.Count - 1; i++)
        {
            Skill skill = (Skill)AttackSkillCheckedListBox.Items[i];

            if (selectedSkillIds.Any(x => x == skill.Id))
                AttackSkillCheckedListBox.SetItemCheckState(i, CheckState.Checked);
            else
                AttackSkillCheckedListBox.SetItemCheckState(i, CheckState.Unchecked);
        }

        for (int i = 0; i <= SelfSkillCheckedListBox.Items.Count - 1; i++)
        {
            Skill skill = (Skill)SelfSkillCheckedListBox.Items[i];

            if (selectedSkillIds.Any(x => x == skill.Id))
                SelfSkillCheckedListBox.SetItemCheckState(i, CheckState.Checked);
            else
                SelfSkillCheckedListBox.SetItemCheckState(i, CheckState.Unchecked);
        }

        TargetCheckedListBox.DataSource = Character.SelectedTargetList;
        TargetCheckedListBox.DisplayMember = "Name";

        for (int i = 0; i <= TargetCheckedListBox.Items.Count - 1; i++)
            TargetCheckedListBox.SetItemCheckState(i, CheckState.Checked);


        SupplyItemList = JsonSerializer.Deserialize<BindingList<Supply>>(Controller.GetControl("SupplyList", "[]"))!;
        SupplyItemDataGrid.DataSource = SupplyItemList;

        SupplyItemDataGrid.Columns[1].ReadOnly = true;
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
        if (Character == null) return;

        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Character.Zone)!;
        if (zoneData == null) return;

        CharacterHandler.RouteQueue.Clear();
        CharacterHandler.SelectTarget(-1);

        Vector3 mapPosition = GetMiniMapPositionToWorld(MiniMap, e.X, e.Y);
        Character.SetMovePosition(mapPosition);
    }

    private void SearchTargetButton_Click(object sender, EventArgs e)
    {
        var selectedTargetIds = JsonSerializer.Deserialize<List<int>>(Controller.GetControl("SelectedTargetList", "[]"))!;

        TargetCheckedListBox.DataSource = TableHandler.GetMonsterList()
                .FindAll(x => CharacterHandler.GetNpcList().Any(y => y.ProtoId == x.Id) || selectedTargetIds.Contains(x.Id))
                .GroupBy(p => p.Id)
                .Select(g => g.First())
                .ToList();

        TargetCheckedListBox.DisplayMember = "Name";

        for (int i = 0; i <= TargetCheckedListBox.Items.Count - 1; i++)
        {
            var target = (Monster)TargetCheckedListBox.Items[i];

            if (selectedTargetIds.Contains(target.Id))
                TargetCheckedListBox.SetItemCheckState(i, CheckState.Checked);
            else
                TargetCheckedListBox.SetItemCheckState(i, CheckState.Unchecked);
        }
    }

    private void AttackSkillCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (Controller == null) return;

        Skill selectedSkill = (Skill)AttackSkillCheckedListBox.Items[e.Index];

        if (e.NewValue == CheckState.Checked)
        {
            if (!Character.SelectedSkillList.Any(x => x.Id == selectedSkill.Id))
                Character.SelectedSkillList.Add(selectedSkill);
        }
        else
            Character.SelectedSkillList.RemoveAll(x => x.Id == selectedSkill.Id);

        Controller.SetControl("SelectedSkillList", JsonSerializer.Serialize(Character.SelectedSkillList.Select(e => e.Id)));
    }

    private void SelfSkillCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (Controller == null) return;

        Skill selectedSkill = (Skill)SelfSkillCheckedListBox.Items[e.Index];

        if (e.NewValue == CheckState.Checked)
        {
            if (!Character.SelectedSkillList.Any(x => x.Id == selectedSkill.Id))
                Character.SelectedSkillList.Add(selectedSkill);
        }
        else
            Character.SelectedSkillList.RemoveAll(x => x.Id == selectedSkill.Id);

        Controller.SetControl("SelectedSkillList", JsonSerializer.Serialize(Character.SelectedSkillList.Select(e => e.Id)));
    }

    private void TargetCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (Controller == null) return;

        Monster selectedTarget = (Monster)TargetCheckedListBox.Items[e.Index];

        if (e.NewValue == CheckState.Checked)
        {
            if (!Character.SelectedTargetList.Any(x => x.Id == selectedTarget.Id))
                Character.SelectedTargetList.Add(selectedTarget);
        }
        else
            Character.SelectedTargetList.RemoveAll(x => x.Id == selectedTarget.Id);

        Controller.SetControl("SelectedTargetList", JsonSerializer.Serialize(Character.SelectedTargetList.Select(e => e.Id)));
    }

    private void AttackButton_Click(object sender, EventArgs e)
    {
        if (Controller == null) return;

        if (Controller.GetControl("Attack", false))
            Controller.SetControl("Attack", false);
        else
            Controller.SetControl("Attack", true);
    }

    private void PressOkButton_Click(object sender, EventArgs e)
    {
        CharacterHandler.Regen();
    }

    private void TownButton_Click(object sender, EventArgs e)
    {
        CharacterHandler.Town();
    }

    private void HpPotionCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(HpPotionCheckBox.Name, HpPotionCheckBox.Checked);
    }

    private void MpPotionCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(MpPotionCheckBox.Name, MpPotionCheckBox.Checked);
    }

    private void SelfSkillButton_Click(object sender, EventArgs e)
    {
        if (Controller == null) return;

        if (Controller.GetControl("SelfSkill", true))
            Controller.SetControl("SelfSkill", false);
        else
            Controller.SetControl("SelfSkill", true);
    }

    private void AttackRange_ValueChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(AttackRange.Name, AttackRange.Value);
    }

    private void RoutePlannerButton_Click(object sender, EventArgs e)
    {
        RoutePlanner routePlanner = new RoutePlanner(Client);
        routePlanner.ShowDialog();
    }

    private void InventoryButton_Click(object sender, EventArgs e)
    {
        InventoryController inventoryController = new InventoryController(Client);
        inventoryController.ShowDialog();
    }

    private void CharacterInfoButton_Click(object sender, EventArgs e)
    {
        CharacterInfo characterInfo = new CharacterInfo(Client);
        characterInfo.ShowDialog();
    }

    private void SkillInfoButton_Click(object sender, EventArgs e)
    {
        SkillInfo skillInfo = new SkillInfo(Client);
        skillInfo.ShowDialog();
    }

    private void EnableLoot_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(EnableLoot.Name, EnableLoot.Checked);
    }

    private void LootMinPrice_ValueChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(LootMinPrice.Name, LootMinPrice.Value);
    }

    private void HpPotionPercentageValue_ValueChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(HpPotionPercentageValue.Name, HpPotionPercentageValue.Value);
    }

    private void MpPotionPercentageValue_ValueChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(HpPotionPercentageValue.Name, HpPotionPercentageValue.Value);
    }

    private void SupplyItemDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl("SupplyList", JsonSerializer.Serialize(SupplyItemList));
    }

    private void SupplyItemDataGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl("SupplyList", JsonSerializer.Serialize(SupplyItemList));
    }

    private void MoveToTarget_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(MoveToTarget.Name, MoveToTarget.Checked);
    }

    private void FollowSelect_SelectionChangeCommitted(object sender, EventArgs e)
    {
        var client = (Client)FollowSelect.SelectedItem;
        if (client == null) return;

        if (client.Character.Name != Character.Name)
            Controller.SetControl("Follow", client.Name);
        else
            Controller.SetControl("Follow", "");
    }

    private void PartyAcceptButton_Click(object sender, EventArgs e)
    {
        CharacterHandler.PartyAccept();
    }

    private void PartyDeclineButton_Click(object sender, EventArgs e)
    {
        CharacterHandler.PartyDestroy();
    }

    private void PartyKickButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in PartyListDataGrid.SelectedRows)
        {
            var partyMember = (PartyMember)row.DataBoundItem;
            CharacterHandler.PartyRemove(partyMember.MemberId);
        }
    }

    private void PartyDisbandButton_Click(object sender, EventArgs e)
    {
        CharacterHandler.PartyDestroy();
    }

    private void RefreshPlayerListButton_Click(object sender, EventArgs e)
    {
        
    }

    private void SendPartyButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in NearbyPlayerListDataGrid.SelectedRows)
        {
            var player = (Character)row.DataBoundItem;
            CharacterHandler.PartySend(player.Name);
        }
    }

    private void RebornWhenDie_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(RegenerateWhenDie.Name, RegenerateWhenDie.Checked);
    }

    private void PartyMakeLeaderButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in PartyListDataGrid.SelectedRows)
        {
            var partyMember = (PartyMember)row.DataBoundItem;
            CharacterHandler.PartyPromoteLeader(partyMember.MemberId);
        }
    }

    private void FollowTargetSync_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(FollowTargetSync.Name, FollowTargetSync.Checked);
    }

    private void FollowOnlyNearby_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(FollowOnlyNearby.Name, FollowOnlyNearby.Checked);
    }

    private void FullScreenMapButton_Click(object sender, EventArgs e)
    {
        MapController mapController = new MapController(Client);
        mapController.ShowDialog();
    }

    private void MinorCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(MinorCheckBox.Name, MinorCheckBox.Checked);
    }

    private void MinorPercentageValue_ValueChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(MinorPercentageValue.Name, MinorPercentageValue.Value);
    }

    private void GodModeCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(GodModeCheckBox.Name, GodModeCheckBox.Checked);
    }

    private void HyperNoahCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(HyperNoahCheckBox.Name, HyperNoahCheckBox.Checked);
    }

    private void MoveToLootCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(MoveToLootCheckBox.Name, MoveToLootCheckBox.Checked);
    }

    private void ReloadSkillButton_Click(object sender, EventArgs e)
    {
        if (Controller == null) return;
        Controller.SetControl("SelectedSkillList", "[]");
        CharacterHandler.InitializeSkillList();
        InitializeSkillList();
        InitializeControl();
    }

    private void SwiftPartyMemberCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(SwiftPartyMemberCheckBox.Name, SwiftPartyMemberCheckBox.Checked);
    }

    private void MiniMapTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (CharacterHandler == null || Character == null) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME) return;

            var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Character.Zone)!;
            if (zoneData == null) return;

            string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\image";
            Bitmap image = new Bitmap($"{dataDirectory}\\{zoneData.GetMinimapImage()}");

            if (image == null) return;

            using (Graphics graphic = Graphics.FromImage(image))
            {
                if (CharacterHandler.RouteQueue.Count > 0)
                {
                    var moveList = CharacterHandler.RouteQueue.ToList();

                    Vector3 prevPosition = GetWorldPositionToMinimap(MiniMap, Character.X, Character.Y);

                    moveList.ForEach(x =>
                    {
                        Pen linePen = new Pen(Brushes.Orange);
                        linePen.Width = 3;
                        Vector3 position = GetWorldPositionToMinimap(MiniMap, x.X, x.Y);
                        graphic.DrawLine(linePen, prevPosition.X, prevPosition.Y, position.X, position.Y);
                        prevPosition = new Vector3(position.X, position.Y, 0.0f);
                    });
                }

                Vector3 characterPosition = GetWorldPositionToMinimap(MiniMap, Character.GetPosition().X, Character.GetPosition().Y);

                if (Character.GetMovePosition() != Vector3.Zero)
                {
                    Vector3 movePosition = GetWorldPositionToMinimap(MiniMap, Character.GetMovePosition().X, Character.GetMovePosition().Y);

                    Pen linePen = new Pen(Brushes.Red);
                    linePen.Width = 3;
                    graphic.DrawLine(linePen, characterPosition.X, characterPosition.Y, movePosition.X, movePosition.Y);
                }

                CharacterHandler?.GetPlayerList().ForEach(x =>
                {
                    if (x == null) return;
                    Vector3 otherPlayerPosition = GetWorldPositionToMinimap(MiniMap, x.GetPosition().X, x.GetPosition().Y);
                    graphic.FillRectangle(Brushes.DeepSkyBlue, otherPlayerPosition.X, otherPlayerPosition.Y, 4, 4);
                });

                CharacterHandler?.GetNpcList().ForEach(x =>
                {
                    if (x?.MonsterOrNpc == 1)
                    {
                        var monColor = Brushes.Red;

                        if (x.IsDead())
                            monColor = Brushes.Orange;

                        Vector3 otherMonsterPosition = GetWorldPositionToMinimap(MiniMap, x.GetPosition().X, x.GetPosition().Y);
                        graphic.FillRectangle(monColor, otherMonsterPosition.X, otherMonsterPosition.Y, 4, 4);
                    }
                    else
                    {
                        if (x != null)
                        {
                            Vector3 otherNpcPosition = GetWorldPositionToMinimap(MiniMap, x.GetPosition().X, x.GetPosition().Y);
                            graphic.FillRectangle(Brushes.DarkBlue, otherNpcPosition.X, otherNpcPosition.Y, 4, 4);
                        }
                    }
                });

                var targetSearchRange = (int)(Controller?.GetControl("TargetSearchRange", 45) / 2)!;

                graphic.FillRectangle(Brushes.SpringGreen, characterPosition.X, characterPosition.Y, 4, 4);
                graphic.FillEllipse(new SolidBrush(Color.FromArgb(75, 0, 0, 255)), characterPosition.X - targetSearchRange, characterPosition.Y - targetSearchRange, targetSearchRange * 2, targetSearchRange * 2);

                if (MiniMap.Image != null)
                    MiniMap.Image.Dispose();

                MiniMap.Image = image;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    private void SupplyTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME || CharacterHandler.IsRouting()) return;
            if (Character.IsTrading) return;
            if (!Controller.GetControl("Bot", false)) return;

            var route = SQLiteHandler.Table<Route>().SingleOrDefault(x => x.Id == Controller.GetControl("SelectedRoute", 0));

            if (route == null) return;

            BindingList<Supply> supplyList = (BindingList<Supply>)SupplyItemDataGrid.DataSource;

            bool needSupply = false;

            if (supplyList != null)
                needSupply = CharacterHandler.IsNeedSupply(supplyList.ToList());

            needSupply = needSupply || CharacterHandler.IsNeedRepair() || CharacterHandler.IsInventoryFull();

            if (needSupply && route.Zone == Character.Zone)
            {
                CharacterHandler.RouteQueue.Clear();
                CharacterHandler.Route(JsonSerializer.Deserialize<List<RouteData>>(route.Data)!);
            }
                
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void ProtectionTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME || CharacterHandler.IsUntouchable() || Character.IsDead()) return;
            if (Character.IsTrading) return;

            double hpPotionPercent = Math.Ceiling((Character.Hp * 100) / (float)Character.MaxHp);

            if (Controller.GetControl("HpPotionCheckBox", false) && (decimal)hpPotionPercent <= Controller.GetControl("HpPotionPercentageValue", HpPotionPercentageValue.Value))
            {
                int[] hpPotion = {
                        389064000, 910005000, 389063000, 399014000,
                        810265000, 810267000, 810269000, 810272000,
                        890229000, 899996000, 910004000, 930665000,
                        931786000, 389062000, 900790000, 910003000,
                        930664000, 389061000, 900780000, 910002000,
                        389060000, 900770000, 910001000, 910012000,
                        389310000, 389320000, 389330000, 389390000,
                        900817000, 389015000, 389014000, 389013000,
                        389012000, 389011000, 389010000, 389070000,
                        389071000, 800124000, 800126000, 810189000,
                        810247000, 811006000, 811008000, 814679000,
                        900486000
                    };

                hpPotion = hpPotion.Reverse().ToArray();

                var item = Character.Inventory.FirstOrDefault(x => hpPotion.Contains((int)x.ItemID));

                if (item != null)
                    CharacterHandler.UseItem(item.ItemID);
            }

            double mpPotionPercent = Math.Ceiling((Character.Mp * 100) / (float)Character.MaxMp);

            if (Controller.GetControl("MpPotionCheckBox", false) && (decimal)mpPotionPercent <= Controller.GetControl("MpPotionPercentageValue", MpPotionPercentageValue.Value))
            {
                int[] mpPotion = {
                        389072000, 800125000, 800127000, 810192000,
                        810248000, 900487000, 811006000, 811008000,
                        814679000, 900486000, 389020000, 389019000,
                        389018000, 389017000, 389016000, 389340000,
                        389350000, 389360000, 389400000, 900818000,
                        910006000, 389078000, 910007000, 900800000,
                        389079000, 910008000, 900810000, 389080000,
                        910009000, 900820000, 389081000, 910010000,
                        899997000, 399020000, 389082000
                    };

                mpPotion = mpPotion.Reverse().ToArray();

                var item = Character.Inventory.FirstOrDefault(x => mpPotion.Contains((int)x.ItemID));

                if (item != null)
                    CharacterHandler.UseItem(item.ItemID);
            }

            if (Character.GetRepresentClass(Character.Class) == (int)ClassRepresentType.CLASS_REPRESENT_ROGUE)
            {
                double hpPercent = Math.Ceiling((Character.Hp * 100) / (float)Character.MaxHp);

                if (Controller.GetControl("MinorCheckBox", false) && (decimal)hpPercent <= Controller.GetControl("MinorPercentageValue", MinorPercentageValue.Value))
                {
                    var skill = Character.SkillList.FirstOrDefault(x => x.Id.ToString().Substring(0, 3) == Character.Class.ToString() && x.BaseId == 107705); //Minor Healing

                    if (skill != null &&
                        !CharacterHandler.SkillQueue.Any(x => x.Id == skill.Id) &&
                        Character.Mp >= skill.Mana)
                    {
                        CharacterHandler.SkillQueue.Enqueue(skill);
                    }
                }

            }

            if (Controller.GetControl("GodModeCheckBox", true))
            {
                var skill = Character.SkillList.FirstOrDefault(x => x.Id == 500344);

                if (skill != null &&
                    !CharacterHandler.SkillQueue.Any(x => x.Id == skill.Id) &&
                    Environment.TickCount - skill.GetSkillUseTime() > (skill.CoolDown))
                {
                    double hpGodModePercent = Math.Ceiling((Character.Hp * 100) / (float)Character.MaxHp);
                    double mpGodModePercent = Math.Ceiling((Character.Mp * 100) / (float)Character.MaxMp);

                    if ((decimal)hpGodModePercent <= Controller.GetControl("HpPotionPercentageValue", HpPotionPercentageValue.Value) ||
                        (decimal)mpGodModePercent <= Controller.GetControl("MpPotionPercentageValue", MpPotionPercentageValue.Value) ||
                        !CharacterHandler.SkillBuffEffected((byte)skill.Extension.BuffType))
                    {
                        CharacterHandler.CancelSkill(skill);
                        CharacterHandler.SkillQueue.Enqueue(skill);
                    }
                }
            }

            if (Controller.GetControl("HyperNoahCheckBox", true))
            {
                var skill = Character.SkillList.FirstOrDefault(x => x.Id == 500094);

                if (skill != null && !CharacterHandler.SkillQueue.Any(x => x.Id == skill.Id))
                {
                    if (!CharacterHandler.SkillBuffEffected((byte)skill.Extension.BuffType))
                    {
                        CharacterHandler.SkillQueue.Enqueue(skill);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void StatusTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;

            if (Character.Hp <= Character.MaxHp)
            {
                HealthProgressBar.Minimum = 0;
                HealthProgressBar.Maximum = Character.MaxHp;
                HealthProgressBar.Value = Character.Hp;
            }

            if (Character.Mp <= Character.MaxMp)
            {
                ManaProgressBar.Minimum = 0;
                ManaProgressBar.Maximum = Character.MaxMp;
                ManaProgressBar.Value = Character.Mp;
            }

            if ((int)Character.Experience <= (int)Character.MaxExperience)
            {
                ExperienceProgressBar.Minimum = 0;
                ExperienceProgressBar.Maximum = (int)Character.MaxExperience;
                ExperienceProgressBar.Value = (int)Character.Experience;
            }

            if (Controller.GetControl("Bot", true))
                BotButton.ForeColor = Color.LimeGreen;
            else
                BotButton.ForeColor = Color.Red;

            if (Controller.GetControl("Attack", false))
                AttackButton.ForeColor = Color.LimeGreen;
            else
                AttackButton.ForeColor = Color.Red;

            if (Controller.GetControl("SelfSkill", true))
                SelfSkillButton.ForeColor = Color.LimeGreen;
            else
                SelfSkillButton.ForeColor = Color.Red;

            if (Controller.GetControl("Quest", false))
                QuestButton.ForeColor = Color.LimeGreen;
            else
                QuestButton.ForeColor = Color.Red;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void AttackTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (!Controller.GetControl("Attack", false)) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME || CharacterHandler.IsUntouchable() || CharacterHandler.IsRouting()) return;
            if (Character.IsTrading) return;

            var attackRange = Controller.GetControl("AttackRange", 45);

            if (Character.GetTargetId() != -1)
            {
                var target = CharacterHandler.GetNpcList().FirstOrDefault(x => x?.Id == Character.GetTargetId());

                if (target != null)
                {
                    if (target.IsDead() || Vector3.Distance(Character.GetPosition(), target.GetPosition()) >= (float)attackRange)
                        return;

                    CharacterHandler.Attack();
                }              
            }
                
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void SelfSkillTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (!Controller.GetControl("SelfSkill", true)) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME || CharacterHandler.IsUntouchable()) return;
            if (Character.IsTrading) return;

            CharacterHandler.SelfProtection();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void TargetAndActionTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (!Controller.GetControl("Attack", false)) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME || CharacterHandler.IsRouting()) return;
            if (Character.IsTrading) return;

            //bool test = false;

            //if (test)
            //{
            //    CharacterHandler.SelectTarget(20573); // KUKLA TEST
            //    return;
            //}

            var selectedTargetIds = JsonSerializer.Deserialize<List<int>>(Controller.GetControl("SelectedTargetList", "[]"))!;

            var followedClient = ClientHandler.ClientList.FirstOrDefault(x => x != null && x.CharacterHandler.GetGameState() == GameState.GAME_STATE_INGAME && x.Character.Name == Controller.GetControl("Follow", ""))!;

            if (Controller.GetControl("FollowTargetSync", true)
                && followedClient != null
                && !followedClient.CharacterHandler.IsRouting()
                && !followedClient.Character.IsTrading
                && CharacterHandler.GetGameState() == GameState.GAME_STATE_INGAME)
            {
                if (followedClient.Character.GetTargetId() != Character.GetTargetId()
                    && Vector3.Distance(followedClient.Character.GetPosition(), Character.GetPosition()) <= 150)
                    CharacterHandler.SelectTarget(followedClient.Character.GetTargetId());
            }
            else
            {
                var targetSearchRange = Controller.GetControl("TargetSearchRange", 45);

                if (Character.GetTargetId() != -1)
                {
                    var target = CharacterHandler.GetNpcList().FirstOrDefault(x => x?.Id == Character.GetTargetId());

                    if (target != null)
                    {
                        if (target.IsDead() || Vector3.Distance(Character.GetPosition(), target.GetPosition()) >= (float)targetSearchRange)
                        {
                            CharacterHandler.SelectTarget(-1);
                        }
                        else
                        {
                            if (!CharacterHandler.IsRouting() && !CharacterHandler.IsMovingToLoot() && Controller.GetControl("MoveToTarget", true) && target.GetPosition() != Character.GetPosition())
                                Character.SetMovePosition(target.GetPosition());
                        }
                    }
                    else
                        CharacterHandler.SelectTarget(-1);
                }
                else
                {
                    Character target = default!;

                    if (selectedTargetIds.Count() > 0)
                    {
                        target = CharacterHandler.GetNpcList()
                           .FindAll(x => !x.IsDead() && selectedTargetIds.Contains(x.ProtoId) && x.MonsterOrNpc == 1 && Vector3.Distance(x.GetPosition(), Character.GetPosition()) < (float)targetSearchRange)
                           .OrderBy(x => Vector3.Distance(Character.GetPosition(), x.GetPosition()))
                           ?.FirstOrDefault()!;
                    }
                    else
                    {
                        target = CharacterHandler.GetNpcList()
                           .FindAll(x => !x.IsDead() && x.MonsterOrNpc == 1 && Vector3.Distance(x.GetPosition(), Character.GetPosition()) < (float)targetSearchRange)
                           .OrderBy(x => Vector3.Distance(Character.GetPosition(), x.GetPosition()))
                           ?.FirstOrDefault()!;
                    }

                    if (target != null)
                        CharacterHandler.SelectTarget(target.Id);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void AutoPartyTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME) return;
            if (Character.IsTrading) return;

            var followers = ClientHandler.ClientList.Where(x =>
            {
                if (x.CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME) return false;
                return x.CharacterHandler.Controller?.GetControl("Follow", "") == Character.Name && x.Name != Character.Name;
            }).ToList();

            if (followers.Count == 0) return;

            if (Character.Party.IsInParty())
            {
                if (Character.Party.IsFull()) return;

                followers.ForEach(x =>
                {
                    if (Character.Party.IsFull()) return;
                    if (Character.Zone != x.Character.Zone) return;

                    if (Character.Party.GetMemberById(x.Character.Id) == null)
                        CharacterHandler.PartySend(x.Character);
                });
            }
            else
            {
                followers.ForEach(x =>
                {
                    if (x.Character.Party.IsInParty())
                    {
                        if (x.Character.Party.IsFull()) return;
                        if (x.Character.Zone != Character.Zone) return;

                        if (x.Character.Party.GetMemberById(Character.Id) == null)
                            x.CharacterHandler.PartySend(Character);
                    }
                    else
                    {
                        if (Character.Party.IsFull()) return;
                        if (Character.Zone != x.Character.Zone) return;

                        CharacterHandler.PartySend(x.Character);
                    }  
                });
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void PartyTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME || CharacterHandler.IsUntouchable()) return;
            if (!Character.Party.IsInParty()) return;
            if (Character.IsTrading) return;

            Character.Party.Members.ForEach(x =>
            {
                if (Character.GetRepresentClass(Character.Class) == (int)ClassRepresentType.CLASS_REPRESENT_ROGUE && Controller.GetControl("SwiftPartyMemberCheckBox", true))
                {
                    var character = Character;

                    if (x.MemberId != Character.Id)
                        character = CharacterHandler.PlayerList.FirstOrDefault(y => y.Id == x.MemberId)!;

                    if (character != null && !character.IsDead())
                    {
                        if (character.Id == Character.Id) return;

                        if (character.Speed == 45)
                        {
                            var skill = Character.SkillList.FirstOrDefault(x => x.Id.ToString().Substring(0, 3) == Character.Class.ToString() && x.BaseId == 107010); //Swift

                            if (skill != null &&
                                !CharacterHandler.SkillQueue.Any(x => x.Id == skill.Id) &&
                                    Character.Mp >= skill.Mana)
                            {
                                skill.SetTarget(character.Id);
                                CharacterHandler.SkillQueue.Enqueue(skill);
                                character.Speed = 67;
                            }
                        }
                    }
                }
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void ChangeCampTo1Button_Click(object sender, EventArgs e)
    {
        CharacterHandler.ZoneChange(1);
    }

    private void ChangeCampTo2Button_Click(object sender, EventArgs e)
    {
        CharacterHandler.ZoneChange(5);
    }

    private void LoadWarpListButton_Click(object sender, EventArgs e)
    {
        if (Controller == null) return;
        if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME) return;

        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Character.Zone)!;

        //if (zoneData != null)
        //{
        //    var gateObject = zoneData.ObjectEventArray
        //        .OrderBy(x => Vector2.Distance(new Vector2(x.fPosX, x.fPosZ), Character.GetPosition2D()))
        //        .FirstOrDefault(x => x.iNation == Character.NationId && x.sType == 5)!;

        //    var distance = Vector2.Distance(new Vector2(gateObject.fPosX, gateObject.fPosZ), Character.GetPosition2D());

        //    if (distance <= 3.0f)
        //        CharacterHandler.ObjectEvent(gateObject.sIndex, gateObject.sControlNpcID);
        //}

        
        GateListDataGrid.Update();
    }

    private void GoToNearestGateButton_Click(object sender, EventArgs e)
    {
        if (Controller == null) return;
        if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME) return;

        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Character.Zone)!;

        if (zoneData != null)
        {
            var gateObject = zoneData.ObjectPostData.ShapeManager.Shapes
                .OrderBy(x => Vector2.Distance(new Vector2(x.GetPosition.X, x.GetPosition.Z), Character.GetPosition2D()))
                .FirstOrDefault(x => x.GetBelong == Character.NationId && x.GetEventType == 5)!;

            var distance = Vector2.Distance(new Vector2(gateObject.GetPosition.X, gateObject.GetPosition.Z), Character.GetPosition2D());

            if (distance > 100.0f)
            {
                var routeData = new List<RouteData>()
                {
                    new RouteData() { Action = RouteActionType.TOWN, X = 0, Y = 0, Z = 0 },

                    new RouteData() {
                        Action = RouteActionType.OBJECTEVENT,
                        X = (float)Math.Round(gateObject.GetPosition.X, 1),
                        Y = (float)Math.Round(gateObject.GetPosition.Z, 1),
                        Z = (float)Math.Round(gateObject.GetPosition.Y, 1),
                        EventId = (short)gateObject.GetEventId,
                        ObjectId = (short)gateObject.GetNpcId
                    }
                };

                // Self action
                if (CharacterHandler.IsRouting())
                    CharacterHandler.RouteQueue.Clear();

                CharacterHandler.Route(routeData);

                // Forward action to all followers
                var followers = CharacterHandler.GetFollowersAtSameZone();

                followers.ForEach(x =>
                {
                    if (x.CharacterHandler.IsRouting())
                        x.CharacterHandler.RouteQueue.Clear();

                    x.CharacterHandler.Route(routeData);
                });
            }
            //else if (distance <= 10.0f)
            //    CharacterHandler.ObjectEvent((short)gateObject.GetEventId, (short)gateObject.GetNpcId);
            else
            {
                var routeData = new List<RouteData>()
                {
                    new RouteData() {
                        Action = RouteActionType.OBJECTEVENT,
                        X = (float)Math.Round(gateObject.GetPosition.X, 1),
                        Y = (float)Math.Round(gateObject.GetPosition.Z, 1),
                        Z = (float)Math.Round(gateObject.GetPosition.Y, 1),
                        EventId = (short)gateObject.GetEventId,
                        ObjectId = (short)gateObject.GetNpcId
                    }
                };

                // Self action
                if (CharacterHandler.IsRouting())
                    CharacterHandler.RouteQueue.Clear();

                CharacterHandler.ObjectEvent((short)gateObject.GetEventId, (short)gateObject.GetNpcId);

                CharacterHandler.Route(routeData);

                // Forward action to all followers
                var followers = CharacterHandler.GetFollowersAtSameZone();

                followers.ForEach(x =>
                {
                    if (x.CharacterHandler.IsRouting())
                        x.CharacterHandler.RouteQueue.Clear();

                    x.CharacterHandler.ObjectEvent((short)gateObject.GetEventId, (short)gateObject.GetNpcId);

                    x.CharacterHandler.Route(routeData);
                });
            }
        }
    }

    private void GateTeleportButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in GateListDataGrid.SelectedRows)
        {
            var warpInfo = (Data.Models.WarpInfo)row.DataBoundItem;

            if (warpInfo != null)
            {
                // Self action
                CharacterHandler.WarpTeleport(warpInfo);

                // Forward action to all followers
                var followers = CharacterHandler.GetFollowersAtSameZone();

                followers.ForEach(x =>
                {
                    x.CharacterHandler.WarpTeleport(warpInfo);
                });
            }
        }
    }

    private void UITimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME) return;

            var partyMembers = Character.Party.Members;

            if (PartyListDataGrid.RowCount != partyMembers.Count())
            {
                PartyListDataGrid.DataSource = null;
                PartyListDataGrid.DataSource = partyMembers;

                CurrencyManager cm = (CurrencyManager)PartyListDataGrid.BindingContext[partyMembers];

                if (cm != null)
                    cm.Refresh();
            }
                
            PartyListDataGrid.Refresh();

            var playerList = CharacterHandler.GetPlayerList();

            if (NearbyPlayerListDataGrid.RowCount != playerList.Count())
            {
                NearbyPlayerListDataGrid.DataSource = null;
                NearbyPlayerListDataGrid.DataSource = playerList;

                CurrencyManager cm = (CurrencyManager)NearbyPlayerListDataGrid.BindingContext[playerList];

                if (cm != null)
                    cm.Refresh();
            }

            NearbyPlayerListDataGrid.Refresh();

            var warpList = CharacterHandler.WarpList.ToList();

            if (GateListDataGrid.RowCount != warpList.Count())
            {
                GateListDataGrid.DataSource = null;
                GateListDataGrid.DataSource = warpList;

                CurrencyManager cm = (CurrencyManager)GateListDataGrid.BindingContext[warpList];

                if (cm != null)
                    cm.Refresh();
            }

            GateListDataGrid.Refresh();

            var npcShopList = CharacterHandler.NpcList
            .FindAll(x => x.MonsterOrNpc == 2 && (x.FamilyType == 21 || x.FamilyType == 22 || x.FamilyType == 41));

            if (NpcShopDataList.RowCount != npcShopList.Count())
            {
                NpcShopDataList.DataSource = null;
                NpcShopDataList.DataSource = npcShopList;

                CurrencyManager cm = (CurrencyManager)NpcShopDataList.BindingContext[npcShopList];

                if (cm != null)
                    cm.Refresh();
            }

            NpcShopDataList.Refresh();

            if (Character.NpcEventGroup != 0)
            {
                ItemListButton.ForeColor = Color.LimeGreen;
                ItemListButton.Enabled = true;
            }
            else
            {
                ItemListButton.ForeColor = Color.Black;
                ItemListButton.Enabled = false;
            }

            MapGroupBox.Text = $"Minimap ({Character.X}, {Character.Y})";

            if (GateListDataGrid.RowCount > 0)
            {
                GateTeleportButton.ForeColor = Color.LimeGreen;
                GateTeleportButton.Enabled = true;
            }
            else
            {
                GateTeleportButton.ForeColor = Color.Black;
                GateTeleportButton.Enabled = false;
            }

            var activeQuestList = Character.ActiveQuestList
                .FindAll(x => x.Status == 1)
                .OrderBy(x => x.LuaName)
                .ToList();

            if (RunningQuestListDataGrid.RowCount != activeQuestList.Count)
            {
                RunningQuestListDataGrid.DataSource = null;
                RunningQuestListDataGrid.DataSource = activeQuestList;
                RunningQuestListDataGrid.Refresh();
                ActiveQuestListTab.Text = $"Running ({activeQuestList.Count})";
            }

            var finishedQuestList = Character.ActiveQuestList
                .FindAll(x => x.Status == 2)
                .OrderBy(x => x.LuaName)
                .ToList();

            if (FinishedQuestListDataGrid.RowCount != finishedQuestList.Count)
            {
                FinishedQuestListDataGrid.DataSource = null;
                FinishedQuestListDataGrid.DataSource = finishedQuestList;
                FinishedQuestListDataGrid.Refresh();
                FinishedQuestListTab.Text = $"Finished ({finishedQuestList.Count})";
            }

            var completedQuestList = Character.ActiveQuestList
                .FindAll(x => x.Status == 3)
                .OrderBy(x => x.LuaName)
                .ToList();

            if (CompletedQuestListDataGrid.RowCount != completedQuestList.Count)
            {
                CompletedQuestListDataGrid.DataSource = null;
                CompletedQuestListDataGrid.DataSource = completedQuestList;
                CompletedQuestListDataGrid.Refresh();
                CompletedTabPage.Text = $"Completed ({completedQuestList.Count})";
            }

            var questList = CharacterHandler.QuestList
                .FindAll(x => !activeQuestList.Any(y => y.Id == x.Id) && !completedQuestList.Any(y => y.Id == x.Id))
                .OrderBy(x => x.LuaName)
                .ToList();

            if (QuestListViewDataGrid.RowCount != questList.Count)
            {
                QuestListViewDataGrid.DataSource = null;
                QuestListViewDataGrid.DataSource = questList;
                QuestListViewDataGrid.Refresh();
                QuestListTab.Text = $"Quests ({questList.Count})";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void MasterCharacter_TextChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;
        Controller.SetControl(MasterCharacter.Name, MasterCharacter.Text);
    }

    private void SendTradeMasterNearby_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;
        Controller.SetControl(SendTradeMasterNearby.Name, SendTradeMasterNearby.Checked);
    }

    private void MasterGiveNoahAmount_ValueChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;
        Controller.SetControl(MasterGiveNoahAmount.Name, MasterGiveNoahAmount.Value);
    }

    private void MasterCharacterTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME) return;
            if (!Controller.GetControl("SendTradeMasterNearby", false)) return;

            var master = CharacterHandler.PlayerList.FirstOrDefault(x => x.Name == Controller.GetControl("MasterCharacter", ""));
            var giveNoah = Controller.GetControl("MasterGiveNoahAmount", 0);

            if (master != null && Vector3.Distance(master.GetPosition(), Character.GetPosition()) <= 5.0f && Character.IsTrading == false && Character.Gold >= (giveNoah + 500000))
                CharacterHandler.ExhangeRequest(master.Id);

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void BotButton_Click(object sender, EventArgs e)
    {
        if (Controller == null) return;

        if (Controller.GetControl("Bot", false))
            Controller.SetControl("Bot", false);
        else
            Controller.SetControl("Bot", true);
    }

    private void LoadNpcListButton_Click(object sender, EventArgs e)
    {
        if (Character == null) return;
        
        QuestNpcList.DataSource = CharacterHandler.NpcList
            .FindAll(x => x.MonsterOrNpc == 2 && x.FamilyType != 11 && x.FamilyType != 15 && x.FamilyType != 74 && x.FamilyType != 24 && x.FamilyType != 174);
    }

    private void GoToNpcButton_Click(object sender, EventArgs e)
    {
        /*foreach (DataGridViewRow row in QuestNpcList.SelectedRows)
        {
            var character = (Character)row.DataBoundItem;

            if (character != null)
            {
                CharacterHandler.Route(new List<RouteData>()
                {
                    new RouteData() { Action = RouteActionType.MOVE, X = character.X, Y = character.Y, Z = character.Z },

                    new RouteData() {
                        Action = RouteActionType.NPCEVENT,
                        X = character.X,
                        Y = character.Y,
                        Z = character.Z,
                        NpcId = character.Id
                    }

                });
            }
        }*/

        // Self action
        CharacterHandler.LoadQuestList();

        // Forward action to all followers
        var followers = CharacterHandler.GetFollowersAtSameZone();

        followers.ForEach(x =>
        {
            x.CharacterHandler.LoadQuestList();
        });
    }

    private void TakeQuestButton_Click(object sender, EventArgs e)
    {
        /*if(QuestListViewDataGrid.SelectedRows.Count > 1)
        {
            MessageBox.Show("You cannot take one more than task at same time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }*/

        foreach (DataGridViewRow row in QuestListViewDataGrid.SelectedRows)
        {
            var quest = (Quest)row.DataBoundItem;
            if (quest == null) continue;

            var npc = CharacterHandler.NpcList.FirstOrDefault(x => x.ProtoId == quest.NpcProtoId)!;

            if (npc == null) continue;

            // Self action
            if (!Character.ActiveQuestList.Any(x => x.Id == quest.Id))
                CharacterHandler.QuestTake((uint)quest.BaseId);

            // Forward action to all followers
            var followers = CharacterHandler.GetFollowersAtSameZone();

            followers.ForEach(x =>
            {
                if (!x.Character.ActiveQuestList.Any(x => x.Id == quest.Id))
                    x.CharacterHandler.QuestTake((uint)quest.BaseId);
            });
        }
    }

    private void RemoveQuestButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in RunningQuestListDataGrid.SelectedRows)
        {
            var quest = (Quest)row.DataBoundItem;

            if (quest == null) continue;

            // Self action
            CharacterHandler.QuestRemove((uint)quest.BaseId);

            // Forward action to all followers
            var followers = CharacterHandler.GetFollowersAtSameZone();

            followers.ForEach(x =>
            {
                x.CharacterHandler.QuestRemove((uint)quest.BaseId);
            });
        }
    }

    private void QuestTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME || CharacterHandler.IsRouting()) return;
            if (!Controller.GetControl("Quest", false)) return;

            var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Character.Zone)!;

            var runningQuest = Character.ActiveQuestList
                .FindAll(x =>
                {
                    if (x.TargetNpc != null && 
                        x.TargetNpc.Any(x => x.ProtoId != 0 && x.KillCount < x.NeededKillCount) && !x.GetQuestNpcPosition().Equals(Vector3.Zero) && x.NpcZone == Character.Zone)
                        return true;

                    if (x.CollectItem != null && 
                        x.CollectItem.Any(x => x.CollectableNpcProtoId != 0 && !x.GetCollectableNpcPosition().Equals(Vector3.Zero) && x.CollectableNpcZone == Character.Zone))
                        return true;

                    return false;
                })
                .OrderBy(x => x.MinLevel)
                .FirstOrDefault(x => x.Status == 1)!;

            if (runningQuest != null)
            {
                if (runningQuest.TargetNpc != null)
                {
                    var target = runningQuest.TargetNpc.FirstOrDefault(x => x.ProtoId != 0 && x.KillCount < x.NeededKillCount)!;

                    Vector3 targetPosition = new Vector3(target.NpcX, target.NpcY, 0.0f);

                    targetPosition.Z = zoneData.GetHeightBy2DPos(targetPosition.X, targetPosition.Y);

                    if (Vector3.Distance(Character.GetPosition(), targetPosition) > 65.0f)
                        Character.SetMovePosition(targetPosition);

                    if (!Character.SelectedTargetList.Any(x => x.Id == target.ProtoId))
                    {
                        var selectedTarget = TableHandler.GetMonsterList().FirstOrDefault(x => x.Id == target.ProtoId);

                        if (selectedTarget != null)
                        {
                            TargetCheckedListBox.DataSource = null;

                            Character.SelectedTargetList.Clear();
                            Character.SelectedTargetList.Add(selectedTarget);

                            Controller.SetControl("SelectedTargetList", JsonSerializer.Serialize(Character.SelectedTargetList.Select(e => e.Id)));

                            TargetCheckedListBox.DataSource = Character.SelectedTargetList;
                            TargetCheckedListBox.DisplayMember = "Name";

                            for (int i = 0; i <= TargetCheckedListBox.Items.Count - 1; i++)
                                TargetCheckedListBox.SetItemCheckState(i, CheckState.Checked);
                        }
                    }
                }
                else
                {
                    if (runningQuest.CollectItem != null)
                    {
                        var collectableNpc = runningQuest.CollectItem.FirstOrDefault(x => x.CollectableNpcProtoId != 0)!;

                        Vector3 targetPosition = new Vector3(collectableNpc.CollectableNpcX, collectableNpc.CollectableNpcY, 0.0f);

                        targetPosition.Z = zoneData.GetHeightBy2DPos(targetPosition.X, targetPosition.Y);

                        if (Vector3.Distance(Character.GetPosition(), targetPosition) > 65.0f)
                            Character.SetMovePosition(targetPosition);
                        else
                        {
                            if (!Character.SelectedTargetList.Any(x => x.Id == collectableNpc.CollectableNpcProtoId))
                            {
                                var selectedTarget = TableHandler.GetMonsterList().FirstOrDefault(x => x.Id == collectableNpc.CollectableNpcProtoId);

                                if (selectedTarget != null)
                                {
                                    TargetCheckedListBox.DataSource = null;

                                    Character.SelectedTargetList.Clear();
                                    Character.SelectedTargetList.Add(selectedTarget);

                                    Controller.SetControl("SelectedTargetList", JsonSerializer.Serialize(Character.SelectedTargetList.Select(e => e.Id)));

                                    TargetCheckedListBox.DataSource = Character.SelectedTargetList;
                                    TargetCheckedListBox.DisplayMember = "Name";

                                    for (int i = 0; i <= TargetCheckedListBox.Items.Count - 1; i++)
                                        TargetCheckedListBox.SetItemCheckState(i, CheckState.Checked);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                var completedQuest = Character.ActiveQuestList
                    .FindAll(x => !x.GetQuestNpcPosition().Equals(Vector3.Zero) && x.NpcZone == Character.Zone)
                    .OrderBy(x => x.MinLevel)
                    .FirstOrDefault(x => x.Status == 3)!;

                if (completedQuest != null)
                {
                    Vector3 targetPosition = completedQuest.GetQuestNpcPosition();

                    targetPosition.Z = zoneData.GetHeightBy2DPos(targetPosition.X, targetPosition.Y);

                    if (Character.GetPosition() != targetPosition)
                        Character.SetMovePosition(targetPosition);
                    else
                    {
                        var character = CharacterHandler.GetNpcList().FirstOrDefault(x => x.ProtoId == completedQuest.NpcProtoId)!;

                        if (character != null)
                        {
                            CharacterHandler.NpcEvent(character.Id);

                            CharacterHandler.QuestGive((uint)completedQuest.BaseId);
                            CharacterHandler.SelectMenu(0, completedQuest.LuaName, false);
                            CharacterHandler.QuestCompleted((uint)completedQuest.BaseId);

                            completedQuest.Status = 2;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void QuestButton_Click(object sender, EventArgs e)
    {
        if (Controller == null) return;

        if (Controller.GetControl("Quest", false))
            Controller.SetControl("Quest", false);
        else
            Controller.SetControl("Quest", true);
    }

    private void RemoveCompletedQuestButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in CompletedQuestListDataGrid.SelectedRows)
        {
            var quest = (Quest)row.DataBoundItem;

            if (quest == null) continue;

            // Self action
            CharacterHandler.QuestRemove((uint)quest.BaseId);

            // Forward action to all followers
            var followers = CharacterHandler.GetFollowersAtSameZone();

            followers.ForEach((x) =>
            {
                x.CharacterHandler.QuestRemove((uint)quest.BaseId);
            });
        }
    }

    private async void GiveQuestButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in CompletedQuestListDataGrid.SelectedRows)
        {
            var quest = (Quest)row.DataBoundItem;

            if (quest == null) continue;

            var npc = CharacterHandler.GetNpcList().FirstOrDefault(x => x.ProtoId == quest.NpcProtoId)!;

            if (npc != null)
            {
                // Self action
                await Task.Run(async () =>
                {
                    CharacterHandler.NpcEvent(npc.Id);
                    await Task.Delay(250);
                    CharacterHandler.QuestGive((uint)quest.BaseId);
                    await Task.Delay(250);
                    CharacterHandler.SelectMenu(0, quest.LuaName, false);
                    var questHelper = TableHandler.GetQuestHelperList()
                        .FirstOrDefault(x => x.EventDataIndex == quest.Id && x.EventStatus == 1);

                    if (questHelper != null)
                        CharacterHandler.QuestCompleted((uint)questHelper.EventDataIndex);
                });

                // Forward action to all followers
                var followers = CharacterHandler.GetFollowersAtSameZone();

                followers.ForEach(async (x) =>
                {
                    await Task.Run(async () =>
                    {
                        x.CharacterHandler.NpcEvent(npc.Id);
                        await Task.Delay(250);
                        x.CharacterHandler.QuestGive((uint)quest.BaseId);
                        await Task.Delay(250);
                        x.CharacterHandler.SelectMenu(0, quest.LuaName, false);
                        var questHelper = TableHandler.GetQuestHelperList()
                            .FirstOrDefault(y => y.EventDataIndex == quest.Id && y.EventStatus == 1);

                        if (questHelper != null)
                            x.CharacterHandler.QuestCompleted((uint)questHelper.EventDataIndex);
                    });
                });
            }

        }
    }

    private void AttackSpeed_ValueChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(AttackSpeed.Name, AttackSpeed.Value);
    }

    private void SpeedhackCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;
        Controller.SetControl(SpeedhackCheckbox.Name, SpeedhackCheckbox.Checked);
    }

    private void GetCurrentCoordinate_Click(object sender, EventArgs e)
    {
        MoveCoordinateX.Value = (decimal)Character.X;
        MoveCoordinateY.Value = (decimal)Character.Y;
    }

    private void MoveCoordinateDirect_Click(object sender, EventArgs e)
    {
        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Character.Zone)!;

        if (zoneData == null)
            return;

        var movePosition = new Vector3((float)MoveCoordinateX.Value, (float)MoveCoordinateY.Value, zoneData.GetHeightBy2DPos((float)MoveCoordinateX.Value, (float)MoveCoordinateY.Value));

        Character.SetMovePosition(movePosition);
    }

    private void MoveCoordinateWithRoute_Click(object sender, EventArgs e)
    {
        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Character.Zone)!;

        if (zoneData == null)
            return;

        var movePosition = new Vector3((float)MoveCoordinateX.Value, (float)MoveCoordinateY.Value, zoneData.GetHeightBy2DPos((float)MoveCoordinateX.Value, (float)MoveCoordinateY.Value));

        CharacterHandler.Route(new List<RouteData>()
        {
            new RouteData() { Action = RouteActionType.MOVE, X = movePosition.X, Y = movePosition.Y, Z = movePosition.Z },
        });
    }

    private async void SendPacket_Click(object sender, EventArgs e)
    {
        var repatCount = SendPacketRepeatCount.Value;
        var packetTextBoxArray = PacketTextBox.Lines.ToArray();
        var sendPacketDelay = SendPacketDelay.Value;

        Task t = new Task(async () =>
        {
            for (int a = 0; a < (int)repatCount; a++)
            {
                for (int b = 0; b < packetTextBoxArray.Length; b++)
                {
                    try
                    {
                        if (sendPacketCancelationTokenCt.IsCancellationRequested)
                        {
                            sendPacketCancelationTokenCt.ThrowIfCancellationRequested();
                        }
                    }
                    catch (Exception)
                    {
                        sendPacketCancelationToken = new CancellationTokenSource();
                        sendPacketCancelationTokenCt = sendPacketCancelationToken.Token;
                        return;
                    }

                    var packetText = packetTextBoxArray[b];
                    var packetArray = Convert.FromHexString(packetText);
                    var message = new KOF.Core.Communications.Message(packetArray.Length, packetArray);

                    await Client.Session.SendAsync(message);
                    await Task.Delay((int)sendPacketDelay);
                }
            }
        }, sendPacketCancelationTokenCt);

        if (!t.IsCanceled || !t.IsCanceled || !t.IsCompleted || !t.IsFaulted)
            t.Start();

    }

    private void DisableSkillCasting_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;
        Controller.SetControl(DisableSkillCasting.Name, DisableSkillCasting.Checked);
    }

    private void TargetSearchRange_ValueChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        Controller.SetControl(TargetSearchRange.Name, TargetSearchRange.Value);
    }

    private void ItemListButton_Click(object sender, EventArgs e)
    {
        if(Character.NpcEventGroup != 0)
        {
            ShopWindow shopBuyItem = new ShopWindow(Client);
            shopBuyItem.ShowDialog();
        }
    }

    private void GoToNpcShopButton_Click(object sender, EventArgs e)
    {
        Character.NpcEventGroup = 0;

        foreach (DataGridViewRow row in NpcShopDataList.SelectedRows)
        {
           var character = (Character)row.DataBoundItem;

           if (character != null)
           {
               CharacterHandler.Route(new List<RouteData>()
               {
                   new RouteData() { Action = RouteActionType.MOVE, X = character.X, Y = character.Y, Z = character.Z },

                   new RouteData() {
                       Action = RouteActionType.NPCEVENT,
                       X = character.X,
                       Y = character.Y,
                       Z = character.Z,
                       NpcId = character.Id
                   }
               });
           }
        }
    }

    private void ConvertMsToExp_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        if (ConvertMsToExp.Checked)
            AutoJoinMs.Enabled = false;
        else
            AutoJoinMs.Enabled = true;

        Controller.SetControl(ConvertMsToExp.Name, ConvertMsToExp.Checked);
    }

    private void AutoJoinMs_CheckedChanged(object sender, EventArgs e)
    {
        if (Controller == null) return;

        if (AutoJoinMs.Checked)
            ConvertMsToExp.Enabled = false;
        else
            ConvertMsToExp.Enabled = true;

        Controller.SetControl(AutoJoinMs.Name, AutoJoinMs.Checked);
    }

    private void MSAutoEvent_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME || CharacterHandler.IsUntouchable()) return;
            if (!Controller.GetControl("AutoJoinMs", false)) return;

            if (Character.IsInMonsterStone())
            {
                var monsterStonePhase = Controller.GetControl("MonsterStonePhase", 0);

                if(monsterStonePhase == 0)
                {
                    if (Character.GetPosition() == Character.GetMovePosition())
                        Controller.SetControl("MonsterStonePhase", 1);
                    else
                        Character.SetMovePosition(new Vector3(45.0f, 52.0f, 0.0f));
                }

                MSAutoEvent.Interval = 1000;
            }
            else
            {
                var monsterStone = Character.Inventory.FirstOrDefault(x => x.Name != null && x.Name.Contains("Monster Stone", StringComparison.InvariantCultureIgnoreCase));

                if (monsterStone == null) return;

                var selectedTarget = TableHandler.GetMonsterList().FindAll(x => x.Id == 9824 || x.Id == 8800 || x.Id == 8737 || x.Id == 8784);

                if (selectedTarget != null)
                {
                    TargetCheckedListBox.DataSource = null;

                    Character.SelectedTargetList.Clear();
                    
                    selectedTarget.ForEach(x =>
                    {
                        Character.SelectedTargetList.Add(x);
                    });

                    Controller.SetControl("SelectedTargetList", JsonSerializer.Serialize(Character.SelectedTargetList.Select(e => e.Id)));
                    
                    TargetCheckedListBox.DataSource = Character.SelectedTargetList;
                    TargetCheckedListBox.DisplayMember = "Name";

                    for (int i = 0; i <= TargetCheckedListBox.Items.Count - 1; i++)
                        TargetCheckedListBox.SetItemCheckState(i, CheckState.Checked);
                }

                Controller.SetControl("MonsterStonePhase", 0);

                CharacterHandler.Event((byte)EventOpCode.MONSTER_STONE, monsterStone.ItemID);

                MSAutoEvent.Interval = 5000;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void MSConvertExperience_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Controller == null) return;
            if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME || CharacterHandler.IsUntouchable()) return;
            if (!Controller.GetControl("ConvertMsToExp", false)) return;

            var monsterStone = Character.Inventory.Where(x => x.Name != null && x.Name.Contains("Monster Stone", StringComparison.InvariantCultureIgnoreCase)).ToList();

            if (monsterStone == null) return;

            monsterStone.ForEach(x =>
            {
                CharacterHandler.Event((byte)EventOpCode.MONSTER_STONE_EXP, x.ItemID);
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void SendPacketStop_Click(object sender, EventArgs e)
    {
        sendPacketCancelationToken.Cancel();
    }
}
