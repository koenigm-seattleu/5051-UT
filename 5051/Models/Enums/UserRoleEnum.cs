using System.ComponentModel.DataAnnotations;

namespace _5051.Models
{
    /// <summary>
    /// Factory Inventory Item Categories
    /// </summary>
    public enum UserRoleEnum
    {
        // Not classified
        [Display(Name = "Unknown")]
        Unknown = 0,

        [Display(Name = "Student")]
        StudentUser= 10,

        [Display(Name = "Admin")]
        AdminUser = 40,

        [Display(Name = "Support")]
        SupportUser = 50,

        [Display(Name = "Teacher")]
        TeacherUser = 60,
    }
}