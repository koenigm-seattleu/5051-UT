using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;
using _5051.Models;
using System.Diagnostics;
using _5051.Backend;
using System.Web;
using System.Web.Routing;
using Moq;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class PortalControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region RosterRegion
        [TestMethod]
        public void Controller_Portal_Roster_Should_Return_NewModel()
        {
            // Arrange
            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            PortalController controller = new PortalController();

            var context = CreateMoqSetupForCookie(Student.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Roster() as ViewResult;

            var resultStudentViewModel = result.Model as StudentViewModel;

            // Assert
            Assert.IsNotNull(resultStudentViewModel, TestContext.TestName);
        }
        #endregion

        #region IndexStringRegion

        [TestMethod]
        public void Controller_Portal_Index_IDValid_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = new StudentModel();
            string id = Backend.DataSourceBackend.Instance.StudentBackend.Create(data).Id;

            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index(id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Index_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_VeryHappy_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.VeryHappy
            };
            data.Attendance.Add(myAttendance);

            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Index_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_Happy_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.Happy
            };
            data.Attendance.Add(myAttendance);

            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Index_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_Neutral_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.Neutral
            };
            data.Attendance.Add(myAttendance);

            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Index_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_Sad_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.Sad
            };
            data.Attendance.Add(myAttendance);

            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Index_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_VerySad_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.VerySad
            };
            data.Attendance.Add(myAttendance);

            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Index_Invalid_ID_Should_Fail()
        {
            // Arrange
            PortalController controller = new PortalController();

            // Act
            var result = (RedirectToRouteResult)controller.Index("Bogus");

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.AreEqual("Roster", result.RouteValues["action"], TestContext.TestName);
        }
        #endregion


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
