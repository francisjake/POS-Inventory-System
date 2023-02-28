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
    public partial class AddProd : Form
    {
        public AddProd()
        {
            InitializeComponent();
        }

        private void AddProd_Load(object sender, EventArgs e)
        {
            loadProductData();
        }

        private void loadProductData()
        {
            DataTable proddata = ServerConnection.executeSQL("SELECT ProductID, [Product Name], Category, [Price Medium], [Price Large] FROM tblProducts ORDER BY [Product Name]");
            dataGridView1.DataSource = proddata;
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
        }

        private void btnClo_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnCle_Click(object sender, EventArgs e)
        {
            clearControls();
            tbPrdID.Select();
        }

        private void clearControls()
        {
            tbPrdID.Clear();
            tbPrdNm.Clear();
            tbCat.Clear();
            tbPrcMdm.Clear();
            tbPrcLrg.Clear();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tbPrdID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            tbPrdNm.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            tbCat.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            tbPrcMdm.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            tbPrcLrg.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnUpD_Click(object sender, EventArgs e)
        {
            string mySQL = string.Empty;

            mySQL += "UPDATE tblProducts SET ProductID = '" + tbPrdID.Text + "', [Product Name] = '" + tbPrdNm.Text + "', [Price Medium] = '" + tbPrcMdm.Text + "', [Price Large] = '" + tbPrcLrg.Text + "' WHERE ProductID = '" + tbPrdID.Text + "'";

            CSLoginRegisterForm.Connection.ServerConnection.executeSQL(mySQL);

            loadProductData();
            clearControls();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxIcon ico = MessageBoxIcon.Information;
            string caption = "Add Data";

            if (string.IsNullOrEmpty(tbPrdID.Text))
            {
                MessageBox.Show("Please enter a product id.", caption, btn, ico);
                tbPrdID.Select();
                return;
            }

            string yourSQL = "SELECT ProductID FROM tblProducts WHERE ProductID = '" + tbPrdID.Text + "'";
            DataTable checkDuplicates = CSLoginRegisterForm.Connection.ServerConnection.executeSQL(yourSQL);

            if (checkDuplicates.Rows.Count > 0)
            {
                MessageBox.Show("A product id already exists. Please try another product id.",
                    "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbPrdID.SelectAll();
                return;
            }

            DialogResult result;
            result = MessageBox.Show("Do you want to save the record?", "Save Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string mySQL = string.Empty;

                mySQL += "INSERT INTO tblProducts (ProductID, [Product Name], Category, [Price Medium], [Price Large]) ";
                mySQL += "VALUES ('" + tbPrdID.Text + "','" + tbPrdNm.Text + "','" + tbCat.Text + "','" + tbPrcMdm.Text + "',";
                mySQL += "'" + tbPrcLrg.Text + "')";

                CSLoginRegisterForm.Connection.ServerConnection.executeSQL(mySQL);

                MessageBox.Show("The record has been saved successfully.",
                                "Save Data", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                loadProductData();
                tbPrdNm.Clear();
                tbPrdNm.Select();
            }
        }
    }
}
