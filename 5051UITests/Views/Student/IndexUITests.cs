using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Student
{
    [TestClass]
    public class IndexUITests
    {
        private string _Controller = "Student";
        private string _Action = "Index";

        [TestMethod]
        public void Student_Index_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action);
        }

    }
}
