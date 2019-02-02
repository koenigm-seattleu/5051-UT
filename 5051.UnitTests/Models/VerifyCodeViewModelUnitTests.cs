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
    public class VerifyCodeViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_VerifyCodeViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new VerifyCodeViewModel();
            var expectProvider = "Good Provider";
            var expectCode = "Good Code";
            var expectReturnUrl = "www.deliciousurl.gov";
            var expectRememberBrower = true;
            var expectRememberMe = true;

            //act
            test.Provider = expectProvider;
            test.Code = expectCode;
            test.ReturnUrl = expectReturnUrl;
            test.RememberBrowser = expectRememberBrower;
            test.RememberMe = expectRememberMe;

            //assert
            Assert.AreEqual(expectProvider, test.Provider, TestContext.TestName);
            Assert.AreEqual(expectCode, test.Code, TestContext.TestName);
            Assert.AreEqual(expectReturnUrl, test.ReturnUrl, TestContext.TestName);
            Assert.AreEqual(expectRememberBrower, test.RememberBrowser, TestContext.TestName);
            Assert.AreEqual(expectRememberMe, test.RememberMe, TestContext.TestName);
        }
        #endregion Instantiate

    }
}
