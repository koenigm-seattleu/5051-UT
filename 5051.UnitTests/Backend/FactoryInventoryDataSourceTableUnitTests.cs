using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class FactoryInventoryDataSourceTableUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize()]
        public void Initialize()
        {
            DataSourceBackend.SetTestingMode(true);
            DataSourceBackend.Instance.SetDataSource(DataSourceEnum.Local);
        }

        [TestCleanup]
        public void Cleanup()
        {
            DataSourceBackend.SetTestingMode(false);
            DataSourceBackend.Instance.SetDataSource(DataSourceEnum.Mock);
        }

        #region Instantiate
        [TestMethod]
        public void Backend_FactoryInventoryDataSourceTable_Default_Instantiate_Should_Pass()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            // Set for ServerTest
            DataSourceBackend.Instance.SetDataSource(DataSourceEnum.ServerTest);

            var backend = DataSourceBackend.Instance;

            //var expect = backend;

            // Act
            var result = backend.FactoryInventoryBackend;

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate

        #region read
        [TestMethod]
        public void Backend_FactoryInventoryDataSourceTable_Read_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = FactoryInventoryDataSourceTable.Instance;

            // Act
            var result = backend.Read(null);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryDataSourceTable_Read_Valid_ID_Should_Pass()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = FactoryInventoryDataSourceTable.Instance;
            var shopBackend = FactoryInventoryBackend.Instance;
            var expectShopItem = shopBackend.GetFirstFactoryInventoryId();

            // Act
            var result = backend.Read(expectShopItem);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region update
        [TestMethod]
        public void Backend_FactoryInventoryDataSourceTable_Update_Invalid_Data_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = FactoryInventoryDataSourceTable.Instance;

            // Act
            var result = backend.Update(null);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryDataSourceTable_Update_Invalid_Data_Shop_Item_Does_Not_Exist_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = FactoryInventoryDataSourceTable.Instance;
            var expectShopModel = new FactoryInventoryModel();

            // Act
            var result = backend.Update(expectShopModel);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryDataSourceTable_Update_Valid_Data_Should_Pass()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = FactoryInventoryDataSourceTable.Instance;
            var shopBackend = FactoryInventoryBackend.Instance;
            var expectShopItemId = shopBackend.GetFirstFactoryInventoryId();
            var expectShopModel = backend.Read(expectShopItemId);
            var expectId = "GoodID";
            var expectUri = "GoodUri";
            var expectName = "Shop Item";
            var expectDescription = "A good description of the item";
            var expectTokens = 1000;

            // Act
            expectShopModel.Id = expectId;
            expectShopModel.Uri = expectUri;
            expectShopModel.Name = expectName;
            expectShopModel.Description = expectDescription;
            expectShopModel.Tokens = expectTokens;

            var resultUpdate = backend.Update(expectShopModel);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(expectShopModel, resultUpdate, TestContext.TestName);
            Assert.AreEqual(expectId, resultUpdate.Id, TestContext.TestName);
            Assert.AreEqual(expectUri, resultUpdate.Uri, TestContext.TestName);
            Assert.AreEqual(expectName, resultUpdate.Name, TestContext.TestName);
            Assert.AreEqual(expectDescription, resultUpdate.Description, TestContext.TestName);
            Assert.AreEqual(expectTokens, resultUpdate.Tokens, TestContext.TestName);
        }
        #endregion update

        #region delete
        [TestMethod]
        public void Backend_FactoryInventoryDataSourceTable_Delete_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = FactoryInventoryDataSourceTable.Instance;
            var expect = false;

            // Act
            var result = backend.Delete(null);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryDataSourceTable_Delete_Invalid_ID_Bogus_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = FactoryInventoryDataSourceTable.Instance;
            var expect = false;

            // Act
            var result = backend.Delete("bogus");

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryDataSourceTable_Delete_Valid_ID_Should_Pass()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = FactoryInventoryDataSourceTable.Instance;

            DataSourceBackend.Instance.SetDataSource(DataSourceEnum.Local);
            var shopBackend = DataSourceBackend.Instance.FactoryInventoryBackend;

            var expectShopItem = shopBackend.GetFirstFactoryInventoryId();
            var expect = true;

            // Act
            var result = backend.Delete(expectShopItem);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region DataSet
        [TestMethod]
        public void Backend_FactoryInventoryDataSourceTable_LoadDataSet_Valid_Enum_Demo_Should_Pass()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = FactoryInventoryDataSourceTable.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.Demo;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryDataSourceTable_LoadDataSet_Valid_Enum_UnitTest_Should_Pass()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = FactoryInventoryDataSourceTable.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryDataSourceMock_LoadDataSet_Twice_Valid_Enum_UnitTest_Should_Pass()
        {
            // Arrange
            var backend = FactoryInventoryDataSourceTable.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            // Act
            backend.LoadDataSet(expectEnum);

            // Load again, makes sure there is multiple data
            backend.CreateDataSetDefaultData();

            var result = backend;

            //reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion DataSet
    }
}