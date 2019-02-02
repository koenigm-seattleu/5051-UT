using System.ComponentModel.DataAnnotations;

namespace _5051.Models
{
    /// <summary>
    /// Factory Inventory Item Categories
    /// </summary>
    public enum AvatarItemCategoryEnum
    {
        // Not classified
        [Display(Name = "Unknown")]
        Unknown = 0,

        [Display(Name = "Head")]
        Head = 10,

        [Display(Name = "Bangs")]
        HairFront = 40,

        [Display(Name = "Hair")]
        HairBack = 50,

        //ShirtShort = 3,
        [Display(Name = "Expression")]
        Expression = 20,

        [Display(Name = "Cheeks")]
        Cheeks = 30,

        [Display(Name = "Accessory")]
        Accessory = 60,

        [Display(Name = "Shirt")]
        ShirtFull = 70,

        [Display(Name = "Pants")]
        Pants = 80,
    }
}