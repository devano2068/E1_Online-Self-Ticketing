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
    public partial class FormTiket : Form
    {
        SqlConnection conn = new SqlConnection(
            "Data Source=LAPTOP-3UJU5DJS\\VANTAMERAH;Initial Catalog=BioskopDB;Integrated Security=True");
        public FormTiket()
        {
            InitializeComponent();
        }
        private void FormTiket_Load(object sender, EventArgs e)
        {
            LoadFilm();
            LoadJadwal();
            LoadKursi();

            MessageBox.Show("Film dipilih: " + SelectedFilm);
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
        public string SelectedFilm;

        private void btnPesan_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Tiket (jadwal_id, kursi_id, user_id, harga, status) " +
                    "VALUES (@jadwal, @kursi, @user, @harga, @status)", conn);

                cmd.Parameters.AddWithValue("@jadwal", cmbJadwal.SelectedValue);
                cmd.Parameters.AddWithValue("@kursi", cmbKursi.SelectedValue);
                cmd.Parameters.AddWithValue("@user", 1);
                cmd.Parameters.AddWithValue("@harga", 50000);
                cmd.Parameters.AddWithValue("@status", "Booked");

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Tiket berhasil dipesan (belum dibayar)");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

        private void FormTiket_Load_1(object sender, EventArgs e)
        {

        }
        private void btnBayar_Click(object sender, EventArgs e)
        {
            FormPembayaran fp = new FormPembayaran();

            fp.film = cmbFilm.Text;
            fp.jadwal = cmbJadwal.Text;
            fp.kursi = cmbKursi.Text;

            fp.Show();
            this.Hide();
        }
    }
}
