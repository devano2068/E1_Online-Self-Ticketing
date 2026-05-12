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
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
            ApplyStyling();
        }
        private void ApplyStyling()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // --- LABEL MENU UTAMA ---
            lblMenuUtama.ForeColor = Color.FromArgb(255, 220, 180, 60); // emas
            lblMenuUtama.BackColor = Color.FromArgb(30, 30, 30);        // gelap solid
            lblMenuUtama.Font = new Font("Arial", 14, FontStyle.Bold);

            // --- TOMBOL LIHAT FILM ---
            btnFilm.BackColor = Color.FromArgb(180, 140, 80, 0); // emas
            btnFilm.ForeColor = Color.White;
            btnFilm.FlatStyle = FlatStyle.Flat;
            btnFilm.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 160, 0);
            btnFilm.FlatAppearance.BorderSize = 1;
            btnFilm.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 180, 120, 0);
            btnFilm.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 120, 70, 0);
            btnFilm.Font = new Font("Arial", 11, FontStyle.Bold);
            btnFilm.Cursor = Cursors.Hand;
            btnFilm.Height = 40;

            // --- TOMBOL LOGOUT ---
            btnLogout.BackColor = Color.FromArgb(160, 140, 30, 30); // merah gelap
            btnLogout.ForeColor = Color.White;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 50, 50);
            btnLogout.FlatAppearance.BorderSize = 1;
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 180, 50, 50);
            btnLogout.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 120, 20, 20);
            btnLogout.Font = new Font("Arial", 11, FontStyle.Bold);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Height = 40;

            // --- SEJAJARKAN TOMBOL: lebar sama, tengah horizontal ---
            int centerX = this.ClientSize.Width / 2;
            btnFilm.Width = 200;
            btnLogout.Width = 200;
            btnFilm.Left = centerX - btnFilm.Width / 2;
            btnLogout.Left = centerX - btnLogout.Width / 2;

            // --- LABEL CENTER ---
            lblMenuUtama.Left = centerX - lblMenuUtama.Width / 2;
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
    }
}
