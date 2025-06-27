using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Application_1
{
    public partial class BuySell : Form
    {
        private string connString;
        public BuySell(string connectionString)
        {
            InitializeComponent();
            connString = connectionString;
        }

        private void BuySell_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void BuySell_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void LoadInventoryData(string searchText)
        {
            //dataGridView1.ReadOnly = true;
            //dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.AllowUserToDeleteRows = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string choice = cbCardGame.Text;
            string cardGame = string.Format("{0}Inventory", cbCardGame); //Chose card game from dropdown menu

            string query = string.Format("SELECT cardName, rarity, setId, mktPrice, conditionId, amtInStock, imageURL " +
                                         "FROM {0} " +
                                         "WHERE cardName LIKE @cardName " +
                                         "ORDER BY cardName;", cardGame);
        }

        private void tbSearchBar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
