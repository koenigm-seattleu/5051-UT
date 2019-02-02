using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.GameSettings
{
    [TestClass]
    public class UpdateUITests
    {
        private string _Controller = "GameSettings";
        private string _Action = "Update";

        [TestMethod]
        public void Game_Update_NavigateToPage_Valid_Should_Pass()
        {
            // Get the data for the record to update
            NavigateToPage(_Controller, "Read");

            var element = AssemblyTests.CurrentDriver.FindElement(By.Id("Id"));
            var elementval = element.GetAttribute("value");

            NavigateToPage(_Controller, _Action, elementval);
        }

        [TestMethod]
        public void Game_Update_Back_Click_Should_Pass()
        {
            // Get the data for the record to update
            NavigateToPage(_Controller, "Read");

            var element = AssemblyTests.CurrentDriver.FindElement(By.Id("Id"));
            var elementval = element.GetAttribute("value");

            NavigateToPage(_Controller, _Action, elementval);

            // Click the Back Button
            ClickActionById("BackButton");

            // Vaidate back on Read page
            ValidatePageTransition(_Controller, "Read");
        }

        [TestMethod]
        public void Game_Update_Enabled_Click_Should_Pass()
        {
            // Get the data for the record to update
            NavigateToPage(_Controller, "Read");

            var element = AssemblyTests.CurrentDriver.FindElement(By.Id("Id"));
            var elementval = element.GetAttribute("value");

            NavigateToPage(_Controller, _Action, elementval);

            // Get the Value of Enabled
            var dataEnabledFirst = GetCheckedById("Enabled");

            // Change the state
            ClickActionById("Enabled");

            // Save
            ClickActionById("Submit");

            // Go back to Update
            NavigateToPage(_Controller, _Action, elementval);

            // Verify the State changed back
            // Get the Value of Enabled
            var dataEnabledSecond = GetCheckedById("Enabled");

            Assert.AreNotEqual(dataEnabledFirst, dataEnabledSecond);

            // Set it Back
            ClickActionById("Enabled");

            // Save
            ClickActionById("Submit");

            // Go back to Update
            NavigateToPage(_Controller, _Action, elementval);

            // Verify the State changed back
            var dataEnabledThird = GetCheckedById("Enabled");

            Assert.AreEqual(dataEnabledFirst, dataEnabledThird);
        }

        [TestMethod]
        public void Game_Update_NavigateToPage_Invalid_No_ID_Should_See_Error_Page()
        {
            NavigateToPageNoValidation(_Controller, _Action);

            ValidatePageTransition(ErrorControllerName, ErrorViewName);
        }
    }
}
