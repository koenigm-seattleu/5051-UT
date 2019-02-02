using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models.Enums;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class SchoolCalendarDismissalEnumUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_SchoolCalendarDismissalEnumUnitTests_Values_Should_Pass()
        {
            // Assert

            // Make sure there are no additional values
            var enumCount = SchoolCalendarDismissalEnum.GetNames(typeof(SchoolCalendarDismissalEnum)).Length;
            Assert.AreEqual(4, enumCount, TestContext.TestName);

            // Check each value against their expected value.
            Assert.AreEqual(0, (int)SchoolCalendarDismissalEnum.Unknown, TestContext.TestName);
            Assert.AreEqual(1, (int)SchoolCalendarDismissalEnum.Normal, TestContext.TestName);
            Assert.AreEqual(2, (int)SchoolCalendarDismissalEnum.Early, TestContext.TestName);
            Assert.AreEqual(3, (int)SchoolCalendarDismissalEnum.Late, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
