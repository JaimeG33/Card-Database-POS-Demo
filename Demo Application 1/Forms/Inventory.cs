using Demo_Application_1.Helpers;
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

namespace Demo_Application_1.Forms
{
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
        }

        private string connString;
        private HomePage _homePage;
        private bool changingTabs = false;

        public Inventory(string connectionString, HomePage homePage)
        {
            InitializeComponent();
            connString = connectionString;
            _homePage = homePage;
        }
        private void Inventory_Load(object sender, EventArgs e)
        {
            SetupDatagridView();
        }

        // Return to HomePage method (Different from the others)
        private void ReturnHome()
        {
            _homePage.Show();
            changingTabs = true;
            this.Close();
        }
        private void cbReturnHome_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbReturnHome.Text == "Return Home")
            {
                changingTabs = true;
                NavigationHelper.ReturnToHome(this, _homePage, ref changingTabs);
            }
        }
        private void Inventory_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (changingTabs == false)
            {
                Application.Exit();
            }
        }
        
        private void SetupDatagridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = false;

            
        }
        private void tbSearchBar_TextChanged(object sender, EventArgs e)
        {

        }
        private void tbSearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            //When you press enter on the search bar, Load the inventory data onto the datagridview
            if (e.KeyCode == System.Windows.Forms.Keys.Enter) //System.Windows.Forms added to prevent mixup with selenium
            {
                LoadInventoryData(tbSearchBar.Text.Trim());
            }
        }

        private void LoadInventoryData(string searchText)
        {
            string choice = cbCardGame.Text;
            searchText = tbSearchBar.Text.Trim();
            string cardGame = string.Format("{0}Inventory", choice); //Chose card game from dropdown menu

            string query = string.Format("SELECT cardName, rarity, setId, mktPrice, conditionId, amtInStock, priceUp2Date, imageURL, mktPriceURL, cardId " +
                                         "FROM {0} " +
                                         "WHERE cardName LIKE @cardName " +
                                         "ORDER BY cardName;", cardGame);

            //Before connecting to database, check to see if valid card game is selected
            List<string> allowedTables = new List<string>
            { "YugiohInventory", "MagicInventory", "PokemonInventory" };
            if (!allowedTables.Contains(cardGame))
            {
                MessageBox.Show("Invalid card game selection.");
                return;
            }

            // Temporarily detach handler to prevent annoying errors during data load
            dataGridView1.CellValueChanged -= dataGridView1_CellValueChanged;

            //connect to database using connection string
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    //enter the query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        //Filter by the text from the search bar
                        command.Parameters.AddWithValue("@cardName", "%" + searchText + "%");


                        //Now display the results on the data table
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            if (dataTable.Rows.Count == 0)
                            {
                                MessageBox.Show($"No results found for \"{searchText}\".");
                                dataGridView1.DataSource = null;
                            }
                            else
                            {
                                dataGridView1.DataSource = dataTable;

                                // Visual Stuff for the collumns
                                dataGridView1.Columns["cardName"].HeaderText = "Card Name";
                                dataGridView1.Columns["rarity"].HeaderText = "Rarity";
                                dataGridView1.Columns["mktPrice"].HeaderText = "Market Price";
                                dataGridView1.Columns["conditionId"].HeaderText = "Condition";
                                dataGridView1.Columns["amtInStock"].HeaderText = "In Stock";
                                dataGridView1.Columns["priceUp2Date"].HeaderText = "Up 2 Date";
                                //Hide urls and unnessisary columns
                                if (dataGridView1.Columns.Contains("imageURL"))
                                    dataGridView1.Columns["imageURL"].Visible = false;
                                if (dataGridView1.Columns.Contains("mktPriceURL"))
                                    dataGridView1.Columns["mktPriceURL"].Visible = false;
                                if (dataGridView1.Columns.Contains("cardId"))
                                    dataGridView1.Columns["cardId"].Visible = false;
                                //Width of collumns
                                dataGridView1.Columns["conditionId"].Width = 55;
                                dataGridView1.Columns["setId"].Width = 60;
                                dataGridView1.Columns["mktPrice"].Width = 80;
                                dataGridView1.Columns["amtInStock"].Width = 40;
                                dataGridView1.Columns["priceUp2Date"].Width = 40;
                                dataGridView1.Columns["cardName"].Width = 225;
                                dataGridView1.Columns["mktPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dataGridView1.Columns["mktPrice"].DefaultCellStyle.Format = "C2";

                                // Columns that you can / cant edit
                                dataGridView1.Columns["conditionId"].ReadOnly = true;
                                dataGridView1.Columns["amtInStock"].ReadOnly = false; // Editable
                                dataGridView1.Columns["mktPrice"].ReadOnly = true;
                                dataGridView1.Columns["priceUp2Date"].ReadOnly = true;
                                dataGridView1.Columns["setId"].ReadOnly = true;
                                dataGridView1.Columns["cardName"].ReadOnly = true;
                                dataGridView1.Columns["rarity"].ReadOnly = true;


                            }
                        }
                    }
                    // Reattach handler that was removed before data load
                    dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error loading inventory: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore invalid events
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            // Make sure the DataSource is set
            if (dataGridView1.DataSource == null)
                return;

            // Only handle amtInStock column
            if (dataGridView1.Columns[e.ColumnIndex].Name == "amtInStock")
            {
                var row = dataGridView1.Rows[e.RowIndex];
                var newValue = row.Cells["amtInStock"].Value;
                var cardId = row.Cells["cardId"].Value;
                var conditionId = row.Cells["conditionId"].Value;
                var cardGame = cbCardGame.Text + "Inventory";

                if (newValue == null || cardId == null || conditionId == null)
                    return;

                if (!int.TryParse(newValue.ToString(), out int amtInStock))
                {
                    MessageBox.Show("Invalid stock value.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = $"UPDATE [{cardGame}] SET amtInStock = @amtInStock WHERE cardId = @cardId AND conditionId = @conditionId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@amtInStock", amtInStock);
                        cmd.Parameters.AddWithValue("@cardId", cardId);
                        cmd.Parameters.AddWithValue("@conditionId", conditionId);
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error updating stock: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        
    }
}
