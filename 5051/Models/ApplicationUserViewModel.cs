using _5051.Backend;
using System;
using System.Collections.Generic;

namespace _5051.Models
{
    /// <summary>
    /// List of Users by Role
    /// </summary>
    public class ApplicationUserViewModel
    {
        public List<UserRoleEnum> UserRole;
        public StudentDisplayViewModel Student;

        public ApplicationUserViewModel(ApplicationUser User) 
        {
            if (User == null)
            {
                return;
            }

            UserRole = new List<UserRoleEnum>();
            foreach (UserRoleEnum RoleEnum in Enum.GetValues(typeof(UserRoleEnum)))
            {
                if(DataSourceBackend.Instance.IdentityBackend.UserHasClaimOfType(User.Id, RoleEnum))
                {
                    UserRole.Add(RoleEnum);
                }
            }

            var StudentData = DataSourceBackend.Instance.StudentBackend.Read(User.Id);
            Student = new StudentDisplayViewModel(StudentData)
            {
                Name = User.UserName,
                Id = User.Id
            };
        }
    }
}