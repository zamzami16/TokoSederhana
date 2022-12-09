using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TokoSederhana
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string unameDefault = "masukkan username anda";
            string passDefault = "masukkan password anda";
            if ((textBoxUsername.Text.ToLower() != unameDefault) & (textBoxPassword.Text.ToLower() != passDefault))
            {
                if ((textBoxPassword.Text != string.Empty) & (textBoxUsername.Text != string.Empty))
                {
                    if (textBoxUsername.Text == "zami" & textBoxPassword.Text == "1234")
                    {
                        MessageBox.Show("Login berhasil", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TambahDataBarang tambahDataBarang = new TambahDataBarang();
                        this.Hide();
                        tambahDataBarang.ShowDialog();
                        Application.Exit();
                    }
                }
                else
                {
                    MessageBox.Show(
                    "Masukkan Username dan password yang benar.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                }
            }
            else
            {
                MessageBox.Show(
                    "Masukkan Username dan password yang benar.",
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                    );
            }
        }
    }
}
