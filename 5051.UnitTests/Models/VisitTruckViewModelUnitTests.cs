using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using System.Globalization;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class VisitTruckViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_VisitTruckViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new VisitTruckViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion Instantiate
    }
}
