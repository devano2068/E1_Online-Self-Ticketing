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
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(
    "Data Source=LAPTOP-3UJU5DJS\\VANTAMERAH;Initial Catalog=BioskopDB;Integrated Security=True");

        // 🔥 TAMBAHAN: simpan role user
        public static string userRole;

        public Form1()
        {
            InitializeComponent();

            txtPassword.PasswordChar = '*';
            this.AcceptButton = btnLogin;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // VALIDASI
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Email tidak boleh kosong!");
                return;
            }

            if (txtPassword.Text == "")
            {
                MessageBox.Show("Password tidak boleh kosong!");
                return;
            }

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM [User] WHERE email=@email AND password=@password", conn);

                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    string role = reader["role"].ToString();

                    MessageBox.Show("Login berhasil sebagai " + role);

                    // 🔥 TAMBAHAN: simpan role
                    userRole = role;

                    if (role == "Admin")
                    {
                        FormMenu admin = new FormMenu();
                        admin.Show();
                    }
                    else if (role == "Customer")
                    {
                        FormCustomer cust = new FormCustomer();
                        cust.Show();
                    }
                    else if (role == "Manager")
                    {
                        FormManager mgr = new FormManager();
                        mgr.Show();
                    }
                    else
                    {
                        MessageBox.Show("Role tidak dikenali!");
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email atau Password salah!");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormRegister reg = new FormRegister();
            reg.Show();
        }
    }
}