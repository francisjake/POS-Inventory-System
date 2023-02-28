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
using System.Data.SqlClient;

namespace PointOfSales_InventorySystem.Forms
{
    public partial class CustomProduct : Form
    {
        public CustomProduct()
        {
            InitializeComponent();
        }

        private void btnClo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCle_Click(object sender, EventArgs e)
        {
            clearControls();
            tbIng1.Select();
        }

        private void clearControls()
        {
            tbIng1.Clear();
            tbIng2.Clear();
            tbIng3.Clear();
            tbIng4.Clear();
            tbIng5.Clear();
            tbIng6.Clear();
            tbIng7.Clear();
            tbIng8.Clear();
            tbIng9.Clear();
            tbIng10.Clear();
        }

        private void CustomProduct_Load(object sender, EventArgs e)
        {
            loadComBox();
            loadProdIngs();
        }

        private void loadComBox()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-E3N36HA;Initial Catalog=POS&IS;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblProducts", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            con.Open();

            SqlDataReader myReader;
            comboBox1.Items.Clear();

            try
            {
                myReader = cmd.ExecuteReader();

                while(myReader.Read())
                {
                    string n = myReader.GetString(1);
                    comboBox1.Items.Add(n);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }

            con.Close();
        }

        private void loadProdIngs()
        {
            DataTable prodIng = ServerConnection.executeSQL("SELECT * FROM tblProdIngs");
            dataGridView1.DataSource = prodIng;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            tbIng1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            tbIng2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            tbIng3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            tbIng4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            tbIng5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            tbIng6.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            tbIng7.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            tbIng8.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            tbIng9.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            tbIng10.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
        }

        private void btnUpD_Click(object sender, EventArgs e)
        {
            string mySQL = string.Empty;

            mySQL += "UPDATE tblProdIngs SET ProductID = '" + comboBox1.Text + "', Ingredient Name 1 = '" + tbIng1.Text + "', Ingredient Name 2 = '" + tbIng2.Text + "', Ingredient Name 3 = '" + tbIng3.Text + "', Ingredient Name 4 = '" + tbIng4.Text + "', Ingredient Name 5 = '" + tbIng5.Text + "'";
            mySQL += "Ingredient Name 6 = '" + tbIng6.Text + "', Ingredient Name 7 = '" + tbIng7.Text + "', Ingredient Name 8 = '" + tbIng8.Text + "', Ingredient Name 9 = '" + tbIng9.Text + "', Ingredient Name 10 = '" + tbIng10.Text + "' WHERE ProductID = '" + comboBox1.Text + "'";

            CSLoginRegisterForm.Connection.ServerConnection.executeSQL(mySQL);

            loadProdIngs();
            clearControls();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxIcon ico = MessageBoxIcon.Information;
            string caption = "Save Data";

            if (string.IsNullOrEmpty(tbIng1.Text))
            {
                MessageBox.Show("Please enter atleast 1 ingredient.", caption, btn, ico);
                tbIng1.Select();
                return;
            }

            DialogResult result;
            result = MessageBox.Show("Do you want to save the record?", "Save Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string mySQL = string.Empty;

                mySQL += "INSERT INTO tblProdIngs (ProductID, [Ingredient Name 1], [Ingredient Name 2], [Ingredient Name 3], [Ingredient Name 4], [Ingredient Name 5], [Ingredient Name 6], [Ingredient Name 7], [Ingredient Name 8], [Ingredient Name 9], [Ingredient Name 10]) ";
                mySQL += "VALUES ('" + comboBox1.SelectedItem + "','" + tbIng1.Text + "','" + tbIng2.Text + "','" + tbIng3.Text + "',";
                mySQL += "'" + tbIng4.Text + "','" + tbIng5.Text + "','" + tbIng6.Text + "','" + tbIng7.Text + "',";
                mySQL += "'" + tbIng8.Text + "','" + tbIng9.Text + "','" + tbIng10.Text + "')";

                CSLoginRegisterForm.Connection.ServerConnection.executeSQL(mySQL);

                MessageBox.Show("The record has been saved successfully.",
                                "Save Data", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                loadProdIngs();
                clearControls();

            }
        }
    }
}
