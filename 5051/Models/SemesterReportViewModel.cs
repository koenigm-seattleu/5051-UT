using System.Collections.Generic;
using System.Web.Mvc;

namespace _5051.Models
{
    /// <summary>
    /// Semester report view model, for generating semester report view
    /// </summary>
    public class SemesterReportViewModel : BaseReportViewModel
    {
        /// <summary>
        /// Contains a select list for semester selection dropdown
        /// </summary>
        public List<SelectListItem> Semesters { get; set; }

        /// <summary>
        /// The selected semester's id for semester selection dropdown
        /// </summary>
        public int SelectedSemesterId { get; set; }
    }
}