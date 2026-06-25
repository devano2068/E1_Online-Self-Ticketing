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
    public partial class FormRegister : Form // 13
    {
        SqlConnection conn = new SqlConnection(
@"Data Source=192.168.110.102;Initial Catalog=BioskopDB;User ID=sa;Password=vano7474");
        public FormRegister()
        {
            InitializeComponent();
            ApplyStyling();
        }
        private void ApplyStyling()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // --- SEMBUNYIKAN LABEL LAMA --
            lblNama.Visible = false;
            lblEmail.Visible = false;
            lblPassword.Visible = false;

            // --- TEXTBOX NAMA: placeholder ---
            txtNama.BackColor = Color.FromArgb(60, 60, 60);
            txtNama.ForeColor = Color.FromArgb(160, 160, 160);
            txtNama.BorderStyle = BorderStyle.FixedSingle;
            txtNama.Font = new Font("Arial", 10);
            txtNama.Text = "👤  Nama Lengkap";

            txtNama.Enter += (s, ev) => {
                if (txtNama.Text == "👤  Nama Lengkap")
                {
                    txtNama.Text = "";
                    txtNama.ForeColor = Color.White;
                }
            };
            txtNama.Leave += (s, ev) => {
                if (txtNama.Text == "")
                {
                    txtNama.Text = "👤  Nama Lengkap";
                    txtNama.ForeColor = Color.FromArgb(160, 160, 160);
                }
            };

            // --- TEXTBOX EMAIL: placeholder ---
            txtEmail.BackColor = Color.FromArgb(60, 60, 60);
            txtEmail.ForeColor = Color.FromArgb(160, 160, 160);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Arial", 10);
            txtEmail.Text = "✉  Email";

            txtEmail.Enter += (s, ev) => {
                if (txtEmail.Text == "✉  Email")
                {
                    txtEmail.Text = "";
                    txtEmail.ForeColor = Color.White;
                }
            };
            txtEmail.Leave += (s, ev) => {
                if (txtEmail.Text == "")
                {
                    txtEmail.Text = "✉  Email";
                    txtEmail.ForeColor = Color.FromArgb(160, 160, 160);
                }
            };

            // --- TEXTBOX PASSWORD: placeholder ---
            txtPassword.BackColor = Color.FromArgb(60, 60, 60);
            txtPassword.ForeColor = Color.FromArgb(160, 160, 160);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Arial", 10);
            txtPassword.Text = "🔒  Password";
            txtPassword.PasswordChar = '\0'; // matikan dulu saat placeholder

            txtPassword.Enter += (s, ev) => {
                if (txtPassword.Text == "🔒  Password")
                {
                    txtPassword.Text = "";
                    txtPassword.ForeColor = Color.White;
                    txtPassword.PasswordChar = '*'; // aktifkan bintang
                }
            };
            txtPassword.Leave += (s, ev) => {
                if (txtPassword.Text == "")
                {
                    txtPassword.Text = "🔒  Password";
                    txtPassword.ForeColor = Color.FromArgb(160, 160, 160);
                    txtPassword.PasswordChar = '\0'; // matikan bintang
                }
            };

            // --- TOMBOL REGISTRASI ---
            btnRegister.BackColor = Color.FromArgb(180, 140, 80, 0); // emas
            btnRegister.ForeColor = Color.White;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 160, 0);
            btnRegister.FlatAppearance.BorderSize = 1;
            btnRegister.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 180, 120, 0);
            btnRegister.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 120, 70, 0);
            btnRegister.Font = new Font("Arial", 11, FontStyle.Bold);
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.Height = 40;
            btnRegister.Width = 160;

            // --- CENTER semua elemen ---
            int centerX = this.ClientSize.Width / 2;
            txtNama.Left = centerX - txtNama.Width / 2;
            txtEmail.Left = centerX - txtEmail.Width / 2;
            txtPassword.Left = centerX - txtPassword.Width / 2;
            btnRegister.Left = centerX - btnRegister.Width / 2;
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtNama.Text == "" || txtNama.Text == "👤  Nama Lengkap" ||
        txtEmail.Text == "" || txtEmail.Text == "✉  Email" ||
        txtPassword.Text == "" || txtPassword.Text == "🔒  Password")
            {
                MessageBox.Show("Semua field harus diisi!");
                return;
            }

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_InsertUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nama", txtNama.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@role", "Customer");

                SqlParameter output = new SqlParameter("@result", SqlDbType.NVarChar, 100);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery();
                conn.Close();

                string result = output.Value.ToString();
                MessageBox.Show(result);

                if (result.StartsWith("BERHASIL"))
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
