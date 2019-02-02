using System;
using System.ComponentModel.DataAnnotations;
using _5051.Backend;
using _5051.Models.Enums;

namespace _5051.Models
{
    /// <summary>
    /// Track the School Calendar
    /// </summary>
    public class SchoolCalendarModel
    {
        /// <summary>
        /// The Id for the Record
        /// </summary>
        [Display(Name = "Id", Description = "Day Id")]
        public string Id { get; set; }

        /// <summary>
        /// The Date including year
        /// </summary>
        [Display(Name = "Date", Description = "Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// The number of hours and minutes in the school day
        /// </summary>
        [Display(Name = "Max Time", Description = "Hours for the Day")]
        public TimeSpan TimeDuration { get; set; }

        /// <summary>
        /// The start of school time
        /// </summary>
        [Display(Name = "Start Time", Description = "Start Time")]
        public TimeSpan TimeStart { get; set; }

        /// <summary>
        /// The End of school time  (full day, or part time)
        /// </summary>
        [Display(Name = "End Time", Description = "End Time")]
        public TimeSpan TimeEnd { get; set; }

        /// <summary>
        /// School day Starts normal, early, or late (snow day)
        /// </summary>
        [Display(Name = "Start Type", Description = "Type of Start")]
        public SchoolCalendarDismissalEnum DayStart { get; set; }

        /// <summary>
        /// School day Ends normal, early (wed early dismissal), or late
        /// </summary>
        [Display(Name = "End Type", Description = "Type of End")]
        public SchoolCalendarDismissalEnum DayEnd { get; set; }

        /// <summary>
        /// Set to true if this record is modified from the default
        /// </summary>
        [Display(Name = "Modified", Description = "Modified Record")]
        public bool Modified { get; set; }

        /// <summary>
        /// Set to true if the day is a school day
        /// </summary>
        [Display(Name = "School Day", Description = "School Day")]
        public bool SchoolDay { get; set; }

        /// <summary>
        /// Set to true if the day has attendance by any student.
        /// </summary>
        [Display(Name = "Has Attendance", Description = "Has Attendance")]
        public bool HasAttendance { get; set; }

        /// <summary>
        /// Create the default values
        /// </summary>
        public void Initialize(DateTime date)
        {
            Id = Guid.NewGuid().ToString();
            Modified = false;
            SchoolDay = true;
            HasAttendance = false;
            Date = date;

            //SetDefault();
            //SetSchoolDay();
        }

        /// <summary>
        /// New SchoolCalendar, for now
        /// </summary>
        public SchoolCalendarModel()
        {
            Initialize(DateTimeHelper.Instance.GetDateTimeNowUTC());
        }

        /// <summary>
        /// New SchoolCalendar, based on a date
        /// </summary>
        public SchoolCalendarModel(DateTime date)
        {
            Initialize(date);
        }

        ///// <summary>
        ///// Sets the Default Values 
        ///// </summary>
        //public void SetDefault()
        //{
        //    // Date = DateTimeHelper.Instance.GetDateTimeNowUTC();  // Date is not set in the default.  
        //    DayEnd = SchoolCalendarDismissalEnum.Normal;
        //    DayStart = SchoolCalendarDismissalEnum.Normal;
        //    SetSchoolDay();
        //    SetSchoolTime();

        //    // If not school day reset times
        //    SetNoSchoolDayTimes();
        //}

        ///// <summary>
        ///// Sets if the school should be in sesson or not based on the school rules
        ///// </summary>
        //public void SetSchoolDay()
        //{
        //    // todo, change this out to be a structure on the settings for each day.  Show if the school is in session, or start, end different
        //    // Then walk each of the days of the week and apply those settings.
        //    switch (Date.DayOfWeek)
        //    {
        //        case DayOfWeek.Saturday:
        //            SchoolDay = false;
        //            break;

        //        case DayOfWeek.Sunday:
        //            SchoolDay = false;
        //            break;

        //        case DayOfWeek.Wednesday:
        //            DayEnd = SchoolCalendarDismissalEnum.Early;
        //            break;
        //    }
        //}

        ///// <summary>
        ///// Based on the day of the school, decide the start and stop times
        ///// </summary>
        //public void SetSchoolTime()
        //{
            
        //    switch (DayStart)
        //    {
        //        case SchoolCalendarDismissalEnum.Early:
        //            TimeStart = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().StartEarly;
        //            break;
        //        case SchoolCalendarDismissalEnum.Late:
        //            TimeStart = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().StartLate;
        //            break;
        //        case SchoolCalendarDismissalEnum.Normal:
        //            TimeStart = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().StartNormal;
        //            break;
        //    }

        //    switch (DayEnd)
        //    {
        //        case SchoolCalendarDismissalEnum.Early:
        //            TimeEnd = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().EndEarly;
        //            break;
        //        case SchoolCalendarDismissalEnum.Late:
        //            TimeEnd = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().EndLate;
        //            break;
        //        case SchoolCalendarDismissalEnum.Normal:
        //            TimeEnd = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().EndNormal;
        //            break;
        //    }

        //    TimeDuration = TimeEnd.Subtract(TimeStart);
        //}

        ///// <summary>
        ///// No School day, so turn it off, and zero out the times.
        ///// </summary>
        //public void SetNoSchoolDayTimes()
        //{
        //    // If there is no school, then set everything to zero
        //    if (SchoolDay == false)
        //    {
        //        //TimeEnd = TimeSpan.Zero;
        //        //TimeStart = TimeSpan.Zero;
        //        TimeDuration = TimeSpan.Zero;
        //    }
        //}

        /// <summary>
        /// Make an SchoolCalendar from values passed in
        /// </summary>
        /// <param name="data">a valid model</param>
        public SchoolCalendarModel(SchoolCalendarModel data)
        {
            if (data == null)
            {
                // If the data is null, just return a regular initialized one
                return;
            }

            Initialize(data.Date);

            Date = data.Date;
            TimeStart = data.TimeStart;
            TimeEnd = data.TimeEnd;
            DayStart = data.DayStart;
            DayEnd = data.DayEnd;
            Modified = data.Modified;
            SchoolDay = data.SchoolDay;
            HasAttendance = data.HasAttendance;

            TimeDuration = data.TimeEnd.Subtract(TimeStart);
        }

        /// <summary>
        /// Used to Update Before doing a data save
        /// Updates everything except for the ID
        /// </summary>
        /// <param name="data">Data to update</param>
        public void Update(SchoolCalendarModel data)
        {
            if (data == null)
            {
                return;
            }

            Date = data.Date;
            TimeStart = data.TimeStart;
            TimeEnd = data.TimeEnd;
            DayStart = data.DayStart;
            DayEnd = data.DayEnd;
            Modified = data.Modified;
            SchoolDay = data.SchoolDay;
            HasAttendance = data.HasAttendance;

            if (SchoolDay)
            {
                // The time in school is the delta of end - start
                TimeDuration = data.TimeEnd.Subtract(TimeStart);
            }
            else
            {
                TimeDuration = TimeSpan.Zero;
            }


            //// Check to make sure not set as early or late, if the time time is changed
            //// This solves the issue of having an item be set as early dismissal, and then the time changed to say 14:01, but it still shows as early dismissal.
            //var myDefault = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault();

            //if (DayStart == SchoolCalendarDismissalEnum.Early && TimeStart != myDefault.StartEarly)
            //{
            //    DayStart = SchoolCalendarDismissalEnum.Normal;
            //}

            //if (DayStart == SchoolCalendarDismissalEnum.Late && TimeStart != myDefault.StartLate)
            //{
            //    DayStart = SchoolCalendarDismissalEnum.Normal;
            //}

            //if (DayEnd == SchoolCalendarDismissalEnum.Early && TimeEnd != myDefault.EndEarly)
            //{
            //    DayEnd = SchoolCalendarDismissalEnum.Normal;
            //}

            //if (DayEnd == SchoolCalendarDismissalEnum.Late && TimeEnd != myDefault.EndLate)
            //{
            //    DayEnd = SchoolCalendarDismissalEnum.Normal;
            //}


            //// If not a school day, then no time in class.
            //SetNoSchoolDayTimes();
        }
    }
}