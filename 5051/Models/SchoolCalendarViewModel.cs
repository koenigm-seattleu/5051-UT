using _5051.Backend;
using System;
using System.Collections.Generic;

namespace _5051.Models
{
    /// <summary>
    /// Track the School Calendar
    /// </summary>
    public class SchoolCalendarViewModel
    {
        /// <summary>
        /// List of each school day
        /// </summary>
        public List<SchoolCalendarModel> SchoolDays;

        /// <summary>
        /// The First Day of School
        /// </summary>
        public DateTime FirstDay = new DateTime();

        /// <summary>
        /// The Last day of school
        /// </summary>
        public DateTime LastDay = new DateTime();

        /// <summary>
        /// Today
        /// </summary>
        public DateTime CurrentDate = DateTimeHelper.Instance.GetDateTimeNowUTC();

        /// <summary>
        /// A List of Months and the Days per Month
        /// </summary>
        public List<List<SchoolCalendarModel>> Months = new List<List<SchoolCalendarModel>>();

    }
}