namespace chessVisualized
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.init = new System.Windows.Forms.Button();
            this.gamestat = new System.Windows.Forms.Label();
            this.load = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.blackscore = new System.Windows.Forms.Label();
            this.whitescore = new System.Windows.Forms.Label();
            this.multiplayer = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(80, 36);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 500);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // init
            // 
            this.init.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.init.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.init.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.init.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.init.Font = new System.Drawing.Font("Segoe UI Emoji", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.init.Location = new System.Drawing.Point(596, 36);
            this.init.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.init.Name = "init";
            this.init.Size = new System.Drawing.Size(74, 23);
            this.init.TabIndex = 1;
            this.init.Text = "New Game";
            this.init.UseVisualStyleBackColor = false;
            this.init.Click += new System.EventHandler(this.init_Click);
            // 
            // gamestat
            // 
            this.gamestat.AutoSize = true;
            this.gamestat.BackColor = System.Drawing.Color.Transparent;
            this.gamestat.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.gamestat.Location = new System.Drawing.Point(594, 20);
            this.gamestat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gamestat.Name = "gamestat";
            this.gamestat.Size = new System.Drawing.Size(0, 13);
            this.gamestat.TabIndex = 2;
            // 
            // load
            // 
            this.load.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.load.Font = new System.Drawing.Font("Segoe UI Emoji", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.load.Location = new System.Drawing.Point(596, 65);
            this.load.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(74, 23);
            this.load.TabIndex = 4;
            this.load.Text = "Load Game";
            this.load.UseVisualStyleBackColor = false;
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.save.Font = new System.Drawing.Font("Segoe UI Emoji", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save.Location = new System.Drawing.Point(596, 94);
            this.save.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(74, 23);
            this.save.TabIndex = 5;
            this.save.Text = "Save Game";
            this.save.UseVisualStyleBackColor = false;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // blackscore
            // 
            this.blackscore.AutoSize = true;
            this.blackscore.BackColor = System.Drawing.Color.Transparent;
            this.blackscore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackscore.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.blackscore.Location = new System.Drawing.Point(38, 36);
            this.blackscore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.blackscore.Name = "blackscore";
            this.blackscore.Size = new System.Drawing.Size(0, 17);
            this.blackscore.TabIndex = 8;
            // 
            // whitescore
            // 
            this.whitescore.AutoSize = true;
            this.whitescore.BackColor = System.Drawing.Color.Transparent;
            this.whitescore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whitescore.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.whitescore.Location = new System.Drawing.Point(38, 523);
            this.whitescore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.whitescore.Name = "whitescore";
            this.whitescore.Size = new System.Drawing.Size(0, 17);
            this.whitescore.TabIndex = 9;
            // 
            // multiplayer
            // 
            this.multiplayer.AutoSize = true;
            this.multiplayer.BackColor = System.Drawing.Color.Transparent;
            this.multiplayer.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.multiplayer.Location = new System.Drawing.Point(584, 511);
            this.multiplayer.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.multiplayer.Name = "multiplayer";
            this.multiplayer.Size = new System.Drawing.Size(63, 17);
            this.multiplayer.TabIndex = 11;
            this.multiplayer.Text = "2 Player";
            this.multiplayer.UseVisualStyleBackColor = false;
            this.multiplayer.CheckedChanged += new System.EventHandler(this.multiplayer_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(684, 581);
            this.Controls.Add(this.multiplayer);
            this.Controls.Add(this.whitescore);
            this.Controls.Add(this.blackscore);
            this.Controls.Add(this.save);
            this.Controls.Add(this.load);
            this.Controls.Add(this.gamestat);
            this.Controls.Add(this.init);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chess";
            this.TransparencyKey = System.Drawing.Color.DarkRed;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button init;
        private System.Windows.Forms.Label gamestat;
        private System.Windows.Forms.Button load;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Label blackscore;
        private System.Windows.Forms.Label whitescore;
        private System.Windows.Forms.RadioButton multiplayer;
    }
}

