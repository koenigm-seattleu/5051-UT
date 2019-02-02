using _5051.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using _5051.Backend;

namespace _5051.Controllers
{
    /// <summary>
    /// The Calendar crudi
    /// </summary>
    public class CalendarController : Controller
    {
        /// <summary>
        /// Calendar Index
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult Index()
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            var mySchoolDaysData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Index();

            // Removed null check: the data is OK to be count=0 but not possible to be null
            //if (mySchoolDaysData == null)
            //{
            //    // Send to Error Page
            //    return RedirectToAction("Error", "Home");
            //}

            var myData = new SchoolCalendarViewModel
            {
                SchoolDays = mySchoolDaysData,
                CurrentDate = UTCConversionsBackend.UtcToKioskTime(DateTimeHelper.Instance.GetDateTimeNowUTC())
            };

            myData.FirstDay = SchoolDismissalSettingsBackend.Instance.GetDefault().DayFirst;
            myData.LastDay = SchoolDismissalSettingsBackend.Instance.GetDefault().DayLast;

            //Get the First and last month, and set the dates to the first of those months.
            var dateStart = DateTime.Parse(myData.FirstDay.Month.ToString() + "/01/" +
                                           myData.FirstDay.Year.ToString());

            // Go to the month after the last date
            var dateEnd = DateTime.Parse((myData.LastDay.Month + 1).ToString() + "/01/" +
                                         myData.LastDay.Year.ToString());

            dateEnd.AddDays(-1); // Back up 1 day to be the last day of the month

            DateTime currentDate = new DateTime();

            currentDate = dateStart;

            // For every day from the start of the school year, until the end of the school year or now...
            while (currentDate.CompareTo(dateEnd) < 0)
            {
                if (currentDate.Day == 1)
                {
                    // For each month, build a list of the days for that month
                    var temp = myData.SchoolDays.Where(m => m.Date.Month == currentDate.Month).ToList();
                    myData.Months.Add(temp);
                }

                currentDate = currentDate.AddDays(1);
            }

            return View(myData);
        }

        /// <summary>
        /// Calendar Default resets the date to defaults for that date type
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult SetDefault(string id = null)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            if (id == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            var myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Read(id);
            if (myData == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.SetDefault(id);

            return RedirectToAction("Update", "Calendar", new { id });
        }

        /// <summary>
        /// Calendar Default resets the date to defaults for that date type
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult SetLateStart(string id = null)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            if (id == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            var myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Read(id);
            if (myData == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            myData.DayStart = Models.Enums.SchoolCalendarDismissalEnum.Late;
            myData.TimeStart = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().StartLate;

            myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Update(myData);

            return RedirectToAction("Update", "Calendar", new { id });
        }


        /// <summary>
        /// Calendar Default resets the date to defaults for that date type
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult SetEarlyEnd(string id = null)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            if (id == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            var myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Read(id);
            if (myData == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            myData.DayEnd = Models.Enums.SchoolCalendarDismissalEnum.Early;
            myData.TimeEnd = Backend.DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault().EndEarly;

            myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Update(myData);

            return RedirectToAction("Update", "Calendar", new { id });
        }

        /// <summary>
        /// Sets the school day as True, so times can be edited
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult SetSchoolDay(string id = null)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            if (id == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            var myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Read(id);
            if (myData == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            myData.SchoolDay = true;

            myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Update(myData);

            return RedirectToAction("Update", "Calendar", new { id });
        }

        /// <summary>
        /// Sets the school day as True, so times can be edited
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult SetNoSchoolDay(string id = null)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            if (id == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            var myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Read(id);
            if (myData == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            myData.SchoolDay = false;

            myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Update(myData);

            return RedirectToAction("Update", "Calendar", new { id });
        }

        /// <summary>
        /// Calendar Update
        /// </summary>
        /// <returns></returns>
        // GET: Calendar
        public ActionResult Update(string id = null)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            if (id == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            var myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Read(id);
            if (myData == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            return View(myData);
        }

        /// <summary>
        /// This updates the Calendar based on the information posted from the update page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Avatar/Update/5
        [HttpPost]
        public ActionResult Update([Bind(Include=
                                        "Id,"+
                                        "TimeStart,"+
                                        "TimeEnd,"+
                                        "")] SchoolCalendarModel data)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }

            if (data == null)
            {
                // Send to error page
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for Edit
                return RedirectToAction("Error", "Home");
            }

            // Validate Date and Times
            if (data.TimeStart.TotalHours < 1 || data.TimeStart.TotalHours > 24)
            {
                // Must be between 0 and 24
                ModelState.AddModelError("TimeStart", "Enter Valid Start Time");
                return View(data);
            }

            // Validate Date and Times
            if (data.TimeEnd.TotalHours < 1 || data.TimeEnd.TotalHours > 24)
            {
                // Must be between 0 and 24
                ModelState.AddModelError("TimeEnd", "Enter Valid End Time");
                return View(data);
            }

            // Validate Date and Times
            if (data.TimeEnd.Subtract(data.TimeStart).Ticks < 1)
            {
                // End is before Start
                ModelState.AddModelError("TimeStart", "Start Time must be before End Time");
                return View(data);
            }

            // Load the actual record, and only update TimeStart and Time End
            var myData = Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Read(data.Id);

            myData.TimeStart = data.TimeStart;
            myData.TimeEnd = data.TimeEnd;
            myData.SchoolDay = data.SchoolDay;

            Backend.DataSourceBackend.Instance.SchoolCalendarBackend.Update(myData);

            return RedirectToAction("Index");
        }
    }
}