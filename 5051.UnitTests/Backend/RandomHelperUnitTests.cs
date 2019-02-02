using _5051.Backend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class RandomHelperUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Backend_RandomHelper_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var backend = RandomHelper.Instance;

            //var expect = backend;

            // Act
            var result = backend;

            //Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate

        #region SetForcedNumber
        [TestMethod]
        public void Backend_RandomHelper_SetForcedNumber_Should_Pass()
        {
            // Arrange
            var backend = RandomHelper.Instance;
            var expect = 6;
            RandomHelper.Instance.EnableForcedNumber(true);
            RandomHelper.Instance.SetForcedNumber(expect);

            // Act
            var result = RandomHelper.Instance.GetRandomNumber(30);

            //Reset
            DataSourceBackend.Instance.Reset();
            RandomHelper.Instance.EnableForcedNumber(false);

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion SetForcedNumber

        #region GetRandomNumber
        [TestMethod]
        public void Backend_RandomHelper_GetRandomNumber_Data_Is_Valid_Should_Pass()
        {
            // Arrange
            var backend = RandomHelper.Instance;

            // Act
            int expect = 2;
            RandomHelper.Instance.SetForcedNumber(expect);
            var result = RandomHelper.Instance.GetRandomNumber(10);

            //Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_RandomHelper_GetRandomNumber_Data_Valid_Random_Should_Pass()
        {
            /// Random turned on, should yield a number between 0 and max
            // Arrange
            var backend = RandomHelper.Instance;
            var max = 10;

            // Act
            var result = RandomHelper.Instance.GetRandomNumber(max);

            //Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsTrue(result<=max, TestContext.TestName);
            Assert.IsTrue(result >=0, TestContext.TestName);

        }
        #endregion GetRandomNumber
    }
}
