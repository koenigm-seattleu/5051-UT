using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    public class StudentReportStatsModel
    {
        /// <summary>
        /// The number of school days
        /// </summary>
        public int NumOfSchoolDays { get; set; }
        /// <summary>
        /// The single value for the total hours from school start till now attended
        /// </summary>
        public TimeSpan AccumlatedTotalHours { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// The single value for the total hours from school start till now expected to attend
        /// </summary>
        public TimeSpan AccumlatedTotalHoursExpected { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// The number of days present
        /// </summary>
        public int DaysPresent { get; set; }

        /// <summary>
        /// The number of days AbsentExcused
        /// </summary>
        public int DaysAbsentExcused { get; set; }

        /// <summary>
        /// The number of days AbsentUnexcused
        /// </summary>
        public int DaysAbsentUnexcused { get; set; }

        /// <summary>
        /// The number of days on time
        /// </summary>
        public int DaysOnTime { get; set; }

        /// <summary>
        /// The number of days late
        /// </summary>
        public int DaysLate { get; set; }

        /// <summary>
        /// The number of days check out auto
        /// </summary>
        public int DaysOutAuto { get; set; }

        /// <summary>
        /// The number of days check out early
        /// </summary>
        public int DaysOutEarly { get; set; }

        /// <summary>
        /// The percentage of days present: DaysPresent/NumOfSchoolDays
        /// </summary>
        public int PercPresent { get; set; }

        /// <summary>
        /// The percentage of attended hours: AccumlatedTotalHours/AccumlatedTotalHoursExpected
        /// </summary>
        public int PercAttendedHours { get; set; }

        /// <summary>
        /// The percentage of days AbsentExcused: DaysAbsentExcused/NumOfSchoolDays
        /// </summary>
        public int PercExcused { get; set; }

        /// <summary>
        /// The percentage of days AbsentUnexcused: DaysAbsentExcused/NumOfSchoolDays
        /// </summary>
        public int PercUnexcused { get; set; }

        /// <summary>
        /// The percentage of days in late: DaysLate/NumOfSchoolDays
        /// </summary>
        public int PercInLate { get; set; }

        /// <summary>
        /// The percentage of days out early: DaysOutEarly/NumOfSchoolDays
        /// </summary>
        public int PercOutEarly { get; set; }
    }
}