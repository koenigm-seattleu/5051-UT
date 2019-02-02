using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace _5051UITests
{
    public class Extensions
    {
        public const string LocalUrl = "http://localhost:59052";
        public const string BaseUrl = LocalUrl;

        public static string ChromeDriverLocation = "../../ChromeDriver";

        public const int WaitTime = 10;

        public const string PortalControllerName = "Portal";
        public const string RosterViewName = "Roster";
        public const string ErrorControllerName = "Home";
        public const string ErrorViewName = "Error";

        /// <summary>
        /// Creates and executes javascript to scroll to a position on the page
        /// </summary>
        public static void ScrollTo(int xPosition = 0, int yPosition = 0)
        {
            var js = String.Format("window.scrollTo({0}, {1})", xPosition, yPosition);
            ((IJavaScriptExecutor)AssemblyTests.CurrentDriver).ExecuteScript(js);
        }

        /// <summary>
        /// scrolls into the view the given element by the input id
        /// returns the element found
        /// </summary>
        public static IWebElement ScrollToViewById(string IdToFind)
        {
            var element = AssemblyTests.CurrentDriver.FindElement(By.Id(IdToFind));
            ScrollToView(element);
            return element;
        }

        /// <summary>
        /// Scrolls into view the given element
        /// </summary>
        public static void ScrollToView(IWebElement element)
        {
            if (element.Location.Y > 200)
            {
                ScrollTo(0, element.Location.Y - 100); // Make sure element is in the view but below the top navigation pane
            }
        }

        /// <summary>
        /// Validates that the id exists on the page
        /// returns the number of instances of the id
        /// </summary>
        public static int ValidateIdExists(string elementId)
        {
            var element = AssemblyTests.CurrentDriver.FindElements(By.Id(elementId));
            return element.Count;
        }

        /// <summary>
        /// Finds and returns the element using the given id
        /// Can be treated as a void return as well
        /// </summary>
        public static IWebElement GetElementById(string elementId)
        {
            var element = AssemblyTests.CurrentDriver.FindElement(By.Id(elementId));
            return element;
        }

        /// <summary>
        /// Finds and clicks the object on a page based on the ID of the object
        /// </summary>
        public static bool ClickActionById(string elementId)
        {
            var element = ScrollToViewById(elementId);
            element.Click();
            return true;
        }

        /// <summary>
        /// Returns the element value based on the ID
        /// </summary>
        public static string GetValueById(string elementId)
        {
            var element = AssemblyTests.CurrentDriver.FindElement(By.Id(elementId));
            var data = element.GetAttribute("value");
            return data.ToString();
        }

        /// <summary>
        /// Returns if the element has a checked attrigute (checkbox)
        /// </summary>
        public static bool GetCheckedById(string elementId)
        {
            var element = AssemblyTests.CurrentDriver.FindElement(By.Id(elementId));
            var data = element.GetAttribute("checked");
            if (data == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates that the controller / action / data page the driver is on 
        /// is the expected page
        /// </summary>
        public static bool ValidatePageTransition(string controller, string action, string data = null)
        {
            AssemblyTests.CurrentDriver.FindElement(By.Id("Page-Done"));
            AssemblyTests.CurrentDriver.FindElement(By.Id("Area--Done"));
            AssemblyTests.CurrentDriver.FindElement(By.Id("Controller-" + controller + "-Done"));
            AssemblyTests.CurrentDriver.FindElement(By.Id("View-" + action + "-Done"));

            return true;
        }

        /// <summary>
        /// Navigates to the driver to the given controller / action / data page 
        /// and validates that the page was landed on
        /// </summary>
        public static bool NavigateToPage(string controller, string action, string data = null)
        {
            //navigate to the desired page
            AssemblyTests.CurrentDriver.Navigate().GoToUrl(Extensions.BaseUrl + "/" + controller + "/" + action + "/" + data);

            // Wait for Naviation to complete.
            var wait = new WebDriverWait(AssemblyTests.CurrentDriver, TimeSpan.Parse("5000"));
            wait.Until(drv => drv.FindElement(By.Id("Page-Done")));

            //check that page is the right page
            ValidatePageTransition(controller, action);

            return true;
        }

        /// <summary>
        /// Navigates to the driver to the given controller / action / data page 
        /// does not have any validation built into it
        /// </summary>
        public static bool NavigateToPageNoValidation(string controller, string action, string data = null)
        {
            AssemblyTests.CurrentDriver.Navigate().GoToUrl(BaseUrl + "/" + controller + "/" + action + "/" + data);

            // Wait for Naviation to complete.
            var wait = new WebDriverWait(AssemblyTests.CurrentDriver, TimeSpan.Parse("5000"));
            wait.Until(drv => drv.FindElement(By.Id("Page-Done")));

            return true;
        }

        /// <summary>
        /// Returns the ID of the first displayed Student
        /// </summary>
        public static string GetFirstStudentID(IWebDriver driver)
        {
            //the original page
            var beforeURL = driver.Url;

            NavigateToPage("Student", "Index");
            var studentBoxes = driver.FindElements(By.Id("studentContainer"));
            var firstElementBox = studentBoxes.FirstOrDefault();
            var resultA = firstElementBox.FindElements(By.TagName("a"));

            //if this fails, means there are no students
            resultA.FirstOrDefault().Click();

            var pageURL = driver.Url;
            var testurl1 = pageURL.TrimStart(BaseUrl.ToCharArray());
            var testurl2 = testurl1.TrimStart('/');
            var testurl3 = testurl2.TrimStart("Student".ToCharArray());
            var testurl4 = testurl3.TrimStart('/');
            var testurl5 = testurl4.TrimStart("Read".ToCharArray());
            var testurl6 = testurl5.TrimStart('/');
            var firstStudentID = testurl6;

            //these two lines confirm that the ID is correct
            NavigateToPage("Student", "Index");
            NavigateToPage("student", "read", firstStudentID);

            //return to the original page
            driver.Navigate().GoToUrl(beforeURL);

            return firstStudentID;
        }

        public static List<string> GetAllStudentIDs(IWebDriver driver)
        {
            //the original page
            var beforeURL = driver.Url;
            List<string> listOfStudentIDs = new List<string>();

            NavigateToPage("Student", "Index");
            var studentBoxes = driver.FindElements(By.Id("studentContainer"));

            for (int i = 0; i < studentBoxes.Count; i++)
            {
                NavigateToPage("Student", "Index");
                var box = driver.FindElements(By.Id("studentContainer"));

                var elementBox = box[i];
                var resultA = elementBox.FindElements(By.TagName("a"));

                resultA.FirstOrDefault().Click();

                var pageURL = driver.Url;
                var testurl1 = pageURL.TrimStart(BaseUrl.ToCharArray());
                var testurl2 = testurl1.TrimStart('/');
                var testurl3 = testurl2.TrimStart("Student".ToCharArray());
                var testurl4 = testurl3.TrimStart('/');
                var testurl5 = testurl4.TrimStart("Read".ToCharArray());
                var testurl6 = testurl5.TrimStart('/');
                var studentID = testurl6;


                listOfStudentIDs.Add(studentID);
            }

            //return to the original page
            driver.Navigate().GoToUrl(beforeURL);

            return listOfStudentIDs;
        }
    }
}
