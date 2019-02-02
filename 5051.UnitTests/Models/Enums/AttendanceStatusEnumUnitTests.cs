using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models.Enums;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class AttendanceStatusEnumUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_AttendanceStatusEnumUnitTests_Values_Should_Pass()
        {
            // Assert

            // Make sure there are no additional values
            var enumCount = AttendanceStatusEnum.GetNames(typeof(AttendanceStatusEnum)).Length;
            Assert.AreEqual(4, enumCount, TestContext.TestName);

            // Check each value against their expected value.
            Assert.AreEqual(0, (int)AttendanceStatusEnum.Unknown, TestContext.TestName);
            Assert.AreEqual(1, (int)AttendanceStatusEnum.Present, TestContext.TestName);
            Assert.AreEqual(2, (int)AttendanceStatusEnum.AbsentExcused, TestContext.TestName);
            Assert.AreEqual(3, (int)AttendanceStatusEnum.AbsentUnexcused, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
