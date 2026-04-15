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
        "Data Source=LAPTOP-3UJU5DJS\\VANTAMERAH;Initial Catalog=BioskopDB;Integrated Security=True");

        public FormCustomer()
        {
            InitializeComponent();
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            LoadFilm();
            LoadJadwal();
            LoadKursi();
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