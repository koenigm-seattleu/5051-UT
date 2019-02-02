using System;
using _5051.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class LeaderBoardModelUnitTests
    {     
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_LeaderBoardModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new LeaderBoardModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_LeaderBoardModel_Instantiate_Valid_Should_Pass()
        {
            // Arrange
            var expectProfit = 0;
            var expectName = "name";
            var test = new LeaderBoardModel
            {
                Name = expectName,
                Profit = expectProfit
            };

            // Act
            var result = new LeaderBoardModel(test);

            // Assert
            Assert.AreEqual(expectProfit, result.Profit, TestContext.TestName);
            Assert.AreEqual(expectName, result.Name, TestContext.TestName);
        }
        #endregion Instantiate

        #region Update
        [TestMethod]
        public void Models_LeaderBoardModel_Update_With_Valid_Data_Should_Pass()
        {
            // Arrange
            var expectProfit = 0;
            var expectName = "Name";

            var data = new LeaderBoardModel();

            var test = new LeaderBoardModel
            {
                Profit = expectProfit,
                Name = expectName
            };

            // Act
            data.Update(test);
            var result = data;

            // Assert
            Assert.AreEqual(expectProfit, result.Profit, "Profit " + TestContext.TestName);
            Assert.AreEqual(expectName, result.Name, "Name " + TestContext.TestName);
        }

        [TestMethod]
        public void Models_LeaderBoardModel_Update_With_Invalid_Data_Null_Should_Fail()
        {
            // Arrange

            var expect = "test";

            var data = new LeaderBoardModel
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
