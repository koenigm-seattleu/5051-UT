using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class DataSourceBackendTableUnitTests
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
        public void Backend_DataSourceBackendTable_Default_Instantiate_Should_Pass()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            // Set for ServerTest
            DataSourceBackend.Instance.SetDataSource(DataSourceEnum.ServerTest);

            var backend = DataSourceBackend.Instance;

            //var expect = backend;

            // Act
            var result = backend.AvatarItemBackend;

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate

        #region Load
        [TestMethod]
        public void Backend_DataSourceBackendTable_Load_Invalid_Table_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Load<AvatarItemModel>(null, pk,rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Load_Invalid_pk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Load<AvatarItemModel>(table,null, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Load_Invalid_rk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            //var rk = "rk";

            // Act
            var result = backend.Load<AvatarItemModel>(table, pk, null);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Load_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Load<AvatarItemModel>(table, pk, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion Load

        #region LoadDirect
        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadDirect_Invalid_Table_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.LoadDirect<AvatarItemModel>(null, pk, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadDirect_Invalid_pk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.LoadDirect<AvatarItemModel>(table, null, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadDirect_Invalid_rk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            //var rk = "rk";

            // Act
            var result = backend.LoadDirect<AvatarItemModel>(table, pk, null);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadDirect_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.LoadDirect<AvatarItemModel>(table, pk, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        #endregion LoadDirect

        #region LoadAll
        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadAll_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var table = "AvatarItemModel".ToLower();
            var pk = table;

            // Act
            var result = backend.LoadAll<AvatarItemModel>(table, pk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(0, result.Count, TestContext.TestName);

        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadAll_Invalid_table_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var table = "AvatarItemModel".ToLower();
            var pk = table;

            // Act
            var result = backend.LoadAll<AvatarItemModel>(null, pk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(0, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadAll_Invalid_pk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var table = "AvatarItemModel".ToLower();
            var pk = table;

            // Act
            var result = backend.LoadAll<AvatarItemModel>(table, null);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(0, result.Count, TestContext.TestName);
        }
        #endregion LoadAll

        #region LoadAllDirect
        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadAllDirect_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var table = "AvatarItemModel".ToLower();
            var pk = table;

            // Act
            var result = backend.LoadAllDirect<AvatarItemModel>(table, pk,DataSourceEnum.Unknown,false);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(0, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadAllDirect_Invalid_table_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var table = "AvatarItemModel".ToLower();
            var pk = table;

            // Act
            var result = backend.LoadAllDirect<AvatarItemModel>(null, pk, DataSourceEnum.Unknown, false);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(0, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_LoadAllDirect_Invalid_pk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var table = "AvatarItemModel".ToLower();
            var pk = table;

            // Act
            var result = backend.LoadAllDirect<AvatarItemModel>(table, null, DataSourceEnum.Unknown, false);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(0, result.Count, TestContext.TestName);
        }
        #endregion LoadAllDirect

        #region Update
        [TestMethod]
        public void Backend_DataSourceBackendTable_Update_Invalid_Table_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Update<AvatarItemModel>(null, pk, rk,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Update_Invalid_pk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Update<AvatarItemModel>(table, null, rk,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Update_Invalid_rk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            //var rk = "rk";

            // Act
            var result = backend.Update<AvatarItemModel>(table, pk, null,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Update_Invalid_data_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            data = null;

            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Update<AvatarItemModel>(table, pk, rk, data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Update_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Update<AvatarItemModel>(table, pk, rk,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        #endregion Update

        #region UpdateDirect
        [TestMethod]
        public void Backend_DataSourceBackendTable_UpdateDirect_Invalid_Table_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.UpdateDirect<AvatarItemModel>(null, pk, rk,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_UpdateDirect_Invalid_pk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.UpdateDirect<AvatarItemModel>(table, null, rk,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_UpdateDirect_Invalid_rk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            //var rk = "rk";

            // Act
            var result = backend.UpdateDirect<AvatarItemModel>(table, pk, null,data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_UpdateDirect_Invalid_data_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            data = null;

            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.UpdateDirect<AvatarItemModel>(table, pk, rk, data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_UpdateDirect_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.UpdateDirect<AvatarItemModel>(table, pk, rk, data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion UpdateDirect

        #region SetDataSourceServerMode
        [TestMethod]
        public void Backend_DataSourceBackendTable_SetDataSourceServerMode_Valid_Enum_Unknown_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = DataSourceBackendTable.Instance;
            var expectEnum = DataSourceEnum.Unknown;

            // Act
            backend.SetDataSourceServerMode(expectEnum);
            var result = backend;

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_SetDataSourceServerMode_Valid_Enum_Local_Should_Pass()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = DataSourceBackendTable.Instance;
            var expectEnum = DataSourceEnum.Local;

            // Act
            backend.SetDataSourceServerMode(expectEnum);
            var result = backend;

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_SetDataSourceServerMode_Valid_Enum_ServerTest_Should_Pass()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = DataSourceBackendTable.Instance;
            var expectEnum = DataSourceEnum.ServerTest;

            // Act
            backend.SetDataSourceServerMode(expectEnum);
            var result = backend;

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_SetDataSourceServerMode_Valid_Enum_ServerLive_Should_Pass()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = DataSourceBackendTable.Instance;
            var expectEnum = DataSourceEnum.ServerLive;

            // Act
            backend.SetDataSourceServerMode(expectEnum);
            var result = backend;

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_SetDataSourceServerModeDirect_InValid_String_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);

            var backend = DataSourceBackendTable.Instance;
            //var expectEnum = DataSourceEnum.ServerLive;

            // Act
            var result = backend.SetDataSourceServerModeDirect("bogus");

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }
        #endregion DataSet

        #region Delete
        [TestMethod]
        public void Backend_DataSourceBackendTable_Delete_Invalid_Table_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Delete<AvatarItemModel>(null, pk, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(false, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Delete_Invalid_pk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Delete<AvatarItemModel>(table, null, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(false, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Delete_Invalid_rk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            //var rk = "rk";

            // Act
            var result = backend.Delete<AvatarItemModel>(table, pk, null);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(false, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_Delete_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.Delete<AvatarItemModel>(table, pk, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(true, result, TestContext.TestName);
        }
        #endregion Delete

        #region DeleteDirect
        [TestMethod]
        public void Backend_DataSourceBackendTable_DeleteDirect_Invalid_Table_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.DeleteDirect<AvatarItemModel>(null, pk, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(false, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_DeleteDirect_Invalid_pk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.DeleteDirect<AvatarItemModel>(table, null, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(false, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_DeleteDirect_Invalid_rk_Null_Should_Fail()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            //var rk = "rk";

            // Act
            var result = backend.DeleteDirect<AvatarItemModel>(table, pk, null);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(false, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_DeleteDirect_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.DeleteDirect<AvatarItemModel>(table, pk, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.AreEqual(true, result, TestContext.TestName);
        }
        #endregion DeleteDirect

        #region ConvertToEntity
        [TestMethod]
        public void Backend_DataSourceBackendTable_ConvertToEntity_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new AvatarItemModel();
            var table = "AvatarItemModel".ToLower();
            var pk = table;
            var rk = "rk";

            // Act
            var result = backend.ConvertToEntity<AvatarItemModel>(data, pk, rk);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion ConvertToEntity

        #region ConvertFromEntity
        [TestMethod]
        public void Backend_DataSourceBackendTable_ConvertFromEntity_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new DataSourceBackendTableEntity
            {
                PartitionKey = "pk",
                RowKey = "rk"
            };
            var datablob = new AvatarItemModel();

            var entity = backend.ConvertToEntity<AvatarItemModel>(datablob, data.PartitionKey,data.RowKey);

            // Act
            var result = backend.ConvertFromEntity<AvatarItemModel>(entity);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion ConvertFromEntity

        #region ConvertFromEntityList
        [TestMethod]
        public void Backend_DataSourceBackendTable_ConvertFromEntityList_Valid_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new List<DataSourceBackendTableEntity>();

            // Act
            var result = backend.ConvertFromEntityList<AvatarItemModel>(data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_ConvertFromEntityList_Valid_List_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var temp = new DataSourceBackendTableEntity
            {
                PartitionKey = "pk",
                RowKey = "rk"
            };
            var tempblob = new AvatarItemModel();

            var entity = backend.ConvertToEntity<AvatarItemModel>(tempblob, temp.PartitionKey, temp.RowKey);

            var data = new List<DataSourceBackendTableEntity>
            {
                entity
            };

            // Act
            var result = backend.ConvertFromEntityList<AvatarItemModel>(data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DataSourceBackendTable_ConvertFromEntityList_Invalid_Null_Should_Return()
        {
            // Arrange
            DataSourceBackend.SetTestingMode(true);
            var backend = DataSourceBackendTable.Instance;

            var data = new List<DataSourceBackendTableEntity>();
            data = null;

            // Act
            var result = backend.ConvertFromEntityList<AvatarItemModel>(data);

            // Reset
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(false);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion ConvertFromEntityList
    }
}
