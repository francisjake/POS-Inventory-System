using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSales_InventorySystem
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss");
        }

        private void User_Load(object sender, EventArgs e)
        {
            timer1.Start();
            cbMoP.Items.Add("Cash");
            cbMoP.Items.Add("GCash");
            cbMoP.Items.Add("Paymaya");
        }

        private void logout_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            f1.Show();
            this.Hide();
        }

        private void NumbersOnly(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (tbCash.Text == "0")
            {
                tbCash.Text = "";
                tbCash.Text = b.Text;
            }
            else if (b.Text == ".")
            {
                if (!tbCash.Text.Contains("."))
                {
                    tbCash.Text = tbCash.Text + b.Text;
                }
            }
            else
            {
                tbCash.Text = tbCash.Text + b.Text;
            }
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            tbCash.Text = "0";
        }

        private void CMT(object sender, EventArgs e)
        {
            Button CMT = (Button)sender;

            if (tbPrdctNm.Text != null)
            {
                tbPrdctNm.Text = CMT.Text;
                if (cbSz.Checked == true)
                {
                    tbPrice.Text = "59";
                }
                else
                {
                    tbPrice.Text = "45";
                }
            }
        }

        private void MS(object sender, EventArgs e)
        {
            Button MS = (Button)sender;

            if (tbPrdctNm.Text != null)
            {
                tbPrdctNm.Text = MS.Text;
                if (cbSz.Checked == true)
                {
                    tbPrice.Text = "79";
                }
                else
                {
                    tbPrice.Text = "59";
                }
            }
        }

        private void FT(object sender, EventArgs e)
        {
            Button FT = (Button)sender;

            if (tbPrdctNm.Text != null)
            {
                tbPrdctNm.Text = FT.Text;
                if (cbSz.Checked == true)
                {
                    tbPrice.Text = "59";
                }
                else
                {
                    tbPrice.Text = "49";
                }
            }
        }

        private void F(object sender, EventArgs e)
        {
            Button F = (Button)sender;

            if (tbPrdctNm.Text != null)
            {
                tbPrdctNm.Text = F.Text;
                if (cbSz.Checked == true)
                {
                    tbPrice.Text = "89";
                }
                else
                {
                    tbPrice.Text = "79";
                }
            }
        }

        private void C(object sender, EventArgs e)
        {
            Button C = (Button)sender;

            if (tbPrdctNm.Text != null)
            {
                tbPrdctNm.Text = C.Text;
                cbSz.Checked = true;
                tbPrice.Text = "65";
            }
        }

        private void SgrLvl(object sender, EventArgs e)
        {
            Button SL = (Button)sender;

            if (tbSgrLvl.Text != null)
            {
                tbSgrLvl.Text = SL.Text;
            }
        }

        private int cnt = 0;

        private void btnCnfrm_Click(object sender, EventArgs e)
        {
            if ((tbPrdctNm.Text != "") && (tbQty.Text != "") && (tbSgrLvl.Text != ""))
            {
                string PN = tbPrdctNm.Text;
                string Qty = tbQty.Text;
                string[] Size = { "Large", "Medium" };
                string Sz = "Medium";
                if (cbSz.Checked == true)
                {
                    Sz = Size[0];
                }
                else
                {
                    Sz = Size[1];
                }
                string SL = tbSgrLvl.Text;
                string Prc = tbPrice.Text;
                string[] row = { PN, Qty, Sz, SL, Prc };
                cnt++;
                if (cnt != 0)
                {
                    btnPay.Enabled = true;
                }
                dgvOrders.Rows.Add(row);
                AddCost();
            }
            else
            {
                MessageBox.Show("Incomplete information.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int Total()
        {
            int sum = 0;
            int i = 0;
            int qty = Convert.ToInt32(tbQty.Text);

            for (i = 0; i < (dgvOrders.Rows.Count); i++)
            {
                sum = sum + (Convert.ToInt32(dgvOrders.Rows[i].Cells[4].Value) * qty);
            }

            return sum;
        }

        private void AddCost()
        {
            if (dgvOrders.Rows.Count > 0)
            {
                tbTotal.Text = String.Format("{0}", Total());
            }
        }

        private void Change()
        {
            int c;
            if (dgvOrders.Rows.Count > 0)
            {
                c = Convert.ToInt32(tbCash.Text);
                tbChng.Text = String.Format("{0}", c - Total());
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (cbMoP.Text == "Cash")
            {
                Change();
            }
            else if (cbMoP.Text == "GCash")
            {
                Change();
            }
            else if (cbMoP.Text == "Paymaya")
            {
                Change();
            }
            else
            {
                tbCash.Text = "0";
                tbChng.Text = "";
            }

            if ((tbFN.Text != "") && (tbLN.Text != ""))
            {
                string mySQL = string.Empty;
                string abc = "";
                string CName = tbFN.Text + " " + tbLN.Text;

                for (int x = 0; x < cnt; x++)
                {
                    abc += dgvOrders.Rows[x].Cells[1].Value.ToString() + "x" + dgvOrders.Rows[x].Cells[0].Value.ToString() + ", ";
                }

                mySQL += "INSERT INTO tblOrders (Date, [Customer Name], [Order Details]) VALUES ('" + lblDate.Text + "','" + CName + "','" + abc + "')";

                mySQL += "INSERT INTO tblSales (Date, [Customer Name], [Payment Method], Amount, Total, Change, [Order Details]) VALUES ('" + lblDate.Text + "','" + CName + "','" + cbMoP.Text + "','" + tbCash.Text + "','" + tbTotal.Text + "','" + tbChng.Text + "','" + abc + "')";

                CSLoginRegisterForm.Connection.ServerConnection.executeSQL(mySQL);

                abc = "";
                cnt = 0;

                MessageBox.Show("Order Successfully!", "Congrats!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please provide a name.", "Notice!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            tbFN.Text = "";
            tbLN.Text = "";
            tbPrdctNm.Text = "";
            tbPrice.Text = "";
            tbQty.Text = "";
            tbSgrLvl.Text = "";
            tbTotal.Text = "";
            cbMoP.Text = "";
            tbCash.Text = "";
            tbChng.Text = "";
            dgvOrders.Rows.Clear();
            dgvOrders.Refresh();
        }

        private void btnRmv_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvOrders.SelectedRows)
            {
                dgvOrders.Rows.Remove(row);
            }

            AddCost();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.')))
            {
                e.Handled = true;
            }
        }
    }
}
