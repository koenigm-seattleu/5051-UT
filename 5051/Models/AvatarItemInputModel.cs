namespace _5051.Models
{
    // Where on the Truck Items are positions
    public class AvatarItemInputModel
    {
        public string StudentId { get; set; }

        public string ItemId { get; set; }

        // Where to place the item
        public AvatarItemCategoryEnum Position { get; set; }
    }
}