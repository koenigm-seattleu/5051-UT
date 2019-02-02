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
    public class ForgotPasswordViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_ForgotPasswordViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new ForgotPasswordViewModel();
            var expectEmail = "test@gmail.com";

            //act
            test.Email = expectEmail;

            //assert
            Assert.AreEqual(expectEmail, test.Email, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
