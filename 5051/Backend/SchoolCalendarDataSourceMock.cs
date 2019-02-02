using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _5051.Models;
namespace _5051.Backend
{
    /// <summary>
    /// Backend Mock DataSource for SchoolCalendars, to manage them
    /// </summary>
    public class SchoolCalendarDataSourceMock : ISchoolCalendarInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile SchoolCalendarDataSourceMock instance;
        private static readonly object syncRoot = new Object();

        private SchoolCalendarDataSourceMock() { }

        public static SchoolCalendarDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SchoolCalendarDataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The SchoolCalendar List
        /// </summary>
        private List<SchoolCalendarModel> SchoolCalendarList = new List<SchoolCalendarModel>();

        /// <summary>
        /// Makes a new SchoolCalendar
        /// </summary>
        /// <param name="data"></param>
        /// <returns>SchoolCalendar Passed In</returns>
        public SchoolCalendarModel Create(SchoolCalendarModel data, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            SchoolCalendarList.Add(data);

            //sort by date
            SchoolCalendarList = SchoolCalendarList.OrderBy(x => x.Date).ToList();

            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public SchoolCalendarModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = SchoolCalendarList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public SchoolCalendarModel Update(SchoolCalendarModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = SchoolCalendarList.Find(n => n.Id == data.Id);
            if (myReturn == null)
            {
                return null;
            }

            myReturn.Update(data);

            //sort by date
            SchoolCalendarList = SchoolCalendarList.OrderBy(x => x.Date).ToList();

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

            var myData = SchoolCalendarList.Find(n => n.Id == Id);
            var myReturn = SchoolCalendarList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of SchoolCalendars</returns>
        public List<SchoolCalendarModel> Index()
        {
            return SchoolCalendarList;
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
            SchoolCalendarList.Clear();
        }

        /// <summary>
        /// The Defalt Data Set
        /// </summary>
        private void DataSetDefault()
        {
            DataSetClear();

            var startDate = SchoolDismissalSettingsBackend.Instance.GetDefault().DayFirst;  //school year start date
            var endDate = SchoolDismissalSettingsBackend.Instance.GetDefault().DayLast;  //school year end date
            var currentDate = startDate;  //loop variable

            while (currentDate.CompareTo(endDate) <= 0)
            {

                var newCalendarModel = new SchoolCalendarModel(currentDate);
                Create(newCalendarModel);

                SchoolCalendarBackendHelper.SetDefault(newCalendarModel);  //use default settings
                Update(newCalendarModel);

                currentDate = currentDate.AddDays(1);
            }

            //sort by date
            SchoolCalendarList = SchoolCalendarList.OrderBy(x => x.Date).ToList();
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

        /// <summary>
        /// Find a date in the data store
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public SchoolCalendarModel ReadDate(DateTime date)
        {
            // Use the short date string because only checking dd.mm.yy not time...
            var myData = SchoolCalendarList.Find(n => n.Date.ToShortDateString() == date.ToShortDateString());
            return myData;
        }

        /// <summary>
        /// Not implemented for Mock
        /// </summary>
        /// <param name="dataSourceSource"></param>
        /// <param name="dataSourceDestination"></param>
        /// <returns></returns>
        public bool BackupData(DataSourceEnum dataSourceSource, DataSourceEnum dataSourceDestination)
        {
            return true;
        }

    }
}