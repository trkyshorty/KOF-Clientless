namespace KOF.UI.Forms
{
    partial class ShopWindow
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ShopWeightLabel = new System.Windows.Forms.Label();
            this.ShopNoahTextLabel = new System.Windows.Forms.Label();
            this.BuyItemButton = new System.Windows.Forms.Button();
            this.ShopItemListDataGridView = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.InventoryWeightLabel = new System.Windows.Forms.Label();
            this.InventoryNoahTextLabel = new System.Windows.Forms.Label();
            this.InventoryItemListDataGridView = new System.Windows.Forms.DataGridView();
            this.SellItemButton = new System.Windows.Forms.Button();
            this.UITimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShopItemListDataGridView)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryItemListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(644, 598);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ShopWeightLabel);
            this.tabPage1.Controls.Add(this.ShopNoahTextLabel);
            this.tabPage1.Controls.Add(this.BuyItemButton);
            this.tabPage1.Controls.Add(this.ShopItemListDataGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(636, 570);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Shop";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ShopWeightLabel
            // 
            this.ShopWeightLabel.AutoSize = true;
            this.ShopWeightLabel.Location = new System.Drawing.Point(451, 11);
            this.ShopWeightLabel.Name = "ShopWeightLabel";
            this.ShopWeightLabel.Size = new System.Drawing.Size(54, 15);
            this.ShopWeightLabel.TabIndex = 4;
            this.ShopWeightLabel.Text = "Weight : ";
            // 
            // ShopNoahTextLabel
            // 
            this.ShopNoahTextLabel.AutoSize = true;
            this.ShopNoahTextLabel.Location = new System.Drawing.Point(219, 10);
            this.ShopNoahTextLabel.Name = "ShopNoahTextLabel";
            this.ShopNoahTextLabel.Size = new System.Drawing.Size(45, 15);
            this.ShopNoahTextLabel.TabIndex = 3;
            this.ShopNoahTextLabel.Text = "Noah : ";
            // 
            // BuyItemButton
            // 
            this.BuyItemButton.Location = new System.Drawing.Point(8, 6);
            this.BuyItemButton.Name = "BuyItemButton";
            this.BuyItemButton.Size = new System.Drawing.Size(81, 23);
            this.BuyItemButton.TabIndex = 2;
            this.BuyItemButton.Text = "Buy Item";
            this.BuyItemButton.UseVisualStyleBackColor = true;
            this.BuyItemButton.Click += new System.EventHandler(this.BuyItemButton_Click);
            // 
            // ShopItemListDataGridView
            // 
            this.ShopItemListDataGridView.AllowUserToAddRows = false;
            this.ShopItemListDataGridView.AllowUserToDeleteRows = false;
            this.ShopItemListDataGridView.AllowUserToResizeRows = false;
            this.ShopItemListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ShopItemListDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ShopItemListDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.ShopItemListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ShopItemListDataGridView.Location = new System.Drawing.Point(6, 35);
            this.ShopItemListDataGridView.MultiSelect = false;
            this.ShopItemListDataGridView.Name = "ShopItemListDataGridView";
            this.ShopItemListDataGridView.RowTemplate.Height = 25;
            this.ShopItemListDataGridView.Size = new System.Drawing.Size(625, 530);
            this.ShopItemListDataGridView.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.InventoryWeightLabel);
            this.tabPage2.Controls.Add(this.InventoryNoahTextLabel);
            this.tabPage2.Controls.Add(this.InventoryItemListDataGridView);
            this.tabPage2.Controls.Add(this.SellItemButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(636, 570);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Inventory";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // InventoryWeightLabel
            // 
            this.InventoryWeightLabel.AutoSize = true;
            this.InventoryWeightLabel.Location = new System.Drawing.Point(451, 11);
            this.InventoryWeightLabel.Name = "InventoryWeightLabel";
            this.InventoryWeightLabel.Size = new System.Drawing.Size(51, 15);
            this.InventoryWeightLabel.TabIndex = 6;
            this.InventoryWeightLabel.Text = "Weight :";
            // 
            // InventoryNoahTextLabel
            // 
            this.InventoryNoahTextLabel.AutoSize = true;
            this.InventoryNoahTextLabel.Location = new System.Drawing.Point(219, 10);
            this.InventoryNoahTextLabel.Name = "InventoryNoahTextLabel";
            this.InventoryNoahTextLabel.Size = new System.Drawing.Size(45, 15);
            this.InventoryNoahTextLabel.TabIndex = 5;
            this.InventoryNoahTextLabel.Text = "Noah : ";
            // 
            // InventoryItemListDataGridView
            // 
            this.InventoryItemListDataGridView.AllowUserToAddRows = false;
            this.InventoryItemListDataGridView.AllowUserToDeleteRows = false;
            this.InventoryItemListDataGridView.AllowUserToResizeRows = false;
            this.InventoryItemListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.InventoryItemListDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.InventoryItemListDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.InventoryItemListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InventoryItemListDataGridView.Location = new System.Drawing.Point(6, 33);
            this.InventoryItemListDataGridView.Name = "InventoryItemListDataGridView";
            this.InventoryItemListDataGridView.ReadOnly = true;
            this.InventoryItemListDataGridView.RowTemplate.Height = 25;
            this.InventoryItemListDataGridView.Size = new System.Drawing.Size(625, 530);
            this.InventoryItemListDataGridView.TabIndex = 4;
            // 
            // SellItemButton
            // 
            this.SellItemButton.Location = new System.Drawing.Point(8, 6);
            this.SellItemButton.Name = "SellItemButton";
            this.SellItemButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SellItemButton.Size = new System.Drawing.Size(81, 23);
            this.SellItemButton.TabIndex = 3;
            this.SellItemButton.Text = "Sell Item";
            this.SellItemButton.UseVisualStyleBackColor = true;
            this.SellItemButton.Click += new System.EventHandler(this.SellItemButton_Click);
            // 
            // UITimer
            // 
            this.UITimer.Enabled = true;
            this.UITimer.Tick += new System.EventHandler(this.UITimer_Tick);
            // 
            // ShopWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 599);
            this.Controls.Add(this.tabControl1);
            this.Name = "ShopWindow";
            this.Text = "Shop";
            this.Load += new System.EventHandler(this.ShopWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShopItemListDataGridView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryItemListDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button BuyItemButton;
        private DataGridView ShopItemListDataGridView;
        private DataGridView InventoryItemListDataGridView;
        private Button SellItemButton;
        private Label ShopNoahTextLabel;
        private Label ShopWeightLabel;
        private Label InventoryWeightLabel;
        private Label InventoryNoahTextLabel;
        private System.Windows.Forms.Timer UITimer;
    }
}