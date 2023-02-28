using System;
using System.Data;
using System.Windows.Forms;
using CSLoginRegisterForm.Connection;

namespace PointOfSales_InventorySystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            passWord.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(userName.Text) &&
               !string.IsNullOrEmpty(passWord.Text))
            {
                string mySQL = string.Empty;

                mySQL += "SELECT * FROM tblUsers ";
                mySQL += "WHERE Username = '" + userName.Text + "' ";
                mySQL += "AND Password = '" + passWord.Text + "'";

                DataTable userData = ServerConnection.executeSQL(mySQL);

                if (userName.Text == "admin" && passWord.Text == "admin")
                {
                    Admin f2 = new Admin();
                    f2.Show();
                    this.Hide();
                }
                else if (userData.Rows.Count > 0)
                {
                    userName.Clear();
                    passWord.Clear();
                    checkBox1.Checked = false;

                    User f3 = new User();
                    f3.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("The username or password is incorrect. Try again.",
                        "Login", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    userName.Focus();
                    userName.SelectAll();
                }

            }
            else
            {
                MessageBox.Show("Please enter username and password.", "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                userName.Select();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                passWord.UseSystemPasswordChar = false;
            }
            else
            {
                passWord.UseSystemPasswordChar = true;
            }
        }
    }
}
