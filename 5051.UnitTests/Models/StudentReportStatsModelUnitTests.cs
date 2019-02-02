using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class StudentReportStatsModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_StudentReportStatsModel_Default_Instantiate_Get_Set_Should_Pass()
        {
            //arrange
            var test = new StudentReportStatsModel();
            var expectNumOfSchoolDays = 100;
            var expectAccumulatedTotalHours = TimeSpan.MinValue;
            var expectAccumulatedTotalHoursExpected = TimeSpan.MaxValue;
            var expectDaysPresent = 1;
            var expectDaysAbsentExcused = 2;
            var expectDaysAbsentUnexcused = 3;
            var expectTotalHoursAttended = 4;
            var expectTotalHoursMissing = 5;
            var expectDaysOnTime = 6;
            var expectDaysLate = 7;
            var expectDaysOutsAuto = 7;
            var expectDaysOutEarly = 3;
            var expectPercPresent = 100 * expectDaysPresent / (expectDaysPresent + expectDaysAbsentExcused + expectDaysAbsentUnexcused);
            var expectPercAttendedHours = 100 * expectTotalHoursAttended / (expectTotalHoursAttended + expectTotalHoursMissing);
            var expectPercExcused = 100 * expectDaysAbsentExcused / (expectDaysPresent + expectDaysAbsentExcused + expectDaysAbsentUnexcused);
            var expectPercUnexcused = 100 * expectDaysAbsentUnexcused / (expectDaysPresent + expectDaysAbsentExcused + expectDaysAbsentUnexcused);
            var expectPercInLate = expectDaysLate * 100 / expectDaysPresent + expectDaysAbsentExcused + expectDaysAbsentUnexcused;
            var expectPercOutEarly = expectDaysOutEarly * 100 / expectDaysPresent + expectDaysAbsentExcused + expectDaysAbsentUnexcused;


            //act
            test.NumOfSchoolDays = expectNumOfSchoolDays;
            test.AccumlatedTotalHours = expectAccumulatedTotalHours;
            test.AccumlatedTotalHoursExpected = expectAccumulatedTotalHoursExpected;
            test.DaysPresent = expectDaysPresent;
            test.DaysAbsentExcused = expectDaysAbsentExcused;
            test.DaysAbsentUnexcused = expectDaysAbsentUnexcused;
            test.DaysOnTime = expectDaysOnTime;
            test.DaysLate = expectDaysLate;
            test.DaysOutAuto = expectDaysOutsAuto;
            test.DaysOutEarly = expectDaysOutEarly;
            test.PercPresent = expectPercPresent;
            test.PercAttendedHours = expectPercAttendedHours;
            test.PercExcused = expectPercExcused;
            test.PercUnexcused = expectPercUnexcused;
            test.PercInLate = expectPercInLate;
            test.PercOutEarly = expectPercOutEarly;

            //assert
            Assert.AreEqual(expectNumOfSchoolDays, test.NumOfSchoolDays, TestContext.TestName);
            Assert.AreEqual(expectAccumulatedTotalHours, test.AccumlatedTotalHours, TestContext.TestName);
            Assert.AreEqual(expectAccumulatedTotalHoursExpected, test.AccumlatedTotalHoursExpected, TestContext.TestName);
            Assert.AreEqual(expectDaysPresent, test.DaysPresent, TestContext.TestName);
            Assert.AreEqual(expectDaysAbsentExcused, test.DaysAbsentExcused, TestContext.TestName);
            Assert.AreEqual(expectDaysAbsentUnexcused, test.DaysAbsentUnexcused, TestContext.TestName);
            Assert.AreEqual(expectDaysOnTime, test.DaysOnTime, TestContext.TestName);
            Assert.AreEqual(expectDaysLate, test.DaysLate, TestContext.TestName);
            Assert.AreEqual(expectDaysOutsAuto, test.DaysOutAuto, TestContext.TestName);
            Assert.AreEqual(expectDaysOutEarly, test.DaysOutEarly, TestContext.TestName);
            Assert.AreEqual(expectPercPresent, test.PercPresent, TestContext.TestName);
            Assert.AreEqual(expectPercAttendedHours, test.PercAttendedHours, TestContext.TestName);
            Assert.AreEqual(expectPercExcused, test.PercExcused, TestContext.TestName);
            Assert.AreEqual(expectPercUnexcused, test.PercUnexcused, TestContext.TestName);
            Assert.AreEqual(expectPercInLate, test.PercInLate, TestContext.TestName);
            Assert.AreEqual(expectPercOutEarly, test.PercOutEarly, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
