using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;
using _5051.Models.Enums;

namespace _5051.Models
{
    /// <summary>
    /// Setting model to track the school Times for Start and End
    /// The Amin should be allowed to change these
    /// Save as defaults to the Database
    /// </summary>
    public class SchoolDismissalSettingsModel
    {
        // Used to access the settings instance, even if there is just one
        public string Id { get; set; }

        // The Normal Start time.  8:55am
        [Display(Name = "Start Normal", Description = "Time Class Usualy Starts")]
        public TimeSpan StartNormal { get; set; }

        // The Early Start time.  8:30am
        [Display(Name = "Start Early", Description = "Time Class Starts Early")]
        public TimeSpan StartEarly { get; set; }

        // The Late Start Time 10:55am (snow start)
        [Display(Name = "Start Late", Description = "Late Start Time")]
        public TimeSpan StartLate { get; set; }

        // The Normal end time 3:45am
        [Display(Name = "End Normal", Description = "Time Class Usualy Ends")]
        public TimeSpan EndNormal { get; set; }

        // The Early dismissal (wed 2pm)
        [Display(Name = "End Early", Description = "Time Class Ends Early")]
        public TimeSpan EndEarly { get; set; }

        // The Late Dismissal 4pm
        [Display(Name = "End Late", Description = "Time Class Ends if Late End")]
        public TimeSpan EndLate { get; set; }

        // Day start time drop down list: normal, early, late
        public List<SelectListItem> DayStartDropDown { get; set; }

        // Day end time drop down list: normal, early, late
        public List<SelectListItem> DayEndDropDown { get; set; }

        //Selected start time and end time type id for each day of week.
        public int MonStartSelected { get; set; }        
        public int MonEndSelected { get; set; }

        public int TueStartSelected { get; set; }
        public int TueEndSelected { get; set; }

        public int WedStartSelected { get; set; }
        public int WedEndSelected { get; set; }

        public int ThuStartSelected { get; set; }
        public int ThuEndSelected { get; set; }

        public int FriStartSelected { get; set; }
        public int FriEndSelected { get; set; }

        public int SatStartSelected { get; set; }
        public int SatEndSelected { get; set; }

        public int SunStartSelected { get; set; }
        public int SunEndSelected { get; set; }

        public SchoolCalendarDismissalEnum MonStartType { get; set; }
        public SchoolCalendarDismissalEnum MonEndType { get; set; }
        public SchoolCalendarDismissalEnum TueStartType { get; set; }
        public SchoolCalendarDismissalEnum TueEndType { get; set; }
        public SchoolCalendarDismissalEnum WedStartType { get; set; }
        public SchoolCalendarDismissalEnum WedEndType { get; set; }
        public SchoolCalendarDismissalEnum ThuStartType { get; set; }
        public SchoolCalendarDismissalEnum ThuEndType { get; set; }
        public SchoolCalendarDismissalEnum FriStartType { get; set; }
        public SchoolCalendarDismissalEnum FriEndType { get; set; }
        public SchoolCalendarDismissalEnum SatStartType { get; set; }
        public SchoolCalendarDismissalEnum SatEndType { get; set; }
        public SchoolCalendarDismissalEnum SunStartType { get; set; }
        public SchoolCalendarDismissalEnum SunEndType { get; set; }


        // First day of school
        [Display(Name = "First Day", Description = "First Day of School")]
        public DateTime DayFirst { get; set; }

        // Last day of school
        [Display(Name = "Last Day", Description = "Last Day of School")]
        public DateTime DayLast { get; set; }


        [Display(Name = "Semester 1 First Class Day", Description = "Semester 1 First Class Day")]
        public DateTime S1Start { get; set; }

        [Display(Name = "Semester 2 First Class Day", Description = "Semester 2 First Class Day")]
        public DateTime S2Start { get; set; }

        [Display(Name = "Quarter 1 First Class Day", Description = "Quarter 1 First Class Day")]
        public DateTime Q1Start { get; set; }

        [Display(Name = "Quarter 2 First Class Day", Description = "Quarter 2 First Class Day")]
        public DateTime Q2Start { get; set; }

        [Display(Name = "Quarter 3 First Class Day", Description = "Quarter 3 First Class Day")]
        public DateTime Q3Start { get; set; }

        [Display(Name = "Quarter 4 First Class Day", Description = "Quarter 4 First Class Day")]
        public DateTime Q4Start { get; set; }


        // The current setting for goal percentage
        [Display(Name = "Attendance Goal", Description = "Attendance Goal Percentage")]
        public int Goal { get; set; }

        // The period of time before school during which time attended by student is counted into attendance duration
        // while time attended before this window starts is not counted into attendance duration
        [Display(Name = "Early Window", Description = "The period of time before school during which time attended by student is counted into attendance duration")]
        public TimeSpan EarlyWindow { get; set; }

