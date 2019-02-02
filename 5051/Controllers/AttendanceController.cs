using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Backend;
using _5051.Models;

namespace _5051.Controllers
{
    //The controller that handles attendance crudi
    public class AttendanceController : Controller
    {
        // GET: Attendance. Select a student here
        public ActionResult Index()
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            // Load the list of data into the StudentList
            var myDataList = DataSourceBackend.Instance.StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            return View(StudentViewModel);
        }

        // GET: Attendance/Read/. Read the attendance history of the student
        public ActionResult Read(string id)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (myStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myReturn = new StudentDisplayViewModel(myStudent);

            var attendanceListOrdered = myReturn.Attendance.OrderByDescending(m => m.In);

            //Deep copy Attendance list and convert time zone
            var myAttendanceModels = new List<AttendanceModel>();

            foreach (var item in attendanceListOrdered)
            {
                var myAttendance = new AttendanceModel()
                {
                    //deep copy the AttendanceModel and convert time zone
                    In = UTCConversionsBackend.UtcToKioskTime(item.In),
                    Out = UTCConversionsBackend.UtcToKioskTime(item.Out),
                    Id = item.Id,
                    StudentId = myStudent.Id,
                    Emotion = item.Emotion,
                    EmotionUri = Emotion.GetEmotionURI(item.Emotion)
                };

                myAttendance.Id = item.Id;

                myAttendanceModels.Add(myAttendance);
            }

            myReturn.Attendance = myAttendanceModels;

            return View(myReturn);
        }

