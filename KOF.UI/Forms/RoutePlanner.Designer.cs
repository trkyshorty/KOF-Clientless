namespace KOF.UI.Forms;

partial class RoutePlanner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoutePlanner));
            this.MapGroupBox = new System.Windows.Forms.GroupBox();
            this.ResetRouteButton = new System.Windows.Forms.Button();
            this.RunRouteButton = new System.Windows.Forms.Button();
            this.DeleteRouteButton = new System.Windows.Forms.Button();
            this.RouteName = new System.Windows.Forms.TextBox();
            this.SaveRouteButton = new System.Windows.Forms.Button();
            this.SelectedRouteComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Map = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.InnHostessAreaButton = new System.Windows.Forms.Button();
            this.TownActionButton = new System.Windows.Forms.Button();
            this.SupplyAreaActionButton = new System.Windows.Forms.Button();
            this.RenderTimer = new System.Windows.Forms.Timer(this.components);
            this.MapGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Map)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MapGroupBox
            // 
            this.MapGroupBox.Controls.Add(this.ResetRouteButton);
            this.MapGroupBox.Controls.Add(this.RunRouteButton);
            this.MapGroupBox.Controls.Add(this.DeleteRouteButton);
            this.MapGroupBox.Controls.Add(this.RouteName);
            this.MapGroupBox.Controls.Add(this.SaveRouteButton);
            this.MapGroupBox.Controls.Add(this.SelectedRouteComboBox);
            this.MapGroupBox.Controls.Add(this.label1);
            this.MapGroupBox.Controls.Add(this.Map);
            this.MapGroupBox.Location = new System.Drawing.Point(3, 3);
            this.MapGroupBox.Name = "MapGroupBox";
            this.MapGroupBox.Size = new System.Drawing.Size(813, 854);
            this.MapGroupBox.TabIndex = 5;
            this.MapGroupBox.TabStop = false;
            this.MapGroupBox.Text = "Planner";
            // 
            // ResetRouteButton
            // 
            this.ResetRouteButton.Location = new System.Drawing.Point(731, 15);
            this.ResetRouteButton.Name = "ResetRouteButton";
            this.ResetRouteButton.Size = new System.Drawing.Size(75, 23);
            this.ResetRouteButton.TabIndex = 12;
            this.ResetRouteButton.Text = "Reset";
            this.ResetRouteButton.UseVisualStyleBackColor = true;
            this.ResetRouteButton.Click += new System.EventHandler(this.ResetRouteButton_Click);
            // 
            // RunRouteButton
            // 
            this.RunRouteButton.Location = new System.Drawing.Point(217, 16);
            this.RunRouteButton.Name = "RunRouteButton";
            this.RunRouteButton.Size = new System.Drawing.Size(75, 23);
            this.RunRouteButton.TabIndex = 11;
            this.RunRouteButton.Text = "Run";
            this.RunRouteButton.UseVisualStyleBackColor = true;
            this.RunRouteButton.Click += new System.EventHandler(this.RunRouteButton_Click);
            // 
            // DeleteRouteButton
            // 
            this.DeleteRouteButton.ForeColor = System.Drawing.Color.Red;
            this.DeleteRouteButton.Location = new System.Drawing.Point(298, 16);
            this.DeleteRouteButton.Name = "DeleteRouteButton";
            this.DeleteRouteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteRouteButton.TabIndex = 10;
            this.DeleteRouteButton.Text = "Delete";
            this.DeleteRouteButton.UseVisualStyleBackColor = true;
            this.DeleteRouteButton.Click += new System.EventHandler(this.DeleteRouteButton_Click);
            // 
            // RouteName
            // 
            this.RouteName.Location = new System.Drawing.Point(523, 16);
            this.RouteName.Name = "RouteName";
            this.RouteName.PlaceholderText = "Route Name";
            this.RouteName.Size = new System.Drawing.Size(121, 23);
            this.RouteName.TabIndex = 9;
            // 
            // SaveRouteButton
            // 
            this.SaveRouteButton.Location = new System.Drawing.Point(650, 15);
            this.SaveRouteButton.Name = "SaveRouteButton";
            this.SaveRouteButton.Size = new System.Drawing.Size(75, 23);
            this.SaveRouteButton.TabIndex = 0;
            this.SaveRouteButton.Text = "Save";
            this.SaveRouteButton.UseVisualStyleBackColor = true;
            this.SaveRouteButton.Click += new System.EventHandler(this.SaveRouteButton_Click);
            // 
            // SelectedRouteComboBox
            // 
            this.SelectedRouteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectedRouteComboBox.FormattingEnabled = true;
            this.SelectedRouteComboBox.Location = new System.Drawing.Point(90, 17);
            this.SelectedRouteComboBox.Name = "SelectedRouteComboBox";
            this.SelectedRouteComboBox.Size = new System.Drawing.Size(121, 23);
            this.SelectedRouteComboBox.TabIndex = 8;
            this.SelectedRouteComboBox.SelectedIndexChanged += new System.EventHandler(this.SelectedRouteComboBox_SelectedIndexChanged);
            this.SelectedRouteComboBox.SelectionChangeCommitted += new System.EventHandler(this.SelectedRouteComboBox_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select Route :";
            // 
            // Map
            // 
            this.Map.ErrorImage = ((System.Drawing.Image)(resources.GetObject("Map.ErrorImage")));
            this.Map.Location = new System.Drawing.Point(6, 48);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(800, 800);
            this.Map.TabIndex = 3;
            this.Map.TabStop = false;
            this.Map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Map_MouseDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.InnHostessAreaButton);
            this.groupBox1.Controls.Add(this.TownActionButton);
            this.groupBox1.Controls.Add(this.SupplyAreaActionButton);
            this.groupBox1.Location = new System.Drawing.Point(822, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 854);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Action List";
            // 
            // InnHostessAreaButton
            // 
            this.InnHostessAreaButton.Location = new System.Drawing.Point(6, 51);
            this.InnHostessAreaButton.Name = "InnHostessAreaButton";
            this.InnHostessAreaButton.Size = new System.Drawing.Size(154, 23);
            this.InnHostessAreaButton.TabIndex = 2;
            this.InnHostessAreaButton.Text = "Inn Hostess";
            this.InnHostessAreaButton.UseVisualStyleBackColor = true;
            this.InnHostessAreaButton.Click += new System.EventHandler(this.InnHostessAreaButton_Click);
            // 
            // TownActionButton
            // 
            this.TownActionButton.Location = new System.Drawing.Point(6, 80);
            this.TownActionButton.Name = "TownActionButton";
            this.TownActionButton.Size = new System.Drawing.Size(154, 23);
            this.TownActionButton.TabIndex = 1;
            this.TownActionButton.Text = "Town";
            this.TownActionButton.UseVisualStyleBackColor = true;
            this.TownActionButton.Click += new System.EventHandler(this.TownActionButton_Click);
            // 
            // SupplyAreaActionButton
            // 
            this.SupplyAreaActionButton.Location = new System.Drawing.Point(6, 22);
            this.SupplyAreaActionButton.Name = "SupplyAreaActionButton";
            this.SupplyAreaActionButton.Size = new System.Drawing.Size(154, 23);
            this.SupplyAreaActionButton.TabIndex = 0;
            this.SupplyAreaActionButton.Text = "Supply";
            this.SupplyAreaActionButton.UseVisualStyleBackColor = true;
            this.SupplyAreaActionButton.Click += new System.EventHandler(this.SupplyAreaActionButton_Click);
            // 
            // RenderTimer
            // 
            this.RenderTimer.Interval = 1;
            this.RenderTimer.Tick += new System.EventHandler(this.RenderTimer_Tick);
            // 
            // RoutePlanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 861);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MapGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RoutePlanner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Route Planner";
            this.Load += new System.EventHandler(this.RoutePlanner_Load);
            this.VisibleChanged += new System.EventHandler(this.RoutePlanner_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RoutePlanner_KeyDown);
            this.MapGroupBox.ResumeLayout(false);
            this.MapGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Map)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private GroupBox MapGroupBox;
    private PictureBox Map;
    private GroupBox groupBox1;
    private Button SupplyAreaActionButton;
    private System.Windows.Forms.Timer RenderTimer;
    private Button TownActionButton;
    private Button InnHostessAreaButton;
    private Button RunRouteButton;
    private Button DeleteRouteButton;
    private TextBox RouteName;
    private Button SaveRouteButton;
    private ComboBox SelectedRouteComboBox;
    private Label label1;
    private Button ResetRouteButton;
}