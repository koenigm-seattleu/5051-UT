using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Portal
{
    [TestClass]
    public class RosterUITests
    {
        private string _Controller = "Portal";
        private string _Action = "Roster";

        [TestMethod]
        public void Portal_Roster_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action);
        }

        [TestMethod]
        public void Portal_Roster_Click_All_Nav_Bar_And_Footer_Links()
        {
            NavigateToPage(_Controller, _Action);

            Shared._Layout.Click_All_Nav_Bar_Links(_Controller, _Action);

            Shared._Layout.Click_All_Footer_Links(_Controller, _Action);
        }

        [TestMethod]
        public void Portal_Roster_Click_All_On_Page_Links()
        {
            NavigateToPage(_Controller, _Action);

            var listOFStudentIds = AssemblyTests.listOfStudentIDs;

            foreach (var item in listOFStudentIds)
            {
                ClickActionById(item);
                ValidatePageTransition("Portal", "Login");
                NavigateToPage(_Controller, _Action);
            }
        }
    }
}
