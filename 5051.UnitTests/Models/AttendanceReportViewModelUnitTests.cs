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
    public class AttendanceReportViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_AttendanceReportViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new AttendanceReportViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_AttendanceReportViewModel_Default_Instantiate_Get_Set_Should_Pass()
        {
            //arrange
            var result = new AttendanceReportViewModel();
            var expectDate = DateTime.Today;
            var expectIsSchoolDay = false;
            var expectTimeIn = DateTime.UtcNow;
            var expectTimeOut = DateTime.UtcNow;
            var expectTotalHours = TimeSpan.Zero;
            var expectTotalHoursExpected = TimeSpan.MaxValue;
            var expectHoursAttended = TimeSpan.Zero;
            var expectHoursExpected = TimeSpan.MaxValue;
            var expectPercentAttended = 10;
            var expectAttendanceStatus = _5051.Models.Enums.AttendanceStatusEnum.Present;
            var expectCheckInStatus = _5051.Models.Enums.CheckInStatusEnum.ArriveOnTime;
            var expectCheckOutstatus = _5051.Models.Enums.CheckOutStatusEnum.DoneEarly;
            var expectEmotion = _5051.Models.EmotionStatusEnum.Happy;
            var expectEmotionUri = Emotion.GetEmotionURI(result.Emotion);

            // Act
            result.Date = expectDate;
            result.IsSchoolDay = expectIsSchoolDay;
            result.TimeIn = expectTimeIn;
            result.TimeOut = expectTimeOut;
            result.TotalHours = expectTotalHours;
            result.TotalHoursExpected = expectTotalHoursExpected;
            result.HoursAttended = expectHoursAttended;
            result.HoursExpected = expectHoursExpected;
            result.PercentAttended = expectPercentAttended;
            result.AttendanceStatus = expectAttendanceStatus;
            result.CheckInStatus = expectCheckInStatus;
            result.CheckOutStatus = expectCheckOutstatus;
            result.Emotion = expectEmotion;
            result.EmotionUri = expectEmotionUri;

            // Assert
            Assert.AreEqual(expectDate, result.Date, TestContext.TestName);
            Assert.AreEqual(expectIsSchoolDay, result.IsSchoolDay, TestContext.TestName);
            Assert.AreEqual(expectTimeIn, result.TimeIn, TestContext.TestName);
            Assert.AreEqual(expectTimeOut, result.TimeOut, TestContext.TestName);
            Assert.AreEqual(expectTotalHours, result.TotalHours, TestContext.TestName);
            Assert.AreEqual(expectTotalHoursExpected, result.TotalHoursExpected, TestContext.TestName);
            Assert.AreEqual(expectHoursAttended, result.HoursAttended, TestContext.TestName);
            Assert.AreEqual(expectHoursExpected, result.HoursExpected, TestContext.TestName);
            Assert.AreEqual(expectPercentAttended, result.PercentAttended, TestContext.TestName);
            Assert.AreEqual(expectAttendanceStatus, result.AttendanceStatus, TestContext.TestName);
            Assert.AreEqual(expectCheckInStatus, result.CheckInStatus, TestContext.TestName);
            Assert.AreEqual(expectCheckOutstatus, result.CheckOutStatus, TestContext.TestName);
            Assert.AreEqual(expectEmotion, result.Emotion, TestContext.TestName);
            Assert.AreEqual(expectEmotionUri, result.EmotionUri, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
