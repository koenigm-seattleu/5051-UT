using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class SchoolCalendarModelUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Models_SchoolCalendarModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new SchoolCalendarModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SchoolCalendarModel_Default_Instantiate_With_Data_Should_Pass()
        {
            var test = new SchoolCalendarModel
            {
                Modified = true
            };

            // Act
            var result = new SchoolCalendarModel(test);
            var expect = test.Modified;

            // Assert
            Assert.AreEqual(expect, result.Modified, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SchoolCalendarModel_Default_Instantiate_With_Invalid_Data_Should_Fail()
        {
            // Act
            var result = new SchoolCalendarModel(null);
            string expect = null;

            // Assert
            Assert.AreEqual(expect, result.Id, TestContext.TestName);
        }
        #endregion Instantiate

        #region Update
        [TestMethod]
        public void Models_SchoolCalendarModel_Update_Invalid_Null_Should_Fail()
        {
            // Arrange
            var expect = DateTime.Now;
            var data = new SchoolCalendarModel
            {
                Date = expect
            };

            // Act
            data.Update((SchoolCalendarModel)null);

            // Assert
            Assert.AreEqual(expect, data.Date, TestContext.TestName);
        }
        #endregion Update

        #region SetGet
        [TestMethod]
        public void Models_SchoolCalendarModel_SetGet_Should_Pass()
        {
            // Arrange

            var TempId = "123";
            var TempDate = DateTime.UtcNow;
            var TempTimeDuration = TimeSpan.FromHours(6);
            var TempTimeStart = TimeSpan.FromHours(8);
            var TempTimeEnd = TimeSpan.FromHours(14);
            var TempDayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
            var TempDayEnd = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
            var TempModified = true;
            var TempSchoolDay = true;
            var TempHasAttendance = true;

            // Act
            var result = new SchoolCalendarModel(null)
            {
                Id = TempId,
                Date = TempDate,
                TimeDuration = TempTimeDuration,
                TimeStart = TempTimeStart,
                TimeEnd = TempTimeEnd,
                DayStart = TempDayStart,
                DayEnd = TempDayEnd,
                Modified = TempModified,
                SchoolDay = TempSchoolDay,
                HasAttendance = TempHasAttendance
            };

            // Assert
            Assert.AreEqual(TempId, result.Id, TestContext.TestName);
            Assert.AreEqual(TempDate, result.Date, TestContext.TestName);
            Assert.AreEqual(TempTimeDuration, result.TimeDuration, TestContext.TestName);
            Assert.AreEqual(TempTimeStart, result.TimeStart, TestContext.TestName);
            Assert.AreEqual(TempTimeEnd, result.TimeEnd, TestContext.TestName);
            Assert.AreEqual(TempDayStart, result.DayStart, TestContext.TestName);
            Assert.AreEqual(TempDayEnd, result.DayEnd, TestContext.TestName);
            Assert.AreEqual(TempModified, result.Modified, TestContext.TestName);
            Assert.AreEqual(TempSchoolDay, result.SchoolDay, TestContext.TestName);
            Assert.AreEqual(TempHasAttendance, result.HasAttendance, TestContext.TestName);
        }

        #endregion SetGet

        //#region SetSchoolTime
        //[TestMethod]
        //public void Models_SchoolCalendarModel_SetSchoolTime_Start_Early_Should_Pass()
        //{
        //    // Arrange
        //    var data = new SchoolCalendarModel();
        //    data.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
        //    var expect = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().StartEarly;

        //    // Act
        //    data.SetSchoolTime();
        //    var result = data.TimeStart;

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Models_SchoolCalendarModel_SetSchoolTime_Start_Late_Should_Pass()
        //{
        //    // Arrange
        //    var data = new SchoolCalendarModel();
        //    data.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Late;
        //    var expect = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().StartLate;

        //    // Act
        //    data.SetSchoolTime();
        //    var result = data.TimeStart;

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Models_SchoolCalendarModel_SetSchoolTime_Start_Normal_Should_Pass()
        //{
        //    // Arrange
        //    var data = new SchoolCalendarModel();
        //    data.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Normal;
        //    var expect = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().StartNormal;

        //    // Act
        //    data.SetSchoolTime();
        //    var result = data.TimeStart;

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Models_SchoolCalendarModel_SetSchoolTime_End_Early_Should_Pass()
        //{
        //    // Arrange
        //    var data = new SchoolCalendarModel();
        //    data.DayEnd = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
        //    var expect = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().EndEarly;

        //    // Act
        //    data.SetSchoolTime();
        //    var result = data.TimeEnd;

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Models_SchoolCalendarModel_SetSchoolTime_End_Late_Should_Pass()
        //{
        //    // Arrange
        //    var data = new SchoolCalendarModel();
        //    data.DayEnd = _5051.Models.Enums.SchoolCalendarDismissalEnum.Late;
        //    var expect = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().EndLate;

        //    // Act
        //    data.SetSchoolTime();
        //    var result = data.TimeEnd;

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Models_SchoolCalendarModel_SetSchoolTime_End_Normal_Should_Pass()
        //{
        //    // Arrange
        //    var data = new SchoolCalendarModel();
        //    data.DayEnd = _5051.Models.Enums.SchoolCalendarDismissalEnum.Normal;
        //    var expect = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().EndNormal;

        //    // Act
        //    data.SetSchoolTime();
        //    var result = data.TimeEnd;

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}
        //#endregion SetScoolTime

        //#region Update
        //[TestMethod]
        //public void Models_SchoolCalendarModel_Update_Valid_Data_Should_Pass()
        //{
        //    // Arrange
        //    var data = new SchoolCalendarModel();
        //    data.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
        //    data.SetSchoolTime();

        //    // Test is different than initial
        //    var test = new SchoolCalendarModel();
        //    test.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Normal;
        //    test.SetSchoolTime();
        //    test.SchoolDay = true;  // Force the day to a school day, so the test always runs even late at night, or on weekends when devs are running unit tests...

        //    var expect = test.TimeDuration;

        //    // Act
        //    data.Update(test);
        //    var result = data.TimeDuration;

        //    //Reset
        //    DataSourceBackend.Instance.Reset();

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Models_SchoolCalendarModel_Update_InValid_Data_Null_Should_Pass()
        //{
        //    // Arrange
        //    var data = new SchoolCalendarModel();
        //    data.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
        //    data.SetSchoolTime();
        //    var expect = data.TimeDuration;

        //    // Test is different than initial
        //    var test = new SchoolCalendarModel();
        //    test.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Normal;
        //    test.SetSchoolTime();

        //    // Act
        //    data.Update(null);
        //    var result = data.TimeDuration;

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}

        ////[TestMethod]
        ////public void Models_SchoolCalendarModel_Update_Fix_DayStart_Should_Pass()
        ////{

        ////    if (DayStart == SchoolCalendarDismissalEnum.Early && TimeStart != myDefault.StartEarly)
        ////    {
        ////        DayStart = SchoolCalendarDismissalEnum.Normal;
        ////    }

        ////    Arrange
        ////   var data = new SchoolCalendarModel();
        ////    data.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
        ////    data.SetSchoolTime();

        ////    var myDefault = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault();

        ////    Test is different than initial
        ////   var test = new SchoolCalendarModel();
        ////    test.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
        ////    test.TimeStart = TimeSpan.Zero;

        ////    test.SetSchoolTime();
        ////    var expect = myDefault.StartEarly;

        ////    Act
        ////    data.Update(test);
        ////    var result = data.TimeStart;

        ////    Assert
        ////    Assert.AreEqual(expect, result, TestContext.TestName);
        ////}

        //[TestMethod]
        //public void Models_SchoolCalendarModel_Update_Fix_DayStart_StartEarly_Should_Pass()
        //{

        //    //if (DayStart == SchoolCalendarDismissalEnum.Early && TimeStart != myDefault.StartEarly)
        //    //{
        //    //    DayStart = SchoolCalendarDismissalEnum.Normal;
        //    //}

        //    // Arrange
        //    var data = new SchoolCalendarModel();
        //    data.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
        //    data.SetSchoolTime();

        //    var myDefault = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault();

        //    // Test is different than initial
        //    var test = new SchoolCalendarModel();
        //    test.SetSchoolTime();
        //    test.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
        //    test.TimeStart = myDefault.StartLate;   // Set to Start late, Update will reset it

        //    var expect = _5051.Models.Enums.SchoolCalendarDismissalEnum.Normal;

        //    // Act
        //    data.Update(test);
        //    var result = data.DayStart;

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Models_SchoolCalendarModel_Update_Fix_DayStart_StartLate_Should_Pass()
        //{

        //    //if (DayStart == SchoolCalendarDismissalEnum.Late && TimeStart != myDefault.StartLate)
        //    //{
        //    //    DayStart = SchoolCalendarDismissalEnum.Normal;
        //    //}

        //    // Arrange
        //    var data = new SchoolCalendarModel();
        //    data.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
        //    data.SetSchoolTime();

        //    var myDefault = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault();

        //    // Test is different than initial
        //    var test = new SchoolCalendarModel();
        //    test.SetSchoolTime();
        //    test.DayStart = _5051.Models.Enums.SchoolCalendarDismissalEnum.Late;
        //    test.TimeStart = myDefault.StartEarly;   // Set to Start late, Update will reset it

        //    var expect = _5051.Models.Enums.SchoolCalendarDismissalEnum.Normal;

        //    // Act
        //    data.Update(test);
        //    var result = data.DayStart;

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Models_SchoolCalendarModel_Update_Fix_DayEnd_EndEarly_Should_Pass()
        //{
        //    //if (DayEnd == SchoolCalendarDismissalEnum.Early && TimeEnd != myDefault.EndEarly)
        //    //{
        //    //    DayEnd = SchoolCalendarDismissalEnum.Normal;
        //    //}

        //    // Arrange
        //    var data = new SchoolCalendarModel();
        //    data.DayEnd = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
        //    data.SetSchoolTime();

        //    var myDefault = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault();

        //    // Test is different than initial
        //    var test = new SchoolCalendarModel();
        //    test.SetSchoolTime();
        //    test.DayEnd = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
        //    test.TimeEnd = myDefault.EndLate;  

        //    var expect = _5051.Models.Enums.SchoolCalendarDismissalEnum.Normal;

        //    // Act
        //    data.Update(test);
        //    var result = data.DayStart;

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}

        //[TestMethod]
        //public void Models_SchoolCalendarModel_Update_Fix_DayEnd_EndLate_Should_Pass()
        //{
        //    //if (DayEnd == SchoolCalendarDismissalEnum.Late && TimeEnd != myDefault.EndLate)
        //    //{
        //    //    DayEnd = SchoolCalendarDismissalEnum.Normal;
        //    //}

        //    // Arrange
        //    var data = new SchoolCalendarModel();
        //    data.DayEnd = _5051.Models.Enums.SchoolCalendarDismissalEnum.Early;
        //    data.SetSchoolTime();

        //    var myDefault = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault();

        //    // Test is different than initial
        //    var test = new SchoolCalendarModel();
        //    test.SetSchoolTime();
        //    test.DayEnd = _5051.Models.Enums.SchoolCalendarDismissalEnum.Late;
        //    test.TimeEnd = myDefault.EndEarly;

        //    var expect = _5051.Models.Enums.SchoolCalendarDismissalEnum.Normal;

        //    // Act
        //    data.Update(test);
        //    var result = data.DayEnd;

        //    // Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}
        //#endregion Update
    }
}
