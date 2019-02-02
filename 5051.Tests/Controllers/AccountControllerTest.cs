using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;
using _5051.Backend;
using _5051.Models;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Controller_Account_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new AccountController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region AccountUserSignInManagersRegion

        [TestMethod]
        public void Controller_Account_User_SignIn_Managers_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController((ApplicationUserManager)null, (ApplicationSignInManager)null);

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new AccountController().GetType(), TestContext.TestName);
        }

        #endregion AccountUserSignInManagersRegion

        #region RegisterRegion

        [TestMethod]
        public void Controller_Account_Register_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.Register() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion RegisterRegion

        #region ForgotPasswordRegion

        [TestMethod]
        public void Controller_Account_ForgotPassword_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ForgotPassword() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ForgotPasswordRegion

        #region ForgotPasswordConfirmationRegion

        [TestMethod]
        public void Controller_Account_ForgotPasswordConfirmation_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ForgotPasswordConfirmation() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ForgotPasswordConfirmationRegion

        #region ResetPasswordConfirmationRegion

        [TestMethod]
        public void Controller_Account_ResetPasswordConfirmation_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ResetPasswordConfirmation() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ResetPasswordConfirmationRegion

        #region ExternalLoginFailureRegion

        [TestMethod]
        public void Controller_Account_ExternalLoginFailure_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ExternalLoginFailure() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ExternalLoginFailureRegion

        #region LoginRegion

        [TestMethod]
        public void Controller_Account_Login_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            string url = "abc";
            string expect = url;

            // Act
            var result = controller.Login(url) as ViewResult;

            var resultBag = controller.ViewBag;

            string resultURL = "";
            resultURL = controller.ViewData["ReturnUrl"].ToString();

            // Assert
            Assert.AreEqual(expect, resultURL, "URL " + TestContext.TestName);
            Assert.IsNotNull(result, "Null " + TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Account_Login_Post_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            LoginViewModel loginViewModel = new LoginViewModel();
            var viewModelResult = loginViewModel.GetType();

            string url = "abc";

            // Act
            var result = controller.Login(url) as ViewResult;
            var resultViewModel = controller.Login(loginViewModel, url).GetType();

            // Assert
            Assert.AreEqual(viewModelResult, new LoginViewModel().GetType(), TestContext.TestName);

        }

        [TestMethod]
        public void Controller_Account_Login_Invalid_Model_Should_Fail()
        {
            // Arrange
            var controller = new AccountController();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var model = new LoginViewModel();
            var returnUrl = "abc";

            // Act
            var result = controller.Login(model, returnUrl);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion LoginRegion

        #region ResetPasswordRegion
        [TestMethod]
        public void Controller_Account_ResetPassword_Default_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            string code = "abc";

            // Act
            var result = controller.ResetPassword(code) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Account_ResetPassword_Null_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            string code = null;

            // Act
            var result = controller.ResetPassword(code) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ResetPasswordRegion

        #region VerifyCode
        [TestMethod]
        public void Controller_Account_VerifyCode_Invalid_Model_Should_Fail()
        {
            // Arrange
            var controller = new AccountController();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var model = new VerifyCodeViewModel();

            // Act
            var result = controller.VerifyCode(model);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion VerifyCode

        #region Register
        [TestMethod]
        public void Controller_Account_Register_Get_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            var model = new RegisterViewModel();

            // Act
            var result = controller.Register() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Account_Register_Invalid_Model_Should_Fail()
        {
            // Arrange
            var controller = new AccountController();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var model = new RegisterViewModel();

            // Act
            var result = controller.Register(model);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Account_Register_Valid_Model_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            var model = new RegisterViewModel
            {
                ConfirmPassword = "password",
                Email = "test@abc.com",
                Password = "password"
            };

            // Act
            var result = controller.Register(model);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Register

        #region ConfirmEmail
        [TestMethod]
        public void Controller_Account_ConfirmEmail_Invalid_Null_UserId_Should_Fail()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var resultTask = controller.ConfirmEmail(null, "Bogus");
            var result = resultTask.Result as ViewResult;

            // Assert
            Assert.AreEqual("Error",result.ViewName, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Account_ConfirmEmail_Invalid_Null_Code_Should_Fail()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var resultTask = controller.ConfirmEmail("Bogus", null);
            var result = resultTask.Result as ViewResult;

            // Assert
            Assert.AreEqual("Error", result.ViewName, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Account_ConfirmEmail_Invalid_Both_Bogus_Should_Fail()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ConfirmEmail("Bogus", "bogus");

            // Assert
            Assert.AreEqual(System.Threading.Tasks.TaskStatus.Faulted, result.Status, TestContext.TestName);
        }
        #endregion ConfirmEmail

        #region ForgetPassword
        [TestMethod]
        public void Controller_Account_ForgotPassword_Valid_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ForgotPassword() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Account_ForgotPassword_Invalid_Model_Should_Fail()
        {
            // Arrange
            var controller = new AccountController();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var model = new ForgotPasswordViewModel();

            // Act
            var result = controller.ForgotPassword(model);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Account_ForgotPassword_Invalid_NotUser_Should_Fail()
        {
            // Arrange
            var controller = new AccountController();

            var model = new ForgotPasswordViewModel
            {
                Email = "abc@abc.com"
            };

            // Act
            var result = controller.ForgotPassword(model);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Account_ForgotPasswordConfirmation_Valid_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ForgotPasswordConfirmation() as ActionResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion  ForgetPassword

        #region RestPassword
        [TestMethod]
        public void Controller_Account_ResetPassword_Valid_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ResetPassword("code") as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Account_ResetPassword_InValid_Code_NullShould_Fail()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ResetPassword((string) null) as ViewResult;

            // Assert
            Assert.AreEqual("Error", result.ViewName, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Account_ResetPassword_Invalid_Model_Should_Fail()
        {
            // Arrange
            var controller = new AccountController();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var model = new ResetPasswordViewModel();

            // Act
            var result = controller.ResetPassword(model);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Account_ResetPassword_Invalid_NotUser_Should_Fail()
        {
            // Arrange
            var controller = new AccountController();

            var model = new ResetPasswordViewModel
            {
                Email = "abc@abc.com"
            };

            // Act
            var result = controller.ResetPassword(model);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ResetPassword

        #region ExternalLogin

        //[TestMethod]
        //public void Controller_Account_ExternalLogin_InValid_Code_NullShould_Fail()
        //{
        //    // Arrange
        //    var controller = new AccountController();

        //    // Act
        //    var result = controller.ExternalLogin("bogus","bogus") as ViewResult;

        //    // Assert
        //    Assert.AreEqual("Error", result.ViewName, TestContext.TestName);
        //}
        #endregion  ExternalLogin

        #region SendCode
        [TestMethod]
        public void Controller_Account_SendCode_Invalid_Model_Should_Fail()
        {
            // Arrange
            var controller = new AccountController();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            var model = new SendCodeViewModel();

            // Act
            var result = controller.SendCode(model);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion SendCode

        #region ExternalLoginFailure
        [TestMethod]
        public void Controller_Account_ExternalLoginFailure_Valid_Should_Pass()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ExternalLoginFailure() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion ExternalLoginFailure
    }
}
