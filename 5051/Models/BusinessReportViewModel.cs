using System.Collections.Generic;
using System.Web.Mvc;

namespace _5051.Models
{
    /// <summary>
    /// View Model for the GameResult Views to have the list of GameResults
    /// </summary>
    public class BusinessReportViewModel : BaseReportViewModel
    {
        /// <summary>
        /// The Report Parts
        /// </summary>
        public int income;
        public int outcome;
        public List<TransactionModel> BusinessList;

    }
}