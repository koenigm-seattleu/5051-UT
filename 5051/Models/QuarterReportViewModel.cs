using System.Collections.Generic;
using System.Web.Mvc;

namespace _5051.Models
{
    /// <summary>
    /// Quarter report view model, for generating quarter report view
    /// </summary>
    public class QuarterReportViewModel: BaseReportViewModel
    {
        /// <summary>
        /// Contains a select list for quarter selection dropdown
        /// </summary>
        public List<SelectListItem> Quarters { get; set; }

        /// <summary>
        /// The selected quarter's id for quarter selection dropdown
        /// </summary>
        public int SelectedQuarterId { get; set; }
    }
}