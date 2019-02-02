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
    public class KioskControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Controller_Kiosk_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var controller = new KioskController();

            // Act
            var result = controller.GetType();

            // Assert
            Assert.AreEqual(result, new KioskController().GetType(), TestContext.TestName);
        }

        #endregion Instantiate

        #region IndexRegion
        [TestMethod]
        public void Controller_Kiosk_Index_Default_Should_Pass()
        {
            // Arrange
            KioskController controller = new KioskController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        //[TestMethod]
        //public void Controller_Kiosk_Index_With_Empty_List_Should_Return_Error_Page()
        //{
        //    // Arrange
        //    KioskController controller = new KioskController();
            
        //    // Set unitesting backend data
        //    DataSourceBackend.Instance.SetDataSourceDataSet(DataSourceDataSetEnum.UnitTest);

        //    // Make empty StudentList
        //    while (DataSourceBackend.Instance.StudentBackend.Index().Count != 0)
        //    {
        //        var first = DataSourceBackend.Instance.StudentBackend.GetDefault();
        //        DataSourceBackend.Instance.StudentBackend.Delete(first.Id);
        //    }

        //    // Act
        //    var result = (RedirectToRouteResult)controller.Index();

        //    // Reset DataSourceBackend
        //    DataSourceBackend.Instance.Reset();

        //    // Assert
        //    Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        //}
        #endregion IndexRegion

        #region KioskLoginRegion
        [TestMethod]
        public void Controller_Kiosk_KioskLogin_Valid_Data_Should_Pass()
        {
            // Arrange
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            var controller = new KioskController();

            // Get the first Studnet from the DataSource
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;            
            
            // Act
            var result = (RedirectToRouteResult)controller.KioskLogin(data);            

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert            
            Assert.AreEqual("ConfirmLogin", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_KioskLogin_Valid_Data_Should_Update_Status()
        {
            // Arrange
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            var controller = new KioskController();

            // Get the first Studnet from the DataSource
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            var result = (RedirectToRouteResult)controller.KioskLogin(data);
            
            // check status change after KioskLogin
            var resultStatus = DataSourceBackend.Instance.StudentBackend.Read(id).Status;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(StudentStatusEnum.In, resultStatus, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_KioskLogin_Valid_Data_Should_Update_Emotion()
        {
            // Arrange
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            var controller = new KioskController();

            // Get the first Studnet from the DataSource
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            var result = (RedirectToRouteResult)controller.KioskLogin(data);
            
            // check status change after SetLogin
            var resultStatus = DataSourceBackend.Instance.StudentBackend.Read(id).EmotionCurrent;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(EmotionStatusEnum.Neutral, resultStatus, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_KioskLogin_Null_Or_Empty_Data_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new KioskController();
            StudentModel data = null;

            // Act
            var result = (RedirectToRouteResult)controller.KioskLogin(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_KioskLogin_Null_Or_Empty_Id_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new KioskController();

            // Create StudentModel instance
            StudentModel data = new StudentModel
            {

                // Set student Id to null
                Id = null
            };

            // Act
            var result = (RedirectToRouteResult)controller.KioskLogin(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }
        #endregion SetLoginRegion

        #region SetLogoutRegion
        [TestMethod]
        public void Controller_Kiosk_KioskLogout_Valid_Data_Should_Pass()
        {
            // Arrange
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            var controller = new KioskController();

            // Get the first Studnet from the DataSource
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Login, so logout can happen
            controller.KioskLogin(data);

            // Act
            var result = (RedirectToRouteResult)controller.KioskLogout(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert            
            Assert.AreEqual("ConfirmLogout", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_KioskLogout_Valid_Data_Should_Update_Status()
        {
            // Arrange
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            var controller = new KioskController();

            // Get the first Studnet from the DataSource
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Login, so logout can happen
            controller.KioskLogin(data);

            // Act
            var result = (RedirectToRouteResult)controller.KioskLogout(data);

            // check status change after KioskLogout
            var resultStatus = DataSourceBackend.Instance.StudentBackend.Read(id).Status;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(StudentStatusEnum.Out, resultStatus, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_KioskLogout_Null_Or_Empty_Data_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new KioskController();
            StudentModel data = null;

            // Act
            var result = (RedirectToRouteResult)controller.KioskLogout(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_KioskLogout_Null_Or_Empty_Id_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new KioskController();

            // Create StudentModel instance
            StudentModel data = new StudentModel
            {

                // Set student Id to null
                Id = null
            };

            // Act
            var result = (RedirectToRouteResult)controller.KioskLogout(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }
        #endregion SetLogoutRegion

        #region ConfirmLogin
        [TestMethod]
        public void Controller_Kiosk_ConfirmLogin_Valid_Id_Should_Pass()
        {
            // Arrange
            var controller = new KioskController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.ConfirmLogin(id) as ViewResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_ConfirmLogin_Null_Or_Empty_Id_Should_Return_Error_Page()
        {
            // Arrage
            var controller = new KioskController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.ConfirmLogin(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        //[TestMethod]
        //public void Controller_Kiosk_ConfirmLogin_Invalid_Id_Should_Return_Error_Page()
        //{
        //    // Arrange
        //    var controller = new KioskController();
        //    // Create new Id that does not existing in Student list
        //    string id = Guid.NewGuid().ToString();

        //    // Act
        //    var result = (RedirectToRouteResult)controller.ConfirmLogin(id);

        //    // Assert
        //    Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        //}
        #endregion ConfirmLogin

        //public ActionResult ConfirmLogin(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return RedirectToAction("Error", "Home");
        //    }

        //    var myDataList = DataSourceBackend.Instance.StudentBackend.Read(id);
        //    if (myDataList == null)
        //    {
        //        return RedirectToAction("Error", "Home");
        //    }
        //    var StudentViewModel = new StudentDisplayViewModel(myDataList);

        //    //Todo, replace with actual transition time
        //    StudentViewModel.LastDateTime = DateTime.Now;

        //    return View(StudentViewModel);
        //}

        #region ConfirmLogout
        [TestMethod]
        public void Controller_Kiosk_ConfirmLogout_Valid_Id_Should_Pass()
        {
            // Arrange
            var controller = new KioskController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.ConfirmLogout(id) as ViewResult;

            // Reset
            DataSourceBackend.Instance.Reset();
            
            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_ConfirmLogout_Null_Or_Empty_Id_Should_Return_Error_Page()
        {
            // Arrage
            var controller = new KioskController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.ConfirmLogout(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        //[TestMethod]
        //public void Controller_Kiosk_ConfirmLogout_Invalid_Id_Should_Return_Error_Page()
        //{
        //    // Arrange
        //    var controller = new KioskController();
        //    // Create new Id that does not existing in Student list
        //    string id = Guid.NewGuid().ToString();

        //    // Act
        //    var result = (RedirectToRouteResult)controller.ConfirmLogout(id);

        //    // Assert
        //    Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        //}
        #endregion ConfirmLogout

        //public ActionResult ConfirmLogout(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return RedirectToAction("Error", "Home");
        //    }

        //    var myDataList = DataSourceBackend.Instance.StudentBackend.Read(id);
        //    if (myDataList == null)
        //    {
        //        return RedirectToAction("Error", "Home");
        //    }
        //    var StudentViewModel = new StudentDisplayViewModel(myDataList);

        //    //Todo, replace with actual transition time
        //    StudentViewModel.LastDateTime = DateTime.Now;

        //    return View(StudentViewModel);
        //}

        #region Login

        [TestMethod]
        public void Controller_Kiosk_Login_Get_Should_Pass()
        {
            // Arrage
            var controller = new KioskController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_Login_Valid_Password_Should_Pass()
        {
            // Arrange
            var controller = new KioskController();
            string Password = KioskSettingsBackend.Instance.GetDefault().Password;

            var data = new KioskSettingsModel
            {
                Password = Password
            };

            // Act
            var result = (RedirectToRouteResult)controller.Login(data);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_Login_InValid_Password_Null_Should_Fail()
        {
            // Arrange
            var controller = new KioskController();
            string Password = KioskSettingsBackend.Instance.GetDefault().Password;

            var data = new KioskSettingsModel
            {
                Password = null
            };

            ViewResult result = controller.Login(data) as ViewResult;

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_Login_InValid_Password_Empty_Should_Fail()
        {
            // Arrange
            var controller = new KioskController();
            string Password = KioskSettingsBackend.Instance.GetDefault().Password;

            var data = new KioskSettingsModel
            {
                Password = ""
            };

            // Act
            ViewResult result = controller.Login(data) as ViewResult;

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_Login_InValid_Password_Bogus_Should_Fail()
        {
            // Arrange
            var controller = new KioskController();
            string Password = KioskSettingsBackend.Instance.GetDefault().Password;

            var data = new KioskSettingsModel
            {
                Password = "bogus"
            };

            // Act
            ViewResult result = controller.Login(data) as ViewResult;

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_Login_InValid_Model_Null_Should_Fail()
        {
            // Arrange
            var controller = new KioskController();
            string Password = KioskSettingsBackend.Instance.GetDefault().Password;

            var data = new KioskSettingsModel
            {
                Password = Password
            };

            // Make a model error then try to send it as a KioskSettings
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Login(data) as ViewResult;

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }
        #endregion Login

        #region CreateRegion

        [TestMethod]
        public void Controller_Kiosk_Create_Default_Should_Pass()
        {
            // Arrange
            KioskController controller = new KioskController();

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion CreateRegion

        #region CreatePostRegion

        [TestMethod]
        public void Controller_Kiosk_Create_Post_Model_Is_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            KioskController controller = new KioskController();

            StudentModel data = new StudentModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Create(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_Create_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            KioskController controller = new KioskController();

            // Act
            var result = (RedirectToRouteResult)controller.Create((StudentModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_Create_Post_Id_Is_Null_Or_Empty_Should_Return_Back_For_Edit()
        {
            // Arrange
            KioskController controller = new KioskController();

            StudentModel dataNull = new StudentModel();
            StudentModel dataEmpty = new StudentModel();

            // Make data.Id = null
            dataNull.Id = null;

            // Make data.Id empty
            dataEmpty.Id = "";

            // Act
            var resultNull = (ViewResult)controller.Create(dataNull);
            var resultEmpty = (ViewResult)controller.Create(dataEmpty);

            // Assert
            Assert.IsNotNull(resultNull, TestContext.TestName);
            Assert.IsNotNull(resultEmpty, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Kiosk_Create_Post_Default_Should_Return_Index_Page()
        {
            // Arrange
            KioskController controller = new KioskController();

            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();

            // Act
            var result = (RedirectToRouteResult)controller.Create(data);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion CreatePostRegion
    }
}