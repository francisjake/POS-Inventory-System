using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSLoginRegisterForm.Connection;

namespace PointOfSales_InventorySystem
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearControls();
            fName.Select();
        }

        private void clearControls()
        {
            fName.Clear();
            lName.Clear();
            cNum.Clear();
            eAdd.Clear();
            uName.Clear();
            pWord.Clear();
            repWord.Clear();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            loadUserData();
            fName.Select();
        }

        private void loadUserData()
        {
            DataTable userData = ServerConnection.executeSQL("SELECT ([First Name] + ' ' + [Last Name]) AS Fullname, Username FROM tblUsers");
            dataGridView1.DataSource = userData;
            dataGridView1.Columns[0].HeaderText = "Full Name";
            dataGridView1.Columns[1].HeaderText = "Username";
            dataGridView1.Columns[0].Width = 315;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Do you want to permanently delete the selected record?",
                    "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {

                    ServerConnection.executeSQL("DELETE FROM tblUsers WHERE Username = '" + dataGridView1.CurrentRow.Cells[1].Value + "'");

                    loadUserData();

                    MessageBox.Show("The record has been deleted.",
                        "Delete Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    return;
                }

            }
            catch (Exception)
            {
                // An error occured!
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxIcon ico = MessageBoxIcon.Information;
            string caption = "Save Data";

            if (string.IsNullOrEmpty(fName.Text))
            {
                MessageBox.Show("Please enter First Name.", caption, btn, ico);
                fName.Select();
                return;
            }

            if (string.IsNullOrEmpty(lName.Text))
            {
                MessageBox.Show("Please enter Last Name.", caption, btn, ico);
                lName.Select();
                return;
            }

            if (string.IsNullOrEmpty(uName.Text))
            {
                MessageBox.Show("Please enter Username.", caption, btn, ico);
                uName.Select();
                return;
            }

            if (string.IsNullOrEmpty(pWord.Text))
            {
                MessageBox.Show("Please enter Password.", caption, btn, ico);
                pWord.Select();
                return;
            }

            if (string.IsNullOrEmpty(repWord.Text))
            {
                MessageBox.Show("Please enter Confirmation Password.", caption, btn, ico);
                repWord.Select();
                return;
            }

            if (pWord.Text != repWord.Text)
            {
                MessageBox.Show("Your password and confirmation password do not match.", caption, btn, ico);
                repWord.SelectAll();
                return;
            }

            string yourSQL = "SELECT Username FROM tblUsers WHERE Username = '" + uName.Text + "'";
            DataTable checkDuplicates = CSLoginRegisterForm.Connection.ServerConnection.executeSQL(yourSQL);

            if (checkDuplicates.Rows.Count > 0)
            {
                MessageBox.Show("The username already exists. Please try another username.",
                    "C# Registration Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uName.SelectAll();
                return;
            }

            DialogResult result;
            result = MessageBox.Show("Do you want to save the record?", "Save Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string mySQL = string.Empty;

                mySQL += "INSERT INTO tblUsers ([First Name], [Last Name], [Contact Number], [Email Address], Username, Password) ";
                mySQL += "VALUES ('" + fName.Text + "','" + lName.Text + "','" + cNum.Text + "','" + eAdd.Text + "',";
                mySQL += "'" + uName.Text + "','" + pWord.Text + "')";

                CSLoginRegisterForm.Connection.ServerConnection.executeSQL(mySQL);

                MessageBox.Show("The record has been saved successfully.",
                                "Save Data", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                loadUserData();
                clearControls();

            }
        }
    }
}
