using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.VisualStyles;

namespace Demo_Application_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //If IP is not saved yet (usually the first time the app loads) ask user for it
            if (string.IsNullOrEmpty(Properties.Settings.Default.ServerIp) )
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox( //must add Microsoft.VisualBasic in the References for this to work
                    "Enter the SQL Server IP address:", "Server IP Required", "127.0.0.1");

                if (!string.IsNullOrEmpty(input) )
                {
                    Properties.Settings.Default.ServerIp = input;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show("IP address is required. Exiting.");
                    Application.Exit();
                }
            }
        }


        public class DatabaseManager //Need public class so it can be accessed by the other forms
        {
            private string connectionString;
            //get = lets other classes read, private set = only this class inside DatabaseManager can change the value of Connection
            public SqlConnection Connection { get; private set; } // Exposes a usable connection if needed


            // the "TryConnect" method
            public bool TryConnect(string username, string password, out string message)
            {
                // Builds connection string using the user's input
                //Current ipAddress = 192.168.1.153
                string serverIP = Properties.Settings.Default.ServerIp;
                connectionString = string.Format("Server={2}\\SQLEXPRESS;Database=Revised Demo Database CAv2;User Id={0};Password={1};", username, password, serverIP);
                //Connection String Explained
                //Server = MainComputer (//usingSQLExpress)
                //Database = whichever one you are connected to (Revised Demo Database CAv2)
                //Username = SellerTest, Password = Test_123
                //IMPORTANT: In SQL Configuration Manager, (SQL Services) -> (SQL Server Browsing) must be turned on
                Connection = new SqlConnection(connectionString);

                try
                {
                    //try: to run this code that could possibly fail
                    Connection.Open();
                    message = $"Connected successfully as {username}";
                    return true;
                }
                catch (Exception ex)
                {
                    //catch = if it fails, handle error here
                    message = "Connection failed: " + ex.Message;
                    return false;
                }
                finally
                {
                    //Always try to close it afterward
                    Connection.Close();
                }
            }
            //This function lets other forms get a reusable SqlConnection based on the user credentials
            public SqlConnection GetConnection()  
            {
                return new SqlConnection(connectionString);
            }

            //GetConnectionString	Returns the raw connection string as plain text
            public string GetConnectionString()
            {
                return connectionString;
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tbUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUser.Text.Trim();
            string password = tbPswd.Text.Trim();


            DatabaseManager dbManager = new DatabaseManager();
            //if TryConnect method is successfull...
            if (dbManager.TryConnect(username, password, out string message))
            {
                //If connection worked:
                MessageBox.Show(message);// "Connected Successfully!"
                string loginConnString = dbManager.GetConnectionString();

                // Move to next form
                Form homePage = new HomePage(loginConnString);
                homePage.Show();
                this.Hide(); // Or Close() if you want to exit the login form
            }
            else
            {
                //If connection failed:
                MessageBox.Show(message);// "Connection Failed:..."
            }
        }

        private void tbPswd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Runs the btnLogin_Click method
                           //(Needs the dummy paramaters)
                btnLogin_Click(this, EventArgs.Empty);
            }
        }
    }
}
