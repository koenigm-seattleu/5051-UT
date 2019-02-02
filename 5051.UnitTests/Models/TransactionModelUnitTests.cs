using System;
using _5051.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class TransactionModelUnitTests
    {     
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_TransactionModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new TransactionModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_TransactionModel_Instantiate_Valid_Should_Pass()
        {
            // Arrange
            var expectUri = "uri";
            var expectName = "name";
            var test = new TransactionModel
            {
                Name = expectName,
                Uri = expectUri
            };

            // Act
            var result = new TransactionModel(test);

            // Assert
            Assert.AreEqual(expectUri, result.Uri, TestContext.TestName);
            Assert.AreEqual(expectName, result.Name, TestContext.TestName);
        }
        #endregion Instantiate

        #region Update
        [TestMethod]
        public void Models_TransactionModel_Update_With_Valid_Data_Should_Pass()
        {
            // Arrange
            var expectUri = "uri";
            var expectName = "Name";

            var data = new TransactionModel();

            var test = new TransactionModel
            {
                Uri = expectUri,
                Name = expectName
            };

            // Act
            data.Update(test);
            var result = data;

            // Assert
            Assert.AreEqual(expectUri, result.Uri, "Uri " + TestContext.TestName);
            Assert.AreEqual(expectName, result.Name, "Name " + TestContext.TestName);
        }

        [TestMethod]
        public void Models_TransactionModel_Update_With_Invalid_Data_Null_Should_Fail()
        {
            // Arrange

            var expect = "test";

            var data = new TransactionModel
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
