﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class AvatarItemDataSourceMockUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Backend_AvatarItemDataSourceMock_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = AvatarItemDataSourceMock.Instance;

            //var expect = backend;

            // Act
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion Instantiate

        #region read
        [TestMethod]
        public void Backend_AvatarItemDataSourceMock_Read_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = AvatarItemDataSourceMock.Instance;

            // Act
            var result = backend.Read(null);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemDataSourceMock_Read_Valid_ID_Should_Pass()
        {
            // Arrange
            var backend = AvatarItemDataSourceMock.Instance;
            var shopBackend = AvatarItemBackend.Instance;
            var expectShopItem = shopBackend.GetFirstAvatarItemId();

            // Act
            var result = backend.Read(expectShopItem);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region update
        [TestMethod]
        public void Backend_AvatarItemDataSourceMock_Update_Invalid_Data_Null_Should_Fail()
        {
            // Arrange
            var backend = AvatarItemDataSourceMock.Instance;

            // Act
            var result = backend.Update(null);

            //reset
            backend.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemDataSourceMock_Update_Invalid_Data_Shop_Item_Does_Not_Exist_Should_Fail()
        {
            // Arrange
            var backend = AvatarItemDataSourceMock.Instance;
            var expectShopModel = new AvatarItemModel();

            // Act
            var result = backend.Update(expectShopModel);

            //reset
            backend.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemDataSourceMock_Update_Valid_Data_Should_Pass()
        {
            // Arrange
            var backend = AvatarItemDataSourceMock.Instance;
            var shopBackend = AvatarItemBackend.Instance;
            var expectShopItemId = shopBackend.GetFirstAvatarItemId();
            var expectShopModel = backend.Read(expectShopItemId);
            var expectId = "GoodID";
            var expectUri = "GoodUri";
            var expectName = "Shop Item";
            var expectDescription = "A good description of the item";
            var expectCatagory = AvatarItemCategoryEnum.Accessory;
            var expectTokens = 1000;

            // Act
            expectShopModel.Id = expectId;
            expectShopModel.Uri = expectUri;
            expectShopModel.Name = expectName;
            expectShopModel.Description = expectDescription;
            expectShopModel.Category = expectCatagory;
            expectShopModel.Tokens = expectTokens;

            var resultUpdate = backend.Update(expectShopModel);

            //reset
            backend.Reset();
            shopBackend.Reset();

            // Assert
            Assert.AreEqual(expectShopModel, resultUpdate, TestContext.TestName);
            Assert.AreEqual(expectId, resultUpdate.Id, TestContext.TestName);
            Assert.AreEqual(expectUri, resultUpdate.Uri, TestContext.TestName);
            Assert.AreEqual(expectName, resultUpdate.Name, TestContext.TestName);
            Assert.AreEqual(expectDescription, resultUpdate.Description, TestContext.TestName);
            Assert.AreEqual(expectCatagory, resultUpdate.Category, TestContext.TestName);
            Assert.AreEqual(expectTokens, resultUpdate.Tokens, TestContext.TestName);
        }
        #endregion update

        #region delete
        [TestMethod]
        public void Backend_AvatarItemDataSourceMock_Delete_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = AvatarItemDataSourceMock.Instance;
            var expect = false;

            // Act
            var result = backend.Delete(null);

            //reset
            backend.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemDataSourceMock_Delete_Valid_ID_Should_Pass()
        {
            // Arrange
            var backend = AvatarItemDataSourceMock.Instance;
            var shopBackend = AvatarItemBackend.Instance;
            var expectShopItem = shopBackend.GetFirstAvatarItemId();
            var expect = true;

            // Act
            var result = backend.Delete(expectShopItem);

            //reset
            backend.Reset();
            shopBackend.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region DataSet
        [TestMethod]
        public void Backend_AvatarItemDataSourceMock_LoadDataSet_Valid_Enum_Demo_Should_Pass()
        {
            // Arrange
            var backend = AvatarItemDataSourceMock.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.Demo;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemDataSourceMock_LoadDataSet_Valid_Enum_UnitTest_Should_Pass()
        {
            // Arrange
            var backend = AvatarItemDataSourceMock.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion DataSet
    }
}
