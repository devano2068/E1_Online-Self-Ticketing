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
    public partial class FormMenu : Form// form menu untuk admin
    {
        public FormMenu()
        {
            InitializeComponent();
            ApplyStyling();
        }

        private void ApplyStyling()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;

            int centerX = this.ClientSize.Width / 2;

            // --- LABEL MENU UTAMA ---
            lblMenuUtama.ForeColor = Color.FromArgb(255, 220, 180, 60);
            lblMenuUtama.BackColor = Color.FromArgb(30, 30, 30);
            lblMenuUtama.Font = new Font("Arial", 14, FontStyle.Bold);
            lblMenuUtama.Left = centerX - lblMenuUtama.Width / 2;

            // --- TOMBOL LIHAT FILM ---
            btnFilm.BackColor = Color.FromArgb(180, 140, 80, 0);
            btnFilm.ForeColor = Color.White;
            btnFilm.FlatStyle = FlatStyle.Flat;
            btnFilm.Width = 200;
            btnFilm.Height = 40;
            btnFilm.Left = centerX - btnFilm.Width / 2;

            // --- TOMBOL LOGOUT ---
            btnLogout.BackColor = Color.FromArgb(160, 140, 30, 30);
            btnLogout.ForeColor = Color.White;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Width = 200;
            btnLogout.Height = 40;
            btnLogout.Left = centerX - btnLogout.Width / 2;

            // --- TAMBAHAN: BUAT TOMBOL CETAK LAPORAN ---
            Button btnCetakBaru = new Button();
            btnCetakBaru.Name = "btnCetakBaru";
            btnCetakBaru.Text = "Cetak Laporan";
            btnCetakBaru.BackColor = Color.FromArgb(60, 140, 80);
            btnCetakBaru.ForeColor = Color.White;
            btnCetakBaru.FlatStyle = FlatStyle.Flat;
            btnCetakBaru.Font = new Font("Arial", 11, FontStyle.Bold);
            btnCetakBaru.Width = 200;
            btnCetakBaru.Height = 40;
            btnCetakBaru.Cursor = Cursors.Hand;

            //-----
            btnKeDashboard.BackColor = Color.FromArgb(200, 150, 40, 160); // Warna ungu/magenta gelap elegan (atau ganti ke emas jika mau)
            btnKeDashboard.ForeColor = Color.White;
            btnKeDashboard.FlatStyle = FlatStyle.Flat;
            btnKeDashboard.Font = new Font("Arial", 11, FontStyle.Bold);
            btnKeDashboard.Width = 200;
            btnKeDashboard.Height = 40;
            btnKeDashboard.Left = centerX - btnKeDashboard.Width / 2;
            btnKeDashboard.Cursor = Cursors.Hand;

            // Posisi tombol (di bawah tombol Logout)
            btnCetakBaru.Left = centerX - btnCetakBaru.Width / 2;
            btnCetakBaru.Top = btnLogout.Bottom + 10;

            // Hubungkan ke method button1_Click
            btnCetakBaru.Click += new EventHandler(button1_Click);

            // Tambahkan ke Form
            this.Controls.Add(btnCetakBaru);
            btnCetakBaru.BringToFront();
        }

        private void btnFilm_Click(object sender, EventArgs e)
        {
            FormFilm f = new FormFilm();
            f.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            if (Login.userRole != "Admin")
            {
                MessageBox.Show("Akses ditolak!");
                this.Close();
            }
        }

        // Method ini akan dipanggil saat tombol Cetak Laporan diklik
        private void button1_Click(object sender, EventArgs e)
        {
            FormCetak f = new FormCetak();
            f.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnKeDashboard_Click(object sender, EventArgs e)
        {
            FormDashboard dashboard = new FormDashboard();
            dashboard.Show();

            // Menyembunyikan FormMenu agar layar tidak penuh (atau gunakan this.Close() jika ingin ditutup)
            this.Hide();
        }
    }
}