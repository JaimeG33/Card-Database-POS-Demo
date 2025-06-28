using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Application_1
{
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
        }
        private void History_Load(object sender, EventArgs e)
        {
            changingTabs = false;
        }
        //Stuff to accept a reference to the HomePage (Different from the other tabs)
        private string connString;
        private HomePage _homePage;
        private bool changingTabs = false;
        public History (string connectionString, HomePage homePage)
        {
            InitializeComponent ();
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
        private void cbxProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxProfile.Text == "Return Home")
            {
                ReturnHome();
            }
        }

        private void History_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (changingTabs == false)
            {
                Application.Exit();
            }
            
        }

        
    }
}
