using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class AttendanceModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_AttendanceModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new AttendanceModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_AttendanceModel_Default_Instantiate_With_Null_Should_Fail()
        {
            // Arange
            var myData = new AttendanceModel();

            // Act
            var result = myData.Update(null);

            // Assert
            Assert.AreEqual(result, null, TestContext.TestName);
        }

        [TestMethod]
        public void Models_AttendanceModel_Default_Instantiate_Get_Set_Should_Pass()
        {
            //arrange
            var result = new AttendanceModel();
            var expectStudentId = "GoodID1";
            var expectIn = DateTime.UtcNow;
            var expectOut = DateTime.UtcNow;
            //var expectStatus = _5051.Models.StudentStatusEnum.In;
            //var expectDuration = TimeSpan.Zero;
            var expectedIsNew = false;
            var expectEmotion = _5051.Models.EmotionStatusEnum.Neutral;
            var expectEmotionUri = Emotion.GetEmotionURI(expectEmotion);
            
            // Act
            result.StudentId = expectStudentId;
            result.In = expectIn;
            result.Out = expectOut;
            //result.Status = expectStatus;
            result.IsNew = expectedIsNew;
            result.Emotion = expectEmotion;
            result.EmotionUri = expectEmotionUri;

            // Assert
            Assert.IsNotNull(result.Id, TestContext.TestName);
            Assert.AreEqual(expectStudentId, result.StudentId, TestContext.TestName);
            Assert.AreEqual(expectIn, result.In, TestContext.TestName);
            Assert.AreEqual(expectOut, result.Out, TestContext.TestName);
            //Assert.AreEqual(expectStatus, result.Status, TestContext.TestName);
            Assert.AreEqual(expectEmotion, result.Emotion, TestContext.TestName);
            Assert.AreEqual(expectEmotionUri, result.EmotionUri, TestContext.TestName);

            Assert.AreEqual(expectedIsNew, result.IsNew, TestContext.TestName);

        }
        #endregion Instantiate
    }
}
