using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using Microsoft.AspNet.Identity;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class IndexViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Models_IndexViewModels_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new IndexViewModel();

            var expectHasPassword = true;
            var expectLogins = new List<UserLoginInfo>();
            var expectPhoneNumber = "749 8100";
            var expectTwoFactor = true;
            var expectBrowserRemembered = true;

            //act
            test.HasPassword = expectHasPassword;
            test.Logins = expectLogins;
            test.PhoneNumber = expectPhoneNumber;
            test.TwoFactor = expectTwoFactor;
            test.BrowserRemembered = expectBrowserRemembered;

            //assert
            Assert.AreEqual(expectHasPassword, test.HasPassword, TestContext.TestName);
            Assert.AreEqual(expectLogins, test.Logins, TestContext.TestName);
            Assert.AreEqual(expectPhoneNumber, test.PhoneNumber, TestContext.TestName);
            Assert.AreEqual(expectTwoFactor, test.TwoFactor, TestContext.TestName);
            Assert.AreEqual(expectBrowserRemembered, test.BrowserRemembered, TestContext.TestName);
        }
    }
}
