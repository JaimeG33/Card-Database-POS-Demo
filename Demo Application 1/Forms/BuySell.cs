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
using Demo_Application_1.Helpers;
using HtmlAgilityPack; //the NuGet package downloaded to scrape websites
using Microsoft.Win32;
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
        private int selectedCardGame;
        private int selectedConditionId;
        private int selectedCardId;
        private int selectedSetId;
        private string selectedPriceURL = "";
        private string selectedCardName = "";
        private double selectedMktPrice;
        private bool selectedPriceUp2Date = true;
        private double transactionPrice;
        
        

        //Stuff for returning to HomePage
        private HomePage _homePage;
        private bool changingTabs = false;

        public BuySell(string connectionString, HomePage homePage)//Constructor must be updated to accept HomePage reference
        {
            InitializeComponent();
            connString = connectionString;
            _homePage = homePage;
        }

        private void BuySell_Load(object sender, EventArgs e)
        {
            //setup for sale / transaction system
            CheckSeller();
            if (string.IsNullOrEmpty(currentSaleStatus) )
            {
                GenerateSale();
            }

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

            //HomePage reset
            changingTabs = false;
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

                //Position tbPrice text box
                tbPrice.Location = new Point(lblMktPrice.Left, lblMktPrice.Bottom + 10);

                //Position add2cart button
                btnAddCt.Location = new Point(tbPrice.Left, tbPrice.Bottom + 10);
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
            if (changingTabs == false)
            {
                Application.Exit();
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
                // Get the column that was clicked
                string clickedColumn = dataGridView1.Columns[e.ColumnIndex].Name;
                // Select the row that was clicked
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                //gets the selected image url
                string imageURL = row.Cells["imageURL"].Value?.ToString();

                //gets the selected values (used in pricecheck function)
                selectedPriceURL = row.Cells["mktPriceURL"].Value?.ToString();
                selectedPriceUp2Date = Convert.ToBoolean(row.Cells["priceUp2Date"].Value);
                //Format market price onto the label properly and record value
                string mktPriceRaw = row.Cells["mktPrice"].Value?.ToString();
                
                if (double.TryParse(mktPriceRaw, System.Globalization.NumberStyles.Currency,
                System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), out double mktPriceParsed))
                {
                    selectedMktPrice = mktPriceParsed;
                    TransactionPriceLogic();
                    lblMktPrice.Text = $"Market Price: {mktPriceParsed.ToString("C2", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))}";
                    tbPrice.Text = transactionPrice.ToString("C2");
                }
                else
                {
                    lblMktPrice.Text = "Market Price: N/A";
                }

                if (selectedPriceUp2Date ==  false )
                    {
                        lblMktPrice.Text = lblMktPrice.Text + " (Price not up to Date)";
                    }

                selectedCardId = Convert.ToInt32(row.Cells["cardId"].Value);
                selectedConditionId = Convert.ToInt32(row.Cells["conditionId"].Value?.ToString());
                selectedSetId = Convert.ToInt32(row.Cells["setId"].Value?.ToString());
                selectedCardName = row.Cells["cardName"].Value?.ToString();

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
                if (clickedColumn == "priceUp2Date")
                {
                    //check to see if the selected row is up to date
                    bool isUp2Date = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["priceUp2Date"].Value);
                    if (isUp2Date == false)
                    {
                        UpdatePrice();
                        //lblMktPrice.BackColor = Color.LightYellow;
                        //await Task.Delay(1500);
                        //lblMktPrice.BackColor = SystemColors.AppWorkspace;
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
                TransactionPriceLogic();
                tbPrice.Text = transactionPrice.ToString("C2");

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
            UpdatePrice();

            // 1. Collect input from your form (dropdowns, textboxes, etc.)
            // For this example, we’ll hardcode values to test
            TransactionLineItem newItem = new TransactionLineItem
            {
                CardGameId = selectedCardGame,
                CardId = selectedCardId,
                ConditionId = selectedConditionId,
                CardName = selectedCardName,
                SetId = selectedSetId,
                TimeMktPrice = (decimal)selectedMktPrice, //convert to decimal for now
                AgreedPrice = (decimal)transactionPrice, // same, should fix later
                AmtTraded = 1, //add amount funtionality later
                BuyOrSell = true // true = store is selling
            };

            // 2. Add to cart
            cartItems.Add(newItem);

            // 3. Refresh the DataGridView
            SaleTransactionLineSystem();
        }
        private void UpdatePrice()
        {
            if (selectedPriceUp2Date == false)
            {
                lblMktPrice.BackColor = Color.LightYellow; // change the color so the user knows the program is loading and not frozen
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
                lblMktPrice.BackColor = SystemColors.AppWorkspace; // return the color back to normal
            }
        }
        private void TransactionPriceLogic()
        {
            double mathPrice = selectedMktPrice;
            
            if (mathPrice < 0.18)
            {
                transactionPrice = 0.10;
            }
            else if (mathPrice > 0.18 && mathPrice < 0.80)
            {
                // Round to nearest quarter (0.25)
                transactionPrice = Math.Round(mathPrice * 4, MidpointRounding.AwayFromZero) / 4;
            }
            else if (mathPrice >= 0.80 && mathPrice < 5)
            {
                // Ceiling = round up, (this rounds up to the nearest 0.50)
                transactionPrice = Math.Ceiling(mathPrice * 2) / 2;
            }
            else if (mathPrice >= 5 && mathPrice < 18)
            {
                // (round up to the nearest dollar)
                transactionPrice = Math.Ceiling(mathPrice);
            }
            else if (mathPrice >= 18 && mathPrice < 20)
            {
                transactionPrice = 20;
            }
            else if (mathPrice >= 20 && mathPrice < 35)
            {
                //just regular rounding
                transactionPrice = Math.Round(mathPrice);
            }
            else if (mathPrice >= 35 )
            {
                // round to the nearest whole 5
                transactionPrice = Math.Round(mathPrice / 5) * 5;
            }
            else
            {
                //round up to the next whole number
                transactionPrice = Math.Ceiling(mathPrice);
            }

            
        }
        private void cbCardGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCardGame.Text == "Yugioh")
            {
                selectedCardGame = 1;
                panel4.BackColor = Color.DarkOrange;
                panelTransSales.BackColor = Color.DarkOrange;
            }
            if (cbCardGame.Text == "Magic")
            {
                selectedCardGame = 2;
                panel4.BackColor = Color.LightGray;
                panelTransSales.BackColor = Color.LightGray;
            }
            if (cbCardGame.Text == "Pokemon")
            {
                selectedCardGame = 3;
                panel4.BackColor = Color.Red;
                panelTransSales.BackColor = Color.Red;
            }

            if (tbSearchBar.Text != null && tbSearchBar.TextLength > 1)
            {
                LoadInventoryData(tbSearchBar.Text.Trim());
            }
        }
        //Return to home screen button
        private void cbxProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxProfile.Text == "Return Home")
            {
                changingTabs = true;
                NavigationHelper.ReturnToHome(this, _homePage, ref changingTabs);
            }
        }
        //Stuff for selecting the agreed apon price of the transaction
        private void tbPrice_MouseClick(object sender, MouseEventArgs e)
        {
            UpdatePrice();
        }
        private void tbPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits, backspace, and delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block everything else
            }
        }

        private void tbPrice_TextChanged(object sender, EventArgs e)
        {
            // temporarily remove handlers to prevent recursion
            tbPrice.TextChanged -= tbPrice_TextChanged;
            // only didgets allowed
            string raw = new string (tbPrice.Text.Where(char.IsDigit).ToArray());

            if (double.TryParse(raw, out double value))
            {
                value /= 100; //convert to dollars
                tbPrice.Text = value.ToString("C2", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                tbPrice.SelectionStart = tbPrice.Text.Length;

                transactionPrice = value;
            }
            else
            {
                tbPrice.Text = "$0.00";
                tbPrice.SelectionStart = tbPrice.Text.Length;
            }
            // Reattach handler
            tbPrice.TextChanged += tbPrice_TextChanged;
        }

        //Stuff for the Sale / Transaction system
        private int currentSaleId;
        private string currentSaleStatus;
        private void CheckSeller()
        {
            int saleId = 1; //Default if no sales today
            //Get current date / time
            DateTime currentDate = DateTime.Now.Date;

            //Find which saleId the user is currently on:
            string query = @"
                            SELECT MAX(saleId)
                            FROM dbo.Sale
                            WHERE CAST(saleDate AS DATE) = @TodayDate;
                            ";

            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                //List all sales made today
                cmd.Parameters.AddWithValue("@TodayDate", currentDate);
                connection.Open();

                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    saleId = Convert.ToInt32(result) + 1;
                }
                currentSaleId = saleId;
            }          
        }
        private void GenerateSale()
        {
            DateTime currentDateTime = DateTime.Now;
            int employeeIdColumn = 10; //Make a funciton to get this later
            int registerColumn = 1; //Make a funciton to get this later
            currentSaleStatus = "pre-prep";
            int saleIdColumn = currentSaleId;

            string query = @"
    INSERT INTO dbo.Sale (
        saleDate,
        saleId,
        employeeId,
        orderStatus,
        register
    )
    VALUES (
        @saleDate,
        @saleId,
        @employeeId,
        @orderStatus,
        @register
    );
";
            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@saleDate", currentDateTime);
                cmd.Parameters.AddWithValue("@saleId", saleIdColumn);
                cmd.Parameters.AddWithValue("@employeeId", employeeIdColumn);
                cmd.Parameters.AddWithValue("@orderStatus", currentSaleStatus);
                cmd.Parameters.AddWithValue("@register", registerColumn);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        private List<TransactionLineItem> cartItems = new List<TransactionLineItem>();
        public class TransactionLineItem
        {
            public int CardGameId { get; set; }
            public int CardId { get; set; }
            public int ConditionId { get; set; }
            public string CardName { get; set; }
            public int SetId { get; set; }
            public decimal TimeMktPrice { get; set; }
            public decimal AgreedPrice { get; set; }
            public int AmtTraded { get; set; }
            public bool BuyOrSell { get; set; }
        }
        
        private void SaleTransactionLineSystem()
        {
            int employeeIdColumn = 10; //Make a funciton to get this later
            int registerColumn = 1; //Make a funciton to get this later

            DataTable dt = new DataTable();
            dt.Columns.Add("Type");              // "Sale" or "Item"
            dt.Columns.Add("Card Name");
            dt.Columns.Add("Set ID");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Agreed Price");
            dt.Columns.Add("Market Price");
            dt.Columns.Add("Total");

            dt.Rows.Add("Sale Info", $"Sale ID: {currentSaleId}", $"Employee ID: {employeeIdColumn}", "", "", "", $"Register: {registerColumn}");

            foreach (var item in cartItems) // cartItems is List<TransactionLineItem>
            {
                decimal total = item.AgreedPrice * item.AmtTraded;
                dt.Rows.Add("Item", item.CardName, item.SetId, item.AmtTraded, item.AgreedPrice, item.TimeMktPrice, total);
            }

            decimal grandTotal = cartItems.Sum(i => i.AgreedPrice * i.AmtTraded);
            dt.Rows.Add("TOTAL", "", "", "", "", "", grandTotal);

            dataGridTransactionSystem.DataSource = dt;

            dataGridTransactionSystem.ReadOnly = true;
            dataGridTransactionSystem.Rows[0].DefaultCellStyle.BackColor = Color.LightBlue; // Sale Info
            dataGridTransactionSystem.Rows[dataGridTransactionSystem.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray; // Total

        }

    }
}

