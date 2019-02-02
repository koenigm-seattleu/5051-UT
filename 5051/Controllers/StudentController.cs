using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    public class StudentController : BaseController
    {
        // GET: Student
        /// <summary>
        /// Index, the page that shows all the Students
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Read information on a single Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Student/Details/5
        public ActionResult Read(string id = null)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            var myDataStudent = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (myDataStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myData = new StudentDisplayViewModel(myDataStudent);
            // null not possible
            //if (myData == null)
            //{
            //    return RedirectToAction("Error", "Home");
            //}

            return View(myData);
        }

        /// <summary>
        /// This opens up the make a new Student screen
        /// </summary>
        /// <returns></returns>
        // GET: Student/Create
        public ActionResult Create()
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            var myData = new StudentModel();
            return View(myData);
        }

        /// <summary>
        /// Make a new Student sent in by the create Student screen
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        // POST: Student/Create
        [HttpPost]
        public ActionResult Create([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Status,"+
                                        "Tokens,"+
                                        "ExperiencePoints,"+
                                        "")] StudentModel data)
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

            if (string.IsNullOrEmpty(data.Name))
            {
                ModelState.AddModelError("Name", "Please Enter a Name.");
                // Return back for Edit
                return View(data);
            }

            DataSourceBackend.Instance.StudentBackend.Create(data);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This will show the details of the Student to update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Student/Edit/5
        public ActionResult Update(string id = null)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            var myDataStudent = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (myDataStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myData = new StudentDisplayViewModel(myDataStudent);

            return View(myData);
        }

        /// <summary>
        /// This updates the Student based on the information posted from the udpate page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Student/Update/5
        [HttpPost]
        public ActionResult Update([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Tokens,"+
                                        "Status,"+
                                        "ExperiencePoints,"+
                                        "Truck,"+
                                        "")] StudentDisplayViewModel data)
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

            var myDataStudent = DataSourceBackend.Instance.StudentBackend.Read(data.Id);

            myDataStudent.EmotionCurrent = data.EmotionCurrent;
            //myDataStudent.Name = data.Name;
            myDataStudent.Tokens = data.Tokens;
            myDataStudent.Status = data.Status;
            myDataStudent.ExperiencePoints = data.ExperiencePoints;
            myDataStudent.Truck.TruckName = data.Truck.TruckName;

            DataSourceBackend.Instance.StudentBackend.Update(myDataStudent);

            if(myDataStudent.Name != data.Name)
            {
                DataSourceBackend.Instance.IdentityBackend.ChangeUserName(data.Id, data.Name);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This shows the Student info to be deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Student/Delete/5
        public ActionResult Delete(string id = null)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            var myDataStudent = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (myDataStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myData = new StudentDisplayViewModel(myDataStudent);
            // null not possible
            //if (myData == null)
            //{
            //    RedirectToAction("Error", "Home");
            //}

            return View(myData);
        }

        /// <summary>
        /// This deletes the Student sent up as a post from the Student delete page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "Status,"+
                                        "Tokens,"+
                                        "ExperiencePoints,"+
                                        "AvatarLevel,"+
                                        "")] StudentDisplayViewModel data)
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
                // Send to Error page
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for Edit
                return View(data);
            }

            DataSourceBackend.Instance.StudentBackend.Delete(data.Id);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// shows the student whose password is to be reset
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ResetPassword(string id = null)
        {
            var CurrentId = DataSourceBackend.Instance.IdentityBackend.GetCurrentStudentID(HttpContext);

            if (DataSourceBackend.Instance.IdentityBackend.BlockExecptForRole(CurrentId, UserRoleEnum.TeacherUser))
            {
                return RedirectToAction("Login", "Admin");
            }

            var myDataStudent = DataSourceBackend.Instance.StudentBackend.Read(id);
            if (myDataStudent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var myData = new StudentDisplayViewModel(myDataStudent);
            // null not possible
            //if (myData == null)
            //{
            //    RedirectToAction("Error", "Home");
            //}

            return View(myData);
        }

        /// <summary>
        /// This resets the students password to their name
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult ResetPassword([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Description,"+
                                        "Uri,"+
                                        "Status,"+
                                        "Tokens,"+
                                        "ExperiencePoints,"+
                                        "AvatarLevel,"+
                                        "")] StudentDisplayViewModel data)
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
                // Send to Error page
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for Edit
                return View(data);
            }

            var findResult = DataSourceBackend.Instance.IdentityBackend.FindUserByID(data.Id);
            var student = DataSourceBackend.Instance.StudentBackend.Read(data.Id);

            var changeResult = DataSourceBackend.Instance.IdentityBackend.ChangeUserPassword(student.Name, "abc", student.Password, _5051.Models.UserRoleEnum.StudentUser);
            if (!changeResult)
            {
                ModelState.AddModelError("", "Password Reset resulted in error.");
                return View(data);
            }


            return RedirectToAction("Index");
        }
    }
}
