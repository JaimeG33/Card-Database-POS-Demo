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
    public partial class HomePage : Form
    {
        private string connString;

        //public HomePage()  moddified to take the connection string from the login page
        public HomePage(string connectionString)
        {
            InitializeComponent();
            connString = connectionString;
        }

        private void HomePage_Load(object sender, EventArgs e)
        {

        }

        private void HomePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnBuySell_Click(object sender, EventArgs e)
        {
            //Move to next form
            Form buySellForm = new BuySell(connString);//pass the connection string
            buySellForm.Show();//oppen the seller form
            this.Hide();//hide this form
        }
    }
}
