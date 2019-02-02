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
    public class SetPasswordViewModelUnitTests
    {
		public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_SetPasswordViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new SetPasswordViewModel();
            var expectNew = "passWORD23";
            var expectConfirm = expectNew;

            //act
            test.NewPassword = expectNew;
            test.ConfirmPassword = expectConfirm;

            //assert
            Assert.AreEqual(expectNew, test.NewPassword, TestContext.TestName);
            Assert.AreEqual(expectConfirm, test.ConfirmPassword, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
