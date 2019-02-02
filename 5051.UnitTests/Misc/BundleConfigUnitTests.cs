using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using System.Web.Optimization;

namespace _5051.UnitTests.Misc
{
    [TestClass]
    public class BundleConfigUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Misc_BundleConfig_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new BundleConfig();
            BundleCollection data = new BundleCollection();

            // Act
            BundleConfig.RegisterBundles(data);

            // Assert
            Assert.IsNotNull(test, TestContext.TestName);
        }

        #endregion Instantiate
    }
}
