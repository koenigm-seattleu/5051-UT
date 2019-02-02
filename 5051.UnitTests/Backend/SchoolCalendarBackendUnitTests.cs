using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;
using _5051.Models.Enums;
using System.Linq;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class SchoolCalendarBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region delete
        [TestMethod]
        public void Backend_SchoolCalendarBackend_Delete_Valid_Data_Should_Pass()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;
            var data = new SchoolCalendarModel();
            var createResult = test.Create(data);
            var expect = true;

            //act
            var deleteResult = test.Delete(data.Id);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, deleteResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackend_Delete_With_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;
            var expect = false;

            //act
            var result = test.Delete(null);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region update
        [TestMethod]
        public void Backend_SchoolCalendarBackend_Update_Valid_Data_Should_Pass()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;

            var data = new SchoolCalendarModel();
            var createResult = test.Create(data);

            data.Date = DateTime.Parse("01/23/2018");
            data.TimeDuration = TimeSpan.Parse("04:13");
            data.TimeStart = TimeSpan.Parse("01:15");
            data.TimeEnd= TimeSpan.Parse("02:15");
            data.DayStart = SchoolCalendarDismissalEnum.Late;
            data.DayEnd = SchoolCalendarDismissalEnum.Early;
            data.Modified = true;
            data.SchoolDay = false;

            var expect = data;

            //act
            var updateResult = test.Update(data);

            var result = test.Read(data.Id);

            //reset
            test.Reset();

            //assert
            Assert.IsNotNull(result, "Updated "+TestContext.TestName);
            Assert.AreEqual(expect.Date, result.Date, "Date "+TestContext.TestName);
            Assert.AreEqual(expect.TimeDuration, result.TimeDuration, "TimeDuration "+TestContext.TestName);
            Assert.AreEqual(expect.TimeStart, result.TimeStart, "TimeStart "+TestContext.TestName);
            Assert.AreEqual(expect.TimeEnd, result.TimeEnd, "TimeEnd "+TestContext.TestName);
            Assert.AreEqual(expect.DayStart, result.DayStart, "DayStart " + TestContext.TestName);
            Assert.AreEqual(expect.DayEnd, result.DayEnd, "DayEnd " + TestContext.TestName);
            Assert.AreEqual(expect.Modified, result.Modified, "Modified " + TestContext.TestName);
            Assert.AreEqual(expect.SchoolDay, result.SchoolDay, "SchoolDay " + TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackend_Update_Valid_Reset_Modified_Should_Pass()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;

            var data = new SchoolCalendarModel();
            var createResult = test.Create(data);

            data.Date = DateTime.Parse("01/23/2018");

            var myDefault = new SchoolCalendarModel(data.Date);

            data.TimeDuration = TimeSpan.Parse("04:13");
            data.TimeStart = myDefault.TimeStart;
            data.TimeEnd = myDefault.TimeEnd;

            data.DayStart = SchoolCalendarDismissalEnum.Late;
            data.DayEnd = SchoolCalendarDismissalEnum.Early;
            data.Modified = true;
            data.SchoolDay = false;

            var expect = data;

            //act
            var updateResult = test.Update(data);

            var result = test.Read(data.Id);

            //reset
            test.Reset();

            //assert
            Assert.IsNotNull(result, "Updated " + TestContext.TestName);
            Assert.AreEqual(expect.Date, result.Date, "Date " + TestContext.TestName);
            Assert.AreEqual(expect.TimeDuration, result.TimeDuration, "TimeDuration " + TestContext.TestName);
            Assert.AreEqual(expect.TimeStart, result.TimeStart, "TimeStart " + TestContext.TestName);
            Assert.AreEqual(expect.TimeEnd, result.TimeEnd, "TimeEnd " + TestContext.TestName);
            Assert.AreEqual(expect.DayStart, result.DayStart, "DayStart " + TestContext.TestName);
            Assert.AreEqual(expect.DayEnd, result.DayEnd, "DayEnd " + TestContext.TestName);
            Assert.AreEqual(expect.Modified, result.Modified, "Modified " + TestContext.TestName);
            Assert.AreEqual(expect.SchoolDay, result.SchoolDay, "SchoolDay " + TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackend_Update_With_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;

            //act
            var result = test.Update(null);

            //reset
            test.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion update

        #region SetDefault
        [TestMethod]
        public void Backend_SchoolCalendarBackend_SetDefault_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;

            // act
            string id = null;
            var result = test.SetDefault(id);

            //reset
            test.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackend_SetDefault_Invalid_ID_Bogus_Should_Fail()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;

            // act
            string id = "bogus";
            var result = test.SetDefault(id);

            //reset
            test.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackend_SetDefault_Valid_Should_Pass()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;

            var data = test.GetDefault();

            data.Modified = true;

            var expect = data;

            //act
            var result = test.SetDefault(data.Id);

            //reset
            test.Reset();

            //assert
            Assert.IsNotNull(result, "Updated " + TestContext.TestName);
            Assert.AreEqual(expect.Modified, result.Modified, "Modified " + TestContext.TestName);
        }
        #endregion SetDefault

        #region index
        [TestMethod]
        public void Backend_SchoolCalendarBackend_Index_Valid_Should_Pass()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;

            //act
            var result = test.Index();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion index

        #region read
        [TestMethod]
        public void Backend_SchoolCalendarBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;

            //act
            var result = test.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackend_ReadDate_Invalid_Date_Should_Fail()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;

            //act
            var result = test.ReadDate(DateTime.Parse("01/01/1990"));

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackend_GetToday_Valid_Should_Pass()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;

            //act
            var result = test.GetToday();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackend_GetDefault_Valid_Should_Pass()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;

            //act
            var result = test.GetDefault();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region create
        [TestMethod]
        public void Backend_SchoolCalendarBackend_Create_Valid_Data_Should_Pass()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;
            var data = new SchoolCalendarModel();

            //act
            var result = test.Create(data);

            //reset
            test.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
            Assert.AreEqual(data.Id, result.Id, TestContext.TestName);
        }
        #endregion create

        #region SetDataSourceDataSet
        [TestMethod]
        public void Backend_SchoolCalendarBackend_SetDataSourceDataSet_Uses_MockData_Should_Pass()
        {
            //arrange
            var test = SchoolCalendarBackend.Instance;
            var testDataSourceBackend = DataSourceBackend.Instance;
            var mockEnum = DataSourceDataSetEnum.Demo;

            //act
            testDataSourceBackend.SetDataSourceDataSet(mockEnum);

            //reset
            test.Reset();

            //assert
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackend_SetDataSourceDatSet_Uses_SQLData_Should_Pass()
        {
            //arange
            var test = SchoolCalendarBackend.Instance;
            var testDataSourceBackend = DataSourceBackend.Instance;
            var SQLEnum = DataSourceEnum.SQL;

            //act
            testDataSourceBackend.SetDataSource(SQLEnum);

            //reset
            test.Reset();

            //asset
        }
        #endregion SetDataSourceDataSet

        #region AutoSetNoSchool
        [TestMethod]
        public void Backend_SchoolCalendarBackend_AutoSetNoSchool_InValid_MinValue_Should_Skip()
        {
            // Arrange
            var date = DateTime.MinValue;

            var DateList = DataSourceBackend.Instance.SchoolCalendarBackend.Index();
            var expect = DateList.Where(m => m.SchoolDay == false).ToList();

            // Act
            DataSourceBackend.Instance.SchoolCalendarBackend.AutoSetNoSchool(date);
            var result = DateList.Where(m => m.SchoolDay == false).ToList();

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect.Count, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackend_AutoSetNoSchool_Valid_OneDate_Should_Pass()
        {
            // Arrange

            var DateList = DataSourceBackend.Instance.SchoolCalendarBackend.Index();
            var expect = DateList.Where(
                        m => m.SchoolDay == true &&
                        m.HasAttendance == false
                        ).ToList();

            // set first in the list to a school day with no attendance
            var data = expect.FirstOrDefault();
            data.SchoolDay = true;
            data.HasAttendance = false;
            DataSourceBackend.Instance.SchoolCalendarBackend.Update(data);

            var setDate = data.Date.AddDays(1); // Add 1 day to include this day

            // Act
            DataSourceBackend.Instance.SchoolCalendarBackend.AutoSetNoSchool(setDate);

            // Read that date again
            var result = DataSourceBackend.Instance.SchoolCalendarBackend.Read(data.Id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(false, result.SchoolDay, TestContext.TestName);
        }
        #endregion AutoSetNoSchool
    }
}
