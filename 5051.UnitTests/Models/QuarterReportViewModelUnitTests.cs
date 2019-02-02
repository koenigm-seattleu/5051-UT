using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class QuarterReportViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_QuarterReportViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new QuarterReportViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate

        #region Get_Set_All_Fields
        [TestMethod]
        public void Models_QuarterReportViewModel_Get_Set_Check_All_Fields_Should_Pass()
        {
            // Arrange

            // Act
            // Set all the fields for a BaseReportViewModel
            var test = new QuarterReportViewModel
            {
                SelectedQuarterId = 1,
                Quarters = new List<SelectListItem>()
            };

            var expectedSelectedSemesterId = 1;
            // Assert

            //Check each value
            Assert.AreEqual(test.SelectedQuarterId, expectedSelectedSemesterId, "SelectedQuarterId " + TestContext.TestName);

            Assert.IsNotNull(test.Quarters, "Quarters " + TestContext.TestName);
        }
        #endregion Get_Set_All_Fields
    }
}
