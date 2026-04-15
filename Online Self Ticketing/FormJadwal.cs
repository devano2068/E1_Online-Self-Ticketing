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
    public partial class FormJadwal : Form
    {
        SqlConnection conn = new SqlConnection(
        "Data Source=LAPTOP-3UJU5DJS\\VANTAMERAH;Initial Catalog=BioskopDB;Integrated Security=True");
        public FormJadwal()
        {
            InitializeComponent();
        }
        private void FormJadwal_Load(object sender, EventArgs e)
        {
            LoadFilm();
            LoadJadwal();
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
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT Jadwal.jadwal_id, Film.judul, Jadwal.tanggal " +
                "FROM Jadwal INNER JOIN Film ON Jadwal.film_id = Film.film_id",
                conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
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
    }
}
