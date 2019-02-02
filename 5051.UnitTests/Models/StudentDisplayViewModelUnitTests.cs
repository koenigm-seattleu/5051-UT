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
    public class StudentDisplayViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new StudentDisplayViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_With_Data_Should_Pass()
        {
            // Arrange
            var data = new StudentModel
            {
                Id = "hi"
            };
            var expect = "hi";

            // Act
            var returned = new StudentDisplayViewModel(data);
            var result = returned.Id;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_With_Invalid_Data_Null_Should_Fail()
        {
            // Arrange

            // Act
            var result = new StudentDisplayViewModel(null);
      
            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_With_Invalid_Data_Avatar_Null_Should_Fail()
        {
            // Arrange
            var data = new StudentModel
            {
                Id = "hi"
            };

            // Act
            var result = new StudentDisplayViewModel(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_Get_Valid_Data_Should_Pass()
        {
            // Arrange
            var result = new StudentDisplayViewModel();

            //act
            var expectLastDateTime = result.LastDateTime;

            // Assert
            Assert.AreEqual(expectLastDateTime, result.LastDateTime, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_Set_LastDateTime_Should_Pass()
        {
            // Arrange
            var result = new StudentDisplayViewModel();
            var expectLastDateTime = DateTime.UtcNow;

            //act
            result.LastDateTime = expectLastDateTime;

            // Assert
            Assert.AreEqual(expectLastDateTime, result.LastDateTime, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_Set_LastLogin_Should_Pass()
        {
            // Arrange
            var result = new StudentDisplayViewModel();
            var expectLastLogin= DateTime.UtcNow;

            //act
            result.LastLogIn = expectLastLogin;

            // Assert
            Assert.AreEqual(expectLastLogin, result.LastLogIn, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Default_Instantiate_Set_EmotionImgeUri_Should_Pass()
        {
            // Arrange
            var result = new StudentDisplayViewModel();
            var expect = "uri";

            //act
            result.EmotionUri = expect;

            // Assert
            Assert.AreEqual(expect, result.EmotionUri, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Get_Set_MonthlyAttendanceScore_Should_Pass()
        {
            // Arrange
            var expect = 123;
            var data = new StudentDisplayViewModel
            {
                MonthlyAttendanceScore = expect
            };

            // Act
            var result = data.MonthlyAttendanceScore;

            // Reset

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentDisplayViewModel_Get_Set_WeeklyAttendanceScore_Should_Pass()
        {
            // Arrange
            var expect = 123;
            var data = new StudentDisplayViewModel
            {
                WeeklyAttendanceScore = expect
            };

            // Act
            var result = data.WeeklyAttendanceScore;

            // Reset

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        #endregion Instantiate
    }
}
