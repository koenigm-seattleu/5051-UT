using System;
using System.Collections.Generic;

namespace _5051.Models
{
    /// <summary>
    /// The base report view model, all types of report view models(weekly, monthly, semester, school year) inherit this class
    /// Contains info about who the student is, start date and end date, a list of view models of individual day's attendance, and overall stats of the report.
    /// </summary>
    public class BaseReportViewModel
    {
        /// <summary>
        /// The Student id
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// The Student record
        /// </summary>
        public StudentModel Student { get; set; }

        /// <summary>
        /// Date Start 
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Date end 
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// The attendance Record for each date to show on the report
        /// </summary>
        public List<AttendanceReportViewModel> AttendanceList = new List<AttendanceReportViewModel>();

        /// <summary>
        /// The Statistics for this report
        /// </summary>
        public StudentReportStatsModel Stats = new StudentReportStatsModel();

        /// <summary>
        /// The goal percentage for total attended time
        /// </summary>
        public int Goal { get; set; }

        /// <summary>
        /// Hold the string representation of all year values of dates. Use this string in javascript to generate charts.
        /// </summary>
        public string YearArray { get; set; }

        /// <summary>
        /// Hold the string representation of all months values of dates. Use this string in javascript to generate charts.
        /// </summary>
        public string MonthArray { get; set; }

        /// <summary>
        /// Hold the string representation of all day values of dates. Use this string in javascript to generate charts.
        /// </summary>
        public string DayArray { get; set; }

        /// <summary>
        /// Hold the string representation of all perfect attendance values of dates. Use this string in javascript to generate charts.
        /// </summary>
        public string PerfectValues { get; set; }

        /// <summary>
        /// Hold the string representation of all goal attendance values of dates. Use this string in javascript to generate charts.
        /// </summary>
        public string GoalValues { get; set; }

        /// <summary>
        /// Hold the string representation of all actual attendance values of dates. Use this string in javascript to generate charts.
        /// </summary>
        public string ActualValues { get; set; }

        /// <summary>
        /// Hold the string representation of all emotion level values values of dates. Use this string in javascript to generate charts.
        /// </summary>
        public string EmotionLevelValues { get; set; }

    }
}
