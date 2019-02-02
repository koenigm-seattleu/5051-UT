using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using System.Globalization;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class GameModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_GameModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new GameModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_GameModel_Default_Instantiate_With_Data_Should_Pass()
        {
            // Arrange
            bool expect = true;

            // Act
            var temp = new GameModel();
            var returned = new GameModel(temp);
            var result = returned.Enabled;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        #endregion Instantiate

        #region Update
        [TestMethod]
        public void Models_GameModel_Update_With_Invalid_Data_Null_Should_Fail()
        {
            // Arrange

            var expect = "test";

            var data = new GameModel
            {
                Id = "test"
            };

            // Act
            data.Update(null);
            var result = data.Id;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_GameModel_Update_With_Valid_Data_Null_Should_Pass()
        {
            // Arrange
            var expect = new GameModel
            {
                Id = "Id",
                Enabled = false,
                IterationNumber = 1000,
                RefreshRate = TimeSpan.ParseExact("01:01:00", @"hh\:mm\:ss", CultureInfo.InvariantCulture, TimeSpanStyles.None),  // default to 1 hr 1 minute
                RunDate = DateTime.Parse("01/23/2018"),
                TimeIteration = TimeSpan.ParseExact("01:01:00", @"hh\:mm\:ss", CultureInfo.InvariantCulture, TimeSpanStyles.None),  // default to 1 hr 1 minute
                Income = 100,
                Outcome = 50
            };

            var result  = new GameModel();

            // Act
            result.Update(expect);

            // Assert
            Assert.AreEqual(expect.Enabled, result.Enabled, "Enabled " + TestContext.TestName);
            Assert.AreEqual(expect.IterationNumber, result.IterationNumber, "IterationNumber " + TestContext.TestName);
            Assert.AreEqual(expect.RefreshRate, result.RefreshRate, "RefreshRate " + TestContext.TestName);
            Assert.AreEqual(expect.RunDate, result.RunDate, "RunDate" + TestContext.TestName);
            Assert.AreEqual(expect.TimeIteration, result.TimeIteration, "TimeIteration" + TestContext.TestName);
            Assert.AreEqual(expect.Income, result.Income, "Income " + TestContext.TestName);
            Assert.AreEqual(expect.Outcome, result.Outcome, "Outcome" + TestContext.TestName);
        }
        #endregion Update
    }
}
