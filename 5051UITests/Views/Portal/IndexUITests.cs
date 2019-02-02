using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Portal
{
    [TestClass]
    public class IndexUITests
    {
        private string _Controller = "Portal";
        private string _Action = "Index";
        private string _DataFirstStudentID = AssemblyTests.firstStudentID;

        [TestMethod]
        public void Portal_Index_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Index_NavigateToPage_Invalid_No_ID_Should_See_Roster_Page()
        {
            NavigateToPageNoValidation(_Controller, _Action);

            ValidatePageTransition(PortalControllerName, RosterViewName);
        }

        [TestMethod]
        public void Portal_Index_Click_All_Nav_Bar_And_Footer_Links()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Nav_Bar_Links(_Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Footer_Links(_Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Index_Click_All_On_Page_Links()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //commenting out until report is done
            //reports link
            //ClickActionById("reportsLinkPortalIndex");
            //ValidatePageTransition("Admin", "MonthlyReport", _DataFirstStudentID);
            //NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //attendance link
            ClickActionById("attendanceLinkPortalIndex");
            ValidatePageTransition("Portal", "Attendance", _DataFirstStudentID);
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //right now visit link on index page returns the error page
            //visit link
            //ClickActionById("visitLinkPortalIndex");
            //ValidatePageTransition("Shop", "Visit", _DataFirstStudentID);
            //NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //shop link
            ClickActionById("shopLinkPortalIndex");
            ValidatePageTransition("Shop", "Index", _DataFirstStudentID);
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //customize link
            ClickActionById("settingsLinkPortalIndex");
            ValidatePageTransition("Portal", "Settings", _DataFirstStudentID);
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //avatar link
            ClickActionById("avatarLinkPortalIndex");
            ValidatePageTransition("Portal", "Avatar", _DataFirstStudentID);
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Index_Check_All_Info_Displayed()
        {
            var expectWelcomeMessageStudentName = "Mike";
            var expectLevel = "1";
            var expectXp = "0";
            var expectTokens = "10";
            var expectStatus = "Out";

            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //student name
            var welcomeMessageName = GetElementById("welcomeMessageStudentName");
            Assert.AreEqual(expectWelcomeMessageStudentName, welcomeMessageName.Text);

            //level
            var resultLevel = GetElementById("levelValue");
            Assert.AreEqual(expectLevel, resultLevel.Text);

            //xp
            var resultXP = GetElementById("xpValue");
            Assert.AreEqual(expectXp, resultXP.Text);

            //tokens
            var resultTokens = GetElementById("tokensValue");
            Assert.AreEqual(expectTokens, resultTokens.Text);

            //current status
            var resultStatus = GetElementById("statusValue");
            Assert.AreEqual(expectStatus, resultStatus.Text);

            //last login (available because the demo data exists)
            var resultLastLogin = GetElementById("lastLoginValue");
            Assert.IsNotNull(resultLastLogin.Text);

            //emotion state
            var resultEmotion = GetElementById("currentEmotionValue");
            Assert.IsNotNull(resultEmotion);
        }
    }
}
