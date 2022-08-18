namespace KOF.UI.Forms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.FollowSelect = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NationComboBox = new System.Windows.Forms.ComboBox();
            this.AddAccountButton = new System.Windows.Forms.Button();
            this.ServerListComboBox = new System.Windows.Forms.ComboBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.AccountIdTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ResetCharacterButton = new System.Windows.Forms.Button();
            this.DeleteAccountButton = new System.Windows.Forms.Button();
            this.LoginServerButton = new System.Windows.Forms.Button();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.AccountDataGrid = new System.Windows.Forms.DataGridView();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.Launcher = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ClientTabPage = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.SetGroupColorButton = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ClearFollowButton = new System.Windows.Forms.Button();
            this.SetFollowButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TownButton = new System.Windows.Forms.Button();
            this.RunRouteButton = new System.Windows.Forms.Button();
            this.CloseClientButton = new System.Windows.Forms.Button();
            this.ClientListDataGrid = new System.Windows.Forms.DataGridView();
            this.GroupColorDialog = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AccountDataGrid)).BeginInit();
            this.Launcher.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.ClientTabPage.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClientListDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // FollowSelect
            // 
            this.FollowSelect.CausesValidation = false;
            this.FollowSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FollowSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FollowSelect.FormattingEnabled = true;
            this.FollowSelect.Location = new System.Drawing.Point(6, 22);
            this.FollowSelect.Name = "FollowSelect";
            this.FollowSelect.Size = new System.Drawing.Size(110, 23);
            this.FollowSelect.TabIndex = 1;
            this.FollowSelect.Tag = "123";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NationComboBox);
            this.groupBox1.Controls.Add(this.AddAccountButton);
            this.groupBox1.Controls.Add(this.ServerListComboBox);
            this.groupBox1.Controls.Add(this.PasswordTextBox);
            this.groupBox1.Controls.Add(this.AccountIdTextBox);
            this.groupBox1.Location = new System.Drawing.Point(6, 503);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Account";
            // 
            // NationComboBox
            // 
            this.NationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NationComboBox.FormattingEnabled = true;
            this.NationComboBox.Location = new System.Drawing.Point(123, 22);
            this.NationComboBox.Name = "NationComboBox";
            this.NationComboBox.Size = new System.Drawing.Size(111, 23);
            this.NationComboBox.TabIndex = 5;
            // 
            // AddAccountButton
            // 
            this.AddAccountButton.Location = new System.Drawing.Point(61, 85);
            this.AddAccountButton.Name = "AddAccountButton";
            this.AddAccountButton.Size = new System.Drawing.Size(112, 23);
            this.AddAccountButton.TabIndex = 4;
            this.AddAccountButton.Text = "Add Account";
            this.AddAccountButton.UseVisualStyleBackColor = true;
            this.AddAccountButton.Click += new System.EventHandler(this.AddAccountButton_Click);
            // 
            // ServerListComboBox
            // 
            this.ServerListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ServerListComboBox.FormattingEnabled = true;
            this.ServerListComboBox.Location = new System.Drawing.Point(123, 51);
            this.ServerListComboBox.Name = "ServerListComboBox";
            this.ServerListComboBox.Size = new System.Drawing.Size(111, 23);
            this.ServerListComboBox.TabIndex = 2;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(6, 51);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.PlaceholderText = "Password";
            this.PasswordTextBox.Size = new System.Drawing.Size(111, 23);
            this.PasswordTextBox.TabIndex = 3;
            // 
            // AccountIdTextBox
            // 
            this.AccountIdTextBox.Location = new System.Drawing.Point(6, 22);
            this.AccountIdTextBox.Name = "AccountIdTextBox";
            this.AccountIdTextBox.PlaceholderText = "ID";
            this.AccountIdTextBox.Size = new System.Drawing.Size(111, 23);
            this.AccountIdTextBox.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.AccountDataGrid);
            this.groupBox2.Location = new System.Drawing.Point(3, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(867, 623);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Account";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ResetCharacterButton);
            this.groupBox4.Controls.Add(this.DeleteAccountButton);
            this.groupBox4.Controls.Add(this.LoginServerButton);
            this.groupBox4.Controls.Add(this.DisconnectButton);
            this.groupBox4.Location = new System.Drawing.Point(740, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(121, 188);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Action";
            // 
            // ResetCharacterButton
            // 
            this.ResetCharacterButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ResetCharacterButton.Location = new System.Drawing.Point(3, 130);
            this.ResetCharacterButton.Name = "ResetCharacterButton";
            this.ResetCharacterButton.Size = new System.Drawing.Size(115, 23);
            this.ResetCharacterButton.TabIndex = 4;
            this.ResetCharacterButton.Text = "Reset Character";
            this.ResetCharacterButton.UseVisualStyleBackColor = true;
            this.ResetCharacterButton.Click += new System.EventHandler(this.ResetCharacterButton_Click);
            // 
            // DeleteAccountButton
            // 
            this.DeleteAccountButton.ForeColor = System.Drawing.Color.Red;
            this.DeleteAccountButton.Location = new System.Drawing.Point(3, 159);
            this.DeleteAccountButton.Name = "DeleteAccountButton";
            this.DeleteAccountButton.Size = new System.Drawing.Size(115, 23);
            this.DeleteAccountButton.TabIndex = 3;
            this.DeleteAccountButton.Text = "Delete";
            this.DeleteAccountButton.UseVisualStyleBackColor = true;
            this.DeleteAccountButton.Click += new System.EventHandler(this.DeleteAccountButton_Click);
            // 
            // LoginServerButton
            // 
            this.LoginServerButton.Location = new System.Drawing.Point(3, 22);
            this.LoginServerButton.Name = "LoginServerButton";
            this.LoginServerButton.Size = new System.Drawing.Size(115, 23);
            this.LoginServerButton.TabIndex = 0;
            this.LoginServerButton.Text = "Start";
            this.LoginServerButton.UseVisualStyleBackColor = true;
            this.LoginServerButton.Click += new System.EventHandler(this.LoginServerButton_Click);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Location = new System.Drawing.Point(3, 51);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(115, 23);
            this.DisconnectButton.TabIndex = 2;
            this.DisconnectButton.Text = "Close";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // AccountDataGrid
            // 
            this.AccountDataGrid.AllowDrop = true;
            this.AccountDataGrid.AllowUserToAddRows = false;
            this.AccountDataGrid.AllowUserToDeleteRows = false;
            this.AccountDataGrid.AllowUserToOrderColumns = true;
            this.AccountDataGrid.AllowUserToResizeColumns = false;
            this.AccountDataGrid.AllowUserToResizeRows = false;
            this.AccountDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.AccountDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.AccountDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.AccountDataGrid.Location = new System.Drawing.Point(6, 22);
            this.AccountDataGrid.Name = "AccountDataGrid";
            this.AccountDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.AccountDataGrid.RowTemplate.Height = 25;
            this.AccountDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AccountDataGrid.ShowEditingIcon = false;
            this.AccountDataGrid.Size = new System.Drawing.Size(728, 475);
            this.AccountDataGrid.TabIndex = 2;
            this.AccountDataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AccountDataGrid_CellDoubleClick);
            this.AccountDataGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.AccountDataGrid_EditingControlShowing);
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Interval = 500;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // Launcher
            // 
            this.Launcher.Controls.Add(this.tabPage1);
            this.Launcher.Controls.Add(this.ClientTabPage);
            this.Launcher.Location = new System.Drawing.Point(2, 2);
            this.Launcher.Name = "Launcher";
            this.Launcher.SelectedIndex = 0;
            this.Launcher.Size = new System.Drawing.Size(884, 663);
            this.Launcher.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(876, 635);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Launcher";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ClientTabPage
            // 
            this.ClientTabPage.Controls.Add(this.groupBox5);
            this.ClientTabPage.Location = new System.Drawing.Point(4, 24);
            this.ClientTabPage.Name = "ClientTabPage";
            this.ClientTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ClientTabPage.Size = new System.Drawing.Size(876, 635);
            this.ClientTabPage.TabIndex = 1;
            this.ClientTabPage.Text = "Control";
            this.ClientTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.SetGroupColorButton);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Controls.Add(this.ClientListDataGrid);
            this.groupBox5.Location = new System.Drawing.Point(3, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(867, 626);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Client";
            // 
            // SetGroupColorButton
            // 
            this.SetGroupColorButton.Location = new System.Drawing.Point(747, 594);
            this.SetGroupColorButton.Name = "SetGroupColorButton";
            this.SetGroupColorButton.Size = new System.Drawing.Size(108, 23);
            this.SetGroupColorButton.TabIndex = 5;
            this.SetGroupColorButton.Text = "Set Group Color";
            this.SetGroupColorButton.UseVisualStyleBackColor = true;
            this.SetGroupColorButton.Click += new System.EventHandler(this.SetGroupColorButton_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ClearFollowButton);
            this.groupBox6.Controls.Add(this.SetFollowButton);
            this.groupBox6.Controls.Add(this.FollowSelect);
            this.groupBox6.Location = new System.Drawing.Point(6, 22);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(287, 56);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Follow";
            // 
            // ClearFollowButton
            // 
            this.ClearFollowButton.Location = new System.Drawing.Point(203, 22);
            this.ClearFollowButton.Name = "ClearFollowButton";
            this.ClearFollowButton.Size = new System.Drawing.Size(75, 23);
            this.ClearFollowButton.TabIndex = 3;
            this.ClearFollowButton.Text = "Clear";
            this.ClearFollowButton.UseVisualStyleBackColor = true;
            this.ClearFollowButton.Click += new System.EventHandler(this.ClearFollowButton_Click);
            // 
            // SetFollowButton
            // 
            this.SetFollowButton.Location = new System.Drawing.Point(122, 22);
            this.SetFollowButton.Name = "SetFollowButton";
            this.SetFollowButton.Size = new System.Drawing.Size(75, 23);
            this.SetFollowButton.TabIndex = 2;
            this.SetFollowButton.Text = "Set";
            this.SetFollowButton.UseVisualStyleBackColor = true;
            this.SetFollowButton.Click += new System.EventHandler(this.SetFollowButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TownButton);
            this.groupBox3.Controls.Add(this.RunRouteButton);
            this.groupBox3.Controls.Add(this.CloseClientButton);
            this.groupBox3.Location = new System.Drawing.Point(299, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(562, 56);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Action";
            // 
            // TownButton
            // 
            this.TownButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TownButton.Location = new System.Drawing.Point(127, 22);
            this.TownButton.Name = "TownButton";
            this.TownButton.Size = new System.Drawing.Size(115, 23);
            this.TownButton.TabIndex = 4;
            this.TownButton.Text = "Send Town";
            this.TownButton.UseVisualStyleBackColor = true;
            this.TownButton.Click += new System.EventHandler(this.TownButton_Click);
            // 
            // RunRouteButton
            // 
            this.RunRouteButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RunRouteButton.Location = new System.Drawing.Point(6, 22);
            this.RunRouteButton.Name = "RunRouteButton";
            this.RunRouteButton.Size = new System.Drawing.Size(115, 23);
            this.RunRouteButton.TabIndex = 3;
            this.RunRouteButton.Text = "Run Route";
            this.RunRouteButton.UseVisualStyleBackColor = true;
            this.RunRouteButton.Click += new System.EventHandler(this.RunRouteButton_Click);
            // 
            // CloseClientButton
            // 
            this.CloseClientButton.ForeColor = System.Drawing.Color.Red;
            this.CloseClientButton.Location = new System.Drawing.Point(441, 21);
            this.CloseClientButton.Name = "CloseClientButton";
            this.CloseClientButton.Size = new System.Drawing.Size(115, 23);
            this.CloseClientButton.TabIndex = 2;
            this.CloseClientButton.Text = "Disconnect";
            this.CloseClientButton.UseVisualStyleBackColor = true;
            this.CloseClientButton.Click += new System.EventHandler(this.CloseClientButton_Click);
            // 
            // ClientListDataGrid
            // 
            this.ClientListDataGrid.AllowUserToAddRows = false;
            this.ClientListDataGrid.AllowUserToDeleteRows = false;
            this.ClientListDataGrid.AllowUserToResizeColumns = false;
            this.ClientListDataGrid.AllowUserToResizeRows = false;
            this.ClientListDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.ClientListDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ClientListDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ClientListDataGrid.Location = new System.Drawing.Point(3, 84);
            this.ClientListDataGrid.Name = "ClientListDataGrid";
            this.ClientListDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ClientListDataGrid.RowTemplate.Height = 25;
            this.ClientListDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ClientListDataGrid.ShowEditingIcon = false;
            this.ClientListDataGrid.Size = new System.Drawing.Size(858, 504);
            this.ClientListDataGrid.TabIndex = 3;
            this.ClientListDataGrid.VirtualMode = true;
            this.ClientListDataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClientListDataGrid_CellDoubleClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(886, 667);
            this.Controls.Add(this.Launcher);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KOF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.MainModern_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AccountDataGrid)).EndInit();
            this.Launcher.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ClientTabPage.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ClientListDataGrid)).EndInit();
            this.ResumeLayout(false);

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
    }
}