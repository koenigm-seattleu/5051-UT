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

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class GameSettingsControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Controller_GameSettings_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new GameSettingsController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new GameSettingsController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region ReadRegion
        [TestMethod]
        public void Controller_GameSettings_Read_Id_Is_Null_Should_Return_Default_Model()
        {
            // Arrange
            var controller = new GameSettingsController();

            string id = null;

            // Act
            var result = controller.Read(id);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_GameSettings_Read_No_Id_Should_Return_Default_Model()
        {
            // Arrange
            var controller = new GameSettingsController();

            // Act
            var result = controller.Read();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_GameSettings_Read_Invalid_Id_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new GameSettingsController();
            string id = "bogus";

            // Act
            var result = (RedirectToRouteResult)controller.Read(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }
        #endregion ReadRegion

        #region UpdateGetRegion
        [TestMethod]
        public void Controller_GameSettings_Update_Get_Default_Should_Pass()
        {
            // Arrange
            var controller = new GameSettingsController();

            string id = DataSourceBackend.Instance.GameBackend.GetDefault().Id;

            // Act
            var result = (ViewResult)controller.Update(id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_GameSettings_Update_Get_Invalid_Id_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new GameSettingsController();

            string id = "bogus";

            // Act
            var result = (RedirectToRouteResult)controller.Update(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }
        #endregion UpdateGetRegion

        #region PostUpdateRegion
        [TestMethod]
        public void Controller_GameSettings_Post_Update_Invalid_Model_Should_Send_Back_For_Edit()
        {
            // Arrange
            var controller = new GameSettingsController();
            var data = new GameModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = (ViewResult)controller.Update(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_GameSettings_Post_Update_Null_Data_Should_Return_Error()
        {
            // Arrange
            var controller = new GameSettingsController();

            // Act
            var result = (RedirectToRouteResult)controller.Update((GameModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_GameSettings_Post_Update_Null_Id_Should_Send_Back_For_Edit()
        {
            // Arrange
            var controller = new GameSettingsController();
            GameModel dataNull = new GameModel
            {

                // Make data.Id = null
                Id = null
            };

            // Act
            var resultNull = (ViewResult)controller.Update(dataNull);

            // Assert
            Assert.IsNotNull(resultNull, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_GameSettings_Post_Update_Empty_Id_Should_Send_Back_For_Edit()
        {
            // Arrange
            var controller = new GameSettingsController();
            GameModel dataEmpty = new GameModel
            {

                // Make data.Id empty
                Id = ""
            };

            // Act
            var resultEmpty = (ViewResult)controller.Update(dataEmpty);

            // Assert
            Assert.IsNotNull(resultEmpty, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_GameSettings_Post_Update_Default_Should_Return_Admin_Settings_Page()
        {
            // Arrange
            var controller = new GameSettingsController();

            // Get default GameModel
            GameModel GameModel = DataSourceBackend.Instance.GameBackend.GetDefault();

            // Act
            var result = (RedirectToRouteResult)controller.Update(GameModel);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Settings", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Admin", result.RouteValues["controller"], TestContext.TestName);
        }
        #endregion PostUpdateRegion
    }
}
