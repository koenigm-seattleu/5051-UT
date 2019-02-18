using System;
using System.Linq;

using _5051.Models.Enums;
using _5051.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace _5051.Backend
{
    /// <summary>
    /// Manages the Reports for Student and Admin
    /// </summary>

    public class ReportBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile ReportBackend instance;
        private static readonly object syncRoot = new Object();

        private ReportBackend() { }

        public static ReportBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ReportBackend();
                        }
                    }
                }

                return instance;
            }
        }


        #region GenerateWeeklyReportRegion
        /// <summary>
        /// Generate Weekly report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public WeeklyReportViewModel GenerateWeeklyReport(WeeklyReportViewModel report)
        {
            //set student
            report.Student = DataSourceBackend.Instance.StudentBackend.Read(report.StudentId);

            //to generate week selection drop down, make a list item for every week between first and last day of school
            var dayFirst = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayFirst.Date;
            var dayLast = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayLast.Date;

            var dayNow = UTCConversionsBackend.UtcToKioskTime(DateTimeHelper.Instance.GetDateTimeNowUTC()).Date;

            //The first valid week(Monday's date) for the dropdown
            var FirstWeek = dayFirst.AddDays(-((dayFirst.DayOfWeek - DayOfWeek.Monday + 7) % 7)); //added this mod operation to make sure it's the previous monday not the next monday
            //The last valid month for the dropdown
            var LastWeek = dayLast.AddDays(-((dayLast.DayOfWeek - DayOfWeek.Monday + 7) % 7));
            //The month of today
            var WeekNow = dayNow.AddDays(-((dayNow.DayOfWeek - DayOfWeek.Monday + 7) % 7)); //if today is sunday, dayNow.DayOfWeek - DayOfWeek.Monday = -1

            //do not go beyond the week of today
            if (LastWeek > WeekNow)
            {
                LastWeek = WeekNow;
            }

            //Set the current week (loop variable) to the last valid week
            var currentWeek = LastWeek;


            //initialize the dropdownlist
            report.Weeks = new List<SelectListItem>();

            // the week id
            int weekId = 1;

            //loop backwards in time so that the week select list items are in time reversed order
            while (currentWeek >= FirstWeek)
            {
                //the friday's date of the current week
                var currentWeekFriday = currentWeek.AddDays(4);

                //make a list item for the current week
                var week = new SelectListItem { Value = "" + weekId, Text = "" + currentWeek.ToShortDateString() + " to " + currentWeekFriday.ToShortDateString() };

                //add to the select list
                report.Weeks.Add(week);

                //if current week is the selected month, set the start date and end date for this report
                if (weekId == report.SelectedWeekId)
                {
                    //set start date and end date
                    report.DateStart = currentWeek;
                    report.DateEnd = currentWeekFriday;
                }

                weekId++;
                currentWeek = currentWeek.AddDays(-7);

            }


            return report;
        }


        #endregion
        #region GenerateMonthlyReportRegion
        /// <summary>
        /// Generate monthly report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public MonthlyReportViewModel GenerateMonthlyReport(MonthlyReportViewModel report)
        {
            //set student
            report.Student = DataSourceBackend.Instance.StudentBackend.Read(report.StudentId);

            //to generate month selection drop down, make a list item for every month between first and last day of school
            var dayFirst = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayFirst.Date;
            var dayLast = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayLast.Date;

            var dayNow = UTCConversionsBackend.UtcToKioskTime(DateTimeHelper.Instance.GetDateTimeNowUTC()).Date;

            //The first valid month for the dropdown
            var monthFirst = new DateTime(dayFirst.Year, dayFirst.Month, 1);
            //The last valid month for the dropdown
            var monthLast = new DateTime(dayLast.Year, dayLast.Month, 1);
            //The month of today
            var monthNow = new DateTime(dayNow.Year, dayNow.Month, 1);

            //do not go beyond the month of today
            if (monthLast > monthNow)
            {
                monthLast = monthNow;
            }

            //Set the current month (loop variable) to the last valid month
            var currentMonth = monthLast;

            //initialize the dropdownlist
            report.Months = new List<SelectListItem>();

            // the month id
            int monthId = 1;

            //loop backwards in time so that the month select list items are in time reversed order
            while (currentMonth >= monthFirst)
            {
                //make a list item for the current month
                var month = new SelectListItem { Value = "" + monthId, Text = currentMonth.ToString("MMMM yyyy") };

                //add to the select list
                report.Months.Add(month);

                //if current month is the selected month, set the start date and end date for this report
                if (monthId == report.SelectedMonthId)
                {
                    //set start date and end date
                    report.DateStart = currentMonth;
                    report.DateEnd = new DateTime(currentMonth.Year, currentMonth.Month, DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month));
                }

                monthId++;
                currentMonth = currentMonth.AddMonths(-1);

            }

            return report;
        }
        #endregion
        #region GenerateSemesterReportRegion

        /// <summary>
        /// Generate Weekly report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public SemesterReportViewModel GenerateSemesterReport(SemesterReportViewModel report)
        {
            //set student
            report.Student = DataSourceBackend.Instance.StudentBackend.Read(report.StudentId);

            //settings
            var settings = SchoolDismissalSettingsBackend.Instance.GetDefault();

            //initialize the drop down list
            report.Semesters = new List<SelectListItem>();

            var s1 = new SelectListItem { Value = "1", Text = "Semester 1" };

            //add to the select list
            report.Semesters.Add(s1);

            if (report.SelectedSemesterId == 1)
            {
                //set the first and last day of fall semester according to school dismissal settings
                report.DateStart = settings.S1Start;
                report.DateEnd = settings.S2Start.AddDays(-1);
            }

            var s2 = new SelectListItem { Value = "2", Text = "Semester 2" };

            //add to the select list
            report.Semesters.Add(s2);

            if (report.SelectedSemesterId == 2)
            {
                //set the first and last day of fall semester according to school dismissal settings
                report.DateStart = settings.S2Start;
                report.DateEnd = settings.DayLast;
            }


            return report;
        }


        #endregion
        #region GenerateQuarterReportRegion
        /// <summary>
        /// Generate Weekly report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public QuarterReportViewModel GenerateQuarterReport(QuarterReportViewModel report)
        {
            //set student
            report.Student = DataSourceBackend.Instance.StudentBackend.Read(report.StudentId);

            //settings
            var settings = SchoolDismissalSettingsBackend.Instance.GetDefault();

            report.Quarters = new List<SelectListItem>();

            var q1 = new SelectListItem { Value = "1", Text = "Quarter 1" };

            //add to the select list
            report.Quarters.Add(q1);

            if (report.SelectedQuarterId == 1)
            {
                //set the first and last day of this quarter according to school dismissal settings
                report.DateStart = settings.Q1Start.Date;
                report.DateEnd = settings.Q2Start.Date.AddDays(-1);
            }

            var q2 = new SelectListItem { Value = "2", Text = "Quarter 2" };

            //add to the select list
            report.Quarters.Add(q2);

            if (report.SelectedQuarterId == 2)
            {
                //set the first and last day of this quarter according to school dismissal settings
                report.DateStart = settings.Q2Start.Date;
                report.DateEnd = settings.Q3Start.Date.AddDays(-1);
            }

            var q3 = new SelectListItem { Value = "3", Text = "Quarter 3" };

            //add to the select list
            report.Quarters.Add(q3);

            if (report.SelectedQuarterId == 3)
            {
                //set the first and last day of this quarter according to school dismissal settings
                report.DateStart = settings.Q3Start.Date;
                report.DateEnd = settings.Q4Start.Date.AddDays(-1);
            }

            var q4 = new SelectListItem { Value = "4", Text = "Quarter 4" };

            //add to the select list
            report.Quarters.Add(q4);

            if (report.SelectedQuarterId == 4)
            {
                //set the first and last day of this quarter according to school dismissal settings
                report.DateStart = settings.Q4Start.Date;
                report.DateEnd = settings.DayLast.Date;
            }


            return report;
        }
        #endregion
        #region GenerateOverallReportRegion
        /// <summary>
        /// Generate overall report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public BaseReportViewModel GenerateOverallReport(BaseReportViewModel report)
        {
            //set student
            report.Student = DataSourceBackend.Instance.StudentBackend.Read(report.StudentId);

            //set start date and end date according to school dismissal settings
            report.DateStart = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayFirst.Date;
            report.DateEnd = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayLast.Date;


            return report;
        }
        #endregion

        /// <summary>
        /// calculate non overlapping hours attended using given intervals
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns></returns>
        private TimeSpan CalculateHoursAttended(List<Interval> intervals, TimeSpan start, TimeSpan end)
        {
            TimeSpan result = TimeSpan.Zero;
            List<Interval> merged = mergeIntervals(intervals);
            foreach (var item in merged)
            {
                Interval trimmed = trimInterval(item, start, end);
                TimeSpan period = trimmed.end.Add(-trimmed.start);
                result = result.Add(period);
            }
            return result;
        }

        /// <summary>
        /// Trim the interval to be between start and end
        /// </summary>
        /// <param name="interv"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private Interval trimInterval(Interval interv, TimeSpan start, TimeSpan end)
        {
            if (interv.start < start)
            {
                interv.start = start;
            }
            if (interv.end > end)
            {
                interv.end = end;
            }
            return interv;
        }

        /// <summary>
        /// Merge given intervals into non-overlapping intervals
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns></returns>
        private List<Interval> mergeIntervals(List<Interval> intervals)
        {
            //sort the intervals by start time
            intervals.OrderBy(i => i.start);

            List<Interval> result = new List<Interval>();
            Interval last = intervals[0];
            result.Add(last);

            for (int i = 1; i < intervals.Count(); i++)
            {
                Interval cur = intervals[i];
                if (cur.start <= last.end)
                {
                    if (cur.end > last.end)
                    {
                        last.end = cur.end;
                    }
                }
                else
                {
                    result.Add(cur);
                    last = cur;
                }
            }
            return result;
        }

        /// <summary>
        /// Represents a time interval from start to end
        /// </summary>
        class Interval
        {
            public TimeSpan start;
            public TimeSpan end;

            public Interval(TimeSpan start, TimeSpan end)
            {
                this.start = start;
                this.end = end;
            }
        }
    }

}