using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _5051.Models;
namespace _5051.Backend
{
    /// <summary>
    /// Backend Mock DataSource for KioskSettingss, to manage them
    /// </summary>
    public class KioskSettingsDataSourceMock : IKioskSettingsInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile KioskSettingsDataSourceMock instance;
        private static readonly object syncRoot = new Object();

        private KioskSettingsDataSourceMock() { }

        public static KioskSettingsDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new KioskSettingsDataSourceMock();
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
        private List<KioskSettingsModel> KioskSettingsList = new List<KioskSettingsModel>();

        /// <summary>
        /// Makes a new KioskSettings
        /// </summary>
        /// <param name="data"></param>
        /// <returns>KioskSettings Passed In</returns>
        public KioskSettingsModel Create(KioskSettingsModel data, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            KioskSettingsList.Add(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public KioskSettingsModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = KioskSettingsList.Find(n => n.Id == id);
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
            var myReturn = KioskSettingsList.Find(n => n.Id == data.Id);
            if (myReturn == null)
            {
                return null;
            }

            myReturn.Update(data);

            return myReturn;
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

            var myData = KioskSettingsList.Find(n => n.Id == Id);
            var myReturn = KioskSettingsList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of KioskSettingss</returns>
        public List<KioskSettingsModel> Index()
        {
            return KioskSettingsList;
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
            KioskSettingsList.Clear();
        }

        /// <summary>
        /// The Defalt Data Set
        /// </summary>
        private void DataSetDefault()
        {
            DataSetClear();

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