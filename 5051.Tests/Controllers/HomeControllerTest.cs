using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051;
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

        #region AboutRegion

        [TestMethod]
        public void Controller_Home_About_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion AboutRegion

        #region ContactRegion

        [TestMethod]
        public void Controller_Home_Contact_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ContactRegion

        #region PrivacyRegion

        [TestMethod]
        public void Controller_Home_Privacy_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Privacy() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion PrivacyRegion

        #region ShopRegion

        [TestMethod]
        public void Controller_Home_ShopExample_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.ShopExample() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ShopRegion

        #region StudentRegion

        [TestMethod]
        public void Controller_Home_StudentExample_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.StudentExample() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion StudentRegion

        #region HouseRegion

        [TestMethod]
        public void Controller_Home_HouseExample_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.HouseExample() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion HouseRegion

        #region AvatarRegion

        [TestMethod]
        public void Controller_Home_AvatarExample_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.AvatarExample() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion AvatarRegion

        #region FAQRegion

        [TestMethod]
        public void Controller_Home_FAQ_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.FAQ() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion FAQRegion

        #region GuideRegion

        [TestMethod]
        public void Controller_Home_Guide_Default_Should_Pass()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Guide() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion GuideRegion

    }
}
