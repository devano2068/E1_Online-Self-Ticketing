using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Online_Self_Ticketing
{
    public partial class Login : Form
    {
        SqlConnection conn = new SqlConnection(
            @"Data Source=LAPTOP-BUHABIQL;Initial Catalog=BioskopDB;User ID=sa;Password=vano7474;TrustServerCertificate=True");

        public static string userRole;

        public Login()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            this.AcceptButton = btnLogin;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            ApplyStyling();
        }

        private void ApplyStyling()
        {
            lblEmail.Visible = false;
            lblPassword.Visible = false;

            txtEmail.BackColor = Color.FromArgb(60, 60, 60);
            txtEmail.ForeColor = Color.FromArgb(180, 180, 180);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Arial", 10);
            txtEmail.Text = "✉  Email";

            txtPassword.BackColor = Color.FromArgb(60, 60, 60);
            txtPassword.ForeColor = Color.FromArgb(180, 180, 180);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Arial", 10);
            txtPassword.Text = "🔒  Password";
            txtPassword.PasswordChar = '\0';

            txtEmail.Enter += (s, ev) =>
            {
                if (txtEmail.Text == "✉  Email") { txtEmail.Text = ""; txtEmail.ForeColor = Color.White; }
            };
            txtEmail.Leave += (s, ev) =>
            {
                if (txtEmail.Text == "") { txtEmail.Text = "✉  Email"; txtEmail.ForeColor = Color.FromArgb(180, 180, 180); }
            };

            txtPassword.Enter += (s, ev) =>
            {
                if (txtPassword.Text == "🔒  Password") { txtPassword.Text = ""; txtPassword.ForeColor = Color.White; txtPassword.PasswordChar = '*'; }
            };
            txtPassword.Leave += (s, ev) =>
            {
                if (txtPassword.Text == "") { txtPassword.Text = "🔒  Password"; txtPassword.ForeColor = Color.FromArgb(180, 180, 180); txtPassword.PasswordChar = '\0'; }
            };

            btnLogin.BackColor = Color.FromArgb(180, 140, 80, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 160, 0);
            btnLogin.FlatAppearance.BorderSize = 1;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 180, 120, 0);
            btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 120, 70, 0);
            btnLogin.Font = new Font("Arial", 10, FontStyle.Bold);
            btnLogin.Cursor = Cursors.Hand;

            btnRegister.BackColor = Color.FromArgb(80, 50, 50, 50);
            btnRegister.ForeColor = Color.FromArgb(220, 220, 220);
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderColor = Color.FromArgb(150, 200, 200, 200);
            btnRegister.FlatAppearance.BorderSize = 1;
            btnRegister.FlatAppearance.MouseOverBackColor = Color.FromArgb(150, 80, 80, 80);
            btnRegister.Font = new Font("Arial", 9);
            btnRegister.Cursor = Cursors.Hand;

            // Styling tombol SQL Injection demo
            btnSQLi.BackColor = Color.FromArgb(180, 180, 30, 30);
            btnSQLi.ForeColor = Color.White;
            btnSQLi.FlatStyle = FlatStyle.Flat;
            btnSQLi.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 50, 50);
            btnSQLi.FlatAppearance.BorderSize = 1;
            btnSQLi.Font = new Font("Arial", 8, FontStyle.Bold);
            btnSQLi.Cursor = Cursors.Hand;
            btnSQLi.Text = "⚠ Login (SQL Injection Demo)";

            int centerX = this.ClientSize.Width / 2;
            txtEmail.Left = centerX - txtEmail.Width / 2;
            txtPassword.Left = centerX - txtPassword.Width / 2;
            btnLogin.Left = centerX - btnLogin.Width / 2;
            btnRegister.Left = centerX - btnRegister.Width / 2;
            btnSQLi.Left = centerX - btnSQLi.Width / 2;
        }

        // =============================================
        // LOGIN AMAN - menggunakan STORED PROCEDURE
        // =============================================
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "" || txtEmail.Text == "✉  Email")
            {
                MessageBox.Show("Email tidak boleh kosong!");
                return;
            }
            if (txtPassword.Text == "" || txtPassword.Text == "🔒  Password")
            {
                MessageBox.Show("Password tidak boleh kosong!");
                return;
            }

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_LoginUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string role = reader["role"].ToString();
                    MessageBox.Show("Login berhasil sebagai " + role);
                    userRole = role;

                    reader.Close();
                    conn.Close();

                    if (role == "Admin") { new FormMenu().Show(); }
                    else if (role == "Customer") { new FormCustomer().Show(); }
                    else if (role == "Manager") { new FormManager().Show(); }
                    else { MessageBox.Show("Role tidak dikenali!"); return; }

                    this.Hide();
                }
                else
                {
                    reader.Close();
                    conn.Close();
                    MessageBox.Show("Email atau Password salah!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // =============================================
        // LOGIN SQL INJECTION DEMO
        // Sengaja rentan - untuk demonstrasi UCP 2
        // Coba input: ' OR '1'='1
        // =============================================
        

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormRegister reg = new FormRegister();
            reg.Show();
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void btnSQLi_Click_1(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            // Query SENGAJA tidak pakai parameterized - rentan SQL Injection!
            string query = "SELECT * FROM [User] WHERE email='" + email + "' AND password='" + password + "'";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string role = reader["role"].ToString();
                    string nama = reader["nama"].ToString();
                    MessageBox.Show(
                        "⚠ [SQL INJECTION DEMO]\n\n" +
                        "Login berhasil sebagai: " + nama + " (" + role + ")\n\n" +
                        "Query yang dieksekusi:\n" + query,
                        "SQL Injection Berhasil!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        "⚠ [SQL INJECTION DEMO]\n\nLogin gagal.\n\nQuery:\n" + query,
                        "SQL Injection Gagal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }
    }
}