using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class ShopTruckInputModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_ShopTruckInputModel_Default_Instantiate_Should_Pass()
        {
            //arrange

            //act
            var result = new ShopTruckInputModel();

            //assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion Instantiate
    }
}
