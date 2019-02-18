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
    /// The Student, this holds the student id, name, tokens.  Other things about the student such as their attendance is pulled from the attendance data, or the owned items, from the inventory data
    /// </summary>
    public class StudentModel
    {
        // When the record was created, used for Table Storage
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// The ID for the Student, this is the key, and a required field
        /// </summary>
        [Key]
        [Display(Name = "Id", Description = "Student Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        /// <summary>
        /// The Friendly name for the student, does not need to be directly associated with the actual student name
        /// </summary>
        [Display(Name = "Name", Description = "Nick Name")]
        public string Name { get; set; }

        /// <summary>
        ///  The composite for the Avatar
        /// </summary>
        public AvatarCompositeModel AvatarComposite { get; set; }

        /// <summary>
        /// The inventory list of the Avatar Components for the student
        /// </summary>
        public List<AvatarItemModel> AvatarInventory { get; set; }

        /// <summary>
        /// The personal level for the Avatar, the avatar levels up.  switching the avatar ID (picture), does not change the level
        /// </summary>
        [Display(Name = "Avatar Level", Description = "Level of the Avatar")]
        public int AvatarLevel { get; set; }

        /// <summary>
        /// The number of Tokens the student has, tokens are used in the store, and also to level up
        /// </summary>
        [Display(Name = "XP", Description = "Experience Points Earned")]
        public int ExperiencePoints { get; set; }

        /// <summary>
        /// The number of Tokens the student has, tokens are used in the store, and also to level up
        /// </summary>
        [Display(Name = "Tokens", Description = "Tokens Saved")]
        public int Tokens { get; set; }

        /// <summary>
        /// The status of the student, for example currently logged in, out
        /// </summary>
        [Display(Name = "Current Status", Description = "Status of the Student")]
        public StudentStatusEnum Status { get; set; }

        /// <summary>
        /// The status of the student, for example currently logged in, out
        /// </summary>
        [Display(Name = "Password", Description = "Student Password")]
        [PasswordPropertyText]
        public string Password { get; set; }

        /// <summary>
        /// The current emotion status of the student
        /// </summary>
        [Display(Name = "Current Emotion", Description = "Current Emotion")]
        public EmotionStatusEnum EmotionCurrent { get; set; }

        /// <summary>
        /// The URL to the current emotion
        /// </summary>
        public string EmotionUri { get; set; }

        /// <summary>
        /// The Attendance list for the student
        /// </summary>
        public List<AttendanceModel> Attendance { get; set; }

        /// <summary>
        /// Attended Minutes This Week
        /// </summary>
        [Display(Name = "Attended Minutes This Week", Description = "Attended Minutes This Week")]
        public int AttendedMinutesThisWeek { get; set; }

        /// <summary>
        /// The defaults for a new student
        /// </summary>
        public void Initialize()
        {
            TimeStamp = DateTimeHelper.Instance.GetDateTimeNowUTC();
            Id = Guid.NewGuid().ToString();
            Tokens = 20;
            AvatarLevel = 1;
            Status = StudentStatusEnum.Out;
            ExperiencePoints = 0;
            Password = "abc";
            Attendance = new List<AttendanceModel>();
            EmotionCurrent = EmotionStatusEnum.Neutral;
            EmotionUri = Emotion.GetEmotionURI(EmotionCurrent);

            AvatarComposite = new AvatarCompositeModel();


            AvatarInventory = new List<AvatarItemModel>
            {
                DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory),
                DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Cheeks),
                DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Expression),
                DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.HairBack),
                DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.HairFront),
                DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.ShirtFull),
                //DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.ShirtShort),
                DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Pants)
            };

            // Load all the Heads
            AvatarInventory.AddRange(DataSourceBackend.Instance.AvatarItemBackend.GetAllAvatarItem(AvatarItemCategoryEnum.Head));

        }

        /// <summary>
        /// Constructor for a student
        /// </summary>
        public StudentModel()
        {
            Initialize();
        }


        /// <summary>
        /// Constructor for Student.  make a copy of one passed in.
        /// </summary>
        /// <param name="name">The Name to call the student</param>
        public StudentModel(StudentModel data)
        {
            Initialize();
            Update(data);
        }

        /// <summary>
        /// Constructor for Student.  Call this when making a new student
        /// </summary>
        /// <param name="name">The Name to call the student</param>
        public StudentModel(string name)
        {
            Initialize();

            Name = name;
        }

        /// <summary>
        /// Convert a Student Display View Model, to a Student Model, used for when passed data from Views that use the full Student Display View Model.
        /// </summary>
        /// <param name="data">The student data to pull</param>
        public StudentModel(StudentDisplayViewModel data)
        {
            Id = data.Id;
            Name = data.Name;

            AvatarComposite = data.AvatarComposite;
            AvatarInventory = data.AvatarInventory;

            AvatarLevel = data.AvatarLevel;
            Tokens = data.Tokens;
            Status = data.Status;
            ExperiencePoints = data.ExperiencePoints;
            Password = data.Password;
            Attendance = data.Attendance;
            EmotionCurrent = data.EmotionCurrent;
            EmotionUri = Emotion.GetEmotionURI(EmotionCurrent);

        }

        /// <summary>
        /// Update the Data Fields with the values passed in, do not update the ID.
        /// </summary>
        /// <param name="data">The values to update</param>
        /// <returns>False if null, else true</returns>
        public bool Update(StudentModel data)
        {
            if (data == null)
            {
                return false;
            }

            Name = data.Name;

            AvatarComposite = data.AvatarComposite;
            AvatarInventory = data.AvatarInventory;

            AvatarLevel = data.AvatarLevel;
            Tokens = data.Tokens;
            Status = data.Status;
            ExperiencePoints = data.ExperiencePoints;
            Password = data.Password;
            Attendance = data.Attendance;
            EmotionCurrent = data.EmotionCurrent;
            EmotionUri = Emotion.GetEmotionURI(EmotionCurrent);

            return true;
        }
    }
}