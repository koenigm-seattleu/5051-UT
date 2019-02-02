using System.ComponentModel.DataAnnotations;

namespace _5051.Models
{
    /// <summary>
    /// Used when passing to a Post that needs a Student Id
    /// </summary>
    public class StudentInputModel
    {
        /// <summary>
        /// The ID for the Student, this is the key, and a required field
        /// </summary>
        [Key]
        [Display(Name = "Id", Description = "Student Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }
    }
}