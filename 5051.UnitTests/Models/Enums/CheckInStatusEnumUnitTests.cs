using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models.Enums;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class CheckInStatusEnumUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_CheckInStatusEnum_Values_Should_Pass()
        {
            // Assert

            // Make sure there are no additional values
            var enumCount = CheckInStatusEnum.GetNames(typeof(CheckInStatusEnum)).Length;
            Assert.AreEqual(3, enumCount, "Count "+TestContext.TestName);

            // Check each value against their expected value.
            Assert.AreEqual(0, (int)CheckInStatusEnum.Unknown, TestContext.TestName);
            Assert.AreEqual(1, (int)CheckInStatusEnum.ArriveOnTime, TestContext.TestName);
            Assert.AreEqual(2, (int)CheckInStatusEnum.ArriveLate, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
