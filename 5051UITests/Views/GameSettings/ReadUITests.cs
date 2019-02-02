using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.GameSettings
{
    [TestClass]
    public class ReadUITests
    {
        private string _Controller = "GameSettings";
        private string _Action = "Read";

        [TestMethod]
        public void GameSettings_Read_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action);
        }

        [TestMethod]
        public void GameSettings_Read_NavigateToPage_Invalid_No_ID_Should_See_Error_Page()
        {
            NavigateToPageNoValidation(_Controller, _Action, "bogus");

            ValidatePageTransition(ErrorControllerName, ErrorViewName);
        }

        [TestMethod]
        public void Game_Read_Back_Click_Should_Pass()
        {
            // Get the data for the record to update
            NavigateToPage(_Controller, "Read");

            // Click the Back Button
            ClickActionById("BackButton");

            // Vaidate back on Read page
            ValidatePageTransition("Admin", "Settings");
        }
    }
}
