namespace Demo_Application_1.Forms
{
    partial class AddInventory
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
            this.panelTitle = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbCardGame = new System.Windows.Forms.ComboBox();
            this.tbItemName = new System.Windows.Forms.TextBox();
            this.lblGroupID = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pasteORupload = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pDragDrop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTDD = new System.Windows.Forms.Button();
            this.gbSetInfo = new System.Windows.Forms.GroupBox();
            this.tbGroupId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCardGameId = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSetAbrev = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnConfirmAllInfo = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTable = new System.Windows.Forms.Label();
            this.panelTitle.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pasteORupload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbSetInfo.SuspendLayout();
            this.SuspendLayout();
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
            this.panelTitle.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(658, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Add New Product";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 65);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1487, 604);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cbCardGame);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1481, 54);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // cbCardGame
            // 
            this.cbCardGame.FormattingEnabled = true;
            this.cbCardGame.Items.AddRange(new object[] {
            "Yugioh",
            "Pokemon",
            "Magic"});
            this.cbCardGame.Location = new System.Drawing.Point(3, 3);
            this.cbCardGame.Name = "cbCardGame";
            this.cbCardGame.Size = new System.Drawing.Size(215, 21);
            this.cbCardGame.TabIndex = 0;
            this.cbCardGame.SelectedIndexChanged += new System.EventHandler(this.cbCardGame_SelectedIndexChanged);
            // 
            // tbItemName
            // 
            this.tbItemName.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbItemName.Location = new System.Drawing.Point(3, 16);
            this.tbItemName.Name = "tbItemName";
            this.tbItemName.Size = new System.Drawing.Size(382, 20);
            this.tbItemName.TabIndex = 1;
            // 
            // lblGroupID
            // 
            this.lblGroupID.AutoSize = true;
            this.lblGroupID.Location = new System.Drawing.Point(6, 39);
            this.lblGroupID.Name = "lblGroupID";
            this.lblGroupID.Size = new System.Drawing.Size(62, 13);
            this.lblGroupID.TabIndex = 3;
            this.lblGroupID.Text = "Group ID = ";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.gbSetInfo);
            this.groupBox1.Controls.Add(this.btnTDD);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.pasteORupload);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1481, 538);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(39, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(966, 452);
            this.dataGridView1.TabIndex = 1;
            // 
            // pasteORupload
            // 
            this.pasteORupload.Controls.Add(this.splitContainer1);
            this.pasteORupload.Controls.Add(this.label1);
            this.pasteORupload.Location = new System.Drawing.Point(1055, 48);
            this.pasteORupload.Name = "pasteORupload";
            this.pasteORupload.Size = new System.Drawing.Size(321, 269);
            this.pasteORupload.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AllowDrop = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.MistyRose;
            this.splitContainer1.Panel2.Controls.Add(this.pDragDrop);
            this.splitContainer1.Size = new System.Drawing.Size(318, 227);
            this.splitContainer1.SplitterDistance = 156;
            this.splitContainer1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(150, 20);
            this.textBox1.TabIndex = 0;
            // 
            // pDragDrop
            // 
            this.pDragDrop.AllowDrop = true;
            this.pDragDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDragDrop.Location = new System.Drawing.Point(0, 0);
            this.pDragDrop.Name = "pDragDrop";
            this.pDragDrop.Size = new System.Drawing.Size(158, 227);
            this.pDragDrop.TabIndex = 0;
            this.pDragDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.pDragDrop_DragDrop);
            this.pDragDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.pDragDrop_DragEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Paste Text or Upload .csv file";
            // 
            // btnTDD
            // 
            this.btnTDD.Location = new System.Drawing.Point(1055, 19);
            this.btnTDD.Name = "btnTDD";
            this.btnTDD.Size = new System.Drawing.Size(107, 23);
            this.btnTDD.TabIndex = 2;
            this.btnTDD.Text = "Toggle Drag Drop";
            this.btnTDD.UseVisualStyleBackColor = true;
            this.btnTDD.Click += new System.EventHandler(this.btnTDD_Click);
            // 
            // gbSetInfo
            // 
            this.gbSetInfo.Controls.Add(this.lblTable);
            this.gbSetInfo.Controls.Add(this.label6);
            this.gbSetInfo.Controls.Add(this.btnConfirmAllInfo);
            this.gbSetInfo.Controls.Add(this.dateTimePicker1);
            this.gbSetInfo.Controls.Add(this.label5);
            this.gbSetInfo.Controls.Add(this.tbSetAbrev);
            this.gbSetInfo.Controls.Add(this.label4);
            this.gbSetInfo.Controls.Add(this.lblCardGameId);
            this.gbSetInfo.Controls.Add(this.label3);
            this.gbSetInfo.Controls.Add(this.tbGroupId);
            this.gbSetInfo.Controls.Add(this.tbItemName);
            this.gbSetInfo.Controls.Add(this.lblGroupID);
            this.gbSetInfo.Location = new System.Drawing.Point(1026, 291);
            this.gbSetInfo.Name = "gbSetInfo";
            this.gbSetInfo.Size = new System.Drawing.Size(388, 238);
            this.gbSetInfo.TabIndex = 3;
            this.gbSetInfo.TabStop = false;
            this.gbSetInfo.Text = "Set Information";
            this.gbSetInfo.Visible = false;
            // 
            // tbGroupId
            // 
            this.tbGroupId.Location = new System.Drawing.Point(75, 39);
            this.tbGroupId.Name = "tbGroupId";
            this.tbGroupId.Size = new System.Drawing.Size(85, 20);
            this.tbGroupId.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "CardGameId = ";
            // 
            // lblCardGameId
            // 
            this.lblCardGameId.AutoSize = true;
            this.lblCardGameId.Location = new System.Drawing.Point(260, 42);
            this.lblCardGameId.Name = "lblCardGameId";
            this.lblCardGameId.Size = new System.Drawing.Size(0, 13);
            this.lblCardGameId.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Set Abreviation =";
            // 
            // tbSetAbrev
            // 
            this.tbSetAbrev.Location = new System.Drawing.Point(102, 72);
            this.tbSetAbrev.Name = "tbSetAbrev";
            this.tbSetAbrev.Size = new System.Drawing.Size(100, 20);
            this.tbSetAbrev.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Set Release Date:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(101, 107);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 10;
            // 
            // btnConfirmAllInfo
            // 
            this.btnConfirmAllInfo.BackColor = System.Drawing.SystemColors.Info;
            this.btnConfirmAllInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmAllInfo.Location = new System.Drawing.Point(52, 174);
            this.btnConfirmAllInfo.Name = "btnConfirmAllInfo";
            this.btnConfirmAllInfo.Size = new System.Drawing.Size(275, 58);
            this.btnConfirmAllInfo.TabIndex = 11;
            this.btnConfirmAllInfo.Text = "Confirm All Info";
            this.btnConfirmAllInfo.UseVisualStyleBackColor = false;
            this.btnConfirmAllInfo.Click += new System.EventHandler(this.btnConfirmAllInfo_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Affected Table = ";
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(153, 158);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(0, 13);
            this.lblTable.TabIndex = 13;
            // 
            // AddInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1487, 669);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelTitle);
            this.MinimumSize = new System.Drawing.Size(1400, 700);
            this.Name = "AddInventory";
            this.Text = "AddInventory";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddInventory_FormClosed);
            this.Load += new System.EventHandler(this.AddInventory_Load);
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pasteORupload.ResumeLayout(false);
            this.pasteORupload.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbSetInfo.ResumeLayout(false);
            this.gbSetInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxProfile;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox cbCardGame;
        private System.Windows.Forms.TextBox tbItemName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pasteORupload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblGroupID;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel pDragDrop;
        private System.Windows.Forms.Button btnTDD;
        private System.Windows.Forms.GroupBox gbSetInfo;
        private System.Windows.Forms.TextBox tbGroupId;
        private System.Windows.Forms.Label lblCardGameId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSetAbrev;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConfirmAllInfo;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label label6;
    }
}