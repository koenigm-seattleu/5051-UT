using System.ComponentModel.DataAnnotations;

namespace _5051.Models
{
    /// <summary>
    /// Factory Inventory Item Categories
    /// </summary>
    public enum FactoryInventoryCategoryEnum
    {
        // Not classified
        [Display(Name = "Unknown")]
        Unknown = 0,

        [Display(Name = "Food")]
        // food
        Food = 5,

        // Truck Inventory Items
        [Display(Name = "Truck")]
        Truck = 10,

        [Display(Name = "Wheels")]
        Wheels = 11,

        [Display(Name = "Topper")]
        Topper = 12,

        [Display(Name = "Sign")]
        Sign = 13,

        [Display(Name = "Trailer")]
        Trailer = 14,

        [Display(Name = "Menu")]
        Menu = 15
    }
}