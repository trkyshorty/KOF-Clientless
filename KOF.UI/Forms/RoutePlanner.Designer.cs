namespace KOF.UI.Forms;

partial class RoutePlanner {
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoutePlanner));
        MapGroupBox = new GroupBox();
        ResetRouteButton = new Button();
        RunRouteButton = new Button();
        DeleteRouteButton = new Button();
        RouteName = new TextBox();
        SaveRouteButton = new Button();
        SelectedRouteComboBox = new ComboBox();
        label1 = new Label();
        Map = new PictureBox();
        groupBox1 = new GroupBox();
        btnAddEndPosition = new Button();
        MoveCoordinateY = new NumericUpDown();
        label7 = new Label();
        label6 = new Label();
        MoveCoordinateX = new NumericUpDown();
        cbxOnlyPotionBuy = new CheckBox();
        InnHostessAreaButton = new Button();
        TownActionButton = new Button();
        SupplyAreaActionButton = new Button();
        RenderTimer = new System.Windows.Forms.Timer(components);
        MapGroupBox.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)Map).BeginInit();
        groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)MoveCoordinateY).BeginInit();
        ((System.ComponentModel.ISupportInitialize)MoveCoordinateX).BeginInit();
        SuspendLayout();
        // 
        // MapGroupBox
        // 
        MapGroupBox.Controls.Add(ResetRouteButton);
        MapGroupBox.Controls.Add(RunRouteButton);
        MapGroupBox.Controls.Add(DeleteRouteButton);
        MapGroupBox.Controls.Add(RouteName);
        MapGroupBox.Controls.Add(SaveRouteButton);
        MapGroupBox.Controls.Add(SelectedRouteComboBox);
        MapGroupBox.Controls.Add(label1);
        MapGroupBox.Controls.Add(Map);
        MapGroupBox.Location = new Point(3, 3);
        MapGroupBox.Name = "MapGroupBox";
        MapGroupBox.Size = new Size(813, 854);
        MapGroupBox.TabIndex = 5;
        MapGroupBox.TabStop = false;
        MapGroupBox.Text = "Planner";
        // 
        // ResetRouteButton
        // 
        ResetRouteButton.Location = new Point(731, 15);
        ResetRouteButton.Name = "ResetRouteButton";
        ResetRouteButton.Size = new Size(75, 23);
        ResetRouteButton.TabIndex = 12;
        ResetRouteButton.Text = "Reset";
        ResetRouteButton.UseVisualStyleBackColor = true;
        ResetRouteButton.Click += ResetRouteButton_Click;
        // 
        // RunRouteButton
        // 
        RunRouteButton.Location = new Point(217, 16);
        RunRouteButton.Name = "RunRouteButton";
        RunRouteButton.Size = new Size(75, 23);
        RunRouteButton.TabIndex = 11;
        RunRouteButton.Text = "Run";
        RunRouteButton.UseVisualStyleBackColor = true;
        RunRouteButton.Click += RunRouteButton_Click;
        // 
        // DeleteRouteButton
        // 
        DeleteRouteButton.ForeColor = Color.Red;
        DeleteRouteButton.Location = new Point(298, 16);
        DeleteRouteButton.Name = "DeleteRouteButton";
        DeleteRouteButton.Size = new Size(75, 23);
        DeleteRouteButton.TabIndex = 10;
        DeleteRouteButton.Text = "Delete";
        DeleteRouteButton.UseVisualStyleBackColor = true;
        DeleteRouteButton.Click += DeleteRouteButton_Click;
        // 
        // RouteName
        // 
        RouteName.Location = new Point(523, 16);
        RouteName.Name = "RouteName";
        RouteName.PlaceholderText = "Route Name";
        RouteName.Size = new Size(121, 23);
        RouteName.TabIndex = 9;
        // 
        // SaveRouteButton
        // 
        SaveRouteButton.Location = new Point(650, 15);
        SaveRouteButton.Name = "SaveRouteButton";
        SaveRouteButton.Size = new Size(75, 23);
        SaveRouteButton.TabIndex = 0;
        SaveRouteButton.Text = "Save";
        SaveRouteButton.UseVisualStyleBackColor = true;
        SaveRouteButton.Click += SaveRouteButton_Click;
        // 
        // SelectedRouteComboBox
        // 
        SelectedRouteComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        SelectedRouteComboBox.FormattingEnabled = true;
        SelectedRouteComboBox.Location = new Point(90, 17);
        SelectedRouteComboBox.Name = "SelectedRouteComboBox";
        SelectedRouteComboBox.Size = new Size(121, 23);
        SelectedRouteComboBox.TabIndex = 8;
        SelectedRouteComboBox.SelectedIndexChanged += SelectedRouteComboBox_SelectedIndexChanged;
        SelectedRouteComboBox.SelectionChangeCommitted += SelectedRouteComboBox_SelectionChangeCommitted;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(6, 20);
        label1.Name = "label1";
        label1.Size = new Size(78, 15);
        label1.TabIndex = 7;
        label1.Text = "Select Route :";
        // 
        // Map
        // 
        Map.ErrorImage = (Image)resources.GetObject("Map.ErrorImage");
        Map.Location = new Point(6, 48);
        Map.Name = "Map";
        Map.Size = new Size(800, 800);
        Map.TabIndex = 3;
        Map.TabStop = false;
        Map.MouseDown += Map_MouseDown;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(btnAddEndPosition);
        groupBox1.Controls.Add(MoveCoordinateY);
        groupBox1.Controls.Add(label7);
        groupBox1.Controls.Add(label6);
        groupBox1.Controls.Add(MoveCoordinateX);
        groupBox1.Controls.Add(cbxOnlyPotionBuy);
        groupBox1.Controls.Add(InnHostessAreaButton);
        groupBox1.Controls.Add(TownActionButton);
        groupBox1.Controls.Add(SupplyAreaActionButton);
        groupBox1.Location = new Point(822, 3);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(166, 854);
        groupBox1.TabIndex = 6;
        groupBox1.TabStop = false;
        groupBox1.Text = "Action List";
        // 
        // btnAddEndPosition
        // 
        btnAddEndPosition.Location = new Point(6, 756);
        btnAddEndPosition.Name = "btnAddEndPosition";
        btnAddEndPosition.Size = new Size(154, 23);
        btnAddEndPosition.TabIndex = 11;
        btnAddEndPosition.Text = "add last position";
        btnAddEndPosition.UseVisualStyleBackColor = true;
        btnAddEndPosition.Click += btnAddEndPosition_Click;
        // 
        // MoveCoordinateY
        // 
        MoveCoordinateY.Location = new Point(37, 823);
        MoveCoordinateY.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        MoveCoordinateY.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
        MoveCoordinateY.Name = "MoveCoordinateY";
        MoveCoordinateY.Size = new Size(107, 23);
        MoveCoordinateY.TabIndex = 10;
        MoveCoordinateY.TextAlign = HorizontalAlignment.Center;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(3, 828);
        label7.Name = "label7";
        label7.Size = new Size(20, 15);
        label7.TabIndex = 9;
        label7.Text = "Y :";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(3, 799);
        label6.Name = "label6";
        label6.Size = new Size(20, 15);
        label6.TabIndex = 8;
        label6.Text = "X :";
        // 
        // MoveCoordinateX
        // 
        MoveCoordinateX.Location = new Point(37, 796);
        MoveCoordinateX.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        MoveCoordinateX.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
        MoveCoordinateX.Name = "MoveCoordinateX";
        MoveCoordinateX.Size = new Size(107, 23);
        MoveCoordinateX.TabIndex = 7;
        MoveCoordinateX.TextAlign = HorizontalAlignment.Center;
        // 
        // cbxOnlyPotionBuy
        // 
        cbxOnlyPotionBuy.Location = new Point(6, 109);
        cbxOnlyPotionBuy.Name = "cbxOnlyPotionBuy";
        cbxOnlyPotionBuy.Size = new Size(154, 23);
        cbxOnlyPotionBuy.TabIndex = 6;
        cbxOnlyPotionBuy.Text = "Supply (Only Potion)";
        cbxOnlyPotionBuy.TextAlign = ContentAlignment.MiddleCenter;
        cbxOnlyPotionBuy.UseVisualStyleBackColor = true;
        cbxOnlyPotionBuy.CheckedChanged += cbxOnlyPotionBuy_CheckedChanged;
        // 
        // InnHostessAreaButton
        // 
        InnHostessAreaButton.Location = new Point(6, 51);
        InnHostessAreaButton.Name = "InnHostessAreaButton";
        InnHostessAreaButton.Size = new Size(154, 23);
        InnHostessAreaButton.TabIndex = 2;
        InnHostessAreaButton.Text = "Inn Hostess";
        InnHostessAreaButton.UseVisualStyleBackColor = true;
        InnHostessAreaButton.Click += InnHostessAreaButton_Click;
        // 
        // TownActionButton
        // 
        TownActionButton.Location = new Point(6, 80);
        TownActionButton.Name = "TownActionButton";
        TownActionButton.Size = new Size(154, 23);
        TownActionButton.TabIndex = 1;
        TownActionButton.Text = "Town";
        TownActionButton.UseVisualStyleBackColor = true;
        TownActionButton.Click += TownActionButton_Click;
        // 
        // SupplyAreaActionButton
        // 
        SupplyAreaActionButton.Location = new Point(6, 22);
        SupplyAreaActionButton.Name = "SupplyAreaActionButton";
        SupplyAreaActionButton.Size = new Size(154, 23);
        SupplyAreaActionButton.TabIndex = 0;
        SupplyAreaActionButton.Text = "Supply";
        SupplyAreaActionButton.UseVisualStyleBackColor = true;
        SupplyAreaActionButton.Click += SupplyAreaActionButton_Click;
        // 
        // RenderTimer
        // 
        RenderTimer.Interval = 1;
        RenderTimer.Tick += RenderTimer_Tick;
        // 
        // RoutePlanner
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(992, 861);
        Controls.Add(groupBox1);
        Controls.Add(MapGroupBox);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = (Icon)resources.GetObject("$this.Icon");
        KeyPreview = true;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "RoutePlanner";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Route Planner";
        Load += RoutePlanner_Load;
        VisibleChanged += RoutePlanner_VisibleChanged;
        KeyDown += RoutePlanner_KeyDown;
        MapGroupBox.ResumeLayout(false);
        MapGroupBox.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)Map).EndInit();
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)MoveCoordinateY).EndInit();
        ((System.ComponentModel.ISupportInitialize)MoveCoordinateX).EndInit();
        ResumeLayout(false);
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
    private CheckBox cbxOnlyPotionBuy;
    private NumericUpDown MoveCoordinateY;
    private Label label7;
    private Label label6;
    private NumericUpDown MoveCoordinateX;
    private Button btnAddEndPosition;
}