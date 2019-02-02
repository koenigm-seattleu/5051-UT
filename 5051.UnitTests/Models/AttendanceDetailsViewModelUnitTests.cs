using System;
using _5051.Backend;
using _5051.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class AttendanceDetailsViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        [TestMethod]
        public void AttendanceDetailsViewModel_Initialize_Invalid_AttendanceId_Should_Fail()
        {
            var myAttendanceDetails = new AttendanceDetailsViewModel();

            var result = myAttendanceDetails.Initialize(null,null);

            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void AttendanceDetailsViewModel_Initialize_Invalid_AttendanceId_Does_Not_Exist_Should_Fail()
        {
            var myAttendanceDetails = new AttendanceDetailsViewModel();
            var testAttendance = new AttendanceModel();

            var result = myAttendanceDetails.Initialize("bogus",testAttendance.Id);

            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void AttendanceDetailsViewModel_Initialize_Valid_AttendanceId_Should_Pass()
        {
            // Arrange
            var myAttendanceDetails = new AttendanceDetailsViewModel();
            var testStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var testAttendance = new AttendanceModel
            {
                StudentId = testStudent.Id
            };
            testStudent.Attendance.Add(testAttendance);
            DataSourceBackend.Instance.StudentBackend.Update(testStudent);

            // Act
            var result = myAttendanceDetails.Initialize(testStudent.Id, testAttendance.Id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(testStudent.Id, result.Attendance.StudentId, TestContext.TestName);
            Assert.AreEqual(testAttendance.Id, result.Attendance.Id, TestContext.TestName);
        }

        [TestMethod]
        public void AttendanceDetailsViewModel_Get_Set_Should_Pass()
        {
            // Arrange
            var myAttendanceDetails = new AttendanceDetailsViewModel();
            var testStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var testAttendance = new AttendanceModel
            {
                StudentId = testStudent.Id
            };
            testStudent.Attendance.Add(testAttendance);
            DataSourceBackend.Instance.StudentBackend.Update(testStudent);

            // Act
            var result = myAttendanceDetails.Initialize(testStudent.Id, testAttendance.Id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(testStudent.Id, result.Attendance.StudentId, TestContext.TestName);
            Assert.AreEqual(testAttendance.Id, result.Attendance.Id, TestContext.TestName);

            Assert.IsNotNull(result.AvatarComposite, TestContext.TestName);
            Assert.IsNotNull(result.Name, TestContext.TestName);
        }

        [TestMethod]
        public void AttendanceDetailsViewModel_Invalid_Null_Id_Should_Fail()
        {
            // Arrange

            // Act
            var result = new AttendanceDetailsViewModel(null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert

            Assert.IsNull(result.AvatarComposite, TestContext.TestName);
        }

        //[TestMethod]
        //public void AttendanceDetailsViewModel_Valid_Id_Should_Pass()
        //{
        //    // Arrange
        //    var testStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
        //    var testAttendance = new AttendanceModel();
        //    testAttendance.StudentId = testStudent.Id;
        //    testStudent.Attendance.Add(testAttendance);
        //    DataSourceBackend.Instance.StudentBackend.Update(testStudent);

        //    // Act
        //    var result = new AttendanceDetailsViewModel(testAttendance.StudentId);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();

        //    // Assert

        //    Assert.IsNotNull(result.AvatarComposite, TestContext.TestName);
        //}

        [TestMethod]
        public void AttendanceDetailsViewModel_InValid_Id_Bogus_Should_Fail()
        {
            // Arrange

            // Act
            var result = new AttendanceDetailsViewModel("bogus");

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert

            Assert.IsNull(result.AvatarComposite, TestContext.TestName);
        }
    }
}
