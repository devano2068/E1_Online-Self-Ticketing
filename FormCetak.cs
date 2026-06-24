using System;
using System.Data;
using System.Data.SqlClient; // Penting untuk koneksi
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine; // Penting untuk Crystal Report

namespace Online_Self_Ticketing
{
    public partial class FormCetak : Form // cetak rtp
    {
        // 1. Ganti string ini dengan Connection String database kamu
        string connString = @"Data Source=LAPTOP-BUHABIQL;Initial Catalog=BioskopDB;User ID=sa;Password=vano7474;TrustServerCertificate=True";

        public FormCetak()
        {
            InitializeComponent();
            this.Load += FormCetak_Load_Final;
        }

        private void FormCetak_Load_Final(object sender, EventArgs e)
        {
            try
            {
                // 1. PANGGIL DATASET ASLI: Menggunakan DataSetReport.xsd yang ada di project kamu
                DataSetReport ds = new DataSetReport();

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    // QUERY DIPERBAIKI: Menggunakan INNER JOIN agar mengambil judul berbentuk teks dari tabel Film
                    // N.B. Jika nama kolom judul di tabel Film kamu bukan 'judul' (misal: 'nama_film' atau 'judul_film'), 
                    // silakan ganti tulisan 'f.judul' di bawah sesuai nama kolom di database kamu.
                    string query = @"SELECT j.jadwal_id AS ID, f.judul AS JudulFilm, j.tanggal AS Tanggal 
                                     FROM dbo.Jadwal j 
                                     INNER JOIN dbo.Film f ON j.film_id = f.film_id";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);

                    // 2. ISI LANGSUNG KE TABEL ASLINYA: ds.dtLaporanJadwal
                    da.Fill(ds.dtLaporanJadwal);
                }

                // Cek apakah data berhasil masuk ke tabel DataSet
                if (ds.dtLaporanJadwal.Rows.Count == 0)
                {
                    MessageBox.Show("Tidak ada data di tabel Jadwal!");
                    return;
                }

                // Load file report
                ReportDocument rd = new ReportDocument();
                string path = Application.StartupPath + "\\CrystalReport1.rpt";
                rd.Load(path);

                // 3. Kirim DataSet asli yang sudah terisi data ke report
                rd.SetDataSource(ds);

                // Tampilkan ke layar
                crystalReportViewer1.ReportSource = rd;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}