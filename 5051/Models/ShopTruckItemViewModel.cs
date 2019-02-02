using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    // Used for Truck Editing Controller and View
    public class ShopTruckItemViewModel
    {
        // Current Item
        public FactoryInventoryModel Item = new FactoryInventoryModel();

        // List of Options to Pick...
        public List<FactoryInventoryModel> ItemList = new List<FactoryInventoryModel>();
    }
}