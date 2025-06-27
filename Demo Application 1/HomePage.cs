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
            //Move to next form
            Form buySellForm = new BuySell(connString);//pass the connection string
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
    }
}
