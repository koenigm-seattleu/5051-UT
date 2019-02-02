using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class FactoryInventoryCategoryEnumUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_FactoryInventoryCategoryEnumUnitTests_Values_Should_Pass()
        {
            // Assert

            // Make sure there are no additional values
            var enumCount = FactoryInventoryCategoryEnum.GetNames(typeof(FactoryInventoryCategoryEnum)).Length;
            Assert.AreEqual(8, enumCount, TestContext.TestName);

            // Check each value against their expected value.
            Assert.AreEqual(0, (int)FactoryInventoryCategoryEnum.Unknown, TestContext.TestName);
            Assert.AreEqual(5, (int)FactoryInventoryCategoryEnum.Food, TestContext.TestName);

            Assert.AreEqual(10, (int)FactoryInventoryCategoryEnum.Truck, TestContext.TestName);
            Assert.AreEqual(11, (int)FactoryInventoryCategoryEnum.Wheels, TestContext.TestName);
            Assert.AreEqual(12, (int)FactoryInventoryCategoryEnum.Topper, TestContext.TestName);
            Assert.AreEqual(13, (int)FactoryInventoryCategoryEnum.Sign, TestContext.TestName);
            Assert.AreEqual(14, (int)FactoryInventoryCategoryEnum.Trailer, TestContext.TestName);
            Assert.AreEqual(15, (int)FactoryInventoryCategoryEnum.Menu, TestContext.TestName);
        }
        #endregion Instantiate

        [TestMethod]
        public void Models_FactoryInventoryCategoryEnumUnitTests_DisplayName_Values_Should_Pass()
        {
            // Arrange

            // Act
            // Make sure there are no additional values
            var enumCount = FactoryInventoryCategoryEnum.GetNames(typeof(FactoryInventoryCategoryEnum)).Length;

            // Reset


            // Assert

            // Check each value against their expected value.
            Assert.AreEqual("Unknown", FactoryInventoryCategoryEnum.Unknown.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Food", FactoryInventoryCategoryEnum.Food.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Menu", FactoryInventoryCategoryEnum.Menu.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Sign", FactoryInventoryCategoryEnum.Sign.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Topper", FactoryInventoryCategoryEnum.Topper.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Trailer", FactoryInventoryCategoryEnum.Trailer.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Truck", FactoryInventoryCategoryEnum.Truck.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Wheels", FactoryInventoryCategoryEnum.Wheels.GetDisplayName(), TestContext.TestName);
        }
    }
}
