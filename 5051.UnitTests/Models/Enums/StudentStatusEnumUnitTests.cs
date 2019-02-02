using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class StudentStatusEnumUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_StudentStatusEnumUnitTests_Values_Should_Pass()
        {
            // Assert

            // Make sure there are no additional values
            var enumCount = StudentStatusEnum.GetNames(typeof(StudentStatusEnum)).Length;
            Assert.AreEqual(3, enumCount, TestContext.TestName);

            // Check each value against their expected value.
            Assert.AreEqual(1, (int)StudentStatusEnum.Out, TestContext.TestName);
            Assert.AreEqual(2, (int)StudentStatusEnum.In, TestContext.TestName);
            Assert.AreEqual(3, (int)StudentStatusEnum.Hold, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
