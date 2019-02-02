using _5051.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Backend
{
    /// <summary>
    /// FactoryInventory Backend handles the business logic and data for FactoryInventorys
    /// </summary>
    public class FactoryInventoryBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile FactoryInventoryBackend instance;
        private static readonly object syncRoot = new Object();

        private FactoryInventoryBackend() { }

        public static FactoryInventoryBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new FactoryInventoryBackend();
                            SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
                        }
                    }
                }

                return instance;
            }
        }

        // Get the Datasource to use
        private static IFactoryInventoryInterface DataSource;

        /// <summary>
        /// Sets the Datasource to be Mock or SQL
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
                    DataSource = FactoryInventoryDataSourceTable.Instance;
                    break;

                case DataSourceEnum.Mock:
                default:
                    // Default is to use the Mock
                    DataSource = FactoryInventoryDataSourceMock.Instance;
                    break;
            }
        }

        /// <summary>
        /// Makes a new FactoryInventory
        /// </summary>
        /// <param name="data"></param>
        /// <returns>FactoryInventory Passed In</returns>
        public FactoryInventoryModel Create(FactoryInventoryModel data)
        {
            DataSource.Create(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public FactoryInventoryModel Read(string id)
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
        public FactoryInventoryModel Update(FactoryInventoryModel data)
        {
            if (data == null)
            {
                return null;
            }

            var myReturn = DataSource.Update(data);

            return myReturn;
        }

        /// <summary>
        /// Remove the Data item if it is in the list
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True for success, else false</returns>
        public bool Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myReturn = DataSource.Delete(Id);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of FactoryInventorys</returns>
        public List<FactoryInventoryModel> Index()
        {
            var myData = DataSource.Index();
            return myData;
        }

        /// <summary>
        /// Helper that returns the First FactoryInventory ID in the list, this will be used for creating new FactoryInventorys if no FactoryInventoryID is specified
        /// </summary>
        /// <returns>Null, or FactoryInventory ID of the first FactoryInventory in the list.</returns>
        public string GetFirstFactoryInventoryId()
        {
            string myReturn = null;

            var myData = Index().ToList().FirstOrDefault();
            if (myData == null)
            {
                return myReturn;
            }

            myReturn = myData.Id;
            return myReturn;
        }

        /// <summary>
        /// Helper function that returns the FactoryInventory Image URI
        /// </summary>
        /// <param name="data">The FactoryInventoryId to look up</param>
        /// <returns>null, or the FactoryInventory image URI</returns>
        public string GetFactoryInventoryUri(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            string myReturn = null;

            var myData = Read(data);
            if (myData == null)
            {
                return myReturn;
            }

            myReturn = myData.Uri;
            return myReturn;
        }

        /// <summary>
        /// Helper that gets the list of Items, and converst them to a SelectList, so they can show in a Drop Down List box
        /// </summary>
        /// <param name="id">optional paramater, of the Item that is currently selected</param>
        /// <returns>List of SelectListItems as a SelectList</returns>
        public List<SelectListItem> GetFactoryInventoryListItem(string id = null)
        {
            var myDataList = Index();

            //var myReturn = new SelectList(myDataList);

            var myReturn = myDataList.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id,
                Selected = (a.Id == id),
            }).ToList();

            return myReturn;
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
        /// Helper function that resets the DataSource, and rereads it.
        /// </summary>
        public void Reset()
        {
            DataSource.Reset();
        }

        /// <summary>
        /// Return the First item in the List for the Given Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public FactoryInventoryModel GetDefault(FactoryInventoryCategoryEnum category)
        {
            // If there is a category item, check to see if there is a IsDefault for one as well.

            var dataTemp = Index().Where(m => m.Category == category).ToList();
            if (dataTemp.Any())
            {
                var dataDefault = dataTemp.Where(m => m.IsDefault == true).ToList();
                if (dataDefault.Any())
                {
                    return dataDefault.FirstOrDefault();
                }
            }

            return dataTemp.FirstOrDefault();
        }

        public FactoryInventoryModel GetDefaultTruckFullItem(FactoryInventoryCategoryEnum category)
        {
            var data = new FactoryInventoryModel();

            // Return the next item that is NOT the default
            var TruckSet = Index().Where(m => m.Category == category).ToList();
            if (TruckSet.Any())
            {
                var dataDefault = TruckSet.Where(m => m.IsDefault == false).ToList();
                if (dataDefault.Any())
                {
                    data = dataDefault.FirstOrDefault();
                }
            }

            return data;
        }

        /// <summary>
        /// Updates the student Model
        /// Adds the Inventory Item to the Model
        /// Adds the Item to the Truck Slot
        /// Returns for all slots
        ///  Usefull when calling for a default truck that is visible
        ///  Good for demostration or testing
        /// </summary>
        /// <returns></returns>
        public StudentModel GetDefaultFullTruck(StudentModel studentData)
        {
            FactoryInventoryModel Item;

            //Truck
            Item = GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Truck);
            if (!string.IsNullOrEmpty(Item.Name))
            {
                if (studentData.Inventory.Where(x => x.Id == Item.Id).FirstOrDefault() == null)
                {
                    studentData.Inventory.Add(Item);
                }
                studentData.Truck.Truck = Item.Id;
            }

            //Wheels
            Item = GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Wheels);
            if (!string.IsNullOrEmpty(Item.Name))
            {
                if (studentData.Inventory.Where(x => x.Id == Item.Id).FirstOrDefault() == null)
                {
                    studentData.Inventory.Add(Item);
                }
                studentData.Truck.Wheels = Item.Id;
            }

            // Topper
            Item = GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Topper);
            if (!string.IsNullOrEmpty(Item.Name))
            {
                if (studentData.Inventory.Where(x => x.Id == Item.Id).FirstOrDefault() == null)
                {
                    studentData.Inventory.Add(Item);
                }
                studentData.Truck.Topper = Item.Id;
            }

            // Menu
            Item = GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Menu);
            if (!string.IsNullOrEmpty(Item.Name))
            {
                if (studentData.Inventory.Where(x => x.Id == Item.Id).FirstOrDefault() == null)
                {
                    studentData.Inventory.Add(Item);
                }
                studentData.Truck.Menu = Item.Id;
            }

            // Sign
            Item = GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Sign);
            if (!string.IsNullOrEmpty(Item.Name))
            {
                if (studentData.Inventory.Where(x => x.Id == Item.Id).FirstOrDefault() == null)
                {
                    studentData.Inventory.Add(Item);
                }
                studentData.Truck.Sign = Item.Id;
            }

            // Trialer
            Item = GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Trailer);
            if (!string.IsNullOrEmpty(Item.Name))
            {
                if (studentData.Inventory.Where(x => x.Id == Item.Id).FirstOrDefault() == null)
                {
                    studentData.Inventory.Add(Item);
                }
                studentData.Truck.Trailer = Item.Id;
            }

            return studentData;
        }

        /// <summary>
        /// Updates the student Model
        /// Adds the Inventory Item to the Model
        /// Adds the Item to the Truck Slot
        /// Returns for all slots
        ///  UseEmpty when calling for a default truck that is visible
        ///  Good for demostration or testing
        /// </summary>
        /// <returns></returns>
        public StudentModel GetDefaultEmptyTruck(StudentModel studentData)
        {
            FactoryInventoryModel Item;

            // Food
            Item = GetDefault(FactoryInventoryCategoryEnum.Food);
            if (!string.IsNullOrEmpty(Item.Name))
            {
                if (studentData.Inventory.Where(x => x.Id == Item.Id).FirstOrDefault() == null)
                {
                    studentData.Inventory.Add(Item);
                }
            }

            //Truck
            Item = GetDefault(FactoryInventoryCategoryEnum.Truck);
            if (!string.IsNullOrEmpty(Item.Name))
            {
                if (studentData.Inventory.Where(x => x.Id == Item.Id).FirstOrDefault() == null)
                {
                    studentData.Inventory.Add(Item);
                }
                studentData.Truck.Truck = Item.Id;
            }

            //Wheels
            Item = GetDefault(FactoryInventoryCategoryEnum.Wheels);
            if (!string.IsNullOrEmpty(Item.Name))
            {
                if (studentData.Inventory.Where(x => x.Id == Item.Id).FirstOrDefault() == null)
                {
                    studentData.Inventory.Add(Item);
                }
                studentData.Truck.Wheels = Item.Id;
            }

            // Topper
            Item = GetDefault(FactoryInventoryCategoryEnum.Topper);
            if (!string.IsNullOrEmpty(Item.Name))
            {
                if (studentData.Inventory.Where(x => x.Id == Item.Id).FirstOrDefault() == null)
                {
                    studentData.Inventory.Add(Item);
                }

                studentData.Truck.Topper = Item.Id;
            }

            // Menu
            Item = GetDefault(FactoryInventoryCategoryEnum.Menu);
            if (!string.IsNullOrEmpty(Item.Name))
            {
                if (studentData.Inventory.Where(x => x.Id == Item.Id).FirstOrDefault() == null)
                {
                    studentData.Inventory.Add(Item);
                }

                studentData.Truck.Menu = Item.Id;
            }

            // Sign
            Item = GetDefault(FactoryInventoryCategoryEnum.Sign);
            if (!string.IsNullOrEmpty(Item.Name))
            {
                if (studentData.Inventory.Where(x => x.Id == Item.Id).FirstOrDefault() == null)
                {
                    studentData.Inventory.Add(Item);
                }

                studentData.Truck.Sign = Item.Id;
            }

            // Trialer
            Item = GetDefault(FactoryInventoryCategoryEnum.Trailer);
            if (!string.IsNullOrEmpty(Item.Name))
            {
                if (studentData.Inventory.Where(x => x.Id == Item.Id).FirstOrDefault() == null)
                {
                    studentData.Inventory.Add(Item);
                }

                studentData.Truck.Trailer = Item.Id;
            }

            return studentData;
        }

        public ShopTruckViewModel GetShopTruckViewModel(StudentModel studentData)
        {
            var data = new ShopTruckViewModel
            {
                StudentId = studentData.Id,

                // Load the data set for each type
                TruckItem = GetShopTruckItemViewModel(studentData.Id, studentData.Truck.Truck),
                WheelsItem = GetShopTruckItemViewModel(studentData.Id, studentData.Truck.Wheels),
                TopperItem = GetShopTruckItemViewModel(studentData.Id, studentData.Truck.Topper),
                TrailerItem = GetShopTruckItemViewModel(studentData.Id, studentData.Truck.Trailer),
                MenuItem = GetShopTruckItemViewModel(studentData.Id, studentData.Truck.Menu),
                SignItem = GetShopTruckItemViewModel(studentData.Id, studentData.Truck.Sign)
            };

            return data;
        }

        /// <summary>
        /// Build out the View used by Edit.
        /// If anything is wrong, don't init the studentID.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="InventoryId"></param>
        /// <param name="defaultUri"></param>
        public ShopTruckItemViewModel GetShopTruckItemViewModel(string studentId, string inventoryId)
        {
            /* 
             * Pass in the inventory Id for the Item.
             * 
             * Retrieve the Id item
             * 
             * Put the returned Factory Item in the Item Slot
             * 
             * Get the Category
             * 
             * Get the StudentId Inventory
             * 
             * Find all the Items that match the Category
             * 
             * Add them to the Item List
             * 
             */

            var data = new ShopTruckItemViewModel();

            if (string.IsNullOrEmpty(studentId))
            {
                return null;
            }

            if (string.IsNullOrEmpty(inventoryId))
            {
                return null;
            }

            var InventoryData = DataSourceBackend.Instance.FactoryInventoryBackend.Read(inventoryId);
            if (InventoryData == null)
            {
                return null;
            }

            var StudentData = DataSourceBackend.Instance.StudentBackend.Read(studentId);
            if (StudentData == null)
            {
                return null;
            }

            var InventoryListData = StudentData.Inventory.Where(m => m.Category == InventoryData.Category).ToList();

            // Found the Item, and Found the Inventory List for the Item
            data.Item = InventoryData;
            data.ItemList = InventoryListData;

            return data;
        }

        /// <summary>
        /// Backup the Data from Source to Destination
        /// </summary>
        /// <param name="dataSourceSource"></param>
        /// <param name="dataSourceDestination"></param>
        /// <returns></returns>
        public bool BackupData(DataSourceEnum dataSourceSource, DataSourceEnum dataSourceDestination)
        {
            var result = DataSource.BackupData(dataSourceSource, dataSourceDestination);
            return result;
        }
    }
}