using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
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
            startDate = dtpStart.Value;

            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "MM/dd/yyyy HH:mm";
            dtpEnd.Font = bigFont;
            endDate = dtpEnd.Value;

            HighlightTimeframe();
            UpdateChart_xAxis();

            // Set default times
            dtpStart.Value = DateTime.Today.AddHours(14);      // 2:00 PM
            dtpEnd.Value = DateTime.Today.AddHours(23).AddMinutes(30); // 11:30 PM
        }
        

        //    ---------------------------------------------------  Important Variables  -------------------------------------------------------------------------------
        public string selectedTimeFrame = "Custom"; // Default Time Frame is today from 2:00PM to 11:30PM

        public DateTime startDate;
        public DateTime endDate;
        public DateTime currentDate = DateTime.Now;

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
        private List<GeneralSales> generalSales = new List<GeneralSales>();
        public class GeneralSales
        {
            public DateTime saleDate { get; set; }
            public decimal revenue { get; set; }
        }


        //Stuff to accept a reference to the HomePage (Different from the other tabs)
        private string connString;
        private HomePage _homePage;
        private bool changingTabs = false;
        //    ----------------------------------------------------------------------  Page Setup  --------------------------------------------------------------------------------
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
        
        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            startDate = dtpStart.Value;
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            endDate = dtpEnd.Value;
        }
        //   ----------------------------------------------------------------------  Functions  --------------------------------------------------------------------------------





        // ------------------------------------------------------------------------  Time Frame  ----------------------------------------------------------------------------------
        private void ResetStardEndDates()
        {
            startDate = DateTime.Today;
            endDate = DateTime.Today;
        }

        private void UpdateDatePickers()
        {
            dtpStart.Value = startDate;
            dtpEnd.Value = endDate;
        }

        private void HighlightTimeframe()
        {
            switch(selectedTimeFrame)
            {
                case "Today":
                    btnToday.BackColor = Color.LightBlue;
                    btnWeek.BackColor = SystemColors.Control;
                    btnMonth.BackColor = SystemColors.Control;
                    btnCustom.BackColor = SystemColors.Control;
                    break;
                case "Week":
                    btnToday.BackColor = SystemColors.Control;
                    btnWeek.BackColor = Color.LightBlue;
                    btnMonth.BackColor = SystemColors.Control;
                    btnCustom.BackColor = SystemColors.Control;
                    break;
                case "Month":
                    btnToday.BackColor = SystemColors.Control;
                    btnWeek.BackColor = SystemColors.Control;
                    btnMonth.BackColor = Color.LightBlue;
                    btnCustom.BackColor = SystemColors.Control;
                    break;
                case "Custom":
                    btnToday.BackColor = SystemColors.Control;
                    btnWeek.BackColor = SystemColors.Control;
                    btnMonth.BackColor = SystemColors.Control;
                    btnCustom.BackColor = Color.LightBlue;
                    break;
            }

        }
        private void btnToday_Click(object sender, EventArgs e)
        {
            ResetStardEndDates();
            startDate = startDate.AddHours(14);      // 2:00 PM
            endDate = DateTime.Now; // Current Time

            if (startDate > endDate)
            {
                startDate = endDate.AddHours(-8); // Ensure end date is after start date
            }
            UpdateDatePickers();
            selectedTimeFrame = "Today";
            HighlightTimeframe();
            UpdateChart_xAxis();
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            ResetStardEndDates();
            startDate = startDate.AddDays(-7).AddHours(14);      // 2:00 PM, 7 days ago
            endDate = DateTime.Today.AddHours(23).AddMinutes(30); // 11:30 PM, Today
            UpdateDatePickers();
            selectedTimeFrame = "Week";
            HighlightTimeframe();
            UpdateChart_xAxis();
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            ResetStardEndDates();
            startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddHours(14); // 2:00 PM, starting on the 1st day of the month
            endDate = DateTime.Today.AddHours(23).AddMinutes(30); // 11:30 PM, Today
            UpdateDatePickers();
            selectedTimeFrame = "Month";
            HighlightTimeframe();
            UpdateChart_xAxis();
        }




        // ----------------------------------------------------------  Charts and Info  ----------------------------------------------------------------------------------

        private void UpdateChart_xAxis()
        {
            // After you set or update your chart data (e.g., after UpdateDatePickers or when refreshing the chart)
            chart1.ChartAreas[0].AxisX.Minimum = startDate.ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = endDate.ToOADate();
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MM/dd HH:mm"; // Optional: format for DateTime axis
        }







        // ------------------------------------------------------------------  SQL Stuff  -----------------------------------------------------------------------------------------




    }
}
