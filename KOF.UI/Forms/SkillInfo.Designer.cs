namespace KOF.UI.Forms
{
    partial class SkillInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillInfo));
            this.Skill1Label = new System.Windows.Forms.Label();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.Skill2Label = new System.Windows.Forms.Label();
            this.Skill3Label = new System.Windows.Forms.Label();
            this.MasterLabel = new System.Windows.Forms.Label();
            this.Skill1UpButton = new System.Windows.Forms.Button();
            this.Skill2UpButton = new System.Windows.Forms.Button();
            this.Skill3UpButton = new System.Windows.Forms.Button();
            this.MasterUpButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Skill1StJobButton = new System.Windows.Forms.Button();
            this.SkillPointLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Skill1Label
            // 
            this.Skill1Label.AutoSize = true;
            this.Skill1Label.Location = new System.Drawing.Point(6, 47);
            this.Skill1Label.Name = "Skill1Label";
            this.Skill1Label.Size = new System.Drawing.Size(40, 15);
            this.Skill1Label.TabIndex = 0;
            this.Skill1Label.Text = "Skill1 :";
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // Skill2Label
            // 
            this.Skill2Label.AutoSize = true;
            this.Skill2Label.Location = new System.Drawing.Point(6, 76);
            this.Skill2Label.Name = "Skill2Label";
            this.Skill2Label.Size = new System.Drawing.Size(40, 15);
            this.Skill2Label.TabIndex = 1;
            this.Skill2Label.Text = "Skill2 :";
            // 
            // Skill3Label
            // 
            this.Skill3Label.AutoSize = true;
            this.Skill3Label.Location = new System.Drawing.Point(6, 106);
            this.Skill3Label.Name = "Skill3Label";
            this.Skill3Label.Size = new System.Drawing.Size(40, 15);
            this.Skill3Label.TabIndex = 2;
            this.Skill3Label.Text = "Skill3 :";
            // 
            // MasterLabel
            // 
            this.MasterLabel.AutoSize = true;
            this.MasterLabel.Location = new System.Drawing.Point(6, 133);
            this.MasterLabel.Name = "MasterLabel";
            this.MasterLabel.Size = new System.Drawing.Size(49, 15);
            this.MasterLabel.TabIndex = 3;
            this.MasterLabel.Text = "Master :";
            // 
            // Skill1UpButton
            // 
            this.Skill1UpButton.Location = new System.Drawing.Point(204, 43);
            this.Skill1UpButton.Name = "Skill1UpButton";
            this.Skill1UpButton.Size = new System.Drawing.Size(49, 23);
            this.Skill1UpButton.TabIndex = 4;
            this.Skill1UpButton.Text = "+";
            this.Skill1UpButton.UseVisualStyleBackColor = true;
            this.Skill1UpButton.Click += new System.EventHandler(this.Skill1UpButton_Click);
            // 
            // Skill2UpButton
            // 
            this.Skill2UpButton.Location = new System.Drawing.Point(204, 72);
            this.Skill2UpButton.Name = "Skill2UpButton";
            this.Skill2UpButton.Size = new System.Drawing.Size(49, 23);
            this.Skill2UpButton.TabIndex = 5;
            this.Skill2UpButton.Text = "+";
            this.Skill2UpButton.UseVisualStyleBackColor = true;
            this.Skill2UpButton.Click += new System.EventHandler(this.Skill2UpButton_Click);
            // 
            // Skill3UpButton
            // 
            this.Skill3UpButton.Location = new System.Drawing.Point(204, 102);
            this.Skill3UpButton.Name = "Skill3UpButton";
            this.Skill3UpButton.Size = new System.Drawing.Size(49, 23);
            this.Skill3UpButton.TabIndex = 6;
            this.Skill3UpButton.Text = "+";
            this.Skill3UpButton.UseVisualStyleBackColor = true;
            this.Skill3UpButton.Click += new System.EventHandler(this.Skill3UpButton_Click);
            // 
            // MasterUpButton
            // 
            this.MasterUpButton.Location = new System.Drawing.Point(204, 131);
            this.MasterUpButton.Name = "MasterUpButton";
            this.MasterUpButton.Size = new System.Drawing.Size(49, 23);
            this.MasterUpButton.TabIndex = 7;
            this.MasterUpButton.Text = "+";
            this.MasterUpButton.UseVisualStyleBackColor = true;
            this.MasterUpButton.Click += new System.EventHandler(this.MasterUpButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Skill1StJobButton);
            this.groupBox1.Controls.Add(this.SkillPointLabel);
            this.groupBox1.Controls.Add(this.MasterUpButton);
            this.groupBox1.Controls.Add(this.Skill1Label);
            this.groupBox1.Controls.Add(this.Skill3UpButton);
            this.groupBox1.Controls.Add(this.Skill2Label);
            this.groupBox1.Controls.Add(this.Skill2UpButton);
            this.groupBox1.Controls.Add(this.Skill3Label);
            this.groupBox1.Controls.Add(this.Skill1UpButton);
            this.groupBox1.Controls.Add(this.MasterLabel);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 190);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Skill";
            // 
            // Skill1StJobButton
            // 
            this.Skill1StJobButton.Location = new System.Drawing.Point(75, 157);
            this.Skill1StJobButton.Name = "Skill1StJobButton";
            this.Skill1StJobButton.Size = new System.Drawing.Size(90, 23);
            this.Skill1StJobButton.TabIndex = 8;
            this.Skill1StJobButton.Text = "1. St Job";
            this.Skill1StJobButton.UseVisualStyleBackColor = true;
            this.Skill1StJobButton.Click += new System.EventHandler(this.Skill1StJobButton_Click);
            // 
            // SkillPointLabel
            // 
            this.SkillPointLabel.AutoSize = true;
            this.SkillPointLabel.Location = new System.Drawing.Point(6, 19);
            this.SkillPointLabel.Name = "SkillPointLabel";
            this.SkillPointLabel.Size = new System.Drawing.Size(41, 15);
            this.SkillPointLabel.TabIndex = 1;
            this.SkillPointLabel.Text = "Point :";
            // 
            // SkillInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 195);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SkillInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Skill Info";
            this.Load += new System.EventHandler(this.SkillInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label Skill1Label;
        private System.Windows.Forms.Timer UpdateTimer;
        private Label Skill2Label;
        private Label Skill3Label;
        private Label MasterLabel;
        private Button Skill1UpButton;
        private Button Skill2UpButton;
        private Button Skill3UpButton;
        private Button MasterUpButton;
        private GroupBox groupBox1;
        private Label SkillPointLabel;
        private Button Skill1StJobButton;
    }
}