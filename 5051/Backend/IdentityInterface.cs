using System.Collections.Generic;
using System.Web;
using _5051.Models;

namespace _5051.Backend
{
    public interface IIdentityInterface
    {
        ApplicationUser CreateNewSupportUser(string userName, string password, string supportId);

        ApplicationUser CreateNewTeacher(string teacherName, string teacherPassword, string teacherId);

        StudentModel CreateNewStudent(StudentModel student);

        StudentModel CreateNewStudentUserIdRecordOnly(StudentModel student, DataSourceEnum dataSourceEnum = DataSourceEnum.Unknown);

        bool ChangeUserName(string userId, string newName);

        bool ChangeUserPassword(string userName, string newPass, string oldPass, _5051.Models.UserRoleEnum role);

        ApplicationUser FindUserByUserName(string userName);

        ApplicationUser FindUserByID(string id);

        StudentModel GetStudentById(string id);

        List<ApplicationUser> ListAllUsers();

        List<ApplicationUser> ListAllStudentUsers();

        List<ApplicationUser> ListAllTeacherUsers();

        List<ApplicationUser> ListAllSupportUsers();

        bool UserHasClaimOfType(string userID, _5051.Models.UserRoleEnum role);

        ApplicationUser AddClaimToUser(string userID, string claimTypeToAdd, string claimValueToAdd);

        bool RemoveClaimFromUser(string userID, string claimTypeToRemove);

        bool DeleteUser(string id);

        bool DeleteUserIdRecordOnly(string Id);

        bool LogUserIn(string userName, string password, _5051.Models.UserRoleEnum role, HttpContextBase context);

        bool BlockAccess(string userId, string requestedId, HttpContextBase context);

        string GetCurrentStudentID(HttpContextBase context);

        bool LogUserOut(HttpContextBase context);

        bool CreateCookie(string cookieName, string cookieValue, HttpContextBase context);

        string ReadCookieValue(string cookieName, HttpContextBase context);

        bool DeleteCookie(string cookiename, HttpContextBase context);

        void Reset();

        void LoadDataSet(DataSourceDataSetEnum setEnum);

        bool BackupData(DataSourceEnum dataSourceSource, DataSourceEnum dataSourceDestination);
    }
}