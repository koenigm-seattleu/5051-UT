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
    public class AvatarModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_AvatarModel_Default_Instantiate_Should_Pass()
        {

            // Arrange

            // Act
            var result = new AvatarModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_AvatarModel_Default_Instantiate_With_Data_Should_Pass()
        {
            // Arrange

            // Act

            // Assert

            Assert.IsTrue(true);    // Temp placeholder of a passing test....
            //Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion Instantiate

        #region Update


        #endregion Update
    }
}
