using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;
using _5051.Models.Enums;

namespace _5051.Backend
{
    /// <summary>
    /// SchoolCalendarBackend helper functions
    /// </summary>
    public static class SchoolCalendarBackendHelper
    {
        /// <summary>
        /// Set the given calendar model to default
        /// </summary>
        /// <param name="calendar"></param>
        public static SchoolCalendarModel SetDefault(SchoolCalendarModel calendar)
        {
            if (calendar == null)
            {
                return null;
            }

            // Clear modified flag on date
            calendar.Modified = false;

            calendar = SetDefaultTypes(calendar);
            calendar = SetSchoolTime(calendar);

            return calendar;
        }

        /// <summary>
        /// Set start and end type using default dismissal settings
        /// </summary>
        /// <param name="date"></param>
        public static SchoolCalendarModel SetDefaultTypes(SchoolCalendarModel calendar)
        {
            if (calendar == null)
            {
                return null;
            }

            var dismissalSettings = SchoolDismissalSettingsBackend.Instance.GetDefault();
            var myDate = calendar.Date.DayOfWeek;
            switch (myDate)
            {
                case DayOfWeek.Monday:
                    calendar.DayStart = dismissalSettings.MonStartType;
                    calendar.DayEnd = dismissalSettings.MonEndType;
                    break;
                case DayOfWeek.Tuesday:
                    calendar.DayStart = dismissalSettings.TueStartType;
                    calendar.DayEnd = dismissalSettings.TueEndType;
                    break;
                case DayOfWeek.Wednesday:
                    calendar.DayStart = dismissalSettings.WedStartType;
                    calendar.DayEnd = dismissalSettings.WedEndType;
                    break;
                case DayOfWeek.Thursday:
                    calendar.DayStart = dismissalSettings.ThuStartType;
                    calendar.DayEnd = dismissalSettings.ThuEndType;
                    break;
                case DayOfWeek.Friday:
                    calendar.DayStart = dismissalSettings.FriStartType;
                    calendar.DayEnd = dismissalSettings.FriEndType;
                    break;
                case DayOfWeek.Saturday:
                    calendar.DayStart = dismissalSettings.SatStartType;
                    calendar.DayEnd = dismissalSettings.SatEndType;
                    break;
                case DayOfWeek.Sunday:
                    calendar.DayStart = dismissalSettings.SunStartType;
                    calendar.DayEnd = dismissalSettings.SunEndType;
                    break;
            }

            return calendar;
        }

        /// <summary>
        /// Set start and end time for the given calendar model, then calculate duration
        /// </summary>
        /// <param name="calendar"></param>
        public static SchoolCalendarModel SetSchoolTime(SchoolCalendarModel calendar)
        {
            if (calendar == null)
            {
                return null;
            }

            var startType = calendar.DayStart;
            var endType = calendar.DayEnd;

            //if not a school day
            if (startType == SchoolCalendarDismissalEnum.Unknown || endType == SchoolCalendarDismissalEnum.Unknown)
            {
                calendar.SchoolDay = false;
                calendar.TimeStart = SchoolDismissalSettingsBackend.Instance.GetDefault().StartNormal;
                calendar.TimeEnd = SchoolDismissalSettingsBackend.Instance.GetDefault().EndNormal;
                calendar.TimeDuration = TimeSpan.Zero;

                return calendar;
            }

            calendar.SchoolDay = true;
            switch (calendar.DayStart)
            {
                case SchoolCalendarDismissalEnum.Early:
                    calendar.TimeStart = SchoolDismissalSettingsBackend.Instance.GetDefault().StartEarly;
                    break;
                case SchoolCalendarDismissalEnum.Late:
                    calendar.TimeStart = SchoolDismissalSettingsBackend.Instance.GetDefault().StartLate;
                    break;
                default:
                    calendar.TimeStart = SchoolDismissalSettingsBackend.Instance.GetDefault().StartNormal;
                    break;
            }

            switch (calendar.DayEnd)
            {
                case SchoolCalendarDismissalEnum.Early:
                    calendar.TimeEnd = SchoolDismissalSettingsBackend.Instance.GetDefault().EndEarly;
                    break;
                case SchoolCalendarDismissalEnum.Late:
                    calendar.TimeEnd = SchoolDismissalSettingsBackend.Instance.GetDefault().EndLate;
                    break;
                default:
                    calendar.TimeEnd = SchoolDismissalSettingsBackend.Instance.GetDefault().EndNormal;
                    break;
            }

            calendar.TimeDuration = calendar.TimeEnd.Subtract(calendar.TimeStart);

            return calendar;
        }
    }
}