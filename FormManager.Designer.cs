namespace Online_Self_Ticketing
{
    partial class FormManager
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
            this.lblJadwal = new System.Windows.Forms.DataGridView();
            this.lblLaporan = new System.Windows.Forms.DataGridView();
            this.btnLogout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lblJadwal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLaporan)).BeginInit();
            this.SuspendLayout();
            // 
            // lblJadwal
            // 
            this.lblJadwal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lblJadwal.Location = new System.Drawing.Point(12, 12);
            this.lblJadwal.Name = "lblJadwal";
            this.lblJadwal.RowHeadersWidth = 62;
            this.lblJadwal.RowTemplate.Height = 28;
            this.lblJadwal.Size = new System.Drawing.Size(859, 150);
            this.lblJadwal.TabIndex = 0;
            // 
            // lblLaporan
            // 
            this.lblLaporan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lblLaporan.Location = new System.Drawing.Point(12, 184);
            this.lblLaporan.Name = "lblLaporan";
            this.lblLaporan.RowHeadersWidth = 62;
            this.lblLaporan.RowTemplate.Height = 28;
            this.lblLaporan.Size = new System.Drawing.Size(859, 147);
            this.lblLaporan.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(349, 373);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(105, 35);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // FormManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Online_Self_Ticketing.Properties.Resources.background_tiket;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(917, 450);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblLaporan);
            this.Controls.Add(this.lblJadwal);
            this.Name = "FormManager";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.FormManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lblJadwal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLaporan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView lblJadwal;
        private System.Windows.Forms.DataGridView lblLaporan;
        private System.Windows.Forms.Button btnLogout;
    }
}