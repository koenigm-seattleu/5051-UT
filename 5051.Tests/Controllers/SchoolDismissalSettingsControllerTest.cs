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
using Moq;
using System.Web.Routing;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class SchoolDismissalSettingsControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region ReadRegion
        [TestMethod]
        public void Controller_SchoolDismissalSettings_Read_Null_Id_Should_Return_Default_Model()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();
            string id = null;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (ViewResult)controller.Read(id);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_SchoolDismissalSettings_Read_No_Id_Should_Return_Default_Model()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (ViewResult)controller.Read();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_SchoolDismissalSettings_Read_Invalid_Id_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();
            string id = "bogus";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Read(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }
        #endregion ReadRegion

        #region GetUpdateRegion
        [TestMethod]
        public void Controller_SchoolDismissalSettings_Get_Update_Valid_Id_Should_Return_Model()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();
            string id = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (ViewResult)controller.Update(id);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_SchoolDismissalSettings_Get_Update_Invalid_Id_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();
            string id = "bogus";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Update(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_SchoolDismissalSettings_Get_Update_Invalid_Id_Null_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();
            string id = null;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (ViewResult)controller.Update(id);
            var resultModel = (SchoolDismissalSettingsModel)result.Model;

            var expect = SchoolDismissalSettingsBackend.Instance.GetDefault();

            // Assert
            Assert.AreEqual(expect.Id, resultModel.Id, TestContext.TestName);
        }
        #endregion GetUpdateRegion

        #region PostUpdateRegion
        [TestMethod]
        public void Controller_SchoolDismissalSettings_Post_Update_Invalid_Model_Should_Send_Back_For_Edit()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();
            SchoolDismissalSettingsModel data = new SchoolDismissalSettingsModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = (ViewResult)controller.Update(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_SchoolDismissalSettings_Post_Update_Null_Data_Should_Return_Error()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Update((SchoolDismissalSettingsModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_SchoolDismissalSettings_Post_Update_Null_Id_Should_Send_Back_For_Edit()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();
            SchoolDismissalSettingsModel dataNull = new SchoolDismissalSettingsModel
            {

                // Make data.Id = null
                Id = null
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var resultNull = (ViewResult)controller.Update(dataNull);

            // Assert
            Assert.IsNotNull(resultNull, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_SchoolDismissalSettings_Post_Update_Empty_Id_Should_Send_Back_For_Edit()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();
            SchoolDismissalSettingsModel dataEmpty = new SchoolDismissalSettingsModel
            {

                // Make data.Id empty
                Id = ""
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var resultEmpty = (ViewResult)controller.Update(dataEmpty);

            // Assert
            Assert.IsNotNull(resultEmpty, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_SchoolDismissalSettings_Post_Update_Default_Should_Return_Read_SchoolDismissalSettings_Page()
        {
            // Arrange
            var controller = new SchoolDismissalSettingsController();

            // Get default SchoolDismissalSettingsModel
            SchoolDismissalSettingsModel schoolDismissalSettingsModel = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Update(schoolDismissalSettingsModel);

            // Assert
            Assert.AreEqual("Read", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("SchoolDismissalSettings", result.RouteValues["controller"], TestContext.TestName);
        }
        #endregion PostUpdateRegion

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
