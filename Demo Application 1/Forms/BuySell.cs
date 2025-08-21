using Demo_Application_1.Helpers;
using HtmlAgilityPack; //the NuGet package downloaded to scrape websites
using Microsoft.Win32;
using OpenQA.Selenium;//The Selenium packages are nessisary to access tcgplayer data without an api
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections;
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
        private int selectedAmtInStock;
        private string selectedPriceURL = "";
        private string selectedCardName = "";
        private string selectedRarity = "";
        private double selectedMktPrice;
        private bool selectedPriceUp2Date = true;
        private bool modeBuySell = true;
        private double transactionPrice;
        private double transactionAmt = 1; // Default Value
        private int employeeIdColumn = 10; //make something to change later
        private int registerColumn = 1; //change later
        

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
            //Setup browser in background
            BackgroundBrowser();

            //setup for sale / transaction system
            CheckSeller();
            if (currentSaleStatus !=  null || currentSaleStatus == "finished and paid")
            {
                CheckTransactionId();
            }
            //these if statements are not connected
            if (string.IsNullOrEmpty(currentSaleStatus) && !(workingOnOrder == true))
            {
                GenerateSale();// updates the database and creates a row for the Sale table
            }
            btnFinalizeSale.Visible = false;

            //Form Size
            this.WindowState = FormWindowState.Maximized;
            ImageResizing(); // run ImageResizing Logic
            PositionLabels();

            //Data Grid Security
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridTransactionSystem.ReadOnly = true;
            dataGridTransactionSystem.AllowUserToAddRows = false;
            
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

                //Position tbAmtTraded text box
                tbAmtTraded.Location = new Point(lblMktPrice.Right + 25, lblMktPrice.Bottom + 10);

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
                string imageURL = row.Cells["imageURL"].Value?.ToString().Trim();

                //gets the selected values (used in pricecheck function) 
                selectedAmtInStock = Convert.ToInt32(row.Cells["amtInStock"].Value?.ToString());
                selectedPriceURL = row.Cells["mktPriceURL"].Value?.ToString();
                selectedPriceUp2Date = Convert.ToBoolean(row.Cells["priceUp2Date"].Value);
                selectedRarity = row.Cells["rarity"].Value?.ToString();
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
                    try //the old (simple) method got blocked by tcgplayer
                    {
                        // Create a new HTTP request to the image URL
                        var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(imageURL);
                        // Spoof a browser User-Agent to avoid being blocked by the server (403 Forbidden)
                        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)";
                        // Set a Referer header to make the request appear as if it's coming from a browser visiting TCGPlayer
                        request.Referer = "https://www.tcgplayer.com/";
                        // Accept header tells the server what types of content we want (images mostly)
                        request.Accept = "image/webp,image/apng,image/*,*/*;q=0.8";
                        // Send the request and get the response (which should contain the image data)
                        using (var response = request.GetResponse())
                        // Get the image stream from the response
                        using (var stream = response.GetResponseStream())
                        {
                            // Convert the stream into an Image object and display it in the PictureBox
                            imgCardUrl.Image = Image.FromStream(stream);
                        }
                    }
                    catch (Exception ex) 
                    {
                        imgCardUrl.Image = null;
                        MessageBox.Show($"Unable to load image.\n\nError: {ex.Message}", "Image Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (clickedColumn == "priceUp2Date")
                {
                    //check to see if the selected row is up to date
                    bool isUp2Date = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["priceUp2Date"].Value);
                    if (isUp2Date == false)
                    {
                        UpdatePrice();
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

        private void btnAddCt_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Left click = normal add to cart (sell item to customer) (default)
                modeBuySell = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Right click = buy item from customer 
                modeBuySell = false;
                btnAddCt_Click(sender, EventArgs.Empty); // Need to manually run the on_click function because right clicking it doesnt run it
            }
        }
        private void btnAddCt_Click(object sender, EventArgs e)
        {
            UpdatePrice(); //First update the price if needed to make sure its accurate

            // Check to see if the item is being bought or sold
            decimal finalValue = Convert.ToDecimal(transactionPrice);
            if (modeBuySell == false)
            {
                finalValue = finalValue * -1;
            }
            // 1. Collect input from form (dropdowns, textboxes, etc.)
            TransactionLineItem newItem = new TransactionLineItem
            {
                CardGameId = selectedCardGame,
                CardId = selectedCardId,
                ConditionId = selectedConditionId,
                CardName = selectedCardName,
                Rarity = selectedRarity,
                SetId = selectedSetId,
                TimeMktPrice = (decimal)selectedMktPrice, 
                AgreedPrice = finalValue, 
                AmtTraded = (int)transactionAmt, 
                BuyOrSell = modeBuySell // true = store is selling,         false = store is buying item from customer
            };

            // 2. Add to cart
            cartItems.Add(newItem);

            // 3. Refresh the DataGridView
            if(currentSaleStatus == "pre-prep")
            {
                currentSaleStatus = "taking order";

            }
            btnFinalizeSale.Visible = true;
            SaleTransactionLineSystem();
            dataGridTransactionSystem.ReadOnly = true;
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

        private void tbAmtTraded_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits, backspace, and delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block everything else
            }
        }
        private void tbAmtTraded_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbAmtTraded.Text))
            {
                transactionAmt = Convert.ToInt16(tbAmtTraded.Text);
            }
            else if (Convert.ToInt16(tbAmtTraded.Text) == 0)
            {
                transactionAmt = 1;
            }
            else
            {
                transactionAmt = 1;
            }
            
        }


        //Stuff for the Sale / Transaction system
        private int currentSaleId;
        private int currentTransactionId;
        private string currentSaleStatus;
        private bool workingOnOrder;
        private DateTime selectedSellerDateTime;
        private decimal saleTotal;
        private void CheckTransactionId()
        {
            //the query that will be used to enter into sql server (not finished)
            string query = @"
SELECT TOP 1 transactionId
FROM dbo.TransactionLine
WHERE saleId = @saleId
ORDER BY transactionId DESC;";
            using(SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using(SqlCommand cmd = new SqlCommand (query, connection))
                {
                    cmd.Parameters.AddWithValue("@saleId", currentSaleId);//fills in the blanks / finishes the query

                    object result = cmd.ExecuteScalar();//gets the result of the entered query
                    if (result != null && result != DBNull.Value)
                    {
                        currentTransactionId = Convert.ToInt32(result) + 1;
                    }
                    else
                    {
                        currentTransactionId = 1; 
                    }
                }
            }

        }
        private void CheckSeller()
        {
            int saleId = 1; //Default if no sales today
            int employeeId = 10; //Fix later, just keep 10 for now
            //Get current date / time
            DateTime currentDateTime = DateTime.Now;
            DateTime currentDate = DateTime.Now.Date;

            //Queries to be used later
            string queryFindLatestSaleId = "SELECT ISNULL(MAX(saleId), 0) FROM dbo.Sale;";

            string queryCheckLastSale = @" 
        SELECT TOP 1 saleId, orderStatus, saleDate
        FROM dbo.Sale
        WHERE employeeId = @employeeId
        ORDER BY saleDate DESC;
                                        ";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                // find the latest SaleId
                using(SqlCommand cmd1 = new SqlCommand(queryFindLatestSaleId, connection))
                {
                    object result = cmd1.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        saleId = Convert.ToInt32(result);
                    }
                }

                // then check if the user has any unfinished orders
                using(SqlCommand cmd2 = new SqlCommand(queryCheckLastSale, connection))
                {
                    cmd2.Parameters.AddWithValue("@employeeId", employeeId);

                    using (SqlDataReader reader = cmd2.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string lastStatus = reader["orderStatus"] != DBNull.Value
                                ? reader["orderStatus"].ToString() : null;
                            //check back here
                            // If last sale is null or marked "finished and paid", treat it as done
                            if (string.IsNullOrEmpty(lastStatus) || lastStatus == "finished and paid")
                            {
                                currentSaleId = saleId + 1;
                                currentSaleStatus = null;
                                workingOnOrder = false;
                                selectedSellerDateTime = currentDateTime;
                            }                           
                            else
                            {
                                // Still working on previous sale
                                currentSaleId = Convert.ToInt32(reader["saleId"]);
                                currentSaleStatus = lastStatus;
                                workingOnOrder = true;
                                selectedSellerDateTime = Convert.ToDateTime(reader["saleDate"]);

                            }

                            return;
                        }
                    }
                }
                // 3. If employee has never made a sale, start new one
                //doesnt get to happen if the return function runs
                currentSaleId = saleId + 1;
                currentSaleStatus = null;
                workingOnOrder = false;
                selectedSellerDateTime = currentDateTime;
            }
         
        }
        private void GenerateSale()
        {
            DateTime currentDateTime = DateTime.Now;
            int employeeIdColumn = 10; //Make a funciton to get this later
            int registerColumn = 1; //Make a funciton to get this later
            int saleIdColumn = currentSaleId;

            if (!workingOnOrder == true)
            {
                currentSaleStatus = "pre-prep";

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
                workingOnOrder = true;
            }
            else if (workingOnOrder == true)
            {

            }
            
        }
        private List<TransactionLineItem> cartItems = new List<TransactionLineItem>(); //The list representing the selected items added to cart 
        public class TransactionLineItem //(TransactioLine)
        {
            public int CardGameId { get; set; }
            public int CardId { get; set; }
            public int ConditionId { get; set; }
            public string CardName { get; set; }
            public int SetId { get; set; }
            public decimal TimeMktPrice { get; set; }
            public decimal AgreedPrice { get; set; }
            public int AmtTraded { get; set; }
            public bool BuyOrSell { get; set; } //True = Sold to customer, False = Store bought card from customer (set to True for now)
            public string Rarity { get; set; }
        }
        
        private void SaleTransactionLineSystem()
        {
            int employeeIdColumn = 10; //Make a funciton to get this later
            int registerColumn = 1; //Make a funciton to get this later

            DataTable dt = new DataTable();
            dt.Columns.Add("Type");              // "Sale" or "Item"
            dt.Columns.Add("Card Name");
            dt.Columns.Add("Rarity");
            dt.Columns.Add("Set ID");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Agreed Price");
            dt.Columns.Add("Market Price");
            dt.Columns.Add("Total");

            //This is a Sale Info type of row (at the top of the grid)
            dt.Rows.Add("Sale Info", $"Sale ID: {currentSaleId}", $"DateTime: {selectedSellerDateTime}" , $"Employee ID: {employeeIdColumn}", "", "",
                $"Status: {currentSaleStatus ?? "N/A"}", $"Register: {registerColumn}");

            foreach (var item in cartItems) // cartItems is List<TransactionLineItem>
            {
                decimal total = item.AgreedPrice * item.AmtTraded;
                //This is an Item type of row (TransactionLine)
                dt.Rows.Add("Item", item.CardName, item.Rarity , item.SetId, item.AmtTraded, item.AgreedPrice.ToString("C2"),
                    item.TimeMktPrice.ToString("C2"), total.ToString("C2"));
            }
            //Stuff at the bottom of the row
            decimal grandTotal = cartItems.Sum(i => i.AgreedPrice * i.AmtTraded);
            dt.Rows.Add("TOTAL", "", "", "", "", "", "", grandTotal.ToString("C2"));
            saleTotal = grandTotal;

            dataGridTransactionSystem.DataSource = dt;

            //Only allow the user to edit the Agreed Price column
            for (int i = 0; i < dataGridTransactionSystem.Rows.Count; i++)
            {
                var row = dataGridTransactionSystem.Rows[i];
                var cellValue = row.Cells[0].Value;
                if (cellValue != null && cellValue.ToString() == "Item")
                {
                    row.Cells["Agreed Price"].ReadOnly = false;
                }
                else
                {
                    row.ReadOnly = true;
                }
            }


            //change colors of certain rows to make it easier to read
            dataGridTransactionSystem.Rows[0].DefaultCellStyle.BackColor = Color.LightBlue; // Sale Info
            dataGridTransactionSystem.Rows[dataGridTransactionSystem.Rows.Count - 1].DefaultCellStyle.
                BackColor = Color.LightGray; // Total
            //Quantity check to see if the amount of cards requested is more than the amount in stock


            //also prevent user from adding new rows manually (might allow later, idk yet)
            dataGridTransactionSystem.AllowUserToAddRows = false;

        }
        
        private void dataGridTransactionSystem_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridTransactionSystem.Rows[e.RowIndex];
            if (row.Cells[0].Value.ToString() != "Item") return; // Ignore non-item rows

            string cardName = row.Cells["Card Name"].Value.ToString();
            int setId = Convert.ToInt32(row.Cells["Set ID"].Value);

            var item = cartItems.FirstOrDefault(i => i.CardName == cardName && i.SetId == setId);
            if (item == null) return;

            // Try updating quantity (AmtTraded)
            if (int.TryParse(row.Cells["Qty"].Value?.ToString(), out int newQty))
            {
                item.AmtTraded = newQty;
            }

            // Try parsing Agreed Price
            string priceText = row.Cells["Agreed Price"].Value.ToString();
            decimal newPrice;

            if (decimal.TryParse(priceText, out newPrice))
            {
                item.AgreedPrice = newPrice;
            }
            else if (priceText.StartsWith("$") &&
                     decimal.TryParse(priceText.TrimStart('$'), out decimal parsedPrice))
            {
                item.AgreedPrice = parsedPrice;
            }
            else
            {
                MessageBox.Show("Please enter a valid decimal price.");
                row.Cells["Agreed Price"].Value = ""; // Reset invalid input
                return;
            }

            // Update total for the row
            decimal total = item.AgreedPrice * item.AmtTraded;
            row.Cells["Total"].Value = total.ToString("C2");

            // Update grand total
            decimal grandTotal = cartItems.Sum(i => i.AgreedPrice * i.AmtTraded);
            var totalRow = dataGridTransactionSystem.Rows[dataGridTransactionSystem.Rows.Count - 1];
            totalRow.Cells["Total"].Value = grandTotal.ToString("C2");

            // Optional: Force UI refresh of the grid
            dataGridTransactionSystem.InvalidateRow(e.RowIndex); // Only refresh the edited row
            // Or use: dataGridTransactionSystem.Refresh(); // Full grid refresh (heavier)
        }

        private void dataGridTransactionSystem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridTransactionSystem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return; // Ignore header clicks
            
            var clickedRow = dataGridTransactionSystem.Rows [e.RowIndex];
            var clickedColumn = dataGridTransactionSystem.Columns [e.ColumnIndex];

            var clickedCell = dataGridTransactionSystem.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string lineType = clickedRow.Cells["Type"].Value?.ToString();
            //only allow certain rows / columns to be edited
            if (lineType == "Item" && (clickedColumn.Name == "Agreed Price" || clickedColumn.Name == "Qty") )
            {
                dataGridTransactionSystem.ReadOnly = false;
                clickedRow.Cells[clickedColumn.Name].ReadOnly = false;
                dataGridTransactionSystem.BeginEdit(true);
            }
            else if (lineType == "TOTAL" && clickedColumn.Name == "Total")
            {
                dataGridTransactionSystem.ReadOnly = false;
                clickedRow.Cells["Total"].ReadOnly = false;
                dataGridTransactionSystem.BeginEdit(true);
            }
            else if(clickedCell is DataGridViewButtonCell && clickedCell.Value?.ToString() == "Finalize Sale")
            {
                
            }
            else
            {
                dataGridTransactionSystem.ReadOnly = true;
            }

        }

        private void TransactionLineLogic()
        {
            int transactionId = currentTransactionId; //default

            // Use the cartItems list to write sql queries to insert into the database
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                // Step 1: TractionLine Table
                foreach (var item in cartItems) // Loop through all entries in the cartItems list to write them down as lines of the sql query
                {
                    string query = @"
                INSERT INTO TransactionLine 
                (transactionId, saleId, cardGameId, cardId, conditionId, cardName, rarity, setId, timeMktPrice, agreedPrice, amtTraded, buyOrSell)
                VALUES 
                (@transactionId, @saleId, @cardGameId, @cardId, @conditionId, @cardName, @rarity, @setId, @timeMktPrice, @agreedPrice, @amtTraded, @buyOrSell)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@transactionId", currentTransactionId);
                        ++currentTransactionId;
                        cmd.Parameters.AddWithValue("@saleId", currentSaleId);
                        cmd.Parameters.AddWithValue("@cardGameId", item.CardGameId); //remember, this is the number value, not the text
                        cmd.Parameters.AddWithValue("@cardId", item.CardId);
                        cmd.Parameters.AddWithValue("@conditionId", item.ConditionId);
                        cmd.Parameters.AddWithValue("@cardName", item.CardName);
                        cmd.Parameters.AddWithValue("@rarity", item.Rarity);
                        cmd.Parameters.AddWithValue("@setId", item.SetId);
                        cmd.Parameters.AddWithValue("@timeMktPrice", item.TimeMktPrice);
                        cmd.Parameters.AddWithValue("@agreedPrice", item.AgreedPrice);
                        cmd.Parameters.AddWithValue("@amtTraded", item.AmtTraded);
                        cmd.Parameters.AddWithValue("@buyOrSell", item.BuyOrSell);

                        cmd.ExecuteNonQuery();
                    }
                }

                //Step 2: Update amtInStock columns of affected _Inventory tables
                // Assemble values of cartItems into batches, then create another sql query to update the amtInStock collumns of the affected tables
                var grouped = cartItems.GroupBy(x => x.CardGameId);

                foreach (var group in grouped)
                {
                    string tableName;
                    switch (group.Key)
                    {
                        case 1: // Yugioh
                            tableName = "YugiohInventory";
                            break;
                        case 2: // Magic
                            tableName = "MagicInventory";
                            break;
                        case 3: // Pokemon
                            tableName = "PokemonInventory";
                            break;
                        default:
                            continue; // Skip if not a valid card game
                    }

                    // Assemble sql querys to update the amtInStock collumns of the affected tables
                    var sb = new StringBuilder();
                    var idList = new List<int>(); // Keep track of all cardIds for WHERE clause

                    // Start building UPDATE query
                    sb.Append($"UPDATE {tableName} SET amtInStock = amtInStock + CASE cardId ");

                    // CASE WHEN for each item in the group
                    foreach (var item in group)
                    {
                        // If buyOrSell = true → shop sold → decrease stock
                        // If buyOrSell = false → shop bought → increase stock
                        int stockChange = item.BuyOrSell ? -item.AmtTraded : item.AmtTraded;

                        // Add a CASE entry: WHEN {cardId} THEN {amount to add/subtract}
                        sb.Append($"WHEN {item.CardId} THEN {stockChange} ");

                        // Track cardId for the WHERE clause
                        idList.Add(item.CardId);
                    }

                    // Close out the CASE expression and add WHERE with all cardIds
                    sb.Append("END WHERE cardId IN (");
                    sb.Append(string.Join(",", idList)); // e.g., (111,222,333)
                    sb.Append(");");

                    // Execute the single batched UPDATE statement
                    using (SqlCommand cmdUpdate = new SqlCommand(sb.ToString(), connection))
                    {
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
            // Step 3: Refresh DataGridView
            // Update the DataGridView to show the new transaction lines in the UI
            currentSaleStatus = "processing";
            // The Status column is the 6th column (index 6)
            dataGridTransactionSystem.Rows[0].Cells[6].Value = $"Status: {currentSaleStatus ?? "N/A"}";
            MessageBox.Show("Transaction lines saved successfully.");
        }
        private void UpdateSaleStatus()
        {
            string query = @"
UPDATE dbo.Sale
SET orderStatus = @orderStatus
WHERE saleId = @saleId;"; // took out: AND CAST(saleDate AS DATE) = CAST(@saleDate AS DATE)
            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@orderStatus", currentSaleStatus);
                    cmd.Parameters.AddWithValue("@saleId", currentSaleId);
                    //cmd.Parameters.AddWithValue("@saleDate", selectedSellerDateTime);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    connection.Close();


                    MessageBox.Show($"{rowsAffected} sale(s) updated to status '{currentSaleStatus}'.\n" +
                        $"This is where you should go collect the cards in the back");
                }
            }
        }
        private void CalcSale()
        {
            currentSaleStatus = "finished and paid";
            string query = @"
UPDATE dbo.Sale
SET 
    orderStatus = @orderStatus,
    profit = @saleTotal
WHERE 
    saleId = @saleId;"; // took out: AND CAST(saleDate AS DATE) = CAST(@saleDate AS DATE)
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open ();
                using(SqlCommand cmd = new SqlCommand(query,connection))
                {
                    cmd.Parameters.AddWithValue("@orderStatus", currentSaleStatus);
                    cmd.Parameters.AddWithValue("@saleTotal", saleTotal);
                    cmd.Parameters.AddWithValue("@saleId", currentSaleId);

                    cmd.ExecuteNonQuery();//finaly, hopefully this will actually work
                }
            }
            MessageBox.Show($"Order {currentSaleId} has been filled. Returning to home now \n" +
                $"{currentSaleStatus}");
        }
        private void btnFinalizeSale_Click(object sender, EventArgs e)
        {
            TransactionLineLogic();
            UpdateSaleStatus();
            CalcSale();

            workingOnOrder = false;
            changingTabs = true;
            NavigationHelper.ReturnToHome(this, _homePage, ref changingTabs);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

