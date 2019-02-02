using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class ApplicationUserInputUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Models_ApplicationUserInput_Instantiate_Should_Pass()
        {
            // Arange

            // Act
            var result = new ApplicationUserInputModel();

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_ApplicationUserInputModel_Instantiate_Should_Pass()
        {
            // Arange
            //DataSourceBackend.Instance.StudentBackend.Index();
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var userInfo = DataSourceBackend.Instance.IdentityBackend.FindUserByID(Student.Id);

            // Act
            var result = new ApplicationUserInputModel(userInfo);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(Student, TestContext.TestName);
            Assert.IsNotNull(userInfo, TestContext.TestName);
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_ApplicationUserInputModel_Get_Set_Should_Pass()
        {
            // Arange
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var userInfo = DataSourceBackend.Instance.IdentityBackend.FindUserByUserName(Student.Name);

            // Act
            var result = new ApplicationUserInputModel(userInfo)
            {
                Id = "123",
                Role = UserRoleEnum.StudentUser,
                Student = Student,
                State = true
            };

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
            Assert.AreEqual(result.Id, result.Id, TestContext.TestName);
            Assert.AreEqual(result.Role, result.Role, TestContext.TestName);
            Assert.AreEqual(result.Student, result.Student, TestContext.TestName);
            Assert.AreEqual(result.State, result.State, TestContext.TestName);
        }

        [TestMethod]
        public void Models_ApplicationUserInput_Invalid_User_Should_Fail()
        {

            // Arange
            var userInfo = DataSourceBackend.Instance.IdentityBackend.FindUserByUserName("bogus");

            // Act
            var result = new ApplicationUserInputModel(userInfo);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNull(result.Student, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
