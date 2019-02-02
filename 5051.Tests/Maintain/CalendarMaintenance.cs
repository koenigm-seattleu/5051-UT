using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Maintain;
using _5051.Backend;
using _5051.Models;

namespace _5051.Tests.Maintenance
{
    [TestClass]
    public class CalendarMaintenanceTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Maintenance_Calendar_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var Maintenance = new CalendarMaintenance();

            // Act
            var result = Maintenance.GetType();

            // Assert
            Assert.AreEqual(result, new CalendarMaintenance().GetType(), TestContext.TestName);
        }

        [TestMethod]
        public void Maintenance_Calendar_Reset_Default_Should_Pass()
        {
            // Arrange
            var Maintenance = new CalendarMaintenance();

            // Act
            var result = Maintenance.ResetCalendar();

            // Assert
            Assert.AreEqual(true, result, TestContext.TestName);
        }
        #endregion Instantiate

        #region TestRemoveDuplicateOrNot
        [TestMethod]
        public void Maintenance_Reset_Test_Remove_Duplicate_Or_Not_Should_Pass()
        {
            var Maintenance = new CalendarMaintenance();

            var calendarSet = DataSourceBackend.Instance.SchoolCalendarBackend.Index();
            calendarSet.Clear();

            var calendarModel1 = new SchoolCalendarModel();
            calendarModel1.Date = new DateTime(2010, 6, 4);

            var calendarModel2 = new SchoolCalendarModel();
            calendarModel2.Date = new DateTime(2010, 6, 5);

            var calendarModel3 = new SchoolCalendarModel();
            calendarModel3.Date = new DateTime(2010, 6, 5);

            calendarSet.Add(calendarModel1);
            calendarSet.Add(calendarModel2);
            calendarSet.Add(calendarModel3);

            Assert.AreEqual(calendarSet.Count(), 3, TestContext.TestName);

            var result = Maintenance.ResetCalendar();

            // Assert
            Assert.AreEqual(calendarSet.Count(), 2, TestContext.TestName);

            // reset
            DataSourceBackend.Instance.SchoolCalendarBackend.Reset();
        }

        #endregion TestRemoveDuplicateOrNot
    }
}