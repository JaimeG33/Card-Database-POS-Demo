namespace Demo_Application_1
{
    partial class BuySell
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
            this.cbxProfile = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tbAmtTraded = new System.Windows.Forms.TextBox();
            this.btnFinalizeSale = new System.Windows.Forms.Button();
            this.tbPrice = new System.Windows.Forms.TextBox();
            this.btnAddCt = new System.Windows.Forms.Button();
            this.lblSaleInfo = new System.Windows.Forms.Label();
            this.lblMktPrice = new System.Windows.Forms.Label();
            this.tLP_img = new System.Windows.Forms.TableLayoutPanel();
            this.imgCardUrl = new System.Windows.Forms.PictureBox();
            this.lblInStock = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panelTransSales = new System.Windows.Forms.Panel();
            this.dataGridTransactionSystem = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbCardGame = new System.Windows.Forms.ComboBox();
            this.tbSearchBar = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tLP_img.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCardUrl)).BeginInit();
            this.panel7.SuspendLayout();
            this.panelTransSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTransactionSystem)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxProfile
            // 
            this.cbxProfile.FormattingEnabled = true;
            this.cbxProfile.Items.AddRange(new object[] {
            "Current Tab",
            "Return Home"});
            this.cbxProfile.Location = new System.Drawing.Point(12, 18);
            this.cbxProfile.Name = "cbxProfile";
            this.cbxProfile.Size = new System.Drawing.Size(144, 21);
            this.cbxProfile.TabIndex = 3;
            this.cbxProfile.SelectedIndexChanged += new System.EventHandler(this.cbxProfile_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.cbxProfile);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 65);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnSettings);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 65);
            this.panel2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(407, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Seller Interface";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Location = new System.Drawing.Point(937, 3);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(44, 41);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel3.Controls.Add(this.tableLayoutPanel2);
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 65);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(984, 396);
            this.panel3.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.panel6, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel7, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(352, 376);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.tbAmtTraded);
            this.panel6.Controls.Add(this.btnFinalizeSale);
            this.panel6.Controls.Add(this.tbPrice);
            this.panel6.Controls.Add(this.btnAddCt);
            this.panel6.Controls.Add(this.lblSaleInfo);
            this.panel6.Controls.Add(this.lblMktPrice);
            this.panel6.Controls.Add(this.tLP_img);
            this.panel6.Controls.Add(this.lblInStock);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(346, 182);
            this.panel6.TabIndex = 0;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // tbAmtTraded
            // 
            this.tbAmtTraded.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbAmtTraded.Location = new System.Drawing.Point(281, 74);
            this.tbAmtTraded.Name = "tbAmtTraded";
            this.tbAmtTraded.Size = new System.Drawing.Size(35, 20);
            this.tbAmtTraded.TabIndex = 7;
            this.tbAmtTraded.TextChanged += new System.EventHandler(this.tbAmtTraded_TextChanged);
            this.tbAmtTraded.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAmtTraded_KeyPress);
            // 
            // btnFinalizeSale
            // 
            this.btnFinalizeSale.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnFinalizeSale.Location = new System.Drawing.Point(236, 158);
            this.btnFinalizeSale.Name = "btnFinalizeSale";
            this.btnFinalizeSale.Size = new System.Drawing.Size(100, 23);
            this.btnFinalizeSale.TabIndex = 6;
            this.btnFinalizeSale.Text = "Finalize Sale";
            this.btnFinalizeSale.UseVisualStyleBackColor = true;
            this.btnFinalizeSale.Click += new System.EventHandler(this.btnFinalizeSale_Click);
            // 
            // tbPrice
            // 
            this.tbPrice.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbPrice.Location = new System.Drawing.Point(164, 75);
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.Size = new System.Drawing.Size(100, 20);
            this.tbPrice.TabIndex = 5;
            this.tbPrice.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbPrice_MouseClick);
            this.tbPrice.TextChanged += new System.EventHandler(this.tbPrice_TextChanged);
            this.tbPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPrice_KeyPress);
            // 
            // btnAddCt
            // 
            this.btnAddCt.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddCt.Location = new System.Drawing.Point(164, 101);
            this.btnAddCt.Name = "btnAddCt";
            this.btnAddCt.Size = new System.Drawing.Size(89, 23);
            this.btnAddCt.TabIndex = 4;
            this.btnAddCt.Text = "Add to Cart";
            this.btnAddCt.UseVisualStyleBackColor = true;
            this.btnAddCt.Click += new System.EventHandler(this.btnAddCt_Click);
            this.btnAddCt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAddCt_MouseDown);
            // 
            // lblSaleInfo
            // 
            this.lblSaleInfo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblSaleInfo.AutoSize = true;
            this.lblSaleInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaleInfo.Location = new System.Drawing.Point(168, 161);
            this.lblSaleInfo.Name = "lblSaleInfo";
            this.lblSaleInfo.Size = new System.Drawing.Size(62, 16);
            this.lblSaleInfo.TabIndex = 3;
            this.lblSaleInfo.Text = "Sale Info:";
            // 
            // lblMktPrice
            // 
            this.lblMktPrice.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblMktPrice.AutoSize = true;
            this.lblMktPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMktPrice.Location = new System.Drawing.Point(168, 56);
            this.lblMktPrice.Name = "lblMktPrice";
            this.lblMktPrice.Size = new System.Drawing.Size(85, 16);
            this.lblMktPrice.TabIndex = 2;
            this.lblMktPrice.Text = "Market Price:";
            // 
            // tLP_img
            // 
            this.tLP_img.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tLP_img.ColumnCount = 1;
            this.tLP_img.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLP_img.Controls.Add(this.imgCardUrl, 0, 1);
            this.tLP_img.Dock = System.Windows.Forms.DockStyle.Left;
            this.tLP_img.Location = new System.Drawing.Point(0, 0);
            this.tLP_img.Name = "tLP_img";
            this.tLP_img.RowCount = 3;
            this.tLP_img.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tLP_img.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLP_img.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tLP_img.Size = new System.Drawing.Size(162, 182);
            this.tLP_img.TabIndex = 1;
            // 
            // imgCardUrl
            // 
            this.imgCardUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgCardUrl.Location = new System.Drawing.Point(4, 5);
            this.imgCardUrl.Name = "imgCardUrl";
            this.imgCardUrl.Size = new System.Drawing.Size(154, 172);
            this.imgCardUrl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgCardUrl.TabIndex = 0;
            this.imgCardUrl.TabStop = false;
            this.imgCardUrl.Click += new System.EventHandler(this.imgCardUrl_Click);
            // 
            // lblInStock
            // 
            this.lblInStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblInStock.AutoSize = true;
            this.lblInStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInStock.Location = new System.Drawing.Point(168, 14);
            this.lblInStock.Name = "lblInStock";
            this.lblInStock.Size = new System.Drawing.Size(105, 16);
            this.lblInStock.TabIndex = 1;
            this.lblInStock.Text = "Amount In Stock:";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panelTransSales);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 191);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(346, 182);
            this.panel7.TabIndex = 1;
            // 
            // panelTransSales
            // 
            this.panelTransSales.BackColor = System.Drawing.SystemColors.Info;
            this.panelTransSales.Controls.Add(this.dataGridTransactionSystem);
            this.panelTransSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTransSales.Location = new System.Drawing.Point(0, 0);
            this.panelTransSales.Name = "panelTransSales";
            this.panelTransSales.Padding = new System.Windows.Forms.Padding(10);
            this.panelTransSales.Size = new System.Drawing.Size(346, 182);
            this.panelTransSales.TabIndex = 0;
            // 
            // dataGridTransactionSystem
            // 
            this.dataGridTransactionSystem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridTransactionSystem.BackgroundColor = System.Drawing.Color.White;
            this.dataGridTransactionSystem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTransactionSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridTransactionSystem.Location = new System.Drawing.Point(10, 10);
            this.dataGridTransactionSystem.Name = "dataGridTransactionSystem";
            this.dataGridTransactionSystem.Size = new System.Drawing.Size(326, 162);
            this.dataGridTransactionSystem.TabIndex = 0;
            this.dataGridTransactionSystem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTransactionSystem_CellClick);
            this.dataGridTransactionSystem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTransactionSystem_CellContentClick);
            this.dataGridTransactionSystem.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTransactionSystem_CellEndEdit);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(362, 10);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(600, 300);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(612, 376);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Info;
            this.panel4.Controls.Add(this.dataGridView1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 53);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(6);
            this.panel4.Size = new System.Drawing.Size(606, 320);
            this.panel4.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(594, 308);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cbCardGame);
            this.panel5.Controls.Add(this.tbSearchBar);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(606, 44);
            this.panel5.TabIndex = 1;
            // 
            // cbCardGame
            // 
            this.cbCardGame.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbCardGame.FormattingEnabled = true;
            this.cbCardGame.Items.AddRange(new object[] {
            "Yugioh",
            "Magic",
            "Pokemon"});
            this.cbCardGame.Location = new System.Drawing.Point(0, 0);
            this.cbCardGame.Name = "cbCardGame";
            this.cbCardGame.Size = new System.Drawing.Size(144, 21);
            this.cbCardGame.TabIndex = 4;
            this.cbCardGame.SelectedIndexChanged += new System.EventHandler(this.cbCardGame_SelectedIndexChanged);
            // 
            // tbSearchBar
            // 
            this.tbSearchBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbSearchBar.Location = new System.Drawing.Point(0, 24);
            this.tbSearchBar.Name = "tbSearchBar";
            this.tbSearchBar.Size = new System.Drawing.Size(606, 20);
            this.tbSearchBar.TabIndex = 1;
            this.tbSearchBar.TextChanged += new System.EventHandler(this.tbSearchBar_TextChanged);
            this.tbSearchBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearchBar_KeyDown);
            // 
            // BuySell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Name = "BuySell";
            this.Text = "BuySell";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BuySell_FormClosed);
            this.Load += new System.EventHandler(this.BuySell_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tLP_img.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgCardUrl)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panelTransSales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTransactionSystem)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbxProfile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox tbSearchBar;
        private System.Windows.Forms.ComboBox cbCardGame;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.PictureBox imgCardUrl;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TableLayoutPanel tLP_img;
        private System.Windows.Forms.Label lblInStock;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Label lblMktPrice;
        private System.Windows.Forms.Label lblSaleInfo;
        private System.Windows.Forms.Button btnAddCt;
        private System.Windows.Forms.TextBox tbPrice;
        private System.Windows.Forms.Panel panelTransSales;
        private System.Windows.Forms.DataGridView dataGridTransactionSystem;
        private System.Windows.Forms.Button btnFinalizeSale;
        private System.Windows.Forms.TextBox tbAmtTraded;
    }
}