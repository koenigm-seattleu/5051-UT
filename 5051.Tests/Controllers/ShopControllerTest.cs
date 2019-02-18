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
    public class ShopControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region IndexRegion

        [TestMethod]
        public void Controller_Shop_Index_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.Index(id) as ViewResult;


            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Index_With_Empty_List_Should_Return_Error_Page()
        {
            // Arrange
            ShopController controller = new ShopController();

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


        #region FactoryRegion
        [TestMethod]
        public void Controller_Shop_Factory_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.Factory(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            ShopBuyViewModel data = new ShopBuyViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Factory(data) as ViewResult;

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }
        
        [TestMethod]
        public void Controller_Shop_Factory_Get_myDataIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            ShopController controller = new ShopController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Act
            var result = (RedirectToRouteResult)controller.Factory(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_Invalid_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            ShopBuyViewModel data;
            data = null;

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_Invalid_StudentID_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel
            {
                StudentId = null
            };

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_Invalid_ItemId_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel
            {
                StudentId = "studentID",
                ItemId = null
            };

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_Invalid_StudentId_Bogus_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel
            {
                StudentId = "bogus",
                ItemId = "itemID"
            };

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_Invalid_ItemId_Bogus_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = "bogus"
            };

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_Valid_Item_Quantity_Is_Not_Limited_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetFirstFactoryInventoryId()
            };

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 1000;
            myStudent.Inventory.Clear();    //Clear inventory
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.FactoryInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 10;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(myInventory);

            var expect = myStudent.Tokens - myInventory.Tokens;
            var expectOutcome = myStudent.Truck.Outcome + myInventory.Tokens;

            // Act
            ViewResult result = controller.Factory(data) as ViewResult;

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectOutcome, myStudent2.Truck.Outcome, TestContext.TestName);
            Assert.IsNotNull(myStudent2.Truck.BusinessList, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_Valid_Item_Is_Limited_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id
            };
            var ItemList = DataSourceBackend.Instance.FactoryInventoryBackend.Index();
            data.ItemId = ItemList.Find(n => n.IsLimitSupply == true).Id;

            // Get the Item which is limited

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 1000;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.FactoryInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 10;
            myInventory.Quantities = 10;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(myInventory);

            var expect = myStudent.Tokens - myInventory.Tokens;
            var expectOutcome = myStudent.Truck.Outcome + myInventory.Tokens;
            var expectQuantities = myInventory.Quantities - 1;

            // Act
            ViewResult result = controller.Factory(data) as ViewResult;

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectOutcome, myStudent2.Truck.Outcome, TestContext.TestName);
            Assert.AreEqual(expectQuantities, myInventory.Quantities, TestContext.TestName);
            Assert.IsNotNull(myStudent2.Truck.BusinessList, TestContext.TestName);
        }


        [TestMethod]
        public void Controller_Shop_Factory_Data_InValid_Quantities_Not_Enough_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetFirstFactoryInventoryId()
            };

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId); myStudent.Tokens = 1000;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.FactoryInventoryBackend.Read(data.ItemId);
            myInventory.Quantities = 0;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(myInventory);

            // No Purchase, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.Inventory.Count();

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.Inventory.Count(), TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_InValid_Tokens_Not_Enough_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetFirstFactoryInventoryId()
            };

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 10;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.FactoryInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 100;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(myInventory);

            // No Purchase, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.Inventory.Count();

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.Inventory.Count(), TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_InValid_Tokens_Less_Than_One_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetFirstFactoryInventoryId()
            };

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 0;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.FactoryInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 0;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(myInventory);

            // No Purchase, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.Inventory.Count();

            // Act
            var result = (RedirectToRouteResult)controller.Factory(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.Inventory.Count(), TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Factory_Data_InValid_Item_Already_Exists_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopBuyViewModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetFirstFactoryInventoryId()
            };

            // Get the Student Record and Add some Tokens to it.
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            myStudent.Tokens = 1000;
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            // Get the Item Record and Set the Token Value
            var myInventory = DataSourceBackend.Instance.FactoryInventoryBackend.Read(data.ItemId);

            myInventory.Tokens = 10;
            DataSourceBackend.Instance.FactoryInventoryBackend.Update(myInventory);

            // Buy it one time, this puts the item in the student inventory
            var myPurchase1 = (RedirectToRouteResult)controller.Factory(data);

            // No Purchase, so tokens stay the same
            var expect = myStudent.Tokens;
            var expectCount = myStudent.Inventory.Count();

            // Act

            // Trying to buy the second time will fail
            var result = (RedirectToRouteResult)controller.Factory(data);

            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);

            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Factory", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual(expect, myStudent2.Tokens, TestContext.TestName);
            Assert.AreEqual(expectCount, myStudent2.Inventory.Count(), TestContext.TestName);
        }
        #endregion FactoryRegion

        #region DiscountsRegion

        [TestMethod]
        public void Controller_Shop_Discounts_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();

            // Act
            ViewResult result = controller.Discounts() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion DiscountsRegion

        #region SpecialsRegion

        [TestMethod]
        public void Controller_Shop_Specials_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();

            // Act
            ViewResult result = controller.Specials() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion SpecialsRegion

        //#region VisitRegion
        //[TestMethod]
        //public void Controller_Shop_Visit_Valid_Should_Return_List()
        //{
        //    // Arrange
        //    ShopController controller = new ShopController();
        //    var expect = Backend.DataSourceBackend.Instance.StudentBackend.Index();

        //    //// Act
        //    var resultCall = controller.Visit() as ViewResult;
        //    var result = (List<StudentModel>)resultCall.Model;

        //    //// Assert
        //    Assert.AreEqual(expect, result, TestContext.TestName);
        //}
        //#endregion VisitRegion

        #region VisitRegion
        [TestMethod]
        public void Controller_Shop_Visit_Null_Id_Should_Return_IndexPage()
        {
            // Arrange
            ShopController controller = new ShopController();
            string id = null;

            // Reset
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Act
            var result = controller.Visit(id) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Shop", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Visit_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            var result = controller.Visit(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion VisitRegion

        #region VisitShopRegion
        [TestMethod]
        public void Controller_Shop_VisitShop_Invalid_Null_Id_Should_Return_RosterPage()
        {
            //// Arrange
            ShopController controller = new ShopController();
            string id = null;

            //// Act
            var result = (RedirectToRouteResult)controller.VisitShop(id);

            //// Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Visit_Invalid_ID_Should_Fail()
        {
            //// Arrange
            ShopController controller = new ShopController();
            string id = "bogus";

            //// Act
            var result = (RedirectToRouteResult)controller.VisitShop(id);

            //// Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }


        [TestMethod]
        public void Controller_Shop_Visit_Invalid_ID_Item_Should_Fail()
        {
            //// Arrange
            ShopController controller = new ShopController();
            string id = "bogus";

            //// Act
            var result = (RedirectToRouteResult)controller.VisitShop(id,"bogus");

            //// Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Visit_Valid_Id_Should_Pass()
        {
            //// Arrange
            ShopController controller = new ShopController();
            var data = Backend.DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expect = data.Name;

            //// Act
            var resultCall = controller.VisitShop(data.Id) as ViewResult;
            var result = (StudentModel)resultCall.Model;

            //// Assert
            Assert.AreEqual(expect, result.Name, TestContext.TestName);
        }


        [TestMethod]
        public void Controller_Shop_Visit_Valid_Id_Item_Should_Pass()
        {
            //// Arrange
            ShopController controller = new ShopController();
            var data = Backend.DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expect = data.Name;

            //// Act
            var resultCall = controller.VisitShop(data.Id,data.Id) as ViewResult;
            var result = (StudentModel)resultCall.Model;

            //// Assert
            Assert.AreEqual(expect, result.Name, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Visit_Valid_Id_Invalid_Item_Should_Pass()
        {
            //// Arrange
            ShopController controller = new ShopController();
            var data = Backend.DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expect = data.Name;

            //// Act
            var resultCall = controller.VisitShop(data.Id, "bogus") as ViewResult;
            var result = (StudentModel)resultCall.Model;

            //// Assert
            Assert.AreEqual(expect, result.Name, TestContext.TestName);
        }
        #endregion VisitShop

        #region CelebrityPoster
        [TestMethod]
        public void Controller_Shop_CelebrityPoster_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();

            // Act
            ViewResult result = controller.CelebrityPoster() as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion CelebrityPoster

        #region Edit
        [TestMethod]
        public void Controller_Shop_Edit_Invalid_No_StudentID_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            // Act
            var result = controller.Edit() as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Invalid_Null_StudentID_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            // Act
            var result = controller.Edit((string)null) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Invalid_StudentID_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            // Act
            var result = controller.Edit("bogus") as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Valid_StudentID_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();

            // Act
            var result = controller.Edit(data.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Valid_Truck_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();
            var data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Truck = null;

            // Act
            var result = controller.Edit(data.Id) as RedirectToRouteResult;

            //reset
            DataSourceBackend.Instance.Reset();


            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            ShopTruckInputModel data = new ShopTruckInputModel();
            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Data_Invalid_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            ShopTruckInputModel data = new ShopTruckInputModel();
            data = null;

            // Act
            var result = (RedirectToRouteResult)controller.Edit(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Data_Invalid_StudentID_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopTruckInputModel
            {
                StudentId = null
            };

            // Act
            var result = (RedirectToRouteResult)controller.Edit(data);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Data_Invalid_ItemIdIsNull_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopTruckInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = null
            };

            // Act
            var result = (RedirectToRouteResult)controller.Edit(data);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Data_Invalid_StudentId_Bogus_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopTruckInputModel
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
        public void Controller_Shop_Edit_Data_Invalid_ItemId_Bogus_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopTruckInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = "bogus"
            };

            // Act
            var result = (RedirectToRouteResult)controller.Edit(data);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Data_Valid_Position_Truck_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            var data = new ShopTruckInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Truck).Id,
                Position = FactoryInventoryCategoryEnum.Truck
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.Truck.Truck;

            // select item
            var mySelect = (RedirectToRouteResult)controller.Edit(data);
           
            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Truck.Truck, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Data_Valid_Position_Topper_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            var data = new ShopTruckInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Topper).Id,
                Position = FactoryInventoryCategoryEnum.Topper
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.Truck.Topper;

            // select item
            var mySelect = (RedirectToRouteResult)controller.Edit(data);

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Truck.Topper, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Data_Valid_Position_Menu_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            var data = new ShopTruckInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Menu).Id,
                Position = FactoryInventoryCategoryEnum.Menu
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.Truck.Menu;

            // select item
            var mySelect = (RedirectToRouteResult)controller.Edit(data);

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Truck.Menu, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Data_Valid_Position_Sign_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            var data = new ShopTruckInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Sign).Id,
                Position = FactoryInventoryCategoryEnum.Sign
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.Truck.Sign;

            // select item
            var mySelect = (RedirectToRouteResult)controller.Edit(data);

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Truck.Sign, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Data_Valid_Position_Wheels_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            var data = new ShopTruckInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Wheels).Id,
                Position = FactoryInventoryCategoryEnum.Wheels
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.Truck.Wheels;

            // select item
            var mySelect = (RedirectToRouteResult)controller.Edit(data);

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Truck.Wheels, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Edit_Data_Valid_Position_Trailer_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            var data = new ShopTruckInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                ItemId = DataSourceBackend.Instance.FactoryInventoryBackend.GetDefaultTruckFullItem(FactoryInventoryCategoryEnum.Trailer).Id,
                Position = FactoryInventoryCategoryEnum.Trailer
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.Truck.Trailer;

            // select item
            var mySelect = (RedirectToRouteResult)controller.Edit(data);

            // Act
            ViewResult result = controller.Edit(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Truck.Trailer, TestContext.TestName);
        }

        #endregion Edit

        #region EditName
        [TestMethod]
        public void Controller_Shop_EditName_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            ShopTruckInputModel data = new ShopTruckInputModel();
            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.EditName(data) as ViewResult;

            // Assert
            Assert.AreEqual(controller.ModelState.IsValid, false, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_EditName_Data_Invalid_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            ShopTruckInputModel data = new ShopTruckInputModel();
            data = null;

            // Act
            var result = (RedirectToRouteResult)controller.EditName(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_EditName_Data_Invalid_StudentID_Null_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopTruckInputModel
            {
                StudentId = null
            };

            // Act
            var result = (RedirectToRouteResult)controller.EditName(data);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_EditName_Data_Invalid_TruckNameIsNull_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopTruckInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                TruckName = null
            };

            // Act
            var result = (RedirectToRouteResult)controller.EditName(data);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_EditName_Data_Invalid_StudentId_Bogus_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();

            var data = new ShopTruckInputModel
            {
                StudentId = "bogus",
                TruckName = "truckName"
            };

            // Act
            var result = (RedirectToRouteResult)controller.EditName(data);

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_EditName_Data_Valid_Should_Pass()
        {
            // Arrange
            DataSourceBackend.Instance.Reset();
            ShopController controller = new ShopController();
            var data = new ShopTruckInputModel
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                TruckName = DataSourceBackend.Instance.StudentBackend.GetDefault().Truck.TruckName
            };

            // Get the Student Record
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            var expect = myStudent.Truck.TruckName;

            // select item
            var mySelect = (RedirectToRouteResult)controller.EditName(data);

            // Act
            ViewResult result = controller.EditName(data) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, myStudent2.Truck.TruckName, TestContext.TestName);
        }
        #endregion EditName


        #region Inventory
        [TestMethod]
        public void Controller_Shop_Inventory_Default_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            ViewResult result = controller.Inventory(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Inventory_ItemIsNotNull_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            var student = new StudentModel();
            var InventoryList = DataSourceBackend.Instance.FactoryInventoryBackend.Index();
            student.Id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Act
            student.Inventory = InventoryList;
            DataSourceBackend.Instance.StudentBackend.Update(student);

            ViewResult result = controller.Inventory(student.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_Inventory_Get_myDataIsNull_ShouldReturnErrorPage()
        {
            // Arrange
            ShopController controller = new ShopController();

            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            // Reset DataSourceBackend
            DataSourceBackend.Instance.Reset();

            // Act
            var result = (RedirectToRouteResult)controller.Inventory(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }
        #endregion Inventory

        #region BusinessReport
        [TestMethod]
        public void Controller_Shop_BusinessReport_Invalid_Null_Id_Should_Return_ErrorPage()
        {
            // Arrange
            ShopController controller = new ShopController();
            string id = null;

            // Act
            var result = (RedirectToRouteResult)controller.BusinessReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_BusinessReport_Invalid_ID_Should_Fail()
        {
            // Arrange
            ShopController controller = new ShopController();
            string id = "bogus";

            // Act
            var result = (RedirectToRouteResult)controller.BusinessReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Shop_BusinessReport_Valid_Id_Should_Pass()
        {
            // Arrange
            ShopController controller = new ShopController();
            var data = Backend.DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;
            
            // Act
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.Id);
            var expectIncome = myStudent.Truck.Income;
            var expectOutcome = myStudent.Truck.Outcome;
            var expectBusinessList = myStudent.Truck.BusinessList;

            var result = controller.BusinessReport(data.Id) as ViewResult;
            var myStudent2 = DataSourceBackend.Instance.StudentBackend.Read(data.Id);

            //// Assert
            Assert.AreEqual(expectIncome, myStudent2.Truck.Income, TestContext.TestName);
            Assert.AreEqual(expectOutcome, myStudent2.Truck.Outcome, TestContext.TestName);
            Assert.AreEqual(expectBusinessList, myStudent2.Truck.BusinessList, TestContext.TestName);
        }

        #endregion BusinessReport
    }
}
