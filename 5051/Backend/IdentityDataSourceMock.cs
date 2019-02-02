using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using _5051.Models;
using _5051.Controllers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Principal;


namespace _5051.Backend
{
    public class IdentityDataSourceMock
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public IdentityDataSourceMock() { }

        public IdentityDataSourceMock(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        /// <summary>
        /// Creates a new Support User
        /// returns the newly created user
        /// returns null if failed
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ApplicationUser CreateNewSupportUser(string userName, string password, string supportId)
        {
            //fill in all fields needed
            var user = new ApplicationUser { UserName = userName, Email = userName + "@seattleu.edu", Id = supportId };

            var result = UserManager.Create(user, password);

            if (!result.Succeeded)
            {
                //if user does exist, delete. This is just temporary will return null
                //var findResult = FindUserByUserName(userName);

                //var deleteResult = DeleteUser(findResult);

                //user = new ApplicationUser { UserName = userName, Email = userName + "@seattleu.edu" };

                //result = UserManager.Create(user, userName);
                return null;
            }

            var claimResult = AddClaimToUser(user.Id, _5051.Models.UserRoleEnum.TeacherUser.ToString(), "True");
            claimResult = AddClaimToUser(user.Id, _5051.Models.UserRoleEnum.SupportUser.ToString(), "True");

            return claimResult;
        }


        /// <summary>
        /// Creates a teacher user
        /// returns the newly created user or null if failure
        /// </summary>
        /// <param name="teacherName"></param>
        /// <param name="teacherPassword"></param>
        /// <returns></returns>
        public ApplicationUser CreateNewTeacher(string teacherName, string teacherPassword, string teacherId)
        {
            //fill in all fields needed
            var user = new ApplicationUser { UserName = teacherName, Email = teacherName + "@seattleu.edu", Id = teacherId };

            var result = UserManager.Create(user, teacherPassword);

            if (!result.Succeeded)
            {
                return null;
            }

            var claimResult = AddClaimToUser(user.Id, _5051.Models.UserRoleEnum.TeacherUser.ToString(), "True");
            claimResult = AddClaimToUser(user.Id, "TeacherID", teacherId);

            return claimResult;
        }

        /// <summary>
        /// Creates a student user
        /// returns the newly created user or null if failed
        /// right now the password is changed to be the same as the student name
        /// </summary>
        /// <param name="studentName"></param>
        /// <param name="studentPassword"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public ApplicationUser CreateNewStudent(StudentModel student)
        {
            //fill in all fields needed
            var user = new ApplicationUser { UserName = student.Name, Email = student.Name + "@seattleu.edu", Id = student.Id };

            var result = UserManager.Create(user, student.Name);

            if (!result.Succeeded)
            {
                return null;
            }

            var claimResult = AddClaimToUser(user.Id, _5051.Models.UserRoleEnum.StudentUser.ToString(), "True");
            claimResult = AddClaimToUser(user.Id, "StudentID", student.Id);

            return claimResult;
        }


        /// <summary>
        /// updates all fields in the database except the id
        /// returns false if update fails
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public bool UpdateStudent(StudentModel student)
        {
            if(student == null)
            {
                return false;
            }

            var findResult = FindUserByID(student.Id);

            if(findResult == null)
            {
                return false;
            }

            //update all fields in db to match given student record
            findResult.UserName = student.Name;

            var updateResult = UserManager.Update(findResult);

            return true;
        }


        /// <summary>
        /// Finds and returns the user using the given user name
        /// if user does not exist, returns null
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ApplicationUser FindUserByUserName(string userName)
        {
            var findResult = UserManager.FindByName(userName);

            return findResult;
        }

        /// <summary>
        /// Finds and returns the user using the given id
        /// if user does not exist, returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationUser FindUserByID(string id)
        {
            var findResult = UserManager.FindById(id);

            return findResult;
        }

        public StudentModel GetStudentById(string id)
        {
            var studentResult = DataSourceBackend.Instance.StudentBackend.Read(id);

            if(studentResult == null)
            {
                return null;
            }

            return studentResult;
        }

        /// <summary>
        /// Lists all the users
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllUsers()
        {
            var userList = UserManager.Users.ToList();

            return userList;
        }

        /// <summary>
        /// Lists all the student users
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllStudentUsers()
        {
            var userList = ListAllUsers();
            var studentList = new List<ApplicationUser>();

            foreach (var user in userList)
            {
                if (UserHasClaimOfValue(user.Id, _5051.Models.UserRoleEnum.StudentUser.ToString(), "True"))
                {
                    studentList.Add(user);
                }
            }

            return studentList;
        }

        /// <summary>
        /// Lists all the teacher user
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllTeacherUsers()
        {
            var userList = ListAllUsers();
            var teacherList = new List<ApplicationUser>();

            foreach (var user in userList)
            {
                if (UserHasClaimOfValue(user.Id, _5051.Models.UserRoleEnum.TeacherUser.ToString(), "True"))
                {
                    teacherList.Add(user);
                }
            }

            return teacherList;
        }


        /// <summary>
        /// lists all the support users
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> ListAllSupportUsers()
        {
            var userList = ListAllUsers();
            var supportList = new List<ApplicationUser>();

            foreach (var user in userList)
            {
                if(UserHasClaimOfValue(user.Id, _5051.Models.UserRoleEnum.SupportUser.ToString(), "True"))
                {
                    supportList.Add(user);
                }
            }

            return supportList;
        }

        /// <summary>
        /// checks if user has the given claim type and value
        /// returns false if not
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claimType"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        public bool UserHasClaimOfValue(string userID, string claimType, string claimValue)
        {
            var user = FindUserByID(userID);

            if(user == null)
            {
                return false;
            }

            var claims = user.Claims.ToList();

            foreach (var item in claims)
            {
                if (item.ClaimType == claimType)
                {
                    if (item.ClaimValue == claimValue)
                    {
                        return true;
                    }                  
                }
            }

            return false;
        }

        /// <summary>
        /// Adds the given claim type and value to the user
        /// returns null if failure to add
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="claimTypeToAdd"></param>
        /// <param name="claimValueToAdd"></param>
        /// <returns></returns>
        public ApplicationUser AddClaimToUser(string userID, string claimTypeToAdd, string claimValueToAdd)
        {
            var findResult = FindUserByID(userID);

            if(findResult == null)
            {
                return null;
            }

            var claimAddResult = UserManager.AddClaim(userID, new Claim(claimTypeToAdd, claimValueToAdd));

            if(!claimAddResult.Succeeded)
            {
                return null;
            }

            return findResult;
        }

        /// <summary>
        /// removes a claim from the user
        /// returns false if failure to remove
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="claimTypeToRemove"></param>
        /// <returns></returns>
        public bool RemoveClaimFromUser(string userID, string claimTypeToRemove)
        {
            var claims = UserManager.GetClaims(userID);

            if (claims == null)
            {
                return false;
            }

            var lastAccessedClaim = claims.FirstOrDefault(t => t.Type == claimTypeToRemove);

            var resultDelete = (lastAccessedClaim == null) ? null :  UserManager.RemoveClaim(userID, lastAccessedClaim);

            if(!resultDelete.Succeeded)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes the given user
        /// returns false if delete fails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool DeleteUser(ApplicationUser user)
        {
            var deleteResult = UserManager.Delete(user);

            if (deleteResult.Succeeded)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes the user with the given id
        /// returns false if delete fails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool DeleteUser(string id)
        {
            var findResult = FindUserByID(id);

            var deleteResult = UserManager.Delete(findResult);

            if (deleteResult.Succeeded)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Logs the user in with the given password
        /// returns false if login fails
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool LogUserIn(ApplicationUser user, string password)
        {
            var result = SignInManager.PasswordSignIn(user.UserName, password, isPersistent: false, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return true;
                case SignInStatus.Failure:
                default:
                    return false;
            }
        }

        /// <summary>
        /// Logs the user in using the given password
        /// returns false if login fails
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool LogUserIn(string userName, string password)
        {
            if(password == null)
            {
                return false;
            }

            var result = SignInManager.PasswordSignIn(userName, password, isPersistent: false, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return true;
                case SignInStatus.Failure:
                default:
                    return false;
            }
        }

        /// <summary>
        /// logs the currently logged in user out
        /// </summary>
        /// <returns></returns>
        public bool LogUserOut()
        {
            SignInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return true;
        }

        public void Reset()
        {
            var userList = ListAllUsers();

            foreach (var item in userList)
            {
                var deleteResult = DeleteUser(item);
            }
        }


        //need all the above functions again but async

    }


    public static class IdentityExtensions
    {

        public static bool GetIsTeacherUser(this IIdentity identity)
        {
            if (identity == null)
            {
                return false;
            }

            // return new ManageController().IsUserTeacherUser(userId);

            var claim = ((ClaimsIdentity)identity).FindFirst(_5051.Models.UserRoleEnum.TeacherUser.ToString());
            // Test for null to avoid issues during local testing

            if (claim == null)
            {
                return false;
            }

            if (claim.Value.Equals("True"))
            {
                return true;
            }

            return false;
        }

        public static bool GetIsSupportUser(this IIdentity identity)
        {
            if (identity == null)
            {
                return false;
            }

            // return new ManageController().IsUserTeacherUser(userId);

            var claim = ((ClaimsIdentity)identity).FindFirst(_5051.Models.UserRoleEnum.SupportUser.ToString());
            // Test for null to avoid issues during local testing

            if (claim == null)
            {
                return false;
            }

            if (claim.Value.Equals("True"))
            {
                return true;
            }

            return false;
        }


    }
}