        // GET: Attendance/Detail
        // Read the details of the attendance(time in, time out).
        public ActionResult Details(string id, string item)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(item))
            {
                return RedirectToAction("Error", "Home");
            }

            //get the attendance with given id
            var myAttendance = DataSourceBackend.Instance.StudentBackend.ReadAttendance(id, item);
            if (myAttendance == null)
            {
                return RedirectToAction("Error", "Home");
            }

            //Create a new attendance to hold converted times
            var myReturn = new AttendanceModel
            {
                StudentId = myAttendance.StudentId,
                Id = myAttendance.Id,
                In = UTCConversionsBackend.UtcToKioskTime(myAttendance.In),
                Out = UTCConversionsBackend.UtcToKioskTime(myAttendance.Out),
                Emotion = myAttendance.Emotion,
                EmotionUri = Emotion.GetEmotionURI(myAttendance.Emotion),

                IsNew = myAttendance.IsNew
            };

            return View(myReturn);
        }

        // GET: Attendance/Create
        //Create a new attendance
        public ActionResult Create(string id)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (myStudent == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            //current date
            var myDate = UTCConversionsBackend.UtcToKioskTime(DateTimeHelper.Instance.GetDateTimeNowUTC()).Date;
            //the school day model
            var schoolDay = DataSourceBackend.Instance.SchoolCalendarBackend.ReadDate(myDate);
            DateTime defaultStart;
            DateTime defaultEnd;

            var myDefaultDismissalSettings = DataSourceBackend.Instance.SchoolDismissalSettingsBackend.GetDefault();

            if (schoolDay == null)
            {
                defaultStart = myDate.Add(myDefaultDismissalSettings.StartNormal);
                defaultEnd = myDate.Add(myDefaultDismissalSettings.EndNormal);
            }
            else
            {
                defaultStart = myDate.Add(schoolDay.TimeStart);
                defaultEnd = myDate.Add(schoolDay.TimeEnd);
            }

            var myData = new AttendanceModel
            {
                StudentId = id,
                In = defaultStart,
                Out = defaultEnd,
            };

            return View(myData);
        }

        // POST: Attendance/Create
        [HttpPost]
        public ActionResult Create([Bind(Include=
            "Id,"+
            "StudentId,"+
            "In,"+
            "Out,"+
            "Emotion,"+
            "IsNew,"+
            "")] AttendanceModel data)
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
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for edit
                return View(data);
            }

            //create a new attendance using the data
            var myAttendance = new AttendanceModel
            {
                StudentId = data.StudentId,
                //update the time
                In = UTCConversionsBackend.KioskTimeToUtc(data.In),
                Out = UTCConversionsBackend.KioskTimeToUtc(data.Out),
                Emotion = data.Emotion,
                EmotionUri = Emotion.GetEmotionURI(data.Emotion),

                IsNew = data.IsNew
            };

            //add the attendance to the student's attendance
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(myAttendance.StudentId);

            myStudent.Attendance.Add(myAttendance);
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            return RedirectToAction("Read", new { id = myAttendance.StudentId });
        }

        // GET: Attendance/Update
        // Update in and out times.
        public ActionResult Update(string id, string item)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(item))
            {
                return RedirectToAction("Error", "Home");
            }

            //get the attendance with given id
            var myAttendance = DataSourceBackend.Instance.StudentBackend.ReadAttendance(id, item);
            if (myAttendance == null)
            {
                return RedirectToAction("Error", "Home");
            }

            //Create a new attendance to hold converted times
            var myReturn = new AttendanceModel
            {
                StudentId = myAttendance.StudentId,
                Id = myAttendance.Id,
                In = UTCConversionsBackend.UtcToKioskTime(myAttendance.In),
                Out = UTCConversionsBackend.UtcToKioskTime(myAttendance.Out),
                Emotion = myAttendance.Emotion,
                EmotionUri = Emotion.GetEmotionURI(myAttendance.Emotion),

                IsNew = myAttendance.IsNew
            };

            return View(myReturn);
        }

        // POST: Attendance/Update/5
        [HttpPost]
        public ActionResult Update([Bind(Include=
            "Id,"+
            "StudentId,"+
            "In,"+
            "Out,"+
            "Emotion,"+
            "IsNew,"+
            "EmotionUri,"+
            "")] AttendanceModel data)
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
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            // The emotionURI is passed in as the AttendanceID because of conflicts with the model, it is then converted
            if (string.IsNullOrEmpty(data.EmotionUri))
            {
                // Send back for edit
                return View(data);
            }
            data.Id = data.EmotionUri;  //copy the ID back to Data.Id

            if (string.IsNullOrEmpty(data.StudentId))
            {
                return View(data);
            }

            //get the attendance with given id
            var myAttendance = DataSourceBackend.Instance.StudentBackend.ReadAttendance(data.StudentId, data.Id);

            if (myAttendance == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            //update the time
            myAttendance.In = UTCConversionsBackend.KioskTimeToUtc(data.In);
            myAttendance.Out = UTCConversionsBackend.KioskTimeToUtc(data.Out);

            //update the emotion
            myAttendance.Emotion = data.Emotion;
            myAttendance.EmotionUri = Emotion.GetEmotionURI(myAttendance.Emotion);
            DataSourceBackend.Instance.StudentBackend.UpdateAttendance(myAttendance);

            return RedirectToAction("Details", new { id=myAttendance.StudentId, item = myAttendance.Id });
        }

        // GET: Attendance/Delete/5
        // Remove the attendance
        public ActionResult Delete(string id, string item)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(item))
            {
                return RedirectToAction("Error", "Home");
            }

            //get the attendance with given id
            var myAttendance = DataSourceBackend.Instance.StudentBackend.ReadAttendance(id, item);
            if (myAttendance == null)
            {
                return RedirectToAction("Error", "Home");
            }

            //Create a new attendance to hold converted times
            var myReturn = new AttendanceModel
            {
                StudentId = myAttendance.StudentId,
                Id = myAttendance.Id,
                In = UTCConversionsBackend.UtcToKioskTime(myAttendance.In),
                Out = UTCConversionsBackend.UtcToKioskTime(myAttendance.Out),
                Emotion = myAttendance.Emotion,
                EmotionUri = Emotion.GetEmotionURI(myAttendance.Emotion),

                IsNew = myAttendance.IsNew
            };

            return View(myReturn);
        }

        // POST: Attendance/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include =
            "StudentId,"+
            "Id," +
            "EmotionUri,"+
            "")] AttendanceModel data)
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
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            // The emotionURI is passed in as the AttendanceID because of conflicts with the model, it is then converted
            if (string.IsNullOrEmpty(data.EmotionUri))
            {
                // Send back for edit
                return View(data);
            }

            data.Id = data.EmotionUri;  //copy the ID back to Data.Id
            if (string.IsNullOrEmpty(data.StudentId))
            {
                // Send back for edit
                return View(data);
            }

            //get the attendance with given id
            var myAttendance = DataSourceBackend.Instance.StudentBackend.ReadAttendance(data.StudentId, data.Id);
            if (myAttendance == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            //get the student, then remove the attendance from his attendance list
            var myStudent = DataSourceBackend.Instance.StudentBackend.Read(myAttendance.StudentId);

            myStudent.Attendance.Remove(myAttendance);
            DataSourceBackend.Instance.StudentBackend.Update(myStudent);

            return RedirectToAction("Read", new { id = myAttendance.StudentId });
        }
    }
}
