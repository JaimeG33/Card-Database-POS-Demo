using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Demo_Application_1.History;

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

            // Set default times
            dtpStart.Value = DateTime.Today.AddHours(14);      // 2:00 PM
            dtpEnd.Value = DateTime.Today.AddHours(23).AddMinutes(30); // 11:30 PM

            HighlightTimeframe();
            UpdateChart_xAxis();

            // Setup and fill chart with basic sales data


            chart1.Series[0].Name = "Profit Over Time";
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

            // Do the same for the recent sales datagridview
            RecentSales();
            AdjustDGV();
        }


        //    ---------------------------------------------------  Important Variables  -------------------------------------------------------------------------------

        public string selectedTimeFrame = "Today"; // Default Time Frame is today from 2:00PM to 11:30PM

        public DateTime startDate;
        public DateTime endDate;
        public DateTime currentDate = DateTime.Now;

        private List<RawData> rawData_TransactionLine = new List<RawData>();
        public class RawData
        { 
            public DateTime startDateG1 { get; set; }
            public DateTime endDateG1 { get; set; }
            public DateTime saleDate { get; set; }
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

        public class SimplifiedViewingPoints
        {
            public DateTime dateTime { get; set; }
            public decimal profit { get; set; }
            public string info { get; set; }
        }
        List<SimplifiedViewingPoints> pointsByDate = new List<SimplifiedViewingPoints>();


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

        private void AdjustDGV()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //dataGridView1.Dock = DockStyle.Fill;
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }
        
        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            useSalesAsPoints = false;
            startDate = dtpStart.Value;
            selectedTimeFrame = "Custom";
            HighlightTimeframe();
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            useSalesAsPoints = false;
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
            useSalesAsPoints = true;
            ResetStardEndDates();
            startDate = startDate.AddHours(14);      // 2:00 PM
            endDate = endDate.AddHours(23).AddMinutes(30);         // 11:30 PM

            if (startDate > endDate)
            {
                startDate = endDate.AddHours(-4); // Ensure end date is after start date
            }
            UpdateDatePickers();
            selectedTimeFrame = "Today";
            HighlightTimeframe();
            UpdateChart_xAxis();

            basicSalesData();
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            useSalesAsPoints = false;
            numberOfPointsOnChart = 7;
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
            useSalesAsPoints = false;
            numberOfPointsOnChart = 30;
            ResetStardEndDates();
            startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddHours(14); // 2:00 PM, starting on the 1st day of the month
            endDate = DateTime.Today.AddHours(23).AddMinutes(30); // 11:30 PM, Today
            UpdateDatePickers();
            selectedTimeFrame = "Month";
            HighlightTimeframe();
            UpdateChart_xAxis();
        }




        // ----------------------------------------------------------  Charts and Info  ----------------------------------------------------------------------------------

        public int numberOfPointsOnChart = 7;
        public bool useSalesAsPoints = true; // If false, use the numberOfPointsOnChart to determine the amount of points on grahph
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
                    UpdateChart_xAxis();
                    SetupChart_BasicSalesData();
                    FillChart_BasicSalesData();

                    break;

                case "Today":
                    btnToday.PerformClick();
                    break;
            }
        }
        // This fills the pointsByDate list with condensed points (1 point per day) for easier viewing (compared to the old method of using every sale as a point)
        private void SortPointsByDay()
        {
            pointsByDate.Clear();

            // Group points by date, sum profits for each day
            var grouped = points
                .GroupBy(p => p.SaleDate.Date)
                .OrderBy(g => g.Key)
                .ToList();

            // Determine the date range (last 'days' days)
            DateTime start = endDate.Date.AddDays(-numberOfPointsOnChart + 1);
            for (int i = 0; i < numberOfPointsOnChart; i++)
            {
                DateTime day = start.AddDays(i);
                var group = grouped.FirstOrDefault(g => g.Key == day);
                decimal totalProfit = group != null ? group.Sum(x => x.Profit) : 0m;

                pointsByDate.Add(new SimplifiedViewingPoints
                {
                    dateTime = day,
                    profit = totalProfit,
                    info = day.ToString("MM/dd")
                });
            }
        }

        // This fills the chart with 1 point for every sale that made a profit (no condensed points for easier viewing)
        private void basicSalesData()
        {
            cbChart.Text = "Basic Sales Data";
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
            UpdateChart_xAxis();
            SetupChart_BasicSalesData();
            FillChart_BasicSalesData();
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
            if (useSalesAsPoints == true) // Best for time frames of around 1 day
            {
                chart1.Series[0].Points.Clear();
                foreach (var pt in points)
                {
                    chart1.Series[0].Points.AddXY(pt.SaleDate.ToOADate(), pt.Profit);
                }
                chart1.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            }
            else // Best for time frames of around 1 week or more
            {
                chart1.Series[0].Points.Clear();
                foreach (var pt in pointsByDate)
                {
                    chart1.Series[0].Points.AddXY(pt.dateTime.ToOADate(), pt.profit);
                }
            }

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
        

        // Fills the points (SaleProfitPoint) and pointsByDate lists 
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
                            // Fill the points list with the result from the query
                            while (reader.Read())
                            {
                                points.Add(new SaleProfitPoint
                                {
                                    SaleDate = reader.GetDateTime(0),
                                    Profit = reader.GetDecimal(1)
                                });
                            }
                            
                        }
                        //Then sort the points by day for easier viewing (Stored in the pointsByDate list)
                        SortPointsByDay();
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
                                    saleDate = reader.GetDateTime(reader.GetOrdinal("saleDate")),
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
        public int lastX = 0;

        public string recentSalesQuery = @"
