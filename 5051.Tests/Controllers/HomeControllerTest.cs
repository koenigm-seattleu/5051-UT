using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Controllers;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_Home_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new HomeController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region IndexRegion

        [TestMethod]
        public void Controller_Home_Index_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion IndexRegion

        #region ErrorRegion

        [TestMethod]
        public void Controller_Home_Error_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Error() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ErrorRegion
  
    }
}
