using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using System.Web.Mvc;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class ConfigureTwoFactorViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_ConfigureTwoFactorViewModel_Default_Instantiate_Should_Pass()
        {
            //arrange
            var test = new ConfigureTwoFactorViewModel();
            var expectSelectedProvider = "Good provider";
            var expectProviders = new List<SelectListItem> { };

            //act
            test.SelectedProvider = expectSelectedProvider;
            test.Providers = expectProviders;

            //assert
            Assert.AreEqual(expectSelectedProvider, test.SelectedProvider, TestContext.TestName);
            Assert.AreEqual(expectProviders, test.Providers, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
