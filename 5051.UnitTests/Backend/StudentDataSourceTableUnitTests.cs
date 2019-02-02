using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class StudentDataSourceTableUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize()]
        public void Initialize()
        {
            DataSourceBackend.SetTestingMode(true);
            DataSourceBackend.Instance.SetDataSource(DataSourceEnum.Local);
        }

        [TestCleanup]
        public void Cleanup()
        {
            DataSourceBackend.SetTestingMode(false);
            DataSourceBackend.Instance.SetDataSource(DataSourceEnum.Mock);
        }

        #region Instantiate
        [TestMethod]
        public void Backend_StudentDataSourceTable_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = StudentDataSourceTable.Instance;

            //var expect = backend;

            // Act
            var result = backend;

            //Reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion Instantiate

        #region read
        [TestMethod]
        public void Backend_StudentDataSourceTable_Read_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = StudentDataSourceTable.Instance;

            // Act
            var result = backend.Read(null);

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentDataSourceTable_Read_Valid_ID_Should_Pass()
        {
            // Arrange
            var backend = StudentDataSourceTable.Instance;
            var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();

            // Act
            var result = backend.Read(expectStudent.Id);

            // Assert
            Assert.AreEqual(expectStudent, result, TestContext.TestName);
        }
        #endregion read

        #region update
        [TestMethod]
        public void Backend_StudentDataSourceTable_Update_Invalid_Data_Null_Should_Fail()
        {
            // Arrange
            var backend = StudentDataSourceTable.Instance;

            // Act
            var result = backend.Update(null);

            //reset
            backend.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentDataSourceTable_Update_Invalid_Data_Student_Does_Not_Exist_Should_Fail()
        {
            // Arrange
            var backend = StudentDataSourceTable.Instance;
            var expectStudent = new StudentModel();

            // Act
            var result = backend.Update(expectStudent);

            //reset
            backend.Reset();

            // Assert
            Assert.IsNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentDataSourceTable_Update_Valid_Data_Should_Pass()
        {
            // Arrange
            var backend = StudentDataSourceTable.Instance;
            var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expectId = "GoodID";
            var expectName = "Billy";
            var expectAvatarLevel = 7;
            var expectExperiencePoints = 100;
            var expectTokens = 100;
            var expectStatus = _5051.Models.StudentStatusEnum.Out;
            var expectPassword = "passWORD23!";
            var expectEmotionCurrent = _5051.Models.EmotionStatusEnum.Happy;
            var expectAvatarComposite = new AvatarCompositeModel();

            // Act
            expectStudent.Id = expectId;
            expectStudent.Name = expectName;
            expectStudent.AvatarLevel = expectAvatarLevel;
            expectStudent.ExperiencePoints = expectExperiencePoints;
            expectStudent.Tokens = expectTokens;
            expectStudent.Status = expectStatus;
            expectStudent.Password = expectPassword;
            expectStudent.EmotionCurrent = expectEmotionCurrent;
            expectStudent.AvatarComposite = expectAvatarComposite;

            var resultUpdate = backend.Update(expectStudent);

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expectStudent, resultUpdate, TestContext.TestName);
            Assert.AreEqual(expectId, resultUpdate.Id, TestContext.TestName);
            Assert.AreEqual(expectName, resultUpdate.Name, TestContext.TestName);
            Assert.AreEqual(expectAvatarLevel, resultUpdate.AvatarLevel, TestContext.TestName);
            Assert.AreEqual(expectExperiencePoints, resultUpdate.ExperiencePoints, TestContext.TestName);
            Assert.AreEqual(expectTokens, resultUpdate.Tokens, TestContext.TestName);
            Assert.AreEqual(expectStatus, resultUpdate.Status, TestContext.TestName);
            Assert.AreEqual(expectPassword, resultUpdate.Password, TestContext.TestName);
            Assert.AreEqual(expectEmotionCurrent, resultUpdate.EmotionCurrent, TestContext.TestName);
            Assert.AreEqual(expectAvatarComposite.AvatarAccessoryUri, resultUpdate.AvatarComposite.AvatarAccessoryUri, TestContext.TestName);

        }
        #endregion update

        #region delete
        [TestMethod]
        public void Backend_StudentDataSourceTable_Delete_Invalid_ID_Null_Should_Fail()
        {
            // Arrange
            var backend = StudentDataSourceTable.Instance;
            var expect = false;

            // Act
            var result = backend.Delete(null);

            //reset
            backend.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentDataSourceTable_Delete_Valid_ID_Should_Pass()
        {
            // Arrange
            var backend = StudentDataSourceTable.Instance;
            var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expect = true;

            // Act
            var result = backend.Delete(expectStudent.Id);

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }


        [TestMethod]
        public void Backend_StudentDataSourceTable_Delete_Invalid_ID_Bogus_Should_Fail()
        {
            // Arrange
            var backend = StudentDataSourceTable.Instance;
            var expectStudent = DataSourceBackend.Instance.StudentBackend.GetDefault();
            var expect = false;

            // Act
            var result = backend.Delete("bogus");

            //reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion delete

        #region DataSet
        [TestMethod]
        public void Backend_StudentDataSourceTable_LoadDataSet_Valid_Enum_Demo_Should_Pass()
        {
            // Arrange
            var backend = StudentDataSourceTable.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.Demo;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_StudentDataSourceTable_LoadDataSet_Valid_Enum_UnitTest_Should_Pass()
        {
            // Arrange
            var backend = StudentDataSourceTable.Instance;
            var expectEnum = _5051.Models.DataSourceDataSetEnum.UnitTest;

            // Act
            backend.LoadDataSet(expectEnum);
            var result = backend;

            //reset
            backend.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion DataSet
    }
}
