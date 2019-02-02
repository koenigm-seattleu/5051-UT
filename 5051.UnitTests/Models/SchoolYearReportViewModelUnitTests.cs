using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class SchoolYearReportViewModelUnitTests
    {

        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_SchoolYearReportViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new SchoolYearReportViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
