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
    public class AddPhoneNumberViewModelTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_AddPhoneNumberViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new AddPhoneNumberViewModel();
            var expect = "911";

            //act
            test.Number = expect;

            //assert
            Assert.AreEqual(expect, test.Number, TestContext.TestName);
        }

        #endregion Instantiate
    }
}
