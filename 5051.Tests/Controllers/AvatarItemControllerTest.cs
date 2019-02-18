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
    public class AvatarItemControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region IndexRegion
        [TestMethod]
        public void Controller_Avatar_Index_Default_Should_Pass()
        {
            // Arrange
            AvatarItemController controller = new AvatarItemController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion IndexRegion

        #region ReadRegion
        [TestMethod]
        public void Controller_Avatar_Read_ID_Null_Should_Return_Empty_Model()
        {
            // Arrange
            AvatarItemController controller = new AvatarItemController();
            string id = null;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Read(id) as ViewResult;

            // Assert
            Assert.AreEqual(null, result.Model, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Read_ID_Bogus_Should_Return_Empty_Model()
        {
            // Arrange
            AvatarItemController controller = new AvatarItemController();
            string id = "bogus";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Read(id) as ViewResult;

            // Assert
            Assert.AreEqual(null, result.Model, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Read_ID_Valid_Should_Pass()
        {
            // Arrange
            AvatarItemController controller = new AvatarItemController();

            // Get the first Avatar from the DataSource
            string id = AvatarItemBackend.Instance.GetFirstAvatarItemId();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Read(id) as ViewResult;

            var resultAvatar = result.Model as AvatarItemModel;

            // Assert
            Assert.AreEqual(id, resultAvatar.Id, TestContext.TestName);
        }
        #endregion ReadRegion

        #region CreateRegion
        [TestMethod]
        public void Controller_Avatar_Create_Get_Should_Return_New_Model()
        {
            // Arrange
            AvatarItemController controller = new AvatarItemController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Create() as ViewResult;

            var resultAvatar = result.Model as AvatarItemModel;

            // Assert
            Assert.AreNotEqual(null, resultAvatar.Id, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Create_Post_Invalid_Null_Id_Should_Return_Model()
        {
            // Arrange
            AvatarItemController controller = new AvatarItemController();
            var data = new AvatarItemModel
            {
                Description = "description",
                Id = null,
                Name = "Name",
                Uri = "picture"
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Create(data) as ViewResult;

            var resultAvatar = result.Model as AvatarItemModel;

            // Assert
            Assert.AreEqual(data.Description, resultAvatar.Description, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Create_Post_Valid_Should_Return_Index_Page()
        {
            // Arrange
            AvatarItemController controller = new AvatarItemController();

            var data = new AvatarItemModel
            {
                Description = "description",
                Id = "abc",
                Name = "Name",
                Uri = "picture"
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Create(data);

            var resultAvatar = AvatarItemBackend.Instance.Read("abc");           

            // Reset the Avatars
            AvatarItemBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);

            // Check that the item is created
            Assert.AreEqual(data.Id, resultAvatar.Id, TestContext.TestName);
            // No need to check the route, Assert.AreEqual("Avatar", result.RouteValues["route"], TestContext.TestName);

        }

        [TestMethod]
        public void Controller_Avatar_Create_Post_InValid_Should_Return_Error_Page()
        {
            /// <summary>
            /// This Test calls the create, but passes null data
            /// The controller will return a redirect to the error home page
            /// So the test needs to cast the return to a redirect, and then check that it got to the home error page
            /// </summary>

            // Arrange
            AvatarItemController controller = new AvatarItemController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Create(null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Create_Post_InValid_Model_Should_Return_Error()
        {
            // Arrange
            var controller = new AvatarItemController();

            var data = new AvatarItemModel
            {
                Description = "description"
            };

            // Make a model error then try to send it as a Avatar
            controller.ModelState.AddModelError("test", "test");

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Create(data) as ViewResult;

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }
        #endregion CreateRegion

        #region UpdateRegion
        [TestMethod]
        public void Controller_Avatar_Update_Get_Should_Return_New_Model()
        {
            // Arrange (from create)
            AvatarItemController controller = new AvatarItemController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            var data = new AvatarItemModel
            {
                Description = "description",
                Id = "abc",
                Name = "Name",
                Uri = "picture"
            };
            // create test avatar
            var result = (RedirectToRouteResult)controller.Create(data);

            // Check that the item is created (from create)
            var resultAvatar = AvatarItemBackend.Instance.Read("abc");
            Assert.AreEqual(data.Id, resultAvatar.Id, TestContext.TestName);

            // Act
            var updateResult = controller.Update(data.Id) as ViewResult;

            resultAvatar = updateResult.Model as AvatarItemModel;

            // Assert
            Assert.AreNotEqual(null, resultAvatar.Id, TestContext.TestName);

            // Reset the Avatars
            AvatarItemBackend.Instance.Reset();
        }

        [TestMethod]
        public void Controller_Avatar_Update_Post_Invalid_Null_Id_Should_Return_Model()
        {
            // Arrange
            AvatarItemController controller = new AvatarItemController();
            var data = new AvatarItemModel
            {
                Description = "description",
                Id = null,
                Name = "Name",
                Uri = "picture"
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Update(data) as ViewResult;

            var resultAvatar = result.Model as AvatarItemModel;

            // Assert
            Assert.AreEqual(data.Description, resultAvatar.Description, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Update_Post_InValid_Should_Return_Error_Page()
        {
            /// <summary>
            /// This Test calls the Update, but passes null data
            /// The controller will return a redirect to the error home page
            /// So the test needs to cast the return to a redirect, and then check that it got to the home error page
            /// </summary>

            // Arrange
            AvatarItemController controller = new AvatarItemController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Update((AvatarItemModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Update_Post_InValid_Model_Should_Return_Error()
        {
            // Arrange
            var controller = new AvatarItemController();

            var data = new AvatarItemModel
            {
                Description = "description"
            };

            // Make a model error then try to send it as a Avatar
            controller.ModelState.AddModelError("test", "test");

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Update(data) as ViewResult;

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Update_Post_Valid_Data_Should_Pass()
        {
            // Arrange
            var controller = new AvatarItemController();
            var data = new AvatarItemModel();

            // Add to the backend
            AvatarItemBackend.Instance.Create(data);

            data.Description = "Updated Description";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Update(data) as ViewResult;
            var dataResult = AvatarItemBackend.Instance.Read(data.Id);

            AvatarItemBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(data.Description, dataResult.Description, TestContext.TestName);
        }
        #endregion UpdateRegion

        #region DeleteRegion

        [TestMethod]
        public void Controller_Avatar_Delete_ID_Null_Should_Return_Empty_Model()
        {
            // Arrange
            AvatarItemController controller = new AvatarItemController();
            string id = null;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Delete(id) as ViewResult;

            // Assert
            Assert.AreEqual(null, result.Model, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Delete_ID_Bogus_Should_Return_Empty_Model()
        {
            // Arrange
            AvatarItemController controller = new AvatarItemController();
            string id = "bogus";

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Delete(id) as ViewResult;

            // Assert
            Assert.AreEqual(null, result.Model, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Delete_ID_Valid_Should_Pass()
        {
            // Arrange
            AvatarItemController controller = new AvatarItemController();

            // Get the first Avatar from the DataSource
            string id = AvatarItemBackend.Instance.GetFirstAvatarItemId();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Delete(id) as ViewResult;

            var resultAvatar = result.Model as AvatarItemModel;

            AvatarItemBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(id, resultAvatar.Id, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Delete_Post_Invalid_Null_Id_Should_Return_Model()
        {
            // Arrange
            AvatarItemController controller = new AvatarItemController();
            var data = new AvatarItemModel
            {
                Description = "description",
                Id = null,
                Name = "Name",
                Uri = "picture"
            };

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Delete(data) as ViewResult;

            var resultAvatar = result.Model as AvatarItemModel;

            // Assert
            Assert.AreEqual(data.Description, resultAvatar.Description, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Delete_Post_InValid_Should_Return_Error_Page()
        {
            /// <summary>
            /// This Test calls the Delete, but passes null data
            /// The controller will return a redirect to the error home page
            /// So the test needs to cast the return to a redirect, and then check that it got to the home error page
            /// </summary>

            // Arrange
            AvatarItemController controller = new AvatarItemController();

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Delete((AvatarItemModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Delete_Post_InValid_Model_Should_Return_Error()
        {
            // Arrange
            var controller = new AvatarItemController();

            var data = new AvatarItemModel
            {
                Description = "description"
            };

            // Make a model error then try to send it as a Avatar
            controller.ModelState.AddModelError("test", "test");

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Delete(data) as ViewResult;

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Avatar_Delete_Post_Valid_Data_Should_Delete_Pass()
        {
            // Arrange
            AvatarItemController controller = new AvatarItemController();
            var data = new AvatarItemModel
            {
                Description = "description",
                Name = "Name",
                Uri = "picture"
            };

            var id = data.Id;

            bool isFound = true;

            // Make an avatar
            AvatarItemBackend.Instance.Create(data);

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Delete(data) as ViewResult;

            var dataRead = AvatarItemBackend.Instance.Read(id);
            if (dataRead == null)
            {
                isFound = false;
            }

            // Assert
            Assert.AreEqual(false, isFound, TestContext.TestName);
        }

        #endregion DeleteRegion

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

            return context.Object;
        }
    }
}
