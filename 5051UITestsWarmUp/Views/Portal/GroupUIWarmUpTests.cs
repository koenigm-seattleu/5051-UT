using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITestsWarmUp.Extensions;

namespace _5051UITestsWarmUp.Views.Portal
{
    [TestClass]
    public class GroupUITests
    {
        private string _Controller = "Portal";
        private string _Action = "Group";

        [TestMethod]
        public void Portal_Group_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action);
        }

    }
}
