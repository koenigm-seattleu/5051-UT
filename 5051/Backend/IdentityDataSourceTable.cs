using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace _5051.Backend
{
    public class IdentityDataSourceTable : IIdentityInterface
    {
        public string supportUserName = "su5051";
        public string supportPass = "su5051";
        public string teacherUserName = "teacher";
        public string teacherPass = "teacher";

        private static volatile IdentityDataSourceTable instance;
        private static readonly object syncRoot = new Object();

        private IdentityDataSourceTable() { }

        public static IdentityDataSourceTable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new IdentityDataSourceTable();
                        //instance.DataSetDefault();
                        instance.Initialize();
                    }
                }

                return instance;
            }
        }



        private List<ApplicationUser> DataList = new List<ApplicationUser>();

        private const string ClassName = "IdentityModel";

        private readonly string tableName = ClassName.ToLower();

        private readonly string partitionKey = ClassName.ToLower();



        /// <summary>
        /// Creates a new Support User
        /// returns the newly created user
        /// returns null if failed
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ApplicationUser CreateNewSupportUser(string userName, string password, string supportId)
        {
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

            var myResult = Create(user);

            return myResult;
        }


        /// <summary>
        /// Create to write the data to disk
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dataSourceEnum"></param>
        /// <returns></returns>
        public ApplicationUser Create(ApplicationUser user, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            if (dataSourceEnum == DataSourceEnum.Unknown)
            {
                DataList.Add(user);
            }

            //add to storage
            var myResult = DataSourceBackendTable.Instance.Create<ApplicationUser>(tableName, partitionKey, user.Id, user, dataSourceEnum);

            return user;
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

            //need to add claims
            user.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = _5051.Models.UserRoleEnum.TeacherUser.ToString(),
                ClaimValue = "True"
            });

            var myResult = Create(user);

            return myResult;
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
        public StudentModel CreateNewStudent(StudentModel student)
        {
            var createResult = StudentBackend.Instance.Create(student);

            if (createResult == null)
            {
                return null;
            }

            var idResult = CreateNewStudentUserIdRecordOnly(student);

            return idResult;
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

            var myResult = Create(user);

            return student;
        }

        ///// <summary>
        ///// updates all fields in the database except the id
        ///// returns false if update fails
        ///// </summary>
        ///// <param name="student"></param>
        ///// <returns></returns>
        //public bool UpdateStudent(StudentModel student)
        //{
        //    if (student == null)
        //    {
        //        return false;
        //    }

        //    var idFindResult = FindUserByID(student.Id);

        //    if (idFindResult == null)
        //    {
        //        return false;
        //    }

        //    var findStudent = GetStudentById(student.Id);
        //    if(findStudent == null)
        //    {
        //        return false;
        //    }

        //    findStudent.Name = student.Name;
        //    if(student.Password != null)
        //    {
        //        findStudent.Password = student.Password;
        //    }

        //    var updateResult = DataSourceBackendTable.Instance.Update("studentmodel", "student", idFindResult.Id, findStudent);
        //    if(updateResult == null)
        //    {
        //        return false;
        //    }

        //    if(student.Name != idFindResult.UserName)
        //    {
        //        idFindResult.UserName = student.Name;

        //        var tableUpdateResult = DataSourceBackendTable.Instance.Update(tableName, partitionKey, idFindResult.Id, idFindResult);
        //        return true;
        //    }

        //    return true;
        //}

        public bool ChangeUserName(string userId, string newName)
        {
            if (userId == null || newName == null)
            {
                return false;
            }

            var idFind = FindUserByID(userId);
            if (idFind == null)
            {
                return false;
            }

            var studentFind = GetStudentById(userId);
            if (studentFind == null)
            {
                return false;
            }

            studentFind.Name = newName;

            //update List
            DataList.Remove(idFind);
            idFind.UserName = newName;
            DataList.Add(idFind);

            //update id and student storage
            var studentTableUpdate = DataSourceBackendTable.Instance.Update("studentmodel", "student", userId, studentFind);
            if (studentTableUpdate == null)
            {
                return false;
            }
            var idTableUpdate = DataSourceBackendTable.Instance.Update(tableName, partitionKey, userId, idFind);
            if (idFind == null)
            {
                return false;
            }

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
                if (oldPass != student.Password)
                {
                    return false;
                }

                student.Password = newPass;
                //var updateResult = UpdateStudent(student);
                var updateResult = DataSourceBackend.Instance.StudentBackend.Update(student);
                if (updateResult != null)
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Finds and returns the user using the given user name
        /// if user does not exist, returns null
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ApplicationUser FindUserByUserName(string userName)
        {
            //var DataSetList = DataSourceBackendTable.Instance.LoadAll<ApplicationUser>(tableName, partitionKey);

            foreach (var item in DataList)
            {
                if (item.UserName == userName)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds and returns the user using the given id
        /// if user does not exist, returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationUser FindUserByID(string id)
        {
            //var DataSetList = DataSourceBackendTable.Instance.LoadAll<ApplicationUser>(tableName, partitionKey);

            foreach (var item in DataList)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public StudentModel GetStudentById(string id)
        {
            var studentResult = DataSourceBackend.Instance.StudentBackend.Read(id);

            if (studentResult == null)
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
            return DataList;
        }

        /// <summary>
        /// Lists all the student users
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Lists all the teacher user
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// lists all the support users
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// checks if user has the given claim type and value
        /// returns false if not
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claimType"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
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
                if (item.ClaimType == claimType)
                {
                    return true;
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

            if (findResult == null)
            {
                return null;
            }

            findResult.Claims.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim
            {
                ClaimType = claimTypeToAdd,
                ClaimValue = claimValueToAdd
            });


            //storage update
            var updateResult = DataSourceBackendTable.Instance.Update(tableName, partitionKey, userID, findResult);

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

            var updateResult = DataSourceBackendTable.Instance.Update(tableName, partitionKey, userID, findResult);

            return true;
        }

        /// <summary>
        /// Deletes the user with the given id
        /// returns false if delete fails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Delete(string Id, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }
            
            // If using the defaul data source, use it, else just do the table operation
            if (dataSourceEnum == DataSourceEnum.Unknown)
            {
                var data = DataList.Find(n => n.Id == Id);
                if (data == null)
                {
                    return false;
                }

                if (DataList.Remove(data) == false)
                {
                    return false;
                }
            }

            // Storage Delete
            var myReturn = DataSourceBackendTable.Instance.Delete<ApplicationUser>(tableName, partitionKey, Id, dataSourceEnum);

            return myReturn;
        }


        /// <summary>
        /// Deletes the user with the given id
        /// returns false if delete fails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool DeleteUser(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var data = DataList.Find(n => n.Id == Id);
            if (data == null)
            {
                return false;
            }

            var myReturn = Delete(Id);

            if (UserHasClaimOfType(data.Id, UserRoleEnum.StudentUser))
            {
                //delete the student from student table as well
                var deleteResult = DataSourceBackend.Instance.StudentBackend.Delete(data.Id);
            }

            return myReturn;
        }

        public bool DeleteUserIdRecordOnly(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myReturn = Delete(Id);

            return myReturn;
        }

        /// <summary>
        /// Logs the user in using the given password
        /// returns false if login fails
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
            var cookieId = ReadCookieValue("id", context);

            if (cookieId == null)
            {
                return null;
            }

            return cookieId;
        }

        /// <summary>
        /// logs the currently logged in user out
        /// </summary>
        /// <returns></returns>
        public bool LogUserOut(HttpContextBase context)
        {
            string cookieName = "id";

            var result = DeleteCookie(cookieName, context);

            return result;
        }

        public bool CreateCookie(string cookieName, string cookieValue, HttpContextBase context)
        {
            if (cookieName == null || cookieValue == null)
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

            // Storage Load all rows
            var DataSetList = LoadAll();

            foreach (var item in DataSetList)
            {
                DataList.Add(item);
            }

            // If Storage is Empty, then Create.
            if (DataList.Count < 1)
            {
                CreateDataSetDefault();
            }
        }


        /// <summary>
        /// Load all the records from the datasource
        /// </summary>
        /// <param name="dataSourceEnum"></param>
        /// <returns></returns>
        public List<ApplicationUser> LoadAll(DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            var DataSetList = DataSourceBackendTable.Instance.LoadAll<ApplicationUser>(tableName, partitionKey, true, dataSourceEnum);

            return DataSetList;
        }

        private void CreateDataSetDefault()
        {
            supportUserName = "su5051";
            supportPass = "su5051";
            teacherUserName = "teacher";
            teacherPass = "teacher";

            //create support user
            var supportResult = CreateNewSupportUser(supportUserName, supportPass, supportUserName);

            //create teacher user
            var teacherResult = CreateNewTeacher(teacherUserName, teacherPass, teacherUserName);

            //create the student users
            //student backend does this

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
        /// Backup the Data from Source to Destination
        /// </summary>
        /// <param name="dataSourceSource"></param>
        /// <param name="dataSourceDestination"></param>
        /// <returns></returns>
        public bool BackupData(DataSourceEnum dataSourceSource, DataSourceEnum dataSourceDestination)
        {
            // Read all the records from the Source using current database defaults

            var DataAllSource = LoadAll(dataSourceSource);
            if (DataAllSource == null || !DataAllSource.Any())
            {
                return false;
            }

            // Empty out Destination Table
            // Get all rows in the destination Table
            // Walk and delete each item, because delete table takes too long...
            var DataAllDestination = LoadAll(dataSourceDestination);
            if (DataAllDestination == null)
            {
                return false;
            }

            foreach (var data in DataAllDestination)
            {
                Delete(data.Id, dataSourceDestination);
            }

            // Write the data to the destination
            foreach (var data in DataAllSource)
            {
                Create(data, dataSourceDestination);
            }

            return true;
        }
    }
}