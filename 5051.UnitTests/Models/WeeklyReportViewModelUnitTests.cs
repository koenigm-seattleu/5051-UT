using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Backend;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class WeeklyReportViewModelUnitTests
    {
        public TestContext TestContext { get; set; }
        
        #region Instantiate
        [TestMethod]
        public void Models_WeeklyReportViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new WeeklyReportViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate

        #region Get_Set_All_Fields
        [TestMethod]
        public void Models_WeeklyReportViewModel_Get_Set_Check_All_Fields_Should_Pass()
        {
            // Arrange

            // Act
            // Set all the fields for a BaseReportViewModel
            var test = new WeeklyReportViewModel
            {
                SelectedWeekId = 1,
                Weeks = new List<SelectListItem>()
            };

            var expectedSelectedWeekId = 1;
            // Assert

            //Check each value
            Assert.AreEqual(test.SelectedWeekId, expectedSelectedWeekId, "SelectedWeekId " + TestContext.TestName);

            Assert.IsNotNull(test.Weeks, "Weeks " + TestContext.TestName);
        }
        #endregion Get_Set_All_Fields
    }
}
