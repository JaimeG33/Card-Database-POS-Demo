using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Application_1.Helpers
{
    internal class NavigationHelper
    {
        //Function to return home from any tab
        public static void ReturnToHome(Form currentForm, HomePage homePage, ref bool tabSwitchingFlag)
        {
            homePage.Show();
            tabSwitchingFlag = true;
            currentForm.Close();
        }
    //Tab must have the following to work:
        //private HomePage _homePage;
        //private bool changingTabs = false;

    //When opening the tab for the first time, you must also pass the HomePage reference into the constructor
        //Form nameOfNewForm = new NameOfNewForm(connString, this);

    //FormClosed event should look something like this
        //private void New_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    if (!changingTabs)
        //    {
        //        Application.Exit();
        //    }
        //}
    }
}
