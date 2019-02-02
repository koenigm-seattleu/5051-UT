using _5051.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    public class AttendanceDetailsViewModel
    {
        /// <summary>
        /// The attendance record for the 
        /// </summary>
        public AttendanceModel Attendance { get; set; }

        /// <summary>
        /// The Friendly name for the student, does not need to be directly associated with the actual student name
        /// </summary>
        [Display(Name = "Name", Description = "Nick Name")]
        public string Name { get; set; }

        /// <summary>
        ///  The composite for the Avatar
        /// </summary>
        public AvatarCompositeModel AvatarComposite { get; set; }

        public AttendanceDetailsViewModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> Attendance Model ID</param>
        public AttendanceDetailsViewModel(string StudentId, string AttendanceId = null)
        {
            if (string.IsNullOrEmpty(StudentId))
            {
                return;
            }

            if (string.IsNullOrEmpty(AttendanceId))
            {
                return;
            }

            Initialize(StudentId, AttendanceId);
        }

        public AttendanceDetailsViewModel Initialize(string id, string item)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            if (string.IsNullOrEmpty(item))
            {
                return null;
            }

            //get the attendance with given id
            var myAttendance = DataSourceBackend.Instance.StudentBackend.ReadAttendance(id, item);
            if (myAttendance == null)
            {
                return null;
            }

            var studentId = myAttendance.StudentId;
            var student = DataSourceBackend.Instance.StudentBackend.Read(studentId);
            // Not possible to be null, because ReadAttendance returns with the student Id
            //if (student == null)
            //{
            //    return null;
            //}

            var ret = new AttendanceDetailsViewModel
            {
                Attendance = new AttendanceModel
                {
                    StudentId = myAttendance.StudentId,
                    Id = myAttendance.Id,
                    In = UTCConversionsBackend.UtcToKioskTime(myAttendance.In),
                    Out = UTCConversionsBackend.UtcToKioskTime(myAttendance.Out),
                    Emotion = myAttendance.Emotion,
                    EmotionUri = Emotion.GetEmotionURI(myAttendance.Emotion),

                    IsNew = myAttendance.IsNew
                },
                Name = student.Name,

                AvatarComposite = student.AvatarComposite
            };

            return ret;
        }


    }
}