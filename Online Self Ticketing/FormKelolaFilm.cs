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
    public partial class FormKelolaFilm : Form
    {
        SqlConnection conn = new SqlConnection(
"Data Source=LAPTOP-3UJU5DJS\\VANTAMERAH;Initial Catalog=BioskopDB;Integrated Security=True");
        public FormKelolaFilm()
        {
            InitializeComponent();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void LoadFilm()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Film", conn);
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
                    "INSERT INTO Film (judul, genre, durasi) VALUES (@j, @g, @d)", conn);

                cmd.Parameters.AddWithValue("@j", txtJudul.Text);
                cmd.Parameters.AddWithValue("@g", txtGenre.Text);
                cmd.Parameters.AddWithValue("@d", txtDurasi.Text);

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Film berhasil ditambahkan!");

                LoadFilm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadFilm();
        }
        private void FormKelolaFilm_Load(object sender, EventArgs e)
        {
            LoadFilm();
        }
    }
}
