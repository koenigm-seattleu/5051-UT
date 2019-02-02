using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;
using _5051.Models.Enums;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class SchoolCalendarBackendHelperUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region SetDefault
        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetDefault_Valid_Data_Should_Pass()
        {
            // Arrange
            var data = new SchoolCalendarModel();
            var expect = false;

            // Act
            var result = SchoolCalendarBackendHelper.SetDefault(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result.Modified, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetDefault_Null_Data_Should_Fail()
        {
            // Arrange
            var data = new SchoolCalendarModel();
            data = null;

            var expect = data;

            // Act
            var result = SchoolCalendarBackendHelper.SetDefault(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion SetDefault

        #region SetDefaultTypes
        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetDefaultTypes_Valid_Data_Should_Pass()
        {
            // Arrange
            var data = new SchoolCalendarModel();
            var expect = false;

            // Act
            var result = SchoolCalendarBackendHelper.SetDefaultTypes(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result.Modified, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetDefaultTypes_Null_Data_Should_Fail()
        {
            // Arrange
            var data = new SchoolCalendarModel();
            data = null;

            var expect = data;

            // Act
            var result = SchoolCalendarBackendHelper.SetDefaultTypes(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetDefaultTypes_Valid_Mon_Should_Pass()
        {
            // Arrange
            var date = DateTime.Parse("10/1/2018");
            var data = new SchoolCalendarModel(date);
            var dismissalSettings = SchoolDismissalSettingsBackend.Instance.GetDefault();

            // Act
            var result = SchoolCalendarBackendHelper.SetDefaultTypes(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(dismissalSettings.MonStartType, result.DayStart, TestContext.TestName);

            Assert.AreEqual(dismissalSettings.MonEndType, result.DayEnd, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetDefaultTypes_Valid_Tue_Should_Pass()
        {
            // Arrange
            var date = DateTime.Parse("10/2/2018");
            var data = new SchoolCalendarModel(date);
            var dismissalSettings = SchoolDismissalSettingsBackend.Instance.GetDefault();

            // Act
            var result = SchoolCalendarBackendHelper.SetDefaultTypes(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(dismissalSettings.TueStartType, result.DayStart, TestContext.TestName);

            Assert.AreEqual(dismissalSettings.TueEndType, result.DayEnd, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetDefaultTypes_Valid_Wed_Should_Pass()
        {
            // Arrange
            var date = DateTime.Parse("10/3/2018");
            var data = new SchoolCalendarModel(date);
            var dismissalSettings = SchoolDismissalSettingsBackend.Instance.GetDefault();

            // Act
            var result = SchoolCalendarBackendHelper.SetDefaultTypes(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(dismissalSettings.WedStartType, result.DayStart, TestContext.TestName);

            Assert.AreEqual(dismissalSettings.WedEndType, result.DayEnd, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetDefaultTypes_Valid_Thu_Should_Pass()
        {
            // Arrange
            var date = DateTime.Parse("10/4/2018");
            var data = new SchoolCalendarModel(date);
            var dismissalSettings = SchoolDismissalSettingsBackend.Instance.GetDefault();

            // Act
            var result = SchoolCalendarBackendHelper.SetDefaultTypes(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(dismissalSettings.ThuStartType, result.DayStart, TestContext.TestName);

            Assert.AreEqual(dismissalSettings.ThuEndType, result.DayEnd, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetDefaultTypes_Valid_Fri_Should_Pass()
        {
            // Arrange
            var date = DateTime.Parse("10/5/2018");
            var data = new SchoolCalendarModel(date);
            var dismissalSettings = SchoolDismissalSettingsBackend.Instance.GetDefault();

            // Act
            var result = SchoolCalendarBackendHelper.SetDefaultTypes(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(dismissalSettings.FriStartType, result.DayStart, TestContext.TestName);

            Assert.AreEqual(dismissalSettings.FriEndType, result.DayEnd, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetDefaultTypes_Valid_Sat_Should_Pass()
        {
            // Arrange
            var date = DateTime.Parse("10/6/2018");
            var data = new SchoolCalendarModel(date);
            var dismissalSettings = SchoolDismissalSettingsBackend.Instance.GetDefault();

            // Act
            var result = SchoolCalendarBackendHelper.SetDefaultTypes(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(dismissalSettings.SatStartType, result.DayStart, TestContext.TestName);

            Assert.AreEqual(dismissalSettings.SatEndType, result.DayEnd, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetDefaultTypes_Valid_Sun_Should_Pass()
        {
            // Arrange
            var date = DateTime.Parse("10/7/2018");
            var data = new SchoolCalendarModel(date);
            var dismissalSettings = SchoolDismissalSettingsBackend.Instance.GetDefault();

            // Act
            var result = SchoolCalendarBackendHelper.SetDefaultTypes(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(dismissalSettings.SunStartType, result.DayStart, TestContext.TestName);

            Assert.AreEqual(dismissalSettings.SunEndType, result.DayEnd, TestContext.TestName);
        }
        #endregion SetDefaultTypes

        #region SetSchoolTime
        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetSchoolTime_Null_Data_Should_Fail()
        {
            // Arrange
            var data = new SchoolCalendarModel();
            data = null;

            var expect = data;

            // Act
            var result = SchoolCalendarBackendHelper.SetSchoolTime(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetSchoolTime_Start_Unknown_Should_Return_Default()
        {
            // Arrange
            var data = new SchoolCalendarModel
            {
                DayStart = SchoolCalendarDismissalEnum.Unknown,
                DayEnd = SchoolCalendarDismissalEnum.Early
            };

            var expect = data;

            // Act
            var result = SchoolCalendarBackendHelper.SetSchoolTime(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(false, result.SchoolDay, TestContext.TestName);
            Assert.AreEqual(SchoolDismissalSettingsBackend.Instance.GetDefault().StartNormal, result.TimeStart, TestContext.TestName);
            Assert.AreEqual(SchoolDismissalSettingsBackend.Instance.GetDefault().EndNormal, result.TimeEnd, TestContext.TestName);
            Assert.AreEqual(TimeSpan.Zero, result.TimeDuration, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetSchoolTime_End_Unknown_Should_Return_Default()
        {
            // Arrange
            var data = new SchoolCalendarModel
            {
                DayStart = SchoolCalendarDismissalEnum.Early,
                DayEnd = SchoolCalendarDismissalEnum.Unknown
            };

            var expect = data;

            // Act
            var result = SchoolCalendarBackendHelper.SetSchoolTime(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(false, result.SchoolDay, TestContext.TestName);
            Assert.AreEqual(SchoolDismissalSettingsBackend.Instance.GetDefault().StartNormal, result.TimeStart, TestContext.TestName);
            Assert.AreEqual(SchoolDismissalSettingsBackend.Instance.GetDefault().EndNormal, result.TimeEnd, TestContext.TestName);
            Assert.AreEqual(TimeSpan.Zero, result.TimeDuration, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetSchoolTime_DayStart_DayEnd_Normal_Should_Return_Default()
        {
            // Arrange
            var data = new SchoolCalendarModel
            {
                DayStart = SchoolCalendarDismissalEnum.Normal,
                DayEnd = SchoolCalendarDismissalEnum.Normal
            };

            var expect = data;

            // Act
            var result = SchoolCalendarBackendHelper.SetSchoolTime(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(SchoolDismissalSettingsBackend.Instance.GetDefault().EndNormal, result.TimeEnd, TestContext.TestName);

            Assert.AreEqual(SchoolDismissalSettingsBackend.Instance.GetDefault().StartNormal, result.TimeStart, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetSchoolTime_DayStart_DayEnd_Early_Should_Return_Default()
        {
            // Arrange
            var data = new SchoolCalendarModel
            {
                DayStart = SchoolCalendarDismissalEnum.Early,
                DayEnd = SchoolCalendarDismissalEnum.Early
            };

            var expect = data;

            // Act
            var result = SchoolCalendarBackendHelper.SetSchoolTime(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(SchoolDismissalSettingsBackend.Instance.GetDefault().EndEarly, result.TimeEnd, TestContext.TestName);

            Assert.AreEqual(SchoolDismissalSettingsBackend.Instance.GetDefault().StartEarly, result.TimeStart, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_SchoolCalendarBackendHelper_SetSchoolTime_DayStart_DayEnd_Late_Should_Return_Default()
        {
            // Arrange
            var data = new SchoolCalendarModel
            {
                DayStart = SchoolCalendarDismissalEnum.Late,
                DayEnd = SchoolCalendarDismissalEnum.Late
            };

            var expect = data;

            // Act
            var result = SchoolCalendarBackendHelper.SetSchoolTime(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(SchoolDismissalSettingsBackend.Instance.GetDefault().EndLate, result.TimeEnd, TestContext.TestName);

            Assert.AreEqual(SchoolDismissalSettingsBackend.Instance.GetDefault().StartLate, result.TimeStart, TestContext.TestName);
        }
        #endregion SetSchoolTime
    }
}
