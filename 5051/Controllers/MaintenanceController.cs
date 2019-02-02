using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
//using _5051.Maintenance;

namespace _5051.Controllers
{
    public class MaintenanceController : BaseController
    {
        // GET: Support
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BackupData()
        {
            // TODO:
            // Lookup Current user, make sure they are in the support list.
            //var findResult = IdentityBackend.Instance.FindUserByID(id);
            //if (findResult == null)
            //{
            //return RedirectToAction("UserList", "Support");
            //}

            var data = new BackupDataInputModel
            {
                Source = SystemGlobalsModel.Instance.DataSourceValue,
                ConfirmSource = DataSourceEnum.Unknown,

                Destination = DataSourceEnum.Unknown,
                ConfirmDestination = DataSourceEnum.Unknown,

                Password = ""
            };

            return View(data);
        }

        [HttpPost]
        public ActionResult BackupData([Bind(Include =
                                            "Source," +
                                            "ConfirmSource," +
                                            "Destination," +
                                            "ConfirmDestination," +
                                            "Password," +
                                             "")] BackupDataInputModel BackupData)
        {
            // Todo: 
            // Check for Valid User
            //return RedirectToAction("UserList", "Support");

            if (!ModelState.IsValid)
            {
                return RedirectToAction("BackupData", "Maintenance");
            }

            if (BackupData.Destination != BackupData.ConfirmDestination)
            {
                return RedirectToAction("BackupData", "Maintenance");
            }

            if (BackupData.Source != BackupData.ConfirmSource)
            {
                return RedirectToAction("BackupData", "Maintenance");
            }

            if (string.IsNullOrEmpty(BackupData.Password))
            {
                return RedirectToAction("BackupData", "Maintenance");
            }

            var temp = new Maintain.DataBackupMaintenance();
            temp.DataBackup(BackupData.Source, BackupData.Destination);

            return RedirectToAction("Index", "Maintenance");
        }

        public ActionResult ResetCalendar()
        {
            // TODO: Troy
            // Lookup Current user, make sure they are in the support list.
            //var findResult = IdentityBackend.Instance.FindUserByID(id);
            //if (findResult == null)
            //{
            //return RedirectToAction("UserList", "Support");
            //}

            var data = new LoginViewModel
            {
                Password = "",
                Email = ""
            };

            return View(data);
        }

        [HttpPost]
        public ActionResult ResetCalendar([Bind(Include =
                                            "Password," +
                                            "Email," +
                                             "")] LoginViewModel data)
        {
            // Todo: Troy
            // Check for Valid User
            //return RedirectToAction("UserList", "Support");

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Maintenance");
            }

            if (string.IsNullOrEmpty(data.Password))
            {
                return RedirectToAction("Index", "Maintenance");
            }

            // TODO:  Troy, changes this from 123, to be support password via code lookup
            // User will already be logged in, so this is more just a double check that they want to do the operation
            if (!data.Password.Equals("123"))
            {
                return RedirectToAction("Index", "Maintenance");
            }
            
            var temp = new Maintain.CalendarMaintenance();
            temp.ResetCalendar();

            return RedirectToAction("Index", "Maintenance");
        }

        public ActionResult RefreshAvatars()
        {
            // TODO: Troy
            // Lookup Current user, make sure they are in the support list.
            //var findResult = IdentityBackend.Instance.FindUserByID(id);
            //if (findResult == null)
            //{
            //return RedirectToAction("UserList", "Support");
            //}

            var data = new LoginViewModel
            {
                Password = "",
                Email = ""
            };

            return View(data);
        }

        [HttpPost]
        public ActionResult RefreshAvatars([Bind(Include =
                                            "Password," +
                                            "Email," +
                                             "")] LoginViewModel data)
        {
            // Todo: Troy
            // Check for Valid User
            //return RedirectToAction("UserList", "Support");

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Maintenance");
            }

            if (string.IsNullOrEmpty(data.Password))
            {
                return RedirectToAction("Index", "Maintenance");
            }

            // TODO:  Troy, changes this from 123, to be support password via code lookup
            // User will already be logged in, so this is more just a double check that they want to do the operation
            if (!data.Password.Equals("123"))
            {
                return RedirectToAction("Index", "Maintenance");
            }

            var temp = new Maintain.AvatarItemMaintenance();
            temp.RefreshAvatars();

            return RedirectToAction("Index", "Maintenance");
        }

    }
}