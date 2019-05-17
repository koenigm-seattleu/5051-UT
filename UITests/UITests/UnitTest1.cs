using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace UITests
{
    [TestClass]
    public class UnitTest1
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
            homeURL = "http://localhost:59052/Home/Index";
            driver = new ChromeDriver(Path.Combine(GetBasePath, @"..\netcoreapp2.1\"), new ChromeOptions());
        }

        [TestMethod]
        public void TestMethod1()
        {
            driver.Navigate().GoToUrl(homeURL);
            WebDriverWait wait = new WebDriverWait(driver,System.TimeSpan.FromSeconds(15));

            // Find the Student Button
            wait.Until(driver=>driver.FindElement(By.Id("StudentButton")));

            // Finnd the Kiosk Button
            wait.Until(driver => driver.FindElement(By.Id("StudentButton")));

        }
    }
}
