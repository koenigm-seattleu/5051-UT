using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;
using Moq;
using System.Web;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class IdentityDataSourceMockUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Backend_IdentityDataSourceMock_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;

            // Act
            var result = backend;

            //Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region ChangeUserName
        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangeUserName_Valid_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectId = backend.ListAllStudentUsers().FirstOrDefault().Id;
            var expectNewName = "testName";

            //act
            var result = backend.ChangeUserName(expectId, expectNewName);
            var resultName = backend.FindUserByID(expectId).UserName;
            var resultStudentName = DataSourceBackend.Instance.StudentBackend.Read(expectId).Name;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(expectNewName, resultName, TestContext.TestName);
            Assert.AreEqual(expectNewName, resultStudentName, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangeUserName_Invalid_Null_New_Name_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectId = backend.ListAllSupportUsers().FirstOrDefault().Id;

            //act
            var result = backend.ChangeUserName(expectId, null);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangeUserName_Invalid_User_Id_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectId = "badID";
            var expectNewName = "testName";

            //act
           var result = backend.ChangeUserName(expectId, expectNewName);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }
        #endregion

        #region ChangePassword
        //right now this test isn't reseting properly
        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangePassword_Valid_Student_Should_Pass()
        {
            //arrange
            var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var backend = IdentityDataSourceMockV2.Instance;
            var expectName = expectStudent.Name;
            var expectNewPass = "goodPassword";
            var expectOldPass = expectStudent.Password;

            //act
            var result = backend.ChangeUserPassword(expectName, expectNewPass, expectOldPass, _5051.Models.UserRoleEnum.StudentUser);
            var passwordResult = expectStudent.Password;

            //Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(expectNewPass, passwordResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangePassword_Valid_Teacher_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectName = backend.teacherUserName;
            var expectNewPass = "goodPassword";
            var expectOldPass = backend.teacherPass;

            //act
            var result = backend.ChangeUserPassword(expectName, expectNewPass, expectOldPass, _5051.Models.UserRoleEnum.TeacherUser);
            var passwordResult = backend.teacherPass;

            //Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(expectNewPass, passwordResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangePassword_Valid_Support_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectName = backend.supportUserName;
            var expectNewPass = "goodPassword";
            var expectOldPass = backend.supportPass;

            //act
            var result = backend.ChangeUserPassword(expectName, expectNewPass, expectOldPass, _5051.Models.UserRoleEnum.SupportUser);
            var passwordResult = backend.supportPass;

            //Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(expectNewPass, passwordResult, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ChangePassword_Invalid_User_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectName = "badName";
            var expectNewPass = "goodPassword";
            var expectOldPass = "bad";

            //act
            var result = backend.ChangeUserPassword(expectName, expectNewPass, expectOldPass, _5051.Models.UserRoleEnum.SupportUser);
            var passwordResult = backend.supportPass;

            //Reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }
        #endregion

        #region Claims
        [TestMethod]
        public void Backend_IdentityDataSourceMock_UserHasClaimOfValue_Invalid_User_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;

            //act
            var result = backend.UserHasClaimOfType(null, _5051.Models.UserRoleEnum.Unknown);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_AddClaimToUser_Invalid_User_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;

            //act
            var result = backend.AddClaimToUser(null, null, null);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_RemoveClaimFromUser_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectId = "su5051";
            var expectClaimType = "test";
            var expectClaimValue = "True";
            var addClaimResult = backend.AddClaimToUser(expectId, expectClaimType, expectClaimValue);
            var expectNumClaimsBefore = backend.FindUserByID(expectId).Claims.Count;

            //act
            var result = backend.RemoveClaimFromUser(expectId, expectClaimType);
            var resultClaimNum = backend.FindUserByID(expectId).Claims.Count;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(expectNumClaimsBefore-1, resultClaimNum, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_RemoveClaimFromUser_Invalid_user_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;

            //act
            var result = backend.RemoveClaimFromUser(null, null);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_RemoveClaimFromUser_Invalid_Claim_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectId = "su5051";
            var expectInvalidClaim = "test";

            //act
            var result = backend.RemoveClaimFromUser(expectId, expectInvalidClaim);

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }
        #endregion

        #region Create
        [TestMethod]
        public void Backend_IdentityDataSourceMock_CreateNewStudent_SHould_Pass()
        {
            //arrange
            var expectName = "testName";
            var backend = IdentityDataSourceMockV2.Instance;
            var testStudent = new StudentModel
            {
                Name = expectName
            };
            var studentCountBefore = backend.ListAllStudentUsers().Count;

            //act
            var result = backend.CreateNewStudent(testStudent);
            var studentCountAfter = backend.ListAllStudentUsers().Count;

            //reset
            DataSourceBackend.Instance.Reset();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
            Assert.AreEqual(expectName, result.Name, TestContext.TestName);
        }
        #endregion

        #region Delete
        [TestMethod]
        public void Backend_IdentityDataSourceMock_DeleteUser_Valid_Should_Pass()
        {
            // Arrange
            var dataBackend = DataSourceBackend.Instance;
            var studentBackend = dataBackend.StudentBackend;
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserId = backend.ListAllStudentUsers().FirstOrDefault().Id;
            var numUsersBefore = backend.ListAllUsers().Count;

            // Act
            var result = backend.DeleteUser(expectUserId);
            var numUsersAfter = backend.ListAllUsers().Count;

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsTrue(result, TestContext.TestName);
            Assert.AreEqual(numUsersBefore-1, numUsersAfter, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_DeleteUser_Null_ID_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;

            // Act
            var result = backend.DeleteUser(null);

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_DeleteUser_Id_Invalid_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserId = "bogus";

            // Act
            var result = backend.DeleteUser(expectUserId);

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }
        #endregion

        #region FindUser
        [TestMethod]
        public void Backend_IdentityDataSourceMock_FindByUserName_Valid_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "Mike";

            //act
            var result = backend.FindUserByUserName(expectUserName);

            //assert
            Assert.AreEqual(expectUserName, result.UserName, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_FindByUserName_Null_Name_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            string expectUserName = null;

            //act
            var result = backend.FindUserByUserName(expectUserName);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_FindByUserId_Null_ID_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            string expectId = null;

            //act
            var result = backend.FindUserByID(expectId);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_GetStudentById_Invalid_Id_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;

            // Act
            var result = backend.GetStudentById(null);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion

        #region LoadDataSet
        [TestMethod]
        public void Backend_IdentityDataSourceMock_LoadDataSet_Demo_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectEnum = DataSourceDataSetEnum.Demo;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LoadDataSet_UnitTest_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion

        #region ListUsers
        [TestMethod]
        public void Backend_IdentityDataSourceMock_ListAllUsers_Should_Pass()
        {
            //arrange
            DataSourceBackend.Instance.Reset();
            var dataBackend = DataSourceBackend.Instance;
            var backend = IdentityDataSourceMockV2.Instance;
            var expectNumUsers = 7;

            //act
            var result = backend.ListAllUsers();

            //assert
            Assert.AreEqual(expectNumUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ListAllSupportUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectNumUsers = 1;

            //act
            var result = backend.ListAllSupportUsers();

            //assert
            Assert.AreEqual(expectNumUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ListAllTeacherUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectNumUsers = 2;

            //act
            var result = backend.ListAllTeacherUsers();

            //assert
            Assert.AreEqual(expectNumUsers, result.Count, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ListAllStudentUsers_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectNumUsers = 5;

            //act
            var result = backend.ListAllStudentUsers();

            //assert
            Assert.AreEqual(expectNumUsers, result.Count, TestContext.TestName);
        }
        #endregion

        #region Login
        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Valid_Support_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "su5051";
            var expectPassword = "su5051";

            var context = CreateMoqSetupForCookie();

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, _5051.Models.UserRoleEnum.SupportUser, context);

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Valid_Teacher_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "teacher";
            var expectPassword = "teacher";

            var context = CreateMoqSetupForCookie();

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, _5051.Models.UserRoleEnum.TeacherUser, context);

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Valid_Student_Should_Pass()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "Mike";
            var expectPassword = "Mike";

            var testCookieName = "id";
            var testCookieValue = "testID";
            HttpCookie testCookie = new HttpCookie(testCookieName)
            {
                Value = testCookieValue,
                Expires = DateTime.Now.AddSeconds(30)
            };

            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);

            var mockedRequest = Mock.Get(context.Object.Request);
            mockedRequest.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());
            context.Object.Request.Cookies.Add(testCookie);

            var mockedResponse = Mock.Get(context.Object.Response);
            mockedResponse.Setup(r => r.Cookies).Returns(new HttpCookieCollection());

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, _5051.Models.UserRoleEnum.StudentUser, context.Object);

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Invalid_Null_Username_Password_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;

            // Act
            var result = backend.LogUserIn(null, null, _5051.Models.UserRoleEnum.SupportUser, null);

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserInIn_Invalid_UserName_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectPassword = "test";

            // Act
            var result = backend.LogUserIn(null, expectPassword, _5051.Models.UserRoleEnum.SupportUser, null);

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Invalid_Support_Bad_Password_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "su5051";
            var expectPassword = "badpassword";

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, _5051.Models.UserRoleEnum.SupportUser, null);

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Invalid_Support_User_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "teacher";
            var expectPassword = "teacher";

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, _5051.Models.UserRoleEnum.SupportUser, null);

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Invalid_Teacher_User_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "Mike";
            var expectPassword = "Mike";

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, _5051.Models.UserRoleEnum.TeacherUser, null);

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Invalid_Teacher_Password_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "teacher";
            var expectPassword = "badpassword";

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, _5051.Models.UserRoleEnum.TeacherUser, null);

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_LogUserIn_Invalid_Student_Password_Should_Fail()
        {
            // Arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var expectUserName = "Mike";
            var expectPassword = "badpassword";

            // Act
            var result = backend.LogUserIn(expectUserName, expectPassword, _5051.Models.UserRoleEnum.TeacherUser, null);

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_Logout_Should_Pass()
        { 
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var testCookieName = "id";
            var testCookieValue = "testID";
            HttpCookie testCookie = new HttpCookie(testCookieName)
            {
                Value = testCookieValue,
                Expires = DateTime.Now.AddSeconds(30)
            };

            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);

            var mockedRequest = Mock.Get(context.Object.Request);
            mockedRequest.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());
            context.Object.Request.Cookies.Add(testCookie);

            var mockedResponse = Mock.Get(context.Object.Response);
            mockedResponse.Setup(r => r.Cookies).Returns(new HttpCookieCollection());

            // Act
            var result = backend.LogUserOut(context.Object);

            // Assert
            Assert.IsTrue(result, TestContext.TestName);
        }
        #endregion

        #region cookies
        [TestMethod]
        public void Backend_IdentityDataSourceMock_DeleteCookie_Should_Pass()
        {     
            var testCookieName = "id";
            var testCookieValue = "Value";
            HttpCookie testCookie = new HttpCookie(testCookieName)
            {
                Value = testCookieValue,
                Expires = DateTime.Now.AddSeconds(30)
            };

            var backend = IdentityDataSourceMockV2.Instance;

            var context = CreateMoqSetupForCookie();

            context.Request.Cookies.Add(testCookie);

            //act
            var result = backend.DeleteCookie(testCookieName, context);

            //assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_DeleteCookie_Invalid_Cookie_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;

            var context = CreateMoqSetupForCookie();

            //act
            var result = backend.DeleteCookie(null, context);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_CreateCookie_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var testCookieName = "test";
            var testCookieValue = "value";

            var context = CreateMoqSetupForCookie();

            //act
            var result = backend.CreateCookie(testCookieName, testCookieValue, context);

            //assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_CreateCookie_Invalid_Name_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            string testCookieName = null;
            var testCookieValue = "value";

            var context = CreateMoqSetupForCookie();

            //act
            var result = backend.CreateCookie(testCookieName, testCookieValue, context);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_CreateCookie_Invalid_Value_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var testCookieName = "test";
            string testCookieValue = null;

            var context = CreateMoqSetupForCookie();

            //act
            var result = backend.CreateCookie(testCookieName, testCookieValue, context);

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ReadCookie_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var testCookieName = "test";
            var testCookieValue = "value";
            HttpCookie testCookie = new HttpCookie(testCookieName)
            {
                Value = testCookieValue,
                Expires = DateTime.Now.AddSeconds(30)
            };

            var context = CreateMoqSetupForCookie(testCookieValue);

            context.Request.Cookies.Add(testCookie);

            var createResult = backend.CreateCookie(testCookieName, testCookieValue, context);

            //act
            var result = backend.ReadCookieValue(testCookieName, context);

            //assert
            Assert.AreEqual(testCookieValue, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_ReadCookie_Cookie_Not_Found_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var testCookieName = "test";

            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);

            var mockedRequest = Mock.Get(context.Object.Request);
            mockedRequest.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());

            //act
            var result = backend.ReadCookieValue(testCookieName, context.Object);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }

        #endregion

        #region GetCurrentStudentID
        [TestMethod]
        public void Backend_IdentityDataSourceMock_GetCurrentStudentID_Should_Pass()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;

            var testCookieName = "id";
            var testCookieValue = "testID";
            HttpCookie testCookie = new HttpCookie(testCookieName)
            {
                Value = testCookieValue,
                Expires = DateTime.Now.AddSeconds(30)
            };

            var context = CreateMoqSetupForCookie(testCookieValue);

            context.Request.Cookies.Add(testCookie);

            var createResult = backend.CreateCookie(testCookieName, testCookieValue, context);

            //act
            var result = backend.GetCurrentStudentID(context);

            //assert
            Assert.AreEqual(testCookieValue, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_GetCurrentStudentID_No_Cookie_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;

            var context = CreateMoqSetupForCookie();

            //act
            var result = backend.GetCurrentStudentID(context);

            //assert
            Assert.IsNull(result, TestContext.TestName);
        }
        #endregion

        #region BlockAccess
        [TestMethod]
        public void Backend_IdentityDataSourceMock_BlockAccess_Should_Pass()
        {
            //arrange
            var dataSourceBackend = DataSourceBackend.Instance;
            var backend = IdentityDataSourceMockV2.Instance;

            var student = dataSourceBackend.StudentBackend.GetDefault();

            var testCookieName = "id";
            var testCookieValue = student.Id;
            HttpCookie testCookie = new HttpCookie(testCookieName)
            {
                Value = testCookieValue,
                Expires = DateTime.Now.AddSeconds(30)
            };

            var context = CreateMoqSetupForCookie(testCookieValue);

            context.Request.Cookies.Add(testCookie);

            var createResult = backend.CreateCookie(testCookieName, testCookieValue, context);

            //act
            var result = backend.BlockAccess(student.Id, student.Id, context);

            //reset
            dataSourceBackend.Reset();

            //assert
            Assert.IsFalse(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_BlockAccess_RequestId_Null_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            string expectReqId = null;
            string expectUserId = null;

            //act
            var result = backend.BlockAccess(expectUserId, expectReqId, null);

            //assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_BlockAccess_RequestId_Invalid_Should_Fail()
        {
            //arrange

            var backend = IdentityDataSourceMockV2.Instance;
            var expectReqId = "bogus";
            string expectUserId = null;

            //act
            var result = backend.BlockAccess(expectUserId, expectReqId, null);

            //assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_BlockAccess_UserId_Null_Should_Fail()
        {
            //arrange
            var dataBackend = DataSourceBackend.Instance;
            var studentBackend = dataBackend.StudentBackend;
            var backend = IdentityDataSourceMockV2.Instance;
            var expectReqId = backend.ListAllStudentUsers().FirstOrDefault().Id;
            string expectUserId = null;

            //act
            var result = backend.BlockAccess(expectUserId, expectReqId, null);

            //assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_BlockAccess_UserId_Invalid_Should_Fail()
        {
            //arrange
            var dataBackend = DataSourceBackend.Instance;
            var studentBackend = dataBackend.StudentBackend;
            var backend = IdentityDataSourceMockV2.Instance;
            var expectReqId = backend.ListAllStudentUsers().FirstOrDefault().Id;
            var expectUserId = "bogus";

            //act
            var result = backend.BlockAccess(expectUserId, expectReqId, null);

            //assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_BlockAccess_UserId_Not_RequestedId_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var studentList = backend.ListAllStudentUsers();
            var expectReqId = studentList.FirstOrDefault().Id;
            var expectUserId = studentList.Last().Id;

            //act
            var result = backend.BlockAccess(expectUserId, expectReqId, null);

            //assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_IdentityDataSourceMock_BlockAccess_No_User_Logged_In_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var studentList = backend.ListAllStudentUsers();
            var expectReqId = studentList.FirstOrDefault().Id;
            var expectUserId = expectReqId;

            var context = CreateMoqSetupForCookie();

            //act
            var result = backend.BlockAccess(expectUserId, expectReqId, context);

            //assert
            Assert.IsTrue(result, TestContext.TestName);
        }


        [TestMethod]
        public void Backend_IdentityDataSourceMock_BlockAccess_Invalid_User_Should_Fail()
        {
            //arrange
            var backend = IdentityDataSourceMockV2.Instance;
            var studentList = backend.ListAllStudentUsers();
            var expectReqId = studentList.FirstOrDefault().Id;
            var expectUserId = expectReqId;

            var testCookieName = "id";
            var testCookieValue = studentList.Last().Id;
            HttpCookie testCookie = new HttpCookie(testCookieName)
            {
                Value = testCookieValue,
                Expires = DateTime.Now.AddSeconds(30)
            };

            var context = CreateMoqSetupForCookie(testCookieValue);
            context.Request.Cookies.Add(testCookie);

            var createResult = backend.CreateCookie(testCookieName, testCookieValue, context);

            //act
            var result = backend.BlockAccess(expectUserId, expectReqId, context);

            //assert
            Assert.IsTrue(result, TestContext.TestName);
        }

        #endregion



        //move this to a helper folder in the main project and have the other test project and this test project tuse that
        //this is just for testing

            /// <summary>
            /// sets up a moq for http context so that a code dealing with cookeis can be tested
            /// returns a moqed context object
            /// </summary>
        public HttpContextBase CreateMoqSetupForCookie(string cookieValue = null)
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);

            var mockedRequest = Mock.Get(context.Object.Request);
            mockedRequest.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());

            var mockedResponse = Mock.Get(context.Object.Response);
            mockedResponse.Setup(r => r.Cookies).Returns(new HttpCookieCollection());

            if(!string.IsNullOrEmpty(cookieValue))
            {
                var mockedServer = Mock.Get(context.Object.Server);
                mockedServer.Setup(x => x.HtmlEncode(cookieValue)).Returns(cookieValue);
            }

            return context.Object;
        }
    }
}
