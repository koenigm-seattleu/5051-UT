using System.ComponentModel.DataAnnotations;

namespace _5051.Models
{
    /// <summary>
    /// Used when passing to a Post that needs a Student Id
    /// </summary>
    public class BackupDataInputModel
    {
        /// <summary>
        /// Maintenance Password
        /// </summary>
        [Key]
        [Display(Name = "Password", Description = "Password")]
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }


        /// <summary>
        /// Destination
        /// </summary>
        [Key]
        [Display(Name = "Destination", Description = "Destination")]
        [Required(ErrorMessage = "Required")]
        public DataSourceEnum Destination { get; set; }

        /// <summary>
        /// Confirm
        /// </summary>
        [Key]
        [Display(Name = "Confirm Destination", Description = "Confirm Destination")]
        [Required(ErrorMessage = "Required")]
        public DataSourceEnum ConfirmDestination { get; set; }

        /// <summary>
        /// Destination
        /// </summary>
        [Key]
        [Display(Name = "Destination", Description = "Destination")]
        [Required(ErrorMessage = "Required")]
        public DataSourceEnum Source { get; set; }

        /// <summary>
        /// Confirm
        /// </summary>
        [Key]
        [Display(Name = "Confirm Source", Description = "Confirm Source")]
        [Required(ErrorMessage = "Required")]
        public DataSourceEnum ConfirmSource { get; set; }
    }
}