using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051;
using _5051.Controllers;
using _5051.Models;
using _5051.Backend;
using System.Web.Routing;
using Moq;
using System.Web;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        [TestMethod]
        public void Controller_Admin_Index_Default_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result,TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Report_Should_Return_New_Model()
        {
            // Arrange
            AdminController controller = new AdminController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Report() as ViewResult;

            var resultStudentViewModel = result.Model as StudentViewModel;

            // Assert
            Assert.IsNotNull(resultStudentViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Weekly_Report_DeFault_Should_Return_ErrorPage()
        {
            // Arrange
            AdminController controller = new AdminController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.WeeklyReport();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Weekly_Report_Valid_Id_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.WeeklyReport(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Weekly_Report_Incorrect_Id_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = "bogus";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.WeeklyReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Weekly_Report_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            WeeklyReportViewModel data = null;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.WeeklyReport(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Post_Selected_ID_Is_2_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            var data = new WeeklyReportViewModel()
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                SelectedWeekId = 2
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.WeeklyReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_DeFault_Should_Return_ErrorPage()
        {
            // Arrange
            AdminController controller = new AdminController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.WeeklyReport();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_Valid_Id_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.MonthlyReport(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_Incorrect_Id_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = "bogus";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.MonthlyReport(id); 

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            MonthlyReportViewModel data = null;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.MonthlyReport(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_Post_Data_Is_Valid_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            MonthlyReportViewModel data = new MonthlyReportViewModel()
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                SelectedMonthId = 2
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.MonthlyReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }



        [TestMethod]
        public void Controller_Admin_Semester_Report_DeFault_Should_Return_ErrorPage()
        {
            // Arrange
            AdminController controller = new AdminController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SemesterReport();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Valid_Id_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.SemesterReport(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Incorrect_Id_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = "bogus";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SemesterReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            SemesterReportViewModel data = null;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SemesterReport(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Post_Data_Is_Valid_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            var data = new SemesterReportViewModel()
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                SelectedSemesterId = 2
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.SemesterReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_DeFault_Should_Return_ErrorPage()
        {
            // Arrange
            AdminController controller = new AdminController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.QuarterReport();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Valid_Id_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.QuarterReport(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Incorrect_Id_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = "bogus";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.QuarterReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            QuarterReportViewModel data = null;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.QuarterReport(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Post_Selected_ID_Is_2_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            var data = new QuarterReportViewModel()
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                SelectedQuarterId = 2
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.QuarterReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Post_Selected_ID_Is_3_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            var data = new QuarterReportViewModel()
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                SelectedQuarterId = 3
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.QuarterReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Post_Selected_ID_Is_4_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            var data = new QuarterReportViewModel()
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                SelectedQuarterId = 4
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.QuarterReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Overall_Report_Id_Is_Valid_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.OverallReport(id);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Overall_ReportId_Is_Not_Valid_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();
            string id = "abc";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.OverallReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }


        //[TestMethod]
        //public void Controller_Admin_Attendance_Default_Should_Pass()
        //{
        //    // Arrange
        //    AdminController controller = new AdminController();

        //    // Act
        //    ViewResult result = controller.Attendance() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result, TestContext.TestName);
        //}

        [TestMethod]
        public void Controller_Admin_Settings_Default_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Settings() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }


        #region LoginRegion
        [TestMethod]
        public void Controller_Admin_Login_Default_Should_Pass()
        {
            // Arrange
            var controller = new AdminController();

            // Act
            var result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion LoginRegion

        #region LoginPostRegion

        [TestMethod]
        public void Controller_Admin_Login_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            var controller = new AdminController();
            LoginViewModel loginViewModel = new LoginViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = controller.Login(loginViewModel) as ActionResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Login_Post_loginResult_False_Should_Pass()
        {
            // Arrange
            var controller = new AdminController();
            LoginViewModel loginViewModel = new LoginViewModel();

            // Act
            var result = controller.Login(loginViewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Login_Post_loginResult_True_Should_Pass()
        {
            // Arrange
            var controller = new AdminController();
            LoginViewModel loginViewModel = new LoginViewModel();

            var idBackend = IdentityDataSourceTable.Instance;
            var expectUserName = idBackend.teacherUserName;
            var expectPass = idBackend.teacherPass;

            loginViewModel.Email = expectUserName;
            loginViewModel.Password = expectPass;

            var context = CreateMoqSetupForCookie(expectUserName);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.Login(loginViewModel) as ActionResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion LoginPostRegion

        #region ChangePasswordRegion

        [TestMethod]
        public void Controller_Admin_ChangePassword_Get_Id_Is_Null_Should_Return_Settings_Page()
        {
            // Arrange
            AdminController controller = new AdminController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.ChangePassword((string)null);

            // Assert
            Assert.AreEqual("Settings", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_ChangePassword_Get_findResult_Is_Null_Should_Return_Settings_Page()
        {
            // Arrange
            AdminController controller = new AdminController();

            string id = "bogus";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.ChangePassword(id);

            // Assert
            Assert.AreEqual("Settings", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_ChangePassword_Get_Default_Should_Pass()
        {
            // Arrange
            AdminController controller = new AdminController();
            var id = "teacher";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.ChangePassword(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion UpdateRegion

        #region ChangePassWordPostRegion

        [TestMethod]
        public void Controller_Admin_ChangePassword_Post_Model_Is_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            AdminController controller = new AdminController();

            ChangePasswordViewModel data = new ChangePasswordViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.ChangePassword(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_ChangePassword_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            AdminController controller = new AdminController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.ChangePassword((ChangePasswordViewModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_ChangePassword_Post_Id_Is_Null_Or_Empty_Should_Send_Back_For_Edit()
        {
            // Arrange
            AdminController controller = new AdminController();

            ChangePasswordViewModel dataNull = new ChangePasswordViewModel();
            ChangePasswordViewModel dataEmpty = new ChangePasswordViewModel();

            // Make data.Id = null
            dataNull.UserID = null;

            // Make data.Id empty
            dataEmpty.UserID = "";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var resultNull = (ViewResult)controller.ChangePassword(dataNull);
            var resultEmpty = (ViewResult)controller.ChangePassword(dataEmpty);

            // Assert
            Assert.IsNotNull(resultNull, TestContext.TestName);
            Assert.IsNotNull(resultEmpty, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_ChangePassword_Post_OldPassword_Is_Wrong_Should_Send_Back_For_Edit()
        {
            // Arrange
            AdminController controller = new AdminController();

            var backend = DataSourceBackend.Instance;

            //var teacher = IdentityBackend.Instance.FindUserByID("teacher");

            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            ChangePasswordViewModel model = new ChangePasswordViewModel
            {
                UserID = student.Id,
                ConfirmPassword = student.Password,
                OldPassword = "bogus",
                NewPassword = student.Password
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = (ViewResult)controller.ChangePassword(model);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_ChangePassword_Post_Default_Should_Return_Settings_Page()
        {
            // Arrange
            AdminController controller = new AdminController();

            var backend = DataSourceBackend.Instance;

            var teacher = DataSourceBackend.Instance.IdentityBackend.FindUserByID("teacher");

            ChangePasswordViewModel model = new ChangePasswordViewModel
            {
                UserID = "teacher",
                ConfirmPassword = "teacher",
                OldPassword = "teacher",
                NewPassword = "teacher"
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            RedirectToRouteResult result = controller.ChangePassword(model) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Settings", result.RouteValues["action"], TestContext.TestName);
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
