using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Controllers;
using _5051.Models;
using System.Diagnostics;
using _5051.Backend;
using System.Web;
using System.Web.Routing;
using Moq;

namespace _5051.Tests.Controllers
{
    [TestClass]
    public class PortalControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region RosterRegion
        [TestMethod]
        public void Controller_Portal_Roster_Should_Return_NewModel()
        {
            // Arrange
            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            PortalController controller = new PortalController();

            var context = CreateMoqSetupForCookie(Student.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Roster() as ViewResult;

            var resultStudentViewModel = result.Model as StudentViewModel;

            // Assert
            Assert.IsNotNull(resultStudentViewModel, TestContext.TestName);
        }
        #endregion

        #region IndexStringRegion

        [TestMethod]
        public void Controller_Portal_Index_IDValid_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = new StudentModel();
            string id = Backend.DataSourceBackend.Instance.StudentBackend.Create(data).Id;

            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index(id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Index_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_VeryHappy_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.VeryHappy
            };
            data.Attendance.Add(myAttendance);

            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Index_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_Happy_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.Happy
            };
            data.Attendance.Add(myAttendance);

            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Index_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_Neutral_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.Neutral
            };
            data.Attendance.Add(myAttendance);

            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Index_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_Sad_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.Sad
            };
            data.Attendance.Add(myAttendance);

            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Index_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_VerySad_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.VerySad
            };
            data.Attendance.Add(myAttendance);

            var temp = DataSourceBackend.Instance;
            var Student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Index(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }
        #endregion

        #region AttendanceStringRegion
        [TestMethod]
        public void Controller_Portal_Attendance_IDIsNull_Should_Return_RosterPage()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Attendance(id);

            // Assert
            Assert.AreEqual("Roster", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Attendance_IDValid_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = new StudentModel();
            string id = Backend.DataSourceBackend.Instance.StudentBackend.Create(data).Id;

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Attendance(id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Attendance_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_VeryHappy_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.VeryHappy
            };
            data.Attendance.Add(myAttendance);

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Attendance(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Attendance_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_Happy_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.Happy
            };
            data.Attendance.Add(myAttendance);

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Attendance(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Attendance_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_Neutral_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.Neutral
            };
            data.Attendance.Add(myAttendance);

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Attendance(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Attendance_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_Sad_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.Sad
            };
            data.Attendance.Add(myAttendance);

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Attendance(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Attendance_ID_Valid_Attendance_Not_Null_Or_Empty_Emotion_Is_VerySad_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = DataSourceBackend.Instance.StudentBackend.GetDefault();
            data.Attendance = new List<AttendanceModel>();
            var myAttendance = new AttendanceModel()
            {
                StudentId = data.Id,
                In = DateTime.UtcNow,
                Emotion = EmotionStatusEnum.VerySad
            };
            data.Attendance.Add(myAttendance);

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(data.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Attendance(data.Id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }
        #endregion

        #region AttendanceUpdateRegion

        [TestMethod]
        public void Controller_Portal_AttendanceUpdate_Get_Id_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie("test");

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.AttendanceUpdate(null, null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_AttendanceUpdate_Get_myData_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie("bogus");

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.AttendanceUpdate("bogus", "bogus");

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
            Assert.AreEqual("Home", result.RouteValues["controller"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_AttendanceUpdate_Get_Default_Should_Pass()
        {
            // Arrange
            var controller = new PortalController();

            var myStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var myAttendance = new AttendanceModel();
            var myId = myAttendance.Id;
            myStudent.Attendance.Add(myAttendance);

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(myStudent.Id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.AttendanceUpdate(myStudent.Id, myId) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion AttendanceUpdateRegion

        #region AttendanceUpdatePostRegion

        [TestMethod]
        public void Controller_Portal_AttendanceUpdate_Post_Model_Is_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            PortalController controller = new PortalController();

            AttendanceModel data = new AttendanceModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.AttendanceUpdate(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_AttendanceUpdate_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();

            // Act
            var result = (RedirectToRouteResult)controller.AttendanceUpdate((AttendanceModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_AttendanceUpdate_Post_Id_Is_Null_Or_Empty_Should_Send_Back_For_Edit()
        {
            // Arrange
            PortalController controller = new PortalController();

            AttendanceModel dataNull = new AttendanceModel();
            AttendanceModel dataEmpty = new AttendanceModel();

            // Make data.Id = null
            dataNull.Id = null;

            // Make data.Id empty
            dataEmpty.Id = "";

            // Act
            var resultNull = (ViewResult)controller.AttendanceUpdate(dataNull);
            var resultEmpty = (ViewResult)controller.AttendanceUpdate(dataEmpty);

            // Assert
            Assert.IsNotNull(resultNull, TestContext.TestName);
            Assert.IsNotNull(resultEmpty, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_AttendanceUpdate_Post_Id_Is_Invalid_Should_Return_Error_Page()
        {
            var controller = new PortalController();

            var myStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var myAttendance = new AttendanceModel();
            myStudent.Attendance.Add(myAttendance);

            var myData = new AttendanceModel
            {
                StudentId = myAttendance.Id,
                Id = "bogus"
            };
            myData.EmotionUri = myData.Id;

            // Act
            var result = (RedirectToRouteResult)controller.AttendanceUpdate(myData);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_AttendanceUpdate_Post_Default_Should_Return_Attendance_Page()
        {
            // Arrange
            var controller = new PortalController();

            var myStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var myAttendance = new AttendanceModel
            {
                StudentId = myStudent.Id
            };
            myAttendance.EmotionUri = myAttendance.Id;

            myStudent.Attendance.Add(myAttendance);

            // Act
            RedirectToRouteResult result = controller.AttendanceUpdate(myAttendance) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Attendance", result.RouteValues["action"], TestContext.TestName);
        }

        #endregion AttendanceUpdatePostRegion

        #region SettingsStringRegion
        [TestMethod]
        public void Controller_Portal_Settings_IDIsNull_Should_Return_RosterPage()
        {
            // Arrange           
            PortalController controller = new PortalController();
            string id = null;

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.Settings(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }
        
        [TestMethod]
        public void Controller_Portal_Settings_IDValid_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            StudentModel data = new StudentModel();
            string id = Backend.DataSourceBackend.Instance.StudentBackend.Create(data).Id;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Settings(id) as ViewResult;

            var resultStudentDisplayViewModel = result.Model as StudentDisplayViewModel;

            // Reset StudentBackend
            DataSourceBackend.Instance.StudentBackend.Reset();

            // Assert
            Assert.IsNotNull(resultStudentDisplayViewModel, TestContext.TestName);
        }
        #endregion

        #region SettingsPostRegion
        [TestMethod]
        public void Controller_Portal_Settings_Post_ModelIsInvalid_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();

            StudentDisplayViewModel data = new StudentDisplayViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.Settings(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Settings_Post_Invalid_Id_Null_Should_Fail()
        {
            // Arrange
            PortalController controller = new PortalController();

            StudentDisplayViewModel data = new StudentDisplayViewModel
            {
                Id = null
            };

            // Act
            var result = (RedirectToRouteResult)controller.Settings(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Settings_Post_Invalid_Id_Bogus_Should_Fail()
        {
            // Arrange
            PortalController controller = new PortalController();

            StudentDisplayViewModel data = new StudentDisplayViewModel
            {
                Id = "bogus"
            };

            // Act
            var result = (RedirectToRouteResult)controller.Settings(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_Settings_Post_Valid_Should_Fail()
        {
            // Arrange
            PortalController controller = new PortalController();

            string expect = "hi";

            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();
            StudentDisplayViewModel data = new StudentDisplayViewModel(student)
            {
                Name = expect
            };

            // Act
            controller.Settings(data);
            var result = DataSourceBackend.Instance.StudentBackend.Read(data.Id);

            // Assert
            Assert.AreEqual(expect, result.Name, TestContext.TestName);
        }
        #endregion

        #region ReportRegion


        [TestMethod]
        public void Controller_Admin_Weekly_Report_DeFault_Should_Return_ErrorPage()
        {
            // Arrange
            PortalController controller = new PortalController();

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie("test");

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.WeeklyReport();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Weekly_Report_Valid_Id_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.WeeklyReport(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Weekly_Report_Incorrect_Id_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = "bogus";

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.WeeklyReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Weekly_Report_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();
            WeeklyReportViewModel data = null;

            // Act
            var result = (RedirectToRouteResult)controller.WeeklyReport(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Post_Selected_ID_Is_2_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            var data = new WeeklyReportViewModel()
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                SelectedWeekId = 2
            };

            // Act
            var result = controller.WeeklyReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_DeFault_Should_Return_ErrorPage()
        {
            // Arrange
            PortalController controller = new PortalController();

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie();

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.WeeklyReport();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_Valid_Id_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.MonthlyReport(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_Incorrect_Id_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = "bogus";

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.MonthlyReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();
            MonthlyReportViewModel data = null;

            // Act
            var result = (RedirectToRouteResult)controller.MonthlyReport(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Monthly_Report_Post_Data_Is_Valid_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            MonthlyReportViewModel data = new MonthlyReportViewModel()
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                SelectedMonthId = 2
            };

            // Act
            var result = controller.MonthlyReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }



        [TestMethod]
        public void Controller_Admin_Semester_Report_DeFault_Should_Return_ErrorPage()
        {
            // Arrange
            PortalController controller = new PortalController();

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie("test");

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SemesterReport();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Valid_Id_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.SemesterReport(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Incorrect_Id_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = "bogus";

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.SemesterReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();
            SemesterReportViewModel data = null;

            // Act
            var result = (RedirectToRouteResult)controller.SemesterReport(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Semester_Report_Post_Data_Is_Valid_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            var data = new SemesterReportViewModel()
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                SelectedSemesterId = 2
            };

            // Act
            var result = controller.SemesterReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_DeFault_Should_Return_ErrorPage()
        {
            // Arrange
            PortalController controller = new PortalController();

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie("test");

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.QuarterReport();

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Valid_Id_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.QuarterReport(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Incorrect_Id_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = "bogus";

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.QuarterReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();
            QuarterReportViewModel data = null;

            // Act
            var result = (RedirectToRouteResult)controller.QuarterReport(data);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Post_Selected_ID_Is_2_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            var data = new QuarterReportViewModel()
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                SelectedQuarterId = 2
            };

            // Act
            var result = controller.QuarterReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Post_Selected_ID_Is_3_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            var data = new QuarterReportViewModel()
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                SelectedQuarterId = 3
            };

            // Act
            var result = controller.QuarterReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Quarter_Report_Post_Selected_ID_Is_4_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            var data = new QuarterReportViewModel()
            {
                StudentId = DataSourceBackend.Instance.StudentBackend.GetDefault().Id,
                SelectedQuarterId = 4
            };

            // Act
            var result = controller.QuarterReport(data);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Overall_Report_Id_Is_Valid_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.OverallReport(id);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Admin_Overall_ReportId_Is_Not_Valid_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = "abc";

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.OverallReport(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }
        #endregion

        #region ChangePasswordRegion
        [TestMethod]
        public void Controller_Portal_ChangePassword_Get_Id_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();
            string id = null;
            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.ChangePassword((string)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_ChangePassword_Get_findResult_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();

            string id = "bogus";

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.ChangePassword(id);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_ChangePassword_Get_Default_Should_Pass()
        {
            // Arrange
            PortalController controller = new PortalController();
            var id = DataSourceBackend.Instance.StudentBackend.GetDefault().Id;

            var backend = DataSourceBackend.Instance;

            var context = CreateMoqSetupForCookie(id);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.ChangePassword(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion UpdateRegion

        #region ChangePassWordPostRegion

        [TestMethod]
        public void Controller_Portal_ChangePassword_Post_Model_Is_Invalid_Should_Send_Back_For_Edit()
        {
            // Arrange
            PortalController controller = new PortalController();

            ChangePasswordViewModel data = new ChangePasswordViewModel();

            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");

            // Act
            ViewResult result = controller.ChangePassword(data) as ViewResult;

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_ChangePassword_Post_Data_Is_Null_Should_Return_Error_Page()
        {
            // Arrange
            PortalController controller = new PortalController();

            // Act
            var result = (RedirectToRouteResult)controller.ChangePassword((ChangePasswordViewModel)null);

            // Assert
            Assert.AreEqual("Error", result.RouteValues["action"], TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_ChangePassword_Post_Id_Is_Null_Or_Empty_Should_Send_Back_For_Edit()
        {
            // Arrange
            PortalController controller = new PortalController();

            ChangePasswordViewModel dataNull = new ChangePasswordViewModel();
            ChangePasswordViewModel dataEmpty = new ChangePasswordViewModel();

            // Make data.Id = null
            dataNull.UserID = null;

            // Make data.Id empty
            dataEmpty.UserID = "";

            // Act
            var resultNull = (ViewResult)controller.ChangePassword(dataNull);
            var resultEmpty = (ViewResult)controller.ChangePassword(dataEmpty);

            // Assert
            Assert.IsNotNull(resultNull, TestContext.TestName);
            Assert.IsNotNull(resultEmpty, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_ChangePassword_Post_OldPassword_Is_Wrong_Should_Send_Back_For_Edit()
        {
            // Arrange
            PortalController controller = new PortalController();

            var backend = DataSourceBackend.Instance;

            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            ChangePasswordViewModel model = new ChangePasswordViewModel
            {
                UserID = student.Id,
                ConfirmPassword = "test",
                OldPassword = "bogus",
                NewPassword = "test"
            };

            // Act
            ViewResult result = (ViewResult)controller.ChangePassword(model);

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Controller_Portal_ChangePassword_Post_Default_Should_Return_Settings_Page()
        {
            // Arrange
            PortalController controller = new PortalController();

            var backend = DataSourceBackend.Instance;

            var student = DataSourceBackend.Instance.StudentBackend.GetDefault();

            ChangePasswordViewModel model = new ChangePasswordViewModel
            {
                UserID = student.Id,
                ConfirmPassword = student.Password,
                OldPassword = student.Password,
                NewPassword = student.Password
            };

            // Act
            RedirectToRouteResult result = controller.ChangePassword(model) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Settings", result.RouteValues["action"], TestContext.TestName);
        }
        #endregion UpdatePostRegion

        /// <summary>
        /// sets up a moq for http context so that a code dealing with cookeis can be tested
        /// returns a moqed context object
        /// pass in a cookieValue if you are trying to read a cookie, otherwise leave blank
        /// </summary>
        public HttpContextBase CreateMoqSetupForCookie(string cookieValue = null)
        {
            var testCookieName = "id";
            var testCookieValue = cookieValue;
            HttpCookie testCookie = new HttpCookie(testCookieName);

            if (!string.IsNullOrEmpty(cookieValue))
            {
                testCookie.Value = testCookieValue;
                testCookie.Expires = DateTime.Now.AddSeconds(30);
            }

            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);

            var mockedRequest = Mock.Get(context.Object.Request);
            mockedRequest.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());

            var mockedResponse = Mock.Get(context.Object.Response);
            mockedResponse.Setup(r => r.Cookies).Returns(new HttpCookieCollection());

            if (!string.IsNullOrEmpty(cookieValue))
            {
                var mockedServer = Mock.Get(context.Object.Server);
                mockedServer.Setup(x => x.HtmlEncode(cookieValue)).Returns(cookieValue);

                context.Object.Request.Cookies.Add(testCookie);
            }

            return context.Object;
        }
    }
}
