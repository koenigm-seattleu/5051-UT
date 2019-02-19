using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;


namespace _5051.Backend
{
    public class SchoolDismissalSettingsBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile SchoolDismissalSettingsBackend instance;
        private static readonly object syncRoot = new Object();

        private SchoolDismissalSettingsBackend() { }

        public static SchoolDismissalSettingsBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SchoolDismissalSettingsBackend();
                            SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
                        }
                    }
                }

                return instance;
            }
        }

        // Get the Datasource to use
        private static ISchoolDismissalSettingsInterface DataSource;

        /// <summary>
        /// Switches between Live, and Mock Datasets
        /// </summary>
        /// <param name="dataSourceEnum"></param>
        public static void SetDataSource(DataSourceEnum dataSourceEnum)
        {
            switch (dataSourceEnum)
            {
                case DataSourceEnum.SQL:
                    break;

                case DataSourceEnum.Local:
                case DataSourceEnum.ServerLive:
                case DataSourceEnum.ServerTest:
                    DataSourceBackendTable.Instance.SetDataSourceServerMode(dataSourceEnum);
                    DataSource = SchoolDismissalSettingsDataSourceTable.Instance;
                    break;

                case DataSourceEnum.Mock:
                default:
                    // Default is to use the Mock
                    DataSource = SchoolDismissalSettingsDataSourceMock.Instance;
                    break;
            }
        }

        /// <summary>
        /// Switch the data set between Demo, Default and Unit Test
        /// </summary>
        /// <param name="SetEnum"></param>
        public static void SetDataSourceDataSet(DataSourceDataSetEnum SetEnum)
        {
            DataSource.LoadDataSet(SetEnum);
        }

        /// <summary>
        /// Get Index
        /// </summary>
        /// <returns></returns>
        public List<SchoolDismissalSettingsModel> Index()
        {
            return DataSource.Index();
        }

        /// <summary>
        /// Helper function that resets the DataSource, and rereads it.
        /// </summary>
        public void Reset()
        {
            DataSource.Reset();
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

            var myReturn = DataSource.Read(id);
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

            var myReturn = DataSource.Update(data);

            //Todo: not ready yet, need to work out how to call for Resetting of the Calendar properly
            //if (myReturn != null)
            //{
            //    // Changes to the Update, means a setting on the calendar is different.
            //    // Need to go through the calendar and change things.  That way a change to the dismissal time is reflected if changed etc.
            //    SchoolCalendarBackend.Instance.ResetDefaults();
            //}
            
            return myReturn;
        }

        /// <summary>
        /// Returns the First record
        /// </summary>
        /// <returns>Null or valid data</returns>
        public SchoolDismissalSettingsModel GetDefault()
        {
            var myReturn = Index().First();
            return myReturn;
        }
    }
}