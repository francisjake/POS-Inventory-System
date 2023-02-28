using System;
using System.Data;
using System.Windows.Forms;
using CSLoginRegisterForm.Connection;
using PointOfSales_InventorySystem.Forms;

namespace PointOfSales_InventorySystem
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm:ss");
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            timer1.Start();
            loadUserData();
            loadProductData();
            loadDashboardCount();
            loadHSP();
            loadLS();
            loadRAP();
            loadCategories();
            loadInventory();
            loadOrders();
            loadSales();
        }

        private void loadUserData()
        {
            DataTable userData = ServerConnection.executeSQL("SELECT ([First Name] + ' ' + [Last Name]) AS Name, [Contact Number], [Email Address], Username, Password FROM tblUsers");
            dataGridView1.DataSource = userData;            
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void loadDashboardCount()
        {
            UsrsCount.Text = dataGridView1.Rows.Count.ToString();

            DataTable categoryCount = ServerConnection.executeSQL("SELECT Category FROM tblProducts GROUP BY (Category)");
            CtgrsCount.Text = categoryCount.Rows.Count.ToString();

            PrdctsCount.Text = dataGridView5.Rows.Count.ToString();

            SlesCount.Text = dataGridView8.Rows.Count.ToString();
        }

        private void loadHSP()
        {
            DataTable HSP = ServerConnection.executeSQL("SELECT TOP 5 Date, Total, [Order Details] FROM tblSales ORDER BY Total DESC");
            dataGridView2.DataSource = HSP;
            dataGridView2.Columns[1].Width = 50;
            dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void loadLS()
        {
            DataTable LS = ServerConnection.executeSQL("SELECT TOP 5 Date, [Customer Name], [Order Details] FROM tblSales ORDER BY AutoID DESC");
            dataGridView3.DataSource = LS;
            dataGridView3.Columns[0].Width = 52;
            dataGridView3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void loadRAP()
        {
            DataTable RAP = ServerConnection.executeSQL("SELECT TOP 5 ProductID, [Product Name], Category FROM tblProducts ORDER BY AutoID DESC");
            dataGridView4.DataSource = RAP;
            dataGridView4.Columns[0].Width = 65;
            dataGridView4.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            f1.Show();
            this.Hide();
        }

        private void Dshbrd_Click(object sender, EventArgs e)
        {
            loadDashboardCount();
            loadHSP();
            loadLS();
            loadRAP();
            panel14.Show();
            panel15.Hide();
            panel17.Hide();
            panel25.Hide();
            panel27.Hide();
            panel34.Hide();
            panel36.Hide();
        }

        private void UsrMngmnt_Click(object sender, EventArgs e)
        {
            loadUserData();
            panel14.Hide();
            panel15.Show();
            panel17.Hide();
            panel25.Hide();
            panel27.Hide();
            panel34.Hide();
            panel36.Hide();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.ShowDialog();
        }

        private void btnSrch1_Click(object sender, EventArgs e)
        {
            DataTable userData = ServerConnection.executeSQL("SELECT ([First Name] + ' ' + [Last Name]) AS Name, [Contact Number], [Email Address], Username, Password FROM tblUsers WHERE [First Name] = '" + srchBx1.Text + "'");
            dataGridView1.DataSource = userData;
        }

        private void Ctgrs_Click(object sender, EventArgs e)
        {
            loadCategories();
            panel14.Hide();
            panel15.Hide();
            panel17.Show();
            panel25.Hide();
            panel27.Hide();
            panel34.Hide();
            panel36.Hide();
        }

        private void loadCategories()
        {
            DataTable CMT = ServerConnection.executeSQL("SELECT ProductID, [Product Name] FROM tblProducts WHERE Category IN ('Classic Milk Tea')");
            dataGridView9.DataSource = CMT;
            dataGridView9.Columns[0].Width = 62;
            dataGridView9.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataTable MS = ServerConnection.executeSQL("SELECT ProductID, [Product Name] FROM tblProducts WHERE Category IN ('Milk Shake')");
            dataGridView10.DataSource = MS;
            dataGridView10.Columns[0].Width = 62;
            dataGridView10.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataTable FT = ServerConnection.executeSQL("SELECT ProductID, [Product Name] FROM tblProducts WHERE Category IN ('Fruit Tea')");
            dataGridView11.DataSource = FT;
            dataGridView11.Columns[0].Width = 62;
            dataGridView11.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataTable F = ServerConnection.executeSQL("SELECT ProductID, [Product Name] FROM tblProducts WHERE Category IN ('Frappucino')");
            dataGridView12.DataSource = F;
            dataGridView12.Columns[0].Width = 62;
            dataGridView12.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataTable C = ServerConnection.executeSQL("SELECT ProductID, [Product Name] FROM tblProducts WHERE Category IN ('Coffee')");
            dataGridView13.DataSource = C;
            dataGridView13.Columns[0].Width = 62;
            dataGridView13.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataTable Othrs = ServerConnection.executeSQL("SELECT ProductID, [Product Name] FROM tblProducts WHERE Category IN ('Others')");
            dataGridView14.DataSource = Othrs;
            dataGridView14.Columns[0].Width = 62;
            dataGridView14.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Prdcts_Click(object sender, EventArgs e)
        {
            loadProductData();
            panel14.Hide();
            panel15.Hide();
            panel17.Hide();
            panel25.Show();
            panel27.Hide();
            panel34.Hide();
            panel36.Hide();
        }

        private void loadProductData()
        {
            DataTable proddata = ServerConnection.executeSQL("SELECT ProductID, [Product Name], Category, [Price Medium], [Price Large] FROM tblProducts ORDER BY [Product Name]");
            dataGridView5.DataSource = proddata;
            dataGridView5.Columns[0].Width = 100;
            dataGridView5.Columns[1].Width = 175;
            dataGridView5.Columns[2].Width = 125;
            dataGridView5.Columns[3].Width = 80;
            dataGridView5.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void AddProd_Click(object sender, EventArgs e)
        {
            AddProd addprod = new AddProd();
            addprod.ShowDialog();
            loadProductData();
        }

        private void CustProd_Click(object sender, EventArgs e)
        {
            CustomProduct CustProd = new CustomProduct();
            CustProd.ShowDialog();
            loadInventory();
        }

        private void btnSrch2_Click(object sender, EventArgs e)
        {
            DataTable userData = ServerConnection.executeSQL("SELECT ProductID, [Product Name], Category, [Price Medium], [Price Large] FROM tblProducts WHERE [Product Name] = '" + srchBx2.Text + "'");
            dataGridView5.DataSource = userData;
        }

        private void Invtry_Click(object sender, EventArgs e)
        {
            loadInventory();
            panel14.Hide();
            panel15.Hide();
            panel17.Hide();
            panel25.Hide();
            panel27.Show();
            panel34.Hide();
            panel36.Hide();
        }

        private void loadInventory()
        {
            //DataTable invSupp = ServerConnection.executeSQL("SELECT t1.IngredientID, t1.[Ingredient Name], t1.[mL Amount], t2.[Supplier Name], t2.Date FROM tblIngredients t1 INNER JOIN tblSuppliers t2 ON t1.IngredientID = t2.IngredientID");
            //dataGridView6.DataSource = invSupp;
            //dataGridView6.Columns[0].Width = 150;
            //dataGridView6.Columns[1].Width = 180;
            //dataGridView6.Columns[2].Width = 100;
            //dataGridView6.Columns[3].Width = 180;
            //dataGridView6.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void AddIng_Click(object sender, EventArgs e)
        {
            AddItem AddIt = new AddItem();
            AddIt.ShowDialog();
            loadInventory();
        }

        private void Ordrs_Click(object sender, EventArgs e)
        {
            loadOrders();
            panel14.Hide();
            panel15.Hide();
            panel17.Hide();
            panel25.Hide();
            panel27.Hide();
            panel34.Show();
            panel36.Hide();
        }

        private void loadOrders()
        {
            //DataTable orders = ServerConnection.executeSQL("SELECT * FROM tblOrders");
            //dataGridView7.DataSource = orders;
            //dataGridView7.Columns[0].Width = 120;
            //dataGridView7.Columns[1].Width = 200;
            //dataGridView7.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnRmv_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Do you want to permanently delete the selected record?",
                    "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {

                    ServerConnection.executeSQL("DELETE FROM tblOrders WHERE [Customer Name] = '" + dataGridView7.CurrentRow.Cells[1].Value + "'");

                    loadOrders();

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Is the order meets its requirements?",
                    "Order Completion", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {

                    ServerConnection.executeSQL("DELETE FROM tblOrders WHERE [Customer Name] = '" + dataGridView7.CurrentRow.Cells[1].Value + "'");

                    loadOrders();

                    MessageBox.Show("Order Completed.",
                        "Order Completion",
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

        private void Sles_Click(object sender, EventArgs e)
        {
            loadSales();
            panel14.Hide();
            panel15.Hide();
            panel17.Hide();
            panel25.Hide();
            panel27.Hide();
            panel34.Hide();
            panel36.Show();
        }

        private void loadSales()
        {
            DataTable sales = ServerConnection.executeSQL("SELECT * FROM tblSales");
            dataGridView8.DataSource = sales;
            dataGridView8.Columns[0].Width = 120;
            dataGridView8.Columns[3].Width = 50;
            dataGridView8.Columns[4].Width = 50;
            dataGridView8.Columns[5].Width = 50;
            dataGridView8.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Do you want to permanently delete the selected record?",
                    "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {

                    ServerConnection.executeSQL("DELETE FROM tblSales WHERE [Customer Name] = '" + dataGridView8.CurrentRow.Cells[1].Value + "'");

                    loadSales();

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
    }
}
