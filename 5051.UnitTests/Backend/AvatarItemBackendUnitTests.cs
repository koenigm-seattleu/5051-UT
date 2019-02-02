using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class AvatarItemBackendUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }


        #region delete
        [TestMethod]
        public void Backend_AvatarItemBackend_Delete_Valid_Data_Should_Pass()
        {
            //arrange
            var test = AvatarItemBackend.Instance;
            var data = new AvatarItemModel();
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
        public void Backend_AvatarItemBackend__AvatarItemBackend_Delete_With_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = AvatarItemBackend.Instance;
            var expect = false;

            //act
            var result = test.Delete(null);

            //reset
            test.Reset();

            //assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region GetAvatarItemListItem
        [TestMethod]
        public void Backend_AvatarItemBackend_GetAvatarItemListItem_ID_Null_Should_Pass()
        {
            //arrange
            var test = AvatarItemBackend.Instance;

            //act
            var result = test.GetAvatarItemListItem(null);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion GetAvatarItemListItem

        #region GetAvatarItemUri
        [TestMethod]
        public void Backend_AvatarItemBackend_GetAvatarItemUri_Valid_Data_Should_Pass()
        {
            //arrange
            var test = AvatarItemBackend.Instance;
            var testID = test.GetFirstAvatarItemId();

            //act
            var result = test.GetAvatarItemUri(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_GetAvatarItemUri_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = AvatarItemBackend.Instance;

            //act
            var result = test.GetAvatarItemUri(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_GetAvatarItemUri_Invalid_ID_Should_Fail()
        {
            //arrange
            var data = new AvatarItemModel();
            var test = AvatarItemBackend.Instance;
            data.Id = "bogus";

            //act
            var result = test.GetAvatarItemUri(data.Id);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion GetAvatarItemUri

        #region update
        [TestMethod]
        public void Backend_AvatarItemBackend_Update_Valid_Data_Should_Pass()
        {
            //arrange
            var test = AvatarItemBackend.Instance;

            var data = new AvatarItemModel();
            var createResult = test.Create(data);

            data.Name = "GoodTestName";
            data.Description = "Good Test Description";
            data.Uri = "GoodTestUri";
            data.Tokens = 100;
            data.Category = AvatarItemCategoryEnum.Accessory;

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
        public void Backend_AvatarItemBackend__AvatarItemBackend_Update_With_Invalid_Data_Null_Should_Fail()
        {
            //arrange
            var test = AvatarItemBackend.Instance;

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
        public void Backend_AvatarItemBackend_Index_Valid_Should_Pass()
        {
            //arrange
            var test = AvatarItemBackend.Instance;

            //act
            var result = test.Index();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion index

        #region read
        [TestMethod]
        public void Backend_AvatarItemBackend_Read_Invalid_ID_Null_Should_Fail()
        {
            //arrange
            var test = AvatarItemBackend.Instance;

            //act
            var result = test.Read(null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_Read_Valid_ID_Should_Pass()
        {
            //arrange
            var test = AvatarItemBackend.Instance;
            var testID = test.GetFirstAvatarItemId();

            //act
            var result = test.Read(testID);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion read

        #region create
        [TestMethod]
        public void Backend_AvatarItemBackend_Create_Valid_Data_Should_Pass()
        {
            //arrange
            var test = AvatarItemBackend.Instance;
            var data = new AvatarItemModel();

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
        public void Backend_AvatarItemBackend_SetDataSourceDataSet_Uses_MockData_Should_Pass()
        {
            //arrange            
            var test = AvatarItemBackend.Instance;
            var testDataSourceBackend = DataSourceBackend.Instance;
            var mockEnum = DataSourceDataSetEnum.Demo;

            //act
            testDataSourceBackend.SetDataSourceDataSet(mockEnum);

            //reset
            test.Reset();

            //assert
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_SetDataSourceDatest_Uses_SQLData_Should_Pass()
        {
            //arange
            var test = AvatarItemBackend.Instance;
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
        public void Backend_AvatarItemBackend_GetDefault_Valid_Should_Pass()
        {
            //arrange
            var test = AvatarItemBackend.Instance;

            //act
            var result = test.GetDefault(AvatarItemCategoryEnum.Accessory);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_GetDefault_IsDefault_Valid_Should_Pass()
        {
            //arrange
            var test = AvatarItemBackend.Instance;

            //act
            var result = test.GetDefault(AvatarItemCategoryEnum.Accessory);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_GetDefault_IsDefault_False_Valid_Should_Pass()
        {
            //arrange
            var test = AvatarItemBackend.Instance;

            // Remove Default Accessory
            var data = test.GetDefault(AvatarItemCategoryEnum.Accessory);
            AvatarItemBackend.Instance.Delete(data.Id);

            // Add it back, but don't set the default
            AvatarItemBackend.Instance.Create(new AvatarItemModel("Accessory0.png", "None", "None", AvatarItemCategoryEnum.Accessory, 1, 10, false, false));  

            //act
            var result = test.GetDefault(AvatarItemCategoryEnum.Accessory);

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion GetDefault

        #region SetDataSource
        [TestMethod]
        public void Backend_AvatarItemBackend_SetDataSource_Valid_Enum_SQL_Should_Pass()
        {
            //arrange
            var sqlEnum = _5051.Models.DataSourceEnum.SQL;
            var backend = AvatarItemBackend.Instance;

            //act
            AvatarItemBackend.SetDataSource(sqlEnum);
            var result = backend;

            //reset
            backend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_SetDataSourceDataSet_Valid_Enum_UnitTests_Should_Pass()
        {
            //arrange
            var unitEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;
            var backend = AvatarItemBackend.Instance;

            //act
            AvatarItemBackend.SetDataSourceDataSet(unitEnum);
            var result = backend;

            //reset
            backend.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region GetDefaultAvatarItemFullItem
        [TestMethod]
        public void Backend_AvatarItemBackend_GetDefaultAvatarItemFullItem_Valid_Category_Should_Pass()
        {
            //arrange

            //act
            var result = DataSourceBackend.Instance.AvatarItemBackend.GetDefaultAvatarItemFullItem(AvatarItemCategoryEnum.Accessory);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_GetDefaultAvatarItemFullItem_Null_Category_Should_Fail()
        {
            //arrange

            //act
            var result = DataSourceBackend.Instance.AvatarItemBackend.GetDefaultAvatarItemFullItem(AvatarItemCategoryEnum.Unknown);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion GetDefaultAvatarItemFullItem

        #region GetAvatarShopViewModel
        [TestMethod]
        public void Backend_AvatarItemBackend_GetAvatarShopViewModel_Valid_Student_Should_Pass()
        {
            //arrange
            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var item = DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);

            //act
            var result = DataSourceBackend.Instance.AvatarItemBackend.GetAvatarShopViewModel(student,item);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion GetAvatarShopViewModel

        #region GetAvatarItemShopViewModel
        [TestMethod]
        public void Backend_AvatarItemBackend_GetAvatarItemShopViewModel_Valid_Should_Pass()
        {
            //arrange
            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var item = DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);

            //act
            var result = DataSourceBackend.Instance.AvatarItemBackend.GetAvatarItemShopViewModel(student.Id,item.Id );

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_GetAvatarItemShopViewModel_StudentId_Null_Should_Fail()
        {
            //arrange
            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var item = DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);

            //act
            var result = DataSourceBackend.Instance.AvatarItemBackend.GetAvatarItemShopViewModel((string)null, item.Id);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_GetAvatarItemShopViewModel_StudentId_Bogus_Should_Fail()
        {
            //arrange
            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var item = DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);

            //act
            var result = DataSourceBackend.Instance.AvatarItemBackend.GetAvatarItemShopViewModel("bogus", item.Id);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_GetAvatarItemShopViewModel_ItemId_Null_Should_Fail()
        {
            //arrange
            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var item = DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);

            //act
            var result = DataSourceBackend.Instance.AvatarItemBackend.GetAvatarItemShopViewModel(student.Id, (string)null);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_GetAvatarItemShopViewModel_ItemId_Bogus_Should_Fail()
        {
            //arrange
            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var item = DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);

            //act
            var result = DataSourceBackend.Instance.AvatarItemBackend.GetAvatarItemShopViewModel(student.Id,"bogus");

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion GetAvatarItemShopViewModel

        #region GetAllAvatarItem
        [TestMethod]
        public void Backend_AvatarItemBackend_GetAllAvatarItem_Valid_Category_One_Should_Pass()
        {
            //arrange

            //act
            var result = DataSourceBackend.Instance.AvatarItemBackend.GetAllAvatarItem(AvatarItemCategoryEnum.Accessory);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_GetAllAvatarItem_Valid_Category_Multiple_Should_Pass()
        {
            //arrange

            //act
            var result = DataSourceBackend.Instance.AvatarItemBackend.GetAllAvatarItem(AvatarItemCategoryEnum.Head);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_AvatarItemBackend_GetAllAvatarItem_Null_Category_Should_Fail()
        {
            //arrange

            //act
            var result = DataSourceBackend.Instance.AvatarItemBackend.GetAllAvatarItem(AvatarItemCategoryEnum.Unknown);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion GetAllAvatarItem
    }
}
