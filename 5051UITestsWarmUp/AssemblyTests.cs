using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace _5051UITestsWarmUp
{
    [TestClass]
    public class AssemblyTests
    {
        public static IWebDriver CurrentDriver = new ChromeDriver(Extensions.ChromeDriverLocation);
        public static ChromeOptions Options = new ChromeOptions();

        public static string firstStudentID;
        public static List<string> listOfStudentIDs;

        private const string HomePageController = "Home";
        private const string HomePageView = "Index";

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            //set implicit wait for driver
            CurrentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Extensions.WaitTime);

            //navigate to the baseURL and validate it was landed on
            CurrentDriver.Navigate().GoToUrl(Extensions.BaseUrl);
            Extensions.ValidatePageTransition(HomePageController, HomePageView);

            //set the demo data as the data source
            Extensions.NavigateToPage("Admin", "Settings");
            CurrentDriver.FindElement(By.Id("Demo")).Click();
            Extensions.ValidatePageTransition("Admin", "Index");

            listOfStudentIDs = Extensions.GetAllStudentIDs(CurrentDriver);
            firstStudentID = listOfStudentIDs.FirstOrDefault();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            CurrentDriver.Quit();
        }
    }
}

