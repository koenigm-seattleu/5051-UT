using System;
using System.Collections.Generic;
using System.Linq;
using _5051.Models;

using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace _5051.Backend
{
    /// <summary>
    /// Backend Table DataSource for AvatarItems, to manage them
    /// </summary>
    public class DataSourceBackendTable
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile DataSourceBackendTable instance;
        private static readonly object syncRoot = new Object();

        private DataSourceBackendTable() { }

        public static DataSourceBackendTable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DataSourceBackendTable();
                        }
                    }
                }

                return instance;
            }
        }

        public CloudStorageAccount storageAccount;
        public CloudTableClient tableClient;
        public int takeCount = 1000;
        //public DataSourceEnum DataSourceServerMode = DataSourceEnum.Local;

        public bool SetDataSourceServerMode(DataSourceEnum dataSourceServerMode)
        {
            var connectionString = string.Empty;
            var StorageConnectionString = GetDataSourceConnectionString(dataSourceServerMode);

            // If under Test, return True;
            if (DataSourceBackend.GetTestingMode())
            {
                return true;
            }

            return SetDataSourceServerModeDirect(StorageConnectionString);
        }

        public string GetDataSourceConnectionString(DataSourceEnum dataSourceServerMode)
        {
            var StorageConnectionString = string.Empty;
            switch (dataSourceServerMode)
            {
                case DataSourceEnum.Local:
                    StorageConnectionString = "StorageConnectionStringLocal";
                    break;

                case DataSourceEnum.ServerLive:
                    StorageConnectionString = "StorageConnectionStringServerLive";
                    break;

                case DataSourceEnum.ServerTest:
                default:
                    StorageConnectionString = "StorageConnectionStringServerTest";
                    break;
            }
            return StorageConnectionString;
        }

        public bool SetDataSourceServerModeDirect(string StorageConnectionString)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                        CloudConfigurationManager.GetSetting(StorageConnectionString));

                tableClient = storageAccount.CreateCloudTableClient();

                // Test if the connection is working by trying to access a table
                var Table = tableClient.GetTableReference("initaltable");
                Table.CreateIfNotExists();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Load All Rows that match the PK
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public List<T> LoadAll<T>(string tableName, string pk, bool convert = true, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            var myReturnList = new List<T>();

            if (string.IsNullOrEmpty(tableName))
            {
                return myReturnList;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return myReturnList;
            }

            // If under Test, return True;
            if (DataSourceBackend.GetTestingMode())
            {
                return myReturnList;
            }

            return LoadAllDirect<T>(tableName, pk, dataSourceEnum, convert);
        }

        public List<T> LoadAllDirect<T>(string tableName,
                                        string pk,
                                        DataSourceEnum dataSourceEnum,
                                        bool convert = true)
        {
            var myReturnList = new List<T>();

            if (string.IsNullOrEmpty(tableName))
            {
                return myReturnList;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return myReturnList;
            }

            try
            {
                var Table = GetTable(dataSourceEnum, tableName);

                var result = new List<DataSourceBackendTableEntity>();

                var query =
                    new TableQuery<DataSourceBackendTableEntity>().Where(
                        TableQuery.GenerateFilterCondition(
                            "PartitionKey", QueryComparisons.Equal, pk));

                query.TakeCount = takeCount;

                TableContinuationToken tableContinuationToken = null;
                do
                {
                    var queryResponse = Table.ExecuteQuerySegmented(query, tableContinuationToken);
                    tableContinuationToken = queryResponse.ContinuationToken;
                    result.AddRange(queryResponse.Results);
                } while (tableContinuationToken != null);

                foreach (var item in result)
                {
                    if (convert)
                    {
                        myReturnList.Add(ConvertFromEntity<T>(item));
                    }
                    else
                    {
                        myReturnList.Add((T)((Object)item));
                    }
                }
            }
            catch (Exception) { }

            return myReturnList;
        }

        /// <summary>
        /// Call Update, because Update is Insert or Replace
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Create<T>(string tableName, string pk, string rk, T data, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            return Update<T>(tableName, pk, rk, data, dataSourceEnum);
        }

        /// <summary>
        /// Load the record
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pk"></param>
        /// <param name="rk"></param>
        /// <returns></returns>
        public T Load<T>(string tableName, string pk, string rk, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            var myReturn = default(T);

            if (string.IsNullOrEmpty(tableName))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(rk))
            {
                return myReturn;
            }

            // If under Test, return True;
            if (DataSourceBackend.GetTestingMode())
            {
                return myReturn;
            }

            return LoadDirect<T>(tableName, pk, rk, dataSourceEnum);
        }

        public T LoadDirect<T>(string tableName, string pk, string rk, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            var myReturn = default(T);

            if (string.IsNullOrEmpty(tableName))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(rk))
            {
                return myReturn;
            }

            try
            {
                var Table = GetTable(dataSourceEnum, tableName);

                // Retrieve
                var retrieveOperation = TableOperation.Retrieve<DataSourceBackendTableEntity>(pk, rk);
                var query = Table.Execute(retrieveOperation);
                if (query.Result == null)
                {
                    return myReturn;
                }

                var data = (DataSourceBackendTableEntity)query.Result;

                myReturn = ConvertFromEntity<T>(data);
                return myReturn;
            }
            catch (Exception)
            {
            }

            return myReturn;
        }

        /// <summary>
        /// Insert or Replace the Record
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Update<T>(string tableName, string pk, string rk, T data, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            var myReturn = default(T);

            if (string.IsNullOrEmpty(tableName))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(rk))
            {
                return myReturn;
            }

            if (data == null)
            {
                return myReturn;
            }

            // If under Test, return True;
            if (DataSourceBackend.GetTestingMode())
            {
                return myReturn;
            }

            return UpdateDirect<T>(tableName, pk, rk, data, dataSourceEnum);
        }

        public T UpdateDirect<T>(string tableName, string pk, string rk, T data, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            var myReturn = default(T);

            if (string.IsNullOrEmpty(tableName))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return myReturn;
            }

            if (string.IsNullOrEmpty(rk))
            {
                return myReturn;
            }

            if (data == null)
            {
                return myReturn;
            }

            try
            {
                var Table = GetTable(dataSourceEnum, tableName);

                // Add to Storage
                var entity = DataSourceBackendTable.Instance.ConvertToEntity<T>(data, pk, rk);

                var updateOperation = TableOperation.InsertOrReplace(entity);
                var query = Table.Execute(updateOperation);
                if (query.Result == null)
                {
                    return myReturn;
                }

                myReturn = DataSourceBackendTable.Instance.ConvertFromEntity<T>(entity);
            }
            catch (Exception) { }

            return myReturn;
        }

        /// <summary>
        /// Delete the record
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Delete<T>(string tableName, string pk, string rk, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return false;
            }

            if (string.IsNullOrEmpty(rk))
            {
                return false;
            }

            // If under Test, return True;
            if (DataSourceBackend.GetTestingMode())
            {
                return true;
            }

            return DeleteDirect<T>(tableName, pk, rk, dataSourceEnum);
        }

        public bool DeleteDirect<T>(string tableName, string pk, string rk, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(pk))
            {
                return false;
            }

            if (string.IsNullOrEmpty(rk))
            {
                return false;
            }

            try
            {
                var Table = GetTable(dataSourceEnum, tableName);

                var entity = new DataSourceBackendTableEntity
                {
                    ETag = "*",
                    PartitionKey = pk,
                    RowKey = rk
                };

                var Operation = TableOperation.Delete(entity);
                var query = Table.Execute(Operation);
                if (query.Result == null)
                {
                    return false;
                }
            }
            catch (Exception) { }

            return true;
        }

        /// <summary>
        /// Convert the data item to an entity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataSourceBackendTableEntity ConvertToEntity<T>(T data, string pk, string rk)
        {
            var entity = new DataSourceBackendTableEntity
            {
                PartitionKey = pk,
                RowKey = rk,
                Blob = JsonConvert.SerializeObject(data)
            };

            return entity;
        }

        /// <summary>
        /// Convert an entity to a data item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public T ConvertFromEntity<T>(DataSourceBackendTableEntity data)
        {
            var myReturn = JsonConvert.DeserializeObject<T>(data.Blob);
            return myReturn;
        }

        /// <summary>
        /// Convert all the entities to data items
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public List<T> ConvertFromEntityList<T>(List<DataSourceBackendTableEntity> dataList)
        {
            var myReturn = new List<T>();

            if (dataList == null)
            {
                return myReturn;
            }

            foreach (var item in dataList)
            {
                var temp = JsonConvert.DeserializeObject<T>(item.Blob);
                myReturn.Add(temp);
            }

            return myReturn;
        }

        public CloudTable GetTable(DataSourceEnum dataSourceServerMode, string tableName)
        {
            // If the DataSource is unknown, then assume the global one
            if (dataSourceServerMode == DataSourceEnum.Unknown)
            {
                dataSourceServerMode = SystemGlobalsModel.Instance.DataSourceValue;
            }

            CloudTableClient DestinationTableClient;

            var DestinationStorageConnectionString = GetDataSourceConnectionString(dataSourceServerMode);

            var Connection = CloudConfigurationManager.GetSetting(DestinationStorageConnectionString);

            CloudStorageAccount DestinationStorageAccount = CloudStorageAccount.Parse(Connection
            );

            DestinationTableClient = DestinationStorageAccount.CreateCloudTableClient();
            var DestinationTable = DestinationTableClient.GetTableReference(tableName);
            DestinationTable.CreateIfNotExists();

            return DestinationTable;
        }
    }
}