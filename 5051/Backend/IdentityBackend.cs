using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;
using System.Web.Mvc;

namespace _5051.Backend
{
    public class IdentityBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile IdentityBackend instance;
        private static readonly object syncRoot = new Object();

        private IdentityBackend() { }

        public static IdentityBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new IdentityBackend();
                            SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
                        }
                    }
                }

                return instance;
            }
        }

        // Get the Datasource to use
        private static IIdentityInterface DataSource;


        /// <summary>
        /// Sets the Datasource to be Mock or SQL
        /// </summary>
        /// <param name="dataSourceEnum"></param>
        public static void SetDataSource(DataSourceEnum dataSourceEnum)
        {
            switch (dataSourceEnum)
            {
                case DataSourceEnum.SQL:
                    break;

                case DataSourceEnum.Local:
                case DataSourceEnum.ServerLive:
                case DataSourceEnum.ServerTest:
                    DataSourceBackendTable.Instance.SetDataSourceServerMode(dataSourceEnum);
                    DataSource = IdentityDataSourceTable.Instance;
                    break;

                case DataSourceEnum.Mock:
                default:
                    // Default is to use the Mock
                    DataSource = IdentityDataSourceMockV2.Instance;
                    break;
            }

        }

        /// <summary>
        /// Switch the data set between Demo, Default and Unit Test
        /// </summary>
        /// <param name="SetEnum"></param>
        public static void SetDataSourceDataSet(DataSourceDataSetEnum SetEnum)
        {
            DataSource.LoadDataSet(SetEnum);
        }

        public ApplicationUser CreateNewSupportUser(string userName, string password, string id)
        {
            var result = DataSource.CreateNewSupportUser(userName, password, id);
            return result;
        }

        public ApplicationUser CreateNewTeacher(string teacherName, string teacherPassword, string teacherId)
        {
            var myReturn = DataSource.CreateNewTeacher(teacherName, teacherPassword, teacherId);
            return myReturn;
        }


        public StudentModel CreateNewStudentUser(StudentModel student)
        {
            var myReturn = DataSource.CreateNewStudent(student);
            return myReturn;
        }

        public StudentModel CreateNewStudentUserIdRecordOnly(StudentModel student, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown)
        {
            var myReturn = student;

            var findResult = FindUserByID(student.Id);
            if (findResult == null)
            {
                myReturn = DataSource.CreateNewStudentUserIdRecordOnly(student);
            }

            return myReturn;
        }

        public List<ApplicationUser> ListAllStudentUsers()
        {
            var result = DataSource.ListAllStudentUsers();
            return result;
        }

        public List<ApplicationUser> ListAllTeacherUsers()
        {
            return DataSource.ListAllTeacherUsers();
        }

        public List<ApplicationUser> ListAllSupportUsers()
        {
            return DataSource.ListAllSupportUsers();
        }

        public List<ApplicationUser> ListAllUsers()
        {
            return DataSource.ListAllUsers();
        }

        public ApplicationUser FindUserByID(string id)
        {
            var findResult = DataSource.FindUserByID(id);
            return findResult;
        }

        public bool UserHasClaimOfType(string userID, _5051.Models.UserRoleEnum role)
        {
            var myReturn = DataSource.UserHasClaimOfType(userID, role);
            return myReturn;
        }

        public ApplicationUser AddClaimToUser(string userID, string claimTypeToAdd, string claimValueToAdd)
        {
            var myReturn = DataSource.AddClaimToUser(userID, claimTypeToAdd, claimValueToAdd);
            return myReturn;
        }


        public bool RemoveClaimFromUser(string userID, string claimTypeToRemove)
        {
            var myReturn = DataSource.RemoveClaimFromUser(userID, claimTypeToRemove);
            return myReturn;
        }

        public bool DeleteUser(string Id)
        {
            var myReturn = DataSource.DeleteUser(Id);
            return myReturn;
        }

        public bool DeleteUserIdRecordOnly(string Id)
        {
            var myReturn = DataSource.DeleteUserIdRecordOnly(Id);
            return myReturn;
        }

        public bool LogUserIn(string userName, string password, _5051.Models.UserRoleEnum role, HttpContextBase context)
        {
            var myReturn = DataSource.LogUserIn(userName, password, role, context);
            return myReturn;
        }

        public bool BlockAccess(string userId, string requestedId, HttpContextBase context)
        {
            var result = DataSource.BlockAccess(userId, requestedId, context);
            return result;
        }

        public string GetCurrentStudentID(HttpContextBase context)
        {
            var result = DataSource.GetCurrentStudentID(context);
            return result;
        }

        public bool LogUserOut(HttpContextBase context)
        {
            var result = DataSource.LogUserOut(context);
            return result;
        }

        public bool ChangeUserName(string userId, string newName)
        {

            return DataSource.ChangeUserName(userId, newName);
        }

        public bool ChangeUserPassword(string userName, string newPass, string oldPass, _5051.Models.UserRoleEnum role)
        {
            return DataSource.ChangeUserPassword(userName, newPass, oldPass, role);
        }

        public void Reset()
        {
            DataSource.Reset();
        }


        /// <summary>
        /// checks if the provided id should be blocked from access based on the input role
        /// if the id is in the role, block access and return true
        /// if the id is not in the role, don't block access and return true
        /// </summary>
        /// <param name="CurrentId"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public bool BlockExecptForRole(string CurrentId, UserRoleEnum userRole)
        {
            if (DataSourceBackend.GetTestingMode())
            {
                // Allow anyone in for testing...
                return false; 
            }

            // Check that the passed in ID, is in the roll specified...
            var data = DataSource.FindUserByID(CurrentId);

            var result = DataSource.UserHasClaimOfType(CurrentId, userRole);

            // If the user has the claim, then they blocked, else they are OK.
            if (result)
            {
                return false;
            }

            // User does not have the claim, so return true...
            return true;
        }

        public ApplicationUser FindUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }

            var myReturn = DataSource.FindUserByUserName(userName);
            return myReturn;
        }

        /// <summary>
        /// Backup the Data from Source to Destination
        /// </summary>
        /// <param name="dataSourceSource"></param>
        /// <param name="dataSourceDestination"></param>
        /// <returns></returns>
        public bool BackupData(DataSourceEnum dataSourceSource, DataSourceEnum dataSourceDestination)
        {
            var result = DataSource.BackupData(dataSourceSource, dataSourceDestination);
            return result;
        }
    }
}