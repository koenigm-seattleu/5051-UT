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
    public class ApplicationDbContextUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_ApplicationDbContext_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new ApplicationDbContext();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion Instantiate

        #region Create
        [TestMethod]
        public void Models_ApplicationDbContext_Create_Should_Pass()
        {
            // Arrange

            // Act
            var result = ApplicationDbContext.Create();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Create
    }
}
