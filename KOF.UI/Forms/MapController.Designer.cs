namespace KOF.UI.Forms;

partial class MapController {
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapController));
        MapGroupBox = new GroupBox();
        RunRouteButton = new Button();
        SelectedRouteComboBox = new ComboBox();
        label1 = new Label();
        Map = new PictureBox();
        groupBox1 = new GroupBox();
        TargetCheckedListBox = new CheckedListBox();
        RenderTimer = new System.Windows.Forms.Timer(components);
        groupBox2 = new GroupBox();
        PlayerListBox = new ListBox();
        MapGroupBox.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)Map).BeginInit();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        SuspendLayout();
        // 
        // MapGroupBox
        // 
        MapGroupBox.Controls.Add(RunRouteButton);
        MapGroupBox.Controls.Add(SelectedRouteComboBox);
        MapGroupBox.Controls.Add(label1);
        MapGroupBox.Controls.Add(Map);
        MapGroupBox.Location = new Point(3, 3);
        MapGroupBox.Name = "MapGroupBox";
        MapGroupBox.Size = new Size(813, 854);
        MapGroupBox.TabIndex = 5;
        MapGroupBox.TabStop = false;
        MapGroupBox.Text = "Map";
        // 
        // RunRouteButton
        // 
        RunRouteButton.Location = new Point(183, 16);
        RunRouteButton.Name = "RunRouteButton";
        RunRouteButton.Size = new Size(75, 23);
        RunRouteButton.TabIndex = 11;
        RunRouteButton.Text = "Run";
        RunRouteButton.UseVisualStyleBackColor = true;
        RunRouteButton.Click += RunRouteButton_Click;
        // 
        // SelectedRouteComboBox
        // 
        SelectedRouteComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        SelectedRouteComboBox.FormattingEnabled = true;
        SelectedRouteComboBox.Location = new Point(56, 17);
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
        label1.Size = new Size(44, 15);
        label1.TabIndex = 7;
        label1.Text = "Route :";
        // 
        // Map
        // 
        Map.ErrorImage = (Image)resources.GetObject("Map.ErrorImage");
        Map.Location = new Point(6, 48);
        Map.Margin = new Padding(0);
        Map.Name = "Map";
        Map.Size = new Size(800, 800);
        Map.TabIndex = 3;
        Map.TabStop = false;
        Map.MouseDown += Map_MouseDown;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(TargetCheckedListBox);
        groupBox1.Location = new Point(822, 3);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(166, 425);
        groupBox1.TabIndex = 6;
        groupBox1.TabStop = false;
        groupBox1.Text = "Enemy List";
        // 
        // TargetCheckedListBox
        // 
        TargetCheckedListBox.FormattingEnabled = true;
        TargetCheckedListBox.Location = new Point(3, 19);
        TargetCheckedListBox.Name = "TargetCheckedListBox";
        TargetCheckedListBox.Size = new Size(157, 400);
        TargetCheckedListBox.TabIndex = 0;
        TargetCheckedListBox.ItemCheck += TargetCheckedListBox_ItemCheck;
        // 
        // RenderTimer
        // 
        RenderTimer.Interval = 1;
        RenderTimer.Tick += RenderTimer_Tick;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(PlayerListBox);
        groupBox2.Location = new Point(822, 428);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(166, 429);
        groupBox2.TabIndex = 7;
        groupBox2.TabStop = false;
        groupBox2.Text = "Player List";
        // 
        // PlayerListBox
        // 
        PlayerListBox.FormattingEnabled = true;
        PlayerListBox.ItemHeight = 15;
        PlayerListBox.Location = new Point(3, 19);
        PlayerListBox.Name = "PlayerListBox";
        PlayerListBox.Size = new Size(157, 409);
        PlayerListBox.TabIndex = 0;
        // 
        // MapController
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(992, 861);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Controls.Add(MapGroupBox);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = (Icon)resources.GetObject("$this.Icon");
        KeyPreview = true;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "MapController";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Map Controller";
        Load += RoutePlanner_Load;
        VisibleChanged += MapController_VisibleChanged;
        KeyDown += RoutePlanner_KeyDown;
        MapGroupBox.ResumeLayout(false);
        MapGroupBox.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)Map).EndInit();
        groupBox1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private GroupBox MapGroupBox;
    private PictureBox Map;
    private GroupBox groupBox1;
    private System.Windows.Forms.Timer RenderTimer;
    private Button RunRouteButton;
    private ComboBox SelectedRouteComboBox;
    private Label label1;
    private CheckedListBox TargetCheckedListBox;
    private GroupBox groupBox2;
    private ListBox PlayerListBox;
}