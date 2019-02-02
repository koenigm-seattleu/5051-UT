using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    /// <summary>
    /// View Model for the Visit Views to have the list of students' trucks and leaderboard
    /// </summary>
    public class VisitTruckViewModel
    {
        /// <summary>
        /// The List of Students
        /// </summary>
        public List<StudentModel> StudentList = new List<StudentModel>();

        // Leaderboard
        public List<LeaderBoardModel> LeaderBoard;

        // Current Student
        public StudentModel Student;
    }

}