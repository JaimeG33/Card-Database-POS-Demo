namespace Demo_Application_1.Forms
{
    partial class Inventory
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
            this.cbReturnHome = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbReturnHome
            // 
            this.cbReturnHome.FormattingEnabled = true;
            this.cbReturnHome.Location = new System.Drawing.Point(120, 33);
            this.cbReturnHome.Name = "cbReturnHome";
            this.cbReturnHome.Size = new System.Drawing.Size(121, 21);
            this.cbReturnHome.TabIndex = 0;
            this.cbReturnHome.SelectedIndexChanged += new System.EventHandler(this.cbReturnHome_SelectedIndexChanged);
            // 
            // Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbReturnHome);
            this.Name = "Inventory";
            this.Text = "Inventory";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Inventory_FormClosed);
            this.Load += new System.EventHandler(this.Inventory_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbReturnHome;
    }
}