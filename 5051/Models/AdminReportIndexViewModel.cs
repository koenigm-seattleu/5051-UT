using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    public class AdminReportIndexViewModel: StudentViewModel
    {
        // Weekly leaderboard. Rank students by their attended duration in the current week.
        public List<StudentModel> Leaderboard { get; set; } = new List<StudentModel>();

        /// <summary>
        /// Take the data list passed in, and convert each to a new StudentDisplayViewModel item and add that to the StudentList
        /// </summary>
        /// <param name="dataList"></param>
        public AdminReportIndexViewModel(List<StudentModel> dataList)
        {
            foreach (var item in dataList)
            {
                StudentList.Add(new StudentDisplayViewModel(item));
            }
        }
    }
}