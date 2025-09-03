using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

            // Setup and fill chart with basic sales data
            if (chart1.Series.Count < 2)
            {
                chart1.Series.Add("Individual Sale");

                chart1.Series.Add("Average Profit");
                    chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart1.Series[1].Color = Color.Red; // Make the average line stand out
                    chart1.Series[1].BorderWidth = 2;
            }

            SetupChart_BasicSalesData();
            FillChart_BasicSalesData();
            Setup_Chart_Line();


        }


        //    ---------------------------------------------------  Important Variables  -------------------------------------------------------------------------------

        public string selectedTimeFrame = "Custom"; // Default Time Frame is today from 2:00PM to 11:30PM

        public DateTime startDate;
        public DateTime endDate;
        public DateTime currentDate = DateTime.Now;

        private List<RawData> rawData_TransactionLine = new List<RawData>();
        public class RawData
        { 
            public DateTime startDateG1 { get; set; }
            public DateTime endDateG1 { get; set; }
            public int saleId { get; set; }
            public int transactionId { get; set; }
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
        public class SaleProfitPoint
        {
            public DateTime SaleDate { get; set; }
            public decimal Profit { get; set; }
        }
        List<SaleProfitPoint> points = new List<SaleProfitPoint>();


        //Stuff to accept a reference to the HomePage (Different from the other tabs)
        private string connString;
        private HomePage _homePage;
        private bool changingTabs = false;
        //    ----------------------------------------------------------------------  Page Setup  --------------------------------------------------------------------------------
        public History (string connectionString, HomePage homePage) //Constructor must be updated to accept HomePage reference
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
            selectedTimeFrame = "Custom";
            HighlightTimeframe();
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            endDate = dtpEnd.Value;
            selectedTimeFrame = "Custom";
            HighlightTimeframe();
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
        private void cbChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbChart.Text)
            {
                case "Basic Sales Data":
                    // First clear existing points
                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                    // Then set up the series and title
                    chart1.Series[0].Name = "Profit Over Time";
                    if (chart1.Titles.Count == 0)
                    {
                        chart1.Titles.Add("Profit Over Time");
                    }
                    else
                    {
                        chart1.Titles[0].Text = "Profit Over Time";
                    }
                    points.Clear();
                    SetupChart_BasicSalesData();
                    FillChart_BasicSalesData();
                    UpdateChart_xAxis();
                    break;
            }
        }

        private void UpdateChart_xAxis()
        {
            // After you set or update your chart data (e.g., after UpdateDatePickers or when refreshing the chart)
            chart1.ChartAreas[0].AxisX.Minimum = startDate.ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = endDate.ToOADate();
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MM/dd HH:mm"; // Optional: format for DateTime axis
        }

        private void FillChart_BasicSalesData()
        {
            chart1.Series.Clear();
            // First, ensure two series exist
            if (chart1.Series.Count < 2)
            {
                if (chart1.Series.Count == 0)
                {
                    chart1.Series.Add("Individual Sale"); // The points
                        chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                        chart1.Series[0].Color = Color.Blue;
                        chart1.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;

                    chart1.Series.Add("Average Revenue"); // The moving average line
                        chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                        chart1.Series[1].Color = Color.Red;
                        chart1.Series[1].BorderWidth = 2;
                }              
            }


            // Plot individual profit points
            chart1.Series[0].Points.Clear();
            foreach (var pt in points)
            {
                chart1.Series[0].Points.AddXY(pt.SaleDate.ToOADate(), pt.Profit);
            }
            chart1.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;

            // Plot moving average line
            chart1.Series[1].Points.Clear();
            int windowSize = 5; // Adjust for smoothing
            for (int i = 0; i < points.Count; i++)
            {
                int start = Math.Max(0, i - windowSize + 1);
                int count = i - start + 1;
                decimal avg = points.Skip(start).Take(count).Average(p => p.Profit);
                chart1.Series[1].Points.AddXY(points[i].SaleDate.ToOADate(), avg);
            }
            chart1.Series[1].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
        }

        private void Setup_Chart_Line()
        {
            int windowSize = 5; // Change for smoother/less smooth line

            for (int i = 0; i < points.Count; i++)
            {
                int start = Math.Max(0, i - windowSize + 1);
                int count = i - start + 1;
                decimal avg = points.Skip(start).Take(count).Average(p => p.Profit);
                chart1.Series[1].Points.AddXY(points[i].SaleDate.ToOADate(), avg);
            }
            chart1.Series[1].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
        }




        // ------------------------------------------------------------------  SQL Stuff  -----------------------------------------------------------------------------------------
        // This one only shows basic info for now (profit and time)
        public string basicQuery = @"
    SELECT saleDate, profit
    FROM Sale
    WHERE saleDate BETWEEN @startDate AND @endDate AND profit > 0
    ORDER BY saleDate ASC;";

        // BETWEEN 'startDate' AND 'endDate' (only include sales that made money for now, not the store buying from customers)


        // Fills the points (SaleProfitPoint) table
        // Likely to be used to fill in the chart
        private void SetupChart_BasicSalesData() 
        { 
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    //enter the query

                    using (SqlCommand cmd = new SqlCommand(basicQuery, conn))
                    {
                        // Pass C# DateTime values to SQL
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                points.Add(new SaleProfitPoint
                                {
                                    SaleDate = reader.GetDateTime(0),
                                    Profit = reader.GetDecimal(1)
                                });
                            }
                            
                        }
                        //Testing
                        //MessageBox.Show($"Loaded {points.Count} points from DB");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading inventory: " + ex.Message);
                }
            }
                
        }


        public string rawDataQuery = @"
    SELECT saleDate, cardName, rarity, cardGameId, setId, amtTraded, agreedPrice,buyOrSell, Sale.saleId, transactionId, employeeId, register, customerId 
    FROM TransactionLine INNER JOIN Sale ON TransactionLine.saleId = Sale.saleId 
    WHERE saleDate BETWEEN @startDate AND @endDate 
    ORDER BY saleDate ASC;";
        // saleDate = datetime, cardName = varchar(200), rarity = varchar(50), cardGameId = tinyint, setId = smallint, amtTraded = tinyint, agreedPrice = smallmoney, buyOrSell = bit
        // saleId = smallint, transactionId = smallint, employeeId = tinyint, register = tinyint, customerId = smallint
        // Fills the rawData_TransactionLine table
        // (Lotsa Info) Likely to be used for detailed reports and excel export
        private void GetRawData()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    //enter the query

                    using (SqlCommand cmd = new SqlCommand(rawDataQuery, conn))
                    {
                        // Pass C# DateTime values to SQL
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                rawData_TransactionLine.Add(new RawData
                                {
                                    startDateG1 = startDate,
                                    endDateG1 = endDate,

                                    // Query columns (SQL Server to C# data types are an enormous pain in the ass)
                                    saleId = reader.GetInt16(reader.GetOrdinal("saleId")), // smallint
                                    transactionId = reader.GetInt16(reader.GetOrdinal("transactionId")), // smallint
                                    employeeId = reader.GetByte(reader.GetOrdinal("employeeId")), // tinyint
                                    register = reader.GetByte(reader.GetOrdinal("register")), // tinyint
                                    customerId = reader.IsDBNull(reader.GetOrdinal("customerId")) ? (short)0 : reader.GetInt16(reader.GetOrdinal("customerId")), // smallint (can be null)
                                    cardGameId = reader.GetByte(reader.GetOrdinal("cardGameId")), // tinyint
                                    amtTraded = reader.GetByte(reader.GetOrdinal("amtTraded")), // tinyint
                                    setId = reader.GetInt16(reader.GetOrdinal("setId")), // smallint
                                    buyOrSell = reader.GetBoolean(reader.GetOrdinal("buyOrSell")), // bit
                                    agreedPrice = reader.GetDecimal(reader.GetOrdinal("agreedPrice")), // smallmoney
                                    cardName = reader.GetString(reader.GetOrdinal("cardName")), // varchar
                                    rarity = reader.IsDBNull(reader.GetOrdinal("rarity")) ? string.Empty : reader.GetString(reader.GetOrdinal("rarity")), // varchar (can be null)
                                });
                            }

                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading inventory: " + ex.Message);
                }
            }
        }

        // ------------------------------------------------------------------------------------  Excel Stuff  ----------------------------------------------------------------------------
        // Currently using ClosedXML library due to licensing restrictions (likely will not be able to create graphs within program)
        public string excelFileName = string.Format("");
        public string excelFilePath = string.Format("");
        private void btnGenerateExcel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show
                ("This will generate an excel file ",
                    "Confirm or Cancel",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );
            if (result == DialogResult.OK)
            {
                string name = chart1.Text;
                DateTime now = DateTime.Now;
                excelFileName = string.Format("{0}_{1}",name, now);
                excelFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), excelFileName + ".xlsx");

                GetRawData(); // Gets the values ready for export in the rawData_TransactionLine List
                Excel_FileCreation();
            }
            else
            {
                MessageBox.Show("Cancelled");
            }
        }

        private void Excel_FileCreation()
        {
            MessageBox.Show("Excel file generation not yet implemented.");


        }


    }
}
