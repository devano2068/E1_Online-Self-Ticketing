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
    public partial class FormManager : Form // form manager lihat aktivitas penjuakan
    {
        SqlConnection conn = new SqlConnection(
            @"Data Source=LAPTOP-BUHABIQL;Initial Catalog=BioskopDB;User ID=sa;Password=vano7474;TrustServerCertificate=True");
        public FormManager()
        {
            InitializeComponent();
        }
        private void ApplyStyling()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // --- LABEL: ganti warna agar terbaca ---
            // Ganti nama label sesuai Designer kamu
            lblJadwal.ForeColor = Color.FromArgb(255, 220, 180, 60); // emas
            lblJadwal.BackColor = Color.FromArgb(30, 30, 30);
            lblJadwal.Font = new Font("Arial", 10, FontStyle.Bold);

            lblLaporan.ForeColor = Color.FromArgb(255, 220, 180, 60);
            lblLaporan.BackColor = Color.FromArgb(30, 30, 30);
            lblLaporan.Font = new Font("Arial", 10, FontStyle.Bold);

            // --- DATAGRIDVIEW 1: Jadwal ---
            StyleDataGridView(lblJadwal);

            // --- DATAGRIDVIEW 2: Laporan Penjualan ---
            StyleDataGridView(lblLaporan);

            // --- TOMBOL LOGOUT ---
            btnLogout.BackColor = Color.FromArgb(160, 140, 30, 30); // merah gelap
            btnLogout.ForeColor = Color.White;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 50, 50);
            btnLogout.FlatAppearance.BorderSize = 1;
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 180, 50, 50);
            btnLogout.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 120, 20, 20);
            btnLogout.Font = new Font("Arial", 10, FontStyle.Bold);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Height = 35;
            btnLogout.Width = 120;

            // --- CENTER tombol logout ---
            btnLogout.Left = (this.ClientSize.Width - btnLogout.Width) / 2;
        }

        // ============================================
        // TAMBAHAN: Fungsi styling DataGridView reusable
        // ============================================
        private void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.FromArgb(30, 30, 30);
            dgv.GridColor = Color.FromArgb(70, 70, 70);
            dgv.BorderStyle = BorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Font = new Font("Arial", 9);

            // Header emas
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(140, 80, 0);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(140, 80, 0);

            // Baris gelap
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 45);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 140, 80, 0);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            // Baris selang-seling
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(55, 55, 55);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
        }
        private void FormManager_Load(object sender, EventArgs e)
        {
            LoadJadwal();
            LoadLaporan();
            ApplyStyling();
        }
        void LoadJadwal()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT Film.judul, Jadwal.tanggal " +
                "FROM Jadwal INNER JOIN Film ON Film.film_id = Jadwal.film_id",
                conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            lblJadwal.DataSource = dt;
        }
        void LoadLaporan()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT Film.judul, " +
                "COUNT(Tiket.tiket_id) AS total_tiket, " +
                "SUM(Tiket.harga) AS total_pendapatan " +
                "FROM Tiket " +
                "INNER JOIN Jadwal ON Tiket.jadwal_id = Jadwal.jadwal_id " +
                "INNER JOIN Film ON Jadwal.film_id = Film.film_id " +
                "WHERE Tiket.status = 'Paid' " +
                "GROUP BY Film.judul",
                conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            lblLaporan.DataSource = dt;
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();

            this.Close();
        }
    }
}
