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
using System.Web;
using Moq;
using System.Web.Routing;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class FactoryInventoryControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }


        #region Instantiate
        [TestMethod]
        public void Controller_FactoryInventory_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new FactoryInventoryController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new FactoryInventoryController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region IndexRegion
        [TestMethod]
        public void Controller_FactoryInventory_Index_Should_Pass()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            var resultFactoryInventoryViewModel = result.Model as FactoryInventoryListViewModel;

            // Assert
            Assert.IsNotNull(resultFactoryInventoryViewModel, TestContext.TestName);
        }
        #endregion IndexRegion

        #region ReadRegion
        [TestMethod]
        public void Controller_FactoryInventory_Read_IDIsNull_Should_Pass()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            string id = null;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Read(id) as ViewResult;

            // Assert
            Assert.AreEqual(null, result.Model, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_FactoryInventory_Read_IDValid_Should_Pass()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel();
            string id = Backend.FactoryInventoryBackend.Instance.Create(data).Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Read(id) as ViewResult;

            var resultFactoryInventoryModel = result.Model as FactoryInventoryModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(resultFactoryInventoryModel, TestContext.TestName);
        }
        #endregion ReadRegion

        #region CreateRegion
        [TestMethod]
        public void Controller_FactoryInventory_Create_Get_Should_Return_New_Model()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Create() as ViewResult;
            var resultFactoryInventoryModel = result.Model as FactoryInventoryModel;

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreNotEqual(null, resultFactoryInventoryModel.Id, TestContext.TestName);
        }
        #endregion CreateRegion

        #region CreatePostRegion
        [TestMethod]
        public void Controller_FactoryInventory_Create_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Create(data) as ViewResult;

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_FactoryInventory_Create_Post_Invalid_Should_Return_ErrorPage()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel();
            data = null;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Create(data);

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_FactoryInventory_Create_Post_Invalid_IDIsNull_Should_Return_Model()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel
            {
                Id = null
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Create(data) as ViewResult;
            var resultFactoryInventoryModel = result.Model as FactoryInventoryModel;

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(data.Description, resultFactoryInventoryModel.Description, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_FactoryInventory_Create_Post_Valid_Should_Return_IndexPage()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Create(data);
            var resultFactoryInventoryModel = FactoryInventoryBackend.Instance.Create(data);

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(data.Description, resultFactoryInventoryModel.Description, TestContext.TestName);
        }
        #endregion CreatePostRegion

        #region UpdateRegion
        [TestMethod]
        public void Controller_FactoryInventory_Update_IDIsNull_Should_Pass()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Update() as ViewResult;

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(null, result.Model, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_FactoryInventory_Update_IDValid_Should_Pass()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel();
            string id = Backend.FactoryInventoryBackend.Instance.Create(data).Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Update(id) as ViewResult;

            var resultFactoryInventoryModel = result.Model as FactoryInventoryModel;

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(resultFactoryInventoryModel, TestContext.TestName);
        }
        #endregion UpdateRegion

        #region UpdatePostRegion
        [TestMethod]
        public void Controller_FactoryInventory_Update_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Update(data) as ViewResult;

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_FactoryInventory_Update_Post_Invalid_Should_Return_ErrorPage()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel();
            data = null;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Update(data);

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_FactoryInventory_Update_Post_Invalid_IDIsNull_Should_Return_Model()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel
            {
                Id = null
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Update(data) as ViewResult;
            var resultFactoryInventoryModel = result.Model as FactoryInventoryModel;

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(data.Description, resultFactoryInventoryModel.Description, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_FactoryInventory_Update_Post_Valid_Should_Return_IndexPage()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Update(data);
            var resultFactoryInventoryModel = FactoryInventoryBackend.Instance.Create(data);

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(data.Description, resultFactoryInventoryModel.Description, TestContext.TestName);
        }
        #endregion UpdatePostRegion

        #region DeleteRegion
        [TestMethod]
        public void Controller_FactoryInventory_Delete_IDIsNull_Should_Pass()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Delete() as ViewResult;

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(null, result.Model, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_FactoryInventory_Delete_IDValid_Should_Pass()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel();
            string id = Backend.FactoryInventoryBackend.Instance.Create(data).Id;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Delete(id) as ViewResult;
            var resultFactoryInventoryModel = result.Model as FactoryInventoryModel;

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(resultFactoryInventoryModel, TestContext.TestName);
        }
        #endregion DeleteRegion

        #region DeletePostRegion
        [TestMethod]
        public void Controller_FactoryInventory_Delete_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Delete(data) as ViewResult;

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_FactoryInventory_Delete_Post_Invalid_Should_Return_ErrorPage()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel();
            data = null;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Delete(data);

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_FactoryInventory_Delete_Post_Invalid_IDIsNull_Should_Return_Model()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel
            {
                Id = null
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Delete(data) as ViewResult;
            var resultFactoryInventoryModel = result.Model as FactoryInventoryModel;

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(data.Description, resultFactoryInventoryModel.Description, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_FactoryInventory_Delete_Post_Valid_Should_Return_IndexPage()
        {
            // Arrange
            FactoryInventoryController controller = new FactoryInventoryController();
            FactoryInventoryModel data = new FactoryInventoryModel();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Delete(data);
            var resultFactoryInventoryModel = FactoryInventoryBackend.Instance.Create(data);

            // Reset FactoryInventoryBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(data.Description, resultFactoryInventoryModel.Description, TestContext.TestName);
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
