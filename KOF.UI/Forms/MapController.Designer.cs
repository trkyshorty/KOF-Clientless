namespace KOF.UI.Forms;

partial class MapController
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapController));
            this.MapGroupBox = new System.Windows.Forms.GroupBox();
            this.RunRouteButton = new System.Windows.Forms.Button();
            this.SelectedRouteComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Map = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TargetCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.RenderTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PlayerListBox = new System.Windows.Forms.ListBox();
            this.MapGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Map)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MapGroupBox
            // 
            this.MapGroupBox.Controls.Add(this.RunRouteButton);
            this.MapGroupBox.Controls.Add(this.SelectedRouteComboBox);
            this.MapGroupBox.Controls.Add(this.label1);
            this.MapGroupBox.Controls.Add(this.Map);
            this.MapGroupBox.Location = new System.Drawing.Point(3, 3);
            this.MapGroupBox.Name = "MapGroupBox";
            this.MapGroupBox.Size = new System.Drawing.Size(813, 854);
            this.MapGroupBox.TabIndex = 5;
            this.MapGroupBox.TabStop = false;
            this.MapGroupBox.Text = "Map";
            // 
            // RunRouteButton
            // 
            this.RunRouteButton.Location = new System.Drawing.Point(183, 16);
            this.RunRouteButton.Name = "RunRouteButton";
            this.RunRouteButton.Size = new System.Drawing.Size(75, 23);
            this.RunRouteButton.TabIndex = 11;
            this.RunRouteButton.Text = "Run";
            this.RunRouteButton.UseVisualStyleBackColor = true;
            this.RunRouteButton.Click += new System.EventHandler(this.RunRouteButton_Click);
            // 
            // SelectedRouteComboBox
            // 
            this.SelectedRouteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectedRouteComboBox.FormattingEnabled = true;
            this.SelectedRouteComboBox.Location = new System.Drawing.Point(56, 17);
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
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Route :";
            // 
            // Map
            // 
            this.Map.ErrorImage = ((System.Drawing.Image)(resources.GetObject("Map.ErrorImage")));
            this.Map.Location = new System.Drawing.Point(6, 48);
            this.Map.Margin = new System.Windows.Forms.Padding(0);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(800, 800);
            this.Map.TabIndex = 3;
            this.Map.TabStop = false;
            this.Map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Map_MouseDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TargetCheckedListBox);
            this.groupBox1.Location = new System.Drawing.Point(822, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 425);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enemy List";
            // 
            // TargetCheckedListBox
            // 
            this.TargetCheckedListBox.FormattingEnabled = true;
            this.TargetCheckedListBox.Location = new System.Drawing.Point(3, 19);
            this.TargetCheckedListBox.Name = "TargetCheckedListBox";
            this.TargetCheckedListBox.Size = new System.Drawing.Size(157, 400);
            this.TargetCheckedListBox.TabIndex = 0;
            this.TargetCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.TargetCheckedListBox_ItemCheck);
            // 
            // RenderTimer
            // 
            this.RenderTimer.Interval = 1;
            this.RenderTimer.Tick += new System.EventHandler(this.RenderTimer_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PlayerListBox);
            this.groupBox2.Location = new System.Drawing.Point(822, 428);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 429);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Player List";
            // 
            // PlayerListBox
            // 
            this.PlayerListBox.FormattingEnabled = true;
            this.PlayerListBox.ItemHeight = 15;
            this.PlayerListBox.Location = new System.Drawing.Point(3, 19);
            this.PlayerListBox.Name = "PlayerListBox";
            this.PlayerListBox.Size = new System.Drawing.Size(157, 409);
            this.PlayerListBox.TabIndex = 0;
            // 
            // MapController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 861);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MapGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapController";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map Controller";
            this.Load += new System.EventHandler(this.RoutePlanner_Load);
            this.VisibleChanged += new System.EventHandler(this.MapController_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RoutePlanner_KeyDown);
            this.MapGroupBox.ResumeLayout(false);
            this.MapGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Map)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

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