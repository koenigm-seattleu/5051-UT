using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _5051.Backend;

namespace _5051.Models
{
    // The URI for the Images for the Truck
    public class ShopTruckViewModel
    {
        // Positions, with current item.
        public ShopTruckItemViewModel TruckItem;
        public ShopTruckItemViewModel WheelsItem;
        public ShopTruckItemViewModel TopperItem;
        public ShopTruckItemViewModel TrailerItem;
        public ShopTruckItemViewModel MenuItem;
        public ShopTruckItemViewModel SignItem;
        public string TruckName;

        // The StudentID for this truck, used to simplify the models
        public string StudentId;
        
    }
}