namespace KOF.UI.Forms;

partial class InventoryController
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryController));
            this.InventoryTimer = new System.Windows.Forms.Timer(this.components);
            this.ItemListDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.WeightLabel = new System.Windows.Forms.Label();
            this.NoahTextLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.EquipButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.UseButton = new System.Windows.Forms.Button();
            this.AutoSellButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ClearFlagButton = new System.Windows.Forms.Button();
            this.AutoInnStore = new System.Windows.Forms.Button();
            this.AutoDeleteButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ItemListDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // InventoryTimer
            // 
            this.InventoryTimer.Enabled = true;
            this.InventoryTimer.Tick += new System.EventHandler(this.InventoryTimer_Tick);
            // 
            // ItemListDataGridView
            // 
            this.ItemListDataGridView.AllowUserToAddRows = false;
            this.ItemListDataGridView.AllowUserToDeleteRows = false;
            this.ItemListDataGridView.AllowUserToResizeRows = false;
            this.ItemListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ItemListDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ItemListDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.ItemListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemListDataGridView.Location = new System.Drawing.Point(6, 43);
            this.ItemListDataGridView.Name = "ItemListDataGridView";
            this.ItemListDataGridView.ReadOnly = true;
            this.ItemListDataGridView.RowTemplate.Height = 25;
            this.ItemListDataGridView.Size = new System.Drawing.Size(839, 530);
            this.ItemListDataGridView.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.WeightLabel);
            this.groupBox1.Controls.Add(this.NoahTextLabel);
            this.groupBox1.Controls.Add(this.ItemListDataGridView);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(851, 580);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item List";
            // 
            // WeightLabel
            // 
            this.WeightLabel.AutoSize = true;
            this.WeightLabel.Location = new System.Drawing.Point(195, 22);
            this.WeightLabel.Name = "WeightLabel";
            this.WeightLabel.Size = new System.Drawing.Size(54, 15);
            this.WeightLabel.TabIndex = 3;
            this.WeightLabel.Text = "Weight : ";
            // 
            // NoahTextLabel
            // 
            this.NoahTextLabel.AutoSize = true;
            this.NoahTextLabel.Location = new System.Drawing.Point(6, 22);
            this.NoahTextLabel.Name = "NoahTextLabel";
            this.NoahTextLabel.Size = new System.Drawing.Size(45, 15);
            this.NoahTextLabel.TabIndex = 1;
            this.NoahTextLabel.Text = "Noah : ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.EquipButton);
            this.groupBox2.Controls.Add(this.DeleteButton);
            this.groupBox2.Controls.Add(this.UseButton);
            this.groupBox2.Location = new System.Drawing.Point(857, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 201);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Action";
            // 
            // EquipButton
            // 
            this.EquipButton.Location = new System.Drawing.Point(3, 28);
            this.EquipButton.Name = "EquipButton";
            this.EquipButton.Size = new System.Drawing.Size(157, 23);
            this.EquipButton.TabIndex = 4;
            this.EquipButton.Text = "Equip";
            this.EquipButton.UseVisualStyleBackColor = true;
            this.EquipButton.Click += new System.EventHandler(this.EquipButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.ForeColor = System.Drawing.Color.Red;
            this.DeleteButton.Location = new System.Drawing.Point(3, 172);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(157, 23);
            this.DeleteButton.TabIndex = 1;
            this.DeleteButton.Text = "Force Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // UseButton
            // 
            this.UseButton.Location = new System.Drawing.Point(3, 57);
            this.UseButton.Name = "UseButton";
            this.UseButton.Size = new System.Drawing.Size(157, 23);
            this.UseButton.TabIndex = 0;
            this.UseButton.Text = "Use";
            this.UseButton.UseVisualStyleBackColor = true;
            this.UseButton.Click += new System.EventHandler(this.UseButton_Click);
            // 
            // AutoSellButton
            // 
            this.AutoSellButton.ForeColor = System.Drawing.Color.ForestGreen;
            this.AutoSellButton.Location = new System.Drawing.Point(3, 22);
            this.AutoSellButton.Name = "AutoSellButton";
            this.AutoSellButton.Size = new System.Drawing.Size(157, 23);
            this.AutoSellButton.TabIndex = 2;
            this.AutoSellButton.Text = "Auto Sell";
            this.AutoSellButton.UseVisualStyleBackColor = true;
            this.AutoSellButton.Click += new System.EventHandler(this.AutoSellButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ClearFlagButton);
            this.groupBox3.Controls.Add(this.AutoInnStore);
            this.groupBox3.Controls.Add(this.AutoDeleteButton);
            this.groupBox3.Controls.Add(this.AutoSellButton);
            this.groupBox3.Location = new System.Drawing.Point(857, 339);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(166, 174);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Supply Flags";
            // 
            // ClearFlagButton
            // 
            this.ClearFlagButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClearFlagButton.Location = new System.Drawing.Point(3, 145);
            this.ClearFlagButton.Name = "ClearFlagButton";
            this.ClearFlagButton.Size = new System.Drawing.Size(157, 23);
            this.ClearFlagButton.TabIndex = 5;
            this.ClearFlagButton.Text = "Clear Flag";
            this.ClearFlagButton.UseVisualStyleBackColor = true;
            this.ClearFlagButton.Click += new System.EventHandler(this.ClearFlagButton_Click);
            // 
            // AutoInnStore
            // 
            this.AutoInnStore.ForeColor = System.Drawing.Color.Blue;
            this.AutoInnStore.Location = new System.Drawing.Point(3, 80);
            this.AutoInnStore.Name = "AutoInnStore";
            this.AutoInnStore.Size = new System.Drawing.Size(157, 23);
            this.AutoInnStore.TabIndex = 4;
            this.AutoInnStore.Text = "Auto Inn Store";
            this.AutoInnStore.UseVisualStyleBackColor = true;
            this.AutoInnStore.Click += new System.EventHandler(this.AutoInnStore_Click);
            // 
            // AutoDeleteButton
            // 
            this.AutoDeleteButton.ForeColor = System.Drawing.Color.Red;
            this.AutoDeleteButton.Location = new System.Drawing.Point(3, 51);
            this.AutoDeleteButton.Name = "AutoDeleteButton";
            this.AutoDeleteButton.Size = new System.Drawing.Size(157, 23);
            this.AutoDeleteButton.TabIndex = 3;
            this.AutoDeleteButton.Text = "Auto Delete";
            this.AutoDeleteButton.UseVisualStyleBackColor = true;
            this.AutoDeleteButton.Click += new System.EventHandler(this.AutoDeleteButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Location = new System.Drawing.Point(857, 207);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(166, 75);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Trade";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Trade";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 19);
            this.textBox2.Name = "textBox2";
            this.textBox2.PlaceholderText = "Nick";
            this.textBox2.Size = new System.Drawing.Size(157, 23);
            this.textBox2.TabIndex = 0;
            // 
            // InventoryController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 585);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InventoryController";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory";
            this.Load += new System.EventHandler(this.Inventory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ItemListDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Timer InventoryTimer;
    private DataGridView ItemListDataGridView;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private Button DeleteButton;
    private Button UseButton;
    private GroupBox groupBox3;
    private Button AutoSellButton;
    private Label NoahTextLabel;
    private Label WeightLabel;
    private Button EquipButton;
    private Button AutoInnStore;
    private Button AutoDeleteButton;
    private GroupBox groupBox4;
    private Button button1;
    private TextBox textBox2;
    private Button ClearFlagButton;
}