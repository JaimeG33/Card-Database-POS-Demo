using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            //Form Size
            this.WindowState = FormWindowState.Maximized;
            ImageResizing(); // run ImageResizing Logic
            PositionLabels();


            //Data Grid Security
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void ImageResizing()
        {
            double imgHeight = tLP_img.Height;
            double imgWantedWidth = imgHeight * 0.84;
            int imgWidth = Convert.ToInt32(imgWantedWidth);

            tLP_img.Width = imgWidth;
        }
        private void PositionLabels()
        {
            if (this.Width >= 1200)
            {
                int spacing = 20;

                // Align lblInStock to be 20px to the right of tLP_img, and same top
                lblInStock.Location = new Point(tLP_img.Right + spacing, tLP_img.Top);

                // Position lblMarketPrice below lblInStock
                lblMktPrice.Location = new Point(lblInStock.Left, lblInStock.Bottom + 10);

                //Position add2cart button
                btnAddCt.Location = new Point(lblMktPrice.Left, lblMktPrice.Bottom + 10);
                //Sale Info label stays where its at
            }
        }


        private void BuySell_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void LoadInventoryData(string searchText)
        {
            string choice = cbCardGame.Text;
            searchText = tbSearchBar.Text.Trim();
            string cardGame = string.Format("{0}Inventory", choice); //Chose card game from dropdown menu

            string query = string.Format("SELECT cardName, rarity, setId, mktPrice, conditionId, amtInStock, priceUp2Date, imageURL " +
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

                            if(dataTable.Rows.Count == 0)
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

                                //Width of collumns
                                if (dataGridView1.Columns.Contains("imageURL"))
                                    dataGridView1.Columns["imageURL"].Visible = false;
                                dataGridView1.Columns["conditionId"].Width = 55;
                                dataGridView1.Columns["setId"].Width = 60;
                                dataGridView1.Columns["mktPrice"].Width = 80;
                                dataGridView1.Columns["amtInStock"].Width = 40;
                                dataGridView1.Columns["priceUp2Date"].Width = 40;
                                dataGridView1.Columns["cardName"].Width = 150;
                                dataGridView1.Columns["mktPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dataGridView1.Columns["mktPrice"].DefaultCellStyle.Format = "C2";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading inventory: " + ex.Message);
                }
            }
        }

        private void tbSearchBar_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbSearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            

            //When you press enter on the search bar, Load the inventory data onto the datagridview
            if (e.KeyCode == Keys.Enter)
            {
                LoadInventoryData(tbSearchBar.Text.Trim());
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Make sure it's not a header click?
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                //gets the selected image url
                string imageURL = row.Cells["imageURL"].Value?.ToString();

                double inStock = 0;
                //gets the selected amount in stock value
                var value = row.Cells["amtInStock"].Value;
                if (value != null && double.TryParse(value.ToString(), out double parsed))
                {
                    lblInStock.Text = $"Amount In Stock: {inStock}";
                }
                else
                {
                    lblInStock.Text = "Amount In Stock: N/A";
                }


                if (!string.IsNullOrEmpty(imageURL))
                {
                    try
                    {
                        imgCardUrl.Load(imageURL);
                    }
                    catch
                    {
                        imgCardUrl.Image = null;
                        MessageBox.Show("Unable to load image");
                    }
                }
            }
            else
            {
                imgCardUrl.Image = null;
            }
        }
    }
}

