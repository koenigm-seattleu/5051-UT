using _5051.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Backend
{
    /// <summary>
    /// AvatarItem Backend handles the business logic and data for AvatarItems
    /// </summary>
    public class AvatarItemBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile AvatarItemBackend instance;
        private static readonly object syncRoot = new Object();

        private AvatarItemBackend() { }

        public static AvatarItemBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new AvatarItemBackend();
                            SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
                        }
                    }
                }

                return instance;
            }
        }

        // Get the Datasource to use
        public static IAvatarItemInterface DataSource;

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
                    DataSource = AvatarItemDataSourceTable.Instance;
                    break;

                case DataSourceEnum.Mock:
                default:
                    // Default is to use the Mock
                    DataSource = AvatarItemDataSourceMock.Instance;
                    break;
            }

        }

        /// <summary>
        /// Makes a new AvatarItem
        /// </summary>
        /// <param name="data"></param>
        /// <returns>AvatarItem Passed In</returns>
        public AvatarItemModel Create(AvatarItemModel data)
        {
            DataSource.Create(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public AvatarItemModel Read(string id)
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
        public AvatarItemModel Update(AvatarItemModel data)
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
        /// <returns>List of AvatarItems</returns>
        public List<AvatarItemModel> Index()
        {
            var myData = DataSource.Index();
            return myData;
        }

        /// <summary>
        /// Helper that returns the First AvatarItem ID in the list, this will be used for creating new AvatarItems if no AvatarItemID is specified
        /// </summary>
        /// <returns>Null, or AvatarItem ID of the first AvatarItem in the list.</returns>
        public string GetFirstAvatarItemId()
        {
            string myReturn = null;

            var myData = Index().ToList().FirstOrDefault();
            //if (myData == null)
            //{
            //    return myReturn;
            //}

            myReturn = myData.Id;
            return myReturn;
        }

        /// <summary>
        /// Helper function that returns the AvatarItem Image URI
        /// </summary>
        /// <param name="data">The AvatarItemId to look up</param>
        /// <returns>null, or the AvatarItem image URI</returns>
        public string GetAvatarItemUri(string data)
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
        public List<SelectListItem> GetAvatarItemListItem(string id = null)
        {
            var myDataList = Index();

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
        public AvatarItemModel GetDefault(AvatarItemCategoryEnum category)
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

        public List<AvatarItemModel> GetAllAvatarItem(AvatarItemCategoryEnum category)
        {
            var DataSet = Index().Where(m => m.Category == category).ToList();
            return DataSet;
        }

        public AvatarItemModel GetDefaultAvatarItemFullItem(AvatarItemCategoryEnum category)
        {
            var DataSet = Index().Where(m => m.Category == category).ToList();
            if (DataSet.Count == 0)
            {
                return GetDefault(category);
            }

            var data = DataSet.FirstOrDefault();

            return data;
        }

        /// <summary>
        /// Build out the View used by Edit.
        /// If anything is wrong, don't init the studentID.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="InventoryId"></param>
        /// <param name="defaultUri"></param>
        public AvatarItemShopViewModel GetAvatarItemShopViewModel(string studentId, string inventoryId)
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

            var data = new AvatarItemShopViewModel();

            if (string.IsNullOrEmpty(studentId))
            {
                return null;
            }

            if (string.IsNullOrEmpty(inventoryId))
            {
                return null;
            }

            var InventoryData = DataSourceBackend.Instance.AvatarItemBackend.Read(inventoryId);
            if (InventoryData == null)
            {
                return null;
            }

            var StudentData = DataSourceBackend.Instance.StudentBackend.Read(studentId);
            if (StudentData == null)
            {
                return null;
            }

            var InventoryListData = StudentData.AvatarInventory.Where(m => m.Category == InventoryData.Category).ToList();

            // Found the Item, and Found the Inventory List for the Item
            data.Item = InventoryData;
            data.ItemList = InventoryListData;

            return data;
        }

        public AvatarSelectShopViewModel GetAvatarShopViewModel(StudentModel studentData, AvatarItemModel item)
        {
            var data = new AvatarSelectShopViewModel
            {
                Student = studentData,

                data = GetAvatarItemShopViewModel(studentData.Id, item.Id)
            };

            return data;
        }
    }
}