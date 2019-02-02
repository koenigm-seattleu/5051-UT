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
    public class ResetPasswordViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Models_ResetPasswordViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new ResetPasswordViewModel();
            var expectEmail = "test@gmail.com";
            var expectPassword = "passWord23!";
            var expectConfirmPassword = expectPassword;
            var expectCode = "Good Code";

            //act
            test.Email = expectEmail;
            test.Password = expectPassword;
            test.ConfirmPassword = expectConfirmPassword;
            test.Code = expectCode;

            //assert
            Assert.AreEqual(expectEmail, test.Email, TestContext.TestName);
            Assert.AreEqual(expectPassword, test.Password, TestContext.TestName);
            Assert.AreEqual(expectConfirmPassword, test.ConfirmPassword, TestContext.TestName);
            Assert.AreEqual(expectCode, test.Code, TestContext.TestName);
        }
    }
}
