using ExcelDataReader;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Online_Self_Ticketing
{
    public partial class FormDashboard : Form
    {
        // Hubungan Koneksi Database
        string connString = @"Data Source=LAPTOP-BUHABIQL;Initial Catalog=BioskopDB;User ID=sa;Password=vano7474;TrustServerCertificate=True";
        private DataTable dtExcelStorage = null;
        public FormDashboard()
        {
            InitializeComponent();
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            // Pengaman agar desainer Visual Studio tidak crash
            if (!this.DesignMode)
            {
                LoadListFilm();
                LoadDataGrafik();
            }
        }

        private void LoadListFilm()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT film_id, judul FROM dbo.Film";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbFilm.DataSource = dt;
                    cbFilm.DisplayMember = "judul";
                    cbFilm.ValueMember = "film_id";
                    cbFilm.SelectedIndex = -1;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error Load Film: " + ex.Message); }
        }

        // PERBAIKAN: Menggunakan satu chartBar dan Query JOIN ke dbo.Jadwal karena film_id & studio_id tidak ada di dbo.Tiket
        private void LoadChartData(int filmId, string tipe)
        {
            try
            {
                // 1. Bersihkan chartBar sebelum memuat data baru
                chartBar.Series.Clear();
                chartBar.Titles.Clear();

                // 2. Tentukan Tipe Chart berdasarkan pilihan ComboBox
                Series series = new Series("DataPenjualan");
                if (tipe == "Bar")
                    series.ChartType = SeriesChartType.Column;
                else
                    series.ChartType = SeriesChartType.Pie;

                chartBar.Series.Add(series);

                // 3. Ambil data dengan melakukan INNER JOIN dari Tiket ke Jadwal
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = @"SELECT j.studio_id, COUNT(*) as Jumlah 
                                     FROM dbo.Tiket t 
                                     INNER JOIN dbo.Jadwal j ON t.jadwal_id = j.jadwal_id 
                                     WHERE j.film_id = @fid 
                                     GROUP BY j.studio_id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@fid", filmId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string label = "Studio " + reader["studio_id"].ToString();
                            double nilai = Convert.ToDouble(reader["Jumlah"]);

                            // Masukkan data ke satu grafik tunggal (chartBar)
                            series.Points.AddXY(label, nilai);
                        }
                    }
                }
                chartBar.Titles.Add("Data Penjualan Film per Studio");
            }
            catch (Exception ex) { MessageBox.Show("Gagal Load Grafik: " + ex.Message); }
        }

        private void LoadDataGrafik()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_DashBoardFilm", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                chartBar.Series.Clear();

                if (dt.Rows.Count > 0)
                {
                    var series = chartBar.Series.Add("Penayangan");
                    series.ChartType = SeriesChartType.Column;

                    chartBar.DataSource = dt;
                    series.XValueMember = "JudulFilm";
                    series.YValueMembers = "JumlahJadwal";

                    chartBar.Titles.Clear();
                    chartBar.Titles.Add("Grafik Total Penayangan per Judul Film");
                    chartBar.DataBind();
                }
                else
                {
                    chartBar.Titles.Clear();
                    chartBar.Titles.Add("Belum ada data untuk ditampilkan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat grafik utama: " + ex.Message);
            }
        }

        // Fungsi Tombol Import Excel ke Database
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });

                            // Simpan data Excel ke dalam memori aplikasi
                            dtExcelStorage = result.Tables[0];

                            MessageBox.Show("File Excel berhasil dibaca! Silakan klik tombol 'Import to Database' untuk menyimpan.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi error saat membaca Excel: " + ex.Message);
                }
            }
        }

        private void chart1_Click(object sender, EventArgs e) { }

        // PERBAIKAN: Sinkronisasi pemanggilan LoadChartData dengan 2 parameter (id dan tipe)
        private void button2_Click(object sender, EventArgs e)
        {
            if (cbFilm.SelectedValue == null || cbChartType.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih film dan tipe chart terlebih dahulu!");
                return;
            }

            int selectedFilmId = (int)cbFilm.SelectedValue;
            string tipe = cbChartType.SelectedItem.ToString(); // Mengambil pilihan "Bar" atau "Pie"

            LoadChartData(selectedFilmId, tipe);
        }

        // PERBAIKAN: Membersihkan chartBar dengan aman dan memuat ulang data dashboard awal
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cbFilm.SelectedIndex = -1;
            if (cbChartType != null) cbChartType.SelectedIndex = -1;

            chartBar.DataSource = null;
            chartBar.Series.Clear();
            chartBar.Titles.Clear();

            LoadDataGrafik();
        }

        private void btnImportDatabase_Click(object sender, EventArgs e)
        {
            // Validasi jika user belum memilih file Excel sama sekali
            if (dtExcelStorage == null || dtExcelStorage.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data Excel yang siap disimpan. Silakan pilih file Excel terlebih dahulu!");
                return;
            }

            // Deklarasi koneksi dan transaksi di luar blok try agar bisa dibaca di catch/finally
            SqlConnection conn = new SqlConnection(connString);
            SqlTransaction trans = null;
            int count = 0;

            try
            {
                conn.Open();
                // 1. MEMULAI TRANSAKSI (Sesuai Modul)
                trans = conn.BeginTransaction();

                foreach (DataRow row in dtExcelStorage.Rows)
                {
                    // Lewati jika baris pertama di Excel kosong
                    if (row[0] == DBNull.Value) continue;

                    string query = "INSERT INTO dbo.Film (judul, genre, durasi) VALUES (@judul, @genre, @durasi)";

                    // 2. MASUKKAN KONEKSI DAN TRANSAKSI KE COMMAND (Sesuai Modul)
                    using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@judul", row[0].ToString());
                        cmd.Parameters.AddWithValue("@genre", row[1] != DBNull.Value ? row[1].ToString() : "-");
                        cmd.Parameters.AddWithValue("@durasi", row[2] != DBNull.Value ? Convert.ToInt32(row[2]) : 0);

                        cmd.ExecuteNonQuery();
                        count++;
                    }
                }

                // 3. JIKA SEMUA SUKSES, COMMIT DATA (Sesuai Modul)
                trans.Commit();

                // Kosongkan kembali memori penyimpanan sementara setelah sukses
                dtExcelStorage = null;

                MessageBox.Show(count + " data film berhasil disimpan secara permanen ke Database via Transaction!");

                // Segarkan grafik utama agar langsung update
                LoadDataGrafik();
            }
            catch (SqlException ex)
            {
                // 4. JIKA ERROR DATABASE, BATALKAN SEMUA DATA (Sesuai Modul)
                if (trans != null) trans.Rollback();
                MessageBox.Show("Sql Error (Rollback Berhasil): " + ex.Message);
            }
            catch (Exception ex)
            {
                // 5. JIKA ERROR UMUM, BATALKAN SEMUA DATA (Sesuai Modul)
                if (trans != null) trans.Rollback();
                MessageBox.Show("General Error (Rollback Berhasil): " + ex.Message);
            }
            finally
            {
                // 6. KONEKSI DIJAMIN PASTI DITUTUP (Sesuai Modul)
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            FormMenu menu = new FormMenu();
            menu.Show();

            // Menutup FormDashboard yang aktif saat ini
            this.Close();
        }
    }
}