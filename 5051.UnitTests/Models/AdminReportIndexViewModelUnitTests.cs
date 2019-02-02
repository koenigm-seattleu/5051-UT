using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using Microsoft.AspNet.Identity;
using _5051.Backend;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class AdminReportIndexViewModelUnitTests
    {       
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        [TestMethod]
        public void Models_IndexViewModels_Default_Instantiate_Should_Pass()
        {
            //arrange
            var studentList = DataSourceBackend.Instance.StudentBackend.Index();
            var expectedLeaderBoard = new List<StudentModel>();
            var test = new AdminReportIndexViewModel(studentList)
            {

                //act
                Leaderboard = expectedLeaderBoard
            };

            //assert
            Assert.AreEqual(expectedLeaderBoard, test.Leaderboard, TestContext.TestName);
        }
    }
}
