﻿using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;
using System;
using System.Linq;

namespace _5051.Controllers
{
    /// <summary>
    /// The Kiosk that will run in the classroom
    /// </summary>
    public class KioskController : BaseController
    {
        /// <summary>
        /// Return the list of students with the status of logged in or out
        /// </summary>
        /// <returns></returns>
        // GET: Kiosk
        public ActionResult Index()
        {
            //TODO: Need to add a check here to validate if the request comes from a validated login or not.

            var myDataList = DataSourceBackend.Instance.StudentBackend.Index();

            var previousDateUTC = DataSourceBackend.Instance.KioskSettingsBackend.GetLatestDate();
            var previousDate = UTCConversionsBackend.UtcToKioskTime(previousDateUTC).Date;

            var currentDateUTC = DateTimeHelper.Instance.GetDateTimeNowUTC();
            var currentDate = UTCConversionsBackend.UtcToKioskTime(currentDateUTC).Date;

            // Compare the current date with the last saved date from KioskSettings
            if (DateTime.Compare(previousDate, currentDate) != 0) //If date has changed
            {
                DataSourceBackend.Instance.StudentBackend.ResetStatusAndProcessNewAttendance();

                // Save the update date back to KioskSettings
                DataSourceBackend.Instance.KioskSettingsBackend.UpdateLatestDate(currentDateUTC);
            }

            var myReturn = new AdminReportIndexViewModel(myDataList)
            {

                //top 5 leaderboard
                Leaderboard = ReportBackend.Instance.GenerateLeaderboard().Take(5).ToList()
            };

            return View(myReturn);       
        }

        /*
         * 
        // GET: Kiosk/SetLogout/5
        /// <summary>
        /// Manages the Login action, toggles the state
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns></returns>
        public ActionResult SetLogin(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            DataSourceBackend.Instance.StudentBackend.ToggleStatusById(id);
            return RedirectToAction("ConfirmLogin", "Kiosk", new { id });
        }
        *
        */


        /// <summary>
        /// student login with emotion status on Kiosk lndex page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Kiosk/KioskLogin
        [HttpPost]
        public ActionResult KioskLogin([Bind(Include=
                                        "Id,"+
                                        "EmotionCurrent,"+
                                        "")] StudentModel data)
        {

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            // perform student log in and update data
            DataSourceBackend.Instance.StudentBackend.ToggleEmotionStatusById(data.Id, data.EmotionCurrent);

            return RedirectToAction("ConfirmLogin", "Kiosk", new { id=data.Id });
        }

        /*
         * 
        // GET: Kiosk/SetLogout/5
        /// <summary>
        /// Manages the logout action, toggles the state
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns></returns>
        public ActionResult SetLogout(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            DataSourceBackend.Instance.StudentBackend.ToggleStatusById(id);
            return RedirectToAction("ConfirmLogout", "Kiosk", new { id });
        }   
        *
        */

        /// <summary>
        /// student logout with emotion status on Kiosk lndex page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Kiosk/KioskLogin
        [HttpPost]
        public ActionResult KioskLogout([Bind(Include=
                                        "Id,"+
                                        "EmotionCurrent,"+
                                        "")] StudentModel data)
        {

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            // perform student log out and update data
            DataSourceBackend.Instance.StudentBackend.ToggleEmotionStatusById(data.Id, data.EmotionCurrent);
            return RedirectToAction("ConfirmLogout", "Kiosk", new { id = data.Id });
        }

        /// <summary>
        /// Shows the login confirmation screen
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns></returns>
        public ActionResult ConfirmLogin(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            var myDataList = DataSourceBackend.Instance.StudentBackend.Read(id);
            var StudentViewModel = new StudentDisplayViewModel(myDataList)
            {
                LastDateTime = UTCConversionsBackend.UtcToKioskTime(DateTimeHelper.Instance.GetDateTimeNowUTC())
            };

            return View(StudentViewModel);
        }

        /// <summary>
        /// Shows the login confirmation screen
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns></returns>
        public ActionResult ConfirmLogout(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            var myDataList = DataSourceBackend.Instance.StudentBackend.Read(id);
            var StudentViewModel = new StudentDisplayViewModel(myDataList)
            {

                //Todo, replace with actual transition time
                LastDateTime = UTCConversionsBackend.UtcToKioskTime(DateTimeHelper.Instance.GetDateTimeNowUTC())
            };

            return View(StudentViewModel);
        }

        /// <summary>
        /// Will prompt for the kiosk login
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Login takes the password sent in and compares it with the settings for kiosk
        /// If they match, then it redirects to Index
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include=
                                        "Password,"+
                                        "")] KioskSettingsModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit, with Error Message
                return View(data);
            }

            if (string.IsNullOrEmpty(data.Password))
            {
                ModelState.AddModelError("Password", "Please Enter a Password.");
                return View(data);
            }

            var myKioskData = DataSourceBackend.Instance.KioskSettingsBackend.GetDefault();
            // GetDefault always returns valid data.

            // If the passwords match, then redirect
            if (data.Password.Equals(myKioskData.Password))
            {
                //Todo, set flag to mark the current token for the kiosk
                return RedirectToAction("Index", "Kiosk");
            }

            // Login failed, so send back with error message
            ModelState.AddModelError("Password", "Invalid login attempt.");
            return View(data);
        }

        /// <summary>
        /// This opens up the make a new Student screen
        /// </summary>
        /// <returns></returns>
        // GET: Kiosk/Create
        public ActionResult Create()
        {
            var myData = new StudentModel();
            return View(myData);
        }

        /// <summary>
        /// Make a new Student sent in by the create Student screen
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        // POST: Kiosk/Create
        [HttpPost]
        public ActionResult Create([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "Status,"+
                                        "Tokens,"+
                                        "ExperiencePoints,"+
                                        "AvatarLevel,"+
                                        "Password," +
                                        "")] StudentModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.Name))
            {
                ModelState.AddModelError("Name", "Please Enter a Name.");
                // Return back for Edit
                return View(data);
            }

            DataSourceBackend.Instance.StudentBackend.Create(data);

            return RedirectToAction("Index");
        }

    }
}
