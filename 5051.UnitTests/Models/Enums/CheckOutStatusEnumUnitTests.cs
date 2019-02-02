using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models.Enums;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class CheckOutStatusEnumUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_CheckOutStatusEnum_Values_Should_Pass()
        {
            // Assert

            // Make sure there are no additional values
            var enumCount = CheckOutStatusEnum.GetNames(typeof(CheckOutStatusEnum)).Length;
            Assert.AreEqual(3, enumCount, "Count "+TestContext.TestName);

            // Check each value against their expected value.
            Assert.AreEqual(0, (int)CheckOutStatusEnum.Unknown, TestContext.TestName);
            Assert.AreEqual(1, (int)CheckOutStatusEnum.DoneAuto, TestContext.TestName);
            Assert.AreEqual(2, (int)CheckOutStatusEnum.DoneEarly, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