SELECT 
    S.saleDate,
    T.cardName,
    T.rarity,
    T.agreedPrice,
    T.amtTraded,
    (T.agreedPrice * T.amtTraded) AS total
FROM Sale AS S
INNER JOIN TransactionLine AS T
    ON S.saleId = T.saleId
ORDER BY S.saleDate DESC
OFFSET @Offset ROWS FETCH NEXT 50 ROWS ONLY;";

        private void RecentSales()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(recentSalesQuery, conn))
                    {
                        // Add the OFFSET parameter (from lastX)
                        cmd.Parameters.AddWithValue("@Offset", lastX);

                        // Execute the command
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            // Bind to DataGridView
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading recent sales:\n" + ex.Message);
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
                excelFileName = string.Format("{0}_{1:yyyyMMdd_HHmmss}", name, now);
                excelFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), excelFileName + ".xlsx");

                MessageBox.Show($"Excel file will be created at: {excelFilePath}");

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
            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                // Adds the main sheet with the graph
                var wsMain = workbook.Worksheets.Add("Main Graphic");

                // Headers
                wsMain.Cell(1, 1).Value = "Sale Date";
                wsMain.Cell(1, 2).Value = "Profit";

                // Insert data points (from points list)
                for (int i = 0; i < points.Count; i++)
                {
                    wsMain.Cell(i + 2, 1).Value = points[i].SaleDate;
                    wsMain.Cell(i + 2, 1).Style.DateFormat.Format = "yyyy-mm-dd";

                    wsMain.Cell(i + 2, 2).Value = points[i].Profit;
                }
                wsMain.Columns().AdjustToContents();


                // Add a new worksheet for raw data
                var wsRaw = workbook.Worksheets.Add("Raw Data");

                // Headers
                wsRaw.Cell(1, 1).Value = "Date";
                wsRaw.Cell(1, 2).Value = "Product Name";
                wsRaw.Cell(1, 3).Value = "Quantity";
                wsRaw.Cell(1, 4).Value = "Price";
                wsRaw.Cell(1, 5).Value = "BuyFrom or SellTo";
                wsRaw.Cell(1, 6).Value = "Rarity";
                wsRaw.Cell(1, 7).Value = "Game ID";
                wsRaw.Cell(1, 8).Value = "Customer";
                wsRaw.Cell(1, 9).Value = "Set ID";
                wsRaw.Cell(1, 10).Value = "Sale ID";
                wsRaw.Cell(1, 11).Value = "Transaction ID";
                wsRaw.Cell(1, 12).Value = "Employee ID";


                string buyOrSellText = "";
                string cardGameText = "";
                // Insert raw data
                for (int i = 0; i < rawData_TransactionLine.Count; i++)
                {
                    wsRaw.Cell(i + 2, 1).Value = rawData_TransactionLine[i].saleDate;
                    wsRaw.Cell(i + 2, 2).Value = rawData_TransactionLine[i].cardName;
                    wsRaw.Cell(i + 2, 3).Value = rawData_TransactionLine[i].amtTraded;
                    wsRaw.Cell(i + 2, 4).Value = rawData_TransactionLine[i].agreedPrice;
                    
                    buyOrSellText = rawData_TransactionLine[i].buyOrSell ? "Sell" : "Buy";
                        wsRaw.Cell(i + 2, 5).Value = buyOrSellText;
                    wsRaw.Cell(i + 2, 6).Value = rawData_TransactionLine[i].rarity;
                    
                    if (rawData_TransactionLine[i].cardGameId == 1)
                    { 
                        cardGameText = "Yugioh";
                    }
                    else if (rawData_TransactionLine[i].cardGameId == 2)
                    {
                        cardGameText = "Magic";
                    }
                    else if (rawData_TransactionLine[i].cardGameId == 3)
                    {
                        cardGameText = "Pokemon";
                    }
                    wsRaw.Cell(i + 2, 7).Value = cardGameText;
                    wsRaw.Cell(i + 2, 8).Value = rawData_TransactionLine[i].customerId;
                    wsRaw.Cell(i + 2, 9).Value = rawData_TransactionLine[i].setId;
                    wsRaw.Cell(i + 2, 10).Value = rawData_TransactionLine[i].saleId;
                    wsRaw.Cell(i + 2, 11).Value = rawData_TransactionLine[i].transactionId;
                    wsRaw.Cell(i + 2, 12).Value = rawData_TransactionLine[i].employeeId;
                }
                wsRaw.Columns().AdjustToContents();

                // Save the workbook
                workbook.SaveAs(excelFilePath);
            }

            Console.WriteLine($"Excel file created at: {excelFilePath}");
        }


    }
}
