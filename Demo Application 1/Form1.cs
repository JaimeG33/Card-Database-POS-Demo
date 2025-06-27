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
                connectionString = string.Format("Server=192.168.1.153\\SQLEXPRESS;Database=Revised Demo Database CAv2;User Id={0};Password={1};", username, password);
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
