using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using System.Linq;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class UserRoleEnumUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_UserRoleEnumUnitTests_Values_Should_Pass()
        {
            // Assert

            // Make sure there are no additional values
            var enumCount = UserRoleEnum.GetNames(typeof(UserRoleEnum)).Length;
            Assert.AreEqual(5, enumCount, TestContext.TestName);

            // Check each value against their expected value.
            Assert.AreEqual(0, (int)UserRoleEnum.Unknown, TestContext.TestName);
            Assert.AreEqual(10, (int)UserRoleEnum.StudentUser, TestContext.TestName);
            Assert.AreEqual(40, (int)UserRoleEnum.AdminUser, TestContext.TestName);
            Assert.AreEqual(50, (int)UserRoleEnum.SupportUser, TestContext.TestName);
            Assert.AreEqual(60, (int)UserRoleEnum.TeacherUser, TestContext.TestName);
        }
        #endregion Instantiate

        [TestMethod]
        public void Models_UserRoleEnumUnitTests_DisplayName_Values_Should_Pass()
        {
            // Arrange

            // Act
            // Make sure there are no additional values
            var enumCount = UserRoleEnum.GetNames(typeof(UserRoleEnum)).Length;

            // Reset

            // Assert

            // Check each value against their expected value.
            Assert.AreEqual("Unknown", UserRoleEnum.Unknown.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Teacher", UserRoleEnum.TeacherUser.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Student", UserRoleEnum.StudentUser.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Support", UserRoleEnum.SupportUser.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Admin", UserRoleEnum.AdminUser.GetDisplayName(), TestContext.TestName);
        }
    }
}
