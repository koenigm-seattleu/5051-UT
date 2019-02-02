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
    public class SchoolDismissalSettingsModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_SchoolDismissalSettingsModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new SchoolDismissalSettingsModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SchoolDismissalSettingsModel_Default_Instantiate_Null_Should_Fail()
        {
            // Will return a default item

            // Arrange
            var expect = TimeSpan.Parse("8:55");

            // Act
            var result = new SchoolDismissalSettingsModel(null);

            // Assert
            Assert.AreEqual(expect, result.StartNormal, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SchoolDismissalSettingsModel_Default_Instantiate_Data_Should_Pass()
        {

            // Arrange
            var data = new SchoolDismissalSettingsModel
            {
                StartNormal = TimeSpan.Parse("1:11"),
                StartEarly = TimeSpan.Parse("1:12"),
                StartLate = TimeSpan.Parse("1:13"),

                EndNormal = TimeSpan.Parse("2:11"),
                EndEarly = TimeSpan.Parse("2:12"),
                EndLate = TimeSpan.Parse("2:13")
            };


            var Year = DateTime.UtcNow.Year;
            if (DateTime.UtcNow.Month > 1)
            {
                Year--;
            }
            data.DayFirst = DateTime.Parse("09/01/" + Year.ToString());
            data.DayLast = DateTime.Parse("06/30/" + (Year + 1).ToString());

            // Act
            var result = new SchoolDismissalSettingsModel(data);

            // Assert

            // Checking Each value updated
            Assert.AreEqual(data.StartNormal, result.StartNormal, "StartNormal" + TestContext.TestName);
            Assert.AreEqual(data.StartEarly, result.StartEarly, "StartEarly" +TestContext.TestName);
            Assert.AreEqual(data.StartLate, result.StartLate, "StartLate" + TestContext.TestName);

            Assert.AreEqual(data.EndNormal, result.EndNormal, "EndNormal" + TestContext.TestName);
            Assert.AreEqual(data.EndEarly, result.EndEarly, "EndEarly" + TestContext.TestName);
            Assert.AreEqual(data.EndLate, result.EndLate, "EndLate"+ TestContext.TestName);

            Assert.AreEqual(data.DayFirst, result.DayFirst, "DayFirst" + TestContext.TestName);
            Assert.AreEqual(data.DayLast, result.DayLast, "DayLast"+ TestContext.TestName);
        }

        [TestMethod]
        public void Models_SchoolDismissalSettingsModel_Default_Instantiate_Get_ID_Should_Pass()
        {
            //arrange
            var result = new SchoolDismissalSettingsModel();
            var expectID = "anIDString";

            // Act
            result.Id = expectID;

            // Assert
            Assert.AreEqual(expectID, result.Id, TestContext.TestName);
        }
        #endregion Instantiate

        #region Update

        [TestMethod]
        public void Models_SchoolDismissalSettingsModel_Update_Invalid_Should_Fail()
        {
            // Arrange
            var data = new SchoolDismissalSettingsModel();
            var expect = data.EndNormal;

            // Act
            data.Update(null);
            var result = data.EndNormal;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SchoolDismissalSettingsModel_Update_Valid_Should_Pass()
        {

            // Arrange
            var data = new SchoolDismissalSettingsModel
            {
                StartNormal = TimeSpan.Parse("1:11"),
                StartEarly = TimeSpan.Parse("1:12"),
                StartLate = TimeSpan.Parse("1:13"),

                EndNormal = TimeSpan.Parse("2:11"),
                EndEarly = TimeSpan.Parse("2:12"),
                EndLate = TimeSpan.Parse("2:13")
            };


            var Year = DateTime.UtcNow.Year;
            if (DateTime.UtcNow.Month > 1)
            {
                Year--;
            }
            data.DayFirst = DateTime.Parse("09/01/" + Year.ToString());
            data.DayLast = DateTime.Parse("06/30/" + (Year + 1).ToString());

            // Act
            var result = new SchoolDismissalSettingsModel();
            result.Update(data);

            // Assert

            // Checking Each value updated
            Assert.AreEqual(data.StartNormal, result.StartNormal, "StartNormal" + TestContext.TestName);
            Assert.AreEqual(data.StartEarly, result.StartEarly, "StartEarly" + TestContext.TestName);
            Assert.AreEqual(data.StartLate, result.StartLate, "StartLate" + TestContext.TestName);

            Assert.AreEqual(data.EndNormal, result.EndNormal, "EndNormal" + TestContext.TestName);
            Assert.AreEqual(data.EndEarly, result.EndEarly, "EndEarly" + TestContext.TestName);
            Assert.AreEqual(data.EndLate, result.EndLate, "EndLate" + TestContext.TestName);

            Assert.AreEqual(data.DayFirst, result.DayFirst, "DayFirst" + TestContext.TestName);
            Assert.AreEqual(data.DayLast, result.DayLast, "DayLast" + TestContext.TestName);
        }
        #endregion Update
    }
}
