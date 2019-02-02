using System;
using System.Linq;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    public class AvatarSelectController : BaseController
    {
        /// <summary>
        /// Index to the AvatarSelect
        /// </summary>
        /// <returns></returns>
        // GET: AvatarSelect
        public ActionResult Index(string id = null)
        {
            // Temp hold the Student Id for the Nav, until the Nav can call for Identity.
            ViewBag.StudentId = id;

            // Get the Student
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(myStudent);
        }

        /// <summary>
        /// What to Buy at the store
        /// </summary>
        /// <returns></returns>
        // GET: Buy
        public ActionResult Select(string id = null)
        {
            // Temp hold the Student Id for the Nav, until the Nav can call for Identity.
            ViewBag.StudentId = id;

            // Load the list of data into the AvatarItemList
            var myData = new SelectedAvatarItemListForStudentViewModel();

            // Get the Student
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }
            myData.Student = myStudent;

            // Get the Inventory
            var InventoryList = DataSourceBackend.Instance.AvatarItemBackend.Index();

            // Sort the Inventory into List per Category
            // Load the ones
            foreach (var item in Enum.GetValues(typeof(AvatarItemCategoryEnum)))
            {
                var temp = new AvatarItemViewModel
                {
                    Category = (AvatarItemCategoryEnum)item,
                    AvatarItemList = InventoryList.Where(m => m.Category == (AvatarItemCategoryEnum)item).ToList()
                };

                if (temp.AvatarItemList.Any())
                {
                    // todo, tag the ones that are already owned
                    myData.AvatarItemCategoryList.Add(temp);
                }
            }

            return View(myData);
        }

        [HttpPost]
        public ActionResult Select([Bind(Include=
                                        "StudentId,"+
                                        "ItemId,"+
                                        "")] ShopBuyViewModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit, with Error Message
                return RedirectToAction("Select", "AvatarSelect", new { id = data.StudentId });
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.StudentId))
            {
                // Send back for Edit
                return RedirectToAction("Select", "AvatarSelect", new { id = data.StudentId });
            }

            // Temp hold the Student Id for the Nav, until the Nav can call for Identity.
            ViewBag.StudentId = data.StudentId;

            if (string.IsNullOrEmpty(data.ItemId))
            {
                // Send back for Edit
                return RedirectToAction("Select", "AvatarSelect", new { id = data.StudentId });
            }

            // Get Student
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            if (myStudent == null)
            {
                // Send back for Edit
                return RedirectToAction("Select", "AvatarSelect", new { id = data.StudentId });
            }

            // Get Item
            var myItem = DataSourceBackend.Instance.AvatarItemBackend.Read(data.ItemId);
            if (myItem == null)
            {
                // Send back for Edit
                return RedirectToAction("Select", "AvatarSelect", new { id = data.StudentId });
            }

            // Check the Student Token amount, If not enough, return error
            if (myStudent.Tokens < myItem.Tokens)
            {
                // Not enough money...
                // Send back for Edit
                return RedirectToAction("Select", "AvatarSelect", new { id = data.StudentId });
            }

            // check the quantities of item
            if (myItem.Quantities < 1)
            {
                // Not enough quantity...
                // Send back for Edit
                return RedirectToAction("Select", "AvatarSelect", new { id = data.StudentId });
            }

            // Check the Item amount, If not enough, return error
            if (myItem.Tokens < 1)
            {
                // Not enough money...
                // Send back for Edit
                return RedirectToAction("Select", "AvatarSelect", new { id = data.StudentId });
            }

            // Check to see if the student already has the item.  If so, don't buy again, only 1 item per student
            var ItemAlreadyExists = myStudent.AvatarInventory.Exists(m => m.Id == myItem.Id);
            if (ItemAlreadyExists)
            {
                // Already own it.
                return RedirectToAction("Select", "AvatarSelect", new { id = data.StudentId });
            }

            // Reduce the Student Tokens by Item Price
            myStudent.Tokens -= myItem.Tokens;

            // Time to buy !
            // Check if the item is limited or not
            if (myItem.IsLimitSupply == true)
            {
                // Reduce the quantities of Item
                myItem.Quantities -= 1;
            }

            // Add Item to Student Inventory
            // TODO:  Mike, add inventory to Students...
            myStudent.AvatarInventory.Add(myItem);

            // Update Student
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            return RedirectToAction("Select", "AvatarSelect", new { id = data.StudentId });
        }

        /// <summary>
        /// store items of the AvatarSelect
        /// </summary>
        /// <returns></returns>
        // GET: Inventory
        public ActionResult Inventory(string id = null)
        {
            // Temp hold the Student Id for the Nav, until the Nav can call for Identity.
            ViewBag.StudentId = id;

            var myData = new SelectedAvatarItemListForStudentViewModel();

            // Get the Student
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }
            myData.Student = myStudent;

            var InventoryList = myStudent.AvatarInventory;

            foreach (var item in Enum.GetValues(typeof(AvatarItemCategoryEnum)))
            {
                var temp = new AvatarItemViewModel
                {
                    Category = (AvatarItemCategoryEnum)item,
                    AvatarItemList = InventoryList.Where(m => m.Category == (AvatarItemCategoryEnum)item).ToList()
                };

                if (temp.AvatarItemList.Any())
                {
                    // todo, tag the ones that are already owned
                    myData.AvatarItemCategoryList.Add(temp);
                }
            }

            return View(myData);
        }

        /// <summary>
        /// Edit The items from the Inventory that match the Item passed in
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string id = null, string item = null)
        {
            // Temp hold the Student Id for the Nav, until the Nav can call for Identity.
            ViewBag.StudentId = id;

            if (string.IsNullOrEmpty(item))
            {
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            var studentdata = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (studentdata == null)
            {
                return RedirectToAction("Error", "Home");
            }

            if (studentdata.AvatarComposite == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Get Item
            var myItem = DataSourceBackend.Instance.AvatarItemBackend.Read(item);
            if (myItem == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Use the Item, to populate the ShopViewModel
            var data = DataSourceBackend.Instance.AvatarItemBackend.GetAvatarShopViewModel(studentdata, myItem);

            //Return Truck Data
            return View(data);
        }

        /// <summary>
        /// Update the Current Inventory Slot to use tha Item passed up.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit([Bind(Include=
                                        "StudentId,"+
                                        "ItemId,"+
                                        "Position,"+
                                        "")] AvatarItemInputModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit, with Error Message
                return RedirectToAction("Index", "AvatarSelect", new { id = data.StudentId });
            }

            if (data == null)
            {
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.StudentId))
            {
                // Send back for Edit
                return RedirectToAction("Index", "AvatarSelect", new { id = data.StudentId });
            }

            if (string.IsNullOrEmpty(data.ItemId))
            {
                // Send back for Edit
                return RedirectToAction("Index", "AvatarSelect", new { id = data.StudentId });
            }


            // Get Student
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(data.StudentId);
            if (myStudent == null)
            {
                // Send back for Edit
                return RedirectToAction("Index", "AvatarSelect", new { id = data.StudentId });
            }

            // Get Item
            var myItem = DataSourceBackend.Instance.AvatarItemBackend.Read(data.ItemId);
            if (myItem == null)
            {
                // Send back for Edit
                return RedirectToAction("Index", "AvatarSelect", new { id = data.StudentId });
            }

            var AvatarBase = "/Content/Avatar/";

            switch (data.Position)
            {
                case AvatarItemCategoryEnum.Accessory:
                    myStudent.AvatarComposite.AccessoryId = myItem.Id;
                    myStudent.AvatarComposite.AccessoryUri = myItem.Uri;
                    myStudent.AvatarComposite.AvatarAccessoryUri = AvatarBase + myItem.Uri;
                    break;

                case AvatarItemCategoryEnum.Cheeks:
                    myStudent.AvatarComposite.CheeksId = myItem.Id;
                    myStudent.AvatarComposite.CheeksUri = myItem.Uri;
                    myStudent.AvatarComposite.AvatarCheeksUri = AvatarBase + myItem.Uri;
                    break;

                case AvatarItemCategoryEnum.Expression:
                    myStudent.AvatarComposite.ExpressionId = myItem.Id;
                    myStudent.AvatarComposite.ExpressionUri = myItem.Uri;
                    myStudent.AvatarComposite.AvatarExpressionUri = AvatarBase + myItem.Uri;
                    break;

                case AvatarItemCategoryEnum.HairBack:
                    myStudent.AvatarComposite.HairBackId = myItem.Id;
                    myStudent.AvatarComposite.HairBackUri = myItem.Uri;
                    myStudent.AvatarComposite.AvatarHairBackUri = AvatarBase + myItem.Uri;
                    break;

                case AvatarItemCategoryEnum.HairFront:
                    myStudent.AvatarComposite.HairFrontId = myItem.Id;
                    myStudent.AvatarComposite.HairFrontUri = myItem.Uri;
                    myStudent.AvatarComposite.AvatarHairFrontUri = AvatarBase + myItem.Uri;
                    break;

                case AvatarItemCategoryEnum.Head:
                    myStudent.AvatarComposite.HeadId = myItem.Id;
                    myStudent.AvatarComposite.HeadUri = myItem.Uri;
                    myStudent.AvatarComposite.AvatarHeadUri = AvatarBase + myItem.Uri;
                    break;

                case AvatarItemCategoryEnum.Pants:
                    myStudent.AvatarComposite.PantsId = myItem.Id;
                    myStudent.AvatarComposite.PantsUri = myItem.Uri;
                    myStudent.AvatarComposite.AvatarPantsUri = AvatarBase + myItem.Uri;
                    break;

                case AvatarItemCategoryEnum.ShirtFull:
                    myStudent.AvatarComposite.ShirtFullId = myItem.Id;
                    myStudent.AvatarComposite.ShirtFullUri = myItem.Uri + ".png";
                    myStudent.AvatarComposite.AvatarShirtFullUri = AvatarBase + myItem.Uri;

                    myStudent.AvatarComposite.ShirtShortId = myItem.Id;
                    var temp = myItem.Uri.Split('.');
                    myStudent.AvatarComposite.ShirtShortUri = temp[0] + "_short.png"; ;
                    myStudent.AvatarComposite.AvatarShirtShortUri = AvatarBase + myStudent.AvatarComposite.ShirtShortUri;
                    break;

            }

            // Update Student
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            return RedirectToAction("Index", "AvatarSelect", new
            {
                id = data.StudentId
            });
        }
    }
}