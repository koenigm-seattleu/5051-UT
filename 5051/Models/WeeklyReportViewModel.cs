using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Models
{
    /// <summary>
    /// Weekly report view model, for generating Weekly report view
    /// </summary>
    public class WeeklyReportViewModel : BaseReportViewModel
    {
        /// <summary>
        /// Contains a select list for week selection dropdown
        /// </summary>
        public List<SelectListItem> Weeks { get; set; }

        /// <summary>
        /// The selected week's id for week selection dropdown
        /// </summary>
        public int SelectedWeekId { get; set; }
    }
}