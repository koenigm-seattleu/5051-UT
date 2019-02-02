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
    public class ApplicationUserViewUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Models_ApplicationUserView_Instantiate_Should_Pass()
        {
            // Arange
            DataSourceBackend.Instance.StudentBackend.Index();
            var field = DataSourceBackend.Instance.StudentBackend.GetDefault().Name;
            var userInfo = DataSourceBackend.Instance.IdentityBackend.FindUserByUserName(field);

            // Act
            var result = new ApplicationUserViewModel(userInfo);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(field, TestContext.TestName);
            Assert.IsNotNull(userInfo, TestContext.TestName);
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_ApplicationUserView_Invalid_User_Should_Fail()
        {
            // Arange
            var userInfo = DataSourceBackend.Instance.IdentityBackend.FindUserByUserName("bogus");

            // Act
            var result = new ApplicationUserViewModel(userInfo);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNull(result.Student, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
