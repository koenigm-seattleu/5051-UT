using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class FactoryInventoryBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region delete
        [TestMethod]
        public void Backend_FactoryInventoryBackend_Delete_Valid_Data_Should_Pass()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;
            var data = new FactoryInventoryModel();
            var createResult = test.Create(data);
            var expect = true;

            //act
            var deleteResult = test.Delete(data.Id);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, deleteResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend__FactoryInventoryBackend_Delete_With_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;
            var expect = false;

            //act
            var result = test.Delete(null);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region GetFactoryInventoryListItem
        [TestMethod]
        public void Backend_FactoryInventoryBackend_GetFactoryInventoryListItem_ID_Null_Should_Pass()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;

            //act
            var result = test.GetFactoryInventoryListItem(null);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion GetFactoryInventoryListItem

        #region GetFactoryInventoryUri
        [TestMethod]
        public void Backend_FactoryInventoryBackend_GetFactoryInventoryUri_Valid_Data_Should_Pass()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;
            var testID = test.GetFirstFactoryInventoryId();

            //act
            var result = test.GetFactoryInventoryUri(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend_GetFactoryInventoryUri_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;

            //act
            var result = test.GetFactoryInventoryUri(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend_GetFactoryInventoryUri_Invalid_ID_Should_Fail()
        {
            //arrange
            var data = new FactoryInventoryModel();
            var test = FactoryInventoryBackend.Instance;
            data.Id = "bogus";

            //act
            var result = test.GetFactoryInventoryUri(data.Id);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion GetFactoryInventoryUri

        #region update
        [TestMethod]
        public void Backend_FactoryInventoryBackend_Update_Valid_Data_Should_Pass()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;

            var data = new FactoryInventoryModel();
            var createResult = test.Create(data);

            data.Name = "GoodTestName";
            data.Description = "Good Test Description";
            data.Uri = "GoodTestUri";
            data.Tokens = 100;
            data.Category = FactoryInventoryCategoryEnum.Food;

            var expect = data;

            //act
            var updateResult = test.Update(data);

            var result = test.Read(data.Id);

            //reset
            test.Reset();

            //assert
            Assert.IsNotNull(result, "Updated "+TestContext.TestName);
            Assert.AreEqual(expect.Name, result.Name, "Name "+TestContext.TestName);
            Assert.AreEqual(expect.Description, result.Description, "Description "+TestContext.TestName);
            Assert.AreEqual(expect.Uri, result.Uri, "URI "+TestContext.TestName);
            Assert.AreEqual(expect.Tokens, result.Tokens, "Tokens "+TestContext.TestName);
            Assert.AreEqual(expect.Category, result.Category, "Category " + TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend__FactoryInventoryBackend_Update_With_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;

            //act
            var result = test.Update(null);

            //reset
            test.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion update

        #region index
        [TestMethod]
        public void Backend_FactoryInventoryBackend_Index_Valid_Should_Pass()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;

            //act
            var result = test.Index();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion index

        #region read
        [TestMethod]
        public void Backend_FactoryInventoryBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;

            //act
            var result = test.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend_Read_Valid_ID_Should_Pass()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;
            var testID = test.GetFirstFactoryInventoryId();

            //act
            var result = test.Read(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region create
        [TestMethod]
        public void Backend_FactoryInventoryBackend_Create_Valid_Data_Should_Pass()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;
            var data = new FactoryInventoryModel();

            //act
            var result = test.Create(data);

            //reset
            test.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
            Assert.AreEqual(data.Id, result.Id, TestContext.TestName);
        }
        #endregion create

        #region SetDataSourceDataSet
        [TestMethod]
        public void Backend_FactoryInventoryBackend_SetDataSourceDataSet_Uses_MockData_Should_Pass()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;
            var testDataSourceBackend = DataSourceBackend.Instance;
            var mockEnum = DataSourceDataSetEnum.Demo;

            //act
            testDataSourceBackend.SetDataSourceDataSet(mockEnum);

            //reset
            test.Reset();

            //assert
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend_SetDataSourceDatest_Uses_SQLData_Should_Pass()
        {
            //arange
            var test = FactoryInventoryBackend.Instance;
            var testDataSourceBackend = DataSourceBackend.Instance;
            var SQLEnum = DataSourceEnum.SQL;

            //act
            testDataSourceBackend.SetDataSource(SQLEnum);

            //reset
            test.Reset();

            //asset
        }
        #endregion SetDataSourceDataSet

        #region GetDefault
        [TestMethod]
        public void Backend_FactoryInventoryBackend_GetDefault_Valid_Should_Pass()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;

            //act
            var result = test.GetDefault(FactoryInventoryCategoryEnum.Truck);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_GetDefault_IsDefault_False_Valid_Should_Pass()
        {
            //arrange
            var test = FactoryInventoryBackend.Instance;

            // Remove Default Accessory
            var data = test.GetDefault(FactoryInventoryCategoryEnum.Truck);
            FactoryInventoryBackend.Instance.Delete(data.Id);

            // Add it back, but don't set the default
            FactoryInventoryBackend.Instance.Create(new FactoryInventoryModel("Truck1.png", "Slick", "Truck", FactoryInventoryCategoryEnum.Truck, 10, 10, false, false)); // Default


            //act
            var result = test.GetDefault(FactoryInventoryCategoryEnum.Truck);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion GetDefault

        #region GetShopTruckViewModel
        [TestMethod]
        public void Backend_FactoryInventoryBackend__GetShopTruckItemViewModel_Invalid_StudentID_Null_Should_Fail()
        {
            // Arrange
            var test = FactoryInventoryBackend.Instance;

            // Act
            var result = test.GetShopTruckItemViewModel(null, "bogus");

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend__GetShopTruckItemViewModel_Invalid_ItemId_Null_Should_Fail()
        {
            // Arrange
            var test = FactoryInventoryBackend.Instance;

            // Act
            var result = test.GetShopTruckItemViewModel("bogus", null);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend__GetShopTruckItemViewModel_Invalid_StudentID_Bogus_ItemId_Bogus_Should_Fail()
        {
            // Arrange
            var test = FactoryInventoryBackend.Instance;

            // Act
            var result = test.GetShopTruckItemViewModel("bogus", "Bogus");

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend__GetShopTruckItemViewModel_Invalid_StudentID_Valid_ItemId__Should_Fail()
        {
            // Arrange
            var test = FactoryInventoryBackend.Instance;
            var item = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Truck);

            // Act
            var result = test.GetShopTruckItemViewModel("bogus", item.Id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend__GetShopTruckItemViewModel_Valid_StudentID_Bogus_ItemId_Bogus_Should_Fail()
        {
            // Arrange
            var test = FactoryInventoryBackend.Instance;
            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            // Act
            var result = test.GetShopTruckItemViewModel(student.Id, "Bogus");

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend__GetShopTruckItemViewModel_Valid_StudentID_Valid_ItemId_Should_Fail()
        {
            // Arrange
            var test = FactoryInventoryBackend.Instance;

            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var item = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Truck);

            // Act
            var result = test.GetShopTruckItemViewModel(student.Id, item.Id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend__GetShopTruckItemViewModel_Invalid_Category_Empty_Should_Fail()
        {
            // Arrange
            var test = FactoryInventoryBackend.Instance;

            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var item = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Truck);

            // Make a copy of the Item, and add it to Student Inventory and save student
            var studentItem = new FactoryInventoryModel(item);
            student.Inventory.Add(studentItem);
            DataSourceBackend.Instance.StudentBackend.Update(student);

            // Change inventory category to not match, and Save it
            item.Category = FactoryInventoryCategoryEnum.Unknown;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(item);

            // Act
            var result = test.GetShopTruckItemViewModel(student.Id, item.Id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_FactoryInventoryBackend__GetShopTruckItemViewModel_Valid_Should_Fail()
        {
            // Arrange
            var test = FactoryInventoryBackend.Instance;
            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var item = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Truck);

            // Add item to Student Inventory and save student
            student.Inventory.Add(item);
            DataSourceBackend.Instance.StudentBackend.Update(student);

            // Act
            var result = test.GetShopTruckItemViewModel(student.Id, item.Id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion GetShopTruckViewModel

        #region GetShopTruckItemViewModel_StudentModel
        [TestMethod]
        public void Backend_FactoryInventoryBackend_GetShopTruckViewModel_StudentModel_Valid_Model_Should_Pass()
        {
            // Arrange
            var test = FactoryInventoryBackend.Instance;
            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var item = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Truck);

            // Add item to Student Inventory and save student
            student.Inventory.Add(item);
            DataSourceBackend.Instance.StudentBackend.Update(student);

            // Act
            var result = test.GetShopTruckViewModel(student);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion GetShopTruckItemViewModel_StudentModel

        #region GetDefaultTruckFullItem
        [TestMethod]
        public void Backend_AvatarItemBackend_GetDefaultTruckFullItem_Valid_Category_Should_Pass()
        {
            //arrange

            //act

            var result = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Truck);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_GetDefaultTruckFullItem_Null_Category_Should_Fail()
        {
            //arrange

            //act
            var result = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Unknown);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNull(result.Name, TestContext.TestName);
        }
        #endregion GetDefaultTruckFullItem
    }
}
