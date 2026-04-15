namespace Online_Self_Ticketing
{
    partial class FormPembayaran
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
            this.lblFilm = new System.Windows.Forms.Label();
            this.lblJadwal = new System.Windows.Forms.Label();
            this.lblKursi = new System.Windows.Forms.Label();
            this.lblHarga = new System.Windows.Forms.Label();
            this.btnBayar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFilm
            // 
            this.lblFilm.AutoSize = true;
            this.lblFilm.Location = new System.Drawing.Point(236, 103);
            this.lblFilm.Name = "lblFilm";
            this.lblFilm.Size = new System.Drawing.Size(53, 20);
            this.lblFilm.TabIndex = 0;
            this.lblFilm.Text = "lblFilm";
            // 
            // lblJadwal
            // 
            this.lblJadwal.AutoSize = true;
            this.lblJadwal.Location = new System.Drawing.Point(236, 154);
            this.lblJadwal.Name = "lblJadwal";
            this.lblJadwal.Size = new System.Drawing.Size(73, 20);
            this.lblJadwal.TabIndex = 1;
            this.lblJadwal.Text = "lblJadwal";
            // 
            // lblKursi
            // 
            this.lblKursi.AutoSize = true;
            this.lblKursi.Location = new System.Drawing.Point(236, 201);
            this.lblKursi.Name = "lblKursi";
            this.lblKursi.Size = new System.Drawing.Size(59, 20);
            this.lblKursi.TabIndex = 2;
            this.lblKursi.Text = "lblKursi";
            // 
            // lblHarga
            // 
            this.lblHarga.AutoSize = true;
            this.lblHarga.Location = new System.Drawing.Point(236, 249);
            this.lblHarga.Name = "lblHarga";
            this.lblHarga.Size = new System.Drawing.Size(68, 20);
            this.lblHarga.TabIndex = 3;
            this.lblHarga.Text = "lblHarga";
            // 
            // btnBayar
            // 
            this.btnBayar.Location = new System.Drawing.Point(273, 314);
            this.btnBayar.Name = "btnBayar";
            this.btnBayar.Size = new System.Drawing.Size(202, 53);
            this.btnBayar.TabIndex = 4;
            this.btnBayar.Text = "Bayar";
            this.btnBayar.UseVisualStyleBackColor = true;
            this.btnBayar.Click += new System.EventHandler(this.btnBayar_Click);
            // 
            // FormPembayaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBayar);
            this.Controls.Add(this.lblHarga);
            this.Controls.Add(this.lblKursi);
            this.Controls.Add(this.lblJadwal);
            this.Controls.Add(this.lblFilm);
            this.Name = "FormPembayaran";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.FormPembayaran_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFilm;
        private System.Windows.Forms.Label lblJadwal;
        private System.Windows.Forms.Label lblKursi;
        private System.Windows.Forms.Label lblHarga;
        private System.Windows.Forms.Button btnBayar;
    }
}