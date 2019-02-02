using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051;
using _5051.Controllers;
using _5051.Backend;
using _5051.Models;
using System.Web;
using System.Web.Routing;
using Moq;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class CalendarControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Controller_Calendar_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new CalendarController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new CalendarController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region IndexRegion

        [TestMethod]
        public void Controller_Calendar_Index_Default_Should_Pass()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion IndexRegion

        #region SetDefaultRegion

        [TestMethod]
        public void Controller_Calendar_SetDefault_Id_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetDefault(null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetDefault_myData_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetDefault(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetDefault_Default_Should_Return_Update_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetDefault(id);

            // Assert
            Assert.AreEqual("Update", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Calendar", result.RouteValues["controller"], TestContext.TestName);
        }

        #endregion SetDefaultRegion

        #region SetEarlyEndRegion

        [TestMethod]
        public void Controller_Calendar_SetEarlyEnd_Id_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetEarlyEnd(null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetEarlyEnd_myData_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetEarlyEnd(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetEarlyEnd_Should_Return_Update_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetEarlyEnd(id);

            // Assert
            Assert.AreEqual("Update", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Calendar", result.RouteValues["controller"], TestContext.TestName);
        }

        #endregion SetEarlyEndRegion

        #region SetLateStartRegion

        [TestMethod]
        public void Controller_Calendar_SetLateStart_Id_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetLateStart(null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetLateStart_myData_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetLateStart(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetLateStart_Should_Return_Update_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetLateStart(id);

            // Assert
            Assert.AreEqual("Update", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Calendar", result.RouteValues["controller"], TestContext.TestName);
        }

        #endregion SetLateStartRegion

        #region SetNoSchoolDayRegion

        [TestMethod]
        public void Controller_Calendar_SetNoSchoolDay_Id_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetNoSchoolDay(null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetNoSchoolDay_myData_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetNoSchoolDay(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetNoSchoolDay_Should_Return_Update_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetNoSchoolDay(id);

            // Assert
            Assert.AreEqual("Update", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Calendar", result.RouteValues["controller"], TestContext.TestName);
        }

        #endregion SetNoSchoolDayRegion

        #region SetSchoolDayRegion

        [TestMethod]
        public void Controller_Calendar_SetSchoolDay_Id_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetSchoolDay(null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetSchoolDay_myData_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetSchoolDay(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_SetSchoolDay_Should_Return_Update_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SetSchoolDay(id);

            // Assert
            Assert.AreEqual("Update", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Calendar", result.RouteValues["controller"], TestContext.TestName);
        }

        #endregion SetSchoolDayRegion

        #region UpdateRegion

        [TestMethod]
        public void Controller_Calendar_Update_Get_IdIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Update((string)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_Update_Get_myDataIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Update(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_Update_Get_Default_Should_Pass()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            string id = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Update(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion UpdateRegion

        #region UpdatePostRegion

        [TestMethod]
        public void Controller_Calendar_Update_Post_Model_Is_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            SchoolCalendarModel data = new SchoolCalendarModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Update(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_Update_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Update((SchoolCalendarModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_Update_Post_Id_Is_Null_Or_Empty_Should_Return_Error_Page()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            SchoolCalendarModel dataNull = new SchoolCalendarModel();
            SchoolCalendarModel dataEmpty = new SchoolCalendarModel();

            // Make data.Id = null
            dataNull.Id = null;

            // Make data.Id empty
            dataEmpty.Id = "";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var resultNull = (RedirectToRouteResult)controller.Update(dataNull);
            var resultEmpty = (RedirectToRouteResult)controller.Update(dataEmpty);

            // Assert
            Assert.AreEqual("Error", resultNull.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Error", resultEmpty.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_Update_Post_TimeStart_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            SchoolCalendarModel dataBelowMin = new SchoolCalendarModel
            {
                TimeStart = TimeSpan.FromHours(0) // less than 1
            };

            SchoolCalendarModel dataAboveMax = new SchoolCalendarModel
            {
                TimeStart = TimeSpan.FromHours(25) // greater than 24
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult resultBelowMin = controller.Update(dataBelowMin) as ViewResult;
            ViewResult resultAboveMax = controller.Update(dataAboveMax) as ViewResult;

            // Assert
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.IsNotNull(resultBelowMin, TestContext.TestName);
            Assert.IsNotNull(resultAboveMax, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_Update_Post_TimeEnd_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            SchoolCalendarModel dataBelowMin = new SchoolCalendarModel
            {
                TimeEnd = TimeSpan.FromHours(0) // less than 1
            };

            SchoolCalendarModel dataAboveMax = new SchoolCalendarModel
            {
                TimeEnd = TimeSpan.FromHours(25) // greater than 24
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult resultBelowMin = controller.Update(dataBelowMin) as ViewResult;
            ViewResult resultAboveMax = controller.Update(dataAboveMax) as ViewResult;

            // Assert
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.IsNotNull(resultBelowMin, TestContext.TestName);
            Assert.IsNotNull(resultAboveMax, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_Update_Post_TimeEnd_Subtract_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            SchoolCalendarModel data = new SchoolCalendarModel
            {
                // End is before Start
                TimeStart = TimeSpan.FromHours(4),
                TimeEnd = TimeSpan.FromHours(2) 
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Update(data) as ViewResult;

            // Assert
            Assert.IsFalse(controller.ModelState.IsValid); // model is invalid
            Assert.IsTrue(data.TimeEnd.Subtract(data.TimeStart).Ticks < 1); // end is before start
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Calendar_Update_Post_Default_Should_Pass()
        {
            // Arrange
            CalendarController controller = new CalendarController();

            SchoolCalendarModel scm = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.Update(scm) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion UpdatePostRegion

        /// <summary>
        /// sets up a moq for http context so that a code dealing with cookeis can be tested
        /// returns a moqed context object
        /// pass in a cookieValue if you are trying to read a cookie, otherwise leave blank
        /// </summary>
        public HttpContextBase CreateMoqSetupForCookie(string cookieValue = null)
        {
            var testCookieName = "id";
            var testCookieValue = cookieValue;
            HttpCookie testCookie = new HttpCookie(testCookieName);

            if (!string.IsNullOrEmpty(cookieValue))
            {
                testCookie.Value = testCookieValue;
                testCookie.Expires = DateTime.Now.AddSeconds(30);
            }

            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);

            var mockedRequest = Mock.Get(context.Object.Request);
            mockedRequest.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());

            var mockedResponse = Mock.Get(context.Object.Response);
            mockedResponse.Setup(r => r.Cookies).Returns(new HttpCookieCollection());

            if (!string.IsNullOrEmpty(cookieValue))
            {
                var mockedServer = Mock.Get(context.Object.Server);
                mockedServer.Setup(x => x.HtmlEncode(cookieValue)).Returns(cookieValue);

                context.Object.Request.Cookies.Add(testCookie);
            }

            return context.Object;
        }
    }
}
