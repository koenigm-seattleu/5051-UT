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
    public class SchoolCalendarDataSourceTableUnitTests
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
        public void Backend_SchoolCalendarDataSourceTable_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = SchoolCalendarDataSourceTable.Instance;

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
        public void Backend_SchoolCalendarDataSourceTable_Read_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = SchoolCalendarDataSourceTable.Instance;

            // Act
            var result = backend.Read(null);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarDataSourceTable_Read_Valid_ID_Should_Pass()
        {
            // Arrange
            var backend = SchoolCalendarDataSourceTable.Instance;
            var schoolCalendarBackend = SchoolCalendarBackend.Instance;
            var expectSchoolCalendarModel = schoolCalendarBackend.GetDefault();

            // Act
            var result = backend.Read(expectSchoolCalendarModel.Id);

            // Assert
            Assert.AreEqual(expectSchoolCalendarModel, result, TestContext.TestName);
        }
        #endregion read

        #region update
        [TestMethod]
        public void Backend_SchoolCalendarDataSourceTable_Update_Invalid_Data_Null_Should_Fail()
        {
            // Arrange
            var backend = SchoolCalendarDataSourceTable.Instance;

            // Act
            var result = backend.Update(null);

            //reset
            backend.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarDataSourceTable_Update_Invalid_Data_Calendar_Not_Found_Should_Fail()
        {
            // Arrange
            var backend = SchoolCalendarDataSourceTable.Instance;
            var expectSchoolCalendarModel = new SchoolCalendarModel();

            // Act
            var result = backend.Update(expectSchoolCalendarModel);

            //reset
            backend.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarDataSourceTable_Update_Valid_Data_Should_Pass()
        {
            // Arrange
            var backend = SchoolCalendarDataSourceTable.Instance;
            var schoolCalendarBackend = SchoolCalendarBackend.Instance;
            var expectSchoolCalendarModel = schoolCalendarBackend.GetDefault();
            var expectId = "GoodId";
            var expectDate = DateTime.UtcNow;
            var expectTimeDuration = new TimeSpan(6, 0, 0);
            var expectTimeStart = new TimeSpan(8, 0, 0);
            var expectTimeEnd = new TimeSpan(14, 0, 0);
            var expectDayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Normal;
            var expectDayEnd = _5051.Models.Enums.SchoolCalendarDismissalEnum.Normal;
            var expectModified = true;
            var expectSchoolDay = true;

            // Act
            expectSchoolCalendarModel.Id = expectId;
            expectSchoolCalendarModel.Date = expectDate;
            expectSchoolCalendarModel.TimeDuration = expectTimeDuration;
            expectSchoolCalendarModel.TimeStart = expectTimeStart;
            expectSchoolCalendarModel.TimeEnd = expectTimeEnd;
            expectSchoolCalendarModel.DayStart = expectDayStart;
            expectSchoolCalendarModel.DayEnd = expectDayEnd;
            expectSchoolCalendarModel.Modified = expectModified;
            expectSchoolCalendarModel.SchoolDay = expectSchoolDay;

            var resultUpdate = backend.Update(expectSchoolCalendarModel);

            //reset
            backend.Reset();
            schoolCalendarBackend.Reset();

            // Assert
            Assert.AreEqual(expectSchoolCalendarModel, resultUpdate, TestContext.TestName);
            Assert.AreEqual(expectId, resultUpdate.Id, TestContext.TestName);
            Assert.AreEqual(expectDate, resultUpdate.Date, TestContext.TestName);
            Assert.AreEqual(expectTimeDuration, resultUpdate.TimeDuration, TestContext.TestName);
            Assert.AreEqual(expectTimeStart, resultUpdate.TimeStart, TestContext.TestName);
            Assert.AreEqual(expectTimeEnd, resultUpdate.TimeEnd, TestContext.TestName);
            Assert.AreEqual(expectDayStart, resultUpdate.DayStart, TestContext.TestName);
            Assert.AreEqual(expectDayEnd, resultUpdate.DayEnd, TestContext.TestName);
            Assert.AreEqual(expectModified, resultUpdate.Modified, TestContext.TestName);
            Assert.AreEqual(expectSchoolDay, resultUpdate.SchoolDay, TestContext.TestName);
        }
        #endregion update

        #region delete
        [TestMethod]
        public void Backend_SchoolCalendarDataSourceTable_Delete_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = SchoolCalendarDataSourceTable.Instance;
            var expect = false;

            // Act
            var result = backend.Delete(null);

            //reset
            backend.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarDataSourceTable_Delete_Valid_ID_Should_Pass()
        {
            // Arrange
            var backend = SchoolCalendarDataSourceTable.Instance;
            var schoolCalendarBackend = SchoolCalendarBackend.Instance;
            var expectSchoolCalendarModel = schoolCalendarBackend.GetDefault();
            var expect = true;

            // Act
            var result = backend.Delete(expectSchoolCalendarModel.Id);

            //reset
            backend.Reset();
            schoolCalendarBackend.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarDataSourceTable_Delete_Invalid_ID_Bogus_Should_Fail()
        {
            // Arrange
            var backend = SchoolCalendarDataSourceTable.Instance;
            var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expect = false;

            // Act
            var result = backend.Delete("bogus");

            //reset
            DataSourceBackend.Instance.Reset();
            //studentBackend.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region DataSet
        [TestMethod]
        public void Backend_SchoolCalendarDataSourceTable_LoadDataSet_Valid_Enum_Demo_Should_Pass()
        {
            // Arrange
            var backend = SchoolCalendarDataSourceTable.Instance;
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
        public void Backend_SchoolCalendarDataSourceTable_LoadDataSet_Valid_Enum_UnitTest_Should_Pass()
        {
            // Arrange
            var backend = SchoolCalendarDataSourceTable.Instance;
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

        #region ReadDate
        [TestMethod]
        public void Backend_SchoolCalendarDataSourceTable_ReadDate_Should_Pass()
        {
            // Arrange
            var backend = SchoolCalendarDataSourceTable.Instance;
            var expectDate = SchoolDismissalSettingsBackend.Instance.GetDefault().DayFirst;

            // Act
            var result = backend.ReadDate(expectDate);

            //reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion ReadDate
    }
}
