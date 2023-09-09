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
            FlagSet = new Button();
            PrivateChatcheckBox = new CheckBox();
            TradeBlockcheckBox = new CheckBox();
            ExpSealcheckBox = new CheckBox();
            SpeedhackCheckbox = new CheckBox();
            DisableSkillCasting = new CheckBox();
            SetGroupColorButton = new Button();
            groupBox6 = new GroupBox();
            ClearFollowButton = new Button();
            SetFollowButton = new Button();
            groupBox3 = new GroupBox();
            TownButton = new Button();
            RunRouteButton = new Button();
            CloseClientButton = new Button();
            ClientListDataGrid = new DataGridView();
            GroupColorDialog = new ColorDialog();
            SystemTrayNotify = new NotifyIcon(components);
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AccountDataGrid).BeginInit();
            Launcher.SuspendLayout();
            tabPage1.SuspendLayout();
            ClientTabPage.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ClientListDataGrid).BeginInit();
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
            groupBox5.Controls.Add(FlagSet);
            groupBox5.Controls.Add(PrivateChatcheckBox);
            groupBox5.Controls.Add(TradeBlockcheckBox);
            groupBox5.Controls.Add(ExpSealcheckBox);
            groupBox5.Controls.Add(SpeedhackCheckbox);
            groupBox5.Controls.Add(DisableSkillCasting);
            groupBox5.Controls.Add(SetGroupColorButton);
            groupBox5.Controls.Add(groupBox6);
            groupBox5.Controls.Add(groupBox3);
            groupBox5.Controls.Add(ClientListDataGrid);
            groupBox5.Location = new Point(3, 6);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(867, 626);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "Client";
            // 
            // FlagSet
            // 
            FlagSet.Location = new Point(727, 233);
            FlagSet.Name = "FlagSet";
            FlagSet.Size = new Size(134, 23);
            FlagSet.TabIndex = 26;
            FlagSet.Text = "Set";
            FlagSet.UseVisualStyleBackColor = true;
            FlagSet.Click += FlagSet_Click;
            // 
            // PrivateChatcheckBox
            // 
            PrivateChatcheckBox.AutoSize = true;
            PrivateChatcheckBox.Checked = true;
            PrivateChatcheckBox.CheckState = CheckState.Indeterminate;
            PrivateChatcheckBox.Location = new Point(727, 206);
            PrivateChatcheckBox.Name = "PrivateChatcheckBox";
            PrivateChatcheckBox.Size = new Size(76, 19);
            PrivateChatcheckBox.TabIndex = 25;
            PrivateChatcheckBox.Text = "PM Block";
            PrivateChatcheckBox.UseVisualStyleBackColor = true;
            // 
            // TradeBlockcheckBox
            // 
            TradeBlockcheckBox.AutoSize = true;
            TradeBlockcheckBox.Checked = true;
            TradeBlockcheckBox.CheckState = CheckState.Indeterminate;
            TradeBlockcheckBox.Location = new Point(727, 179);
            TradeBlockcheckBox.Name = "TradeBlockcheckBox";
            TradeBlockcheckBox.Size = new Size(86, 19);
            TradeBlockcheckBox.TabIndex = 24;
            TradeBlockcheckBox.Text = "Trade Block";
            TradeBlockcheckBox.UseVisualStyleBackColor = true;
            // 
            // ExpSealcheckBox
            // 
            ExpSealcheckBox.AutoSize = true;
            ExpSealcheckBox.Checked = true;
            ExpSealcheckBox.CheckState = CheckState.Indeterminate;
            ExpSealcheckBox.Location = new Point(727, 152);
            ExpSealcheckBox.Name = "ExpSealcheckBox";
            ExpSealcheckBox.Size = new Size(129, 19);
            ExpSealcheckBox.TabIndex = 22;
            ExpSealcheckBox.Text = "Exp Seal ( 30 lv. >=)";
            ExpSealcheckBox.UseVisualStyleBackColor = true;
            // 
            // SpeedhackCheckbox
            // 
            SpeedhackCheckbox.AutoSize = true;
            SpeedhackCheckbox.Location = new Point(727, 125);
            SpeedhackCheckbox.Name = "SpeedhackCheckbox";
            SpeedhackCheckbox.Size = new Size(83, 19);
            SpeedhackCheckbox.TabIndex = 5;
            SpeedhackCheckbox.Text = "Speedhack";
            SpeedhackCheckbox.UseVisualStyleBackColor = true;
            // 
            // DisableSkillCasting
            // 
            DisableSkillCasting.AutoSize = true;
            DisableSkillCasting.ForeColor = Color.Red;
            DisableSkillCasting.Location = new Point(727, 98);
            DisableSkillCasting.Name = "DisableSkillCasting";
            DisableSkillCasting.Size = new Size(128, 19);
            DisableSkillCasting.TabIndex = 10;
            DisableSkillCasting.Text = "Disable skill casting";
            DisableSkillCasting.UseVisualStyleBackColor = true;
            // 
            // SetGroupColorButton
            // 
            SetGroupColorButton.Location = new Point(747, 594);
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
            groupBox6.Size = new Size(287, 56);
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
            // groupBox3
            // 
            groupBox3.Controls.Add(TownButton);
            groupBox3.Controls.Add(RunRouteButton);
            groupBox3.Controls.Add(CloseClientButton);
            groupBox3.Location = new Point(299, 22);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(562, 56);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "Action";
            // 
            // TownButton
            // 
            TownButton.ForeColor = SystemColors.ControlText;
            TownButton.Location = new Point(127, 22);
            TownButton.Name = "TownButton";
            TownButton.Size = new Size(115, 23);
            TownButton.TabIndex = 4;
            TownButton.Text = "Send Town";
            TownButton.UseVisualStyleBackColor = true;
            TownButton.Click += TownButton_Click;
            // 
            // RunRouteButton
            // 
            RunRouteButton.ForeColor = SystemColors.ControlText;
            RunRouteButton.Location = new Point(6, 22);
            RunRouteButton.Name = "RunRouteButton";
            RunRouteButton.Size = new Size(115, 23);
            RunRouteButton.TabIndex = 3;
            RunRouteButton.Text = "Run Route";
            RunRouteButton.UseVisualStyleBackColor = true;
            RunRouteButton.Click += RunRouteButton_Click;
            // 
            // CloseClientButton
            // 
            CloseClientButton.ForeColor = Color.Red;
            CloseClientButton.Location = new Point(441, 21);
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
            ClientListDataGrid.Location = new Point(3, 84);
            ClientListDataGrid.Name = "ClientListDataGrid";
            ClientListDataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            ClientListDataGrid.RowTemplate.Height = 25;
            ClientListDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ClientListDataGrid.ShowEditingIcon = false;
            ClientListDataGrid.Size = new Size(718, 504);
            ClientListDataGrid.TabIndex = 3;
            ClientListDataGrid.VirtualMode = true;
            ClientListDataGrid.CellDoubleClick += ClientListDataGrid_CellDoubleClick;
            // 
            // SystemTrayNotify
            // 
            SystemTrayNotify.Icon = (Icon)resources.GetObject("SystemTrayNotify.Icon");
            SystemTrayNotify.Text = "KOF continues to run in the background";
            SystemTrayNotify.Visible = true;
            SystemTrayNotify.MouseDoubleClick += SystemTrayNotify_MouseDoubleClick;
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
            groupBox6.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ClientListDataGrid).EndInit();
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
        private Button FlagSet;
    }
}