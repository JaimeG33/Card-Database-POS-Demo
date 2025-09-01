using Demo_Application_1.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private void Inventory_Load(object sender, EventArgs e)
        {

        }

        
    }
}
