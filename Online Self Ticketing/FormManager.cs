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
    public partial class FormManager : Form
    {
        SqlConnection conn = new SqlConnection(
            "Data Source=LAPTOP-3UJU5DJS\\VANTAMERAH;Initial Catalog=BioskopDB;Integrated Security=True");
        public FormManager()
        {
            InitializeComponent();
        }

        private void FormManager_Load(object sender, EventArgs e)
        {
            LoadJadwal();
            LoadLaporan();
        }
        void LoadJadwal()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT Film.judul, Jadwal.tanggal " +
                "FROM Jadwal INNER JOIN Film ON Film.film_id = Jadwal.film_id",
                conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
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

            dataGridView2.DataSource = dt;
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();

            this.Close();
        }
    }
}
