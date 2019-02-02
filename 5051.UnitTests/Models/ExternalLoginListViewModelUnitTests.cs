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
    public class ExternalLoginListViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_ExternalLoginListViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new ExternalLoginListViewModel();
            var expectReturnUrl = "TheUrlOfALifetime";

            //act
            test.ReturnUrl = expectReturnUrl;

            //assert
            Assert.AreEqual(expectReturnUrl, test.ReturnUrl, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
