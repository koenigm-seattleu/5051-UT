using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static _5051UITests.Extensions;

namespace _5051UITests.Views.Shared
{

    public class _Layout
    {


        public static void Click_All_Nav_Bar_Links(string originalController, string originalAction, string originalData = null)
        {
            //find and click a nav bar item
            //validate the correct page was landed on
            //return to original page

            ClickActionById("AvatarAttendanceLinkNavBar");
            ValidatePageTransition("Home", "Index");
            NavigateToPage(originalController, originalAction, originalData);

            if (originalController == "Home")
            {
                ClickActionById("HomeLinkNavBar");
                ValidatePageTransition(PortalControllerName, RosterViewName);
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("AdminLinkNavBar");
                ValidatePageTransition("Admin", "Index");
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("KioskModeLinkNavBar");
                ValidatePageTransition("Kiosk", "Login");
                NavigateToPage(originalController, originalAction, originalData);
            }
            else if (originalController == "Kiosk")
            {

            }
            else if(originalController == "Admin")
            {
                ClickActionById("AdminLinkNavBar");
                ValidatePageTransition("Admin", "Index");
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("AdminReportLinkNavBar");
                ValidatePageTransition("Admin", "Report");
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("StudentLinkNavbar");
                ValidatePageTransition("Student", "Index");
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("CalendarLinkNavBar");
                ValidatePageTransition("Calendar", "Index");
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("AvatarLinkNavBar");
                ValidatePageTransition("Avatar", "Index");
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("AdminSettingsLinkNavBar");
                ValidatePageTransition("Admin", "Settings");
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("FactoryInventoryLinkNavBar");
                ValidatePageTransition("Calendar", "Index");
                NavigateToPage(originalController, originalAction, originalData);
            }
            else if(originalController == "Portal")
            {
                ClickActionById("PortalRosterLinkNavBar");
                ValidatePageTransition(PortalControllerName, RosterViewName);
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("AdminMonthlyReportLinkNavBar");
                ValidatePageTransition("Admin", "MonthlyReport");
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("AttendancePortalLinkLinkNavBar");
                ValidatePageTransition("Portal", "Attendance");
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("ShopVisitlinkNavBar");
                ValidatePageTransition("Shop", "Visit");
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("ShopLinkNavBar");
                ValidatePageTransition("Shop", "Index");
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("PortalSettingsLinkNavBar");
                ValidatePageTransition("Portal", "Settings");
                NavigateToPage(originalController, originalAction, originalData);

                ClickActionById("PortalAvatarLinkNavBar");
                ValidatePageTransition("Portal", "Avatar");
                NavigateToPage(originalController, originalAction, originalData);
            }
            else
            {

            }
        }

        public static void Click_All_Footer_Links(string originalController, string originalAction, string originalData = null)
        {
            //find and click a footer item
            //validate the correct page was landed on
            //return to original page

            ClickActionById("AboutLinkFooter");
            ValidatePageTransition("Home", "About");
            NavigateToPage(originalController, originalAction, originalData);

            ClickActionById("PrivacyPolicyLinkFooter");
            ValidatePageTransition("Home", "Privacy");
            NavigateToPage(originalController, originalAction, originalData);

            ClickActionById("ContactLinkFooter");
            ValidatePageTransition("Home", "Contact");
            NavigateToPage(originalController, originalAction, originalData);

            ClickActionById("FAQLinkFooter");
            ValidatePageTransition("Home", "FAQ");
            NavigateToPage(originalController, originalAction, originalData);

            ClickActionById("UserGuideLinkFooter");
            ValidatePageTransition("Home", "Guide");
            NavigateToPage(originalController, originalAction, originalData);
        }


    }
}
