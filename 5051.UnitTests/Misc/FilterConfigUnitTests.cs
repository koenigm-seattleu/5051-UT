using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Mvc;

namespace _5051.UnitTests.Misc
{
    [TestClass]
    public class FilterConfigUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Misc_FilterConfig_Default_Instantiate_Should_Pass()
        {
            //arrange
            var data = new GlobalFilterCollection();

            // Act
            FilterConfig.RegisterGlobalFilters(data);

            // Assert
            Assert.IsNotNull(data, TestContext.TestName);
        }

        #endregion Instantiate
    }
}
