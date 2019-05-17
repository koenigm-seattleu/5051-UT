using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace UITests
{
    [TestClass]
    public class PortalTests
    {

        private IWebDriver driver;
        public string homeURL;
        public static string GetBasePath
        {
            get
            {
                var basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                basePath = basePath?.Substring(0, basePath.Length - 10);
                return basePath;
            }
        }

        [TestInitialize]
        public void SetupTest()
        {
            homeURL = "http://localhost:59052/portal/Index";
            driver = new ChromeDriver(Path.Combine(GetBasePath, @"..\netcoreapp2.1\"), new ChromeOptions());
        }

        [TestMethod]
        public void RosterPage()
        {
            driver.Navigate().GoToUrl(homeURL);
            WebDriverWait wait = new WebDriverWait(driver,System.TimeSpan.FromSeconds(15));

            // Find the Students Badges for Choosing the Student
            wait.Until(driver=>driver.FindElement(By.Id("e42f1683-7ea1-439c-aebf-ad32f5331723")));
        }

        [TestMethod]
        public void RosterPageClickUser()
        {
            driver.Navigate().GoToUrl(homeURL);
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(15));

            var StudentID = "e42f1683-7ea1-439c-aebf-ad32f5331723";
            // Find the Students Badges for Choosing the Student
            var element = wait.Until(driver => driver.FindElement(By.Id(StudentID)));
            element.Click();
            Assert.IsTrue(driver.Url.Contains(StudentID));
        }
    }
}
