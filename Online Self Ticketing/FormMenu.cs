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
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }
        private void btnFilm_Click(object sender, EventArgs e)
        {
            FormFilm f = new FormFilm();
            f.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Close();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            if (Form1.userRole != "Admin")
            {
                MessageBox.Show("Akses ditolak!");
                this.Close();
            }
        }
    }
}
