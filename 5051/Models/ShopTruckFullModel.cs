using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    // Where on the Truck Items are positions
    public class ShopTruckFullModel
    {
        // The Inventory ID for the Wheels
        public string Wheels { get; set; }

        // The Inventory ID for the Topper
        public string Topper { get; set; }

        // The Inventory ID for the Trailer
        public string Trailer { get; set; }

        // The Inventory ID for the Menu
        public string Menu { get; set; }

        // The Inventory ID for the Sign
        public string Sign { get; set; }

        // The Inventory ID for the Truck
        public string Truck { get; set; }

        // Customer Count
        public int CustomersTotal { get; set; }

        // Track the status of th Truck
        public bool IsClosed { get; set;}

        public List<TransactionModel> TransactionList { get; set; }

        // Truck Name
        public string TruckName { get; set; }

  
        public int Income { get; set; }

        public int Outcome { get; set; }

        public List<TransactionModel> BusinessList { get; set; }

        // Truck Item URIs
        public string WheelsUri { get; set; }

        public string TopperUri { get; set; }

        public string TrailerUri { get; set; }

        public string MenuUri { get; set; }

        public string SignUri { get; set; }

        public string TruckUri { get; set; }

        // Tokens
        public int Tokens { get; set; }

        // Experience
        public int Experience { get; set; }

        // Iteration Number
        public int IterationNumber { get; set; }

         
        public ShopTruckFullModel()
        {
            // New Models, set default data

            IsClosed = false; // Trucks Open to start with
            TransactionList = new List<TransactionModel>();   // Empty List of Transactions
            CustomersTotal = 0; // No customers to start off with
            TruckName = "My Truck";
            Income = 0;
            Outcome = 0;
            BusinessList = new List<TransactionModel>();

            Truck = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Truck).Id;

            Wheels = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Wheels).Id;

            Topper = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Topper).Id;

            Trailer = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Trailer).Id;

            Sign = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Sign).Id;

            Menu = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetDefault(FactoryInventoryCategoryEnum.Menu).Id;

            // Truck Items (Uri)
            TruckUri = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(Truck);
            WheelsUri = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(Wheels);
            TopperUri = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(Topper);
            TrailerUri = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(Trailer);
            SignUri = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(Sign);
            MenuUri = Backend.DataSourceBackend.Instance.FactoryInventoryBackend.GetFactoryInventoryUri(Menu);
        }
    }
}