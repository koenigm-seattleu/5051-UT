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
    public class AvatarItemModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_AvatarItemModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new AvatarItemModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_AvatarItemModel_Instantiate_Valid_Should_Pass()
        {
            // Arrange
            var uri = "uri";
            var name = "name";
            var description = "description";
            AvatarItemCategoryEnum category = AvatarItemCategoryEnum.Accessory;
            int tokens = 10;
            int quantities = 10;
            bool isLimitSupply = true;
            var expect = uri;

            // Act
            var result = new AvatarItemModel(uri, name, description,category,tokens,quantities, isLimitSupply);

            // Assert
            Assert.AreEqual(expect,result.Uri, TestContext.TestName);
        }

        [TestMethod]
        public void Models_AvatarItemModel_Instantiate_Valid_Get_Catagory_Should_Pass()
        {
            // Arrange
            var result = new AvatarItemModel();

            // Act
            var expect = result.Category;

            // Assert
            Assert.AreEqual(expect, result.Category, TestContext.TestName);
        }

        [TestMethod]
        public void Models_AvatarItemModel_Instantiate_With_Valid_Data_Should_Pass()
        {
            // Arrange
            var expectUri = "uri";
            var expectDescription = "Description";
            var expectTokens = 1000;
            var expectQuantities = 2000;
            var expectCategory = AvatarItemCategoryEnum.Accessory;

            var test = new AvatarItemModel
            {
                Uri = expectUri,
                Description = expectDescription,
                Tokens = expectTokens,
                Quantities = expectQuantities,
                Category = expectCategory
            };

            // Act
            var data = new AvatarItemModel(test);

            var result = data;

            // Assert
            Assert.AreEqual(expectUri, result.Uri, "Uri " + TestContext.TestName);
            Assert.AreEqual(expectDescription, result.Description, "Description " + TestContext.TestName);
            Assert.AreEqual(expectTokens, result.Tokens, "Tokens " + TestContext.TestName);
            Assert.AreEqual(expectQuantities, result.Quantities, "Quantities " + TestContext.TestName);
            Assert.AreEqual(expectCategory, result.Category, "Category " + TestContext.TestName);
        }

        #endregion Instantiate

        #region Update
        [TestMethod]
        public void Models_AvatarItemModel_Update_With_Valid_Data_Should_Pass()
        {
            // Arrange
            var expectUri = "uri";
            var expectDescription = "Description";
            var expectTokens = 1000;
            var expectQuantities = 2000;
            var expectCategory = AvatarItemCategoryEnum.Accessory;

            var data = new AvatarItemModel();

            var test = new AvatarItemModel
            {
                Uri = expectUri,
                Description = expectDescription,
                Tokens = expectTokens,
                Quantities = expectQuantities,
                Category = expectCategory
            };

            // Act
            data.Update(test);
            var result = data;

            // Assert
            Assert.AreEqual(expectUri, result.Uri, "Uri "+TestContext.TestName);
            Assert.AreEqual(expectDescription, result.Description, "Description " + TestContext.TestName);
            Assert.AreEqual(expectTokens, result.Tokens, "Tokens " + TestContext.TestName);
            Assert.AreEqual(expectQuantities, result.Quantities, "Quantities " + TestContext.TestName);
            Assert.AreEqual(expectCategory, result.Category, "Category " + TestContext.TestName);
        }

        [TestMethod]
        public void Models_AvatarItemModel_Update_With_Invalid_Data_Null_Should_Fail()
        {
            // Arrange

            var expect = "test";

            var data = new AvatarItemModel
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
