namespace KOF.UI.Forms;

partial class ClientController {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientController));
        MiniMap = new PictureBox();
        MapGroupBox = new GroupBox();
        FullScreenMapButton = new Button();
        RoutePlannerButton = new Button();
        tabControl1 = new TabControl();
        MainPage = new TabPage();
        groupBox19 = new GroupBox();
        ConvertMsToExp = new CheckBox();
        AutoJoinMs = new CheckBox();
        groupBox15 = new GroupBox();
        PrivateChatcheckBox = new CheckBox();
        TradeBlockcheckBox = new CheckBox();
        ExpSealcheckBox = new CheckBox();
        GodModeCheckBox = new CheckBox();
        HyperNoahCheckBox = new CheckBox();
        SpeedhackCheckbox = new CheckBox();
        groupBox13 = new GroupBox();
        KeepFollowingcheckBox = new CheckBox();
        gotoMasterCharacter = new Button();
        MasterGiveNoahAmount = new NumericUpDown();
        label4 = new Label();
        SendTradeMasterNearby = new CheckBox();
        MasterCharacter = new TextBox();
        label3 = new Label();
        groupBox7 = new GroupBox();
        SupplyItemDataGrid = new DataGridView();
        groupBox5 = new GroupBox();
        FastLootMoney = new CheckBox();
        MoveToLootCheckBox = new CheckBox();
        LootMinPrice = new NumericUpDown();
        label2 = new Label();
        EnableLoot = new CheckBox();
        ProtectionGroupBox = new GroupBox();
        MinorPercentageValue = new NumericUpDown();
        MinorCheckBox = new CheckBox();
        RegenerateWhenDie = new CheckBox();
        MpPotionPercentageValue = new NumericUpDown();
        HpPotionPercentageValue = new NumericUpDown();
        MpPotionCheckBox = new CheckBox();
        HpPotionCheckBox = new CheckBox();
        AttackPage = new TabPage();
        groupBox4 = new GroupBox();
        TargetSearchRange = new NumericUpDown();
        label9 = new Label();
        DisableSkillCasting = new CheckBox();
        label5 = new Label();
        AttackSpeed = new NumericUpDown();
        FollowTargetSync = new CheckBox();
        MoveToTarget = new CheckBox();
        AttackRange = new NumericUpDown();
        label1 = new Label();
        groupBox3 = new GroupBox();
        TargetListClearButton = new Button();
        SearchTargetButton = new Button();
        TargetCheckedListBox = new CheckedListBox();
        groupBox2 = new GroupBox();
        SelfSkillCheckedListBox = new CheckedListBox();
        SkillGroupBox = new GroupBox();
        AttackSkillCheckedListBox = new CheckedListBox();
        PartyPage = new TabPage();
        groupBox9 = new GroupBox();
        SendPartyPlayerNametextBox = new TextBox();
        SendPartyButton = new Button();
        NearbyPlayerListDataGrid = new DataGridView();
        PartyListGroupBox = new GroupBox();
        groupBox10 = new GroupBox();
        PartyAddPrefixtextBox = new TextBox();
        AutoPartycheckBox = new CheckBox();
        SummonButton = new Button();
        SwiftPartyMemberCheckBox = new CheckBox();
        PartyMakeLeaderButton = new Button();
        groupBox8 = new GroupBox();
        PartyKickButton = new Button();
        PartyDisbandButton = new Button();
        PartyDeclineButton = new Button();
        PartyAcceptButton = new Button();
        PartyListDataGrid = new DataGridView();
        ActionPage = new TabPage();
        groupBox18 = new GroupBox();
        ItemListButton = new Button();
        GoToNpcShopButton = new Button();
        NpcShopDataList = new DataGridView();
        groupBox14 = new GroupBox();
        tabControl2 = new TabControl();
        NpcListTab = new TabPage();
        GoToNpcButton = new Button();
        QuestNpcList = new DataGridView();
        LoadNpcListButton = new Button();
        QuestListTab = new TabPage();
        TakeQuestButton = new Button();
        QuestListViewDataGrid = new DataGridView();
        ActiveQuestListTab = new TabPage();
        RemoveRunningQuestButton = new Button();
        RunningQuestListDataGrid = new DataGridView();
        CompletedTabPage = new TabPage();
        GiveQuestButton = new Button();
        RemoveCompletedQuestButton = new Button();
        CompletedQuestListDataGrid = new DataGridView();
        FinishedQuestListTab = new TabPage();
        FinishedQuestListDataGrid = new DataGridView();
        groupBox12 = new GroupBox();
        GoToNearestGateButton = new Button();
        GateTeleportButton = new Button();
        GateListDataGrid = new DataGridView();
        ToolPage = new TabPage();
        btnSpawnCreature = new Button();
        groupBox16 = new GroupBox();
        GetCurrentCoordinate = new Button();
        MoveCoordinateY = new NumericUpDown();
        label7 = new Label();
        label6 = new Label();
        MoveCoordinateX = new NumericUpDown();
        MoveCoordinateWithRoute = new Button();
        MoveCoordinateDirect = new Button();
        groupBox17 = new GroupBox();
        SendPacketStop = new Button();
        SendPacketRepeatCount = new NumericUpDown();
        label10 = new Label();
        SendPacketDelay = new NumericUpDown();
        label8 = new Label();
        SendPacket = new Button();
        PacketTextBox = new RichTextBox();
        StatusGroupBox = new GroupBox();
        ManaProgressBar = new Extensions.TextProgressBar();
        HealthProgressBar = new Extensions.TextProgressBar();
        ExperienceProgressBar = new Extensions.TextProgressBar();
        MiniMapTimer = new System.Windows.Forms.Timer(components);
        StatusTimer = new System.Windows.Forms.Timer(components);
        groupBox1 = new GroupBox();
        QuestButton = new Button();
        button1 = new Button();
        SkillInfoButton = new Button();
        CharacterInfoButton = new Button();
        InventoryButton = new Button();
        SelfSkillButton = new Button();
        AttackButton = new Button();
        BotButton = new Button();
        PressOkButton = new Button();
        TownButton = new Button();
        SelfSkillTimer = new System.Windows.Forms.Timer(components);
        groupBox6 = new GroupBox();
        FollowOnlyNearby = new CheckBox();
        FollowSelect = new ComboBox();
        ProtectionTimer = new System.Windows.Forms.Timer(components);
        SupplyTimer = new System.Windows.Forms.Timer(components);
        AutoPartyTimer = new System.Windows.Forms.Timer(components);
        PartyTimer = new System.Windows.Forms.Timer(components);
        groupBox11 = new GroupBox();
        ChangeCampTo2Button = new Button();
        ChangeCampTo1Button = new Button();
        UITimer = new System.Windows.Forms.Timer(components);
        MasterCharacterTimer = new System.Windows.Forms.Timer(components);
        QuestTimer = new System.Windows.Forms.Timer(components);
        MSConvertExperience = new System.Windows.Forms.Timer(components);
        MSAutoEvent = new System.Windows.Forms.Timer(components);
        ((System.ComponentModel.ISupportInitialize)MiniMap).BeginInit();
        MapGroupBox.SuspendLayout();
        tabControl1.SuspendLayout();
        MainPage.SuspendLayout();
        groupBox19.SuspendLayout();
        groupBox15.SuspendLayout();
        groupBox13.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)MasterGiveNoahAmount).BeginInit();
        groupBox7.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)SupplyItemDataGrid).BeginInit();
        groupBox5.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)LootMinPrice).BeginInit();
        ProtectionGroupBox.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)MinorPercentageValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)MpPotionPercentageValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)HpPotionPercentageValue).BeginInit();
        AttackPage.SuspendLayout();
        groupBox4.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)TargetSearchRange).BeginInit();
        ((System.ComponentModel.ISupportInitialize)AttackSpeed).BeginInit();
        ((System.ComponentModel.ISupportInitialize)AttackRange).BeginInit();
        groupBox3.SuspendLayout();
        groupBox2.SuspendLayout();
        SkillGroupBox.SuspendLayout();
        PartyPage.SuspendLayout();
        groupBox9.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)NearbyPlayerListDataGrid).BeginInit();
        PartyListGroupBox.SuspendLayout();
        groupBox10.SuspendLayout();
        groupBox8.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)PartyListDataGrid).BeginInit();
        ActionPage.SuspendLayout();
        groupBox18.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)NpcShopDataList).BeginInit();
        groupBox14.SuspendLayout();
        tabControl2.SuspendLayout();
        NpcListTab.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)QuestNpcList).BeginInit();
        QuestListTab.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)QuestListViewDataGrid).BeginInit();
        ActiveQuestListTab.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)RunningQuestListDataGrid).BeginInit();
        CompletedTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)CompletedQuestListDataGrid).BeginInit();
        FinishedQuestListTab.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)FinishedQuestListDataGrid).BeginInit();
        groupBox12.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)GateListDataGrid).BeginInit();
        ToolPage.SuspendLayout();
        groupBox16.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)MoveCoordinateY).BeginInit();
        ((System.ComponentModel.ISupportInitialize)MoveCoordinateX).BeginInit();
        groupBox17.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)SendPacketRepeatCount).BeginInit();
        ((System.ComponentModel.ISupportInitialize)SendPacketDelay).BeginInit();
        StatusGroupBox.SuspendLayout();
        groupBox1.SuspendLayout();
        groupBox6.SuspendLayout();
        groupBox11.SuspendLayout();
        SuspendLayout();
        // 
        // MiniMap
        // 
        MiniMap.ErrorImage = (Image)resources.GetObject("MiniMap.ErrorImage");
        MiniMap.Location = new Point(6, 22);
        MiniMap.Name = "MiniMap";
        MiniMap.Size = new Size(250, 250);
        MiniMap.TabIndex = 3;
        MiniMap.TabStop = false;
        MiniMap.MouseDown += Map_MouseDown;
        // 
        // MapGroupBox
        // 
        MapGroupBox.Controls.Add(FullScreenMapButton);
        MapGroupBox.Controls.Add(MiniMap);
        MapGroupBox.Location = new Point(6, 84);
        MapGroupBox.Name = "MapGroupBox";
        MapGroupBox.Size = new Size(262, 310);
        MapGroupBox.TabIndex = 4;
        MapGroupBox.TabStop = false;
        MapGroupBox.Text = "Minimap";
        // 
        // FullScreenMapButton
        // 
        FullScreenMapButton.Location = new Point(68, 278);
        FullScreenMapButton.Name = "FullScreenMapButton";
        FullScreenMapButton.Size = new Size(120, 23);
        FullScreenMapButton.TabIndex = 4;
        FullScreenMapButton.Text = "Fullscreen";
        FullScreenMapButton.UseVisualStyleBackColor = true;
        FullScreenMapButton.Click += FullScreenMapButton_Click;
        // 
        // RoutePlannerButton
        // 
        RoutePlannerButton.Location = new Point(6, 51);
        RoutePlannerButton.Name = "RoutePlannerButton";
        RoutePlannerButton.Size = new Size(120, 23);
        RoutePlannerButton.TabIndex = 5;
        RoutePlannerButton.Text = "Route Planner";
        RoutePlannerButton.UseVisualStyleBackColor = true;
        RoutePlannerButton.Click += RoutePlannerButton_Click;
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(MainPage);
        tabControl1.Controls.Add(AttackPage);
        tabControl1.Controls.Add(PartyPage);
        tabControl1.Controls.Add(ActionPage);
        tabControl1.Controls.Add(ToolPage);
        tabControl1.Location = new Point(274, 7);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(458, 804);
        tabControl1.TabIndex = 5;
        // 
        // MainPage
        // 
        MainPage.Controls.Add(groupBox19);
        MainPage.Controls.Add(groupBox15);
        MainPage.Controls.Add(groupBox13);
        MainPage.Controls.Add(groupBox7);
        MainPage.Controls.Add(groupBox5);
        MainPage.Controls.Add(ProtectionGroupBox);
        MainPage.Location = new Point(4, 24);
        MainPage.Name = "MainPage";
        MainPage.Padding = new Padding(3);
        MainPage.Size = new Size(450, 776);
        MainPage.TabIndex = 0;
        MainPage.Text = "Main";
        MainPage.UseVisualStyleBackColor = true;
        // 
        // groupBox19
        // 
        groupBox19.Controls.Add(ConvertMsToExp);
        groupBox19.Controls.Add(AutoJoinMs);
        groupBox19.Location = new Point(6, 368);
        groupBox19.Name = "groupBox19";
        groupBox19.Size = new Size(441, 71);
        groupBox19.TabIndex = 15;
        groupBox19.TabStop = false;
        groupBox19.Text = "Monster Stone";
        // 
        // ConvertMsToExp
        // 
        ConvertMsToExp.AutoSize = true;
        ConvertMsToExp.Location = new Point(6, 47);
        ConvertMsToExp.Name = "ConvertMsToExp";
        ConvertMsToExp.Size = new Size(248, 19);
        ConvertMsToExp.TabIndex = 1;
        ConvertMsToExp.Text = "Auto convert monster stone to experience";
        ConvertMsToExp.UseVisualStyleBackColor = true;
        ConvertMsToExp.CheckedChanged += ConvertMsToExp_CheckedChanged;
        // 
        // AutoJoinMs
        // 
        AutoJoinMs.AutoSize = true;
        AutoJoinMs.Location = new Point(6, 22);
        AutoJoinMs.Name = "AutoJoinMs";
        AutoJoinMs.Size = new Size(75, 19);
        AutoJoinMs.TabIndex = 0;
        AutoJoinMs.Text = "Auto join";
        AutoJoinMs.UseVisualStyleBackColor = true;
        AutoJoinMs.CheckedChanged += AutoJoinMs_CheckedChanged;
        // 
        // groupBox15
        // 
        groupBox15.Controls.Add(PrivateChatcheckBox);
        groupBox15.Controls.Add(TradeBlockcheckBox);
        groupBox15.Controls.Add(ExpSealcheckBox);
        groupBox15.Controls.Add(GodModeCheckBox);
        groupBox15.Controls.Add(HyperNoahCheckBox);
        groupBox15.Controls.Add(SpeedhackCheckbox);
        groupBox15.Location = new Point(6, 119);
        groupBox15.Name = "groupBox15";
        groupBox15.Size = new Size(442, 73);
        groupBox15.TabIndex = 18;
        groupBox15.TabStop = false;
        groupBox15.Text = "Features";
        // 
        // PrivateChatcheckBox
        // 
        PrivateChatcheckBox.AutoSize = true;
        PrivateChatcheckBox.Checked = true;
        PrivateChatcheckBox.CheckState = CheckState.Indeterminate;
        PrivateChatcheckBox.Location = new Point(289, 47);
        PrivateChatcheckBox.Name = "PrivateChatcheckBox";
        PrivateChatcheckBox.Size = new Size(76, 19);
        PrivateChatcheckBox.TabIndex = 23;
        PrivateChatcheckBox.Text = "PM Block";
        PrivateChatcheckBox.UseVisualStyleBackColor = true;
        PrivateChatcheckBox.CheckedChanged += PrivateChatcheckBox_CheckedChanged;
        // 
        // TradeBlockcheckBox
        // 
        TradeBlockcheckBox.AutoSize = true;
        TradeBlockcheckBox.Checked = true;
        TradeBlockcheckBox.CheckState = CheckState.Indeterminate;
        TradeBlockcheckBox.Location = new Point(289, 22);
        TradeBlockcheckBox.Name = "TradeBlockcheckBox";
        TradeBlockcheckBox.Size = new Size(86, 19);
        TradeBlockcheckBox.TabIndex = 22;
        TradeBlockcheckBox.Text = "Trade Block";
        TradeBlockcheckBox.UseVisualStyleBackColor = true;
        TradeBlockcheckBox.CheckedChanged += TradeBlockcheckBox_CheckedChanged;
        // 
        // ExpSealcheckBox
        // 
        ExpSealcheckBox.AutoSize = true;
        ExpSealcheckBox.Checked = true;
        ExpSealcheckBox.CheckState = CheckState.Indeterminate;
        ExpSealcheckBox.Location = new Point(133, 47);
        ExpSealcheckBox.Name = "ExpSealcheckBox";
        ExpSealcheckBox.Size = new Size(129, 19);
        ExpSealcheckBox.TabIndex = 21;
        ExpSealcheckBox.Text = "Exp Seal ( 30 lv. >=)";
        ExpSealcheckBox.UseVisualStyleBackColor = true;
        ExpSealcheckBox.CheckedChanged += ExpSealcheckBox_CheckedChanged;
        // 
        // GodModeCheckBox
        // 
        GodModeCheckBox.AutoSize = true;
        GodModeCheckBox.Location = new Point(6, 22);
        GodModeCheckBox.Name = "GodModeCheckBox";
        GodModeCheckBox.Size = new Size(82, 19);
        GodModeCheckBox.TabIndex = 17;
        GodModeCheckBox.Text = "God mode";
        GodModeCheckBox.UseVisualStyleBackColor = true;
        GodModeCheckBox.CheckedChanged += GodModeCheckBox_CheckedChanged;
        // 
        // HyperNoahCheckBox
        // 
        HyperNoahCheckBox.AutoSize = true;
        HyperNoahCheckBox.Location = new Point(6, 47);
        HyperNoahCheckBox.Name = "HyperNoahCheckBox";
        HyperNoahCheckBox.Size = new Size(88, 19);
        HyperNoahCheckBox.TabIndex = 18;
        HyperNoahCheckBox.Text = "Hyper noah";
        HyperNoahCheckBox.UseVisualStyleBackColor = true;
        HyperNoahCheckBox.CheckedChanged += HyperNoahCheckBox_CheckedChanged;
        // 
        // SpeedhackCheckbox
        // 
        SpeedhackCheckbox.AutoSize = true;
        SpeedhackCheckbox.Location = new Point(134, 22);
        SpeedhackCheckbox.Name = "SpeedhackCheckbox";
        SpeedhackCheckbox.Size = new Size(83, 19);
        SpeedhackCheckbox.TabIndex = 20;
        SpeedhackCheckbox.Text = "Speedhack";
        SpeedhackCheckbox.UseVisualStyleBackColor = true;
        SpeedhackCheckbox.CheckedChanged += SpeedhackCheckbox_CheckedChanged;
        // 
        // groupBox13
        // 
        groupBox13.Controls.Add(KeepFollowingcheckBox);
        groupBox13.Controls.Add(gotoMasterCharacter);
        groupBox13.Controls.Add(MasterGiveNoahAmount);
        groupBox13.Controls.Add(label4);
        groupBox13.Controls.Add(SendTradeMasterNearby);
        groupBox13.Controls.Add(MasterCharacter);
        groupBox13.Controls.Add(label3);
        groupBox13.Location = new Point(6, 279);
        groupBox13.Name = "groupBox13";
        groupBox13.Size = new Size(442, 84);
        groupBox13.TabIndex = 1;
        groupBox13.TabStop = false;
        groupBox13.Text = "Master Character";
        // 
        // KeepFollowingcheckBox
        // 
        KeepFollowingcheckBox.AutoSize = true;
        KeepFollowingcheckBox.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        KeepFollowingcheckBox.Location = new Point(167, 18);
        KeepFollowingcheckBox.Name = "KeepFollowingcheckBox";
        KeepFollowingcheckBox.Size = new Size(76, 30);
        KeepFollowingcheckBox.TabIndex = 6;
        KeepFollowingcheckBox.Text = "Keep\r\nfollowing";
        KeepFollowingcheckBox.UseVisualStyleBackColor = true;
        KeepFollowingcheckBox.CheckedChanged += KeepFollowingcheckBox_CheckedChanged;
        // 
        // gotoMasterCharacter
        // 
        gotoMasterCharacter.Location = new Point(6, 46);
        gotoMasterCharacter.Name = "gotoMasterCharacter";
        gotoMasterCharacter.Size = new Size(155, 23);
        gotoMasterCharacter.TabIndex = 5;
        gotoMasterCharacter.Text = "go if nearby";
        gotoMasterCharacter.UseVisualStyleBackColor = true;
        gotoMasterCharacter.Click += gotoMasterCharacter_Click;
        // 
        // MasterGiveNoahAmount
        // 
        MasterGiveNoahAmount.Increment = new decimal(new int[] { 1000000, 0, 0, 0 });
        MasterGiveNoahAmount.Location = new Point(340, 51);
        MasterGiveNoahAmount.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
        MasterGiveNoahAmount.Name = "MasterGiveNoahAmount";
        MasterGiveNoahAmount.Size = new Size(92, 23);
        MasterGiveNoahAmount.TabIndex = 4;
        MasterGiveNoahAmount.TextAlign = HorizontalAlignment.Center;
        MasterGiveNoahAmount.ValueChanged += MasterGiveNoahAmount_ValueChanged;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(245, 54);
        label4.Name = "label4";
        label4.Size = new Size(81, 15);
        label4.TabIndex = 3;
        label4.Text = "Give Noah >=";
        // 
        // SendTradeMasterNearby
        // 
        SendTradeMasterNearby.AutoSize = true;
        SendTradeMasterNearby.Location = new Point(301, 18);
        SendTradeMasterNearby.Name = "SendTradeMasterNearby";
        SendTradeMasterNearby.Size = new Size(131, 19);
        SendTradeMasterNearby.TabIndex = 2;
        SendTradeMasterNearby.Text = "Send trade if nearby";
        SendTradeMasterNearby.UseVisualStyleBackColor = true;
        SendTradeMasterNearby.CheckedChanged += SendTradeMasterNearby_CheckedChanged;
        // 
        // MasterCharacter
        // 
        MasterCharacter.Location = new Point(61, 18);
        MasterCharacter.Name = "MasterCharacter";
        MasterCharacter.PlaceholderText = "Character Name";
        MasterCharacter.Size = new Size(100, 23);
        MasterCharacter.TabIndex = 1;
        MasterCharacter.TextChanged += MasterCharacter_TextChanged;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(6, 22);
        label3.Name = "label3";
        label3.Size = new Size(49, 15);
        label3.TabIndex = 0;
        label3.Text = "Master :";
        // 
        // groupBox7
        // 
        groupBox7.Controls.Add(SupplyItemDataGrid);
        groupBox7.Location = new Point(6, 445);
        groupBox7.Name = "groupBox7";
        groupBox7.Size = new Size(442, 212);
        groupBox7.TabIndex = 12;
        groupBox7.TabStop = false;
        groupBox7.Text = "Supply Management";
        // 
        // SupplyItemDataGrid
        // 
        SupplyItemDataGrid.AllowUserToAddRows = false;
        SupplyItemDataGrid.AllowUserToDeleteRows = false;
        SupplyItemDataGrid.AllowUserToResizeColumns = false;
        SupplyItemDataGrid.AllowUserToResizeRows = false;
        SupplyItemDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        SupplyItemDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        SupplyItemDataGrid.BackgroundColor = Color.White;
        SupplyItemDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        SupplyItemDataGrid.Location = new Point(4, 21);
        SupplyItemDataGrid.Name = "SupplyItemDataGrid";
        SupplyItemDataGrid.RowTemplate.Height = 25;
        SupplyItemDataGrid.Size = new Size(432, 186);
        SupplyItemDataGrid.TabIndex = 0;
        SupplyItemDataGrid.CellBeginEdit += SupplyItemDataGrid_CellBeginEdit;
        SupplyItemDataGrid.CellEndEdit += SupplyItemDataGrid_CellEndEdit;
        // 
        // groupBox5
        // 
        groupBox5.Controls.Add(FastLootMoney);
        groupBox5.Controls.Add(MoveToLootCheckBox);
        groupBox5.Controls.Add(LootMinPrice);
        groupBox5.Controls.Add(label2);
        groupBox5.Controls.Add(EnableLoot);
        groupBox5.Location = new Point(6, 201);
        groupBox5.Name = "groupBox5";
        groupBox5.Size = new Size(442, 72);
        groupBox5.TabIndex = 11;
        groupBox5.TabStop = false;
        groupBox5.Text = "Loot Management";
        // 
        // FastLootMoney
        // 
        FastLootMoney.AutoSize = true;
        FastLootMoney.Location = new Point(106, 45);
        FastLootMoney.Name = "FastLootMoney";
        FastLootMoney.Size = new Size(150, 19);
        FastLootMoney.TabIndex = 20;
        FastLootMoney.Text = "Fast Loot (Only Money)";
        FastLootMoney.UseVisualStyleBackColor = true;
        FastLootMoney.CheckedChanged += FastLootMoney_CheckedChanged;
        // 
        // MoveToLootCheckBox
        // 
        MoveToLootCheckBox.AutoSize = true;
        MoveToLootCheckBox.Location = new Point(6, 45);
        MoveToLootCheckBox.Name = "MoveToLootCheckBox";
        MoveToLootCheckBox.Size = new Size(94, 19);
        MoveToLootCheckBox.TabIndex = 19;
        MoveToLootCheckBox.Text = "Move to loot";
        MoveToLootCheckBox.UseVisualStyleBackColor = true;
        MoveToLootCheckBox.CheckedChanged += MoveToLootCheckBox_CheckedChanged;
        // 
        // LootMinPrice
        // 
        LootMinPrice.Increment = new decimal(new int[] { 1000000, 0, 0, 0 });
        LootMinPrice.Location = new Point(343, 18);
        LootMinPrice.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
        LootMinPrice.Name = "LootMinPrice";
        LootMinPrice.Size = new Size(92, 23);
        LootMinPrice.TabIndex = 3;
        LootMinPrice.TextAlign = HorizontalAlignment.Center;
        LootMinPrice.Value = new decimal(new int[] { 10000, 0, 0, 0 });
        LootMinPrice.ValueChanged += LootMinPrice_ValueChanged;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(246, 21);
        label2.Name = "label2";
        label2.Size = new Size(79, 15);
        label2.TabIndex = 2;
        label2.Text = "Min. Price >=";
        // 
        // EnableLoot
        // 
        EnableLoot.AutoSize = true;
        EnableLoot.Checked = true;
        EnableLoot.CheckState = CheckState.Checked;
        EnableLoot.Location = new Point(6, 20);
        EnableLoot.Name = "EnableLoot";
        EnableLoot.Size = new Size(61, 19);
        EnableLoot.TabIndex = 0;
        EnableLoot.Text = "Enable";
        EnableLoot.UseVisualStyleBackColor = true;
        EnableLoot.CheckedChanged += EnableLoot_CheckedChanged;
        // 
        // ProtectionGroupBox
        // 
        ProtectionGroupBox.Controls.Add(MinorPercentageValue);
        ProtectionGroupBox.Controls.Add(MinorCheckBox);
        ProtectionGroupBox.Controls.Add(RegenerateWhenDie);
        ProtectionGroupBox.Controls.Add(MpPotionPercentageValue);
        ProtectionGroupBox.Controls.Add(HpPotionPercentageValue);
        ProtectionGroupBox.Controls.Add(MpPotionCheckBox);
        ProtectionGroupBox.Controls.Add(HpPotionCheckBox);
        ProtectionGroupBox.Location = new Point(6, 12);
        ProtectionGroupBox.Name = "ProtectionGroupBox";
        ProtectionGroupBox.Size = new Size(442, 101);
        ProtectionGroupBox.TabIndex = 0;
        ProtectionGroupBox.TabStop = false;
        ProtectionGroupBox.Text = "Protection";
        // 
        // MinorPercentageValue
        // 
        MinorPercentageValue.Location = new Point(89, 69);
        MinorPercentageValue.Name = "MinorPercentageValue";
        MinorPercentageValue.Size = new Size(47, 23);
        MinorPercentageValue.TabIndex = 16;
        MinorPercentageValue.TextAlign = HorizontalAlignment.Center;
        MinorPercentageValue.Value = new decimal(new int[] { 30, 0, 0, 0 });
        MinorPercentageValue.ValueChanged += MinorPercentageValue_ValueChanged;
        // 
        // MinorCheckBox
        // 
        MinorCheckBox.AutoSize = true;
        MinorCheckBox.Location = new Point(6, 71);
        MinorCheckBox.Name = "MinorCheckBox";
        MinorCheckBox.Size = new Size(58, 19);
        MinorCheckBox.TabIndex = 15;
        MinorCheckBox.Text = "Minor";
        MinorCheckBox.UseVisualStyleBackColor = true;
        MinorCheckBox.CheckedChanged += MinorCheckBox_CheckedChanged;
        // 
        // RegenerateWhenDie
        // 
        RegenerateWhenDie.AutoSize = true;
        RegenerateWhenDie.Location = new Point(247, 15);
        RegenerateWhenDie.Name = "RegenerateWhenDie";
        RegenerateWhenDie.Size = new Size(136, 19);
        RegenerateWhenDie.TabIndex = 13;
        RegenerateWhenDie.Text = "Regenerate when die";
        RegenerateWhenDie.UseVisualStyleBackColor = true;
        RegenerateWhenDie.CheckedChanged += RebornWhenDie_CheckedChanged;
        // 
        // MpPotionPercentageValue
        // 
        MpPotionPercentageValue.Location = new Point(89, 42);
        MpPotionPercentageValue.Name = "MpPotionPercentageValue";
        MpPotionPercentageValue.Size = new Size(47, 23);
        MpPotionPercentageValue.TabIndex = 3;
        MpPotionPercentageValue.TextAlign = HorizontalAlignment.Center;
        MpPotionPercentageValue.Value = new decimal(new int[] { 25, 0, 0, 0 });
        MpPotionPercentageValue.ValueChanged += MpPotionPercentageValue_ValueChanged;
        // 
        // HpPotionPercentageValue
        // 
        HpPotionPercentageValue.Location = new Point(89, 15);
        HpPotionPercentageValue.Name = "HpPotionPercentageValue";
        HpPotionPercentageValue.Size = new Size(47, 23);
        HpPotionPercentageValue.TabIndex = 2;
        HpPotionPercentageValue.TextAlign = HorizontalAlignment.Center;
        HpPotionPercentageValue.Value = new decimal(new int[] { 75, 0, 0, 0 });
        HpPotionPercentageValue.ValueChanged += HpPotionPercentageValue_ValueChanged;
        // 
        // MpPotionCheckBox
        // 
        MpPotionCheckBox.AutoSize = true;
        MpPotionCheckBox.Location = new Point(6, 44);
        MpPotionCheckBox.Name = "MpPotionCheckBox";
        MpPotionCheckBox.Size = new Size(82, 19);
        MpPotionCheckBox.TabIndex = 1;
        MpPotionCheckBox.Text = "MP Potion";
        MpPotionCheckBox.UseVisualStyleBackColor = true;
        MpPotionCheckBox.CheckedChanged += MpPotionCheckBox_CheckedChanged;
        // 
        // HpPotionCheckBox
        // 
        HpPotionCheckBox.AutoSize = true;
        HpPotionCheckBox.Location = new Point(6, 19);
        HpPotionCheckBox.Name = "HpPotionCheckBox";
        HpPotionCheckBox.Size = new Size(80, 19);
        HpPotionCheckBox.TabIndex = 0;
        HpPotionCheckBox.Text = "HP Potion";
        HpPotionCheckBox.UseVisualStyleBackColor = true;
        HpPotionCheckBox.CheckedChanged += HpPotionCheckBox_CheckedChanged;
        // 
        // AttackPage
        // 
        AttackPage.Controls.Add(groupBox4);
        AttackPage.Controls.Add(groupBox3);
        AttackPage.Controls.Add(groupBox2);
        AttackPage.Controls.Add(SkillGroupBox);
        AttackPage.Location = new Point(4, 24);
        AttackPage.Name = "AttackPage";
        AttackPage.Padding = new Padding(3);
        AttackPage.Size = new Size(450, 776);
        AttackPage.TabIndex = 1;
        AttackPage.Text = "Attack";
        AttackPage.UseVisualStyleBackColor = true;
        // 
        // groupBox4
        // 
        groupBox4.Controls.Add(TargetSearchRange);
        groupBox4.Controls.Add(label9);
        groupBox4.Controls.Add(DisableSkillCasting);
        groupBox4.Controls.Add(label5);
        groupBox4.Controls.Add(AttackSpeed);
        groupBox4.Controls.Add(FollowTargetSync);
        groupBox4.Controls.Add(MoveToTarget);
        groupBox4.Controls.Add(AttackRange);
        groupBox4.Controls.Add(label1);
        groupBox4.Location = new Point(6, 260);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new Size(442, 103);
        groupBox4.TabIndex = 4;
        groupBox4.TabStop = false;
        groupBox4.Text = "Settings";
        // 
        // TargetSearchRange
        // 
        TargetSearchRange.Increment = new decimal(new int[] { 5, 0, 0, 0 });
        TargetSearchRange.Location = new Point(381, 70);
        TargetSearchRange.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        TargetSearchRange.Name = "TargetSearchRange";
        TargetSearchRange.Size = new Size(54, 23);
        TargetSearchRange.TabIndex = 11;
        TargetSearchRange.TextAlign = HorizontalAlignment.Center;
        TargetSearchRange.Value = new decimal(new int[] { 45, 0, 0, 0 });
        TargetSearchRange.ValueChanged += TargetSearchRange_ValueChanged;
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Location = new Point(260, 73);
        label9.Name = "label9";
        label9.Size = new Size(115, 15);
        label9.TabIndex = 10;
        label9.Text = "Target search range :";
        // 
        // DisableSkillCasting
        // 
        DisableSkillCasting.AutoSize = true;
        DisableSkillCasting.ForeColor = Color.Red;
        DisableSkillCasting.Location = new Point(6, 75);
        DisableSkillCasting.Name = "DisableSkillCasting";
        DisableSkillCasting.Size = new Size(128, 19);
        DisableSkillCasting.TabIndex = 9;
        DisableSkillCasting.Text = "Disable skill casting";
        DisableSkillCasting.UseVisualStyleBackColor = true;
        DisableSkillCasting.CheckedChanged += DisableSkillCasting_CheckedChanged;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(294, 48);
        label5.Name = "label5";
        label5.Size = new Size(81, 15);
        label5.TabIndex = 8;
        label5.Text = "Attack speed :";
        // 
        // AttackSpeed
        // 
        AttackSpeed.Increment = new decimal(new int[] { 50, 0, 0, 0 });
        AttackSpeed.Location = new Point(381, 45);
        AttackSpeed.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        AttackSpeed.Name = "AttackSpeed";
        AttackSpeed.Size = new Size(54, 23);
        AttackSpeed.TabIndex = 7;
        AttackSpeed.TextAlign = HorizontalAlignment.Center;
        AttackSpeed.Value = new decimal(new int[] { 1000, 0, 0, 0 });
        AttackSpeed.ValueChanged += AttackSpeed_ValueChanged;
        // 
        // FollowTargetSync
        // 
        FollowTargetSync.AutoSize = true;
        FollowTargetSync.Checked = true;
        FollowTargetSync.CheckState = CheckState.Checked;
        FollowTargetSync.Location = new Point(6, 49);
        FollowTargetSync.Name = "FollowTargetSync";
        FollowTargetSync.Size = new Size(167, 19);
        FollowTargetSync.TabIndex = 6;
        FollowTargetSync.Text = "Followed client target sync";
        FollowTargetSync.UseVisualStyleBackColor = true;
        FollowTargetSync.CheckedChanged += FollowTargetSync_CheckedChanged;
        // 
        // MoveToTarget
        // 
        MoveToTarget.AutoSize = true;
        MoveToTarget.Location = new Point(6, 24);
        MoveToTarget.Name = "MoveToTarget";
        MoveToTarget.Size = new Size(104, 19);
        MoveToTarget.TabIndex = 5;
        MoveToTarget.Text = "Move to target";
        MoveToTarget.UseVisualStyleBackColor = true;
        MoveToTarget.CheckedChanged += MoveToTarget_CheckedChanged;
        // 
        // AttackRange
        // 
        AttackRange.Increment = new decimal(new int[] { 5, 0, 0, 0 });
        AttackRange.Location = new Point(381, 20);
        AttackRange.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        AttackRange.Name = "AttackRange";
        AttackRange.Size = new Size(54, 23);
        AttackRange.TabIndex = 3;
        AttackRange.TextAlign = HorizontalAlignment.Center;
        AttackRange.Value = new decimal(new int[] { 45, 0, 0, 0 });
        AttackRange.ValueChanged += AttackRange_ValueChanged;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(295, 24);
        label1.Name = "label1";
        label1.Size = new Size(80, 15);
        label1.TabIndex = 2;
        label1.Text = "Attack range :";
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(TargetListClearButton);
        groupBox3.Controls.Add(SearchTargetButton);
        groupBox3.Controls.Add(TargetCheckedListBox);
        groupBox3.Location = new Point(6, 369);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(442, 224);
        groupBox3.TabIndex = 3;
        groupBox3.TabStop = false;
        groupBox3.Text = "Target";
        // 
        // TargetListClearButton
        // 
        TargetListClearButton.Location = new Point(322, 195);
        TargetListClearButton.Name = "TargetListClearButton";
        TargetListClearButton.Size = new Size(113, 23);
        TargetListClearButton.TabIndex = 5;
        TargetListClearButton.Text = "Clear";
        TargetListClearButton.UseVisualStyleBackColor = true;
        // 
        // SearchTargetButton
        // 
        SearchTargetButton.Location = new Point(322, 22);
        SearchTargetButton.Name = "SearchTargetButton";
        SearchTargetButton.Size = new Size(113, 23);
        SearchTargetButton.TabIndex = 1;
        SearchTargetButton.Text = "Search";
        SearchTargetButton.UseVisualStyleBackColor = true;
        SearchTargetButton.Click += SearchTargetButton_Click;
        // 
        // TargetCheckedListBox
        // 
        TargetCheckedListBox.FormattingEnabled = true;
        TargetCheckedListBox.Location = new Point(6, 16);
        TargetCheckedListBox.Name = "TargetCheckedListBox";
        TargetCheckedListBox.Size = new Size(165, 202);
        TargetCheckedListBox.TabIndex = 0;
        TargetCheckedListBox.ItemCheck += TargetCheckedListBox_ItemCheck;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(SelfSkillCheckedListBox);
        groupBox2.Location = new Point(219, 12);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(222, 249);
        groupBox2.TabIndex = 2;
        groupBox2.TabStop = false;
        groupBox2.Text = "Self";
        // 
        // SelfSkillCheckedListBox
        // 
        SelfSkillCheckedListBox.FormattingEnabled = true;
        SelfSkillCheckedListBox.Location = new Point(6, 22);
        SelfSkillCheckedListBox.Name = "SelfSkillCheckedListBox";
        SelfSkillCheckedListBox.Size = new Size(210, 220);
        SelfSkillCheckedListBox.TabIndex = 0;
        SelfSkillCheckedListBox.ItemCheck += SelfSkillCheckedListBox_ItemCheck;
        // 
        // SkillGroupBox
        // 
        SkillGroupBox.Controls.Add(AttackSkillCheckedListBox);
        SkillGroupBox.Location = new Point(5, 12);
        SkillGroupBox.Name = "SkillGroupBox";
        SkillGroupBox.Size = new Size(208, 249);
        SkillGroupBox.TabIndex = 1;
        SkillGroupBox.TabStop = false;
        SkillGroupBox.Text = "Skill";
        // 
        // AttackSkillCheckedListBox
        // 
        AttackSkillCheckedListBox.FormattingEnabled = true;
        AttackSkillCheckedListBox.Location = new Point(6, 22);
        AttackSkillCheckedListBox.Name = "AttackSkillCheckedListBox";
        AttackSkillCheckedListBox.Size = new Size(196, 220);
        AttackSkillCheckedListBox.TabIndex = 0;
        AttackSkillCheckedListBox.ItemCheck += AttackSkillCheckedListBox_ItemCheck;
        // 
        // PartyPage
        // 
        PartyPage.Controls.Add(groupBox9);
        PartyPage.Controls.Add(PartyListGroupBox);
        PartyPage.Location = new Point(4, 24);
        PartyPage.Name = "PartyPage";
        PartyPage.Size = new Size(450, 776);
        PartyPage.TabIndex = 2;
        PartyPage.Text = "Party";
        PartyPage.UseVisualStyleBackColor = true;
        // 
        // groupBox9
        // 
        groupBox9.Controls.Add(SendPartyPlayerNametextBox);
        groupBox9.Controls.Add(SendPartyButton);
        groupBox9.Controls.Add(NearbyPlayerListDataGrid);
        groupBox9.Location = new Point(5, 391);
        groupBox9.Name = "groupBox9";
        groupBox9.Size = new Size(443, 245);
        groupBox9.TabIndex = 16;
        groupBox9.TabStop = false;
        groupBox9.Text = "Nearby Players";
        // 
        // SendPartyPlayerNametextBox
        // 
        SendPartyPlayerNametextBox.Location = new Point(100, 22);
        SendPartyPlayerNametextBox.Name = "SendPartyPlayerNametextBox";
        SendPartyPlayerNametextBox.PlaceholderText = "Character Name";
        SendPartyPlayerNametextBox.Size = new Size(140, 23);
        SendPartyPlayerNametextBox.TabIndex = 4;
        SendPartyPlayerNametextBox.TextAlign = HorizontalAlignment.Center;
        // 
        // SendPartyButton
        // 
        SendPartyButton.Location = new Point(6, 22);
        SendPartyButton.Name = "SendPartyButton";
        SendPartyButton.Size = new Size(81, 23);
        SendPartyButton.TabIndex = 3;
        SendPartyButton.Text = "Send Party";
        SendPartyButton.UseVisualStyleBackColor = true;
        SendPartyButton.Click += SendPartyButton_Click;
        // 
        // NearbyPlayerListDataGrid
        // 
        NearbyPlayerListDataGrid.AllowUserToAddRows = false;
        NearbyPlayerListDataGrid.AllowUserToDeleteRows = false;
        NearbyPlayerListDataGrid.AllowUserToResizeRows = false;
        NearbyPlayerListDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        NearbyPlayerListDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        NearbyPlayerListDataGrid.BackgroundColor = Color.White;
        NearbyPlayerListDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        NearbyPlayerListDataGrid.Location = new Point(6, 51);
        NearbyPlayerListDataGrid.Name = "NearbyPlayerListDataGrid";
        NearbyPlayerListDataGrid.ReadOnly = true;
        NearbyPlayerListDataGrid.RowTemplate.Height = 25;
        NearbyPlayerListDataGrid.Size = new Size(431, 186);
        NearbyPlayerListDataGrid.TabIndex = 1;
        NearbyPlayerListDataGrid.Click += NearbyPlayerListDataGrid_Click;
        // 
        // PartyListGroupBox
        // 
        PartyListGroupBox.Controls.Add(groupBox10);
        PartyListGroupBox.Controls.Add(PartyMakeLeaderButton);
        PartyListGroupBox.Controls.Add(groupBox8);
        PartyListGroupBox.Controls.Add(PartyListDataGrid);
        PartyListGroupBox.Location = new Point(5, 12);
        PartyListGroupBox.Name = "PartyListGroupBox";
        PartyListGroupBox.Size = new Size(443, 374);
        PartyListGroupBox.TabIndex = 13;
        PartyListGroupBox.TabStop = false;
        PartyListGroupBox.Text = "Party List";
        // 
        // groupBox10
        // 
        groupBox10.Controls.Add(PartyAddPrefixtextBox);
        groupBox10.Controls.Add(AutoPartycheckBox);
        groupBox10.Controls.Add(SummonButton);
        groupBox10.Controls.Add(SwiftPartyMemberCheckBox);
        groupBox10.Location = new Point(6, 243);
        groupBox10.Name = "groupBox10";
        groupBox10.Size = new Size(431, 52);
        groupBox10.TabIndex = 17;
        groupBox10.TabStop = false;
        groupBox10.Text = "Settings";
        // 
        // PartyAddPrefixtextBox
        // 
        PartyAddPrefixtextBox.Location = new Point(186, 18);
        PartyAddPrefixtextBox.Name = "PartyAddPrefixtextBox";
        PartyAddPrefixtextBox.PlaceholderText = "party prefix";
        PartyAddPrefixtextBox.Size = new Size(79, 23);
        PartyAddPrefixtextBox.TabIndex = 19;
        PartyAddPrefixtextBox.TextAlign = HorizontalAlignment.Center;
        PartyAddPrefixtextBox.TextChanged += PartyAddPrefixtextBox_TextChanged;
        // 
        // AutoPartycheckBox
        // 
        AutoPartycheckBox.AutoSize = true;
        AutoPartycheckBox.Location = new Point(96, 22);
        AutoPartycheckBox.Name = "AutoPartycheckBox";
        AutoPartycheckBox.Size = new Size(80, 19);
        AutoPartycheckBox.TabIndex = 18;
        AutoPartycheckBox.Text = "auto party";
        AutoPartycheckBox.UseVisualStyleBackColor = true;
        AutoPartycheckBox.CheckedChanged += AutoPartycheckBox_CheckedChanged;
        // 
        // SummonButton
        // 
        SummonButton.Location = new Point(287, 18);
        SummonButton.Name = "SummonButton";
        SummonButton.Size = new Size(138, 23);
        SummonButton.TabIndex = 17;
        SummonButton.Text = "Summon ( only mage )";
        SummonButton.UseVisualStyleBackColor = true;
        SummonButton.Click += SummonButton_Click;
        // 
        // SwiftPartyMemberCheckBox
        // 
        SwiftPartyMemberCheckBox.AutoSize = true;
        SwiftPartyMemberCheckBox.Checked = true;
        SwiftPartyMemberCheckBox.CheckState = CheckState.Checked;
        SwiftPartyMemberCheckBox.Location = new Point(6, 22);
        SwiftPartyMemberCheckBox.Name = "SwiftPartyMemberCheckBox";
        SwiftPartyMemberCheckBox.Size = new Size(82, 19);
        SwiftPartyMemberCheckBox.TabIndex = 16;
        SwiftPartyMemberCheckBox.Text = "Swift party";
        SwiftPartyMemberCheckBox.UseVisualStyleBackColor = true;
        SwiftPartyMemberCheckBox.CheckedChanged += SwiftPartyMemberCheckBox_CheckedChanged;
        // 
        // PartyMakeLeaderButton
        // 
        PartyMakeLeaderButton.Location = new Point(6, 22);
        PartyMakeLeaderButton.Name = "PartyMakeLeaderButton";
        PartyMakeLeaderButton.Size = new Size(88, 23);
        PartyMakeLeaderButton.TabIndex = 15;
        PartyMakeLeaderButton.Text = "Make Leader";
        PartyMakeLeaderButton.UseVisualStyleBackColor = true;
        PartyMakeLeaderButton.Click += PartyMakeLeaderButton_Click;
        // 
        // groupBox8
        // 
        groupBox8.Controls.Add(PartyKickButton);
        groupBox8.Controls.Add(PartyDisbandButton);
        groupBox8.Controls.Add(PartyDeclineButton);
        groupBox8.Controls.Add(PartyAcceptButton);
        groupBox8.Location = new Point(6, 301);
        groupBox8.Name = "groupBox8";
        groupBox8.Size = new Size(431, 65);
        groupBox8.TabIndex = 3;
        groupBox8.TabStop = false;
        groupBox8.Text = "Action";
        // 
        // PartyKickButton
        // 
        PartyKickButton.Location = new Point(183, 29);
        PartyKickButton.Name = "PartyKickButton";
        PartyKickButton.Size = new Size(82, 23);
        PartyKickButton.TabIndex = 15;
        PartyKickButton.Text = "Kick";
        PartyKickButton.UseVisualStyleBackColor = true;
        PartyKickButton.Click += PartyKickButton_Click;
        // 
        // PartyDisbandButton
        // 
        PartyDisbandButton.Location = new Point(271, 29);
        PartyDisbandButton.Name = "PartyDisbandButton";
        PartyDisbandButton.Size = new Size(82, 23);
        PartyDisbandButton.TabIndex = 14;
        PartyDisbandButton.Text = "Disband";
        PartyDisbandButton.UseVisualStyleBackColor = true;
        PartyDisbandButton.Click += PartyDisbandButton_Click;
        // 
        // PartyDeclineButton
        // 
        PartyDeclineButton.Location = new Point(94, 29);
        PartyDeclineButton.Name = "PartyDeclineButton";
        PartyDeclineButton.Size = new Size(82, 23);
        PartyDeclineButton.TabIndex = 1;
        PartyDeclineButton.Text = "Decline";
        PartyDeclineButton.UseVisualStyleBackColor = true;
        PartyDeclineButton.Click += PartyDeclineButton_Click;
        // 
        // PartyAcceptButton
        // 
        PartyAcceptButton.Location = new Point(6, 29);
        PartyAcceptButton.Name = "PartyAcceptButton";
        PartyAcceptButton.Size = new Size(82, 23);
        PartyAcceptButton.TabIndex = 0;
        PartyAcceptButton.Text = "Accept";
        PartyAcceptButton.UseVisualStyleBackColor = true;
        PartyAcceptButton.Click += PartyAcceptButton_Click;
        // 
        // PartyListDataGrid
        // 
        PartyListDataGrid.AllowUserToAddRows = false;
        PartyListDataGrid.AllowUserToDeleteRows = false;
        PartyListDataGrid.AllowUserToResizeRows = false;
        PartyListDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        PartyListDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        PartyListDataGrid.BackgroundColor = Color.White;
        PartyListDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        PartyListDataGrid.Location = new Point(6, 51);
        PartyListDataGrid.Name = "PartyListDataGrid";
        PartyListDataGrid.ReadOnly = true;
        PartyListDataGrid.RowTemplate.Height = 25;
        PartyListDataGrid.Size = new Size(431, 186);
        PartyListDataGrid.TabIndex = 0;
        // 
        // ActionPage
        // 
        ActionPage.Controls.Add(groupBox18);
        ActionPage.Controls.Add(groupBox14);
        ActionPage.Controls.Add(groupBox12);
        ActionPage.Location = new Point(4, 24);
        ActionPage.Name = "ActionPage";
        ActionPage.Size = new Size(450, 776);
        ActionPage.TabIndex = 3;
        ActionPage.Text = "Action";
        ActionPage.UseVisualStyleBackColor = true;
        // 
        // groupBox18
        // 
        groupBox18.Controls.Add(ItemListButton);
        groupBox18.Controls.Add(GoToNpcShopButton);
        groupBox18.Controls.Add(NpcShopDataList);
        groupBox18.Location = new Point(5, 547);
        groupBox18.Name = "groupBox18";
        groupBox18.Size = new Size(438, 223);
        groupBox18.TabIndex = 14;
        groupBox18.TabStop = false;
        groupBox18.Text = "Shop";
        // 
        // ItemListButton
        // 
        ItemListButton.Location = new Point(137, 186);
        ItemListButton.Name = "ItemListButton";
        ItemListButton.Size = new Size(127, 23);
        ItemListButton.TabIndex = 4;
        ItemListButton.Text = "Item list";
        ItemListButton.UseVisualStyleBackColor = true;
        ItemListButton.Click += ItemListButton_Click;
        // 
        // GoToNpcShopButton
        // 
        GoToNpcShopButton.Location = new Point(13, 186);
        GoToNpcShopButton.Name = "GoToNpcShopButton";
        GoToNpcShopButton.Size = new Size(118, 23);
        GoToNpcShopButton.TabIndex = 3;
        GoToNpcShopButton.Text = "Go to npc";
        GoToNpcShopButton.UseVisualStyleBackColor = true;
        GoToNpcShopButton.Click += GoToNpcShopButton_Click;
        // 
        // NpcShopDataList
        // 
        NpcShopDataList.AllowUserToAddRows = false;
        NpcShopDataList.AllowUserToDeleteRows = false;
        NpcShopDataList.AllowUserToResizeRows = false;
        NpcShopDataList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        NpcShopDataList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        NpcShopDataList.BackgroundColor = Color.White;
        NpcShopDataList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        NpcShopDataList.Location = new Point(13, 19);
        NpcShopDataList.Name = "NpcShopDataList";
        NpcShopDataList.ReadOnly = true;
        NpcShopDataList.RowTemplate.Height = 25;
        NpcShopDataList.Size = new Size(411, 161);
        NpcShopDataList.TabIndex = 1;
        // 
        // groupBox14
        // 
        groupBox14.Controls.Add(tabControl2);
        groupBox14.Location = new Point(5, 12);
        groupBox14.Name = "groupBox14";
        groupBox14.Size = new Size(438, 283);
        groupBox14.TabIndex = 13;
        groupBox14.TabStop = false;
        groupBox14.Text = "Quest Management";
        // 
        // tabControl2
        // 
        tabControl2.Controls.Add(NpcListTab);
        tabControl2.Controls.Add(QuestListTab);
        tabControl2.Controls.Add(ActiveQuestListTab);
        tabControl2.Controls.Add(CompletedTabPage);
        tabControl2.Controls.Add(FinishedQuestListTab);
        tabControl2.Location = new Point(6, 19);
        tabControl2.Name = "tabControl2";
        tabControl2.SelectedIndex = 0;
        tabControl2.Size = new Size(428, 259);
        tabControl2.TabIndex = 14;
        // 
        // NpcListTab
        // 
        NpcListTab.Controls.Add(GoToNpcButton);
        NpcListTab.Controls.Add(QuestNpcList);
        NpcListTab.Controls.Add(LoadNpcListButton);
        NpcListTab.Location = new Point(4, 24);
        NpcListTab.Name = "NpcListTab";
        NpcListTab.Padding = new Padding(3);
        NpcListTab.Size = new Size(420, 231);
        NpcListTab.TabIndex = 0;
        NpcListTab.Text = "NPC";
        NpcListTab.UseVisualStyleBackColor = true;
        // 
        // GoToNpcButton
        // 
        GoToNpcButton.Location = new Point(3, 203);
        GoToNpcButton.Name = "GoToNpcButton";
        GoToNpcButton.Size = new Size(118, 23);
        GoToNpcButton.TabIndex = 2;
        GoToNpcButton.Text = "Go to npc";
        GoToNpcButton.UseVisualStyleBackColor = true;
        GoToNpcButton.Click += GoToNpcButton_Click;
        // 
        // QuestNpcList
        // 
        QuestNpcList.AllowUserToAddRows = false;
        QuestNpcList.AllowUserToDeleteRows = false;
        QuestNpcList.AllowUserToResizeRows = false;
        QuestNpcList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        QuestNpcList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        QuestNpcList.BackgroundColor = Color.White;
        QuestNpcList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        QuestNpcList.Location = new Point(3, 36);
        QuestNpcList.Name = "QuestNpcList";
        QuestNpcList.ReadOnly = true;
        QuestNpcList.RowTemplate.Height = 25;
        QuestNpcList.Size = new Size(411, 161);
        QuestNpcList.TabIndex = 0;
        // 
        // LoadNpcListButton
        // 
        LoadNpcListButton.Location = new Point(6, 6);
        LoadNpcListButton.Name = "LoadNpcListButton";
        LoadNpcListButton.Size = new Size(118, 23);
        LoadNpcListButton.TabIndex = 1;
        LoadNpcListButton.Text = "Refresh";
        LoadNpcListButton.UseVisualStyleBackColor = true;
        LoadNpcListButton.Click += LoadNpcListButton_Click;
        // 
        // QuestListTab
        // 
        QuestListTab.Controls.Add(TakeQuestButton);
        QuestListTab.Controls.Add(QuestListViewDataGrid);
        QuestListTab.Location = new Point(4, 24);
        QuestListTab.Name = "QuestListTab";
        QuestListTab.Padding = new Padding(3);
        QuestListTab.Size = new Size(420, 231);
        QuestListTab.TabIndex = 1;
        QuestListTab.Text = "Quests (0)";
        QuestListTab.UseVisualStyleBackColor = true;
        // 
        // TakeQuestButton
        // 
        TakeQuestButton.Location = new Point(6, 202);
        TakeQuestButton.Name = "TakeQuestButton";
        TakeQuestButton.Size = new Size(118, 23);
        TakeQuestButton.TabIndex = 4;
        TakeQuestButton.Text = "Take Quest";
        TakeQuestButton.UseVisualStyleBackColor = true;
        TakeQuestButton.Click += TakeQuestButton_Click;
        // 
        // QuestListViewDataGrid
        // 
        QuestListViewDataGrid.AllowUserToAddRows = false;
        QuestListViewDataGrid.AllowUserToDeleteRows = false;
        QuestListViewDataGrid.AllowUserToResizeColumns = false;
        QuestListViewDataGrid.AllowUserToResizeRows = false;
        QuestListViewDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        QuestListViewDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        QuestListViewDataGrid.BackgroundColor = Color.White;
        QuestListViewDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        QuestListViewDataGrid.Location = new Point(6, 7);
        QuestListViewDataGrid.Name = "QuestListViewDataGrid";
        QuestListViewDataGrid.RowTemplate.Height = 25;
        QuestListViewDataGrid.Size = new Size(411, 189);
        QuestListViewDataGrid.TabIndex = 1;
        // 
        // ActiveQuestListTab
        // 
        ActiveQuestListTab.Controls.Add(RemoveRunningQuestButton);
        ActiveQuestListTab.Controls.Add(RunningQuestListDataGrid);
        ActiveQuestListTab.Location = new Point(4, 24);
        ActiveQuestListTab.Name = "ActiveQuestListTab";
        ActiveQuestListTab.Size = new Size(420, 231);
        ActiveQuestListTab.TabIndex = 2;
        ActiveQuestListTab.Text = "Running (0)";
        ActiveQuestListTab.UseVisualStyleBackColor = true;
        // 
        // RemoveRunningQuestButton
        // 
        RemoveRunningQuestButton.Location = new Point(6, 205);
        RemoveRunningQuestButton.Name = "RemoveRunningQuestButton";
        RemoveRunningQuestButton.Size = new Size(118, 23);
        RemoveRunningQuestButton.TabIndex = 5;
        RemoveRunningQuestButton.Text = "Remove Quest";
        RemoveRunningQuestButton.UseVisualStyleBackColor = true;
        RemoveRunningQuestButton.Click += RemoveQuestButton_Click;
        // 
        // RunningQuestListDataGrid
        // 
        RunningQuestListDataGrid.AllowUserToAddRows = false;
        RunningQuestListDataGrid.AllowUserToDeleteRows = false;
        RunningQuestListDataGrid.AllowUserToResizeColumns = false;
        RunningQuestListDataGrid.AllowUserToResizeRows = false;
        RunningQuestListDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        RunningQuestListDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        RunningQuestListDataGrid.BackgroundColor = Color.White;
        RunningQuestListDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        RunningQuestListDataGrid.Location = new Point(6, 7);
        RunningQuestListDataGrid.Name = "RunningQuestListDataGrid";
        RunningQuestListDataGrid.RowTemplate.Height = 25;
        RunningQuestListDataGrid.Size = new Size(411, 192);
        RunningQuestListDataGrid.TabIndex = 2;
        // 
        // CompletedTabPage
        // 
        CompletedTabPage.Controls.Add(GiveQuestButton);
        CompletedTabPage.Controls.Add(RemoveCompletedQuestButton);
        CompletedTabPage.Controls.Add(CompletedQuestListDataGrid);
        CompletedTabPage.Location = new Point(4, 24);
        CompletedTabPage.Name = "CompletedTabPage";
        CompletedTabPage.Size = new Size(420, 231);
        CompletedTabPage.TabIndex = 4;
        CompletedTabPage.Text = "Completed (0)";
        CompletedTabPage.UseVisualStyleBackColor = true;
        // 
        // GiveQuestButton
        // 
        GiveQuestButton.Location = new Point(6, 205);
        GiveQuestButton.Name = "GiveQuestButton";
        GiveQuestButton.Size = new Size(118, 23);
        GiveQuestButton.TabIndex = 7;
        GiveQuestButton.Text = "Give Quest";
        GiveQuestButton.UseVisualStyleBackColor = true;
        GiveQuestButton.Click += GiveQuestButton_Click;
        // 
        // RemoveCompletedQuestButton
        // 
        RemoveCompletedQuestButton.Location = new Point(299, 205);
        RemoveCompletedQuestButton.Name = "RemoveCompletedQuestButton";
        RemoveCompletedQuestButton.Size = new Size(118, 23);
        RemoveCompletedQuestButton.TabIndex = 6;
        RemoveCompletedQuestButton.Text = "Remove Quest";
        RemoveCompletedQuestButton.UseVisualStyleBackColor = true;
        RemoveCompletedQuestButton.Click += RemoveCompletedQuestButton_Click;
        // 
        // CompletedQuestListDataGrid
        // 
        CompletedQuestListDataGrid.AllowUserToAddRows = false;
        CompletedQuestListDataGrid.AllowUserToDeleteRows = false;
        CompletedQuestListDataGrid.AllowUserToResizeColumns = false;
        CompletedQuestListDataGrid.AllowUserToResizeRows = false;
        CompletedQuestListDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        CompletedQuestListDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        CompletedQuestListDataGrid.BackgroundColor = Color.White;
        CompletedQuestListDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        CompletedQuestListDataGrid.Location = new Point(6, 7);
        CompletedQuestListDataGrid.Name = "CompletedQuestListDataGrid";
        CompletedQuestListDataGrid.RowTemplate.Height = 25;
        CompletedQuestListDataGrid.Size = new Size(411, 192);
        CompletedQuestListDataGrid.TabIndex = 3;
        // 
        // FinishedQuestListTab
        // 
        FinishedQuestListTab.Controls.Add(FinishedQuestListDataGrid);
        FinishedQuestListTab.Location = new Point(4, 24);
        FinishedQuestListTab.Name = "FinishedQuestListTab";
        FinishedQuestListTab.Size = new Size(420, 231);
        FinishedQuestListTab.TabIndex = 3;
        FinishedQuestListTab.Text = "Finished (0)";
        FinishedQuestListTab.UseVisualStyleBackColor = true;
        // 
        // FinishedQuestListDataGrid
        // 
        FinishedQuestListDataGrid.AllowUserToAddRows = false;
        FinishedQuestListDataGrid.AllowUserToDeleteRows = false;
        FinishedQuestListDataGrid.AllowUserToResizeColumns = false;
        FinishedQuestListDataGrid.AllowUserToResizeRows = false;
        FinishedQuestListDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        FinishedQuestListDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        FinishedQuestListDataGrid.BackgroundColor = Color.White;
        FinishedQuestListDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        FinishedQuestListDataGrid.Location = new Point(3, 3);
        FinishedQuestListDataGrid.Name = "FinishedQuestListDataGrid";
        FinishedQuestListDataGrid.RowTemplate.Height = 25;
        FinishedQuestListDataGrid.Size = new Size(411, 225);
        FinishedQuestListDataGrid.TabIndex = 3;
        // 
        // groupBox12
        // 
        groupBox12.Controls.Add(GoToNearestGateButton);
        groupBox12.Controls.Add(GateTeleportButton);
        groupBox12.Controls.Add(GateListDataGrid);
        groupBox12.Location = new Point(5, 310);
        groupBox12.Name = "groupBox12";
        groupBox12.Size = new Size(438, 222);
        groupBox12.TabIndex = 1;
        groupBox12.TabStop = false;
        groupBox12.Text = "Gate Management";
        // 
        // GoToNearestGateButton
        // 
        GoToNearestGateButton.Location = new Point(13, 189);
        GoToNearestGateButton.Name = "GoToNearestGateButton";
        GoToNearestGateButton.Size = new Size(127, 23);
        GoToNearestGateButton.TabIndex = 4;
        GoToNearestGateButton.Text = "Go to nearest gate";
        GoToNearestGateButton.UseVisualStyleBackColor = true;
        GoToNearestGateButton.Click += GoToNearestGateButton_Click;
        // 
        // GateTeleportButton
        // 
        GateTeleportButton.Location = new Point(146, 189);
        GateTeleportButton.Name = "GateTeleportButton";
        GateTeleportButton.Size = new Size(127, 23);
        GateTeleportButton.TabIndex = 3;
        GateTeleportButton.Text = "Teleport";
        GateTeleportButton.UseVisualStyleBackColor = true;
        GateTeleportButton.Click += GateTeleportButton_Click;
        // 
        // GateListDataGrid
        // 
        GateListDataGrid.AllowUserToAddRows = false;
        GateListDataGrid.AllowUserToDeleteRows = false;
        GateListDataGrid.AllowUserToResizeRows = false;
        GateListDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        GateListDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        GateListDataGrid.BackgroundColor = Color.White;
        GateListDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        GateListDataGrid.Location = new Point(13, 22);
        GateListDataGrid.Name = "GateListDataGrid";
        GateListDataGrid.ReadOnly = true;
        GateListDataGrid.RowTemplate.Height = 25;
        GateListDataGrid.Size = new Size(411, 161);
        GateListDataGrid.TabIndex = 1;
        // 
        // ToolPage
        // 
        ToolPage.Controls.Add(btnSpawnCreature);
        ToolPage.Controls.Add(groupBox16);
        ToolPage.Controls.Add(groupBox17);
        ToolPage.Location = new Point(4, 24);
        ToolPage.Name = "ToolPage";
        ToolPage.Size = new Size(450, 776);
        ToolPage.TabIndex = 4;
        ToolPage.Text = "Tool";
        ToolPage.UseVisualStyleBackColor = true;
        // 
        // btnSpawnCreature
        // 
        btnSpawnCreature.Location = new Point(8, 312);
        btnSpawnCreature.Name = "btnSpawnCreature";
        btnSpawnCreature.Size = new Size(174, 23);
        btnSpawnCreature.TabIndex = 15;
        btnSpawnCreature.Text = "spawn creature initialize";
        btnSpawnCreature.UseVisualStyleBackColor = true;
        btnSpawnCreature.Click += btnSpawnCreature_Click;
        // 
        // groupBox16
        // 
        groupBox16.Controls.Add(GetCurrentCoordinate);
        groupBox16.Controls.Add(MoveCoordinateY);
        groupBox16.Controls.Add(label7);
        groupBox16.Controls.Add(label6);
        groupBox16.Controls.Add(MoveCoordinateX);
        groupBox16.Controls.Add(MoveCoordinateWithRoute);
        groupBox16.Controls.Add(MoveCoordinateDirect);
        groupBox16.Location = new Point(5, 12);
        groupBox16.Name = "groupBox16";
        groupBox16.Size = new Size(438, 88);
        groupBox16.TabIndex = 14;
        groupBox16.TabStop = false;
        groupBox16.Text = "Move Management";
        // 
        // GetCurrentCoordinate
        // 
        GetCurrentCoordinate.Location = new Point(214, 21);
        GetCurrentCoordinate.Name = "GetCurrentCoordinate";
        GetCurrentCoordinate.Size = new Size(157, 23);
        GetCurrentCoordinate.TabIndex = 6;
        GetCurrentCoordinate.Text = "Get current coordinate";
        GetCurrentCoordinate.UseVisualStyleBackColor = true;
        GetCurrentCoordinate.Click += GetCurrentCoordinate_Click;
        // 
        // MoveCoordinateY
        // 
        MoveCoordinateY.Location = new Point(36, 53);
        MoveCoordinateY.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        MoveCoordinateY.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
        MoveCoordinateY.Name = "MoveCoordinateY";
        MoveCoordinateY.Size = new Size(107, 23);
        MoveCoordinateY.TabIndex = 5;
        MoveCoordinateY.TextAlign = HorizontalAlignment.Center;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(10, 58);
        label7.Name = "label7";
        label7.Size = new Size(20, 15);
        label7.TabIndex = 4;
        label7.Text = "Y :";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(10, 29);
        label6.Name = "label6";
        label6.Size = new Size(20, 15);
        label6.TabIndex = 3;
        label6.Text = "X :";
        // 
        // MoveCoordinateX
        // 
        MoveCoordinateX.Location = new Point(36, 26);
        MoveCoordinateX.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        MoveCoordinateX.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
        MoveCoordinateX.Name = "MoveCoordinateX";
        MoveCoordinateX.Size = new Size(107, 23);
        MoveCoordinateX.TabIndex = 2;
        MoveCoordinateX.TextAlign = HorizontalAlignment.Center;
        // 
        // MoveCoordinateWithRoute
        // 
        MoveCoordinateWithRoute.Location = new Point(301, 54);
        MoveCoordinateWithRoute.Name = "MoveCoordinateWithRoute";
        MoveCoordinateWithRoute.Size = new Size(127, 23);
        MoveCoordinateWithRoute.TabIndex = 1;
        MoveCoordinateWithRoute.Text = "Move with route";
        MoveCoordinateWithRoute.UseVisualStyleBackColor = true;
        MoveCoordinateWithRoute.Click += MoveCoordinateWithRoute_Click;
        // 
        // MoveCoordinateDirect
        // 
        MoveCoordinateDirect.Location = new Point(159, 54);
        MoveCoordinateDirect.Name = "MoveCoordinateDirect";
        MoveCoordinateDirect.Size = new Size(127, 23);
        MoveCoordinateDirect.TabIndex = 0;
        MoveCoordinateDirect.Text = "Move direct";
        MoveCoordinateDirect.UseVisualStyleBackColor = true;
        MoveCoordinateDirect.Click += MoveCoordinateDirect_Click;
        // 
        // groupBox17
        // 
        groupBox17.Controls.Add(SendPacketStop);
        groupBox17.Controls.Add(SendPacketRepeatCount);
        groupBox17.Controls.Add(label10);
        groupBox17.Controls.Add(SendPacketDelay);
        groupBox17.Controls.Add(label8);
        groupBox17.Controls.Add(SendPacket);
        groupBox17.Controls.Add(PacketTextBox);
        groupBox17.Location = new Point(5, 106);
        groupBox17.Name = "groupBox17";
        groupBox17.Size = new Size(438, 200);
        groupBox17.TabIndex = 0;
        groupBox17.TabStop = false;
        groupBox17.Text = "Packet Sender";
        // 
        // SendPacketStop
        // 
        SendPacketStop.Location = new Point(96, 167);
        SendPacketStop.Name = "SendPacketStop";
        SendPacketStop.Size = new Size(72, 23);
        SendPacketStop.TabIndex = 15;
        SendPacketStop.Text = "Stop";
        SendPacketStop.UseVisualStyleBackColor = true;
        SendPacketStop.Click += SendPacketStop_Click;
        // 
        // SendPacketRepeatCount
        // 
        SendPacketRepeatCount.Location = new Point(229, 167);
        SendPacketRepeatCount.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        SendPacketRepeatCount.Name = "SendPacketRepeatCount";
        SendPacketRepeatCount.Size = new Size(68, 23);
        SendPacketRepeatCount.TabIndex = 6;
        SendPacketRepeatCount.TextAlign = HorizontalAlignment.Center;
        SendPacketRepeatCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Location = new Point(174, 171);
        label10.Name = "label10";
        label10.Size = new Size(49, 15);
        label10.TabIndex = 5;
        label10.Text = "Repeat :";
        // 
        // SendPacketDelay
        // 
        SendPacketDelay.Increment = new decimal(new int[] { 50, 0, 0, 0 });
        SendPacketDelay.Location = new Point(361, 167);
        SendPacketDelay.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        SendPacketDelay.Name = "SendPacketDelay";
        SendPacketDelay.Size = new Size(68, 23);
        SendPacketDelay.TabIndex = 4;
        SendPacketDelay.TextAlign = HorizontalAlignment.Center;
        SendPacketDelay.Value = new decimal(new int[] { 500, 0, 0, 0 });
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Location = new Point(313, 171);
        label8.Name = "label8";
        label8.Size = new Size(42, 15);
        label8.TabIndex = 3;
        label8.Text = "Delay :";
        // 
        // SendPacket
        // 
        SendPacket.Location = new Point(3, 167);
        SendPacket.Name = "SendPacket";
        SendPacket.Size = new Size(87, 23);
        SendPacket.TabIndex = 1;
        SendPacket.Text = "Send Packet";
        SendPacket.UseVisualStyleBackColor = true;
        SendPacket.Click += SendPacket_Click;
        // 
        // PacketTextBox
        // 
        PacketTextBox.BorderStyle = BorderStyle.FixedSingle;
        PacketTextBox.Location = new Point(6, 19);
        PacketTextBox.Name = "PacketTextBox";
        PacketTextBox.Size = new Size(426, 139);
        PacketTextBox.TabIndex = 0;
        PacketTextBox.Text = "";
        // 
        // StatusGroupBox
        // 
        StatusGroupBox.Controls.Add(ManaProgressBar);
        StatusGroupBox.Controls.Add(HealthProgressBar);
        StatusGroupBox.Location = new Point(6, 7);
        StatusGroupBox.Name = "StatusGroupBox";
        StatusGroupBox.Size = new Size(262, 71);
        StatusGroupBox.TabIndex = 0;
        StatusGroupBox.TabStop = false;
        StatusGroupBox.Text = "Status";
        // 
        // ManaProgressBar
        // 
        ManaProgressBar.BackColor = Color.Black;
        ManaProgressBar.CustomText = "";
        ManaProgressBar.ForeColor = Color.Red;
        ManaProgressBar.Location = new Point(6, 46);
        ManaProgressBar.Name = "ManaProgressBar";
        ManaProgressBar.ProgressColor = Color.Blue;
        ManaProgressBar.Size = new Size(250, 16);
        ManaProgressBar.TabIndex = 1;
        ManaProgressBar.TextColor = Color.White;
        ManaProgressBar.TextFont = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point);
        ManaProgressBar.VisualMode = Extensions.ProgressBarDisplayMode.CurrProgress;
        // 
        // HealthProgressBar
        // 
        HealthProgressBar.BackColor = Color.Black;
        HealthProgressBar.CustomText = "";
        HealthProgressBar.ForeColor = Color.Red;
        HealthProgressBar.Location = new Point(6, 22);
        HealthProgressBar.Name = "HealthProgressBar";
        HealthProgressBar.ProgressColor = Color.Red;
        HealthProgressBar.Size = new Size(250, 16);
        HealthProgressBar.TabIndex = 0;
        HealthProgressBar.TextColor = Color.White;
        HealthProgressBar.TextFont = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point);
        HealthProgressBar.VisualMode = Extensions.ProgressBarDisplayMode.CurrProgress;
        // 
        // ExperienceProgressBar
        // 
        ExperienceProgressBar.BackColor = Color.Black;
        ExperienceProgressBar.CustomText = "";
        ExperienceProgressBar.ForeColor = Color.Red;
        ExperienceProgressBar.Location = new Point(6, 817);
        ExperienceProgressBar.Name = "ExperienceProgressBar";
        ExperienceProgressBar.ProgressColor = Color.FromArgb(255, 128, 0);
        ExperienceProgressBar.Size = new Size(722, 16);
        ExperienceProgressBar.TabIndex = 2;
        ExperienceProgressBar.TextColor = Color.Black;
        ExperienceProgressBar.TextFont = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point);
        ExperienceProgressBar.VisualMode = Extensions.ProgressBarDisplayMode.Percentage;
        // 
        // MiniMapTimer
        // 
        MiniMapTimer.Tick += MiniMapTimer_Tick;
        // 
        // StatusTimer
        // 
        StatusTimer.Tick += StatusTimer_Tick;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(QuestButton);
        groupBox1.Controls.Add(button1);
        groupBox1.Controls.Add(RoutePlannerButton);
        groupBox1.Controls.Add(SkillInfoButton);
        groupBox1.Controls.Add(CharacterInfoButton);
        groupBox1.Controls.Add(InventoryButton);
        groupBox1.Controls.Add(SelfSkillButton);
        groupBox1.Controls.Add(AttackButton);
        groupBox1.Controls.Add(BotButton);
        groupBox1.Controls.Add(PressOkButton);
        groupBox1.Controls.Add(TownButton);
        groupBox1.Location = new Point(6, 541);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(262, 266);
        groupBox1.TabIndex = 6;
        groupBox1.TabStop = false;
        groupBox1.Text = "Quick Action";
        // 
        // QuestButton
        // 
        QuestButton.ForeColor = Color.Red;
        QuestButton.Location = new Point(132, 208);
        QuestButton.Name = "QuestButton";
        QuestButton.Size = new Size(120, 23);
        QuestButton.TabIndex = 15;
        QuestButton.Text = "Quest";
        QuestButton.UseVisualStyleBackColor = true;
        QuestButton.Click += QuestButton_Click;
        // 
        // button1
        // 
        button1.Location = new Point(132, 109);
        button1.Name = "button1";
        button1.Size = new Size(120, 23);
        button1.TabIndex = 5;
        button1.Text = "Reload Skill";
        button1.UseVisualStyleBackColor = true;
        button1.Click += ReloadSkillButton_Click;
        // 
        // SkillInfoButton
        // 
        SkillInfoButton.Location = new Point(132, 80);
        SkillInfoButton.Name = "SkillInfoButton";
        SkillInfoButton.Size = new Size(120, 23);
        SkillInfoButton.TabIndex = 10;
        SkillInfoButton.Text = "Skill";
        SkillInfoButton.UseVisualStyleBackColor = true;
        SkillInfoButton.Click += SkillInfoButton_Click;
        // 
        // CharacterInfoButton
        // 
        CharacterInfoButton.Location = new Point(6, 80);
        CharacterInfoButton.Name = "CharacterInfoButton";
        CharacterInfoButton.Size = new Size(120, 23);
        CharacterInfoButton.TabIndex = 9;
        CharacterInfoButton.Text = "Stat";
        CharacterInfoButton.UseVisualStyleBackColor = true;
        CharacterInfoButton.Click += CharacterInfoButton_Click;
        // 
        // InventoryButton
        // 
        InventoryButton.Location = new Point(132, 51);
        InventoryButton.Name = "InventoryButton";
        InventoryButton.Size = new Size(120, 23);
        InventoryButton.TabIndex = 8;
        InventoryButton.Text = "Inventory";
        InventoryButton.UseVisualStyleBackColor = true;
        InventoryButton.Click += InventoryButton_Click;
        // 
        // SelfSkillButton
        // 
        SelfSkillButton.ForeColor = Color.Red;
        SelfSkillButton.Location = new Point(132, 237);
        SelfSkillButton.Name = "SelfSkillButton";
        SelfSkillButton.Size = new Size(120, 23);
        SelfSkillButton.TabIndex = 5;
        SelfSkillButton.Text = "Self";
        SelfSkillButton.UseVisualStyleBackColor = true;
        SelfSkillButton.Click += SelfSkillButton_Click;
        // 
        // AttackButton
        // 
        AttackButton.ForeColor = Color.Red;
        AttackButton.Location = new Point(6, 237);
        AttackButton.Name = "AttackButton";
        AttackButton.Size = new Size(120, 23);
        AttackButton.TabIndex = 4;
        AttackButton.Text = "Attack";
        AttackButton.UseVisualStyleBackColor = true;
        AttackButton.Click += AttackButton_Click;
        // 
        // BotButton
        // 
        BotButton.ForeColor = Color.Red;
        BotButton.Location = new Point(6, 208);
        BotButton.Name = "BotButton";
        BotButton.Size = new Size(120, 23);
        BotButton.TabIndex = 3;
        BotButton.Text = "Bot";
        BotButton.UseVisualStyleBackColor = true;
        BotButton.Click += BotButton_Click;
        // 
        // PressOkButton
        // 
        PressOkButton.Location = new Point(132, 22);
        PressOkButton.Name = "PressOkButton";
        PressOkButton.Size = new Size(120, 23);
        PressOkButton.TabIndex = 1;
        PressOkButton.Text = "Press OK";
        PressOkButton.UseVisualStyleBackColor = true;
        PressOkButton.Click += PressOkButton_Click;
        // 
        // TownButton
        // 
        TownButton.Location = new Point(6, 22);
        TownButton.Name = "TownButton";
        TownButton.Size = new Size(120, 23);
        TownButton.TabIndex = 0;
        TownButton.Text = "Town";
        TownButton.UseVisualStyleBackColor = true;
        TownButton.Click += TownButton_Click;
        // 
        // SelfSkillTimer
        // 
        SelfSkillTimer.Enabled = true;
        SelfSkillTimer.Tick += SelfSkillTimer_Tick;
        // 
        // groupBox6
        // 
        groupBox6.Controls.Add(FollowOnlyNearby);
        groupBox6.Controls.Add(FollowSelect);
        groupBox6.Location = new Point(6, 400);
        groupBox6.Name = "groupBox6";
        groupBox6.Size = new Size(262, 76);
        groupBox6.TabIndex = 13;
        groupBox6.TabStop = false;
        groupBox6.Text = "Follow";
        // 
        // FollowOnlyNearby
        // 
        FollowOnlyNearby.AutoSize = true;
        FollowOnlyNearby.Location = new Point(6, 22);
        FollowOnlyNearby.Name = "FollowOnlyNearby";
        FollowOnlyNearby.Size = new Size(126, 19);
        FollowOnlyNearby.TabIndex = 14;
        FollowOnlyNearby.Text = "Follow only nearby";
        FollowOnlyNearby.UseVisualStyleBackColor = true;
        FollowOnlyNearby.CheckedChanged += FollowOnlyNearby_CheckedChanged;
        // 
        // FollowSelect
        // 
        FollowSelect.DropDownStyle = ComboBoxStyle.DropDownList;
        FollowSelect.FlatStyle = FlatStyle.System;
        FollowSelect.FormattingEnabled = true;
        FollowSelect.Location = new Point(6, 47);
        FollowSelect.Name = "FollowSelect";
        FollowSelect.Size = new Size(246, 23);
        FollowSelect.TabIndex = 0;
        FollowSelect.SelectionChangeCommitted += FollowSelect_SelectionChangeCommitted;
        // 
        // ProtectionTimer
        // 
        ProtectionTimer.Enabled = true;
        ProtectionTimer.Tick += ProtectionTimer_Tick;
        // 
        // SupplyTimer
        // 
        SupplyTimer.Enabled = true;
        SupplyTimer.Interval = 1000;
        SupplyTimer.Tick += SupplyTimer_Tick;
        // 
        // AutoPartyTimer
        // 
        AutoPartyTimer.Enabled = true;
        AutoPartyTimer.Interval = 1000;
        AutoPartyTimer.Tick += AutoPartyTimer_Tick;
        // 
        // PartyTimer
        // 
        PartyTimer.Enabled = true;
        PartyTimer.Interval = 1000;
        PartyTimer.Tick += PartyTimer_Tick;
        // 
        // groupBox11
        // 
        groupBox11.Controls.Add(ChangeCampTo2Button);
        groupBox11.Controls.Add(ChangeCampTo1Button);
        groupBox11.Location = new Point(6, 482);
        groupBox11.Name = "groupBox11";
        groupBox11.Size = new Size(262, 53);
        groupBox11.TabIndex = 14;
        groupBox11.TabStop = false;
        groupBox11.Text = "Military Camp";
        // 
        // ChangeCampTo2Button
        // 
        ChangeCampTo2Button.Location = new Point(132, 22);
        ChangeCampTo2Button.Name = "ChangeCampTo2Button";
        ChangeCampTo2Button.Size = new Size(120, 23);
        ChangeCampTo2Button.TabIndex = 1;
        ChangeCampTo2Button.Text = "Camp 2";
        ChangeCampTo2Button.UseVisualStyleBackColor = true;
        ChangeCampTo2Button.Click += ChangeCampTo2Button_Click;
        // 
        // ChangeCampTo1Button
        // 
        ChangeCampTo1Button.Location = new Point(6, 22);
        ChangeCampTo1Button.Name = "ChangeCampTo1Button";
        ChangeCampTo1Button.Size = new Size(120, 23);
        ChangeCampTo1Button.TabIndex = 0;
        ChangeCampTo1Button.Text = "Camp 1";
        ChangeCampTo1Button.UseVisualStyleBackColor = true;
        ChangeCampTo1Button.Click += ChangeCampTo1Button_Click;
        // 
        // UITimer
        // 
        UITimer.Enabled = true;
        UITimer.Tick += UITimer_Tick;
        // 
        // MasterCharacterTimer
        // 
        MasterCharacterTimer.Enabled = true;
        MasterCharacterTimer.Interval = 1250;
        MasterCharacterTimer.Tick += MasterCharacterTimer_Tick;
        // 
        // QuestTimer
        // 
        QuestTimer.Enabled = true;
        QuestTimer.Tick += QuestTimer_Tick;
        // 
        // MSConvertExperience
        // 
        MSConvertExperience.Enabled = true;
        MSConvertExperience.Interval = 60000;
        MSConvertExperience.Tick += MSConvertExperience_Tick;
        // 
        // MSAutoEvent
        // 
        MSAutoEvent.Enabled = true;
        MSAutoEvent.Interval = 1000;
        MSAutoEvent.Tick += MSAutoEvent_Tick;
        // 
        // ClientController
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(733, 836);
        Controls.Add(groupBox6);
        Controls.Add(groupBox11);
        Controls.Add(groupBox1);
        Controls.Add(ExperienceProgressBar);
        Controls.Add(StatusGroupBox);
        Controls.Add(tabControl1);
        Controls.Add(MapGroupBox);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ClientController";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Controller";
        FormClosing += ClientController_FormClosing;
        Load += ClientController_Load;
        VisibleChanged += ClientController_VisibleChanged;
        ((System.ComponentModel.ISupportInitialize)MiniMap).EndInit();
        MapGroupBox.ResumeLayout(false);
        tabControl1.ResumeLayout(false);
        MainPage.ResumeLayout(false);
        groupBox19.ResumeLayout(false);
        groupBox19.PerformLayout();
        groupBox15.ResumeLayout(false);
        groupBox15.PerformLayout();
        groupBox13.ResumeLayout(false);
        groupBox13.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)MasterGiveNoahAmount).EndInit();
        groupBox7.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)SupplyItemDataGrid).EndInit();
        groupBox5.ResumeLayout(false);
        groupBox5.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)LootMinPrice).EndInit();
        ProtectionGroupBox.ResumeLayout(false);
        ProtectionGroupBox.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)MinorPercentageValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)MpPotionPercentageValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)HpPotionPercentageValue).EndInit();
        AttackPage.ResumeLayout(false);
        groupBox4.ResumeLayout(false);
        groupBox4.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)TargetSearchRange).EndInit();
        ((System.ComponentModel.ISupportInitialize)AttackSpeed).EndInit();
        ((System.ComponentModel.ISupportInitialize)AttackRange).EndInit();
        groupBox3.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        SkillGroupBox.ResumeLayout(false);
        PartyPage.ResumeLayout(false);
        groupBox9.ResumeLayout(false);
        groupBox9.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)NearbyPlayerListDataGrid).EndInit();
        PartyListGroupBox.ResumeLayout(false);
        groupBox10.ResumeLayout(false);
        groupBox10.PerformLayout();
        groupBox8.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)PartyListDataGrid).EndInit();
        ActionPage.ResumeLayout(false);
        groupBox18.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)NpcShopDataList).EndInit();
        groupBox14.ResumeLayout(false);
        tabControl2.ResumeLayout(false);
        NpcListTab.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)QuestNpcList).EndInit();
        QuestListTab.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)QuestListViewDataGrid).EndInit();
        ActiveQuestListTab.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)RunningQuestListDataGrid).EndInit();
        CompletedTabPage.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)CompletedQuestListDataGrid).EndInit();
        FinishedQuestListTab.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)FinishedQuestListDataGrid).EndInit();
        groupBox12.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)GateListDataGrid).EndInit();
        ToolPage.ResumeLayout(false);
        groupBox16.ResumeLayout(false);
        groupBox16.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)MoveCoordinateY).EndInit();
        ((System.ComponentModel.ISupportInitialize)MoveCoordinateX).EndInit();
        groupBox17.ResumeLayout(false);
        groupBox17.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)SendPacketRepeatCount).EndInit();
        ((System.ComponentModel.ISupportInitialize)SendPacketDelay).EndInit();
        StatusGroupBox.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        groupBox6.ResumeLayout(false);
        groupBox6.PerformLayout();
        groupBox11.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private PictureBox MiniMap;
    private GroupBox MapGroupBox;
    private Button FullScreenMapButton;
    private TabControl tabControl1;
    private TabPage MainPage;
    private TabPage AttackPage;
    private GroupBox StatusGroupBox;
    private System.Windows.Forms.Timer MiniMapTimer;
    private System.Windows.Forms.Timer StatusTimer;
    private Extensions.TextProgressBar HealthProgressBar;
    private Extensions.TextProgressBar ManaProgressBar;
    private Extensions.TextProgressBar ExperienceProgressBar;
    private GroupBox groupBox1;
    private Button SelfSkillButton;
    private Button AttackButton;
    private Button BotButton;
    private Button PressOkButton;
    private Button TownButton;
    private GroupBox ProtectionGroupBox;
    private NumericUpDown MpPotionPercentageValue;
    private NumericUpDown HpPotionPercentageValue;
    private CheckBox MpPotionCheckBox;
    private CheckBox HpPotionCheckBox;
    private GroupBox groupBox2;
    private CheckedListBox SelfSkillCheckedListBox;
    private GroupBox SkillGroupBox;
    private CheckedListBox AttackSkillCheckedListBox;
    private GroupBox groupBox4;
    private GroupBox groupBox3;
    private Button TargetListClearButton;
    private Button SearchTargetButton;
    private CheckedListBox TargetCheckedListBox;
    private System.Windows.Forms.Timer SelfSkillTimer;
    private NumericUpDown AttackRange;
    private Label label1;
    private Button RoutePlannerButton;
    private Button InventoryButton;
    private Button CharacterInfoButton;
    private Button SkillInfoButton;
    private GroupBox groupBox5;
    private NumericUpDown LootMinPrice;
    private Label label2;
    private CheckBox EnableLoot;
    private GroupBox groupBox6;
    private ComboBox FollowSelect;
    private System.Windows.Forms.Timer ProtectionTimer;
    private GroupBox groupBox7;
    private DataGridView SupplyItemDataGrid;
    private System.Windows.Forms.Timer SupplyTimer;
    private CheckBox MoveToTarget;
    private System.Windows.Forms.Timer AutoPartyTimer;
    private TabPage PartyPage;
    private GroupBox PartyListGroupBox;
    private DataGridView PartyListDataGrid;
    private Button PartyMakeLeaderButton;
    private GroupBox groupBox8;
    private Button PartyKickButton;
    private Button PartyDisbandButton;
    private Button PartyDeclineButton;
    private Button PartyAcceptButton;
    private GroupBox groupBox9;
    private Button SendPartyButton;
    private DataGridView NearbyPlayerListDataGrid;
    private CheckBox RegenerateWhenDie;
    private CheckBox FollowTargetSync;
    private CheckBox FollowOnlyNearby;
    private NumericUpDown MinorPercentageValue;
    private CheckBox MinorCheckBox;
    private CheckBox GodModeCheckBox;
    private CheckBox HyperNoahCheckBox;
    private CheckBox MoveToLootCheckBox;
    private Button button1;
    private GroupBox groupBox10;
    private CheckBox SwiftPartyMemberCheckBox;
    private System.Windows.Forms.Timer PartyTimer;
    private GroupBox groupBox11;
    private Button ChangeCampTo2Button;
    private Button ChangeCampTo1Button;
    private GroupBox groupBox12;
    private Button GoToNearestGateButton;
    private Button GateTeleportButton;
    private DataGridView GateListDataGrid;
    private System.Windows.Forms.Timer UITimer;
    private GroupBox groupBox13;
    private NumericUpDown MasterGiveNoahAmount;
    private Label label4;
    private CheckBox SendTradeMasterNearby;
    private TextBox MasterCharacter;
    private Label label3;
    private System.Windows.Forms.Timer MasterCharacterTimer;
    private TabPage ActionPage;
    private GroupBox groupBox14;
    private DataGridView QuestNpcList;
    private Button LoadNpcListButton;
    private TabControl tabControl2;
    private TabPage NpcListTab;
    private TabPage QuestListTab;
    private Button GoToNpcButton;
    private DataGridView QuestListViewDataGrid;
    private TabPage ActiveQuestListTab;
    private Button TakeQuestButton;
    private DataGridView RunningQuestListDataGrid;
    private TabPage FinishedQuestListTab;
    private DataGridView FinishedQuestListDataGrid;
    private TabPage CompletedTabPage;
    private Button RemoveRunningQuestButton;
    private DataGridView CompletedQuestListDataGrid;
    private Button QuestButton;
    private System.Windows.Forms.Timer QuestTimer;
    private Button RemoveCompletedQuestButton;
    private Button GiveQuestButton;
    private Label label5;
    private NumericUpDown AttackSpeed;
    private GroupBox groupBox15;
    private CheckBox SpeedhackCheckbox;
    private GroupBox groupBox16;
    private Button GetCurrentCoordinate;
    private NumericUpDown MoveCoordinateY;
    private Label label7;
    private Label label6;
    private NumericUpDown MoveCoordinateX;
    private Button MoveCoordinateWithRoute;
    private Button MoveCoordinateDirect;
    private TabPage ToolPage;
    private GroupBox groupBox17;
    private Button SendPacket;
    private RichTextBox PacketTextBox;
    private Label label8;
    private NumericUpDown SendPacketDelay;
    private CheckBox DisableSkillCasting;
    private NumericUpDown TargetSearchRange;
    private Label label9;
    private GroupBox groupBox18;
    private Button ItemListButton;
    private Button GoToNpcShopButton;
    private DataGridView NpcShopDataList;
    private GroupBox groupBox19;
    private CheckBox ConvertMsToExp;
    private CheckBox AutoJoinMs;
    private System.Windows.Forms.Timer MSConvertExperience;
    private System.Windows.Forms.Timer MSAutoEvent;
    private NumericUpDown SendPacketRepeatCount;
    private Label label10;
    private Button SendPacketStop;
    private CheckBox FastLootMoney;
    private CheckBox ExpSealcheckBox;
    private Button btnSpawnCreature;
    private Button SummonButton;
    private CheckBox PrivateChatcheckBox;
    private CheckBox TradeBlockcheckBox;
    private TextBox SendPartyPlayerNametextBox;
    private Button gotoMasterCharacter;
    private CheckBox KeepFollowingcheckBox;
    private TextBox PartyAddPrefixtextBox;
    private CheckBox AutoPartycheckBox;
}