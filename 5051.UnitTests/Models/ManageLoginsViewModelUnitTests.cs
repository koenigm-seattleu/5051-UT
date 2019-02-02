using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class ManageLoginsViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_ManageLoginsViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new ManageLoginsViewModel();
            var expectCurrentLogins = new List<UserLoginInfo>();
            var expectOtherLogins = new List<AuthenticationDescription>();

            //act
            test.CurrentLogins = expectCurrentLogins;
            test.OtherLogins = expectOtherLogins;

            //assert
            Assert.AreEqual(expectCurrentLogins, test.CurrentLogins, TestContext.TestName);
            Assert.AreEqual(expectOtherLogins, test.OtherLogins, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
