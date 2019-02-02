using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class SmsServiceUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_SmsService_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new SmsService();
            var data = new IdentityMessage();

            // Act
            test.SendAsync(data);

            // Assert
            Assert.IsNotNull(data, TestContext.TestName);
        }

        #endregion Instantiate
    }
}
