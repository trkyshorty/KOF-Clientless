namespace KOF.UI.Forms {
    partial class Main {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            FollowSelect = new ComboBox();
            groupBox1 = new GroupBox();
            NationComboBox = new ComboBox();
            AddAccountButton = new Button();
            ServerListComboBox = new ComboBox();
            PasswordTextBox = new TextBox();
            AccountIdTextBox = new TextBox();
            groupBox2 = new GroupBox();
            groupBox4 = new GroupBox();
            ResetCharacterButton = new Button();
            DeleteAccountButton = new Button();
            LoginServerButton = new Button();
            DisconnectButton = new Button();
            AccountDataGrid = new DataGridView();
            UpdateTimer = new System.Windows.Forms.Timer(components);
            Launcher = new TabControl();
            tabPage1 = new TabPage();
            ClientTabPage = new TabPage();
            groupBox5 = new GroupBox();
            FollowTargetSync = new CheckBox();
            TransformationIdTextBox = new TextBox();
            TransformationScrollbutton = new Button();
            groupBox17 = new GroupBox();
            SendPacketStop = new Button();
            SendPacketRepeatCount = new NumericUpDown();
            label10 = new Label();
            SendPacketDelay = new NumericUpDown();
            label8 = new Label();
            SendPacket = new Button();
            PacketTextBox = new RichTextBox();
            FastLootMoney = new CheckBox();
            EnableLoot = new CheckBox();
            SetGroupColorButton = new Button();
            groupBox6 = new GroupBox();
            ClearFollowButton = new Button();
            SetFollowButton = new Button();
            PrivateChatcheckBox = new CheckBox();
            groupBox3 = new GroupBox();
            ClanDisbandbutton = new Button();
            ClanAcceptButton = new Button();
            RegenButton = new Button();
            TownButton = new Button();
            RunRouteButton = new Button();
            CloseClientButton = new Button();
            ClientListDataGrid = new DataGridView();
            TradeBlockcheckBox = new CheckBox();
            DisableSkillCasting = new CheckBox();
            SpeedhackCheckbox = new CheckBox();
            ExpSealcheckBox = new CheckBox();
            GroupColorDialog = new ColorDialog();
            SystemTrayNotify = new NotifyIcon(components);
            label5 = new Label();
            AttackSpeed = new NumericUpDown();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AccountDataGrid).BeginInit();
            Launcher.SuspendLayout();
            tabPage1.SuspendLayout();
            ClientTabPage.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SendPacketRepeatCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SendPacketDelay).BeginInit();
            groupBox6.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ClientListDataGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AttackSpeed).BeginInit();
            SuspendLayout();
            // 
            // FollowSelect
            // 
            FollowSelect.CausesValidation = false;
            FollowSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            FollowSelect.FlatStyle = FlatStyle.Flat;
            FollowSelect.FormattingEnabled = true;
            FollowSelect.Location = new Point(6, 22);
            FollowSelect.Name = "FollowSelect";
            FollowSelect.Size = new Size(110, 23);
            FollowSelect.TabIndex = 1;
            FollowSelect.Tag = "123";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(NationComboBox);
            groupBox1.Controls.Add(AddAccountButton);
            groupBox1.Controls.Add(ServerListComboBox);
            groupBox1.Controls.Add(PasswordTextBox);
            groupBox1.Controls.Add(AccountIdTextBox);
            groupBox1.Location = new Point(6, 503);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(242, 114);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "New Account";
            // 
            // NationComboBox
            // 
            NationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            NationComboBox.FormattingEnabled = true;
            NationComboBox.Location = new Point(123, 22);
            NationComboBox.Name = "NationComboBox";
            NationComboBox.Size = new Size(111, 23);
            NationComboBox.TabIndex = 5;
            // 
            // AddAccountButton
            // 
            AddAccountButton.Location = new Point(61, 85);
            AddAccountButton.Name = "AddAccountButton";
            AddAccountButton.Size = new Size(112, 23);
            AddAccountButton.TabIndex = 4;
            AddAccountButton.Text = "Add Account";
            AddAccountButton.UseVisualStyleBackColor = true;
            AddAccountButton.Click += AddAccountButton_Click;
            // 
            // ServerListComboBox
            // 
            ServerListComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ServerListComboBox.FormattingEnabled = true;
            ServerListComboBox.Location = new Point(123, 51);
            ServerListComboBox.Name = "ServerListComboBox";
            ServerListComboBox.Size = new Size(111, 23);
            ServerListComboBox.TabIndex = 2;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(6, 51);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.PlaceholderText = "Password";
            PasswordTextBox.Size = new Size(111, 23);
            PasswordTextBox.TabIndex = 3;
            // 
            // AccountIdTextBox
            // 
            AccountIdTextBox.Location = new Point(6, 22);
            AccountIdTextBox.Name = "AccountIdTextBox";
            AccountIdTextBox.PlaceholderText = "ID";
            AccountIdTextBox.Size = new Size(111, 23);
            AccountIdTextBox.TabIndex = 2;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(groupBox1);
            groupBox2.Controls.Add(AccountDataGrid);
            groupBox2.Location = new Point(3, 6);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(867, 623);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Account";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(ResetCharacterButton);
            groupBox4.Controls.Add(DeleteAccountButton);
            groupBox4.Controls.Add(LoginServerButton);
            groupBox4.Controls.Add(DisconnectButton);
            groupBox4.Location = new Point(740, 22);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(121, 188);
            groupBox4.TabIndex = 5;
            groupBox4.TabStop = false;
            groupBox4.Text = "Action";
            // 
            // ResetCharacterButton
            // 
            ResetCharacterButton.ForeColor = SystemColors.ControlText;
            ResetCharacterButton.Location = new Point(3, 130);
            ResetCharacterButton.Name = "ResetCharacterButton";
            ResetCharacterButton.Size = new Size(115, 23);
            ResetCharacterButton.TabIndex = 4;
            ResetCharacterButton.Text = "Reset Character";
            ResetCharacterButton.UseVisualStyleBackColor = true;
            ResetCharacterButton.Click += ResetCharacterButton_Click;
            // 
            // DeleteAccountButton
            // 
            DeleteAccountButton.ForeColor = Color.Red;
            DeleteAccountButton.Location = new Point(3, 159);
            DeleteAccountButton.Name = "DeleteAccountButton";
            DeleteAccountButton.Size = new Size(115, 23);
            DeleteAccountButton.TabIndex = 3;
            DeleteAccountButton.Text = "Delete";
            DeleteAccountButton.UseVisualStyleBackColor = true;
            DeleteAccountButton.Click += DeleteAccountButton_Click;
            // 
            // LoginServerButton
            // 
            LoginServerButton.Location = new Point(3, 22);
            LoginServerButton.Name = "LoginServerButton";
            LoginServerButton.Size = new Size(115, 23);
            LoginServerButton.TabIndex = 0;
            LoginServerButton.Text = "Start";
            LoginServerButton.UseVisualStyleBackColor = true;
            LoginServerButton.Click += LoginServerButton_Click;
            // 
            // DisconnectButton
            // 
            DisconnectButton.Location = new Point(3, 51);
            DisconnectButton.Name = "DisconnectButton";
            DisconnectButton.Size = new Size(115, 23);
            DisconnectButton.TabIndex = 2;
            DisconnectButton.Text = "Close";
            DisconnectButton.UseVisualStyleBackColor = true;
            DisconnectButton.Click += DisconnectButton_Click;
            // 
            // AccountDataGrid
            // 
            AccountDataGrid.AllowDrop = true;
            AccountDataGrid.AllowUserToAddRows = false;
            AccountDataGrid.AllowUserToDeleteRows = false;
            AccountDataGrid.AllowUserToOrderColumns = true;
            AccountDataGrid.AllowUserToResizeColumns = false;
            AccountDataGrid.AllowUserToResizeRows = false;
            AccountDataGrid.BackgroundColor = Color.White;
            AccountDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            AccountDataGrid.EditMode = DataGridViewEditMode.EditOnEnter;
            AccountDataGrid.Location = new Point(6, 22);
            AccountDataGrid.Name = "AccountDataGrid";
            AccountDataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            AccountDataGrid.RowTemplate.Height = 25;
            AccountDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            AccountDataGrid.ShowEditingIcon = false;
            AccountDataGrid.Size = new Size(728, 475);
            AccountDataGrid.TabIndex = 2;
            AccountDataGrid.CellDoubleClick += AccountDataGrid_CellDoubleClick;
            AccountDataGrid.EditingControlShowing += AccountDataGrid_EditingControlShowing;
            // 
            // UpdateTimer
            // 
            UpdateTimer.Enabled = true;
            UpdateTimer.Interval = 500;
            UpdateTimer.Tick += UpdateTimer_Tick;
            // 
            // Launcher
            // 
            Launcher.Controls.Add(tabPage1);
            Launcher.Controls.Add(ClientTabPage);
            Launcher.Location = new Point(2, 2);
            Launcher.Name = "Launcher";
            Launcher.SelectedIndex = 0;
            Launcher.Size = new Size(884, 663);
            Launcher.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(876, 635);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Launcher";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // ClientTabPage
            // 
            ClientTabPage.Controls.Add(groupBox5);
            ClientTabPage.Location = new Point(4, 24);
            ClientTabPage.Name = "ClientTabPage";
            ClientTabPage.Padding = new Padding(3);
            ClientTabPage.Size = new Size(876, 635);
            ClientTabPage.TabIndex = 1;
            ClientTabPage.Text = "Control";
            ClientTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label5);
            groupBox5.Controls.Add(AttackSpeed);
            groupBox5.Controls.Add(FollowTargetSync);
            groupBox5.Controls.Add(TransformationIdTextBox);
            groupBox5.Controls.Add(TransformationScrollbutton);
            groupBox5.Controls.Add(groupBox17);
            groupBox5.Controls.Add(FastLootMoney);
            groupBox5.Controls.Add(EnableLoot);
            groupBox5.Controls.Add(SetGroupColorButton);
            groupBox5.Controls.Add(groupBox6);
            groupBox5.Controls.Add(PrivateChatcheckBox);
            groupBox5.Controls.Add(groupBox3);
            groupBox5.Controls.Add(ClientListDataGrid);
            groupBox5.Controls.Add(TradeBlockcheckBox);
            groupBox5.Controls.Add(DisableSkillCasting);
            groupBox5.Controls.Add(SpeedhackCheckbox);
            groupBox5.Controls.Add(ExpSealcheckBox);
            groupBox5.Location = new Point(3, 6);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(867, 626);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "Client";
            // 
            // FollowTargetSync
            // 
            FollowTargetSync.AutoSize = true;
            FollowTargetSync.Checked = true;
            FollowTargetSync.CheckState = CheckState.Indeterminate;
            FollowTargetSync.Location = new Point(141, 602);
            FollowTargetSync.Name = "FollowTargetSync";
            FollowTargetSync.Size = new Size(167, 19);
            FollowTargetSync.TabIndex = 31;
            FollowTargetSync.Text = "Followed client target sync";
            FollowTargetSync.UseVisualStyleBackColor = true;
            FollowTargetSync.CheckedChanged += FollowTargetSync_CheckedChanged;
            // 
            // TransformationIdTextBox
            // 
            TransformationIdTextBox.Location = new Point(299, 516);
            TransformationIdTextBox.MaxLength = 6;
            TransformationIdTextBox.Name = "TransformationIdTextBox";
            TransformationIdTextBox.PlaceholderText = "Transformation ID";
            TransformationIdTextBox.Size = new Size(77, 23);
            TransformationIdTextBox.TabIndex = 30;
            TransformationIdTextBox.Text = "472150";
            TransformationIdTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // TransformationScrollbutton
            // 
            TransformationScrollbutton.ForeColor = SystemColors.ControlText;
            TransformationScrollbutton.Location = new Point(141, 516);
            TransformationScrollbutton.Name = "TransformationScrollbutton";
            TransformationScrollbutton.Size = new Size(145, 23);
            TransformationScrollbutton.TabIndex = 7;
            TransformationScrollbutton.Text = "Transformation scroll";
            TransformationScrollbutton.UseVisualStyleBackColor = true;
            TransformationScrollbutton.Click += TransformationScrollbutton_Click;
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
            groupBox17.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox17.Location = new Point(504, 515);
            groupBox17.Name = "groupBox17";
            groupBox17.Size = new Size(357, 104);
            groupBox17.TabIndex = 29;
            groupBox17.TabStop = false;
            groupBox17.Text = "Packet Sender";
            // 
            // SendPacketStop
            // 
            SendPacketStop.Location = new Point(53, 71);
            SendPacketStop.Name = "SendPacketStop";
            SendPacketStop.Size = new Size(45, 23);
            SendPacketStop.TabIndex = 15;
            SendPacketStop.Text = "Stop";
            SendPacketStop.UseVisualStyleBackColor = true;
            SendPacketStop.Click += SendPacketStop_Click;
            // 
            // SendPacketRepeatCount
            // 
            SendPacketRepeatCount.Location = new Point(159, 71);
            SendPacketRepeatCount.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            SendPacketRepeatCount.Name = "SendPacketRepeatCount";
            SendPacketRepeatCount.Size = new Size(68, 22);
            SendPacketRepeatCount.TabIndex = 6;
            SendPacketRepeatCount.TextAlign = HorizontalAlignment.Center;
            SendPacketRepeatCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(104, 71);
            label10.Name = "label10";
            label10.Size = new Size(49, 13);
            label10.TabIndex = 5;
            label10.Text = "Repeat :";
            // 
            // SendPacketDelay
            // 
            SendPacketDelay.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            SendPacketDelay.Location = new Point(280, 74);
            SendPacketDelay.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            SendPacketDelay.Name = "SendPacketDelay";
            SendPacketDelay.Size = new Size(68, 22);
            SendPacketDelay.TabIndex = 4;
            SendPacketDelay.TextAlign = HorizontalAlignment.Center;
            SendPacketDelay.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(233, 73);
            label8.Name = "label8";
            label8.Size = new Size(41, 13);
            label8.TabIndex = 3;
            label8.Text = "Delay :";
            // 
            // SendPacket
            // 
            SendPacket.Location = new Point(6, 71);
            SendPacket.Name = "SendPacket";
            SendPacket.Size = new Size(41, 23);
            SendPacket.TabIndex = 1;
            SendPacket.Text = "start";
            SendPacket.UseVisualStyleBackColor = true;
            SendPacket.Click += SendPacket_Click;
            // 
            // PacketTextBox
            // 
            PacketTextBox.BorderStyle = BorderStyle.FixedSingle;
            PacketTextBox.Location = new Point(6, 19);
            PacketTextBox.Name = "PacketTextBox";
            PacketTextBox.Size = new Size(343, 46);
            PacketTextBox.TabIndex = 0;
            PacketTextBox.Text = "4800";
            // 
            // FastLootMoney
            // 
            FastLootMoney.AutoSize = true;
            FastLootMoney.Location = new Point(321, 577);
            FastLootMoney.Name = "FastLootMoney";
            FastLootMoney.Size = new Size(150, 19);
            FastLootMoney.TabIndex = 28;
            FastLootMoney.Text = "Fast Loot (Only Money)";
            FastLootMoney.UseVisualStyleBackColor = true;
            FastLootMoney.CheckedChanged += FastLootMoney_CheckedChanged;
            // 
            // EnableLoot
            // 
            EnableLoot.AutoSize = true;
            EnableLoot.Checked = true;
            EnableLoot.CheckState = CheckState.Indeterminate;
            EnableLoot.Location = new Point(321, 552);
            EnableLoot.Name = "EnableLoot";
            EnableLoot.Size = new Size(127, 19);
            EnableLoot.TabIndex = 27;
            EnableLoot.Text = "Enabled (auto loot)";
            EnableLoot.UseVisualStyleBackColor = true;
            EnableLoot.CheckedChanged += EnableLoot_CheckedChanged;
            // 
            // SetGroupColorButton
            // 
            SetGroupColorButton.Location = new Point(3, 516);
            SetGroupColorButton.Name = "SetGroupColorButton";
            SetGroupColorButton.Size = new Size(108, 23);
            SetGroupColorButton.TabIndex = 5;
            SetGroupColorButton.Text = "Set Group Color";
            SetGroupColorButton.UseVisualStyleBackColor = true;
            SetGroupColorButton.Click += SetGroupColorButton_Click;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(ClearFollowButton);
            groupBox6.Controls.Add(SetFollowButton);
            groupBox6.Controls.Add(FollowSelect);
            groupBox6.Location = new Point(6, 22);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(287, 55);
            groupBox6.TabIndex = 7;
            groupBox6.TabStop = false;
            groupBox6.Text = "Follow";
            // 
            // ClearFollowButton
            // 
            ClearFollowButton.Location = new Point(203, 22);
            ClearFollowButton.Name = "ClearFollowButton";
            ClearFollowButton.Size = new Size(75, 23);
            ClearFollowButton.TabIndex = 3;
            ClearFollowButton.Text = "Clear";
            ClearFollowButton.UseVisualStyleBackColor = true;
            ClearFollowButton.Click += ClearFollowButton_Click;
            // 
            // SetFollowButton
            // 
            SetFollowButton.Location = new Point(122, 22);
            SetFollowButton.Name = "SetFollowButton";
            SetFollowButton.Size = new Size(75, 23);
            SetFollowButton.TabIndex = 2;
            SetFollowButton.Text = "Set";
            SetFollowButton.UseVisualStyleBackColor = true;
            SetFollowButton.Click += SetFollowButton_Click;
            // 
            // PrivateChatcheckBox
            // 
            PrivateChatcheckBox.AutoSize = true;
            PrivateChatcheckBox.Checked = true;
            PrivateChatcheckBox.CheckState = CheckState.Indeterminate;
            PrivateChatcheckBox.Location = new Point(6, 552);
            PrivateChatcheckBox.Name = "PrivateChatcheckBox";
            PrivateChatcheckBox.Size = new Size(76, 19);
            PrivateChatcheckBox.TabIndex = 25;
            PrivateChatcheckBox.Text = "PM Block";
            PrivateChatcheckBox.UseVisualStyleBackColor = true;
            PrivateChatcheckBox.CheckedChanged += PrivateChatcheckBox_CheckedChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(ClanDisbandbutton);
            groupBox3.Controls.Add(ClanAcceptButton);
            groupBox3.Controls.Add(RegenButton);
            groupBox3.Controls.Add(TownButton);
            groupBox3.Controls.Add(RunRouteButton);
            groupBox3.Controls.Add(CloseClientButton);
            groupBox3.Location = new Point(299, 22);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(562, 55);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "Action";
            // 
            // ClanDisbandbutton
            // 
            ClanDisbandbutton.ForeColor = SystemColors.ControlText;
            ClanDisbandbutton.Location = new Point(317, 20);
            ClanDisbandbutton.Name = "ClanDisbandbutton";
            ClanDisbandbutton.Size = new Size(88, 23);
            ClanDisbandbutton.TabIndex = 7;
            ClanDisbandbutton.Text = "Clan disband";
            ClanDisbandbutton.UseVisualStyleBackColor = true;
            ClanDisbandbutton.Click += ClanDisbandbutton_Click;
            // 
            // ClanAcceptButton
            // 
            ClanAcceptButton.ForeColor = SystemColors.ControlText;
            ClanAcceptButton.Location = new Point(221, 20);
            ClanAcceptButton.Name = "ClanAcceptButton";
            ClanAcceptButton.Size = new Size(88, 23);
            ClanAcceptButton.TabIndex = 6;
            ClanAcceptButton.Text = "Clan accept";
            ClanAcceptButton.UseVisualStyleBackColor = true;
            ClanAcceptButton.Click += ClanAcceptButton_Click;
            // 
            // RegenButton
            // 
            RegenButton.ForeColor = SystemColors.ControlText;
            RegenButton.Location = new Point(149, 20);
            RegenButton.Name = "RegenButton";
            RegenButton.Size = new Size(64, 23);
            RegenButton.TabIndex = 5;
            RegenButton.Text = "Regen";
            RegenButton.UseVisualStyleBackColor = true;
            RegenButton.Click += RegenButton_Click;
            // 
            // TownButton
            // 
            TownButton.ForeColor = SystemColors.ControlText;
            TownButton.Location = new Point(85, 20);
            TownButton.Name = "TownButton";
            TownButton.Size = new Size(56, 23);
            TownButton.TabIndex = 4;
            TownButton.Text = "Town";
            TownButton.UseVisualStyleBackColor = true;
            TownButton.Click += TownButton_Click;
            // 
            // RunRouteButton
            // 
            RunRouteButton.ForeColor = SystemColors.ControlText;
            RunRouteButton.Location = new Point(5, 20);
            RunRouteButton.Name = "RunRouteButton";
            RunRouteButton.Size = new Size(72, 23);
            RunRouteButton.TabIndex = 3;
            RunRouteButton.Text = "Run Route";
            RunRouteButton.UseVisualStyleBackColor = true;
            RunRouteButton.Click += RunRouteButton_Click;
            // 
            // CloseClientButton
            // 
            CloseClientButton.ForeColor = Color.Red;
            CloseClientButton.Location = new Point(440, 20);
            CloseClientButton.Name = "CloseClientButton";
            CloseClientButton.Size = new Size(115, 23);
            CloseClientButton.TabIndex = 2;
            CloseClientButton.Text = "Disconnect";
            CloseClientButton.UseVisualStyleBackColor = true;
            CloseClientButton.Click += CloseClientButton_Click;
            // 
            // ClientListDataGrid
            // 
            ClientListDataGrid.AllowUserToAddRows = false;
            ClientListDataGrid.AllowUserToDeleteRows = false;
            ClientListDataGrid.AllowUserToResizeColumns = false;
            ClientListDataGrid.AllowUserToResizeRows = false;
            ClientListDataGrid.BackgroundColor = Color.White;
            ClientListDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ClientListDataGrid.EditMode = DataGridViewEditMode.EditOnEnter;
            ClientListDataGrid.Location = new Point(3, 83);
            ClientListDataGrid.Name = "ClientListDataGrid";
            ClientListDataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            ClientListDataGrid.RowTemplate.Height = 25;
            ClientListDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ClientListDataGrid.ShowEditingIcon = false;
            ClientListDataGrid.Size = new Size(858, 426);
            ClientListDataGrid.TabIndex = 3;
            ClientListDataGrid.VirtualMode = true;
            ClientListDataGrid.CellDoubleClick += ClientListDataGrid_CellDoubleClick;
            // 
            // TradeBlockcheckBox
            // 
            TradeBlockcheckBox.AutoSize = true;
            TradeBlockcheckBox.Checked = true;
            TradeBlockcheckBox.CheckState = CheckState.Indeterminate;
            TradeBlockcheckBox.Location = new Point(6, 577);
            TradeBlockcheckBox.Name = "TradeBlockcheckBox";
            TradeBlockcheckBox.Size = new Size(86, 19);
            TradeBlockcheckBox.TabIndex = 24;
            TradeBlockcheckBox.Text = "Trade Block";
            TradeBlockcheckBox.UseVisualStyleBackColor = true;
            TradeBlockcheckBox.CheckedChanged += TradeBlockcheckBox_CheckedChanged;
            // 
            // DisableSkillCasting
            // 
            DisableSkillCasting.AutoSize = true;
            DisableSkillCasting.ForeColor = Color.Red;
            DisableSkillCasting.Location = new Point(141, 577);
            DisableSkillCasting.Name = "DisableSkillCasting";
            DisableSkillCasting.Size = new Size(128, 19);
            DisableSkillCasting.TabIndex = 10;
            DisableSkillCasting.Text = "Disable skill casting";
            DisableSkillCasting.UseVisualStyleBackColor = true;
            DisableSkillCasting.CheckedChanged += DisableSkillCasting_CheckedChanged;
            // 
            // SpeedhackCheckbox
            // 
            SpeedhackCheckbox.AutoSize = true;
            SpeedhackCheckbox.Location = new Point(141, 552);
            SpeedhackCheckbox.Name = "SpeedhackCheckbox";
            SpeedhackCheckbox.Size = new Size(83, 19);
            SpeedhackCheckbox.TabIndex = 5;
            SpeedhackCheckbox.Text = "Speedhack";
            SpeedhackCheckbox.UseVisualStyleBackColor = true;
            SpeedhackCheckbox.CheckedChanged += SpeedhackCheckbox_CheckedChanged;
            // 
            // ExpSealcheckBox
            // 
            ExpSealcheckBox.AutoSize = true;
            ExpSealcheckBox.Checked = true;
            ExpSealcheckBox.CheckState = CheckState.Indeterminate;
            ExpSealcheckBox.Location = new Point(6, 602);
            ExpSealcheckBox.Name = "ExpSealcheckBox";
            ExpSealcheckBox.Size = new Size(129, 19);
            ExpSealcheckBox.TabIndex = 22;
            ExpSealcheckBox.Text = "Exp Seal ( 30 lv. >=)";
            ExpSealcheckBox.UseVisualStyleBackColor = true;
            ExpSealcheckBox.CheckedChanged += ExpSealcheckBox_CheckedChanged;
            // 
            // SystemTrayNotify
            // 
            SystemTrayNotify.Icon = (Icon)resources.GetObject("SystemTrayNotify.Icon");
            SystemTrayNotify.Text = "KOF continues to run in the background";
            SystemTrayNotify.Visible = true;
            SystemTrayNotify.MouseDoubleClick += SystemTrayNotify_MouseDoubleClick;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(321, 606);
            label5.Name = "label5";
            label5.Size = new Size(81, 15);
            label5.TabIndex = 33;
            label5.Text = "Attack speed :";
            // 
            // AttackSpeed
            // 
            AttackSpeed.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            AttackSpeed.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            AttackSpeed.Location = new Point(408, 602);
            AttackSpeed.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            AttackSpeed.Name = "AttackSpeed";
            AttackSpeed.Size = new Size(54, 23);
            AttackSpeed.TabIndex = 32;
            AttackSpeed.TextAlign = HorizontalAlignment.Center;
            AttackSpeed.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            AttackSpeed.ValueChanged += AttackSpeed_ValueChanged;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(886, 667);
            Controls.Add(Launcher);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "KOF";
            FormClosing += Main_FormClosing;
            Load += MainModern_Load;
            Resize += Main_Resize;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)AccountDataGrid).EndInit();
            Launcher.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ClientTabPage.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox17.ResumeLayout(false);
            groupBox17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SendPacketRepeatCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)SendPacketDelay).EndInit();
            groupBox6.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ClientListDataGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)AttackSpeed).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button DisconnectButton;
        private Button LoginServerButton;
        private DataGridView AccountDataGrid;
        private System.Windows.Forms.Timer UpdateTimer;
        private TabControl Launcher;
        private TabPage tabPage1;
        private GroupBox groupBox5;
        private DataGridView ClientListDataGrid;
        private ComboBox ServerListComboBox;
        private Button AddAccountButton;
        private TextBox PasswordTextBox;
        private TextBox AccountIdTextBox;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
        private Button CloseClientButton;
        private TabPage ClientTabPage;
        private Button DeleteAccountButton;
        private GroupBox groupBox6;
        private Button SetFollowButton;
        private ComboBox FollowSelect;
        private Button RunRouteButton;
        private Button TownButton;
        private ComboBox NationComboBox;
        private Button ResetCharacterButton;
        private Button ClearFollowButton;
        private Button SetGroupColorButton;
        private ColorDialog GroupColorDialog;
        private NotifyIcon SystemTrayNotify;
        private CheckBox SpeedhackCheckbox;
        private CheckBox DisableSkillCasting;
        private CheckBox ExpSealcheckBox;
        private CheckBox PrivateChatcheckBox;
        private CheckBox TradeBlockcheckBox;
        private CheckBox FastLootMoney;
        private CheckBox EnableLoot;
        private Button RegenButton;
        private Button ClanAcceptButton;
        private GroupBox groupBox17;
        private Button SendPacketStop;
        private NumericUpDown SendPacketRepeatCount;
        private Label label10;
        private NumericUpDown SendPacketDelay;
        private Label label8;
        private Button SendPacket;
        private RichTextBox PacketTextBox;
        private TextBox textBox1;
        private Button TransformationScrollbutton;
        private TextBox TransformationIdTextBox;
        private Button ClanDisbandbutton;
        private CheckBox FollowTargetSync;
        private Label label5;
        private NumericUpDown AttackSpeed;
    }
}