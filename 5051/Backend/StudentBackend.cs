using _5051.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Backend
{
    /// <summary>
    /// Student Backend handles the business logic and data for Students
    /// </summary>
    public class StudentBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile StudentBackend instance;
        private static readonly object syncRoot = new Object();

        private StudentBackend() { }

        public static StudentBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StudentBackend();
                            SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
                        }
                    }
                }

                return instance;
            }
        }

        // Get the Datasource to use
        private static IStudentInterface DataSource;

        /// <summary>
        /// Switches between Live, and Mock Datasets
        /// </summary>
        /// <param name="dataSourceEnum"></param>
        public static void SetDataSource(DataSourceEnum dataSourceEnum)
        {
            switch (dataSourceEnum)
            {
                case DataSourceEnum.SQL:
                    break;

                case DataSourceEnum.Local:
                case DataSourceEnum.ServerLive:
                case DataSourceEnum.ServerTest:
                    DataSourceBackendTable.Instance.SetDataSourceServerMode(dataSourceEnum);
                    DataSource = StudentDataSourceTable.Instance;
                    break;

                case DataSourceEnum.Mock:
                default:
                    // Default is to use the Mock
                    DataSource = StudentDataSourceMock.Instance;
                    break;
            }
        }

        /// <summary>
        /// Switch the data set between Demo, Default and Unit Test
        /// </summary>
        /// <param name="SetEnum"></param>
        public static void SetDataSourceDataSet(DataSourceDataSetEnum SetEnum)
        {
            DataSource.LoadDataSet(SetEnum);
        }

        /// <summary>
        /// Makes a new Student
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Student Passed In</returns>
        public StudentModel Create(StudentModel data)
        {
            DataSource.Create(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public StudentModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = DataSource.Read(id);

            if (myReturn == null)
            {
                return null;
            }

            myReturn.EmotionCurrent = EmotionStatusEnum.Neutral;

            //Todo: refactor this to a different location
            if (myReturn.Attendance.Any())
            {
                // Set the current emotion, to the last check in status emotion
                var temp = myReturn.Attendance.LastOrDefault(/*m => m.Status == StudentStatusEnum.In*/);

                if (temp != null)
                {
                    myReturn.EmotionCurrent = temp.Emotion;
                }
            }

            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public StudentModel Update(StudentModel data)
        {
            if (data == null)
            {
                return null;
            }

            var myData = DataSource.Read(data.Id);
            if (myData == null)
            {
                // Not found
                return null;
            }

            if (myData.Status != data.Status)
            {
                // Status Changed, need to process the status change
                ToggleStatus(myData);
            }

            // Update the record
            var myReturn = DataSource.Update(data);

            return myReturn;
        }

        /// <summary>
        /// Remove the Data item if it is in the list
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True for success, else false</returns>
        public bool Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myReturn = DataSource.Delete(Id);
            //delete the identity side as well
            var idDeleteResult = DataSourceBackend.Instance.IdentityBackend.DeleteUserIdRecordOnly(Id);

            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Students</returns>
        public List<StudentModel> Index()
        {
            var myData = DataSource.Index();
            return myData;
        }

        /// <summary>
        /// Sets the student to be logged In
        /// </summary>
        /// <param name="id">The Student ID</param>
        public void SetLogIn(StudentModel data)
        {
            if (data == null)
            {
                return;
            }

            data.Status = StudentStatusEnum.In;

            var currentTime = DateTimeHelper.Instance.GetDateTimeNowUTC();

            // Update the Attendance Log
            var temp = new AttendanceModel
            {
                In = currentTime,
                Emotion = data.EmotionCurrent,
                StudentId = data.Id
            };

            data.Attendance.Add(temp);

            Update(data);
        }

        /// <summary>
        /// Sets the student to be logged Out
        /// </summary>
        /// <param name="id">The Student ID</param>
        public void SetLogOut(StudentModel data)
        {
            if (data == null)
            {
                return;
            }

            data.Status = StudentStatusEnum.Out;

            var myTimeData = data.Attendance.OrderByDescending(m => m.In).FirstOrDefault();

            if (myTimeData == null)
            {
                return;
            }

            myTimeData.Out = DateTimeHelper.Instance.GetDateTimeNowUTC();

            //save the change
            Update(data);
        }

        /// <summary>
        /// Use the ID to toggle the emotion and status 
        /// </summary>
        /// <param name="id">Id of the student</param>
        public void ToggleEmotionStatusById(string id, EmotionStatusEnum emotion)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            var myData = DataSource.Read(id);
            if (myData == null)
            {
                return;
            }

            myData.EmotionCurrent = emotion;
            ToggleStatus(myData);
        }

        //// </summary> Uses an attendance id to fetch attendance data and then student data to create a viewmodel </summary>
        //public AttendanceDetailsViewModel GetAttendanceDetailsViewModel(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return null;
        //    }

        //    var ret = new AttendanceDetailsViewModel(id);

        //    return ret;
        //}

        /// <summary>
        /// Use the ID to toggle the status
        /// </summary>
        /// <param name="id">Id of the student</param>
        public void ToggleStatusById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            var myData = DataSource.Read(id);
            if (myData == null)
            {
                return;
            }

            ToggleStatus(myData);
        }

        /// <summary>
        /// Change the Status of the student
        /// </summary>
        /// <param name="id">The Student ID</param>
        public void ToggleStatus(StudentModel data)
        {
            if (data == null)
            {
                return;
            }

            switch (data.Status)
            {
                case StudentStatusEnum.In:
                    SetLogOut(data);
                    break;

                case StudentStatusEnum.Out:
                    SetLogIn(data);
                    break;

                case StudentStatusEnum.Hold:
                    SetLogOut(data);
                    break;

            }

            Update(data);
        }

        /// <summary>
        /// Get the attendance model with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AttendanceModel ReadAttendance(string StudentId, string AttendanceId)
        {
            if (string.IsNullOrEmpty(StudentId))
            {
                return null;
            }

            if (string.IsNullOrEmpty(AttendanceId))
            {
                return null;
            }

            var student = Read(StudentId);
            if (student == null)
            {
                return null;
            }

            var record = student.Attendance.Find(m => m.Id == AttendanceId);
            if (record == null)
            {
                return null;
            }

            return record;
        }

        /// <summary>
        /// Get the attendance model with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AttendanceModel UpdateAttendance(AttendanceModel data)
        {
            if (data == null)
            {
                return null;
            }

            var student = Read(data.StudentId);
            if (student == null)
            {
                return null;
            }

            var record = student.Attendance.Find(m => m.Id == data.Id);
            if (record == null)
            {
                return null;
            }

            // Update the attendance record directly
            record.Update(data);

            // Update the Student in the DB of which the attendance record is part of
            Update(student);
                
            return data;
        }

        /// <summary>
        /// Reset all students' status to "out", then for each new attendance of each student,
        /// set auto check-out time, then compute tokens
        /// </summary>
        public void ResetStatusAndProcessNewAttendance()
        {

            foreach (var item in Index())  //for each student
            {
                //Reset Status to "Out"
                item.Status = StudentStatusEnum.Out;

                //get the list of new attendances of the student, for which token amount has not been added yet,
                //and auto check-out time has not been set yet
                var newLogins = item.Attendance.Where(m => m.IsNew);

                //for each new attendance, set auto check-out time, then calculate effective duration and according collected tokens,
                //add to current tokens of the student.
                foreach (var attendance in newLogins)
                {
                    item.Tokens += CalculateTokens(attendance);

                    //mark it as old attendance
                    attendance.IsNew = false;
                }

                // Save upated student
                Update(item);
            }
        }

        public int CalculateTokens(AttendanceModel attendance)
        {
            var collectedTokens = 0;

            return collectedTokens;
        }

        ///// <summary>
        ///// Update token
        ///// </summary>
        ///// <param name="id"></param>
        //public void UpdateToken(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return;
        //    }

        //    var data = DataSource.Read(id);
        //    if (data == null)
        //    {
        //        return;
        //    }

        //    //get the list of new attendances, for which token amount has not been added yet.
        //    var newLogIns = data.Attendance.Where(m => m.IsNew);

        //    //for each new attendance, calculate effective duration and according collected tokens,
        //    //add to current tokens of the student.
        //    foreach (var attendance in newLogIns)
        //    {
        //        var effectiveDuration = CalculateEffectiveDuration(attendance);

        //        //todo: since hours attended is rounded up, need to prevent the case where consecutive check-ins in a short period
        //        //todo: of time could add 1 tokens everytime
        //        var collectedTokens = (int)Math.Ceiling(effectiveDuration.TotalHours);
        //        data.Tokens += collectedTokens;

        //        //mark it as old attendance
        //        attendance.IsNew = false;
        //    }

        //}

        /// <summary>
        /// Helper function that resets the DataSource, and rereads it.
        /// </summary>
        public void Reset()
        {
            DataSource.Reset();
        }

        /// <summary>
        /// Returns the First Student in the system
        /// </summary>
        /// <returns>Null or valid data</returns>
        public StudentModel GetDefault()
        {
            var myReturn = Index().FirstOrDefault();
            return myReturn;
        }

        public bool BackupData(DataSourceEnum dataSourceSource, DataSourceEnum dataSourceDestination)
        {
            var result = DataSource.BackupData(dataSourceSource, dataSourceDestination);
            return result;
        }
    }
}