namespace _5051.Models
{
    // Where on the Truck Items are positions
    public class ShopTruckInputModel
    {
        // The Inventory ID for the Wheels
        public string StudentId { get; set; }

        // The Inventory ID for the Wheels
        public string ItemId { get; set; }

        // Where to place the item
        public FactoryInventoryCategoryEnum Position { get; set; }

        // updated the truck name
        public string TruckName { get; set; }
    }
}