using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _5051.Models;
namespace _5051.Backend
{
    /// <summary>
    /// Backend Mock DataSource for SchoolDismissalSettingss, to manage them
    /// </summary>
    public class SchoolDismissalSettingsDataSourceMock : ISchoolDismissalSettingsInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile SchoolDismissalSettingsDataSourceMock instance;
        private static readonly object syncRoot = new Object();

        private SchoolDismissalSettingsDataSourceMock() { }

        public static SchoolDismissalSettingsDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SchoolDismissalSettingsDataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The SchoolDismissalSettings List
        /// </summary>
        private List<SchoolDismissalSettingsModel> SchoolDismissalSettingsList = new List<SchoolDismissalSettingsModel>();

        /// <summary>
        /// Makes a new SchoolDismissalSettings
        /// </summary>
        /// <param name="data"></param>
        /// <returns>SchoolDismissalSettings Passed In</returns>
        public SchoolDismissalSettingsModel Create(SchoolDismissalSettingsModel data, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            SchoolDismissalSettingsList.Add(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public SchoolDismissalSettingsModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = SchoolDismissalSettingsList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public SchoolDismissalSettingsModel Update(SchoolDismissalSettingsModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = SchoolDismissalSettingsList.Find(n => n.Id == data.Id);
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

            var myData = SchoolDismissalSettingsList.Find(n => n.Id == Id);
            var myReturn = SchoolDismissalSettingsList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of SchoolDismissalSettingss</returns>
        public List<SchoolDismissalSettingsModel> Index()
        {
            return SchoolDismissalSettingsList;
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
            SchoolDismissalSettingsList.Clear();
        }

        /// <summary>
        /// The Defalt Data Set
        /// </summary>
        private void DataSetDefault()
        {
            DataSetClear();

            // Call over to the Settings Model itself to have it set it's default settings
            var Temp = new SchoolDismissalSettingsModel();
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