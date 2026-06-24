namespace Online_Self_Ticketing
{
    partial class FormMenu
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
            this.lblMenuUtama = new System.Windows.Forms.Label();
            this.btnFilm = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnKeDashboard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMenuUtama
            // 
            this.lblMenuUtama.AutoSize = true;
            this.lblMenuUtama.Location = new System.Drawing.Point(269, 32);
            this.lblMenuUtama.Name = "lblMenuUtama";
            this.lblMenuUtama.Size = new System.Drawing.Size(159, 16);
            this.lblMenuUtama.TabIndex = 0;
            this.lblMenuUtama.Text = "MENU UTAMA BIOSKOP";
            // 
            // btnFilm
            // 
            this.btnFilm.Location = new System.Drawing.Point(264, 175);
            this.btnFilm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFilm.Name = "btnFilm";
            this.btnFilm.Size = new System.Drawing.Size(163, 26);
            this.btnFilm.TabIndex = 1;
            this.btnFilm.Text = "Lihat Film";
            this.btnFilm.UseVisualStyleBackColor = true;
            this.btnFilm.Click += new System.EventHandler(this.btnFilm_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(265, 225);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(163, 32);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnKeDashboard
            // 
            this.btnKeDashboard.Location = new System.Drawing.Point(264, 113);
            this.btnKeDashboard.Name = "btnKeDashboard";
            this.btnKeDashboard.Size = new System.Drawing.Size(164, 32);
            this.btnKeDashboard.TabIndex = 3;
            this.btnKeDashboard.Text = "keDashboard";
            this.btnKeDashboard.UseVisualStyleBackColor = true;
            this.btnKeDashboard.Click += new System.EventHandler(this.btnKeDashboard_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Online_Self_Ticketing.Properties.Resources.background_tiket;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(711, 360);
            this.Controls.Add(this.btnKeDashboard);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnFilm);
            this.Controls.Add(this.lblMenuUtama);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMenu";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.FormMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMenuUtama;
        private System.Windows.Forms.Button btnFilm;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnKeDashboard;
    }
}