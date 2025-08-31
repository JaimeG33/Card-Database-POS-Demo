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

            //Form Size
            this.WindowState = FormWindowState.Maximized;

            // Customize DateTimePickers
            var bigFont = new Font(dtpStart.Font.FontFamily, 16, FontStyle.Regular);

            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "MM/dd/yyyy HH:mm"; // 24-hour format
            dtpStart.Font = bigFont;

            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "MM/dd/yyyy HH:mm";
            dtpEnd.Font = bigFont;

            // Set default times
            dtpStart.Value = DateTime.Today.AddHours(14);      // 2:00 PM
            dtpEnd.Value = DateTime.Today.AddHours(23).AddMinutes(30); // 11:30 PM
        }
        //Stuff to accept a reference to the HomePage (Different from the other tabs)
        private string connString;
        private HomePage _homePage;
        private bool changingTabs = false;

        // Important Variables
        public DateTime startDate;
        public DateTime endDate;
        public DateTime currentDate = DateTime.Now;
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private List<GraphData1> graphData1 = new List<GraphData1>();
        public class GraphData1
        { 
            public DateTime startDateG1 { get; set; }
            public DateTime endDateG1 { get; set; }
            public int cardGameId { get; set; }
            public string cardName { get; set; }
            public string rarity { get; set; }
            public int setId { get; set; }
            public string setAbrev { get; set; }
            public decimal agreedPrice { get; set; }
            public int amtTraded { get; set; }
            public bool buyOrSell { get; set; }
            public int employeeId { get; set; }
            public int register { get; set; }
            public int customerId { get; set; }
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            startDate = dtpStart.Value;
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            endDate = dtpEnd.Value;
        }
    }
}
