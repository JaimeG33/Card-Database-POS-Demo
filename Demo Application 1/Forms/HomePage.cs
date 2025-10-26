using Demo_Application_1.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Application_1
{
    public partial class HomePage : Form
    {
        private string connString;
        
        
        //public HomePage()  moddified to take the connection string from the login page
        public HomePage(string connectionString)
        {
            InitializeComponent();
            connString = connectionString;
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void HomePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnBuySell_Click(object sender, EventArgs e)
        {
            //Open BuySell Tab
            Form buySellForm = new BuySell(connString, this);//pass the connection string and the HomePage reference
            buySellForm.Show();//oppen the seller form
            this.Hide();//hide this form
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ClearMktPrice()
        {
            string query = $"UPDATE [dbo].[YugiohInventory] " +
                            $"SET priceUp2Date = 0; " +
                            $"UPDATE [dbo].[MagicInventory] " +
                            $"SET priceUp2Date = 0; " +
                            $"UPDATE [dbo].[PokemonInventory] " +
                            $"SET priceUp2Date = 0; ";
            //Now connect to the database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery(); // This actually runs the SQL
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating price: " + ex.Message);
                }
            }

        }

        private void btnResetDate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show
                (   "This will mark all the market prices as outdated. Are you sure you want to continue??",
                    "Confirm Reset",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning 
                );
            if ( result == DialogResult.OK )
            {
                ClearMktPrice();
            }
            else
            {
                MessageBox.Show("Cancelled");
            }
        }

        private void btnResetIp_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show
                ("This will delete the saved Ip Address and make you enter it again to log in",
                    "Confirm Reset (This will exit the application)",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.ServerIp = string.Empty;
                Properties.Settings.Default.Save();

                MessageBox.Show("IP address reset successfully. The application will now close.");
                Application.Exit(); // Forces re-entry of IP on next start
            }
            else
            {
                MessageBox.Show("Cancelled");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Open History tab
            Form history = new History(connString, this);
            history.Show();
            this.Hide();

        }
        private void invMgmt_Click(object sender, EventArgs e)
        {
            // Open Inventory Management tab
            Form inventory = new Inventory(connString, this);
            inventory.Show();
            this.Hide();
        }

        private void cbxProfile_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