        // The period of time after school during which time attended by student is counted into attendance duration
        // while time attended after this window ends is not counted into attendance duration
        [Display(Name = "Late Window", Description = "The period of time after school during which time attended by student is counted into attendance duration")]
        public TimeSpan LateWindow { get; set; }

        public SchoolDismissalSettingsModel()
        {
            Initialize();
        }

        /// <summary>
        /// Create a copy of the item, used for updates.
        /// </summary>
        /// <param name="data"></param>
        public SchoolDismissalSettingsModel(SchoolDismissalSettingsModel data)
        {
            Initialize();
            if (data == null)
            {
                return;
            }

            StartNormal = data.StartNormal;
            StartEarly = data.StartEarly;
            StartLate = data.StartLate;
            EndNormal = data.EndNormal;
            EndEarly = data.EndEarly;
            EndLate = data.EndLate;

            DayStartDropDown = data.DayStartDropDown;
            DayEndDropDown = data.DayEndDropDown;

            MonStartSelected = data.MonStartSelected;
            MonEndSelected = data.MonEndSelected;
            TueStartSelected = data.TueStartSelected;
            TueEndSelected = data.TueEndSelected;
            WedStartSelected = data.WedStartSelected;
            WedEndSelected = data.WedEndSelected;
            ThuStartSelected = data.ThuStartSelected;
            ThuEndSelected = data.ThuEndSelected;
            FriStartSelected = data.FriStartSelected;
            FriEndSelected = data.FriEndSelected;
            SatStartSelected = data.SatStartSelected;
            SatEndSelected = data.SatEndSelected;
            SunStartSelected = data.SunStartSelected;
            SunEndSelected = data.SunEndSelected;

            MonStartType = (SchoolCalendarDismissalEnum)MonStartSelected;
            MonEndType = (SchoolCalendarDismissalEnum)MonEndSelected;
            TueStartType = (SchoolCalendarDismissalEnum)TueStartSelected;
            TueEndType = (SchoolCalendarDismissalEnum)TueEndSelected;
            WedStartType = (SchoolCalendarDismissalEnum)WedStartSelected;
            WedEndType = (SchoolCalendarDismissalEnum)WedEndSelected;
            ThuStartType = (SchoolCalendarDismissalEnum)ThuStartSelected;
            ThuEndType = (SchoolCalendarDismissalEnum)ThuEndSelected;
            FriStartType = (SchoolCalendarDismissalEnum)FriStartSelected;
            FriEndType = (SchoolCalendarDismissalEnum)FriEndSelected;
            SatStartType = (SchoolCalendarDismissalEnum)SatStartSelected;
            SatEndType = (SchoolCalendarDismissalEnum)SatEndSelected;
            SunStartType = (SchoolCalendarDismissalEnum)SunStartSelected;
            SunEndType = (SchoolCalendarDismissalEnum)SunEndSelected;

            DayFirst = data.DayFirst;
            DayLast = data.DayLast;

            S1Start = data.S1Start;
            S2Start = data.S2Start;

            Q1Start = data.Q1Start;
            Q2Start = data.Q2Start;
            Q3Start = data.Q3Start;
            Q4Start = data.Q4Start;

            Goal = data.Goal;
            EarlyWindow = data.EarlyWindow;
            LateWindow = data.LateWindow;
        }

        /// <summary>
        /// Create the default values
        /// </summary>
        private void Initialize()
        {
            SetDefault();
        }

        /// <summary>
        /// Sets the default values for the Item
        /// Because it is set here, there is no need to set defaults over in the Mock, call this instead
        /// </summary>
        public void SetDefault()
        {
            Id = Guid.NewGuid().ToString();
            StartNormal = TimeSpan.Parse("8:55");
            StartEarly = TimeSpan.Parse("8:00");
            StartLate = TimeSpan.Parse("10:55");

            EndNormal = TimeSpan.Parse("15:45");
            EndEarly = TimeSpan.Parse("14:00");
            EndLate = TimeSpan.Parse("16:00");

            DayStartDropDown = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "No School" },
                new SelectListItem { Value = "1", Text = "Start Normal" },
                new SelectListItem { Value = "2", Text = "Start Early" },
                new SelectListItem { Value = "3", Text = "Start Late" }
            };

