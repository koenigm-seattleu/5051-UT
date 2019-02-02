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
    public class StudentBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Initialize
        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }
        #endregion Initialize

        #region Read
        [TestMethod]
        public void Backend_StudentBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Read_Invalid_ID_Does_Not_Exist_Should_Fail()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var testStudent = new StudentModel();

            //act
            var result = backend.Read(testStudent.Id);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Read_Valid_ID_No_Attendance_Should_Pass()
        {
            //arrange
            var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Read(expectStudent.Id);

            //assert
            Assert.AreEqual(expectStudent ,result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Read_Valid_ID_Has_Attendance_Should_Pass()
        {
            //arrange
            var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var tempAttendance = new AttendanceModel();
            expectStudent.Attendance.Add(tempAttendance);

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Read(expectStudent.Id);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expectStudent, result, TestContext.TestName);
        }
        #endregion Read

        #region Update
        [TestMethod]
        public void Backend_StudentBackend_Update_Invalid_Data_Null_Should_Fail()
        {
            //arrange

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Update(null);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Update_Invalid_Data_Does_Not_Exist_Should_Fail()
        {
            //arrange
            var testStudent = new StudentModel();

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Update(testStudent);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Update_Valid_Data_Status_Change_Should_Pass()
        {
            //arrange
            var statusAfterEnum = _5051.Models.StudentStatusEnum.In;
            var testStudent = new StudentModel
            {
                Status = statusAfterEnum,
                Id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id
            };

            //act
            var updateResult = DataSourceBackend.Instance.StudentBackend.Update(testStudent);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(updateResult, TestContext.TestName);
            Assert.AreEqual(statusAfterEnum, updateResult.Status, TestContext.TestName);
        }
        #endregion

        #region Delete
        [TestMethod]
        public void Backend_StudentBackend_Delete_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var expect = false;

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Delete(null);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_Delete_Valid_ID_Should_Pass()
        {
            //arrange
            var testStudent = new StudentModel();
            var createResult = DataSourceBackend.Instance.StudentBackend.Create(testStudent);
            var expect = true;

            //act
            var deleteResult = DataSourceBackend.Instance.StudentBackend.Delete(createResult.Id);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expect, deleteResult, TestContext.TestName);
        }
        #endregion

        #region Index
        [TestMethod]
        public void Backend_StudentBackend_Index_Valid_Should_Pass()
        {
            //arrange

            //act
            var result = DataSourceBackend.Instance.StudentBackend.Index();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion

        #region ToggleStatus
        [TestMethod]
        public void Backend_StudentBackend_ToggleStatusById_Invalid_ID_Null_Should_Fail()
        {
            //arrange

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleStatusById(null);
            var result = DataSourceBackend.Instance.StudentBackend;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleStatusById_Invalid_ID_Does_Not_Exist_Should_Fail()
        {
            //arrange
            var backend = StudentBackend.Instance;
            var testStudent = new StudentModel();

            //act
            backend.ToggleStatusById(testStudent.Id);
            var result = backend;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleStatusById_Valid_Id_Case_In_Should_Pass()
        {
            //arrange
            var testStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expectToggleResult = _5051.Models.StudentStatusEnum.Out;
            testStudent.Status = _5051.Models.StudentStatusEnum.In;
            var updateResult = DataSourceBackend.Instance.StudentBackend.Update(testStudent);

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleStatusById(testStudent.Id);
            var toggleResult = testStudent.Status;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expectToggleResult, toggleResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleEmotionStatusById_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var emotion = EmotionStatusEnum.Neutral;

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleEmotionStatusById(null, emotion);
            var result = DataSourceBackend.Instance.StudentBackend;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleEmotionStatusById_Invalid_ID_Does_Not_Exist_Should_Fail()
        {
            //arrange
            var testStudent = new StudentModel();
            var emotion = EmotionStatusEnum.Neutral;

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleEmotionStatusById(testStudent.Id, emotion);
            var result = DataSourceBackend.Instance.StudentBackend;

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleEmotionStatusById_Valid_Id_Should_Pass()
        {
            //arrange
            var testStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            testStudent.Status = StudentStatusEnum.Out;
            var updateResult = DataSourceBackend.Instance.StudentBackend.Update(testStudent);
            var emotion = EmotionStatusEnum.Neutral;

            var expectToggleStatus = StudentStatusEnum.In;
            var expectToggleEmotion = EmotionStatusEnum.Neutral;

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleEmotionStatusById(testStudent.Id, emotion);
            var toggleStatus = testStudent.Status;
            var toggleEmotion = testStudent.EmotionCurrent;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expectToggleStatus, toggleStatus, TestContext.TestName);
            Assert.AreEqual(expectToggleEmotion, toggleEmotion, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleStatus_Valid_Id_Case_Out_Should_Pass()
        {
            //arrange
            var testStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expectToggleResult = _5051.Models.StudentStatusEnum.In;
            testStudent.Status = _5051.Models.StudentStatusEnum.Out;
            var updateResult = DataSourceBackend.Instance.StudentBackend.Update(testStudent);

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleStatus(testStudent);
            var toggleResult = testStudent.Status;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expectToggleResult, toggleResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleStatus_Valid_Id_Case_Hold_Should_Pass()
        {
            //arrange
            var testStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expectToggleResult = _5051.Models.StudentStatusEnum.Out;
            testStudent.Status = _5051.Models.StudentStatusEnum.Hold;
            var updateResult = DataSourceBackend.Instance.StudentBackend.Update(testStudent);

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleStatus(testStudent);
            var toggleResult = testStudent.Status;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.AreEqual(expectToggleResult, toggleResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ToggleStatus_Invalid_ID_Null_Should_Fail()
        {
            //arrange

            //act
            DataSourceBackend.Instance.StudentBackend.ToggleStatus(null);
            var result = DataSourceBackend.Instance.StudentBackend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ToggleStatus

        #region SetLogin / Logout
        [TestMethod]
        public void Backend_StudentBackend_SetLogIn_Invalid_Id_Null_Should_Fail()
        {
            //arrange

            //act
            DataSourceBackend.Instance.StudentBackend.SetLogIn(null);
            var result = DataSourceBackend.Instance.StudentBackend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_SetLogOut_Invalid_ID_Null_Should_Fail()
        {
            //arrange

            //act
            DataSourceBackend.Instance.StudentBackend.SetLogOut(null);
            var result = DataSourceBackend.Instance.StudentBackend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_SetLogOut_Valid_Data_Has_Attendance_Should_Pass()
        {
            //arrange
            var testStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var tempAttendance = new AttendanceModel();
            testStudent.Attendance.Add(tempAttendance);

            //act
            DataSourceBackend.Instance.StudentBackend.SetLogOut(testStudent);
            var result = DataSourceBackend.Instance.StudentBackend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region SetDataSource
        [TestMethod]
        public void Backend_StudentBackend_SetDataSource_Valid_Enum_SQL_Should_Pass()
        {
            //arrange
            var sqlEnum = _5051.Models.DataSourceEnum.SQL;

            //act
            DataSourceBackend.Instance.SetDataSource(sqlEnum);
            var result = DataSourceBackend.Instance.StudentBackend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_SetDataSourceDataSet_Valid_Enum_UnitTests_Should_Pass()
        {
            //arrange
            var unitEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            //act
            DataSourceBackend.Instance.SetDataSourceDataSet(unitEnum);
            var result = DataSourceBackend.Instance.StudentBackend;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region ResetStatusAndProcessNewAttendance
        [TestMethod]
        public void Backend_StudentBackend_ResetStatusAndProcessNewAttendance_Valid_Should_Pass()
        {
            // Arrange
            var expect = true;

            // Act
            DataSourceBackend.Instance.StudentBackend.ResetStatusAndProcessNewAttendance();

            // Reset
            DataSourceBackend.Instance.Reset();
            var result = true;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_ResetStatusAndProcessNewAttendance_InValid_IsNew_False_Should_Skip()
        {
            // Arrange
            var expect = true;

            // Set is New to false for all items
            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            // Add an Attendance Record
            var data = new AttendanceModel
            {
                Out = DateTime.UtcNow
            };
            data.In = data.Out.AddMinutes(-1);  // Make Time in earlier than Time out, but only 1 minutes, so no tokens
            data.IsNew = false;
            student.Attendance.Add(data);

            DataSourceBackend.Instance.StudentBackend.Update(student);

            // Act
            DataSourceBackend.Instance.StudentBackend.ResetStatusAndProcessNewAttendance();

            // Reset
            DataSourceBackend.Instance.Reset();
            var result = true;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }


        #endregion ResetStatusAndProcessNewAttendance

        #region CalculateTokens
        [TestMethod]
        public void Backend_StudentBackend_CalculateTokens_InValid_Should_Fail()
        {
            // Arrange
            var expect = 0;

            var data = new AttendanceModel();
            data = null;

            // Act
            var result = DataSourceBackend.Instance.StudentBackend.CalculateTokens(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_CalculateTokens_Attendance_Out_Earler_Than_In_Should_Fail()
        {
            // Arrange
            var expect = 0;

            var data = new AttendanceModel
            {
                In = DateTime.UtcNow
            };
            data.Out = data.In.AddMinutes(-1);  // Make Time out earlier than Time in, resulting in no duration

            // Act
            var result = DataSourceBackend.Instance.StudentBackend.CalculateTokens(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_CalculateTokens_Attendance_1Min_Should_Fail()
        {
            // Arrange
            var expect = 0;

            var firstDay = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault();
            var data = new AttendanceModel
            {
                Out = firstDay.Date,
                In = firstDay.Date
            };

            data.In = data.In.AddMinutes(firstDay.TimeStart.TotalMinutes);
            data.Out = data.In.AddMinutes(1);   // Attended class for 1 min.

            data.In = UTCConversionsBackend.KioskTimeToUtc(data.In);
            data.Out = UTCConversionsBackend.KioskTimeToUtc(data.Out);
            // Act
            var result = DataSourceBackend.Instance.StudentBackend.CalculateTokens(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_CalculateTokens_Attendance_6Min_Should_Pass()
        {
            // Arrange
            var expect = 1;

            var firstDay = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault();
            var data = new AttendanceModel
            {
                Out = firstDay.Date,
                In = firstDay.Date
            };

            data.In = data.In.AddMinutes(firstDay.TimeStart.TotalMinutes);
            data.Out = data.In.AddMinutes(6);   // Attended class for 6 min.

            data.In = UTCConversionsBackend.KioskTimeToUtc(data.In);
            data.Out = UTCConversionsBackend.KioskTimeToUtc(data.Out);

            // Act
            var result = DataSourceBackend.Instance.StudentBackend.CalculateTokens(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_CalculateTokens_Attendance_1hr_Should_Pass()
        {
            // Arrange
            var expect = 1;

            var firstDay = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault();
            var data = new AttendanceModel
            {
                Out = firstDay.Date,
                In = firstDay.Date
            };

            data.In = data.In.AddMinutes(firstDay.TimeStart.TotalMinutes);
            data.Out = data.In.AddMinutes(60);   // Attended class for 60 min.

            data.In = UTCConversionsBackend.KioskTimeToUtc(data.In);
            data.Out = UTCConversionsBackend.KioskTimeToUtc(data.Out);

            // Act
            var result = DataSourceBackend.Instance.StudentBackend.CalculateTokens(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_CalculateTokens_Attendance_2hr_Should_Pass()
        {
            // Arrange
            var expect = 2;

            var firstDay = DataSourceBackend.Instance.SchoolCalendarBackend.GetDefault();
            var data = new AttendanceModel
            {
                Out = firstDay.Date,
                In = firstDay.Date
            };

            data.In = data.In.AddMinutes(firstDay.TimeStart.TotalMinutes);
            data.Out = data.In.AddHours(2);   // Attended class for 2 hrs

            data.In = UTCConversionsBackend.KioskTimeToUtc(data.In);
            data.Out = UTCConversionsBackend.KioskTimeToUtc(data.Out);

            // Act
            var result = DataSourceBackend.Instance.StudentBackend.CalculateTokens(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_CalculateTokens_Invalid_Attendance_Should_Fail()
        {
            // Arrange
            var expect = 0;

            var data = new AttendanceModel
            {
                Out = DateTime.Parse("1/1/2100")  // Make date out of range, so return null
            };
            data.In = data.Out.AddHours(-1);  // Make Time in earlier than Time out, but only 1 minutes, so no tokens

            // Act
            var result = DataSourceBackend.Instance.StudentBackend.CalculateTokens(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion CalculateTokens

        #region CalculateEffectiveDuration
        [TestMethod]
        public void Backend_StudentBackend_CalculateEffectiveDuration_Invalid_Attendance_Null_Should_Fail()
        {
            // Arrange
            var expect = TimeSpan.Zero;

            var data = new AttendanceModel();
            data = null;

            // Act
            var result = DataSourceBackend.Instance.StudentBackend.CalculateEffectiveDuration(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentBackend_CalculateEffectiveDuration_Invalid_Date_Bad_Range_Should_Fail()
        {
            // Arrange
            var expect = TimeSpan.Zero;

            var data = new AttendanceModel
            {
                Out = DateTime.Parse("1/1/2100")  // Make date out of range, so return null
            };
            data.In = data.Out.AddHours(-1);  // Make Time in earlier than Time out, but only 1 minutes, so no tokens

            // Act
            var result = DataSourceBackend.Instance.StudentBackend.CalculateEffectiveDuration(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion CalculateEffectiveDuration
    }
}
