using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;

namespace _5051.Backend
{
    public class IdentityDataSourceMockV2 : IIdentityInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile IdentityDataSourceMockV2 instance;
        private static readonly object syncRoot = new Object();

        private IdentityDataSourceMockV2() { }

        public static IdentityDataSourceMockV2 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new IdentityDataSourceMockV2();                         
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        private List<ApplicationUser> DataList = new List<ApplicationUser>();

        public string supportUserName = "su5051";
        public string supportPass = "su5051";
        public string teacherUserName = "teacher";
        public string teacherPass = "teacher";

        public ApplicationUser CreateNewSupportUser(string userName, string password, string supportId)
        {
            //fill in all fields needed
            var user = new ApplicationUser { UserName = userName, Email = userName + "@seattleu.edu", Id = supportId };

            //need to add claims
            user.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = _5051.Models.UserRoleEnum.SupportUser.ToString(),
                ClaimValue = "True"
            });
            user.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = _5051.Models.UserRoleEnum.TeacherUser.ToString(),
                ClaimValue = "True"
            });

            DataList.Add(user);

            return user;
        }

        public ApplicationUser CreateNewTeacher(string teacherName, string teacherPassword, string teacherId)
        {
            //fill in all fields needed
            var user = new ApplicationUser { UserName = teacherName, Email = teacherName + "@seattleu.edu", Id = teacherId };

            //need to add claims
            user.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = _5051.Models.UserRoleEnum.TeacherUser.ToString(),
                ClaimValue = "True"
            });

            DataList.Add(user);

            return user;
        }

        public StudentModel CreateNewStudent(StudentModel student)
        {
            //add to student list
            var createStudent = StudentBackend.Instance.Create(student);

            if (createStudent == null)
            {
                return null;
            }

            var createId = CreateNewStudentUserIdRecordOnly(student);

            return student;
        }

        public StudentModel CreateNewStudentUserIdRecordOnly(StudentModel student, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            //fill in all fields needed
            var user = new ApplicationUser { UserName = student.Name, Email = student.Name + "@seattleu.edu", Id = student.Id };

            //need to add claims
            user.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = _5051.Models.UserRoleEnum.StudentUser.ToString(),
                ClaimValue = "True"
            });

            DataList.Add(user);

            return student;
        }

        public bool ChangeUserName(string userId, string newName)
        {
            if (userId == null || newName == null)
            {
                return false;
            }
            var idFindStudent = FindUserByID(userId);
            if (idFindStudent == null)
            {
                return false;
            }

            var findStudent = GetStudentById(userId);
            if (findStudent == null)
            {
                return false;
            }

            //update for both student and id
            findStudent.Name = newName;

            var studentUpdateResult = StudentBackend.Instance.Update(findStudent);
            if (studentUpdateResult == null)
            {
                return false;
            }

            DataList.Remove(idFindStudent);
            idFindStudent.UserName = newName;
            DataList.Add(idFindStudent);

            return true;
        }

        public bool ChangeUserPassword(string userName, string newPass, string oldPass, _5051.Models.UserRoleEnum role)
        {
            var findResult = FindUserByUserName(userName);
            if (findResult == null)
            {
                return false;
            }

            if (role == _5051.Models.UserRoleEnum.TeacherUser && UserHasClaimOfType(findResult.Id, _5051.Models.UserRoleEnum.TeacherUser))
            {
                if (oldPass != teacherPass)
                {
                    return false;
                }

                teacherPass = newPass;
                return true;
            }

            if (role == _5051.Models.UserRoleEnum.SupportUser && UserHasClaimOfType(findResult.Id, _5051.Models.UserRoleEnum.SupportUser))
            {
                supportPass = newPass;
                return true;
            }

            if (role == _5051.Models.UserRoleEnum.StudentUser)
            {
                //var student = DataSourceBackend.Instance.StudentBackend.Read(findResult.Id);
                var student = GetStudentById(findResult.Id);

                if (student == null)
                {
                    return false;
                }
                if(oldPass != student.Password)
                {
                    return false;
                }

                student.Password = newPass;
                var updateResult = StudentBackend.Instance.Update(student);
                if (updateResult != null)
                {
                    return true;
                }
            }

            return false;
        }

        public ApplicationUser FindUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }

            var myReturn = DataList.Find(n => n.UserName == userName);
            return myReturn;
        }

        public ApplicationUser FindUserByID(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = DataList.Find(n => n.Id == id);
            return myReturn;
        }

        public StudentModel GetStudentById(string id)
        {
            var student = StudentBackend.Instance.Read(id);

            if (student == null)
            {
                return null;
            }

            return student;
        }

        public List<ApplicationUser> ListAllUsers()
        {
            return DataList;
        }

        public List<ApplicationUser> ListAllStudentUsers()
        {
            var myReturn = new List<ApplicationUser>() { };

            foreach (var user in DataList)
            {
                if (UserHasClaimOfType(user.Id, _5051.Models.UserRoleEnum.StudentUser))
                {
                    myReturn.Add(user);
                }
            }

            return myReturn;
        }

        public List<ApplicationUser> ListAllTeacherUsers()
        {
            var myReturn = new List<ApplicationUser>() { };

            foreach (var user in DataList)
            {
                if (UserHasClaimOfType(user.Id, _5051.Models.UserRoleEnum.TeacherUser))
                {
                    myReturn.Add(user);
                }
            }

            return myReturn;
        }

        public List<ApplicationUser> ListAllSupportUsers()
        {
            var myReturn = new List<ApplicationUser>() { };

            foreach (var user in DataList)
            {
                if (UserHasClaimOfType(user.Id, _5051.Models.UserRoleEnum.SupportUser))
                {
                    myReturn.Add(user);
                }
            }

            return myReturn;
        }

        public bool UserHasClaimOfType(string userID, _5051.Models.UserRoleEnum role)
        {
            var findResult = FindUserByID(userID);
            if (findResult == null)
            {
                return false;
            }

            var claims = findResult.Claims.ToList();
            var claimType = role.ToString();

            foreach (var item in claims)
            {
                //if (item.ClaimType == claimType && item.ClaimValue == claimValue)
                if(item.ClaimType == claimType)
                {
                    return true;
                }
            }

            return false;
        }

        public ApplicationUser AddClaimToUser(string userID, string claimTypeToAdd, string claimValueToAdd)
        {
            var findResult = FindUserByID(userID);

            if (findResult == null)
            {
                return null;
            }

            findResult.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = claimTypeToAdd,
                ClaimValue = claimValueToAdd
            });

            return findResult;
        }

        public bool RemoveClaimFromUser(string userID, string claimTypeToRemove)
        {
            var findResult = FindUserByID(userID);
            if (findResult == null)
            {
                return false;
            }

            var claims = findResult.Claims.ToList();

            var lastAccessedClaim = claims.FirstOrDefault(t => t.ClaimType == claimTypeToRemove);

            var resultDelete = findResult.Claims.Remove(lastAccessedClaim);

            if (!resultDelete)
            {
                return false;
            }

            return true;
        }

        public bool DeleteUser(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myData = DataList.Find(n => n.Id == Id);
            if(myData == null)
            {
                return false;
            }

            if (UserHasClaimOfType(myData.Id, _5051.Models.UserRoleEnum.StudentUser))
            {
                //delete the student from student table as well
                var deleteResult = StudentBackend.Instance.Delete(myData.Id);

                return true;
            }

            //remove from list
            if (DataList.Remove(myData) == false)
            {
                return false;
            }

            return true;
        }

        public bool DeleteUserIdRecordOnly(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myData = DataList.Find(n => n.Id == Id);
            if (myData == null)
            {
                return false;
            }

            //remove from list
            if (DataList.Remove(myData) == false)
            {
                return false;
            }

            return true;
        }

        public bool LogUserIn(string userName, string password, _5051.Models.UserRoleEnum role, HttpContextBase context)
        {
            if (userName == null && password == null)
            {
                return false;
            }

            var findResult = FindUserByUserName(userName);
            if (findResult == null)
            {
                return false;
            }

            //check that role is correct
            if (role == _5051.Models.UserRoleEnum.SupportUser)
            {
                if (!UserHasClaimOfType(findResult.Id, _5051.Models.UserRoleEnum.SupportUser))
                {
                    return false;
                }

                if (password == supportPass)
                {
                    var logOutResult = LogUserOut(context);

                    var cookieResult = CreateCookie("id", supportUserName, context);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (role == _5051.Models.UserRoleEnum.TeacherUser)
            {
                if (!UserHasClaimOfType(findResult.Id, _5051.Models.UserRoleEnum.TeacherUser))
                {
                    return false;
                }

                if (password == teacherPass)
                {
                    var logOutResult = LogUserOut(context);

                    var cookieResult = CreateCookie("id", teacherUserName, context);

                    return true;
                }
                else
                {
                    return false;
                }
            }

            var student = GetStudentById(findResult.Id);
            if (student != null && student.Password == password)
            {

                var logOutResult = LogUserOut(context);

                var cookieResult = CreateCookie("id", student.Id, context);

                return true;
            }

            return false;
        }


        /// <summary>
        /// Uses cookies to determine if the user id has access to a requested id
        /// returns true if denied access
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BlockAccess(string userId, string requestedId, HttpContextBase context)
        {
            if (string.IsNullOrEmpty(requestedId))
            {
                return true;
            }

            var requestedFindResult = FindUserByID(requestedId);

            if (requestedFindResult == null)
            {
                return true;
            }

            if (userId == null)
            {
                return true;
            }

            var userFindResult = FindUserByID(userId);

            if (userFindResult == null)
            {
                return true;
            }

            if (userId != requestedId)
            {
                return true;
            }

            var cookieId = ReadCookieValue("id", context);

            if (cookieId == null)
            {
                return true;
            }

            if (cookieId != userId)
            {
                return true;
            }

            //if made it this far, valid user
            return false;
        }        


        /// <summary>
        /// get the ID of the currently logged in student
        /// returns null if no student logged in
        /// </summary>
        /// <returns></returns>
        public string GetCurrentStudentID(HttpContextBase context)
        {
            //var wrapper = new HttpContextWrapper(HttpContext.Current);

            var cookieId = ReadCookieValue("id", context);

            if(cookieId == null)
            {
                return null;
            }

            return cookieId;
        }


        public bool LogUserOut(HttpContextBase context)
        {
            string cookieName = "id";
            
            var result = DeleteCookie(cookieName, context);

            return result;
        }

        public bool CreateCookie(string cookieName, string cookieValue, HttpContextBase context)
        {
            if(cookieName == null || cookieValue == null)
            {
                return false;
            }

            HttpCookie aCookie = new HttpCookie(cookieName)
            {
                Value = cookieValue,
                Expires = DateTime.Now.AddDays(1)
            };
            context.Response.Cookies.Add(aCookie);

            return true;
        }

        public string ReadCookieValue(string cookieName, HttpContextBase context)
        {
            if (context.Request.Cookies[cookieName] == null)
            {
                return null;
            }

            HttpCookie aCookie = context.Request.Cookies[cookieName];
            var cookieValue = context.Server.HtmlEncode(aCookie.Value);

            return cookieValue;
        }

        public bool DeleteCookie(string cookieName, HttpContextBase context)
        {
            if (context.Request.Cookies[cookieName] == null)
            {
                return false;
            }
            
            HttpCookie aCookie = context.Request.Cookies[cookieName];
            var cookieId = context.Server.HtmlEncode(aCookie.Value);
            //delete the cookie with id info
            aCookie = new HttpCookie(cookieName)
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            context.Response.Cookies.Add(aCookie);

            return true;
        }

        public void Reset()
        {
            Initialize();
        }

        public void Initialize()
        {
            LoadDataSet(DataSourceDataSetEnum.Default);
        }

        private void DataSetClear()
        {
            DataList.Clear();
        }

        private void DataSetDefault()
        {
            DataSetClear();

            supportUserName = "su5051";
            supportPass = "su5051";
            teacherUserName = "teacher";
            teacherPass = "teacher";

            //create support user
            var supportResult = CreateNewSupportUser(supportUserName, supportPass, supportUserName);

            //create teacher user
            var teacherResult = CreateNewTeacher(teacherUserName, teacherPass, teacherUserName);
        }


        /// <summary>
        /// Data set for demo
        /// </summary>
        private void DataSetDemo()
        {
            DataSetDefault();
        }

        /// <summary>
        /// Unit Test data set
        /// </summary>
        private void DataSetUnitTest()
        {
            DataSetDefault();
        }

        /// <summary>
        /// Set which data to load
        /// </summary>
        /// <param name="setEnum"></param>
        public void LoadDataSet(DataSourceDataSetEnum setEnum)
        {
            switch (setEnum)
            {
                case DataSourceDataSetEnum.Demo:
                    DataSetDemo();
                    break;

                case DataSourceDataSetEnum.UnitTest:
                    DataSetUnitTest();
                    break;

                case DataSourceDataSetEnum.Default:
                default:
                    DataSetDefault();
                    break;
            }
        }

        /// <summary>
        /// Not implemented for Mock
        /// </summary>
        /// <param name="dataSourceSource"></param>
        /// <param name="dataSourceDestination"></param>
        /// <returns></returns>
        public bool BackupData(DataSourceEnum dataSourceSource, DataSourceEnum dataSourceDestination)
        {
            return true;
        }
    }
}