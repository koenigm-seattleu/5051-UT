using System;
using _5051.Models.Enums;

namespace _5051.Models
{
    /// <summary>
    /// Data to Track Attendance. Contains info about who the student it, when he checked in and out, what his emotion status was. In and Out are in UTC
    /// </summary>
    public class AttendanceModel
    {
        /// <summary>
        /// ID of the attendance
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ID of the Student
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// When Logged in, in utc
        /// </summary>
        public DateTime In { get; set; }

        /// <summary>
        /// When Logged Out, in utc
        /// </summary>
        public DateTime Out { get; set; }

        /// <summary>
        /// The emotion state
        /// </summary>
        public EmotionStatusEnum Emotion { get; set; }
        public string EmotionUri { get; set; }

        /// <summary>
        /// If this is a newly added attendance, which means that token accumulated in this attendance has not been added yet.
        /// Need to process token.
        /// </summary>
        public bool IsNew { get; set; } = true;

        public AttendanceModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public AttendanceModel Update(AttendanceModel data)
        {
            if (data == null)
            {
                return null;
            }

            Id = data.Id;
            StudentId = data.StudentId;
            In = data.In;
            Out = data.Out;
            Emotion = data.Emotion;
            EmotionUri = data.EmotionUri;
            IsNew = data.IsNew;

            return data;
        }

    }
}