            DayEndDropDown = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "No School" },
                new SelectListItem { Value = "1", Text = "End Normal" },
                new SelectListItem { Value = "2", Text = "End Early" },
                new SelectListItem { Value = "3", Text = "End Late" }
            };

            MonStartSelected = 1;
            MonEndSelected = 1;
            TueStartSelected = 1;
            TueEndSelected = 1;
            WedStartSelected = 1;
            WedEndSelected = 2;
            ThuStartSelected = 1;
            ThuEndSelected = 1;
            FriStartSelected = 3;
            FriEndSelected = 1;
            SatStartSelected = 0;
            SatEndSelected = 0;
            SunStartSelected =0;
            SunEndSelected = 0;

            MonStartType = SchoolCalendarDismissalEnum.Normal;
            MonEndType = SchoolCalendarDismissalEnum.Normal;
            TueStartType = SchoolCalendarDismissalEnum.Normal;
            TueEndType = SchoolCalendarDismissalEnum.Normal;
            WedStartType = SchoolCalendarDismissalEnum.Normal;
            WedEndType = SchoolCalendarDismissalEnum.Early;
            ThuStartType = SchoolCalendarDismissalEnum.Normal;
            ThuEndType = SchoolCalendarDismissalEnum.Normal;
            FriStartType = SchoolCalendarDismissalEnum.Late;
            FriEndType = SchoolCalendarDismissalEnum.Normal;
            SatStartType = SchoolCalendarDismissalEnum.Unknown;
            SatEndType = SchoolCalendarDismissalEnum.Unknown;
            SunStartType = SchoolCalendarDismissalEnum.Unknown;
            SunEndType = SchoolCalendarDismissalEnum.Unknown;

            //School year settings
            DayFirst = DateTime.Parse("09/05/2018");
            DayLast = DateTime.Parse("06/20/2019");

            S1Start = DateTime.Parse("09/05/2018");
            S2Start = DateTime.Parse("01/30/2019");

            Q1Start = DateTime.Parse("09/05/2018");
            Q2Start = DateTime.Parse("11/08/2018");
            Q3Start = DateTime.Parse("01/30/2019");
            Q4Start = DateTime.Parse("04/18/2019");


            //Report settings
            Goal = 85;
            EarlyWindow = new TimeSpan(0, 30, 0);
            LateWindow = new TimeSpan(0, 30, 0);
        }

        /// <summary>
        /// Used to Update Before doing a data save
        /// Updates everything except for the ID
        /// </summary>
        /// <param name="data">Data to update</param>
        public void Update(SchoolDismissalSettingsModel data)
        {
            if (data == null)
            {
                return;
            }

            StartNormal = data.StartNormal;
            StartEarly = data.StartEarly;
            StartLate = data.StartLate;
            EndNormal = data.EndNormal;
            EndEarly = data.EndEarly;
            EndLate = data.EndLate;

            DayStartDropDown = data.DayStartDropDown;
            DayEndDropDown = data.DayEndDropDown;

            MonStartSelected = data.MonStartSelected;
            MonEndSelected = data.MonEndSelected;
            TueStartSelected = data.TueStartSelected;
            TueEndSelected = data.TueEndSelected;
            WedStartSelected = data.WedStartSelected;
            WedEndSelected = data.WedEndSelected;
            ThuStartSelected = data.ThuStartSelected;
            ThuEndSelected = data.ThuEndSelected;
            FriStartSelected = data.FriStartSelected;
            FriEndSelected = data.FriEndSelected;
            SatStartSelected = data.SatStartSelected;
            SatEndSelected = data.SatEndSelected;
            SunStartSelected = data.SunStartSelected;
            SunEndSelected = data.SunEndSelected;

            MonStartType = (SchoolCalendarDismissalEnum)MonStartSelected;
            MonEndType = (SchoolCalendarDismissalEnum)MonEndSelected;
            TueStartType = (SchoolCalendarDismissalEnum)TueStartSelected;
            TueEndType = (SchoolCalendarDismissalEnum)TueEndSelected;
            WedStartType = (SchoolCalendarDismissalEnum)WedStartSelected;
            WedEndType = (SchoolCalendarDismissalEnum)WedEndSelected;
            ThuStartType = (SchoolCalendarDismissalEnum)ThuStartSelected;
            ThuEndType = (SchoolCalendarDismissalEnum)ThuEndSelected;
            FriStartType = (SchoolCalendarDismissalEnum)FriStartSelected;
            FriEndType = (SchoolCalendarDismissalEnum)FriEndSelected;
            SatStartType = (SchoolCalendarDismissalEnum)SatStartSelected;
            SatEndType = (SchoolCalendarDismissalEnum)SatEndSelected;
            SunStartType = (SchoolCalendarDismissalEnum)SunStartSelected;
            SunEndType = (SchoolCalendarDismissalEnum)SunEndSelected;

            DayFirst = data.DayFirst;
            DayLast = data.DayLast;

            S1Start = data.S1Start;
            S2Start = data.S2Start;

            Q1Start = data.Q1Start;
            Q2Start = data.Q2Start;
            Q3Start = data.Q3Start;
            Q4Start = data.Q4Start;

            Goal = data.Goal;
            EarlyWindow = data.EarlyWindow;
            LateWindow = data.LateWindow;
        }
    }
}
