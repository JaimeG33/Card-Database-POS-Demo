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
using HtmlAgilityPack; //the NuGet package downloaded to scrape websites
using OpenQA.Selenium;//The Selenium packages are nessisary to access tcgplayer data without an api
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace Demo_Application_1
{
    public partial class BuySell : Form
    {
        //For the Selenium and browser stuff
        private IWebDriver driver;
        private WebDriverWait wait;

        //the Connection string from the login
        private string connString;
        //The stored info for later use
        public int selectedCardGame;
        public int selectedConditionId;
        public int selectedCardId;
        public string selectedPriceURL = "";
        public double selectedMktPrice;
        public bool selectedPriceUp2Date = true;

        //public string imgURL = "";
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

            //Setup browser in background
            BackgroundBrowser();
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
            //When the form closes, close the background browser
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }

            Application.Exit();
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
            if (e.KeyCode == System.Windows.Forms.Keys.Enter) //System.Windows.Forms added to prevent mixup with selenium
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

                //gets the selected values (used in pricecheck function)
                selectedPriceURL = row.Cells["mktPriceURL"].Value?.ToString();
                selectedPriceUp2Date = Convert.ToBoolean(row.Cells["priceUp2Date"].Value);
                //Format market price onto the label properly
                string mktPriceRaw = row.Cells["mktPrice"].Value?.ToString();
                if (double.TryParse(mktPriceRaw, out double mktPriceParsed))
                {
                    lblMktPrice.Text = $"Market Price: {mktPriceParsed.ToString("C2", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))}";
                }
                else
                {
                    lblMktPrice.Text = "Market Price: N/A";
                }

                if (selectedPriceUp2Date ==  false )
                    {
                        lblMktPrice.Font = new Font(lblMktPrice.Font, FontStyle.Strikeout);
                    }
                    else
                    {
                        lblMktPrice.Font = new Font(lblMktPrice.Font, FontStyle.Regular);
                    }
                selectedCardId = Convert.ToInt32(row.Cells["cardId"].Value);
                selectedConditionId = Convert.ToInt32(row.Cells["conditionId"].Value?.ToString());

                //gets the selected amount in stock value
                var value = row.Cells["amtInStock"].Value;
                if (value != null && double.TryParse(value.ToString(), out double parsed))
                {
                    lblInStock.Text = $"Amount In Stock: {parsed}";
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

        private void PriceCheck ()
        {
            try
            {
                //Load the TCGPlayer page from the url entered in the textbox
                driver.Navigate().GoToUrl(selectedPriceURL);

                //Find the market price table when its finished loading
                IWebElement priceElement = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.CssSelector("span.price-points__upper__price")));

                //Change the "Market Price" text to the actual market price
                //Unnessisarily complicated cuz the dollar sign was causing issues (will fix later)
                string rawPrice = priceElement.Text.Trim().Replace("$", "").Replace(",", "");
                selectedMktPrice = double.Parse(rawPrice, System.Globalization.CultureInfo.InvariantCulture);
                lblMktPrice.Text = "Market Price: " + selectedMktPrice.ToString("C2", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));


                selectedPriceUp2Date = true;
                lblMktPrice.Font = new Font(lblMktPrice.Font, FontStyle.Regular);

                //Update row in database
                UpdateDBwPriceCheck();
            }
            catch(WebDriverTimeoutException)
            {
                MessageBox.Show("Price not found in time");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void UpdateDBwPriceCheck()
        {
            string choice = cbCardGame.Text;
            string cardGame = $"{choice}Inventory".Trim();
            //Before connecting to database, check to see if valid card game is selected
            List<string> allowedTables = new List<string>
            { "YugiohInventory", "MagicInventory", "PokemonInventory" };
            if (!allowedTables.Contains(cardGame))
            {
                MessageBox.Show("Invalid card game selection.");
                return;
            }
            //set up query
            string query = $"UPDATE [dbo].[{cardGame}] " +
                   "SET mktPrice = @mktPrice, priceUp2Date = 1 " +
                   "WHERE cardId = @cardId AND conditionID = @conditionId AND cardGameId = @cardGameId";

            //Now connect to the database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@mktPrice", selectedMktPrice);
                        cmd.Parameters.AddWithValue("@cardId", selectedCardId);
                        cmd.Parameters.AddWithValue("@conditionId", selectedConditionId);
                        cmd.Parameters.AddWithValue("@cardGameId", selectedCardGame);

                        int rows = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating price: " + ex.Message);
                }
            }

        }

        private void BackgroundBrowser()
        {
            //Open up a chrome browser in the background (needed without TCGPlayer API)
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless"); // Run in background
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");

            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private void btnAddCt_Click(object sender, EventArgs e)
        {
            if (selectedPriceUp2Date == false)
            {
                //remember the selected position
                int currentRowIndex = dataGridView1.CurrentRow?.Index ?? -1;
                //updates the actual database, but doesnt imediately show on the DataGridView
                PriceCheck(); 
                // Refresh table
                LoadInventoryData(tbSearchBar.Text.Trim());
                // Restore position
                if (currentRowIndex >= 0 && currentRowIndex < dataGridView1.Rows.Count)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[currentRowIndex].Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = currentRowIndex;
                }
            }
        }

        private void cbCardGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCardGame.Text == "Yugioh")
            {
                selectedCardGame = 1;
            }
            if (cbCardGame.Text == "Magic")
            {
                selectedCardGame = 2;
            }
            if (cbCardGame.Text == "Pokemon")
            {
                selectedCardGame = 3;
            }
        }
    }
}

