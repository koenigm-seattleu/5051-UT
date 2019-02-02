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
    public class VerifyPhoneNumberViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region code
        [TestMethod]
        public void Models_VerifyPhoneNumberViewModel_Code_Get_Set_Should_Pass()
        {
            //arrange
            var test = new VerifyPhoneNumberViewModel();
            var expect = "1 916";

            //act
            test.Code = expect;

            //assert
            Assert.AreEqual(expect, test.Code, TestContext.TestName);
        }
        #endregion code

        #region PhoneNumber
        [TestMethod]
        public void Models_VerifyPhoneNumberViewModel_PhoneNumber_Get_Set_Should_Pass()
        {
            //arrange
            var test = new VerifyPhoneNumberViewModel();
            var expect = "742 8100";

            //act
            test.PhoneNumber = expect;

            //assert
            Assert.AreEqual(expect, test.PhoneNumber, TestContext.TestName);
        }
        #endregion PhoneNumber

    }
}
