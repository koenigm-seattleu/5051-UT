using System;
using System.Linq;
using _5051.Models;

namespace _5051.Backend
{
    /// <summary>
    /// StudentBackend helper functions
    /// </summary>
    public static class StudentBackendHelper
    {
        /// <summary>
        /// Create demo attendance
        /// </summary>
        public static void CreateDemoAttendance()
        {
            DateTime dateStart = new DateTime();
            DateTime dateEnd = new DateTime();

            //Set the range to be from the first day of school to the last day of school
            dateStart = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayFirst;
            dateEnd = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().DayLast;

            //Don't generate attendance for today and future days
            DateTime yesterday = DateTimeHelper.Instance.GetDateTimeNowUTC().AddDays(-1);
            if (yesterday.CompareTo(dateEnd) < 0)
            {
                dateEnd = yesterday;
            }

            var tempStudentList = DataSourceBackend.Instance.StudentBackend.Index().ToList<StudentModel>();
            tempStudentList = tempStudentList.OrderBy(x => x.TimeStamp).ToList();

            //Generate attendance records for 5 student personas
            //The scenario of this demo data:
            //Student at index 0 has perfect attendance: always arrive on time and stay
            GenerateAttendance(tempStudentList[0].Id,0, dateStart, dateEnd);
            //Student at index 1 has good attendance: on average has 1 day late and 1 day leave early out of every 5 days
            GenerateAttendance(tempStudentList[1].Id,1, dateStart, dateEnd);
            //Student at index 2 has average attendance: on average has 1 day late, 1 day leave early and 1 day absent out of every 5 days
            GenerateAttendance(tempStudentList[2].Id,2, dateStart, dateEnd);
            //Student at index 3 has bad attendance: on average has 1 day late, 2 days very late, 1 day leave early and 2 days absent out of every 5 days
            GenerateAttendance(tempStudentList[3].Id,3, dateStart, dateEnd);
            //Student at index 4 has no attendance: always absent
            GenerateAttendance(tempStudentList[4].Id,4, dateStart, dateEnd);
            //To do: create scenario for multiple check-ins

            DataSourceBackend.Instance.StudentBackend.ResetStatusAndProcessNewAttendance();
        }

        /// <summary>
        /// Generate attendance records for student of given name from the start date to the end date
        /// </summary>
        /// <param name="index">The index of the student in studentBackend index</param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        private static void GenerateAttendance(string studentId, int StudentType, DateTime dateStart, DateTime dateEnd)
        {
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(studentId);

            // Set current date to be 1 less than the start day, because will get added in the for loop
            DateTime currentDate = dateStart;

            //To generate random numbers, since seed is fixed, the numbers generated will be same in every run
            Random r = new Random(0);

            while (currentDate.CompareTo(dateEnd) < 0)
            {

                // Create an attendance model for this student
                var temp = new AttendanceModel
                {
                    StudentId = myStudent.Id,
                    //Status = StudentStatusEnum.Out
                };

                // Look to the next day
                currentDate = currentDate.AddDays(1);
            }

            // Update the data
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);
        }


    }
}



