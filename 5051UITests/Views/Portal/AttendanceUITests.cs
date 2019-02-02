using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Portal
{
    [TestClass]
    public class AttendanceUITests
    {
        private string _Controller = "Portal";
        private string _Action = "Attendance";
        private string _DataFirstStudentID = AssemblyTests.firstStudentID;

        [TestMethod]
        public void Portal_Attendance_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Attendance_NavigateToPage_Invalid_No_ID_Should_See_Roster_Page()
        {
            NavigateToPageNoValidation(_Controller, _Action);

            ValidatePageTransition(PortalControllerName, RosterViewName);
        }

        [TestMethod]
        public void Portal_Attendance_Click_All_Nav_Bar_And_Footer_Links()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Nav_Bar_Links(_Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Footer_Links(_Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Attendance_Check_That_All_Info_Is_Displayed()
        {
            var expectStatus = "Out";

            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //current status
            var resultStatus = GetElementById("statusValue");
            Assert.AreEqual(expectStatus, resultStatus.Text);

            //last login
            var resultLastLogin = GetElementById("lastLoginValue");
            Assert.IsNotNull(resultLastLogin.Text);

            //emotion state
            var resultEmotion = GetElementById("currentEmotionValue");
            Assert.IsNotNull(resultEmotion);
        }
    }
}
