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


            //Generate report for this month
            GenerateReportFromStartToEnd(report);

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


            //Generate report for this month
            GenerateReportFromStartToEnd(report);

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

            //Generate report for this semester
            GenerateReportFromStartToEnd(report);

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

            //Generate report for this semester
            GenerateReportFromStartToEnd(report);

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


            GenerateReportFromStartToEnd(report);

            return report;
        }
        #endregion

        /// <summary>
        /// Generate a leaderboard. Rank students according to their attended minutes in this week.
        /// </summary>
        /// <returns></returns>
        public List<StudentModel> GenerateLeaderboard()
        {
            var dayNow = UTCConversionsBackend.UtcToKioskTime(DateTimeHelper.Instance.GetDateTimeNowUTC()).Date; //today's date
            var thisMonday = dayNow.AddDays(-((dayNow.DayOfWeek - DayOfWeek.Monday + 7) % 7)); //this Monday's date

            var studentList = DataSourceBackend.Instance.StudentBackend.Index();  //student list

            foreach (var student in studentList)
            {
                student.AttendedMinutesThisWeek = 0; //reset

                var currentDate = thisMonday; //loop variable

                while (currentDate.CompareTo(dayNow) < 0) //loop until today, don't include today
                {
                    //get today's school calendar model
                    var myToday = DataSourceBackend.Instance.SchoolCalendarBackend.ReadDate(currentDate);
                    if (myToday != null && myToday.SchoolDay)
                    {
                        var myRange = student.Attendance
                            .Where(m => UTCConversionsBackend.UtcToKioskTime(m.In).Date == currentDate.Date)
                            .OrderByDescending(m => m.In).ToList();
                        if (myRange.Any())
                        {
                            // a list containing all intervals in this day
                            List<Interval> intervals = new List<Interval>();

                            //loop through all attendance records in my range
                            foreach (var item in myRange)
                            {
                                TimeSpan timeIn = UTCConversionsBackend.UtcToKioskTime(item.In).TimeOfDay;
                                TimeSpan timeOut = UTCConversionsBackend.UtcToKioskTime(item.Out).TimeOfDay;
                                Interval inter = new Interval(timeIn, timeOut);
                                intervals.Add(inter);                            
                            }

                            var earlyWindow = SchoolDismissalSettingsBackend.Instance.GetDefault().EarlyWindow;
                            var lateWindow = SchoolDismissalSettingsBackend.Instance.GetDefault().LateWindow;
                            //the time from which duration starts to count
                            var start = myToday.TimeStart.Add(-earlyWindow);
                            //the time that duration counts until
                            var end = myToday.TimeEnd.Add(lateWindow);

                            //Calculate hours attended on this day
                            var dailyTotalTime = CalculateHoursAttended(intervals, start, end);

                            student.AttendedMinutesThisWeek += (int)dailyTotalTime.TotalMinutes;
                        }
                    }
                    currentDate = currentDate.AddDays(1);
                }
            }
            //sort
            var leaderboard = studentList.OrderByDescending(m => m.AttendedMinutesThisWeek).ToList();
            return leaderboard;
        }

        /// <summary>
        /// Generate the report from the start date to the end date
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        private void GenerateReportFromStartToEnd(BaseReportViewModel report)
        {
            var myDateNow = UTCConversionsBackend.UtcToKioskTime(DateTimeHelper.Instance.GetDateTimeNowUTC()).Date; //today's date in kiosk time zone
            // Don't go beyond today
            if (report.DateEnd.CompareTo(myDateNow) > 0)
            {
                report.DateEnd = myDateNow;
            }

            var currentDate = report.DateStart;  //loop variable

            TimeSpan accumlatedTotalHoursExpected = TimeSpan.Zero; //current accumulated total hours expected
            TimeSpan accumlatedTotalHours = TimeSpan.Zero; //current accululated total hours attended
            int emotionLevel = 0; //current emotion level

            while (currentDate.CompareTo(report.DateEnd) <= 0)  //loop until last date, include last date
            {
                //create a new AttendanceReportViewmodel for each day
                var temp = new AttendanceReportViewModel
                {
                    Date = currentDate
                };

                // Hold the emotion for the null condition
                temp.EmotionUri = "/content/img/placeholder.png";

                //get today's school calendar model
                var myToday = DataSourceBackend.Instance.SchoolCalendarBackend.ReadDate(currentDate);

                // if the day is not a school day, set IsSchoolDay to false
                if (myToday == null || myToday.SchoolDay == false)
                {
                    temp.IsSchoolDay = false;
                }

                // if the day is a school day, perform calculations
                else
                {
                    temp.HoursExpected = myToday.TimeDuration;

                    // Find out if the student attended that day, and add that in.  Because the student can check in/out multiple times add them together.
                    var myRange = report.Student.Attendance.Where(m => UTCConversionsBackend.UtcToKioskTime(m.In).Date == currentDate.Date).OrderBy(m => m.In).ToList();

                    //if no attendance record on this day, set attendance status to absent
                    if (!myRange.Any())
                    {
                        temp.AttendanceStatus = AttendanceStatusEnum.AbsentUnexcused;
                        report.Stats.DaysAbsentUnexcused++;
                    }
                    else
                    {
                        temp.AttendanceStatus = AttendanceStatusEnum.Present;

                        //set TimeIn to be the first check-in time in the list, so that if there are multiple check-ins,
                        //the TimeIn is set to the first check-in time. Same for emotion.
                        temp.TimeIn = UTCConversionsBackend.UtcToKioskTime(myRange.First().In);
                        temp.Emotion = myRange.First().Emotion;
                        temp.EmotionUri = Emotion.GetEmotionURI(temp.Emotion);

                        //determine whether is on time or late
                        if (temp.TimeIn.TimeOfDay > myToday.TimeStart)
                        {
                            temp.CheckInStatus = CheckInStatusEnum.ArriveLate;
                        }
                        else
                        {
                            temp.CheckInStatus = CheckInStatusEnum.ArriveOnTime;
                        }

                        // a list containing all intervals in this day
                        List<Interval> intervals = new List<Interval>();

                        //loop through all attendance records in my range
                        foreach (var item in myRange)
                        {
                            TimeSpan timeIn = UTCConversionsBackend.UtcToKioskTime(item.In).TimeOfDay;
                            TimeSpan timeOut = UTCConversionsBackend.UtcToKioskTime(item.Out).TimeOfDay;
                            Interval inter = new Interval(timeIn, timeOut);

                            intervals.Add(inter);

                            //update the checkout time for this attendance report view model
                            temp.TimeOut = UTCConversionsBackend.UtcToKioskTime(item.Out);

                            //determine whether left early or not
                            if (temp.TimeOut.TimeOfDay < myToday.TimeEnd)
                            {
                                temp.CheckOutStatus = CheckOutStatusEnum.DoneEarly;
                            }
                            else
                            {
                                temp.CheckOutStatus = CheckOutStatusEnum.DoneAuto;
                            }
                        }

                        report.Stats.DaysPresent++;  //increase number of days present

                        var earlyWindow = SchoolDismissalSettingsBackend.Instance.GetDefault().EarlyWindow;
                        var lateWindow = SchoolDismissalSettingsBackend.Instance.GetDefault().LateWindow;
                        //the time from which duration starts to count
                        var start = myToday.TimeStart.Add(-earlyWindow);
                        //the time that duration counts until
                        var end = myToday.TimeEnd.Add(lateWindow);

                        //Calculate hours attended on this day
                        temp.HoursAttended = CalculateHoursAttended(intervals, start, end);

                        temp.PercentAttended = (int)(temp.HoursAttended.TotalMinutes * 100 / temp.HoursExpected.TotalMinutes);  //calculate percentage of attended time

                        if (temp.CheckInStatus == CheckInStatusEnum.ArriveLate)
                        {
                            report.Stats.DaysLate++;
                        }

                        report.Stats.DaysOnTime = report.Stats.DaysPresent - report.Stats.DaysLate;

                        if (temp.CheckOutStatus == CheckOutStatusEnum.DoneEarly)
                        {
                            report.Stats.DaysOutEarly++;
                        }

                        report.Stats.DaysOutAuto = report.Stats.DaysPresent - report.Stats.DaysOutEarly;

                    }

                    switch (temp.Emotion)
                    {
                        case EmotionStatusEnum.VeryHappy:
                            temp.EmotionLevel = emotionLevel + 2;
                            report.Stats.DaysVeryHappy++;
                            break;
                        case EmotionStatusEnum.Happy:
                            temp.EmotionLevel = emotionLevel + 1;
                            report.Stats.DaysHappy++;
                            break;
                        case EmotionStatusEnum.Neutral:
                            temp.EmotionLevel = emotionLevel;
                            report.Stats.DaysNeutral++;
                            break;
                        case EmotionStatusEnum.Sad:
                            temp.EmotionLevel = emotionLevel - 1;
                            report.Stats.DaysSad++;
                            break;
                        case EmotionStatusEnum.VerySad:
                            temp.EmotionLevel = emotionLevel - 2;
                            report.Stats.DaysVerySad++;
                            break;
                        default:
                            temp.EmotionLevel = emotionLevel;
                            break;
                    }

                    emotionLevel = temp.EmotionLevel;
                    //calculations for both absent and present records                    
                    //calculations for both absent and present records                    
                    report.Stats.NumOfSchoolDays++;
                    accumlatedTotalHoursExpected += temp.HoursExpected;
                    accumlatedTotalHours += temp.HoursAttended;

                    // Need to add the totals back to the temp, because the temp is new each iteration
                    temp.TotalHoursExpected += accumlatedTotalHoursExpected;
                    temp.TotalHours = accumlatedTotalHours;
                }

                //add this attendance report to the attendance list
                report.AttendanceList.Add(temp);

                currentDate = currentDate.AddDays(1);
            }

            report.Stats.AccumlatedTotalHoursExpected = accumlatedTotalHoursExpected;
            report.Stats.AccumlatedTotalHours = accumlatedTotalHours;

            //if there is at least one school days in this report, calculate the following stats
            if (report.Stats.NumOfSchoolDays > 0)
            {
                report.Stats.PercPresent = (int)Math.Round((double)report.Stats.DaysPresent * 100 / report.Stats.NumOfSchoolDays);
                report.Stats.PercAttendedHours =
                    (int)Math.Round(report.Stats.AccumlatedTotalHours.TotalHours * 100 / report.Stats.AccumlatedTotalHoursExpected.TotalHours);
                report.Stats.PercExcused = (int)Math.Round((double)report.Stats.DaysAbsentExcused * 100 / report.Stats.NumOfSchoolDays);
                report.Stats.PercUnexcused = (int)Math.Round((double)report.Stats.DaysAbsentUnexcused * 100 / report.Stats.NumOfSchoolDays);
                if (report.Stats.DaysPresent > 0)
                {
                    report.Stats.PercInLate = (int)Math.Round((double)report.Stats.DaysLate * 100 / report.Stats.DaysPresent);
                    report.Stats.PercOutEarly = (int)Math.Round((double)report.Stats.DaysOutEarly * 100 / report.Stats.DaysPresent);
                }
            }

            //set the attendance goal percent according to school dismissal settings
            report.Goal = SchoolDismissalSettingsBackend.Instance.GetDefault().Goal;

            //set the date array, ideal value array and actual value array for line chart
            report.YearArray = @String.Join(", ", report.AttendanceList.Where(m => m.IsSchoolDay).ToList().Select(m => m.Date.Year.ToString()).ToArray());
            report.MonthArray = @String.Join(", ", report.AttendanceList.Where(m => m.IsSchoolDay).ToList().Select(m => m.Date.Month.ToString()).ToArray());
            report.DayArray = @String.Join(", ", report.AttendanceList.Where(m => m.IsSchoolDay).ToList().Select(m => m.Date.Day.ToString()).ToArray());
            report.ActualValues = @String.Join(", ",
                report.AttendanceList.Where(m => m.IsSchoolDay).ToList()
                    .Select(m => m.TotalHours.TotalHours.ToString("0.#")).ToArray());
            report.PerfectValues = @String.Join(", ",
                report.AttendanceList.Where(m => m.IsSchoolDay).ToList()
                    .Select(m => m.TotalHoursExpected.TotalHours.ToString("0.#")).ToArray());
            report.GoalValues = @String.Join(", ",
                report.AttendanceList.Where(m => m.IsSchoolDay).ToList()
                    .Select(m => (m.TotalHoursExpected.TotalHours * (report.Goal) / 100).ToString("0.#")).ToArray());
            report.EmotionLevelValues = @String.Join(", ",
                report.AttendanceList.Where(m => m.IsSchoolDay).Select(m => m.EmotionLevel).ToArray());
        }

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