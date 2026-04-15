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

    public partial class FormFilm : Form
    {
        SqlConnection conn = new SqlConnection(
            "Data Source=LAPTOP-3UJU5DJS\\VANTAMERAH;Initial Catalog=BioskopDB;Integrated Security=True");
        public FormFilm()
        {
            InitializeComponent();
            MessageBox.Show("FORM TERBUKA");
        }

        private void FormFilm_Load(object sender, EventArgs e)
        {
            LoadFilm();
        }
        DataTable TampilFilm()
        {
            DataTable dt = new DataTable();

            try
            {
                MessageBox.Show("SEBELUM CONNECT");

                conn.Open();

                MessageBox.Show("SETELAH CONNECT");

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Film", conn);
                da.Fill(dt);

                MessageBox.Show("DATA MASUK DT: " + dt.Rows.Count);

                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }

            return dt;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string judulFilm = dataGridView1.Rows[e.RowIndex].Cells["judul"].Value.ToString();

                FormTiket f = new FormTiket();
                f.SelectedFilm = judulFilm;
                f.Show();

                this.Hide();
            }
        }

        private void txtJudul_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Film (judul, genre, durasi) VALUES (@j, @g, @d)", conn);

                cmd.Parameters.AddWithValue("@j", txtJudul.Text.Trim());
                cmd.Parameters.AddWithValue("@g", txtGenre.Text.Trim());
                cmd.Parameters.AddWithValue("@d", txtDurasi.Text.Trim());

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Film berhasil ditambahkan!");

                LoadFilm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                conn.Close();
            }
        }

        void LoadFilm()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Film", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Load Film: " + ex.Message);
            }
        }
        private void btnHapus_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Pilih film dulu!");
                    return;
                }

                int filmId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["film_id"].Value);

                DialogResult result = MessageBox.Show(
                    "Yakin ingin menghapus film ini?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Film WHERE film_id=@id", conn);

                    cmd.Parameters.AddWithValue("@id", filmId);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("Film berhasil dihapus!");

                    LoadFilm(); // refresh tabel
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Delete: " + ex.Message);
                conn.Close();
            }
        }
        private void btnJadwal_Click(object sender, EventArgs e)
        {
            FormJadwal f = new FormJadwal();
            f.Show();
            this.Hide();
        }
    }
}
