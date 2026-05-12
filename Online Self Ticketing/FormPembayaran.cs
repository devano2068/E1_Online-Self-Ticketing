using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Online_Self_Ticketing
{
    public partial class FormPembayaran : Form
    {
        public string film;
        public string jadwal;
        public string kursi;
        public int harga = 50000;
        public FormPembayaran()
        {
            InitializeComponent();
        }
        private void ApplyStyling()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // --- PANEL INFO: buat kotak gelap transparan di belakang label ---
            // Tambahkan Panel di Designer, bungkus semua label di dalamnya
            // Nama panel: panelInfo

            // --- LABEL FILM ---
            lblFilm.ForeColor = Color.White;
            lblFilm.BackColor = Color.Transparent;
            lblFilm.Font = new Font("Arial", 11, FontStyle.Bold);

            // --- LABEL JADWAL ---
            lblJadwal.ForeColor = Color.FromArgb(220, 220, 220);
            lblJadwal.BackColor = Color.Transparent;
            lblJadwal.Font = new Font("Arial", 10);

            // --- LABEL KURSI ---
            lblKursi.ForeColor = Color.FromArgb(220, 220, 220);
            lblKursi.BackColor = Color.Transparent;
            lblKursi.Font = new Font("Arial", 10);

            // --- LABEL HARGA ---
            lblHarga.ForeColor = Color.FromArgb(255, 220, 180, 60); // emas
            lblHarga.BackColor = Color.Transparent;
            lblHarga.Font = new Font("Arial", 12, FontStyle.Bold);

            // --- TOMBOL BAYAR ---
            btnBayar.BackColor = Color.FromArgb(180, 140, 80, 0); // emas
            btnBayar.ForeColor = Color.White;
            btnBayar.FlatStyle = FlatStyle.Flat;
            btnBayar.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 160, 0);
            btnBayar.FlatAppearance.BorderSize = 1;
            btnBayar.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 180, 120, 0);
            btnBayar.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 120, 70, 0);
            btnBayar.Font = new Font("Arial", 11, FontStyle.Bold);
            btnBayar.Cursor = Cursors.Hand;
            btnBayar.Height = 40;
            btnBayar.Width = 160;

            // --- CENTER semua elemen ---
            int centerX = this.ClientSize.Width / 2;
            btnBayar.Left = centerX - btnBayar.Width / 2;
        }

        private void FormPembayaran_Load(object sender, EventArgs e)
        {
            lblFilm.Text = "Film: " + film;
            lblJadwal.Text = "Jadwal: " + jadwal;
            lblKursi.Text = "Kursi: " + kursi;
            lblHarga.Text = "Harga: Rp " + harga;
            ApplyStyling();
        }
        private void btnBayar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pembayaran berhasil!");

            Login login = new Login();
            login.Show();

            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
