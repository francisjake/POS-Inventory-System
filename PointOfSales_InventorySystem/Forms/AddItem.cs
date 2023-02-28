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

namespace PointOfSales_InventorySystem.Forms
{
    public partial class AddItem : Form
    {
        public AddItem()
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
            tbIngrdntID.Enabled = true;
            tbIngrdntNm.Enabled = true;
            tbIngrdntID.Select();
        }

        private void clearControls()
        {
            tbIngrdntID.Clear();
            tbIngrdntNm.Clear();
            tbmLAmnt.Clear();
            tbSN.Clear();
        }

        private void AddItem_Load(object sender, EventArgs e)
        {
            loadInventory();
            tbIngrdntID.Select();
        }

        private void loadInventory()
        {
            DataTable userData = ServerConnection.executeSQL("SELECT t1.IngredientID, t1.[Ingredient Name], t1.[mL Amount], t2.[Supplier Name], t2.Date FROM tblIngredients t1 INNER JOIN tblSuppliers t2 ON t1.IngredientID = t2.IngredientID");
            dataGridView1.DataSource = userData;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tbIngrdntID.Enabled = false;
            tbIngrdntNm.Enabled = false;
            tbIngrdntID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            tbIngrdntNm.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            tbmLAmnt.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            tbSN.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btnUpD_Click(object sender, EventArgs e)
        {
            string mySQL = string.Empty;

            mySQL += "UPDATE tblIngredients SET [mL Amount] = '" + tbmLAmnt.Text + "' WHERE IngredientID = '" + tbIngrdntID.Text + "'";
            mySQL += "UPDATE tblSuppliers SET [Supplier Name] = '" + tbSN.Text + "', Date = '" + dateTimePicker1.Value + "'";

            CSLoginRegisterForm.Connection.ServerConnection.executeSQL(mySQL);

            MessageBox.Show("Record updated.", "Update data", MessageBoxButtons.OK, MessageBoxIcon.Information);

            tbIngrdntID.Enabled = true;
            tbIngrdntNm.Enabled = true;

            loadInventory();
            clearControls();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxIcon ico = MessageBoxIcon.Information;
            string caption = "Save Data";

            if (string.IsNullOrEmpty(tbIngrdntID.Text))
            {
                MessageBox.Show("Please enter an iten id.", caption, btn, ico);
                tbIngrdntID.Select();
                return;
            }

            if (string.IsNullOrEmpty(tbIngrdntNm.Text))
            {
                MessageBox.Show("Please enter an item name.", caption, btn, ico);
                tbIngrdntNm.Select();
                return;
            }

            if (string.IsNullOrEmpty(tbmLAmnt.Text))
            {
                MessageBox.Show("Please enter the mL count.", caption, btn, ico);
                tbmLAmnt.Select();
                return;
            }

            if (string.IsNullOrEmpty(tbSN.Text))
            {
                MessageBox.Show("Please enter the supplier's name.", caption, btn, ico);
                tbSN.Select();
                return;
            }

            string yourSQL = "SELECT IngredientID FROM tblIngredients WHERE IngredientID = '" + tbIngrdntID.Text + "'";
            DataTable checkDuplicates = CSLoginRegisterForm.Connection.ServerConnection.executeSQL(yourSQL);

            if (checkDuplicates.Rows.Count > 0)
            {
                MessageBox.Show("The ItemID already exists. Please try another ItemID.",
                    "Add Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbIngrdntID.SelectAll();
                return;
            }

            DialogResult result;
            result = MessageBox.Show("Do you want to save the record?", "Save Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string mySQL = string.Empty;

                mySQL += "INSERT INTO tblIngredients (IngredientID, [Ingredient Name], [mL Amount]) VALUES ('" + tbIngrdntID.Text + "','" + tbIngrdntNm.Text + "','" + tbmLAmnt.Text + "')";
                mySQL += "INSERT INTO tblSuppliers (IngredientID, [Supplier Name], Date) VALUES ('" + tbIngrdntID.Text + "','" + tbSN.Text + "','" + dateTimePicker1.Value + "')";

                CSLoginRegisterForm.Connection.ServerConnection.executeSQL(mySQL);

                MessageBox.Show("The record has been saved successfully.",
                                "Save Data", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                loadInventory();
                clearControls();

            }
        }
    }
}
