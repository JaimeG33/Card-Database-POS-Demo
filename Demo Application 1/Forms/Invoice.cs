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
    public partial class Invoice : Form
    {
        // State variables for handling navigation and database connection
        private HomePage _homePage; // Reference to the HomePage form
        private bool changingTabs = false; // Tracks if the tab is being changed
        private string connString; // Database connection string
        public Invoice(string connectionString, HomePage homePage)
        {
            InitializeComponent();
            // Save the database connection string and reference to the HomePage form
            connString = connectionString;
            _homePage = homePage;
        }
        private void cbxProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxProfile.Text == "Return Home")
            {
                changingTabs = true; // Set tab-changing flag
                NavigationHelper.ReturnToHome(this, _homePage, ref changingTabs); // Navigate back to HomePage
            }
        }
        private void Invoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Logic for when the form is closed
            if (changingTabs == false)
            {
                Application.Exit(); // Ensure the application is terminated
            }
        }


        private void Invoice_Load(object sender, EventArgs e)
        {
            // Maximize the form when it loads
            this.WindowState = FormWindowState.Maximized;
            LoadInvoices(); // Load invoices into the data grid view so the user can view existing invoices
            // Ensure the user cannot edit the DataGridView directly
            dgvInvoices.ReadOnly = true;
        }

        // Important variables
        public int invId; // Invoice ID, should be unique each time a new invoice is created
        public int qty; // Quantity of the item purchased in the invoice
        public string itemName; // Name of the item purchased in the invoice (should match a value from the _Inventory table)
        public int cardId; // Unique ID of the item that was purchased (should match the value from the _Inventory table)
        public int setId = 0; // Unique ID of the set that the item belongs to (should match the value from the _Set table)
        public int cardGameId; // Unique ID of the card game that the set belongs to (should match the value selected from the cbCardGame combo box)
        public decimal costIndv; // Cost of a single item purchased in the invoice
        public decimal costTotal; // Total cost of the invoice (costIndv * qty)
        public DateTime date; // Date the items arive in the store
        public int amtKept; // amount of the item that was kept after the invoice was ordered and delivered (in case the store opens them or moves it to another location)
        public string imageURL; // URL of the image of the item purchased in the invoice (will display somewhere on the form)

        public string dataGridViewMode; // What the datagridview is currently displaying (All Invoices, Search Results, etc.)




        // Loads all invoices from the Invoice table and displays them in the DataGridView
        private void LoadInvoices()
        {
            dataGridViewMode = "Full Invoices"; // Implies all columns for the Invoice table are being shown
            // SQL query to select all invoices
            string query = "SELECT * FROM Invoice";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvInvoices.DataSource = dt; // Assumes you have a DataGridView named dgvInvoices
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoices: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // When a row in the DataGridView is selected, fill in the important variables
        private void SelectedRow()
        {
            // Check if a row is selected
            if (dgvInvoices.CurrentRow != null)
            {
                var row = dgvInvoices.CurrentRow;
                // Assign values from the selected row to the class variables
                invId = Convert.ToInt32(row.Cells["invId"].Value);
                qty = Convert.ToInt32(row.Cells["qty"].Value);
                itemName = row.Cells["itemName"].Value?.ToString();
                cardId = Convert.ToInt32(row.Cells["cardId"].Value);
                setId = Convert.ToInt32(row.Cells["setId"].Value);
                cardGameId = Convert.ToInt32(row.Cells["cardGameId"].Value);
                costIndv = Convert.ToDecimal(row.Cells["costIndv"].Value);
                costTotal = Convert.ToDecimal(row.Cells["costTotal"].Value);
                date = row.Cells["date"].Value != DBNull.Value ? Convert.ToDateTime(row.Cells["date"].Value) : default(DateTime);
                amtKept = Convert.ToInt32(row.Cells["amtKept"].Value);
                imageURL = row.Cells["imageURL"].Value?.ToString();

                // Highlight the selected row
                row.Selected = true;

                // Display itemName in lblSelectedItem
                lblSelectedItem.Text = itemName ?? "";

                // Display image in pictureBoxItem
                if (pictureBoxItem != null)
                {
                    if (!string.IsNullOrEmpty(imageURL))
                    {
                        try
                        {
                            pictureBoxItem.Load(imageURL);
                        }
                        catch
                        {
                            pictureBoxItem.Image = null; // Clear if load fails
                        }
                    }
                    else
                    {
                        pictureBoxItem.Image = null;
                    }
                }

                // Make btnSeeSales visible
                btnSeeSales.Visible = true;
            }
        }
        // Different row select method for finding SetId
        private void SelectedSetRow()
        {
            // Check if a row is selected
            if (dgvInvoices.CurrentRow != null)
            {
                var row = dgvInvoices.CurrentRow;
                // Assign values from the selected row to the class variables
                setId = Convert.ToInt32(row.Cells["setId"].Value);
                // Optionally, you can also get setName if needed
                // string setName = row.Cells["setName"].Value?.ToString();
                // Highlight the selected row
                row.Selected = true;

                // Then display on the label
                lblSelectedSet.Text = setId.ToString();
            }
        }
        // Different row select method for  Item results
        private void SelectedItemRow()
        {
            // Check if a row is selected
            if (dgvInvoices.CurrentRow != null)
            {
                var row = dgvInvoices.CurrentRow;
                // Assign values from the selected row to the class variables
                cardId = Convert.ToInt32(row.Cells["cardId"].Value);
                itemName = row.Cells["cardName"].Value?.ToString();
                imageURL = row.Cells["imageURL"].Value?.ToString();
                // Highlight the selected row
                row.Selected = true;
                // Then display on the label or PictureBox as needed
                lblSelectedItem.Text = itemName;
                if (pictureBoxItem != null && !string.IsNullOrEmpty(imageURL))
                {
                    pictureBoxItem.Load(imageURL); // Load image into PictureBox
                }
            }
        }

        // Starts the process of creating a new invoice
        private void btnBeginNewOrder_Click(object sender, EventArgs e)
        {
            // Make the new order panel visible
            flpNewOrder.Visible = true;

            // Reset all invoice variables for a fresh start
            invId = 0;
            qty = 0;
            itemName = null;
            cardId = 0;
            setId = 0;
            cardGameId = 0;
            costIndv = 0;
            costTotal = 0;
            date = default(DateTime);
            amtKept = 0;
            imageURL = null;

            // Clear the DataGridView and set mode
            dgvInvoices.DataSource = null;
            dgvInvoices.Rows.Clear();
            dataGridViewMode = "None"; // No results shown yet
        }

        // Find the Set the user is ordering from
        private void FindSet() // btnSearchSet_Click
        {
            string selectedTable = cbCardGame.Text + "Set";
            string setName = "%" + tbSelectSet.Text + "%"; // Assumes you have a TextBox named tbSelectSet

            string query = $"SELECT setId, setName FROM [{selectedTable}] WHERE setName LIKE @setName";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@setName", setName);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Display results in the DataGridView
                            dgvInvoices.DataSource = dt;
                            dgvInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Expand columns to fill space
                            dataGridViewMode = "Set Results";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error finding set: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSearchSet_Click(object sender, EventArgs e)
        {
            if (cbCardGame != null)
            {
                FindSet();
            }
            else
            {
                MessageBox.Show("Please select a card game before searching for a set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Step 1: After finding the set, store the card game ID and show the next step panel
        private void CreateNewInvoice1() // btnStep2_Click
        {
            // Store the card game ID (could be index or value from database)
            if (cbCardGame.Text == "Yugioh")
            {
                cardGameId = 1;
            }
            else if (cbCardGame.Text == "Magic")
            {
                cardGameId = 2;
            }
            else if (cbCardGame.Text == "Pokemon")
            {
                cardGameId = 3;
            }
            else
            {
                MessageBox.Show("Please select a valid card game.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Make the next step panel visible
            flpAddStep2.Visible = true; // Assumes you have a FlowLayoutPanel named flpAddStep2
        }
        private void btnStep2_Click(object sender, EventArgs e)
        {
            if (setId != 0)
            {
                CreateNewInvoice1();
            }
            else
            {
                MessageBox.Show("Please select a set before proceeding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        // Finds the item the user is ordering, based on setId and item name
        private void FindItem() // btnSearchItem_Click
        {
            // Build the table name and get the item search text
            string selectedTable = cbCardGame.Text + "Inventory";
            string itemSearch = tbFindItem.Text; // Assumes you have a TextBox named tbFindItem

            // SQL query to find the item in the inventory
            string query = $"SELECT cardId, cardName, imageURL FROM [{selectedTable}] WHERE setId = @setId AND cardName LIKE @itemName";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@setId", setId);
                        command.Parameters.AddWithValue("@itemName", itemSearch + "%");
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Display results in the DataGridView
                            dgvInvoices.DataSource = dt;
                            dgvInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Expand columns to fill space
                            dataGridViewMode = "Item Results";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error finding item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            FindItem();
        }


        // Step 2: Store quantity, cost, and total cost, then show the final step panel
        private void CreateNewInvoice2() // btnConfirmOrder_Click
        {
            int.TryParse(tbQty.Text, out qty);
            decimal.TryParse(tbIndvPrice.Text, out costIndv);
            decimal.TryParse(tbTotalCost.Text, out costTotal);

            // If costIndv is not valid, set to 0 or DBNull later when saving
            // Proceed with saving qty and total cost regardless
            flpStock.Visible = true;
        }

        // Helper to generate a unique invId by checking the last entry in the Invoice table
        private int GenerateUniqueInvoiceId()
        {
            int newId = 1;
            string query = "SELECT ISNULL(MAX(invId), 0) FROM Invoice";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int lastId))
                        {
                            newId = lastId + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating invoice ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return newId;
        }

        // Final check for required fields before saving
        private bool InvoiceFieldsAreValid()
        {
            if (qty <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(itemName))
            {
                MessageBox.Show("Item name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cardId <= 0)
            {
                MessageBox.Show("Card ID is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (setId <= 0)
            {
                MessageBox.Show("Set ID is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cardGameId <= 0)
            {
                MessageBox.Show("Card Game ID is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (costTotal <= 0)
            {
                MessageBox.Show("Total cost must be greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // Step 3: Finalize and insert the invoice into the database
        private void CreateNewInvoice3() // btnFinalizeInvoice_Click or btnFinalizeSkip_Click
        {
            // Get the date from the DateTimePicker control
            date = dtpProductArival.Value;

            // Get amtKept from the textbox as before
            int.TryParse(tbAmtInStock.Text, out amtKept); // Assumes you have a TextBox named tbAmtInStock

            // SQL query to insert the new invoice
            string query = @"INSERT INTO Invoice 
                (invId, qty, itemName, cardId, setId, cardGameId, costIndv, costTotal, date, amtKept, imageURL)
                VALUES (@invId, @qty, @itemName, @cardId, @setId, @cardGameId, @costIndv, @costTotal, @date, @amtKept, @imageURL)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@invId", invId);
                        command.Parameters.AddWithValue("@qty", qty);
                        command.Parameters.AddWithValue("@itemName", itemName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@cardId", cardId);
                        command.Parameters.AddWithValue("@setId", setId);
                        command.Parameters.AddWithValue("@cardGameId", cardGameId);
                        command.Parameters.AddWithValue("@costIndv", costIndv);
                        command.Parameters.AddWithValue("@costTotal", costTotal);
                        command.Parameters.AddWithValue("@date", date); // Uses DateTimePicker value
                        command.Parameters.AddWithValue("@amtKept", amtKept);
                        command.Parameters.AddWithValue("@imageURL", imageURL ?? (object)DBNull.Value);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Invoice created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to create invoice.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating invoice: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Reload the invoices to show the new entry
            LoadInvoices();
        }

        private void cbCardGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCardGame.Text == "Yugioh")
            {
                cardGameId = 1;
            }
            if (cbCardGame.Text == "Magic")
            {
                cardGameId = 2;
            }
            if (cbCardGame.Text == "Pokemon")
            {
                cardGameId = 3;
            }
        }

        private void btnSearchInvoices_Click(object sender, EventArgs e)
        {
            // First clear the DataGridView
            dgvInvoices.DataSource = null;
            dgvInvoices.Rows.Clear();

            // Change to a new functin that allows for text search later
            LoadInvoices();
        }

        private void dgvInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewMode == "Full Invoices")
            {
                SelectedRow();
            }
            else if (dataGridViewMode == "Set Results")
            {
                SelectedSetRow();
            }
            else if (dataGridViewMode == "Item Results")
            {
                SelectedItemRow();
            }
            else if (dataGridViewMode == "Compare Sales")
            {
                SelectedCompareSalesRow();
            }
            // Add more modes as needed
        }

        private void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            if (tbQty.Text != "" && tbTotalCost.Text != "")
            {
                CreateNewInvoice2();
            }
            else
            {
                MessageBox.Show("Please enter both quantity and total price before proceeding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (backspace, delete)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbIndvPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits, control keys, and one decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // Only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void tbTotalCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Same logic as tbIndvPrice
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void tbAmtInStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (backspace, delete)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbAmtRemoved_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (backspace, delete)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void CalculateTotalCost()
        {
            int qty = 0;
            decimal price = 0;
            // Only calculate if both fields are filled
            bool qtyValid = int.TryParse(tbQty.Text, out qty);
            bool priceValid = decimal.TryParse(tbIndvPrice.Text, out price);

            if (qtyValid && priceValid)
            {
                tbTotalCost.Text = (qty * price).ToString("0.##");
            }
            else if (qtyValid && !priceValid)
            {
                tbTotalCost.Text = ""; // Don't autofill if price is missing
            }
            // If both are blank, clear total cost
            else
            {
                tbTotalCost.Text = "";
            }
        }
        private void tbQty_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalCost();
        }

        private void tbIndvPrice_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalCost();
        }

        private void btnFinalizeInvoice_Click(object sender, EventArgs e)
        {
            // Get values from controls
            date = dtpProductArival.Value;
            int.TryParse(tbAmtInStock.Text, out amtKept);

            // Generate unique invId
            invId = GenerateUniqueInvoiceId();

            // Final check for required fields
            if (!InvoiceFieldsAreValid())
                return;

            // Prepare SQL query
            string query = @"INSERT INTO Invoice 
                (invId, qty, itemName, cardId, setId, cardGameId, costIndv, costTotal, date, amtKept, imageURL)
                VALUES (@invId, @qty, @itemName, @cardId, @setId, @cardGameId, @costIndv, @costTotal, @date, @amtKept, @imageURL)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@invId", invId);
                        command.Parameters.AddWithValue("@qty", qty);
                        command.Parameters.AddWithValue("@itemName", itemName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@cardId", cardId);
                        command.Parameters.AddWithValue("@setId", setId);
                        command.Parameters.AddWithValue("@cardGameId", cardGameId);
                        command.Parameters.AddWithValue("@costIndv", costIndv != 0 ? (object)costIndv : DBNull.Value);
                        command.Parameters.AddWithValue("@costTotal", costTotal);
                        command.Parameters.AddWithValue("@date", date != default(DateTime) ? (object)date : DBNull.Value);
                        command.Parameters.AddWithValue("@amtKept", amtKept != 0 ? (object)amtKept : DBNull.Value);
                        command.Parameters.AddWithValue("@imageURL", !string.IsNullOrEmpty(imageURL) ? (object)imageURL : DBNull.Value);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Invoice created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadInvoices();
                        }
                        else
                        {
                            MessageBox.Show("Failed to create invoice.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating invoice: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFinalizeSkip_Click(object sender, EventArgs e)
        {
            // Leave date and amtKept empty
            date = default(DateTime);
            amtKept = 0;

            // Generate unique invId
            invId = GenerateUniqueInvoiceId();

            // Final check for required fields
            if (!InvoiceFieldsAreValid())
                return;

            // Prepare SQL query
            string query = @"INSERT INTO Invoice 
                (invId, qty, itemName, cardId, setId, cardGameId, costIndv, costTotal, date, amtKept, imageURL)
                VALUES (@invId, @qty, @itemName, @cardId, @setId, @cardGameId, @costIndv, @costTotal, @date, @amtKept, @imageURL)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@invId", invId);
                        command.Parameters.AddWithValue("@qty", qty);
                        command.Parameters.AddWithValue("@itemName", itemName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@cardId", cardId);
                        command.Parameters.AddWithValue("@setId", setId);
                        command.Parameters.AddWithValue("@cardGameId", cardGameId);
                        command.Parameters.AddWithValue("@costIndv", costIndv != 0 ? (object)costIndv : DBNull.Value);
                        command.Parameters.AddWithValue("@costTotal", costTotal);
                        command.Parameters.AddWithValue("@date", DBNull.Value); // Leave date empty
                        command.Parameters.AddWithValue("@amtKept", DBNull.Value); // Leave amtKept empty
                        command.Parameters.AddWithValue("@imageURL", !string.IsNullOrEmpty(imageURL) ? (object)imageURL : DBNull.Value);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Invoice created successfully (skipped date/amtKept).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadInvoices();
                        }
                        else
                        {
                            MessageBox.Show("Failed to create invoice.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating invoice: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSeeSales_Click(object sender, EventArgs e)
        {
            if (invId <= 0 || cardId <= 0 || cardGameId <= 0)
            {
                MessageBox.Show("Please select a valid invoice row first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dataGridViewMode = "Compare Sales";

            string query = @"
SELECT 
    tl.transactionId,
    tl.saleId,
    tl.cardGameId,
    tl.cardId,
    tl.conditionId,
    tl.cardName,
    tl.setId,
    tl.rarity,
    tl.timeMktPrice,
    tl.agreedPrice,
    tl.amtTraded,
    tl.buyOrSell,
    s.saleDate
FROM 
    TransactionLine tl
JOIN 
    Sale s 
ON 
    tl.saleId = s.saleId
WHERE 
    tl.cardId = @cardId
    AND tl.conditionId = @conditionId
    AND tl.cardGameId = @cardGameId
    AND tl.amtTraded > 0
    AND tl.buyOrSell = 1";

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@cardId", cardId);
                        command.Parameters.AddWithValue("@conditionId", 1); // Use actual conditionId if available
                        command.Parameters.AddWithValue("@cardGameId", cardGameId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Build combined DataTable
                            DataTable combined = new DataTable();

                            // Add columns for display (match Compare Sales query)
                            combined.Columns.Add("saleId");
                            combined.Columns.Add("cardGameId");
                            combined.Columns.Add("cardId");
                            combined.Columns.Add("cardName");
                            combined.Columns.Add("setId");
                            combined.Columns.Add("timeMktPrice");
                            combined.Columns.Add("agreedPrice");
                            combined.Columns.Add("amtTraded");
                            combined.Columns.Add("saleDate");
                            combined.Columns.Add("costIndv"); // From invoice
                            combined.Columns.Add("imageURL"); // From invoice

                            // Add invoice row as first row
                            DataRow invoiceRow = combined.NewRow();
                            invoiceRow["saleId"] = ""; // Not applicable for invoice
                            invoiceRow["cardGameId"] = cardGameId;
                            invoiceRow["cardId"] = cardId;
                            invoiceRow["cardName"] = itemName;
                            invoiceRow["setId"] = setId;
                            invoiceRow["timeMktPrice"] = ""; // Not applicable for invoice
                            invoiceRow["agreedPrice"] = ""; // Not applicable for invoice
                            invoiceRow["amtTraded"] = qty;
                            invoiceRow["saleDate"] = date != default(DateTime) ? date.ToString() : "";
                            invoiceRow["costIndv"] = costIndv;
                            invoiceRow["imageURL"] = imageURL;
                            combined.Rows.Add(invoiceRow);

                            // Add blank row
                            combined.Rows.Add(combined.NewRow());

                            // Add sales comparison rows
                            foreach (DataRow dr in dt.Rows)
                            {
                                DataRow newRow = combined.NewRow();
                                newRow["saleId"] = dr["saleId"];
                                newRow["cardGameId"] = dr["cardGameId"];
                                newRow["cardId"] = dr["cardId"];
                                newRow["cardName"] = dr["cardName"];
                                newRow["setId"] = dr["setId"];
                                newRow["timeMktPrice"] = dr["timeMktPrice"];
                                newRow["agreedPrice"] = dr["agreedPrice"];
                                newRow["amtTraded"] = dr["amtTraded"];
                                newRow["saleDate"] = dr["saleDate"];
                                // For profit/imageURL, keep backend data
                                newRow["costIndv"] = costIndv;
                                newRow["imageURL"] = imageURL;
                                combined.Rows.Add(newRow);
                            }

                            dgvInvoices.DataSource = combined;
                            dgvInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dgvInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                            // Hide unwanted columns
                            dgvInvoices.Columns["imageURL"].Visible = false;
                            dgvInvoices.Columns.Add("Profit", "Profit"); // Add profit column for display

                            // Rename headers for clarity
                            dgvInvoices.Columns["saleId"].HeaderText = "Sale ID";
                            dgvInvoices.Columns["cardGameId"].HeaderText = "Game";
                            dgvInvoices.Columns["cardId"].HeaderText = "Card ID";
                            dgvInvoices.Columns["cardName"].HeaderText = "Item Name";
                            dgvInvoices.Columns["setId"].HeaderText = "Set";
                            dgvInvoices.Columns["timeMktPrice"].HeaderText = "Market Price";
                            dgvInvoices.Columns["agreedPrice"].HeaderText = "Sold Price";
                            dgvInvoices.Columns["amtTraded"].HeaderText = "Qty";
                            dgvInvoices.Columns["saleDate"].HeaderText = "Sale Date";
                            dgvInvoices.Columns["costIndv"].HeaderText = "Invoice Cost";

                            // Color first two rows
                            if (dgvInvoices.Rows.Count > 0)
                                dgvInvoices.Rows[0].DefaultCellStyle.BackColor = Color.LightBlue;
                            if (dgvInvoices.Rows.Count > 1)
                                dgvInvoices.Rows[1].DefaultCellStyle.BackColor = Color.LightGray;

                            gbProfit.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sales comparison: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectedCompareSalesRow()
        {
            // Only process if a valid sales row is selected (skip first two rows)
            if (dgvInvoices.CurrentRow != null && dgvInvoices.CurrentRow.Index > 1)
            {
                var row = dgvInvoices.CurrentRow;

                // Get saleDate and agreedPrice from the selected row
                string saleDateStr = row.Cells["saleDate"].Value?.ToString();
                decimal agreedPrice = 0;
                decimal.TryParse(row.Cells["agreedPrice"].Value?.ToString(), out agreedPrice);

                // Get costIndv from the invoice row (first row)
                decimal invoiceCostIndv = 0;
                if (dgvInvoices.Rows.Count > 0)
                {
                    decimal.TryParse(dgvInvoices.Rows[0].Cells["costIndv"].Value?.ToString(), out invoiceCostIndv);
                }

                // Calculate profit
                decimal profit = invoiceCostIndv - agreedPrice;

                // Display in gbProfit controls
                lblSelectedDate.Text = saleDateStr;
                lblSelectedProfit.Text = profit.ToString("C2");

                // Optionally, display image if needed
                string imageUrl = dgvInvoices.Rows[0].Cells["imageURL"].Value?.ToString();
                if (pictureBoxItem != null && !string.IsNullOrEmpty(imageUrl))
                {
                    try { pictureBoxItem.Load(imageUrl); }
                    catch { pictureBoxItem.Image = null; }
                }
            }
        }
    }
}
   
