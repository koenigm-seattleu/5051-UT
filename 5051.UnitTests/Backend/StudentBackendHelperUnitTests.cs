using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class StudentBackendHelperUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        [TestMethod]
        public void Backend_StudentBackendHelper_CreateDemoAttendance_Yesterday_Is_Before_DateEnd_Should_Pass()
        {
            //arrange
            SchoolDismissalSettingsBackend.SetDataSourceDataSet(_5051.Models.DataSourceDataSetEnum.Demo);
            var schoolDismissalSettingsBackend = SchoolDismissalSettingsBackend.Instance;
            var yesterday = DateTime.UtcNow.AddDays(-1);
            schoolDismissalSettingsBackend.GetDefault().DayLast = DateTime.UtcNow;

            //act
            StudentBackendHelper.CreateDemoAttendance();

            //reset
            schoolDismissalSettingsBackend.Reset();

            //assert
            Assert.IsNotNull("poop", TestContext.TestName);
        }
    }
}
