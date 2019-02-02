using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class AvatarItemListViewModelTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Models_AvatarItemListViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new AvatarItemListViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SelectedAvatarItemListForStudentViewModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new SelectedAvatarItemListForStudentViewModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion Instantiate
    }
}
