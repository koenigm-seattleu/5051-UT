using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using _5051.Backend;

namespace _5051.Models
{
    /// <summary>
    /// For the Index View, add the Avatar URI to the Student, so it shows the student with the picture
    /// </summary>
    public class StudentDisplayViewModel : StudentModel
    {
        /// <summary>
        /// DateTime of last transaction recorded, used for login and logout
        /// </summary>
        [Display(Name = "Date", Description = "Date and Time")]
        public DateTime LastDateTime { get; set; }

        /// <summary>
        /// DateTime of last transaction recorded, used for login and logout
        /// </summary>
        [Display(Name = "Last Login", Description = "Last Login")]
        public DateTime LastLogIn { get; set; }

        /// <summary>
        /// Percentage attended this week
        /// </summary>
        [Display(Name = "Weekly Attendance Score", Description = "Weekly Attendance Score")]
        public int WeeklyAttendanceScore { get; set; }

        /// <summary>
        /// Percentage attended this month
        /// </summary>
        [Display(Name = "Monthly Attendance Score", Description = "Monthly Attendance Score")]
        public int MonthlyAttendanceScore { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public StudentDisplayViewModel() { }

        /// <summary>
        /// Creates a Student Display View Model from a Student Model
        /// </summary>
        /// <param name="data">The Student Model to create</param>
        public StudentDisplayViewModel(StudentModel data)
        {
            if (data == null)
            {
                // Nothing to convert
                return;
            }

            Id = data.Id;
            Name = data.Name;
            Tokens = data.Tokens;
            AvatarLevel = data.AvatarLevel;
            AvatarComposite = data.AvatarComposite;

            Status = data.Status;
            ExperiencePoints = data.ExperiencePoints;
            Password = data.Password;
            Attendance = data.Attendance;
            EmotionCurrent = data.EmotionCurrent;
            EmotionUri = Emotion.GetEmotionURI(EmotionCurrent);
        }
    }
}