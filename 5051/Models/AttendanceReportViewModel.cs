using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models.Enums;

namespace _5051.Models
{
    /// <summary>
    /// View model for an individual day on a report. Contains info about the date, is school day or not, time in and out, and other stats for generating the report
    /// </summary>
    public class AttendanceReportViewModel
    {
        /// <summary>
        /// The Date, in kiosk time zone
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Is this a school day?
        /// </summary>
        public bool IsSchoolDay { get; set; } = true;

        /// <summary>
        /// Check-in time, in kiosk time zone
        /// </summary>
        public DateTime TimeIn { get; set; }

        /// <summary>
        /// Check-out time, in kiosk time zone
        /// </summary>
        public DateTime TimeOut { get; set; }

        /// <summary>
        /// Total Hours attended from the start of school till now
        /// </summary>
        public TimeSpan TotalHours { get; set; }

        /// <summary>
        /// Total hours Expected to attend at this point in time
        /// </summary>
        public TimeSpan TotalHoursExpected { get; set; }

        /// <summary>
        /// Hours attended Today
        /// </summary>
        public TimeSpan HoursAttended { get; set; }

        /// <summary>
        /// Hours Expected to Attend Today
        /// </summary>
        public TimeSpan HoursExpected { get; set; }


        /// <summary>
        /// HoursAttended divides HoursExpected
        /// </summary>
        public int PercentAttended { get; set; }

        /// <summary>
        /// If the attendance excused etc.
        /// </summary>
        public AttendanceStatusEnum AttendanceStatus { get; set; }

        /// <summary>
        /// The status of the checkin
        /// </summary>
        public CheckInStatusEnum CheckInStatus { get; set; }

        /// <summary>
        ///  The Status of the Checkout
        /// </summary>
        public CheckOutStatusEnum CheckOutStatus { get; set; }

        /// <summary>
        ///  The emotion state
        /// </summary>
        public EmotionStatusEnum Emotion { get; set; }

        /// <summary>
        /// The image uri
        /// </summary>
        public string EmotionUri { get; set; }

        /// <summary>
        /// The current emotion level
        /// </summary>
        public int EmotionLevel { get; set; }
    }
}