namespace KOF.UI.Forms
{
    partial class CharacterInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterInfo));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NationalPointTextLabel = new System.Windows.Forms.Label();
            this.ExperienceTextLabel = new System.Windows.Forms.Label();
            this.NationTextLabel = new System.Windows.Forms.Label();
            this.LevelTextLabel = new System.Windows.Forms.Label();
            this.Stat = new System.Windows.Forms.GroupBox();
            this.MpUpButton = new System.Windows.Forms.Button();
            this.IntUpButton = new System.Windows.Forms.Button();
            this.DexUpButton = new System.Windows.Forms.Button();
            this.HpUpButton = new System.Windows.Forms.Button();
            this.StrUpButton = new System.Windows.Forms.Button();
            this.IntTextLabel = new System.Windows.Forms.Label();
            this.MpTextLabel = new System.Windows.Forms.Label();
            this.DexTextLabel = new System.Windows.Forms.Label();
            this.HpTextLabel = new System.Windows.Forms.Label();
            this.StrTextLabel = new System.Windows.Forms.Label();
            this.StatPointTextLabel = new System.Windows.Forms.Label();
            this.MannerTextLabel = new System.Windows.Forms.Label();
            this.DefenseTextLabel = new System.Windows.Forms.Label();
            this.AttackTextLabel = new System.Windows.Forms.Label();
            this.Resistance = new System.Windows.Forms.GroupBox();
            this.PoisonTextLabel = new System.Windows.Forms.Label();
            this.DarkTextLabel = new System.Windows.Forms.Label();
            this.MagicTextLabel = new System.Windows.Forms.Label();
            this.LightningTextLabel = new System.Windows.Forms.Label();
            this.IceTextLabel = new System.Windows.Forms.Label();
            this.FireTextLabel = new System.Windows.Forms.Label();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.Stat.SuspendLayout();
            this.Resistance.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NationalPointTextLabel);
            this.groupBox1.Controls.Add(this.ExperienceTextLabel);
            this.groupBox1.Controls.Add(this.NationTextLabel);
            this.groupBox1.Controls.Add(this.LevelTextLabel);
            this.groupBox1.Location = new System.Drawing.Point(5, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Main";
            // 
            // NationalPointTextLabel
            // 
            this.NationalPointTextLabel.AutoSize = true;
            this.NationalPointTextLabel.Location = new System.Drawing.Point(6, 73);
            this.NationalPointTextLabel.Name = "NationalPointTextLabel";
            this.NationalPointTextLabel.Size = new System.Drawing.Size(89, 15);
            this.NationalPointTextLabel.TabIndex = 3;
            this.NationalPointTextLabel.Text = "National Point :";
            // 
            // ExperienceTextLabel
            // 
            this.ExperienceTextLabel.AutoSize = true;
            this.ExperienceTextLabel.Location = new System.Drawing.Point(6, 47);
            this.ExperienceTextLabel.Name = "ExperienceTextLabel";
            this.ExperienceTextLabel.Size = new System.Drawing.Size(70, 15);
            this.ExperienceTextLabel.TabIndex = 2;
            this.ExperienceTextLabel.Text = "Experience :";
            // 
            // NationTextLabel
            // 
            this.NationTextLabel.AutoSize = true;
            this.NationTextLabel.Location = new System.Drawing.Point(124, 19);
            this.NationTextLabel.Name = "NationTextLabel";
            this.NationTextLabel.Size = new System.Drawing.Size(49, 15);
            this.NationTextLabel.TabIndex = 1;
            this.NationTextLabel.Text = "Nation :";
            // 
            // LevelTextLabel
            // 
            this.LevelTextLabel.AutoSize = true;
            this.LevelTextLabel.Location = new System.Drawing.Point(6, 19);
            this.LevelTextLabel.Name = "LevelTextLabel";
            this.LevelTextLabel.Size = new System.Drawing.Size(40, 15);
            this.LevelTextLabel.TabIndex = 0;
            this.LevelTextLabel.Text = "Level :";
            // 
            // Stat
            // 
            this.Stat.Controls.Add(this.MpUpButton);
            this.Stat.Controls.Add(this.IntUpButton);
            this.Stat.Controls.Add(this.DexUpButton);
            this.Stat.Controls.Add(this.HpUpButton);
            this.Stat.Controls.Add(this.StrUpButton);
            this.Stat.Controls.Add(this.IntTextLabel);
            this.Stat.Controls.Add(this.MpTextLabel);
            this.Stat.Controls.Add(this.DexTextLabel);
            this.Stat.Controls.Add(this.HpTextLabel);
            this.Stat.Controls.Add(this.StrTextLabel);
            this.Stat.Controls.Add(this.StatPointTextLabel);
            this.Stat.Controls.Add(this.MannerTextLabel);
            this.Stat.Controls.Add(this.DefenseTextLabel);
            this.Stat.Controls.Add(this.AttackTextLabel);
            this.Stat.Location = new System.Drawing.Point(5, 118);
            this.Stat.Name = "Stat";
            this.Stat.Size = new System.Drawing.Size(255, 146);
            this.Stat.TabIndex = 1;
            this.Stat.TabStop = false;
            this.Stat.Text = "Stat";
            // 
            // MpUpButton
            // 
            this.MpUpButton.Location = new System.Drawing.Point(216, 117);
            this.MpUpButton.Name = "MpUpButton";
            this.MpUpButton.Size = new System.Drawing.Size(32, 23);
            this.MpUpButton.TabIndex = 16;
            this.MpUpButton.Text = "+";
            this.MpUpButton.UseVisualStyleBackColor = true;
            this.MpUpButton.Click += new System.EventHandler(this.MpUpButton_Click);
            // 
            // IntUpButton
            // 
            this.IntUpButton.Location = new System.Drawing.Point(216, 92);
            this.IntUpButton.Name = "IntUpButton";
            this.IntUpButton.Size = new System.Drawing.Size(32, 23);
            this.IntUpButton.TabIndex = 15;
            this.IntUpButton.Text = "+";
            this.IntUpButton.UseVisualStyleBackColor = true;
            this.IntUpButton.Click += new System.EventHandler(this.IntUpButton_Click);
            // 
            // DexUpButton
            // 
            this.DexUpButton.Location = new System.Drawing.Point(216, 66);
            this.DexUpButton.Name = "DexUpButton";
            this.DexUpButton.Size = new System.Drawing.Size(32, 23);
            this.DexUpButton.TabIndex = 14;
            this.DexUpButton.Text = "+";
            this.DexUpButton.UseVisualStyleBackColor = true;
            this.DexUpButton.Click += new System.EventHandler(this.DexUpButton_Click);
            // 
            // HpUpButton
            // 
            this.HpUpButton.Location = new System.Drawing.Point(216, 41);
            this.HpUpButton.Name = "HpUpButton";
            this.HpUpButton.Size = new System.Drawing.Size(32, 23);
            this.HpUpButton.TabIndex = 13;
            this.HpUpButton.Text = "+";
            this.HpUpButton.UseVisualStyleBackColor = true;
            this.HpUpButton.Click += new System.EventHandler(this.HpUpButton_Click);
            // 
            // StrUpButton
            // 
            this.StrUpButton.Location = new System.Drawing.Point(216, 15);
            this.StrUpButton.Name = "StrUpButton";
            this.StrUpButton.Size = new System.Drawing.Size(32, 23);
            this.StrUpButton.TabIndex = 12;
            this.StrUpButton.Text = "+";
            this.StrUpButton.UseVisualStyleBackColor = true;
            this.StrUpButton.Click += new System.EventHandler(this.StrUpButton_Click);
            // 
            // IntTextLabel
            // 
            this.IntTextLabel.AutoSize = true;
            this.IntTextLabel.Location = new System.Drawing.Point(124, 94);
            this.IntTextLabel.Name = "IntTextLabel";
            this.IntTextLabel.Size = new System.Drawing.Size(31, 15);
            this.IntTextLabel.TabIndex = 11;
            this.IntTextLabel.Text = "INT :";
            // 
            // MpTextLabel
            // 
            this.MpTextLabel.AutoSize = true;
            this.MpTextLabel.Location = new System.Drawing.Point(124, 121);
            this.MpTextLabel.Name = "MpTextLabel";
            this.MpTextLabel.Size = new System.Drawing.Size(31, 15);
            this.MpTextLabel.TabIndex = 10;
            this.MpTextLabel.Text = "MP :";
            // 
            // DexTextLabel
            // 
            this.DexTextLabel.AutoSize = true;
            this.DexTextLabel.Location = new System.Drawing.Point(124, 70);
            this.DexTextLabel.Name = "DexTextLabel";
            this.DexTextLabel.Size = new System.Drawing.Size(34, 15);
            this.DexTextLabel.TabIndex = 9;
            this.DexTextLabel.Text = "DEX :";
            // 
            // HpTextLabel
            // 
            this.HpTextLabel.AutoSize = true;
            this.HpTextLabel.Location = new System.Drawing.Point(124, 45);
            this.HpTextLabel.Name = "HpTextLabel";
            this.HpTextLabel.Size = new System.Drawing.Size(29, 15);
            this.HpTextLabel.TabIndex = 8;
            this.HpTextLabel.Text = "HP :";
            // 
            // StrTextLabel
            // 
            this.StrTextLabel.AutoSize = true;
            this.StrTextLabel.Location = new System.Drawing.Point(124, 19);
            this.StrTextLabel.Name = "StrTextLabel";
            this.StrTextLabel.Size = new System.Drawing.Size(32, 15);
            this.StrTextLabel.TabIndex = 7;
            this.StrTextLabel.Text = "STR :";
            // 
            // StatPointTextLabel
            // 
            this.StatPointTextLabel.AutoSize = true;
            this.StatPointTextLabel.Location = new System.Drawing.Point(7, 94);
            this.StatPointTextLabel.Name = "StatPointTextLabel";
            this.StatPointTextLabel.Size = new System.Drawing.Size(64, 15);
            this.StatPointTextLabel.TabIndex = 6;
            this.StatPointTextLabel.Text = "Stat Point :";
            // 
            // MannerTextLabel
            // 
            this.MannerTextLabel.AutoSize = true;
            this.MannerTextLabel.Location = new System.Drawing.Point(6, 70);
            this.MannerTextLabel.Name = "MannerTextLabel";
            this.MannerTextLabel.Size = new System.Drawing.Size(54, 15);
            this.MannerTextLabel.TabIndex = 5;
            this.MannerTextLabel.Text = "Manner :";
            // 
            // DefenseTextLabel
            // 
            this.DefenseTextLabel.AutoSize = true;
            this.DefenseTextLabel.Location = new System.Drawing.Point(6, 45);
            this.DefenseTextLabel.Name = "DefenseTextLabel";
            this.DefenseTextLabel.Size = new System.Drawing.Size(55, 15);
            this.DefenseTextLabel.TabIndex = 4;
            this.DefenseTextLabel.Text = "Defense :";
            // 
            // AttackTextLabel
            // 
            this.AttackTextLabel.AutoSize = true;
            this.AttackTextLabel.Location = new System.Drawing.Point(6, 19);
            this.AttackTextLabel.Name = "AttackTextLabel";
            this.AttackTextLabel.Size = new System.Drawing.Size(47, 15);
            this.AttackTextLabel.TabIndex = 3;
            this.AttackTextLabel.Text = "Attack :";
            // 
            // Resistance
            // 
            this.Resistance.Controls.Add(this.PoisonTextLabel);
            this.Resistance.Controls.Add(this.DarkTextLabel);
            this.Resistance.Controls.Add(this.MagicTextLabel);
            this.Resistance.Controls.Add(this.LightningTextLabel);
            this.Resistance.Controls.Add(this.IceTextLabel);
            this.Resistance.Controls.Add(this.FireTextLabel);
            this.Resistance.Location = new System.Drawing.Point(5, 270);
            this.Resistance.Name = "Resistance";
            this.Resistance.Size = new System.Drawing.Size(255, 100);
            this.Resistance.TabIndex = 2;
            this.Resistance.TabStop = false;
            this.Resistance.Text = "Resistance";
            // 
            // PoisonTextLabel
            // 
            this.PoisonTextLabel.AutoSize = true;
            this.PoisonTextLabel.Location = new System.Drawing.Point(124, 69);
            this.PoisonTextLabel.Name = "PoisonTextLabel";
            this.PoisonTextLabel.Size = new System.Drawing.Size(52, 15);
            this.PoisonTextLabel.TabIndex = 9;
            this.PoisonTextLabel.Text = "Poison  :";
            // 
            // DarkTextLabel
            // 
            this.DarkTextLabel.AutoSize = true;
            this.DarkTextLabel.Location = new System.Drawing.Point(124, 43);
            this.DarkTextLabel.Name = "DarkTextLabel";
            this.DarkTextLabel.Size = new System.Drawing.Size(40, 15);
            this.DarkTextLabel.TabIndex = 8;
            this.DarkTextLabel.Text = "Dark  :";
            // 
            // MagicTextLabel
            // 
            this.MagicTextLabel.AutoSize = true;
            this.MagicTextLabel.Location = new System.Drawing.Point(124, 19);
            this.MagicTextLabel.Name = "MagicTextLabel";
            this.MagicTextLabel.Size = new System.Drawing.Size(46, 15);
            this.MagicTextLabel.TabIndex = 7;
            this.MagicTextLabel.Text = "Magic :";
            // 
            // LightningTextLabel
            // 
            this.LightningTextLabel.AutoSize = true;
            this.LightningTextLabel.Location = new System.Drawing.Point(6, 69);
            this.LightningTextLabel.Name = "LightningTextLabel";
            this.LightningTextLabel.Size = new System.Drawing.Size(64, 15);
            this.LightningTextLabel.TabIndex = 6;
            this.LightningTextLabel.Text = "Lightning :";
            // 
            // IceTextLabel
            // 
            this.IceTextLabel.AutoSize = true;
            this.IceTextLabel.Location = new System.Drawing.Point(6, 43);
            this.IceTextLabel.Name = "IceTextLabel";
            this.IceTextLabel.Size = new System.Drawing.Size(28, 15);
            this.IceTextLabel.TabIndex = 5;
            this.IceTextLabel.Text = "Ice :";
            // 
            // FireTextLabel
            // 
            this.FireTextLabel.AutoSize = true;
            this.FireTextLabel.Location = new System.Drawing.Point(6, 19);
            this.FireTextLabel.Name = "FireTextLabel";
            this.FireTextLabel.Size = new System.Drawing.Size(32, 15);
            this.FireTextLabel.TabIndex = 4;
            this.FireTextLabel.Text = "Fire :";
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // CharacterInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 374);
            this.Controls.Add(this.Resistance);
            this.Controls.Add(this.Stat);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CharacterInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Character Info";
            this.Load += new System.EventHandler(this.CharacterInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Stat.ResumeLayout(false);
            this.Stat.PerformLayout();
            this.Resistance.ResumeLayout(false);
            this.Resistance.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private Label NationalPointTextLabel;
        private Label ExperienceTextLabel;
        private Label NationTextLabel;
        private Label LevelTextLabel;
        private GroupBox Stat;
        private Button MpUpButton;
        private Button IntUpButton;
        private Button DexUpButton;
        private Button HpUpButton;
        private Button StrUpButton;
        private Label IntTextLabel;
        private Label MpTextLabel;
        private Label DexTextLabel;
        private Label HpTextLabel;
        private Label StrTextLabel;
        private Label StatPointTextLabel;
        private Label MannerTextLabel;
        private Label DefenseTextLabel;
        private Label AttackTextLabel;
        private GroupBox Resistance;
        private Label PoisonTextLabel;
        private Label DarkTextLabel;
        private Label MagicTextLabel;
        private Label LightningTextLabel;
        private Label IceTextLabel;
        private Label FireTextLabel;
        private System.Windows.Forms.Timer UpdateTimer;
    }
}