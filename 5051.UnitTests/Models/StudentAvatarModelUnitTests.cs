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
    public class StudentAvatarModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_StudentAvatarModel_Default_Instantiate_Get_Set_Should_Pass()
        {
            //arrange
            var result = new StudentAvatarModel();
            var expectStudentId = "GoodStudentId1";

            // Act
            result.StudentId = expectStudentId;

            // Assert
            Assert.AreEqual(expectStudentId, result.StudentId, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
