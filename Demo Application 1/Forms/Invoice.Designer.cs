namespace Demo_Application_1.Forms
{
    partial class Invoice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSettings = new System.Windows.Forms.Button();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxProfile = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.flpNewOrder = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCardGame = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSelectedSet = new System.Windows.Forms.Label();
            this.btnSearchSet = new System.Windows.Forms.Button();
            this.tbSelectSet = new System.Windows.Forms.TextBox();
            this.btnStep2 = new System.Windows.Forms.Button();
            this.flpAddStep2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.tbFindItem = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbQty = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbIndvPrice = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbTotalCost = new System.Windows.Forms.TextBox();
            this.btnBack1 = new System.Windows.Forms.Button();
            this.btnConfirmOrder = new System.Windows.Forms.Button();
            this.flpStock = new System.Windows.Forms.FlowLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpProductArival = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.tbAmtInStock = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbAmtRemoved = new System.Windows.Forms.TextBox();
            this.btnBack2FindItem = new System.Windows.Forms.Button();
            this.btnFinalizeInvoice = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.btnFinalizeSkip = new System.Windows.Forms.Button();
            this.DisplaySelectedItem = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBoxItem = new System.Windows.Forms.PictureBox();
            this.lblSelectedItem = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearchInvoices = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnBeginNewOrder = new System.Windows.Forms.Button();
            this.btnSeeSales = new System.Windows.Forms.Button();
            this.gbProfit = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblSelectedDate = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSelectedProfit = new System.Windows.Forms.Label();
            this.panelTitle.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            this.flpNewOrder.SuspendLayout();
            this.flpAddStep2.SuspendLayout();
            this.flpStock.SuspendLayout();
            this.DisplaySelectedItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItem)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbProfit.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Location = new System.Drawing.Point(1440, 3);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(44, 41);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelTitle.Controls.Add(this.label2);
            this.panelTitle.Controls.Add(this.cbxProfile);
            this.panelTitle.Controls.Add(this.btnSettings);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(1487, 65);
            this.panelTitle.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(658, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Invoice";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbxProfile
            // 
            this.cbxProfile.FormattingEnabled = true;
            this.cbxProfile.Items.AddRange(new object[] {
            "Current Page",
            "Return Home"});
            this.cbxProfile.Location = new System.Drawing.Point(12, 12);
            this.cbxProfile.Name = "cbxProfile";
            this.cbxProfile.Size = new System.Drawing.Size(144, 21);
            this.cbxProfile.TabIndex = 5;
            this.cbxProfile.SelectedIndexChanged += new System.EventHandler(this.cbxProfile_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 65);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1487, 604);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.Controls.Add(this.dgvInvoices, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.flpNewOrder, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.DisplaySelectedItem, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 93);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1481, 508);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // dgvInvoices
            // 
            this.dgvInvoices.AllowUserToAddRows = false;
            this.dgvInvoices.AllowUserToDeleteRows = false;
            this.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInvoices.Location = new System.Drawing.Point(299, 3);
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvoices.Size = new System.Drawing.Size(956, 502);
            this.dgvInvoices.TabIndex = 0;
            this.dgvInvoices.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInvoices_CellContentClick);
            // 
            // flpNewOrder
            // 
            this.flpNewOrder.BackColor = System.Drawing.SystemColors.HighlightText;
            this.flpNewOrder.Controls.Add(this.label3);
            this.flpNewOrder.Controls.Add(this.cbCardGame);
            this.flpNewOrder.Controls.Add(this.label1);
            this.flpNewOrder.Controls.Add(this.lblSelectedSet);
            this.flpNewOrder.Controls.Add(this.btnSearchSet);
            this.flpNewOrder.Controls.Add(this.tbSelectSet);
            this.flpNewOrder.Controls.Add(this.btnStep2);
            this.flpNewOrder.Controls.Add(this.flpAddStep2);
            this.flpNewOrder.Controls.Add(this.flpStock);
            this.flpNewOrder.Location = new System.Drawing.Point(3, 3);
            this.flpNewOrder.Name = "flpNewOrder";
            this.flpNewOrder.Size = new System.Drawing.Size(290, 502);
            this.flpNewOrder.TabIndex = 1;
            this.flpNewOrder.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Select Card Game";
            // 
            // cbCardGame
            // 
            this.cbCardGame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCardGame.FormattingEnabled = true;
            this.cbCardGame.Items.AddRange(new object[] {
            "Yugioh",
            "Pokemon",
            "Magic"});
            this.cbCardGame.Location = new System.Drawing.Point(3, 16);
            this.cbCardGame.Name = "cbCardGame";
            this.cbCardGame.Size = new System.Drawing.Size(230, 21);
            this.cbCardGame.TabIndex = 1;
            this.cbCardGame.SelectedIndexChanged += new System.EventHandler(this.cbCardGame_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search Set: ";
            // 
            // lblSelectedSet
            // 
            this.lblSelectedSet.AutoSize = true;
            this.lblSelectedSet.Location = new System.Drawing.Point(75, 40);
            this.lblSelectedSet.Name = "lblSelectedSet";
            this.lblSelectedSet.Size = new System.Drawing.Size(0, 13);
            this.lblSelectedSet.TabIndex = 7;
            // 
            // btnSearchSet
            // 
            this.btnSearchSet.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSearchSet.Location = new System.Drawing.Point(81, 43);
            this.btnSearchSet.Name = "btnSearchSet";
            this.btnSearchSet.Size = new System.Drawing.Size(149, 28);
            this.btnSearchSet.TabIndex = 9;
            this.btnSearchSet.Text = "Search";
            this.btnSearchSet.UseVisualStyleBackColor = false;
            this.btnSearchSet.Click += new System.EventHandler(this.btnSearchSet_Click);
            // 
            // tbSelectSet
            // 
            this.tbSelectSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSelectSet.Location = new System.Drawing.Point(3, 77);
            this.tbSelectSet.Name = "tbSelectSet";
            this.tbSelectSet.Size = new System.Drawing.Size(273, 20);
            this.tbSelectSet.TabIndex = 5;
            // 
            // btnStep2
            // 
            this.btnStep2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnStep2.Location = new System.Drawing.Point(3, 103);
            this.btnStep2.Name = "btnStep2";
            this.btnStep2.Size = new System.Drawing.Size(108, 28);
            this.btnStep2.TabIndex = 3;
            this.btnStep2.Text = "Next";
            this.btnStep2.UseVisualStyleBackColor = false;
            this.btnStep2.Click += new System.EventHandler(this.btnStep2_Click);
            // 
            // flpAddStep2
            // 
            this.flpAddStep2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpAddStep2.BackColor = System.Drawing.Color.Gainsboro;
            this.flpAddStep2.Controls.Add(this.label4);
            this.flpAddStep2.Controls.Add(this.btnSearchItem);
            this.flpAddStep2.Controls.Add(this.tbFindItem);
            this.flpAddStep2.Controls.Add(this.label5);
            this.flpAddStep2.Controls.Add(this.tbQty);
            this.flpAddStep2.Controls.Add(this.label6);
            this.flpAddStep2.Controls.Add(this.tbIndvPrice);
            this.flpAddStep2.Controls.Add(this.label7);
            this.flpAddStep2.Controls.Add(this.tbTotalCost);
            this.flpAddStep2.Controls.Add(this.btnBack1);
            this.flpAddStep2.Controls.Add(this.btnConfirmOrder);
            this.flpAddStep2.Location = new System.Drawing.Point(3, 137);
            this.flpAddStep2.Name = "flpAddStep2";
            this.flpAddStep2.Size = new System.Drawing.Size(287, 166);
            this.flpAddStep2.TabIndex = 4;
            this.flpAddStep2.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Find Item";
            // 
            // btnSearchItem
            // 
            this.btnSearchItem.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSearchItem.Location = new System.Drawing.Point(59, 3);
            this.btnSearchItem.Name = "btnSearchItem";
            this.btnSearchItem.Size = new System.Drawing.Size(149, 28);
            this.btnSearchItem.TabIndex = 10;
            this.btnSearchItem.Text = "Search";
            this.btnSearchItem.UseVisualStyleBackColor = false;
            this.btnSearchItem.Click += new System.EventHandler(this.btnSearchItem_Click);
            // 
            // tbFindItem
            // 
            this.tbFindItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFindItem.Location = new System.Drawing.Point(3, 37);
            this.tbFindItem.Name = "tbFindItem";
            this.tbFindItem.Size = new System.Drawing.Size(270, 20);
            this.tbFindItem.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Qty";
            // 
            // tbQty
            // 
            this.tbQty.Location = new System.Drawing.Point(32, 63);
            this.tbQty.Name = "tbQty";
            this.tbQty.Size = new System.Drawing.Size(36, 20);
            this.tbQty.TabIndex = 3;
            this.tbQty.TextChanged += new System.EventHandler(this.tbQty_TextChanged);
            this.tbQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbQty_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(74, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Individual Price";
            // 
            // tbIndvPrice
            // 
            this.tbIndvPrice.Location = new System.Drawing.Point(159, 63);
            this.tbIndvPrice.Name = "tbIndvPrice";
            this.tbIndvPrice.Size = new System.Drawing.Size(98, 20);
            this.tbIndvPrice.TabIndex = 5;
            this.tbIndvPrice.TextChanged += new System.EventHandler(this.tbIndvPrice_TextChanged);
            this.tbIndvPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbIndvPrice_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Total Cost";
            // 
            // tbTotalCost
            // 
            this.tbTotalCost.Location = new System.Drawing.Point(64, 89);
            this.tbTotalCost.Name = "tbTotalCost";
            this.tbTotalCost.Size = new System.Drawing.Size(140, 20);
            this.tbTotalCost.TabIndex = 7;
            this.tbTotalCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTotalCost_KeyPress);
            // 
            // btnBack1
            // 
            this.btnBack1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBack1.Location = new System.Drawing.Point(3, 115);
            this.btnBack1.Name = "btnBack1";
            this.btnBack1.Size = new System.Drawing.Size(105, 28);
            this.btnBack1.TabIndex = 8;
            this.btnBack1.Text = "Back to Set";
            this.btnBack1.UseVisualStyleBackColor = false;
            // 
            // btnConfirmOrder
            // 
            this.btnConfirmOrder.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnConfirmOrder.Location = new System.Drawing.Point(114, 115);
            this.btnConfirmOrder.Name = "btnConfirmOrder";
            this.btnConfirmOrder.Size = new System.Drawing.Size(105, 28);
            this.btnConfirmOrder.TabIndex = 9;
            this.btnConfirmOrder.Text = "Next";
            this.btnConfirmOrder.UseVisualStyleBackColor = false;
            this.btnConfirmOrder.Click += new System.EventHandler(this.btnConfirmOrder_Click);
            // 
            // flpStock
            // 
            this.flpStock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpStock.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.flpStock.Controls.Add(this.label8);
            this.flpStock.Controls.Add(this.label9);
            this.flpStock.Controls.Add(this.dtpProductArival);
            this.flpStock.Controls.Add(this.label10);
            this.flpStock.Controls.Add(this.tbAmtInStock);
            this.flpStock.Controls.Add(this.label11);
            this.flpStock.Controls.Add(this.tbAmtRemoved);
            this.flpStock.Controls.Add(this.btnBack2FindItem);
            this.flpStock.Controls.Add(this.btnFinalizeInvoice);
            this.flpStock.Controls.Add(this.label12);
            this.flpStock.Controls.Add(this.btnFinalizeSkip);
            this.flpStock.Location = new System.Drawing.Point(3, 309);
            this.flpStock.Name = "flpStock";
            this.flpStock.Size = new System.Drawing.Size(287, 167);
            this.flpStock.TabIndex = 8;
            this.flpStock.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Amount in Stock (Optional)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(142, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Date Product Arived";
            // 
            // dtpProductArival
            // 
            this.dtpProductArival.Location = new System.Drawing.Point(3, 16);
            this.dtpProductArival.Name = "dtpProductArival";
            this.dtpProductArival.Size = new System.Drawing.Size(242, 20);
            this.dtpProductArival.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Amt In Stock";
            // 
            // tbAmtInStock
            // 
            this.tbAmtInStock.Location = new System.Drawing.Point(77, 42);
            this.tbAmtInStock.Name = "tbAmtInStock";
            this.tbAmtInStock.Size = new System.Drawing.Size(36, 20);
            this.tbAmtInStock.TabIndex = 4;
            this.tbAmtInStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAmtInStock_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(119, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Amt Moved/Opened";
            // 
            // tbAmtRemoved
            // 
            this.tbAmtRemoved.Location = new System.Drawing.Point(229, 42);
            this.tbAmtRemoved.Name = "tbAmtRemoved";
            this.tbAmtRemoved.Size = new System.Drawing.Size(36, 20);
            this.tbAmtRemoved.TabIndex = 6;
            this.tbAmtRemoved.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAmtRemoved_KeyPress);
            // 
            // btnBack2FindItem
            // 
            this.btnBack2FindItem.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBack2FindItem.Location = new System.Drawing.Point(3, 68);
            this.btnBack2FindItem.Name = "btnBack2FindItem";
            this.btnBack2FindItem.Size = new System.Drawing.Size(105, 28);
            this.btnBack2FindItem.TabIndex = 9;
            this.btnBack2FindItem.Text = "Back to Find Item";
            this.btnBack2FindItem.UseVisualStyleBackColor = false;
            // 
            // btnFinalizeInvoice
            // 
            this.btnFinalizeInvoice.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnFinalizeInvoice.Location = new System.Drawing.Point(114, 68);
            this.btnFinalizeInvoice.Name = "btnFinalizeInvoice";
            this.btnFinalizeInvoice.Size = new System.Drawing.Size(105, 28);
            this.btnFinalizeInvoice.TabIndex = 10;
            this.btnFinalizeInvoice.Text = "Confirm";
            this.btnFinalizeInvoice.UseVisualStyleBackColor = false;
            this.btnFinalizeInvoice.Click += new System.EventHandler(this.btnFinalizeInvoice_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(218, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "(Skip if items have not arived in the store yet)";
            // 
            // btnFinalizeSkip
            // 
            this.btnFinalizeSkip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnFinalizeSkip.Location = new System.Drawing.Point(3, 115);
            this.btnFinalizeSkip.Name = "btnFinalizeSkip";
            this.btnFinalizeSkip.Size = new System.Drawing.Size(105, 28);
            this.btnFinalizeSkip.TabIndex = 11;
            this.btnFinalizeSkip.Text = "Skip";
            this.btnFinalizeSkip.UseVisualStyleBackColor = false;
            this.btnFinalizeSkip.Click += new System.EventHandler(this.btnFinalizeSkip_Click);
            // 
            // DisplaySelectedItem
            // 
            this.DisplaySelectedItem.Controls.Add(this.pictureBoxItem);
            this.DisplaySelectedItem.Controls.Add(this.lblSelectedItem);
            this.DisplaySelectedItem.Controls.Add(this.btnSeeSales);
            this.DisplaySelectedItem.Controls.Add(this.gbProfit);
            this.DisplaySelectedItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplaySelectedItem.Location = new System.Drawing.Point(1261, 3);
            this.DisplaySelectedItem.Name = "DisplaySelectedItem";
            this.DisplaySelectedItem.Size = new System.Drawing.Size(217, 502);
            this.DisplaySelectedItem.TabIndex = 2;
            // 
            // pictureBoxItem
            // 
            this.pictureBoxItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxItem.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxItem.Name = "pictureBoxItem";
            this.pictureBoxItem.Size = new System.Drawing.Size(208, 233);
            this.pictureBoxItem.TabIndex = 2;
            this.pictureBoxItem.TabStop = false;
            // 
            // lblSelectedItem
            // 
            this.lblSelectedItem.AutoSize = true;
            this.lblSelectedItem.Location = new System.Drawing.Point(3, 239);
            this.lblSelectedItem.Name = "lblSelectedItem";
            this.lblSelectedItem.Size = new System.Drawing.Size(0, 13);
            this.lblSelectedItem.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearchInvoices);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btnBeginNewOrder);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1481, 84);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnSearchInvoices
            // 
            this.btnSearchInvoices.Location = new System.Drawing.Point(808, 18);
            this.btnSearchInvoices.Name = "btnSearchInvoices";
            this.btnSearchInvoices.Size = new System.Drawing.Size(125, 23);
            this.btnSearchInvoices.TabIndex = 3;
            this.btnSearchInvoices.Text = "Search Invoices";
            this.btnSearchInvoices.UseVisualStyleBackColor = true;
            this.btnSearchInvoices.Click += new System.EventHandler(this.btnSearchInvoices_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(235, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(566, 20);
            this.textBox1.TabIndex = 2;
            // 
            // btnBeginNewOrder
            // 
            this.btnBeginNewOrder.Location = new System.Drawing.Point(38, 18);
            this.btnBeginNewOrder.Name = "btnBeginNewOrder";
            this.btnBeginNewOrder.Size = new System.Drawing.Size(159, 60);
            this.btnBeginNewOrder.TabIndex = 1;
            this.btnBeginNewOrder.Text = "New Order";
            this.btnBeginNewOrder.UseVisualStyleBackColor = true;
            this.btnBeginNewOrder.Click += new System.EventHandler(this.btnBeginNewOrder_Click);
            // 
            // btnSeeSales
            // 
            this.btnSeeSales.Location = new System.Drawing.Point(9, 242);
            this.btnSeeSales.Name = "btnSeeSales";
            this.btnSeeSales.Size = new System.Drawing.Size(75, 23);
            this.btnSeeSales.TabIndex = 4;
            this.btnSeeSales.Text = "See Sales";
            this.btnSeeSales.UseVisualStyleBackColor = true;
            this.btnSeeSales.Visible = false;
            this.btnSeeSales.Click += new System.EventHandler(this.btnSeeSales_Click);
            // 
            // gbProfit
            // 
            this.gbProfit.Controls.Add(this.lblSelectedProfit);
            this.gbProfit.Controls.Add(this.label14);
            this.gbProfit.Controls.Add(this.lblSelectedDate);
            this.gbProfit.Controls.Add(this.label13);
            this.gbProfit.Location = new System.Drawing.Point(3, 271);
            this.gbProfit.Name = "gbProfit";
            this.gbProfit.Size = new System.Drawing.Size(208, 181);
            this.gbProfit.TabIndex = 5;
            this.gbProfit.TabStop = false;
            this.gbProfit.Text = "Profit";
            this.gbProfit.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Date: ";
            // 
            // lblSelectedDate
            // 
            this.lblSelectedDate.AutoSize = true;
            this.lblSelectedDate.Location = new System.Drawing.Point(49, 19);
            this.lblSelectedDate.Name = "lblSelectedDate";
            this.lblSelectedDate.Size = new System.Drawing.Size(63, 13);
            this.lblSelectedDate.TabIndex = 1;
            this.lblSelectedDate.Text = "Select Date";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 54);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Profit: ";
            // 
            // lblSelectedProfit
            // 
            this.lblSelectedProfit.AutoSize = true;
            this.lblSelectedProfit.Location = new System.Drawing.Point(49, 54);
            this.lblSelectedProfit.Name = "lblSelectedProfit";
            this.lblSelectedProfit.Size = new System.Drawing.Size(49, 13);
            this.lblSelectedProfit.TabIndex = 3;
            this.lblSelectedProfit.Text = "Profit$$$";
            // 
            // Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1487, 669);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelTitle);
            this.MinimumSize = new System.Drawing.Size(1400, 700);
            this.Name = "Invoice";
            this.Text = "Invoice";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Invoice_FormClosed);
            this.Load += new System.EventHandler(this.Invoice_Load);
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            this.flpNewOrder.ResumeLayout(false);
            this.flpNewOrder.PerformLayout();
            this.flpAddStep2.ResumeLayout(false);
            this.flpAddStep2.PerformLayout();
            this.flpStock.ResumeLayout(false);
            this.flpStock.PerformLayout();
            this.DisplaySelectedItem.ResumeLayout(false);
            this.DisplaySelectedItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItem)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbProfit.ResumeLayout(false);
            this.gbProfit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxProfile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView dgvInvoices;
        private System.Windows.Forms.FlowLayoutPanel flpNewOrder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSelectSet;
        private System.Windows.Forms.ComboBox cbCardGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStep2;
        private System.Windows.Forms.FlowLayoutPanel flpAddStep2;
        private System.Windows.Forms.Label lblSelectedSet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbFindItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbQty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbIndvPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbTotalCost;
        private System.Windows.Forms.Button btnBack1;
        private System.Windows.Forms.Button btnConfirmOrder;
        private System.Windows.Forms.FlowLayoutPanel flpStock;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpProductArival;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbAmtInStock;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbAmtRemoved;
        private System.Windows.Forms.Button btnBack2FindItem;
        private System.Windows.Forms.Button btnFinalizeInvoice;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnFinalizeSkip;
        private System.Windows.Forms.Button btnBeginNewOrder;
        private System.Windows.Forms.Button btnSearchSet;
        private System.Windows.Forms.Button btnSearchItem;
        private System.Windows.Forms.PictureBox pictureBoxItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSearchInvoices;
        private System.Windows.Forms.FlowLayoutPanel DisplaySelectedItem;
        private System.Windows.Forms.Label lblSelectedItem;
        private System.Windows.Forms.Button btnSeeSales;
        private System.Windows.Forms.GroupBox gbProfit;
        private System.Windows.Forms.Label lblSelectedProfit;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblSelectedDate;
        private System.Windows.Forms.Label label13;
    }
}