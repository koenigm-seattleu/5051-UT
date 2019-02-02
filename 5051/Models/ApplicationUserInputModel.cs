using _5051.Backend;
using System;
using System.Collections.Generic;

namespace _5051.Models
{
    /// <summary>
    /// List of Users by Role
    /// </summary>
    public class ApplicationUserInputModel 
    {
        public string Id { get; set; }
        public UserRoleEnum Role { get; set; }
        public bool State { get; set; }

        public StudentModel Student;

        public ApplicationUserViewModel ApplicationUser;

        public ApplicationUserInputModel(ApplicationUser User)
        {
            if (User == null)
            {
                return;
            }

            ApplicationUser = new ApplicationUserViewModel(User);

            Student = DataSourceBackend.Instance.StudentBackend.Read(User.Id);
            Student.Name = User.UserName;
            Student.Id = User.Id;
        }

        public ApplicationUserInputModel() { }

    }
}