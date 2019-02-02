using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class UserListViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_UserListViewModel_Default_Instantiate_Should_Pass()
        {
            // Arrange

            // Act
            var result = new UserListViewModel();

            // Reset

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate
    }
}