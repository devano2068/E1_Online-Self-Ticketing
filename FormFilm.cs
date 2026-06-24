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

using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Online_Self_Ticketing
{
    public partial class FormFilm : Form // tambah film
    {
        SqlConnection conn = new SqlConnection(
            @"Data Source=LAPTOP-BUHABIQL;Initial Catalog=BioskopDB;User ID=sa;Password=vano7474;TrustServerCertificate=True");

        // Untuk Binding
        private DataTable dtFilm = new DataTable();
        private BindingSource bindingSource = new BindingSource();
        private int selectedFilmId = -1;

        public FormFilm()
        {
            InitializeComponent();
        }

        private void ApplyStyling()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;

            txtJudul.BackColor = Color.FromArgb(60, 60, 60);
            txtJudul.ForeColor = Color.White;
            txtJudul.BorderStyle = BorderStyle.FixedSingle;
            txtJudul.Font = new Font("Arial", 10);

            txtGenre.BackColor = Color.FromArgb(60, 60, 60);
            txtGenre.ForeColor = Color.White;
            txtGenre.BorderStyle = BorderStyle.FixedSingle;
            txtGenre.Font = new Font("Arial", 10);

            txtDurasi.BackColor = Color.FromArgb(60, 60, 60);
            txtDurasi.ForeColor = Color.White;
            txtDurasi.BorderStyle = BorderStyle.FixedSingle;
            txtDurasi.Font = new Font("Arial", 10);

            lblJudul.ForeColor = Color.FromArgb(255, 220, 180, 60);
            lblJudul.BackColor = Color.FromArgb(30, 30, 30);
            lblJudul.Font = new Font("Arial", 9, FontStyle.Bold);

            lblGenre.ForeColor = Color.FromArgb(255, 220, 180, 60);
            lblGenre.BackColor = Color.FromArgb(30, 30, 30);
            lblGenre.Font = new Font("Arial", 9, FontStyle.Bold);

            lblDurasi.ForeColor = Color.FromArgb(255, 220, 180, 60);
            lblDurasi.BackColor = Color.FromArgb(30, 30, 30);
            lblDurasi.Font = new Font("Arial", 9, FontStyle.Bold);

            btnSimpan.BackColor = Color.FromArgb(180, 140, 80, 0);
            btnSimpan.ForeColor = Color.White;
            btnSimpan.FlatStyle = FlatStyle.Flat;
            btnSimpan.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 160, 0);
            btnSimpan.FlatAppearance.BorderSize = 1;
            btnSimpan.Font = new Font("Arial", 10, FontStyle.Bold);
            btnSimpan.Cursor = Cursors.Hand;
            btnSimpan.Height = 35;

            btnHapus.BackColor = Color.FromArgb(160, 140, 30, 30);
            btnHapus.ForeColor = Color.White;
            btnHapus.FlatStyle = FlatStyle.Flat;
            btnHapus.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 50, 50);
            btnHapus.FlatAppearance.BorderSize = 1;
            btnHapus.Font = new Font("Arial", 10, FontStyle.Bold);
            btnHapus.Cursor = Cursors.Hand;
            btnHapus.Height = 35;

            btnJadwal.BackColor = Color.FromArgb(60, 80, 140, 180);
            btnJadwal.ForeColor = Color.White;
            btnJadwal.FlatStyle = FlatStyle.Flat;
            btnJadwal.FlatAppearance.BorderColor = Color.FromArgb(150, 100, 140, 200);
            btnJadwal.FlatAppearance.BorderSize = 1;
            btnJadwal.Font = new Font("Arial", 10, FontStyle.Bold);
            btnJadwal.Cursor = Cursors.Hand;
            btnJadwal.Height = 35;

            dataGridView1.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridView1.GridColor = Color.FromArgb(70, 70, 70);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(140, 80, 0);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 45);
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 140, 80, 0);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(55, 55, 55);
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
        }

        private void FormFilm_Load(object sender, EventArgs e)
        {
            LoadFilm();
            SetupBinding();
            ApplyStyling();
            AturKolomFilm();

        }
        void AturKolomFilm()
        {
            if (dataGridView1.Columns.Count == 0) return;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridView1.Columns["film_id"].Width = 60;
            dataGridView1.Columns["judul"].Width = 180;
            dataGridView1.Columns["genre"].Width = 120;
            dataGridView1.Columns["durasi"].Width = 80;
        }
        // =============================================
        // LOAD DATA menggunakan VIEW
        // =============================================
        void LoadFilm()
        {
            try
            {
                dtFilm.Clear();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_SemuaFilm", conn);
                da.Fill(dtFilm);

                bindingSource.DataSource = dtFilm;
                dataGridView1.DataSource = bindingSource;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Load Film: " + ex.Message);
            }
        }

        // =============================================
        // SETUP BINDING TextBox ke BindingSource
        // =============================================
        void SetupBinding()
        {
            txtJudul.DataBindings.Clear();
            txtGenre.DataBindings.Clear();
            txtDurasi.DataBindings.Clear();

            txtJudul.DataBindings.Add("Text", bindingSource, "judul", true);
            txtGenre.DataBindings.Add("Text", bindingSource, "genre", true);
            txtDurasi.DataBindings.Add("Text", bindingSource, "durasi", true);

            // Binding Navigator ke BindingSource
            bindingNavigator1.BindingSource = bindingSource;
        }

        // =============================================
        // SIMPAN - menggunakan STORED PROCEDURE
        // =============================================
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtJudul.Text == "" || txtGenre.Text == "" || txtDurasi.Text == "")
            {
                MessageBox.Show("Semua field harus diisi!");
                return;
            }

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_InsertFilm", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@judul", txtJudul.Text.Trim());
                cmd.Parameters.AddWithValue("@genre", txtGenre.Text.Trim());
                cmd.Parameters.AddWithValue("@durasi", int.Parse(txtDurasi.Text.Trim()));

                SqlParameter output = new SqlParameter("@result", SqlDbType.NVarChar, 100);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show(output.Value.ToString());
                LoadFilm();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                conn.Close();
            }
        }

        // =============================================
        // HAPUS - menggunakan STORED PROCEDURE
        // =============================================
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Pilih film dulu!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Yakin ingin menghapus film ini?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                int filmId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["film_id"].Value);

                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_DeleteFilm", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@film_id", filmId);

                SqlParameter output = new SqlParameter("@result", SqlDbType.NVarChar, 100);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery();
                conn.Close();

                string pesan = output.Value.ToString();

                if (pesan.Contains("tidak dapat dihapus"))
                {
                    MessageBox.Show(pesan, "Tidak Dapat Dihapus",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(pesan, "Berhasil",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadFilm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Delete: " + ex.Message);
                conn.Close();
            }
        }

        // =============================================
        // KLIK ROW -> isi TextBox otomatis
        // =============================================
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedFilmId = Convert.ToInt32(row.Cells["film_id"].Value);
                txtJudul.Text = row.Cells["judul"].Value?.ToString();
                txtGenre.Text = row.Cells["genre"].Value?.ToString();
                txtDurasi.Text = row.Cells["durasi"].Value?.ToString();
            }
        }

        private void btnJadwal_Click(object sender, EventArgs e)
        {
            FormAdmin f = new FormAdmin();
            f.Show();
            this.Hide();
        }

        private void txtJudul_TextChanged(object sender, EventArgs e) { }

        void ClearForm()
        {
            txtJudul.Text = "";
            txtGenre.Text = "";
            txtDurasi.Text = "";
            selectedFilmId = -1;
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedFilmId == -1)
            {
                MessageBox.Show("Pilih film dulu dari tabel!");
                return;
            }

            if (txtJudul.Text == "" || txtGenre.Text == "" || txtDurasi.Text == "")
            {
                MessageBox.Show("Semua field harus diisi!");
                return;
            }

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_UpdateFilm", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@film_id", selectedFilmId);
                cmd.Parameters.AddWithValue("@judul", txtJudul.Text.Trim());
                cmd.Parameters.AddWithValue("@genre", txtGenre.Text.Trim());
                cmd.Parameters.AddWithValue("@durasi", int.Parse(txtDurasi.Text.Trim()));

                SqlParameter output = new SqlParameter("@result", SqlDbType.NVarChar, 100);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show(output.Value.ToString());
                LoadFilm();
                ClearForm();
                selectedFilmId = -1;
            }
            catch (FormatException)
            {
                MessageBox.Show("Durasi harus berupa angka!");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Update: " + ex.Message);
                conn.Close();
            }
        }
    }
}