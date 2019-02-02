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
    public class StudentInputModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_StudentInputModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new StudentInputModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_StudentInputModel_Default_Get_Set_Should_Pass()
        {

            // Act
            var result = new StudentInputModel();
            var expect = "id";
            result.Id = expect;

            // Assert
            Assert.AreEqual(expect, result.Id, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
