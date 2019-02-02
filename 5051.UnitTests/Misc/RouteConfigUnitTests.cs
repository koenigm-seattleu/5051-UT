using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Optimization;
using System.Web.Routing;

namespace _5051.UnitTests.Misc
{
    [TestClass]
    public class RouteConfigUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Misc_RouteConfig_Default_Instantiate_Should_Pass()
        {
            //arrange
            var data = new RouteCollection();

            // Act
            RouteConfig.RegisterRoutes(data);

            // Assert
            Assert.IsNotNull(data, TestContext.TestName);
        }

        #endregion Instantiate
    }
}
