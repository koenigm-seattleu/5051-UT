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
using System.Web.Helpers;
using System.Reflection;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class GameControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Controller_Game_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new GameController();

            // Act
            var result = controller.GetType();

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(result, new GameController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region PostGetIterationNumberRegion
        [TestMethod]
        public void Controller_Game_Post_GetIterationNumber_Invalid_Model_Should_Return_Error()
        {
            // Arrange
            var controller = new GameController();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var data = DataSourceBackend.Instance.GameBackend.GetDefault();

            // Act
            var result = (JsonResult)controller.GetIterationNumber(data);
            var dataResult = result.Data.GetType().GetProperty("Error", BindingFlags.Instance | BindingFlags.Public);
            var dataVal = dataResult.GetValue(result.Data, null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(true, dataVal, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Game_Post_GetIterationNumber_Null_Data_Should_Return_Error()
        {
            // Arrange
            var controller = new GameController();

            // Act
            var result = (JsonResult)controller.GetIterationNumber((GameModel)null);
            var data = result.Data.GetType().GetProperty("Error", BindingFlags.Instance | BindingFlags.Public);
            var dataVal = data.GetValue(result.Data, null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(true, dataVal, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Game_Post_GetIterationNumber_Null_Id_Should_Return_Error()
        {
            // Arrange
            var controller = new GameController();

            // Get default GameModel
            var data = DataSourceBackend.Instance.GameBackend.GetDefault();
            data.Id = string.Empty;

            // Act
            var result = (JsonResult)controller.GetIterationNumber(data);
            var dataResult = result.Data.GetType().GetProperty("Error", BindingFlags.Instance | BindingFlags.Public);
            var dataVal = dataResult.GetValue(result.Data, null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(true, dataVal, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Game_Post_GetIterationNumber_Default_Should_Return_Json_Zero_Iterations()
        {
            // Arrange
            var controller = new GameController();

            // Get default GameModel
            var data = DataSourceBackend.Instance.GameBackend.GetDefault();

            // Act
            var result = (JsonResult)controller.GetIterationNumber(data);
            var dataResult = result.Data.GetType().GetProperty("Data", BindingFlags.Instance | BindingFlags.Public);
            var dataVal = dataResult.GetValue(result.Data, null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("0", dataVal, TestContext.TestName);
        }
        #endregion PostGetIterationNumberRegion

        #region GetResults
        [TestMethod]
        public void Controller_Game_Post_GetResults_Invalid_Model_Should_Return_Error()
        {
            // Arrange
            var controller = new GameController();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var studentData = new StudentInputModel
            {
                Id = data.Id
            };

            // Act
            var result = (JsonResult)controller.GetResults(studentData);
            var dataResult = result.Data.GetType().GetProperty("Error", BindingFlags.Instance | BindingFlags.Public);
            var dataVal = dataResult.GetValue(result.Data, null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(true, dataVal, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Game_Post_GetResults_Valid_Model_Should_Pass()
        {
            // Arrange
            var controller = new GameController();

            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var studentData = new StudentInputModel
            {
                Id = data.Id
            };

            // Act
            var result = (JsonResult)controller.GetResults(studentData);
            var dataResult = result.Data.GetType().GetProperty("Error", BindingFlags.Instance | BindingFlags.Public);
            var dataVal = dataResult.GetValue(result.Data, null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(false, dataVal, TestContext.TestName);
        }
        #endregion GetResults

        #region GetRefreshRate
        [TestMethod]
        public void Controller_Game_Post_GetRefreshRate_Valid_Model_Should_Pass()
        {
            // Arrange
            var controller = new GameController();

            var data = DataSourceBackend.Instance.GameBackend.GetDefault();

            // Act
            var result = (JsonResult)controller.GetRefreshRate();
            var dataResult = result.Data.GetType().GetProperty("Data", BindingFlags.Instance | BindingFlags.Public);
            var dataVal = dataResult.GetValue(result.Data, null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(data.RefreshRate.TotalMilliseconds, dataVal, TestContext.TestName);
        }
        #endregion GetRefreshRate
    }
}

// Store for later reference
//var expect = new JsonResult
//{
//    Data = new
//    {
//        Error = false,
//        Msg = "OK",
//        data = "0",
//    }
//};

