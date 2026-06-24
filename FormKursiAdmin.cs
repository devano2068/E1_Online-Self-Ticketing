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
    public partial class FormKursiAdmin : Form // tambah kursi
    {
        SqlConnection conn = new SqlConnection(
@"Data Source=LAPTOP-BUHABIQL;Initial Catalog=BioskopDB;User ID=sa;Password=vano7474;TrustServerCertificate=True");
        public FormKursiAdmin()
        {
            InitializeComponent();
            ApplyStyling();
        }

        private void ApplyStyling()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // --- TOMBOL SIMPAN ---
            btnTambah.BackColor = Color.FromArgb(180, 140, 80, 0); // emas
            btnTambah.ForeColor = Color.White;
            btnTambah.FlatStyle = FlatStyle.Flat;
            btnTambah.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 160, 0);
            btnTambah.FlatAppearance.BorderSize = 1;
            btnTambah.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 180, 120, 0);
            btnTambah.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 120, 70, 0);
            btnTambah.Font = new Font("Arial", 10, FontStyle.Bold);
            btnTambah.Cursor = Cursors.Hand;
            

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

            // Tombol sejajar tengah dengan jarak rata
            int centerX = this.ClientSize.Width / 2;
            btnTambah.Left = centerX  / 2;
            btnHapus.Left = btnTambah.Right + 20;

            // Samakan tinggi tombol
            btnTambah.Height = 35;
            btnHapus.Height = 35;
            btnTambah.Top = btnHapus.Top;
            btnHapus.Top = btnTambah.Top;
            
        }
        private void FormKursiAdmin_Load(object sender, EventArgs e)
        {

        }
        void LoadKursi()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kursi", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }
        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Kursi (nomor_kursi) VALUES (@n)", conn);

                cmd.Parameters.AddWithValue("@n", txtNomorKursi.Text.Trim());

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Kursi berhasil ditambah!");
                LoadKursi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }
        private void btnHapus_Click(object sender, EventArgs e)
        {
            try
            {
                  int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["kursi_id"].Value);
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Kursi WHERE kursi_id=@id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Kursi dihapus!");
                LoadKursi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }
    }
}
