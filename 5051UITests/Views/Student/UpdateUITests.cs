using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Student
{
    [TestClass]
    public class UpdateUITests
    {
        private string _Controller = "Student";
        private string _Action = "Update";
        private string _DataFirstStudentID = AssemblyTests.firstStudentID;

        [TestMethod]
        public void Student_Update_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Student_Update_NavigateToPage_Invalid_No_ID_Should_See_Error_Page()
        {
            NavigateToPageNoValidation(_Controller, _Action);

            ValidatePageTransition(ErrorControllerName, ErrorViewName);
        }


    }
}
