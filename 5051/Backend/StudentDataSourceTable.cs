using System;
using System.Collections.Generic;
using System.Linq;

using _5051.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace _5051.Backend
{
    /*
    * Store the Student Record as fragments due to limit on Azure Table Storage of 1mb of data per table
    * rk+"Field"
    * 
    * Need to Clear the sub structures from Student before storing them
    * Then recreate them on retrieval

           AvatarCompositeModel AvatarComposite
           List<AvatarItemModel> AvatarInventory
           List<FactoryInventoryModel> Inventory
           GameResultViewModel TruckItems
           ShopTruckModel Truck
           List<AttendanceModel> Attendance
    */

    /// <summary>
    /// Backend Table DataSource for Students, to manage them
    /// </summary>
    public class StudentDataSourceTable : IStudentInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile StudentDataSourceTable instance;
        private static readonly object syncRoot = new Object();

        private StudentDataSourceTable() { }

        public static StudentDataSourceTable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StudentDataSourceTable();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The Student List
        /// </summary>
        private List<StudentModel> DataList = new List<StudentModel>();

        private const string ClassName = "StudentModel";
        /// <summary>
        /// Table Name used for data storage
        /// </summary>
        private readonly string tableName = ClassName.ToLower();

        /// <summary>
        /// Partition Key used for data storage
        /// </summary>
        private readonly string partitionKey = ClassName.ToLower();

        /// <summary>
        /// Makes a new Student
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Student Passed In</returns>
        public StudentModel Create(StudentModel data, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            // If using the default data source, use it, else just do the table operation
            if (dataSourceEnum == DataSourceEnum.Unknown)
            {
                DataList.Add(data);
            }

            var temp = new StudentModel(data)
            {
                Id = data.Id,
                AvatarComposite = null,
                AvatarInventory = null,
                Attendance = null,
            };

            // Add to Storage, the smaller temp student
            DataSourceBackendTable.Instance.Create<StudentModel>(tableName, "student", data.Id, temp, dataSourceEnum);

            // Sub Components
            var tempData = new StudentModel(data)
            {
                Id = data.Id
            };

            // Add to Storage, the smaller temp student
            DataSourceBackendTable.Instance.Create<StudentModel>(tableName, "student", temp.Id, temp, dataSourceEnum);

            // Now store each of the Sub Structures as independent rows
            DataSourceBackendTable.Instance.Create<AvatarCompositeModel>(tableName, "composite", tempData.Id, tempData.AvatarComposite, dataSourceEnum);

            DataSourceBackendTable.Instance.Create<List<AvatarItemModel>>(tableName, "avatarinventory", tempData.Id, tempData.AvatarInventory, dataSourceEnum);

            DataSourceBackendTable.Instance.Create<List<AttendanceModel>>(tableName, "attendance", tempData.Id, tempData.Attendance, dataSourceEnum);

            DataList = DataList.OrderBy(x => x.Name).ToList();

            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// Does not access storage, just reads from memeory
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public StudentModel Read(string id)
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
        public StudentModel Update(StudentModel data)
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

            // Update Storage
            var temp = new StudentModel(data)
            {
                Id = data.Id,
                AvatarComposite = null,
                AvatarInventory = null,
                Attendance = null,
            };

            // Add to Storage, the smaller temp student
            DataSourceBackendTable.Instance.Create<StudentModel>(tableName, "student", temp.Id, temp);


            // Sub components
            var tempData = new StudentModel(data)
            {
                Id = data.Id
            };

            // Now store each of the Sub Structures as independent rows
            DataSourceBackendTable.Instance.Create<AvatarCompositeModel>(tableName, "composite", tempData.Id, tempData.AvatarComposite);

            DataSourceBackendTable.Instance.Create<List<AvatarItemModel>>(tableName, "avatarinventory", tempData.Id, tempData.AvatarInventory);

            DataSourceBackendTable.Instance.Create<List<AttendanceModel>>(tableName, "attendance", tempData.Id, tempData.Attendance);

            DataList = DataList.OrderBy(x => x.Name).ToList();

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

            StudentModel data;

            // If using the defaul data source, use it, else just do the table operation
            if (dataSourceEnum == DataSourceEnum.Unknown)
            {
                data = DataList.Find(n => n.Id == Id);
                if (data == null)
                {
                    return false;
                }

                DataList.Remove(data);
            }

            // Storage Delete
            DataSourceBackendTable.Instance.Delete<StudentModel>(tableName, "student", Id, dataSourceEnum);
            DataSourceBackendTable.Instance.Delete<AvatarCompositeModel>(tableName, "composite", Id, dataSourceEnum);
            DataSourceBackendTable.Instance.Delete<List<AvatarItemModel>>(tableName, "avatarinventory", Id, dataSourceEnum);
            DataSourceBackendTable.Instance.Delete<List<AttendanceModel>>(tableName, "attendance", Id, dataSourceEnum);

            return true;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Students</returns>
        public List<StudentModel> Index()
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
            DataList = LoadAll();
            foreach (var item in DataList)
            {
                //if storage not empty, be sure identity storage matches
                //DataSourceBackend.Instance.IdentityBackend.CreateNewStudentUserIdRecordOnly(item);
            }

            // If Storage is Empty, then Create.
            if (DataList.Count < 1)
            {
                CreateDataSetDefault();
            }

            // Need to order the return, because the azure table returns based on rk, which is not helpfull. So ordering by Name instead
            DataList = DataList.OrderBy(x => x.Name).ToList();
        }

        public List<StudentModel> LoadAll(DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {

            var tempDataList = new List<StudentModel>();

            // Make a call to LoadAll, and pass false for convert, this will return the raw object
            var DataSetList = DataSourceBackendTable.Instance.LoadAll<DataSourceBackendTableEntity>(tableName, "student", false, dataSourceEnum);

            // Loop through each DataSetList, and reload them with the sub fields
            foreach (var temp in DataSetList)
            {
                // Only parse the Student items
                if (!temp.PartitionKey.Contains("student"))
                {
                    continue;
                }

                // Now store each of the Sub Structures as independent rows

                var TempData = new StudentModel();

                //try
                {
                    TempData = DataSourceBackendTable.Instance.Load<StudentModel>(tableName, "student", temp.RowKey, dataSourceEnum);


                    // Set default value incase the load below fails.
                    TempData.AvatarComposite = new AvatarCompositeModel();
                    TempData.AvatarInventory = new List<AvatarItemModel>();
                    TempData.Attendance = new List<AttendanceModel>();

                    // Load each sub field

                    var tempAvatarComposite = DataSourceBackendTable.Instance.Load<AvatarCompositeModel>(tableName, "composite", temp.RowKey, dataSourceEnum);
                    if (tempAvatarComposite != null)
                    {
                        TempData.AvatarComposite = tempAvatarComposite;
                    }

                    var tempAvatarInventory = DataSourceBackendTable.Instance.Load<List<AvatarItemModel>>(tableName, "avatarinventory", temp.RowKey, dataSourceEnum);
                    if (tempAvatarInventory != null)
                    {
                        TempData.AvatarInventory = tempAvatarInventory;
                    }

                    var tempAttendance = DataSourceBackendTable.Instance.Load<List<AttendanceModel>>(tableName, "attendance", temp.RowKey, dataSourceEnum);
                    if (tempAttendance != null)
                    {
                        TempData.Attendance = DataSourceBackendTable.Instance.Load<List<AttendanceModel>>(tableName, "attendance", temp.RowKey, dataSourceEnum);
                    }

                    var newData = new StudentModel(TempData)
                    {
                        Id = temp.RowKey   //Set the ID to the item loaded
                    };

                    tempDataList.Add(newData);
                }
                //catch (Exception ex)
                //{
                //    throw new NotSupportedException();
                //}
            }

            return tempDataList;
        }

        /// <summary>
        /// Get the Default data set, and then add it to the current
        /// </summary>
        private void CreateDataSetDefault()
        {
            var dataSet = StudentDataSourceHelper.Instance.GetDefaultDataSet();
            foreach (var item in dataSet)
            {
                Create(item);
            }
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