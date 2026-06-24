using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Online_Self_Ticketing
{
    public partial class FormCustomer : Form
    {
        SqlConnection conn = new SqlConnection(
        @"Data Source=LAPTOP-BUHABIQL;Initial Catalog=BioskopDB;User ID=sa;Password=vano7474;TrustServerCertificate=True");

        public FormCustomer()
        {
            InitializeComponent();
            ApplyStyling();
        }

        private void ApplyStyling()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // --- SEMBUNYIKAN LABEL (sama seperti Login) ---
            lblJudul.Visible = false;
            lblJadwal.Visible = false;
            lblKursi.Visible = false;

            // --- COMBOBOX FILM (Judul) ---
            cmbFilm.BackColor = Color.FromArgb(60, 60, 60);
            cmbFilm.ForeColor = Color.White;
            cmbFilm.FlatStyle = FlatStyle.Flat;
            cmbFilm.Font = new Font("Arial", 10);

            // --- COMBOBOX JADWAL ---
            cmbJadwal.BackColor = Color.FromArgb(60, 60, 60);
            cmbJadwal.ForeColor = Color.White;
            cmbJadwal.FlatStyle = FlatStyle.Flat;
            cmbJadwal.Font = new Font("Arial", 10);

            // --- COMBOBOX KURSI ---
            cmbKursi.BackColor = Color.FromArgb(60, 60, 60);
            cmbKursi.ForeColor = Color.White;
            cmbKursi.FlatStyle = FlatStyle.Flat;
            cmbKursi.Font = new Font("Arial", 10);

            // --- TOMBOL PESAN TIKET ---
            btnPesan.BackColor = Color.FromArgb(180, 140, 80, 0); // emas sama seperti Login
            btnPesan.ForeColor = Color.White;
            btnPesan.FlatStyle = FlatStyle.Flat;
            btnPesan.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 160, 0);
            btnPesan.FlatAppearance.BorderSize = 1;
            btnPesan.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 180, 120, 0);
            btnPesan.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 120, 70, 0);
            btnPesan.Font = new Font("Arial", 10, FontStyle.Bold);
            btnPesan.Cursor = Cursors.Hand;

            // --- TAMBAH LABEL PLACEHOLDER di atas setiap ComboBox ---
            // Karena label tidak bisa transparan, kita buat Label baru
            // yang ditempel langsung di atas ComboBox dengan warna solid gelap
            AddPlaceholderLabel("🎬  Pilih Film", cmbFilm);
            AddPlaceholderLabel("📅  Pilih Jadwal", cmbJadwal);
            AddPlaceholderLabel("💺  Pilih Kursi", cmbKursi);

            // --- BALANCE LAYOUT: rata tengah ---
            int centerX = this.ClientSize.Width / 2;
            cmbFilm.Left = centerX - cmbFilm.Width / 2;
            cmbJadwal.Left = centerX - cmbJadwal.Width / 2;
            cmbKursi.Left = centerX - cmbKursi.Width / 2;
            btnPesan.Left = centerX - btnPesan.Width / 2;
        }

        // ============================================
        // TAMBAHAN: Fungsi buat label emas di atas ComboBox
        // ============================================
        private void AddPlaceholderLabel(string text, Control targetControl)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.ForeColor = Color.FromArgb(255, 220, 180, 60); // warna emas
            lbl.BackColor = Color.FromArgb(30, 30, 30);        // gelap solid agar terbaca
            lbl.Font = new Font("Arial", 9, FontStyle.Bold);
            lbl.AutoSize = true;

            // Posisi: di atas ComboBox
            lbl.Left = targetControl.Left - lbl.PreferredWidth - 10;
            lbl.Top = targetControl.Top + (targetControl.Height - lbl.PreferredHeight) / 2;

            this.Controls.Add(lbl);
            lbl.BringToFront();
        }
        private void FormCustomer_Load(object sender, EventArgs e)
        {
            LoadFilm();
            LoadJadwal();
            LoadKursi();
            ApplyStyling();
        }

        void LoadFilm()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Film", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbFilm.DisplayMember = "judul";
            cmbFilm.ValueMember = "film_id";
            cmbFilm.DataSource = dt;
        }

        void LoadJadwal()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Jadwal", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbJadwal.DisplayMember = "tanggal";
            cmbJadwal.ValueMember = "jadwal_id";
            cmbJadwal.DataSource = dt;
        }

        void LoadKursi()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kursi", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbKursi.DisplayMember = "nomor_kursi";
            cmbKursi.ValueMember = "kursi_id";
            cmbKursi.DataSource = dt;
        }

        private void btnPesan_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Tiket (jadwal_id, kursi_id, user_id, harga, status) " +
                    "VALUES (@j, @k, @u, @h, @s)", conn);

                cmd.Parameters.AddWithValue("@j", cmbJadwal.SelectedValue);
                cmd.Parameters.AddWithValue("@k", cmbKursi.SelectedValue);
                cmd.Parameters.AddWithValue("@u", 1); // user login sementara
                cmd.Parameters.AddWithValue("@h", 50000);
                cmd.Parameters.AddWithValue("@s", "Booked");

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Tiket berhasil dipesan!");

                // LANJUT KE PEMBAYARAN
                FormPembayaran fp = new FormPembayaran();

                fp.film = cmbFilm.Text;
                fp.jadwal = cmbJadwal.Text;
                fp.kursi = cmbKursi.Text;
                fp.harga = 50000;

                fp.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                conn.Close();
            }
        }
    }
}