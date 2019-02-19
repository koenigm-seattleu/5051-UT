using System;
using System.Collections.Generic;
using System.Linq;
using _5051.Models;

namespace _5051.Backend
{
    /// <summary>
    /// Backend Table DataSource for KioskSettingss, to manage them
    /// </summary>
    public class KioskSettingsDataSourceTable : IKioskSettingsInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile KioskSettingsDataSourceTable instance;
        private static readonly object syncRoot = new Object();

        private KioskSettingsDataSourceTable() { }

        public static KioskSettingsDataSourceTable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new KioskSettingsDataSourceTable();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The KioskSettings List
        /// </summary>
        private List<KioskSettingsModel> DataList = new List<KioskSettingsModel>();

        private const string ClassName = "KioskSettingsModel";
        /// <summary>
        /// Table Name used for data storage
        /// </summary>
        private readonly string tableName = ClassName.ToLower();

        /// <summary>
        /// Partition Key used for data storage
        /// </summary>
        private readonly string partitionKey = ClassName.ToLower();

        /// <summary>
        /// Makes a new KioskSettings
        /// </summary>
        /// <param name="data"></param>
        /// <returns>KioskSettings Passed In</returns>
        public KioskSettingsModel Create(KioskSettingsModel data, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            // If using the defaul data source, use it, else just do the table operation
            if (dataSourceEnum == DataSourceEnum.Unknown)
            {
                DataList.Add(data);
            }

            // Add to Storage
            var myResult = DataSourceBackendTable.Instance.Create<KioskSettingsModel>(tableName, partitionKey, data.Id, data, dataSourceEnum);

            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// Does not access storage, just reads from memeory
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public KioskSettingsModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = DataList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public KioskSettingsModel Update(KioskSettingsModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = DataList.Find(n => n.Id == data.Id);
            if (myReturn == null)
            {
                return null;
            }

            myReturn.Update(data);

            // Update Storage
            var myResult = DataSourceBackendTable.Instance.Create<KioskSettingsModel>(tableName, partitionKey, data.Id, data);

            return data;
        }

        /// <summary>
        /// Remove the Data item if it is in the list
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True for success, else false</returns>
        public bool Delete(string Id, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            // If using the defaul data source, use it, else just do the table operation
            if (dataSourceEnum == DataSourceEnum.Unknown)
            {
                var data = DataList.Find(n => n.Id == Id);
                if (data == null)
                {
                    return false;
                }

                DataList.Remove(data);
            }

            // Storage Delete
            var myReturn = DataSourceBackendTable.Instance.Delete<KioskSettingsModel>(tableName, partitionKey, Id, dataSourceEnum);

            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of KioskSettingss</returns>
        public List<KioskSettingsModel> Index()
        {
            return DataList;
        }

        /// <summary>
        /// Reset the Data, and reload it
        /// </summary>
        public void Reset()
        {
            Initialize();
        }

        /// <summary>
        /// Create Placeholder Initial Data
        /// </summary>
        public void Initialize()
        {
            LoadDataSet(DataSourceDataSetEnum.Default);
        }

        /// <summary>
        /// Clears the Data
        /// </summary>
        private void DataSetClear()
        {
            DataList.Clear();
        }

        /// <summary>
        /// The Defalt Data Set
        /// </summary>
        private void DataSetDefault()
        {
            DataSetClear();
            CreateDataSetDefaultData();
        }

        /// <summary>
        /// Load the data from the server, and then default data if needed.
        /// </summary>
        public void CreateDataSetDefaultData()
        {

            // Storage Load all rows
            var DataSetList = LoadAll();

            foreach (var item in DataSetList)
            {
                DataList.Add(item);
            }

            // If Storage is Empty, then Create.
            if (DataList.Count < 1)
            {
                CreateDataSetDefault();
            }
        }

        /// <summary>
        /// Load all the records from the datasource
        /// </summary>
        /// <param name="dataSourceEnum"></param>
        /// <returns></returns>
        public List<KioskSettingsModel> LoadAll(DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            var DataSetList = DataSourceBackendTable.Instance.LoadAll<KioskSettingsModel>(tableName, partitionKey, true, dataSourceEnum);

            return DataSetList;
        }

        /// <summary>
        /// Get the Default data set, and then add it to the current
        /// </summary>
        private void CreateDataSetDefault()
        {
            // Call over to the Settings Model itself to have it set it's default settings
            var Temp = new KioskSettingsModel();
            Temp.SetDefault();
            Create(Temp);
        }

        /// <summary>
        /// Data set for demo
        /// </summary>
        private void DataSetDemo()
        {
            DataSetDefault();
        }

        /// <summary>
        /// Unit Test data set
        /// </summary>
        private void DataSetUnitTest()
        {
            DataSetDefault();
        }

        /// <summary>
        /// Set which data to load
        /// </summary>
        /// <param name="setEnum"></param>
        public void LoadDataSet(DataSourceDataSetEnum setEnum)
        {
            switch (setEnum)
            {
                case DataSourceDataSetEnum.Demo:
                    DataSetDemo();
                    break;

                case DataSourceDataSetEnum.UnitTest:
                    DataSetUnitTest();
                    break;

                case DataSourceDataSetEnum.Default:
                default:
                    DataSetDefault();
                    break;
            }
        }
    }
}