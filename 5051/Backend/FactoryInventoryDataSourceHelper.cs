using System;
using System.Collections.Generic;

using _5051.Models;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace _5051.Backend
{
    /// <summary>
    /// Backend Table DataSource for AvatarItems, to manage them
    /// </summary>
    public class FactoryInventoryDataSourceHelper
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile FactoryInventoryDataSourceHelper instance;
        private static readonly object syncRoot = new Object();

        private FactoryInventoryDataSourceHelper() { }

        public static FactoryInventoryDataSourceHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new FactoryInventoryDataSourceHelper();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The AvatarItem List
        /// </summary>
        private List<FactoryInventoryModel> DataList = new List<FactoryInventoryModel>();

        /// <summary>
        /// Clear the Data List, and build up a new one
        /// </summary>
        /// <returns></returns>
        public List<FactoryInventoryModel> GetDefaultDataSet()
        {
            DataList.Clear();

            #region Food
            DataList.Add(new FactoryInventoryModel("GChocolate.png", "Chocolate", "Ice Cream", FactoryInventoryCategoryEnum.Food, 5, 10, false));
            DataList.Add(new FactoryInventoryModel("GCoffee.png", "Coffee", "Ice Cream", FactoryInventoryCategoryEnum.Food, 8, 10, true));
            DataList.Add(new FactoryInventoryModel("GCookiedough.png", "Cookie Dough", "Ice Cream", FactoryInventoryCategoryEnum.Food, 8, 10, true));
            DataList.Add(new FactoryInventoryModel("GMintChip.png", "Mint Chip", "Ice Cream", FactoryInventoryCategoryEnum.Food, 8, 10, true));
            DataList.Add(new FactoryInventoryModel("GSherbert.png", "Sherbert", "Sherbert", FactoryInventoryCategoryEnum.Food, 8, 10, true));
            DataList.Add(new FactoryInventoryModel("GStrawberry.png", "Strawberry", "Ice Cream", FactoryInventoryCategoryEnum.Food, 8, 10, false));

            DataList.Add(new FactoryInventoryModel("Cheese.png", "Cheese", "Dairy", FactoryInventoryCategoryEnum.Food, 8, 10, false));
            DataList.Add(new FactoryInventoryModel("Avacadoes.png", "Avacadoes", "Fruit", FactoryInventoryCategoryEnum.Food, 8, 10, false));
            DataList.Add(new FactoryInventoryModel("Cilantro.png", "Cilantro", "Herb", FactoryInventoryCategoryEnum.Food, 8, 10, false));
            DataList.Add(new FactoryInventoryModel("Onions.png", "Onions", "Veg", FactoryInventoryCategoryEnum.Food, 8, 10, false));
            DataList.Add(new FactoryInventoryModel("Meat.png", "Meat", "Meat", FactoryInventoryCategoryEnum.Food, 8, 10, false));
            DataList.Add(new FactoryInventoryModel("Lime.png", "Lime", "Fruit", FactoryInventoryCategoryEnum.Food, 8, 10, false));
            DataList.Add(new FactoryInventoryModel("Tomatoes.png", "Tomatoes", "Veg", FactoryInventoryCategoryEnum.Food, 8, 10, false));
            DataList.Add(new FactoryInventoryModel("Tortillas.png", "Tortillas", "Shell", FactoryInventoryCategoryEnum.Food, 8, 10, false));
            #endregion Food

            #region Truck
            DataList.Add(new FactoryInventoryModel("Wheels1.png", "Wheel", "Wheels", FactoryInventoryCategoryEnum.Wheels, 1, 10, false, true)); // Default

            DataList.Add(new FactoryInventoryModel("Trailer1.png", "Slick", "Trailer", FactoryInventoryCategoryEnum.Trailer, 1, 10, false, true)); // Default
            DataList.Add(new FactoryInventoryModel("Truck1.png", "Slick", "Truck", FactoryInventoryCategoryEnum.Truck, 1, 10, false, true)); // Default

            DataList.Add(new FactoryInventoryModel("Trailer2.png", "Fire", "Trailer", FactoryInventoryCategoryEnum.Trailer, 20, 1, false));
            DataList.Add(new FactoryInventoryModel("Truck2.png", "Fire", "Truck", FactoryInventoryCategoryEnum.Truck, 20, 1, false));

            DataList.Add(new FactoryInventoryModel("Trailer3.png", "Bubble", "Trailer", FactoryInventoryCategoryEnum.Trailer, 20, 1, false));
            DataList.Add(new FactoryInventoryModel("Truck3.png", "Bubble", "Truck", FactoryInventoryCategoryEnum.Truck, 20, 1, false));
            #endregion Truck

            #region MenuTopperSign
            DataList.Add(new FactoryInventoryModel("Menu0.png", "No Menu", "No Menu", FactoryInventoryCategoryEnum.Menu, 1, 1, false, true)); // Default
            DataList.Add(new FactoryInventoryModel("Topper0.png", "No Topper", "No Topper", FactoryInventoryCategoryEnum.Topper, 1, 1, false, true)); // Default
            DataList.Add(new FactoryInventoryModel("Sign0.png", "No Sign", "No Sign", FactoryInventoryCategoryEnum.Sign, 1, 1, false, true)); // Default

            DataList.Add(new FactoryInventoryModel("Menu1.png", "Ice Cream", "Ice Cream Menu", FactoryInventoryCategoryEnum.Menu, 10, 1, false));
            DataList.Add(new FactoryInventoryModel("Topper1.png", "Ice Cream", "Ice Cream Topper", FactoryInventoryCategoryEnum.Topper, 10, 1, false));
            DataList.Add(new FactoryInventoryModel("Sign1.png", "Ice Cream", "Ice Cream Sign", FactoryInventoryCategoryEnum.Sign, 10, 1, false));

            DataList.Add(new FactoryInventoryModel("Menu2.png", "Taco", "Taco Menu", FactoryInventoryCategoryEnum.Menu, 20, 20, false));
            DataList.Add(new FactoryInventoryModel("Topper2.png", "Taco", "Taco Topper", FactoryInventoryCategoryEnum.Topper, 20, 20, false));
            DataList.Add(new FactoryInventoryModel("Sign2.png", "Taco", "Taco Sign", FactoryInventoryCategoryEnum.Sign, 20, 20, false));

            #endregion MenuTopperSign

            return DataList;
        }
    }
}