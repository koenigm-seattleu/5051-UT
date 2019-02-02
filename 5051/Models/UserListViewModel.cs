using System.Collections.Generic;

namespace _5051.Models
{
    /// <summary>
    /// List of Users by Role
    /// </summary>
    public class UserListViewModel
    {
        public List<StudentDisplayViewModel> StudentList;
        public List<StudentDisplayViewModel> TeacherList;
        public List<StudentDisplayViewModel> SupportList;
        public List<StudentDisplayViewModel> UserList;

        public UserListViewModel()
        {
            StudentList = new List<StudentDisplayViewModel>();
            TeacherList = new List<StudentDisplayViewModel>();
            SupportList = new List<StudentDisplayViewModel>();
            UserList = new List<StudentDisplayViewModel>();
        }
    }
}