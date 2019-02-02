using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class KioskSettingsDataSourceMockUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Backend_KioskSettingsDataSourceMock_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = KioskSettingsDataSourceMock.Instance;

            //var expect = backend;

            // Act
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate

        #region read
        [TestMethod]
        public void Backend_KioskSettingsDataSourceMock_Read_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = KioskSettingsDataSourceMock.Instance;

            // Act
            var result = backend.Read(null);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_KioskSettingsDataSourceMock_Read_valid_Data_Should_Pass()
        {
            // Arrange
            var backend = KioskSettingsDataSourceMock.Instance;
            var tempBackend = KioskSettingsBackend.Instance;

            // Act
            var result = backend.Read(tempBackend.GetDefault().Id);

            //reset
            backend.Reset();
            tempBackend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region update
        [TestMethod]
        public void Backend_KioskSettingsDataSourceMock_Update_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = KioskSettingsDataSourceMock.Instance;

            // Act
            var result = backend.Update(null);

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_KioskSettingsDataSourceMock_Update_Invalid_Data_Should_Fail()
        {
            // Arrange
            var backend = KioskSettingsDataSourceMock.Instance;
            var testModel = new KioskSettingsModel();

            // Act
            var result = backend.Update(testModel);

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_KioskSettingsDataSourceMock_Update_valid_Data_Should_Pass()
        {
            // Arrange
            var backend = KioskSettingsDataSourceMock.Instance;
            var testBackend = KioskSettingsBackend.Instance;
            var testDefault = testBackend.GetDefault();
            var expectId = "GoodID";
            var expectPassword = "passWORD23!";

            // Act
            testDefault.Id = expectId;
            testDefault.Password = expectPassword;

            var resultUpdate = backend.Update(testDefault);

            var resultId = testDefault.Id;
            var resultPassword = testDefault.Password;

            //Reset
            backend.Reset();
            testBackend.Reset();

            // Assert
            Assert.IsNotNull(resultUpdate, TestContext.TestName);
            Assert.AreEqual(expectId, resultId, TestContext.TestName);
            Assert.AreEqual(expectPassword, resultPassword, TestContext.TestName);
        }
        #endregion update

        #region delete
        [TestMethod]
        public void Backend_KioskSettingsDataSourceMock_Delete_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = KioskSettingsDataSourceMock.Instance;

            // Act
            var result = backend.Delete(null);

            //Reset
            backend.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_KioskSettingsDataSourceMock_Delete_Valid_ID_Should_Pass()
        {
            // Arrange
            var backend = KioskSettingsDataSourceMock.Instance;
            var kioskBackend = KioskSettingsBackend.Instance;
            var testDefault = kioskBackend.GetDefault();
            var expectId = testDefault.Id;

            // Act
            var result = backend.Delete(expectId);

            //Reset
            backend.Reset();
            kioskBackend.Reset();

            // Assert
            Assert.IsTrue(result, TestContext.TestName);
        }
        #endregion delete

        #region DataSet
        [TestMethod]
        public void Backend_KioskSettingsDataSourceMock_LoadDataSet_Demo_Data_Should_Pass()
        {
            // Arrange
            var backend = KioskSettingsDataSourceMock.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.Demo;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_KioskSettingsDataSourceMock_LoadDataSet_UnitTest_Data_Should_Pass()
        {
            // Arrange
            var backend = KioskSettingsDataSourceMock.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion DataSet
    }
}
