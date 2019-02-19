using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    public class PortalController : BaseController
    {
        /// <summary>
        /// The list of all the active students in the class, so they can Roster
        /// </summary>
        /// <returns></returns>
        // GET: Portal
        public ActionResult Roster()
        {
            var myDataList = DataSourceBackend.Instance.StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            return View(StudentViewModel);
        }

        /// <summary>
        /// Index Page
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student Record as a Student View Model</returns>
        // GET: Portal
        public ActionResult Index(string id=null)
        {
            ViewBag.StudentId = id;

            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(id);

            if (myStudent == null)
            {
                return RedirectToAction("Roster", "Portal");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);

            //Set the last log in time and emotion status img uri
            if (myReturn.Attendance.Any())
            {
                myReturn.LastLogIn = UTCConversionsBackend.UtcToKioskTime(myReturn.Attendance.OrderByDescending(m => m.In).FirstOrDefault().In);
            }

            return View(myReturn);
        }
    }
}