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
    public class AvatarSelectShopViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate

        [TestMethod]
        public void Models_AvatarSelectShopViewModel_Default_Instantiate_Get_Set_Should_Pass()
        {
            // Arrange
            
            // Act
            var result = new AvatarSelectShopViewModel();

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result,TestContext.TestName);
        }

        #endregion Instantiate
    }
}
