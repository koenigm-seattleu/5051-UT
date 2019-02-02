using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class LoginViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_LoginViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new LoginViewModel();
            var expectEmail = "test@gmail.com";
            var expectPassword = "passWORD23!";
            var expectRememberMe = true;

            //act
            test.Email = expectEmail;
            test.Password = expectPassword;
            test.RememberMe = expectRememberMe;

            //assert
            Assert.AreEqual(expectEmail, test.Email, TestContext.TestName);
            Assert.AreEqual(expectPassword, test.Password, TestContext.TestName);
            Assert.AreEqual(expectRememberMe, test.RememberMe, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
