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
    public class SchoolDismissalSettingsDataSourceTableUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize()]
        public void Initialize()
        {
            DataSourceBackend.SetTestingMode(true);
            DataSourceBackend.Instance.SetDataSource(DataSourceEnum.Local);
        }

        [TestCleanup]
        public void Cleanup()
        {
            DataSourceBackend.SetTestingMode(false);
            DataSourceBackend.Instance.SetDataSource(DataSourceEnum.Mock);
        }

        #region Instantiate
        [TestMethod]
        public void Backend_SchoolDismissalSettingsDataSourceTable_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = SchoolDismissalSettingsDataSourceTable.Instance;

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
        public void Backend_SchoolDismissalSettingsSourceTable_Read_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = SchoolDismissalSettingsDataSourceTable.Instance;

            // Act
            var result = backend.Read(null);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolDismissalSettingsDataSourceTable_Read_Valid_ID_Should_Pass()
        {
            // Arrange
            var backend = SchoolDismissalSettingsDataSourceTable.Instance;
            var schoolDismissalBackend = SchoolDismissalSettingsBackend.Instance;
            var expectSChoolDismissalSettings = schoolDismissalBackend.GetDefault();

            // Act
            var result = backend.Read(expectSChoolDismissalSettings.Id);

            // Assert
            Assert.AreEqual(expectSChoolDismissalSettings, result, TestContext.TestName);
        }
        #endregion read

        #region update
        [TestMethod]
        public void Backend_SchoolDismissalSettingsDataSourceTable_Update_Invalid_Data_Null_Should_Fail()
        {
            // Arrange
            var backend = SchoolDismissalSettingsDataSourceTable.Instance;

            // Act
            var result = backend.Update(null);

            //reset
            backend.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolDismissalSettingsDataSourceTable_Update_Invalid_Data_Settings_Do_Not_Exist_Should_Fail()
        {
            // Arrange
            var backend = SchoolDismissalSettingsDataSourceTable.Instance;
            var expectSchoolDismissalSettingsModel = new SchoolDismissalSettingsModel();

            // Act
            var result = backend.Update(expectSchoolDismissalSettingsModel);

            //reset
            backend.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolDismissalSettingstDataSourceTable_Update_Valid_Data_Should_Pass()
        {
            // Arrange
            var backend = SchoolDismissalSettingsDataSourceTable.Instance;
            var schoolDismissalSettingsBackend = SchoolDismissalSettingsBackend.Instance;
            var expectSchoolDismissalSettings = schoolDismissalSettingsBackend.GetDefault();
            var expectId = "GoodID";
            var expectStartNormal = new TimeSpan(8, 55, 0);
            var expectStartEarly = new TimeSpan(8, 30, 0);
            var expectStartLate = new TimeSpan(10, 55, 0);
            var expectEndNormal = new TimeSpan(15, 45, 0);
            var expectEndEarly = new TimeSpan(14, 0, 0);
            var expectEndLate = new TimeSpan(16, 0, 0);
            var expectDayFirst = new DateTime(2017, 9, 1);
            var expectDayLast = new DateTime(2018, 6, 18);

            // Act
            expectSchoolDismissalSettings.Id = expectId;
            expectSchoolDismissalSettings.StartNormal = expectStartNormal;
            expectSchoolDismissalSettings.StartEarly = expectStartEarly;
            expectSchoolDismissalSettings.StartLate = expectStartLate;
            expectSchoolDismissalSettings.EndNormal = expectEndNormal;
            expectSchoolDismissalSettings.EndEarly = expectEndEarly;
            expectSchoolDismissalSettings.EndLate = expectEndLate;
            expectSchoolDismissalSettings.DayFirst = expectDayFirst;
            expectSchoolDismissalSettings.DayLast = expectDayLast;

            var resultUpdate = backend.Update(expectSchoolDismissalSettings);

            //reset
            backend.Reset();
            schoolDismissalSettingsBackend.Reset();

            // Assert
            Assert.AreEqual(expectSchoolDismissalSettings, resultUpdate, TestContext.TestName);
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

        #region delete
        [TestMethod]
        public void Backend_SchoolDismissalSettingsDataSourceTable_Delete_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = SchoolDismissalSettingsDataSourceTable.Instance;
            var expect = false;

            // Act
            var result = backend.Delete(null);

            //reset
            backend.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolDismissalSettingsDataSourceTable_Delete_Valid_ID_Should_Pass()
        {
            // Arrange
            var backend = SchoolDismissalSettingsDataSourceTable.Instance;
            var schoolDismissalBackend = SchoolDismissalSettingsBackend.Instance;
            var expectSchoolDismissalSettings = schoolDismissalBackend.GetDefault();
            var expect = true;

            // Act
            var result = backend.Delete(expectSchoolDismissalSettings.Id);

            //reset
            backend.Reset();
            schoolDismissalBackend.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolDismissalSettingsDataSourceTable_Delete_Invalid_ID_Bogus_Should_Fail()
        {
            // Arrange
            var backend = SchoolDismissalSettingsDataSourceTable.Instance;
            var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expect = false;

            // Act
            var result = backend.Delete("bogus");

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region DataSet
        [TestMethod]
        public void Backend_SchoolDismissalSettingsDataSourceTable_LoadDataSet_Valid_Enum_Demo_Should_Pass()
        {
            // Arrange
            var backend = SchoolDismissalSettingsDataSourceTable.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.Demo;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolDismissalSettingsDataSourceTable_LoadDataSet_Valid_Enum_UnitTest_Should_Pass()
        {
            // Arrange
            var backend = SchoolDismissalSettingsDataSourceTable.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion DataSet
    }
}
