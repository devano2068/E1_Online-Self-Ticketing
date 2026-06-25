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
    public partial class FormAdmin : Form //form admin ke edit kusi
    {
        SqlConnection conn = new SqlConnection(
        @"Data Source=192.168.110.102;Initial Catalog=BioskopDB;User ID=sa;Password=vano7474");
        private bool _isLoading = false;
        public FormAdmin()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(18, 18, 18); // hitam soft (dark mode)
            ApplyStyling();
        }
        private void ApplyStyling()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // --- DATETIMEPICKER ---
            // --- DATETIMEPICKER: hanya title yang aman di-custom ---
            dtTanggal.Font = new Font("Arial", 10);
            dtTanggal.CalendarTitleBackColor = Color.FromArgb(140, 80, 0);  // emas di header kalender
            dtTanggal.CalendarTitleForeColor = Color.White;
            dtTanggal.CalendarTrailingForeColor = Color.Gray; // tanggal bulan lain
            dtTanggal.MinDate = DateTime.Today.AddMonths(-2);
            dtTanggal.MaxDate = DateTime.Today.AddMonths(2);

            // --- COMBOBOX FILM ---
            cmbFilm.BackColor = Color.FromArgb(60, 60, 60);
            cmbFilm.ForeColor = Color.White;
            cmbFilm.FlatStyle = FlatStyle.Flat;
            cmbFilm.Font = new Font("Arial", 10);

            // --- TOMBOL SIMPAN ---
            btnSimpan.BackColor = Color.FromArgb(180, 140, 80, 0); // emas
            btnSimpan.ForeColor = Color.White;
            btnSimpan.FlatStyle = FlatStyle.Flat;
            btnSimpan.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 160, 0);
            btnSimpan.FlatAppearance.BorderSize = 1;
            btnSimpan.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 180, 120, 0);
            btnSimpan.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 120, 70, 0);
            btnSimpan.Font = new Font("Arial", 10, FontStyle.Bold);
            btnSimpan.Cursor = Cursors.Hand;

            // --- TOMBOL EDIT KURSI ---
            btnKursi.BackColor = Color.FromArgb(60, 80, 140, 180); // biru gelap
            btnKursi.ForeColor = Color.White;
            btnKursi.FlatStyle = FlatStyle.Flat;
            btnKursi.FlatAppearance.BorderColor = Color.FromArgb(150, 100, 140, 200);
            btnKursi.FlatAppearance.BorderSize = 1;
            btnKursi.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, 120, 180, 220);
            btnKursi.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 60, 80, 140);
            btnKursi.Font = new Font("Arial", 10, FontStyle.Bold);
            btnKursi.Cursor = Cursors.Hand;

            // --- TOMBOL HAPUS ---
            btnHapus.BackColor = Color.FromArgb(160, 140, 30, 30); // merah gelap
            btnHapus.ForeColor = Color.White;
            btnHapus.FlatStyle = FlatStyle.Flat;
            btnHapus.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 50, 50);
            btnHapus.FlatAppearance.BorderSize = 1;
            btnHapus.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 180, 50, 50);
            btnHapus.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 120, 20, 20);
            btnHapus.Font = new Font("Arial", 10, FontStyle.Bold);
            btnHapus.Cursor = Cursors.Hand;

            // --- DATAGRIDVIEW ---
            dataGridView1.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridView1.GridColor = Color.FromArgb(70, 70, 70);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);

            // Warna header
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(140, 80, 0);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(140, 80, 0);

            // Warna baris
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 45);
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 140, 80, 0);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            // Warna baris selang-seling
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(55, 55, 55);
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;

            // --- LABEL: sembunyikan dan ganti dengan AddPlaceholderLabel ---
            // Sembunyikan label lama jika ada
            foreach (Control c in this.Controls)
            {
                if (c is Label lbl)
                {
                    lbl.Visible = false;
                }
            }

            // Tambah label emas di atas ComboBox dan DateTimePicker
            AddFloatingLabel("📅  Tanggal Jadwal", dtTanggal);
            AddFloatingLabel("🎬  Pilih Film", cmbFilm);

            // --- BALANCE LAYOUT: rata tengah ---
            int centerX = this.ClientSize.Width / 2;
            dtTanggal.Left = centerX - dtTanggal.Width / 2;
            cmbFilm.Left = centerX - cmbFilm.Width / 2;

            // Tombol sejajar tengah dengan jarak rata
            int totalBtnWidth = btnSimpan.Width + btnKursi.Width + btnHapus.Width + 40;
            btnSimpan.Left = centerX - totalBtnWidth / 2;
            btnKursi.Left = btnSimpan.Right + 20;
            btnHapus.Left = btnKursi.Right + 20;

            // Samakan tinggi tombol
            btnSimpan.Height = 35;
            btnKursi.Height = 35;
            btnHapus.Height = 35;
            btnSimpan.Top = btnKursi.Top;
            btnHapus.Top = btnKursi.Top;
        }

        // ============================================
        // TAMBAHAN: Label emas mengambang di atas kontrol
        // ============================================
        private void AddFloatingLabel(string text, Control targetControl)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.ForeColor = Color.FromArgb(255, 220, 180, 60); // emas
            lbl.BackColor = Color.FromArgb(30, 30, 30);        // gelap solid
            lbl.Font = new Font("Arial", 9, FontStyle.Bold);
            lbl.AutoSize = true;

            // Posisi di sebelah kiri kontrol, vertikal center
            lbl.Left = targetControl.Left - lbl.PreferredWidth - 10;
            lbl.Top = targetControl.Top + (targetControl.Height - lbl.PreferredHeight) / 2;

            this.Controls.Add(lbl);
            lbl.BringToFront();
        }
        private void FormJadwal_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            LoadFilm();
            _isLoading = false;
            LoadJadwal();
            ApplyStyling();
            AturKolomJadwal();
        }

        void AturKolomJadwal()
        {
            if (dataGridView1.Columns.Count == 0) return;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridView1.Columns["ID"].Width = 50;
            dataGridView1.Columns["Judul Film"].Width = 200;
            dataGridView1.Columns["Tanggal"].Width = 120;
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
            if (_isLoading) return; // cegah load ganda

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT Jadwal.jadwal_id AS 'ID', Film.judul AS 'Judul Film', CONVERT(VARCHAR, Jadwal.tanggal, 106) AS 'Tanggal' " +
                "FROM Jadwal INNER JOIN Film ON Jadwal.film_id = Film.film_id",
                conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;

            AturKolomJadwal();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Jadwal (film_id, tanggal) VALUES (@f, @t)", conn);

                cmd.Parameters.AddWithValue("@f", cmbFilm.SelectedValue);
                cmd.Parameters.AddWithValue("@t", dtTanggal.Value);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Jadwal berhasil ditambahkan!");

                LoadJadwal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }
        private void btnKursi_Click(object sender, EventArgs e)
        {
            FormKursiAdmin f = new FormKursiAdmin();
            f.Show();
            this.Hide();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Pilih jadwal terlebih dahulu!");
                    return;
                }

                int jadwalId = Convert.ToInt32(
                    dataGridView1.CurrentRow.Cells["ID"].Value);

                DialogResult result = MessageBox.Show(
                    "Yakin ingin menghapus jadwal ini?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Jadwal WHERE jadwal_id=@id", conn);

                    cmd.Parameters.AddWithValue("@id", jadwalId);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("Jadwal berhasil dihapus!");

                    LoadJadwal(); // refresh data
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi kesalahan database: " + ex.Message);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                conn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
