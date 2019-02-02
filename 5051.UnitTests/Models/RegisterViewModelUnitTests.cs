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
    public class RegisterViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_RegisterViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new RegisterViewModel();
            var expectEmail = "test@gmail.com";
            var expectPassword = "passWord23!";
            var expectConfirmPassword = expectPassword;

            //act
            test.Email = expectEmail;
            test.Password = expectPassword;
            test.ConfirmPassword = expectConfirmPassword;

            //assert
            Assert.AreEqual(expectEmail, test.Email, TestContext.TestName);
            Assert.AreEqual(expectPassword, test.Password, TestContext.TestName);
            Assert.AreEqual(expectConfirmPassword, test.ConfirmPassword, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
