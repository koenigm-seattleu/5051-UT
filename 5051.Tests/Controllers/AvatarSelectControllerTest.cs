using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;
using _5051.Models;
using _5051.Backend;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class AvatarSelectControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }
        
        #region IndexRegion
        [TestMethod]
        public void Controller_AvatarSelectShop_Index_Default_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.Index(id) as ViewResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Index_With_Empty_List_Should_Return_Error_Page()
        {
            // Arrange
            var controller = new AvatarSelectController();

            // Set unitesting backend data
            DataSourceBackend.Instance.SetDataSourceDataSet(DataSourceDataSetEnum.UnitTest);

            // Make empty StudentList
            while (DataSourceBackend.Instance.StudentBackend.Index().Count != 0)
            {
                var first = DataSourceBackend.Instance.StudentBackend.GetDefault();
                DataSourceBackend.Instance.StudentBackend.Delete(first.Id);
            }

            // Act
            var result = (RedirectToRouteResult)controller.Index();

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion IndexRegion

        #region SelectRegion
        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Default_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.Select(id) as ViewResult;
            
            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = new ShopBuyViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Select(data) as ViewResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Get_myDataIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            // Act
            var result = (RedirectToRouteResult)controller.Select(id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Data_Invalid_Null_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();
            ShopBuyViewModel data;
            data = null;

            // Act
            var result = (RedirectToRouteResult)controller.Select(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Data_Invalid_StudentID_Null_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new ShopBuyViewModel
            {
                StudentId = null
            };

            // Act
            var result = (RedirectToRouteResult)controller.Select(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Select", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Data_Invalid_ItemId_Null_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = new ShopBuyViewModel
            {
                StudentId = "studentID",
                ItemId = null
            };

            // Act
            var result = (RedirectToRouteResult)controller.Select(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Select", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Data_Invalid_StudentId_Bogus_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new ShopBuyViewModel
            {
                StudentId = "bogus",
                ItemId = "itemID"
            };

            // Act
            var result = (RedirectToRouteResult)controller.Select(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Select", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Data_Invalid_ItemId_Bogus_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = "bogus"
            };

            // Act
            var result = (RedirectToRouteResult)controller.Select(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Select", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Data_Invalid_AvatarComposite_Null_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory).Id
            };

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.AvatarComposite = null;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Act
            var result = (RedirectToRouteResult)controller.Select(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Select", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Data_Valid_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetFirstAvatarItemId()
            };

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 1000;

            // Clear the Student AvatarInventory, so one can be purchanged.
            myStudent.AvatarInventory.Clear();
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.AvatarItemBackend.Read(data.ItemId);

            myInventory.Tokens = 10;
            DataSourceBackend.Instance.AvatarItemBackend.Update(myInventory);

            var expect = myStudent.Tokens - myInventory.Tokens;

            // Act
            ViewResult result = controller.Select(data) as ViewResult;

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Data_Valid_Limited_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetFirstAvatarItemId()
            };

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 1000;

            // Clear the Student AvatarInventory, so one can be purchanged.
            myStudent.AvatarInventory.Clear();
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.AvatarItemBackend.Read(data.ItemId);

            myInventory.Tokens = 10;
            myInventory.IsLimitSupply = true;

            DataSourceBackend.Instance.AvatarItemBackend.Update(myInventory);

            var expect = myStudent.Tokens - myInventory.Tokens;

            // Act
            ViewResult result = controller.Select(data) as ViewResult;

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Data_InValid_Quantities_Not_Enough_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetFirstAvatarItemId()
            };

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 1000;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.AvatarItemBackend.Read(data.ItemId);
            myInventory.Quantities = 0;
            DataSourceBackend.Instance.AvatarItemBackend.Update(myInventory);

            // No Purchase, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.AvatarInventory.Count();

            // Act
            var result = (RedirectToRouteResult)controller.Select(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Select", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.AvatarInventory.Count(), TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Data_InValid_Tokens_Not_Enough_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetFirstAvatarItemId()
            };

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 10;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.AvatarItemBackend.Read(data.ItemId);

            myInventory.Tokens = 100;
            DataSourceBackend.Instance.AvatarItemBackend.Update(myInventory);

            // No Purchase, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.AvatarInventory.Count();

            // Act
            var result = (RedirectToRouteResult)controller.Select(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Select", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.AvatarInventory.Count(), TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Data_InValid_Tokens_Less_Than_One_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetFirstAvatarItemId()
            };

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 0;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.AvatarItemBackend.Read(data.ItemId);

            myInventory.Tokens = 0;
            DataSourceBackend.Instance.AvatarItemBackend.Update(myInventory);

            // No Purchase, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.AvatarInventory.Count();

            // Act
            var result = (RedirectToRouteResult)controller.Select(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Select", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.AvatarInventory.Count(), TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Select_Data_InValid_Item_Already_Exists_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetFirstAvatarItemId()
            };

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 1000;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.AvatarItemBackend.Read(data.ItemId);

            myInventory.Tokens = 10;
            DataSourceBackend.Instance.AvatarItemBackend.Update(myInventory);

            // Buy it one time, this puts the item in the student inventory
            var myPurchase1 = (RedirectToRouteResult)controller.Select(data);

            // No Purchase, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.AvatarInventory.Count();

            // Act

            // Trying to buy the second time will fail
            var result = (RedirectToRouteResult)controller.Select(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Select", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.AvatarInventory.Count(), TestContext.TestName);
        }
        #endregion SelectRegion

        #region Edit
        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Invalid_No_StudentID_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            // Act
            var result = controller.Edit() as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Invalid_Null_StudentID_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            // Act
            var result = controller.Edit((string)null) as RedirectToRouteResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Invalid_StudentID_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            // Act
            var result = controller.Edit("bogus") as RedirectToRouteResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Valid_StudentID_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var item = DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);

            // Act
            var result = controller.Edit(data.Id, item.Id) as ViewResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_InValid_StudentID_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var item = DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);

            // Act
            var result = controller.Edit((string)null, item.Id) as RedirectToRouteResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_InValid_StudentID_Bogus_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var item = DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);

            // Act
            var result = controller.Edit("Bogus", item.Id) as RedirectToRouteResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_InValid_Item_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var item = DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);

            // Act
            var result = controller.Edit(data.Id, (string)null) as RedirectToRouteResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_InValid_Item_Bogus_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var item = DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);

            // Act
            var result = controller.Edit(data.Id, "Bogus") as RedirectToRouteResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_InValid_AvatarComposite_Null_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.AvatarComposite = null;
            DataSourceBackend.Instance.StudentBackend.Update(data);

            var item = DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);

            // Act
            var result = controller.Edit(data.Id, item.Id) as RedirectToRouteResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            AvatarItemInputModel data = new AvatarItemInputModel();
            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Data_Invalid_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            AvatarItemInputModel data = new AvatarItemInputModel();
            data = null;

            // Act
            var result = (RedirectToRouteResult)controller.Edit(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Data_Invalid_StudentID_Null_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new AvatarItemInputModel
            {
                StudentId = null
            };

            // Act
            var result = (RedirectToRouteResult)controller.Edit(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Data_Invalid_ItemIdIsNull_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new AvatarItemInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = null
            };

            // Act
            var result = (RedirectToRouteResult)controller.Edit(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Data_Invalid_StudentId_Bogus_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new AvatarItemInputModel
            {
                StudentId = "bogus",
                ItemId = "itemID"
            };

            // Act
            var result = (RedirectToRouteResult)controller.Edit(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Data_Invalid_ItemId_Bogus_Should_Fail()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var data = new AvatarItemInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = "bogus"
            };

            // Act
            var result = (RedirectToRouteResult)controller.Edit(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Data_Valid_Position_Accessory_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = new AvatarItemInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetDefaultAvatarItemFullItem(AvatarItemCategoryEnum.Accessory).Id,
                Position = AvatarItemCategoryEnum.Accessory
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.AvatarComposite.AccessoryId;

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.AvatarComposite.AccessoryId, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Data_Valid_Position_Topper_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = new AvatarItemInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetDefaultAvatarItemFullItem(AvatarItemCategoryEnum.Cheeks).Id,
                Position = AvatarItemCategoryEnum.Cheeks
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.AvatarComposite.CheeksId;

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.AvatarComposite.CheeksId, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Data_Valid_Position_Menu_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = new AvatarItemInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetDefaultAvatarItemFullItem(AvatarItemCategoryEnum.Expression).Id,
                Position = AvatarItemCategoryEnum.Expression
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.AvatarComposite.ExpressionId;

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.AvatarComposite.ExpressionId, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Data_Valid_Position_Sign_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = new AvatarItemInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetDefaultAvatarItemFullItem(AvatarItemCategoryEnum.HairBack).Id,
                Position = AvatarItemCategoryEnum.HairBack
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.AvatarComposite.HairBackId;

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.AvatarComposite.HairBackId, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Data_Valid_Position_HairFront_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = new AvatarItemInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetDefaultAvatarItemFullItem(AvatarItemCategoryEnum.HairFront).Id,
                Position = AvatarItemCategoryEnum.HairFront
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.AvatarComposite.HairFrontId;

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.AvatarComposite.HairFrontId, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Data_Valid_Position_Head_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = new AvatarItemInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetDefaultAvatarItemFullItem(AvatarItemCategoryEnum.Head).Id,
                Position = AvatarItemCategoryEnum.Head
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.AvatarComposite.HeadId;

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.AvatarComposite.HeadId, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Data_Valid_Position_Pants_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = new AvatarItemInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetDefaultAvatarItemFullItem(AvatarItemCategoryEnum.Pants).Id,
                Position = AvatarItemCategoryEnum.Pants
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.AvatarComposite.PantsId;

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.AvatarComposite.PantsId, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Edit_Data_Valid_Position_ShirtFull_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var data = new AvatarItemInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetDefaultAvatarItemFullItem(AvatarItemCategoryEnum.ShirtFull).Id,
                Position = AvatarItemCategoryEnum.ShirtFull
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.AvatarComposite.ShirtFullId;

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.AvatarComposite.ShirtFullId, TestContext.TestName);
        }

        //[TestMethod]
        //public void Controller_AvatarSelectShop_Edit_Data_Valid_Position_ShirtShort_Should_Pass()
        //{
        //    // Arrange
        //    var controller = new AvatarSelectController();
        //    var data = new AvatarItemInputModel();
        //    data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
        //    data.ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetDefaultAvatarItemFullItem(AvatarItemCategoryEnum.ShirtShort).Id;
        //    data.Position = AvatarItemCategoryEnum.ShirtShort;

        //    // Get the Student Record
        //    var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
        //    var expect = myStudent.AvatarComposite.ShirtShortId;

        //    // Act
        //    ViewResult result = controller.Edit(data) as ViewResult;
        //    var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();

        //    // Assert
        //    Assert.AreEqual(expect, myStudent2.AvatarComposite.ShirtShortId, TestContext.TestName);
        //}
        #endregion Edit

        #region Inventory
        [TestMethod]
        public void Controller_AvatarSelectShop_Inventory_Default_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.Inventory(id) as ViewResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_AvatarSelectShop_Inventory_ItemIsNotNull_Should_Pass()
        {
            // Arrange
            var controller = new AvatarSelectController();
            var student = new StudentModel();
            var InventoryList = DataSourceBackend.Instance.AvatarItemBackend.Index();
            student.Id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            student.AvatarInventory = InventoryList;
            DataSourceBackend.Instance.StudentBackend.Update(student);

            ViewResult result = controller.Inventory(student.Id) as ViewResult;

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        //[TestMethod]
        //public void Controller_AvatarSelectShop_Inventory_Post_ModelIsInvalid_Should_Pass()
        //{
        //    // Arrange
        //    var controller = new AvatarSelectController();
        //    var data = new ShopBuyViewModel();

        //    // Make ModelState Invalid
        //    controller.ModelState.AddModelError("test", "test");

        //    // Act
        //    ViewResult result = controller.Inventory(data) as ViewResult;

        //    // Reset
        //    DataSourceBackend.Instance.Reset();

        //    // Assert
        //    Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        //}

        [TestMethod]
        public void Controller_AvatarSelectShop_Inventory_Get_myDataIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            var controller = new AvatarSelectController();

            var id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();
            DataSourceBackend.SetTestingMode(true);

            // Act
            var result = (RedirectToRouteResult)controller.Inventory(id);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        //[TestMethod]
        //public void Controller_AvatarSelectShop_Inventory_Data_Invalid_Null_Should_Fail()
        //{
        //    // Arrange
        //    var controller = new AvatarSelectController();

        //    ShopBuyViewModel data;
        //    data = null;

        //    // Act
        //    var result = (RedirectToRouteResult)controller.Inventory(data);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();

        //    // Assert
        //    Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        //}

        //[TestMethod]
        //public void Controller_AvatarSelectShop_Inventory_Data_Invalid_StudentID_Null_Should_Fail()
        //{
        //    // Arrange
        //    var controller = new AvatarSelectController();

        //    var data = new ShopBuyViewModel();
        //    data.StudentId = null;

        //    // Act
        //    var result = (RedirectToRouteResult)controller.Inventory(data);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();

        //    // Assert
        //    Assert.AreEqual("Inventory", result.RouteValues["action"], TestContext.TestName);
        //}

        //[TestMethod]
        //public void Controller_AvatarSelectShop_Inventory_Data_Invalid_ItemId_Null_Should_Fail()
        //{
        //    // Arrange
        //    var controller = new AvatarSelectController();

        //    var data = new ShopBuyViewModel();
        //    data.StudentId = "studentID";
        //    data.ItemId = null;

        //    // Act
        //    var result = (RedirectToRouteResult)controller.Inventory(data);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();

        //    // Assert
        //    Assert.AreEqual("Inventory", result.RouteValues["action"], TestContext.TestName);
        //}

        //[TestMethod]
        //public void Controller_AvatarSelectShop_Inventory_Data_Invalid_StudentId_Bogus_Should_Fail()
        //{
        //    // Arrange
        //    var controller = new AvatarSelectController();

        //    var data = new ShopBuyViewModel();
        //    data.StudentId = "bogus";
        //    data.ItemId = "itemID";

        //    // Act
        //    var result = (RedirectToRouteResult)controller.Inventory(data);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();

        //    // Assert
        //    Assert.AreEqual("Inventory", result.RouteValues["action"], TestContext.TestName);
        //}

        //[TestMethod]
        //public void Controller_AvatarSelectShop_Inventory_Data_Invalid_ItemId_Bogus_Should_Fail()
        //{
        //    // Arrange
        //    var controller = new AvatarSelectController();

        //    var data = new ShopBuyViewModel();
        //    data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
        //    data.ItemId = "bogus";

        //    // Act
        //    var result = (RedirectToRouteResult)controller.Inventory(data);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();

        //    // Assert
        //    Assert.AreEqual("Inventory", result.RouteValues["action"], TestContext.TestName);
        //}

        //[TestMethod]
        //public void Controller_AvatarSelectShop_Inventory_Data_Valid_Should_Pass()
        //{
        //    // Arrange
        //    var controller = new AvatarSelectController();
        //    var data = new ShopBuyViewModel();
        //    data.StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
        //    data.ItemId = DataSourceBackend.Instance.AvatarItemBackend.GetFirstAvatarItemId();
        //    var InventoryList = DataSourceBackend.Instance.AvatarItemBackend.Index();
        //    var Item = new AvatarItemModel();

        //    // Get the Student Record
        //    var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

        //    // Get the Inventory
        //    myStudent.AvatarInventory = InventoryList;
        //    DataSourceBackend.Instance.StudentBackend.Update(myStudent);

        //    // remove the data
        //    Item.Id = data.ItemId;
        //    InventoryList.Remove(Item);
        //    var expect = InventoryList;

        //    // Act
        //    ViewResult result = controller.Inventory(data) as ViewResult;
        //    var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

        //    // Reset
        //    DataSourceBackend.Instance.Reset();

        //    // Assert
        //    Assert.AreEqual(expect, myStudent2.AvatarInventory, TestContext.TestName);
        //}

        #endregion Inventory
    }
}
