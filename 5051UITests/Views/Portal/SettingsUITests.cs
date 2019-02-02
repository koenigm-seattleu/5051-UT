using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Portal
{
    [TestClass]
    public class SettingsUITests
    {
        private string _Controller = "Portal";
        private string _Action = "Settings";
        private string _DataFirstStudentID = AssemblyTests.firstStudentID;

        [TestMethod]
        public void Portal_Settings_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Settings_NavigateToPage_Invalid_No_ID_Should_See_Error_Page()
        {
            NavigateToPageNoValidation(_Controller, _Action);

            ValidatePageTransition(ErrorControllerName, ErrorViewName);
        }

        [TestMethod]
        public void Portal_Settings_Click_All_Nav_Bar_And_Footer_Links()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Nav_Bar_Links(_Controller, _Action, _DataFirstStudentID);

            Shared._Layout.Click_All_Footer_Links(_Controller, _Action, _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Settings_Click_All_On_Page_Links()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //the update button
            ClickActionById("updateSubmitButton");
            ValidatePageTransition(_Controller, "Index", _DataFirstStudentID);
        }

        [TestMethod]
        public void Portal_Settings_Check_That_All_Info_Is_Displayed()
        {
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //avatar
            GetElementById("currentAvatarImg");
            //name under avatar
            GetElementById("currentNameDisplayed");
            //Avatar level and value
            GetElementById("avatarLevelName");
            GetElementById("avatarLevelValue");
            //tokens and value
            GetElementById("tokensName");
            GetElementById("tokensValue");
            //xp and value
            GetElementById("xpName");
            GetElementById("xpValue");
            //current status and value
            GetElementById("currentStatusName");
            GetElementById("currentStatusValue");
            //name and text box with the value
            GetElementById("nameName");
            GetElementById("nameValue");
        }

        [TestMethod]
        public void Portal_Settings_Check_That_Update_Name_Works()
        {
            var expectWelcomeMessageName = "newNameForYou";
            var expectOriginalName = "Mike";

            NavigateToPage(_Controller, _Action, _DataFirstStudentID);

            //get the input box
            var element = GetElementById("Name");

            //type a new name and submit
            element.Clear();
            element.SendKeys(expectWelcomeMessageName);
            ClickActionById("updateSubmitButton");

            //assert that new name is displayed (should be on the portal/index page)
            var indexElement = GetElementById("welcomeMessageStudentName");

            Assert.AreEqual(expectWelcomeMessageName, indexElement.Text);

            //return name to original
            NavigateToPage(_Controller, _Action, _DataFirstStudentID);
            var elementAfter = GetElementById("Name");
            elementAfter.Clear();
            elementAfter.SendKeys(expectOriginalName);
            ClickActionById("updateSubmitButton");
        }
    }
}
