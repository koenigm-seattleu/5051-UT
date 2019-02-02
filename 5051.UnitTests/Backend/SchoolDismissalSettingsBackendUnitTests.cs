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
    public class SchoolDismissalSettingsBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Backend_SchoolDismissalSettingsBackend_Default_Instantiate_Should_Pass()
        {
            //arrange
            var backend = SchoolDismissalSettingsBackend.Instance;

            //act
            var result = backend;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate

        #region Read
        [TestMethod]
        public void Backend_SchoolDismissalSettingsBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var backend = SchoolDismissalSettingsBackend.Instance;

            //act
            var result = backend.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolDismissalSettingsBackend_Read_Valid_ID_Should_Pass()
        {
            //arrange
            var backend = SchoolDismissalSettingsBackend.Instance;
            var expectSchoolDismissalSsettingsModel = backend.GetDefault();

            //act
            var result = backend.Read(expectSchoolDismissalSsettingsModel.Id);

            //assert
            Assert.AreEqual(expectSchoolDismissalSsettingsModel, result, TestContext.TestName);
        }
        #endregion Read

        #region update
        [TestMethod]
        public void Backend_SchoolDismissalSettingsBackend_Update_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var backend = SchoolDismissalSettingsBackend.Instance;

            //act
            var result = backend.Update(null);

            //reset
            backend.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolDismissalSettingsBackend_Update_Valid_Data_Null_Should_Pass()
        {
            //arrange
            var backend = SchoolDismissalSettingsBackend.Instance;
            var expectSchoolDismissalSettingsModel = backend.GetDefault();
            var expectId = "GoodID";
            var expectStartNormal = new TimeSpan(8, 55, 0);
            var expectStartEarly = new TimeSpan(8, 30, 0);
            var expectStartLate = new TimeSpan(10, 55, 0);
            var expectEndNormal = new TimeSpan(15, 45, 0);
            var expectEndEarly = new TimeSpan(14, 0, 0);
            var expectEndLate = new TimeSpan(16, 0, 0);
            var expectDayFirst = new DateTime(2017, 9, 1);
            var expectDayLast = new DateTime(2018, 6, 18);

            //act
            expectSchoolDismissalSettingsModel.Id = expectId;
            expectSchoolDismissalSettingsModel.StartNormal = expectStartNormal;
            expectSchoolDismissalSettingsModel.StartEarly = expectStartEarly;
            expectSchoolDismissalSettingsModel.StartLate = expectStartLate;
            expectSchoolDismissalSettingsModel.EndNormal = expectEndNormal;
            expectSchoolDismissalSettingsModel.EndEarly = expectEndEarly;
            expectSchoolDismissalSettingsModel.EndLate = expectEndLate;
            expectSchoolDismissalSettingsModel.DayFirst = expectDayFirst;
            expectSchoolDismissalSettingsModel.DayLast = expectDayLast;

            var resultUpdate = backend.Update(expectSchoolDismissalSettingsModel);

            //reset
            backend.Reset();

            //assert
            Assert.AreEqual(expectSchoolDismissalSettingsModel, resultUpdate, TestContext.TestName);
            Assert.AreEqual(expectId, resultUpdate.Id, TestContext.TestName);
            Assert.AreEqual(expectStartNormal, resultUpdate.StartNormal, TestContext.TestName);
            Assert.AreEqual(expectStartEarly, resultUpdate.StartEarly, TestContext.TestName);
            Assert.AreEqual(expectStartLate, resultUpdate.StartLate, TestContext.TestName);
            Assert.AreEqual(expectEndNormal, resultUpdate.EndNormal, TestContext.TestName);
            Assert.AreEqual(expectEndEarly, resultUpdate.EndEarly, TestContext.TestName);
            Assert.AreEqual(expectEndLate, resultUpdate.EndLate, TestContext.TestName);
            Assert.AreEqual(expectDayFirst, resultUpdate.DayFirst, TestContext.TestName);
            Assert.AreEqual(expectDayLast, resultUpdate.DayLast, TestContext.TestName);
        }
        #endregion update

        #region SetData
        [TestMethod]
        public void Backend_SchoolDismissalSettingsBackend_SetDataSource_Valid_Enum_SQL_Should_Pass()
        {
            //arrange
            var expectEnum = _5051.Models.DataSourceEnum.SQL;
            var backend = SchoolDismissalSettingsBackend.Instance;

            //act
            SchoolDismissalSettingsBackend.SetDataSource(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolDismissalSettingsBackend_SetDataSourceDataSet_Valid_Enum_UnitTest_Should_Pass()
        {
            //arrange
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;
            var backend = SchoolDismissalSettingsBackend.Instance;

            //act
            SchoolDismissalSettingsBackend.SetDataSourceDataSet(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion SetData
    }
}
