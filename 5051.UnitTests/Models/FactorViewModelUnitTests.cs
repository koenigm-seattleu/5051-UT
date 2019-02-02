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
    public class FactorViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_FactorViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new FactorViewModel();
            var expectPurpose = "purposeOne";

            //act
            test.Purpose = expectPurpose;

            //assert
            Assert.AreEqual(expectPurpose, test.Purpose, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
