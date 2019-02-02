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
    public class KioskSettingsBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Backend_KioskSettingsBackend_Default_Instantiate_Should_Pass()
        {
            //arrange
            var backend = KioskSettingsBackend.Instance;

            //act
            var result = backend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate

        #region read
        [TestMethod]
        public void Backend_KioskSettingsBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var backend = KioskSettingsBackend.Instance;

            //act
            var result = backend.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_KioskSettingsBackend_Read_Valid_ID_Should_Pass()
        {
            //arrange
            var backend = KioskSettingsBackend.Instance;
            var defaultValue = backend.GetDefault();

            //act
            var result = backend.Read(defaultValue.Id);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region update
        [TestMethod]
        public void Backend_KioskSettingsBackend_Update_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var backend = KioskSettingsBackend.Instance;

            //act
            var result = backend.Update(null);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_KioskSettingsBackend_Update_Valid_Data_Should_Pass()
        {
            //arrange
            var backend = KioskSettingsBackend.Instance;
            var testModel = backend.GetDefault();
            var expectId = "goodId";
            var expectPassword = "passWORD23!";

            //act
            testModel.Id = expectId;
            testModel.Password = expectPassword;

            var updateResult = backend.Update(testModel);

            var resultId = testModel.Id;
            var resultPassword = testModel.Password;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(updateResult, TestContext.TestName);
            Assert.AreEqual(expectId, resultId, TestContext.TestName);
            Assert.AreEqual(expectPassword, resultPassword, TestContext.TestName);
        }
        #endregion update

        #region SetDataSource
        [TestMethod]
        public void Backend_KioskSettingsBackend_SetDataSource_Valid_Enum_Should_Pass()
        {
            //arrange
            var expectEnum = _5051.Models.DataSourceEnum.SQL;
            var backend = KioskSettingsBackend.Instance;

            //act
            KioskSettingsBackend.SetDataSource(expectEnum);
            var result = backend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_KioskSettingsBackend_SetDataSource_Local_Should_Pass()
        {
            //arrange
            var expectEnum = _5051.Models.DataSourceEnum.Local;
            var backend = KioskSettingsBackend.Instance;

            //act
            KioskSettingsBackend.SetDataSource(expectEnum);
            var result = backend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_KioskSettingsBackend_SetDataSourceDataSet_Valid_Enum_Should_Pass()
        {
            //arrange
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;
            var backend = KioskSettingsBackend.Instance;

            //act
            KioskSettingsBackend.SetDataSourceDataSet(expectEnum);
            var result = backend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion SetDataSource


        #region GetLatestDate
        [TestMethod]
        public void Backend_KioskSettingsBackend_GetLatestDate_Should_Pass()
        {
            //arrange

            //act
            var result = KioskSettingsBackend.Instance.GetLatestDate();

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion GetLatestDate

        #region UpdateLatestDate
        [TestMethod]
        public void Backend_KioskSettingsBackend_UpdateLatestDate_Should_Pass()
        {
            //arrange
            var date = DateTime.Parse("01/23/2018");

            //act
            KioskSettingsBackend.Instance.UpdateLatestDate(date);
            var result = KioskSettingsBackend.Instance.GetLatestDate();

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(date,result, TestContext.TestName);
        }
        #endregion UpdateLatestDate
    }
}
