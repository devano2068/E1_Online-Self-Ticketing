using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Online_Self_Ticketing
{
    public partial class FormPembayaran : Form
    {
        public string film;
        public string jadwal;
        public string kursi;
        public int harga = 50000;
        public FormPembayaran()
        {
            InitializeComponent();
        }

        private void FormPembayaran_Load(object sender, EventArgs e)
        {
            lblFilm.Text = "Film: " + film;
            lblJadwal.Text = "Jadwal: " + jadwal;
            lblKursi.Text = "Kursi: " + kursi;
            lblHarga.Text = "Harga: Rp " + harga;
        }
        private void btnBayar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pembayaran berhasil!");

            Form1 login = new Form1();
            login.Show();

            this.Close();
        }
    }
}
