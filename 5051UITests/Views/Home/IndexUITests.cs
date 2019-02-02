using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Home
{
    [TestClass]
    public class IndexUITests
    {
        private string _Controller = "Home";
        private string _Action = "Index";

        [TestMethod]
        public void Home_Index_NavigateToPage_Valid_Should_Pass()
        {
            NavigateToPage(_Controller, _Action);
        }


        [TestMethod]
        public void Home_Index_Click_All_Nav_Bar_And_Footer_Links()
        {
            NavigateToPage(_Controller, _Action);

            Shared._Layout.Click_All_Nav_Bar_Links(_Controller, _Action);

            Shared._Layout.Click_All_Footer_Links(_Controller, _Action);
        }

        [TestMethod]
        public void Home_Index_Click_All_On_Page_Links()
        {
            //AvatarHouses link
            ClickActionById("houseExampleLinkHomeIndex");
            ValidatePageTransition("Home", "HouseExample");
            NavigateToPage(_Controller, _Action);

            //ChooseAvatar link
            ClickActionById("avatarExampleLinkHomeIndex");
            ValidatePageTransition("Home", "AvatarExample");
            NavigateToPage(_Controller, _Action);

            //Shop link
            ClickActionById("shopExampleLinkHomeIndex");
            ValidatePageTransition("Home", "ShopExample");
            NavigateToPage(_Controller, _Action);

            //Show Me link
            ClickActionById("studentExampleLinkHomeIndex");
            ValidatePageTransition("Home", "StudentExample");
            NavigateToPage(_Controller, _Action);
        }
    }
}
