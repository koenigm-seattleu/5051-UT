using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class MonthlyReportViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_MonthlyReportViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new MonthlyReportViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate

        #region Get_Set_All_Fields
        [TestMethod]
        public void Models_MonthlyReportViewModel_Get_Set_Check_All_Fields_Should_Pass()
        {
            // Arrange

            // Act
            // Set all the fields for a BaseReportViewModel
            var test = new MonthlyReportViewModel
            {
                SelectedMonthId = 1,
                Months = new List<SelectListItem>()
            };

            var expectedSelectedWeekId = 1;
            // Assert

            //Check each value
            Assert.AreEqual(test.SelectedMonthId, expectedSelectedWeekId, "SelectedWeekId " + TestContext.TestName);

            Assert.IsNotNull(test.Months, "Weeks " + TestContext.TestName);
        }
        #endregion Get_Set_All_Fields
    }
}
