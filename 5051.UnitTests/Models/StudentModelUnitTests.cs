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
    public class StudentModelUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Models_StudentModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new StudentModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentModel_Instantiate_Valid_Data_Should_Pass()
        {
            // Arrange
            var name = "name";

            var expect = name;

            // Act
            var result = new StudentModel(name);

            // Assert
            Assert.AreEqual(expect,result.Name, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentModel_Instantiate_Valid_Model_Data_Should_Pass()
        {
            // Arrange
            var expect = "test";
            var data = new StudentDisplayViewModel
            {
                Name = expect
            };

            // Act
            var result = new StudentModel(data);

            // Assert
            Assert.AreEqual(expect, result.Name, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentModel_Instantiate_Valid_StudentModel_Data_Should_Pass()
        {
            // Arrange
            var expect = "test";
            var data = new StudentModel
            {
                Name = expect
            };

            // Act
            var result = new StudentModel(data);
            data.Name = "bogus";

            // Reset

            // Assert
            Assert.AreEqual(expect, result.Name, TestContext.TestName);
        }

        #endregion Instantiate

        #region Update
        [TestMethod]
        public void Models_StudentModel_Update_Check_All_Fields_Should_Pass()
        {
            // Arrange

            // Set all the fields for a Student
            var data = new StudentModel();

            var test = new StudentModel
            {
                Id = "Id",
                Name = "Name",
                Password = "Password",
                Status = StudentStatusEnum.Hold,
                Tokens = 1000,
                ExperiencePoints = 1000,
                EmotionCurrent = EmotionStatusEnum.Happy,

                AvatarLevel = 10
            };

            var tempAttendance = new AttendanceModel();
            test.Attendance.Add(tempAttendance);

            var tempInventory = new FactoryInventoryModel();
            test.Inventory.Add(tempInventory);

            test.Truck = new ShopTruckFullModel();

            // Act
            data.Update(test);

            // Assert

            // Id should not change...
            Assert.AreNotEqual(test.Id, data.Id, "Name " + TestContext.TestName);

            //Check each value
            Assert.AreEqual(test.Name, data.Name, "Name "+ TestContext.TestName);
            Assert.AreEqual(test.Password, data.Password, "Password " + TestContext.TestName);
            Assert.AreEqual(test.Status, data.Status, "Status " + TestContext.TestName);
            Assert.AreEqual(test.Tokens, data.Tokens, "Tokens " + TestContext.TestName);
            Assert.AreEqual(test.ExperiencePoints, data.ExperiencePoints, "ExperiencePoints " + TestContext.TestName);
            Assert.AreEqual(test.EmotionCurrent, data.EmotionCurrent, "EmotionCurrent " + TestContext.TestName);
            Assert.AreEqual(test.AvatarLevel, data.AvatarLevel, "AvatarLevel " + TestContext.TestName);
            Assert.AreEqual(test.Attendance.Count, data.Attendance.Count, "Attendance " + TestContext.TestName);
            Assert.AreEqual(test.Inventory.Count, data.Inventory.Count, "Inventory " + TestContext.TestName);
            Assert.IsNotNull(data.Truck, "Inventory " + TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentModel_Update_With_Invalid_Data_Null_Should_Fail()
        {
            // Arrange

            var expect = "test";

            var data = new StudentModel
            {
                Id = "test"
            };

            // Act
            data.Update(null);
            var result = data.Id;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        #endregion Update
    }
}
