using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;
using _5051.Models;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class ManageControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_Manage_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new ManageController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new ManageController().GetType(), TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Manage_Instantiate_Default_Application_User_Manager_And_Signin_Manager_Should_Pass()
        {
            // Arrage
            var controller = new ManageController((ApplicationUserManager)null, (ApplicationSignInManager)null);

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new ManageController().GetType(), TestContext.TestName);
        }
        #endregion Instantiate

        #region AddPhoneNumber
        [TestMethod]
        public void Controller_ManageController_Get_AddPhoneNumber_Default_Should_Pass()
        {
            // Arrange
            var controller = new ManageController();

            // Act
            ViewResult result = controller.AddPhoneNumber() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ManageController_Post_AddPhoneNumber_Invalid_Null_Should_Send_Back_For_Edits()
        {
            // Arrange
            var controller = new ManageController();
            var data = new AddPhoneNumberViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = controller.AddPhoneNumber(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ManageController_Post_AddPhoneNumber_Valid_Data_Should_Pass()
        {
            // Arrange
            var controller = new ManageController();
            var data = new AddPhoneNumberViewModel
            {
                Number = "123"
            };

            // Act
            var result = controller.AddPhoneNumber(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion AddPhoneNumber

        #region ChangePassword
        [TestMethod]
        public void Controller_ManageController_Get_ChangePassword_Default_Should_Pass()
        {
            // Arrange
            var controller = new ManageController();

            // Act
            ViewResult result = controller.ChangePassword() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ManageController_Post_ChangePassword_Invalid_Null_Should_Send_Back_For_Edits()
        {
            // Arrange
            var controller = new ManageController();
            var data = new ChangePasswordViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = controller.ChangePassword(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ManageController_Post_ChangePassword_Valid_Data_Should_Pass()
        {
            // Arrange
            var controller = new ManageController();
            var data = new ChangePasswordViewModel
            {
                ConfirmPassword = "123",
                NewPassword = "123",
                OldPassword = "123"
            };

            // Act
            var result = controller.ChangePassword(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ChangePassword

        #region VerifyPhoneNumber
        
        [TestMethod]
        public void Controller_ManageController_Post_VerifyPhoneNumber_Invalid_Null_Should_Send_Back_For_Edits()
        {
            // Arrange
            var controller = new ManageController();
            var data = new VerifyPhoneNumberViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = controller.VerifyPhoneNumber(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ManageController_Post_VerifyPhoneNumber_Valid_Data_Should_Pass()
        {
            // Arrange
            var controller = new ManageController();
            var data = new VerifyPhoneNumberViewModel
            {
                PhoneNumber = "123"
            };

            // Act
            var result = controller.VerifyPhoneNumber(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion VerifyPhoneNumber

        #region SetPassword

        [TestMethod]
        public void Controller_ManageController_Post_SetPassword_Invalid_Null_Should_Send_Back_For_Edits()
        {
            // Arrange
            var controller = new ManageController();
            var data = new SetPasswordViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = controller.SetPassword(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ManageController_Post_SetPassword_Valid_Data_Should_Pass()
        {
            // Arrange
            var controller = new ManageController();
            var data = new SetPasswordViewModel
            {
                NewPassword = "123",
                ConfirmPassword = "123"
            };

            // Act
            var result = controller.SetPassword(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion SetPassword

        #region RemovePhoneNumber

        [TestMethod]
        public void Controller_ManageController_Post_RemovePhoneNumber_Invalid_Null_Should_Send_Back_For_Edits()
        {
            // Arrange
            var controller = new ManageController();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = controller.RemovePhoneNumber();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ManageController_Post_RemovePhoneNumber_Valid_Data_Should_Pass()
        {
            // Arrange
            var controller = new ManageController();

            // Act
            var result = controller.RemovePhoneNumber();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion RemovePhoneNumber

        #region RemoveLogin

        [TestMethod]
        public void Controller_ManageController_Post_RemoveLogin_Invalid_Null_Should_Send_Back_For_Edits()
        {
            // Arrange
            var controller = new ManageController();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");
            var LoginProvider = "LoginProvider";
            var LoginKey = "LoginKey";

            // Act
            var result = controller.RemoveLogin(LoginProvider, LoginKey);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_ManageController_Post_RemoveLogin_Valid_Data_Should_Pass()
        {
            // Arrange
            var controller = new ManageController();
            var LoginProvider = "LoginProvider";
            var LoginKey = "LoginKey";

            // Act
            var result = controller.RemoveLogin(LoginProvider, LoginKey);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion RemoveLogin
        #region LinkLogin

        // Need to add a Moq for User Identity
        //[TestMethod]
        //public void Controller_ManageController_Post_LinkLogin_Valid_Data_Should_Pass()
        //{
        //    // Arrange
        //    var controller = new ManageController();
        //    var data = "http://msn.com";

        //    // Act
        //    var result = controller.LinkLogin(data);

        //    // Assert
        //    Assert.IsNotNull(result, TestContext.TestName);
        //}

        #endregion LinkLogin
    }
}
