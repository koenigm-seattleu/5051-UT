using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Backend;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class BaseReportViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Models_BaseReportViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new BaseReportViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate
        #region Get_Set_All_Fields
        [TestMethod]
        public void Models_BaseReportViewModel_Get_Set_Check_All_Fields_Should_Pass()
        {
            // Arrange




            // Act
            // Set all the fields for a BaseReportViewModel
            var test = new BaseReportViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                Student = DataSourceBackend.Instance.StudentBackend.GetDefault(),
                DateStart = new DateTime(2000, 12, 30),
                DateEnd = new DateTime(2018, 8, 7),
                AttendanceList = new List<AttendanceReportViewModel>(),
                Stats = new StudentReportStatsModel(),
                Goal = 100,
                YearArray = "2018, 2018",
                MonthArray = "1, 1",
                DayArray = "1, 1",
                PerfectValues = "100, 200",
                GoalValues = "80, 160",
                ActualValues = "50, 100",
                EmotionLevelValues = "1, 3"
            };

            var expectedStudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            var expectedStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expectedDateStart = new DateTime(2000, 12, 30);
            var expectedDateEnd = new DateTime(2018, 8, 7);
            var expectedGoal = 100;
            var expectedYearArray = "2018, 2018";
            var expectedMonthArray = "1, 1";
            var expectedDayArray = "1, 1";
            var expectedPerfectValues = "100, 200";
            var expectedGoalValues = "80, 160";
            var expectedActualValues = "50, 100";
            var expectedEmotionLevelValues = "1, 3";
            // Assert

            //Check each value
            Assert.AreEqual(test.StudentId, expectedStudentId, "StudentId " + TestContext.TestName);
            Assert.AreEqual(test.Student, expectedStudent, "Student " + TestContext.TestName);
            Assert.AreEqual(test.DateStart, expectedDateStart, "DateStart " + TestContext.TestName);
            Assert.AreEqual(test.DateEnd, expectedDateEnd, "DateEnd " + TestContext.TestName);
            Assert.AreEqual(test.Goal, expectedGoal, "Goal " + TestContext.TestName);
            Assert.IsNotNull(test.AttendanceList, "AttendanceList " + TestContext.TestName);
            Assert.IsNotNull(test.Stats, "Stats " + TestContext.TestName);
            Assert.AreEqual(test.YearArray, expectedYearArray, "StudentId " + TestContext.TestName);
            Assert.AreEqual(test.MonthArray, expectedMonthArray, "StudentId " + TestContext.TestName);
            Assert.AreEqual(test.DayArray, expectedDayArray, "StudentId " + TestContext.TestName);
            Assert.AreEqual(test.PerfectValues, expectedPerfectValues, "StudentId " + TestContext.TestName);
            Assert.AreEqual(test.GoalValues, expectedGoalValues, "StudentId " + TestContext.TestName);
            Assert.AreEqual(test.ActualValues, expectedActualValues, "StudentId " + TestContext.TestName);
            Assert.AreEqual(test.EmotionLevelValues, expectedEmotionLevelValues, "StudentId " + TestContext.TestName);
        }
        #endregion Get_Set_All_Fields
    }
}
