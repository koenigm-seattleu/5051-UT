using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class StudentViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Models_StudentViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new StudentViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentViewModel_Default_Instantiate_With_Data_Should_Pass()
        {
            // Arrange
            List<StudentModel> data = new List<StudentModel>();
            var data1 = new StudentModel
            {
                Id = "hi"
            };
            data.Add(data1);
            var expect = "hi";

            // Act
            var returned = new StudentViewModel(data);
            var result = returned.StudentList.FirstOrDefault().Id;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        #endregion Instantiate
    }
}
