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
    public partial class FormKursiAdmin : Form
    {
        SqlConnection conn = new SqlConnection(
"Data Source=LAPTOP-3UJU5DJS\\VANTAMERAH;Initial Catalog=BioskopDB;Integrated Security=True");
        public FormKursiAdmin()
        {
            InitializeComponent();
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
