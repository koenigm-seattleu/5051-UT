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
    public class ShopTruckUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Models_ShopTruckFullModel_Default_Instantiate_Get_Set_Should_Pass()
        {
            // Arange

            // Act
            var result = new ShopTruckFullModel();

            // Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);

            Assert.IsNotNull(result.Truck, "Truck " + TestContext.TestName);
            Assert.IsNotNull(result.Menu, "Menu "+ TestContext.TestName);
            Assert.IsNotNull(result.Sign, "Sign " + TestContext.TestName);
            Assert.IsNotNull(result.Trailer, "Trailer " + TestContext.TestName);
            Assert.IsNotNull(result.Topper, "Topper " + TestContext.TestName);
            Assert.IsNotNull(result.Wheels, "Wheels " + TestContext.TestName);
            Assert.IsNotNull(result.IsClosed, "Closed " + TestContext.TestName);
            Assert.IsNotNull(result.CustomersTotal, "Customers" + TestContext.TestName);
            Assert.IsNotNull(result.TransactionList, "TransactionList" + TestContext.TestName);
            Assert.IsNotNull(result.TruckName, "TruckName" + TestContext.TestName);
            Assert.IsNotNull(result.Income, "Income" + TestContext.TestName);
            Assert.IsNotNull(result.Outcome, "Outcome" + TestContext.TestName);
            Assert.IsNotNull(result.BusinessList, "BusinessList" + TestContext.TestName);

            Assert.IsNotNull(result.TruckUri, "TruckUri " + TestContext.TestName);
            Assert.IsNotNull(result.MenuUri, "MenuUri " + TestContext.TestName);
            Assert.IsNotNull(result.SignUri, "SignUri " + TestContext.TestName);
            Assert.IsNotNull(result.TrailerUri, "TrailerUri " + TestContext.TestName);
            Assert.IsNotNull(result.TopperUri, "TopperUri " + TestContext.TestName);
            Assert.IsNotNull(result.WheelsUri, "WheelsUri " + TestContext.TestName);
            Assert.IsNotNull(result.Tokens, "Tokens " + TestContext.TestName);
            Assert.IsNotNull(result.Experience, "Experience " + TestContext.TestName);
            Assert.IsNotNull(result.IterationNumber, "IterationNumber " + TestContext.TestName);

        }

        #endregion Instantiate
    }
}
