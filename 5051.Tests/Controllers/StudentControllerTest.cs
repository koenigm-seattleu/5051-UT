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
    public class StudentControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Controller_Student_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new StudentController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new StudentController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region IndexRegion
        [TestMethod]
        public void Controller_Student_Index_Default_Should_Pass()
        {
            // Arrange
            var controller = new StudentController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion IndexRegion

        #region ReadStringRegion
        [TestMethod]
        public void Controller_Student_Read_Get_Default_Should_Pass()
        {
            // Arrange
            var controller = new StudentController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.Read(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Read_Get_Id_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new StudentController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.Read(null) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Read__Get_myDataStudent_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new StudentController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            var context = CreateMoqSetupForCookie("teacher");

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Read(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        #endregion ReadStringRegion

        #region CreateRegion

        [TestMethod]
        public void Controller_Student_Create_Default_Should_Pass()
        {
            // Arrange
            var controller = new StudentController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion CreateRegion

        #region CreatePostRegion

        [TestMethod]
        public void Controller_Student_Create_Post_Model_Is_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            StudentController controller = new StudentController();

            StudentModel data = new StudentModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Create(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Create_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            StudentController controller = new StudentController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Create((StudentModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Create_Post_Id_Is_Null_Or_Empty_Should_Return_Back_For_Edit()
        {
            // Arrange
            StudentController controller = new StudentController();

            StudentModel dataNull = new StudentModel();
            StudentModel dataEmpty = new StudentModel();

            // Make data.Id = null
            dataNull.Id = null;

            // Make data.Id empty
            dataEmpty.Id = "";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var resultNull = (ViewResult)controller.Create(dataNull);
            var resultEmpty = (ViewResult)controller.Create(dataEmpty);

            // Assert
            Assert.IsNotNull(resultNull, TestContext.TestName);
            Assert.IsNotNull(resultEmpty, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Create_Post_Default_Should_Return_Index_Page()
        {
            // Arrange
            StudentController controller = new StudentController();

            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Create(data);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion CreatePostRegion

        #region UpdateRegion

        [TestMethod]
        public void Controller_Student_Update_Get_Id_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            StudentController controller = new StudentController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Update((string)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Update_Get_myData_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            StudentController controller = new StudentController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Update(id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Update_Get_Default_Should_Pass()
        {
            // Arrange
            StudentController controller = new StudentController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

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
        public void Controller_Student_Update_Post_Model_Is_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            StudentController controller = new StudentController();

            StudentDisplayViewModel data = new StudentDisplayViewModel();

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
        public void Controller_Student_Update_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            StudentController controller = new StudentController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Update((StudentDisplayViewModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Update_Post_Id_Is_Null_Or_Empty_Should_Send_Back_For_Edit()
        {
            // Arrange
            StudentController controller = new StudentController();

            StudentDisplayViewModel dataNull = new StudentDisplayViewModel();
            StudentDisplayViewModel dataEmpty = new StudentDisplayViewModel();

            // Make data.Id = null
            dataNull.Id = null;

            // Make data.Id empty
            dataEmpty.Id = "";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var resultNull = (ViewResult)controller.Update(dataNull);
            var resultEmpty = (ViewResult)controller.Update(dataEmpty);

            // Assert
            Assert.IsNotNull(resultNull, TestContext.TestName);
            Assert.IsNotNull(resultEmpty, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Update_Post_Default_Should_Return_Index_Page()
        {
            // Arrange
            StudentController controller = new StudentController();

            StudentDisplayViewModel data = new StudentDisplayViewModel(DataSourceBackend.Instance.StudentBackend.GetDefault());

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            RedirectToRouteResult result = controller.Update(data) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion UpdatePostRegion

        #region DeleteRegion
        [TestMethod]
        public void Controller_Student_Delete_Get_Null_Id_Should_Return_Error()
        {
            // Arrange
            StudentController controller = new StudentController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Delete((string)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Delete_Invalid_Null_Data_Should_Return_Error()
        {
            // Arrange
            StudentController controller = new StudentController();
            string id = "bogus";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Delete(id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Delete_Get_Default_Should_Pass()
        {
            // Arrange
            StudentController controller = new StudentController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Delete(id) as ViewResult;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion DeleteRegion

        #region DeletePostRegion
        [TestMethod]
        public void Controller_Student_Delete_Post_Invalid_Model_Should_Send_Back_For_Edit()
        {
            // Arrange
            StudentController controller = new StudentController();
            StudentDisplayViewModel data = new StudentDisplayViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Delete(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Delete_Post_Null_Data_Should_Return_Error()
        {
            // Arrange
            StudentController controller = new StudentController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Delete((StudentDisplayViewModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Delete_Post_Null_Id_Should_Send_Back_For_Edit()
        {
            // Arrange
            StudentController controller = new StudentController();
            StudentDisplayViewModel dataNull = new StudentDisplayViewModel
            {

                // Make data.Id = null
                Id = null
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var resultNull = (ViewResult)controller.Delete(dataNull);

            // Assert
            Assert.IsNotNull(resultNull, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Delete_Post_Empty_Id_Should_Send_Back_For_Edit()
        {
            // Arrange
            StudentController controller = new StudentController();
            StudentDisplayViewModel dataEmpty = new StudentDisplayViewModel
            {

                // Make data.Id empty
                Id = ""
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var resultEmpty = (ViewResult)controller.Delete(dataEmpty);

            // Assert
            Assert.IsNotNull(resultEmpty, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Student_Delete_Post_Default_Should_Return_Index_Page()
        {
            // Arrange
            StudentController controller = new StudentController();

            // Get default student
            StudentModel student = DataSourceBackend.Instance.StudentBackend.GetDefault();
            StudentDisplayViewModel data = new StudentDisplayViewModel(student);

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Delete(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }
        #endregion DeletePostRegion

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
