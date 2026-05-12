namespace Online_Self_Ticketing
{
    partial class FormCustomer
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
            this.cmbFilm = new System.Windows.Forms.ComboBox();
            this.cmbJadwal = new System.Windows.Forms.ComboBox();
            this.cmbKursi = new System.Windows.Forms.ComboBox();
            this.btnPesan = new System.Windows.Forms.Button();
            this.lblJudul = new System.Windows.Forms.Label();
            this.lblJadwal = new System.Windows.Forms.Label();
            this.lblKursi = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbFilm
            // 
            this.cmbFilm.FormattingEnabled = true;
            this.cmbFilm.Location = new System.Drawing.Point(284, 106);
            this.cmbFilm.Name = "cmbFilm";
            this.cmbFilm.Size = new System.Drawing.Size(182, 28);
            this.cmbFilm.TabIndex = 0;
            // 
            // cmbJadwal
            // 
            this.cmbJadwal.FormattingEnabled = true;
            this.cmbJadwal.Location = new System.Drawing.Point(284, 184);
            this.cmbJadwal.Name = "cmbJadwal";
            this.cmbJadwal.Size = new System.Drawing.Size(182, 28);
            this.cmbJadwal.TabIndex = 1;
            // 
            // cmbKursi
            // 
            this.cmbKursi.FormattingEnabled = true;
            this.cmbKursi.Location = new System.Drawing.Point(284, 265);
            this.cmbKursi.Name = "cmbKursi";
            this.cmbKursi.Size = new System.Drawing.Size(182, 28);
            this.cmbKursi.TabIndex = 2;
            // 
            // btnPesan
            // 
            this.btnPesan.Location = new System.Drawing.Point(305, 356);
            this.btnPesan.Name = "btnPesan";
            this.btnPesan.Size = new System.Drawing.Size(111, 50);
            this.btnPesan.TabIndex = 3;
            this.btnPesan.Text = "Pesan Tiket";
            this.btnPesan.UseVisualStyleBackColor = true;
            this.btnPesan.Click += new System.EventHandler(this.btnPesan_Click);
            // 
            // lblJudul
            // 
            this.lblJudul.AutoSize = true;
            this.lblJudul.Location = new System.Drawing.Point(200, 109);
            this.lblJudul.Name = "lblJudul";
            this.lblJudul.Size = new System.Drawing.Size(47, 20);
            this.lblJudul.TabIndex = 4;
            this.lblJudul.Text = "Judul";
            // 
            // lblJadwal
            // 
            this.lblJadwal.AutoSize = true;
            this.lblJadwal.Location = new System.Drawing.Point(200, 187);
            this.lblJadwal.Name = "lblJadwal";
            this.lblJadwal.Size = new System.Drawing.Size(58, 20);
            this.lblJadwal.TabIndex = 5;
            this.lblJadwal.Text = "Jadwal";
            // 
            // lblKursi
            // 
            this.lblKursi.AutoSize = true;
            this.lblKursi.Location = new System.Drawing.Point(203, 268);
            this.lblKursi.Name = "lblKursi";
            this.lblKursi.Size = new System.Drawing.Size(44, 20);
            this.lblKursi.TabIndex = 6;
            this.lblKursi.Text = "Kursi";
            // 
            // FormCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Online_Self_Ticketing.Properties.Resources.background_tiket;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblKursi);
            this.Controls.Add(this.lblJadwal);
            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.btnPesan);
            this.Controls.Add(this.cmbKursi);
            this.Controls.Add(this.cmbJadwal);
            this.Controls.Add(this.cmbFilm);
            this.Name = "FormCustomer";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.FormCustomer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbFilm;
        private System.Windows.Forms.ComboBox cmbJadwal;
        private System.Windows.Forms.ComboBox cmbKursi;
        private System.Windows.Forms.Button btnPesan;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Label lblJadwal;
        private System.Windows.Forms.Label lblKursi;
    }
}