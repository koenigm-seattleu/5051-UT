using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class KioskSettingsModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_KioskSettings_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var myModel = new KioskSettingsModel();

            // Act
            var result = myModel;

            // Assert
            Assert.AreEqual(result.Password, new KioskSettingsModel().Password, TestContext.TestName);
        }

        [TestMethod]
        public void Models_KioskSettings_Instantiate_Get_LastProcessedDate_Should_Pass()
        {
            // Arrange
            var expected = DateTime.UtcNow;

            // Act
            var result = new KioskSettingsModel
            {
                LastProcessedDate = expected
            };

            // Assert
            Assert.AreEqual(expected, result.LastProcessedDate, TestContext.TestName);
        }

        [TestMethod]
        public void Models_KioskSettings_Instantiate_Get_ID_Should_Pass()
        {
            // Arrange
            var myModelNew = new KioskSettingsModel();
            var expectId = "GoodId1";

            // Act
            myModelNew.Id = expectId;

            // Assert
            Assert.AreEqual(expectId, myModelNew.Id, TestContext.TestName);
        }

        [TestMethod]
        public void Models_KioskSettings_Instantiate_Get_Default_Time_Zone_Should_Pass()
        {
            // Arrange
            var myModelNew = new KioskSettingsModel();
            var expectedTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

            // Assert
            Assert.AreEqual(expectedTimeZone, myModelNew.TimeZone, TestContext.TestName);
        }

        [TestMethod]
        public void Models_KioskSettings_Instantiate_With_Data_Should_Pass()
        {
            // Arrange
            var myModelNew = new KioskSettingsModel
            {

                // Set values to test
                Password = "bogus",
                Id = "test"
            };
            var expect = myModelNew.Password;

            // Act
            var myModel = new KioskSettingsModel(myModelNew);
            var result = myModel.Password;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_KioskSettings_Instantiate_With_Data_Null_Should_Fail()
        {
            // Null does nothing.

            // Arrange
            var myTemp = new KioskSettingsModel();
            myTemp.SetDefault();
            var expect = myTemp.Password;

            // Act
            var myModel = new KioskSettingsModel(null);
            var result = myModel.Password;

            // Assert
            Assert.AreEqual(null, result, TestContext.TestName);
        }
        #endregion Instantiate

        #region SetDefault
        [TestMethod]
        public void Models_KioskSettings_SetDefault_Default_Should_Pass()
        {
            // Arrange
            var myModel = new KioskSettingsModel
            {
                Password = "bogus"
            };
            var expect = myModel.Password;

            // Act
            myModel.SetDefault();
            var result = myModel.Password;

            // Assert
            Assert.AreNotEqual(expect, result, TestContext.TestName);
        }
        #endregion SetDefault

        #region Update
        [TestMethod]
        public void Models_KioskSettings_Update_Default_Should_Pass()
        {
            // Arrange
            var myModel = new KioskSettingsModel();
            myModel.SetDefault();

            // Set values to test
            myModel.Password = "bogus";
            myModel.Id = "test";
            var expect = myModel.Password;

            // Act
            myModel.Update(myModel);
            var result = myModel.Password;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_KioskSettings_Update_Null_Should_Fail()
        {
            // Null does nothing.

            // Arrange
            var myModel = new KioskSettingsModel();
            myModel.SetDefault();

            // Set values to test
            myModel.Password = "bogus";
            myModel.Id = "test";
            var expect = myModel.Password;

            // Act
            myModel.Update(null);
            var result = myModel.Password;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion Update
    }
}
