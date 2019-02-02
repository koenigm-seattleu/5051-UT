using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class AvatarItemCategoryEnumUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_AvatarItemCategoryEnumUnitTests_Values_Should_Pass()
        {
            // Assert

            // Make sure there are no additional values
            var enumCount = AvatarItemCategoryEnum.GetNames(typeof(AvatarItemCategoryEnum)).Length;
            Assert.AreEqual(9, enumCount, TestContext.TestName);

            // Check each value against their expected value.
            Assert.AreEqual(0, (int)AvatarItemCategoryEnum.Unknown, TestContext.TestName);
            Assert.AreEqual(50, (int)AvatarItemCategoryEnum.HairBack, TestContext.TestName);
            Assert.AreEqual(10, (int)AvatarItemCategoryEnum.Head, TestContext.TestName);
            //Assert.AreEqual(3, (int)AvatarItemCategoryEnum.ShirtShort, TestContext.TestName);
            Assert.AreEqual(20, (int)AvatarItemCategoryEnum.Expression, TestContext.TestName);
            Assert.AreEqual(30, (int)AvatarItemCategoryEnum.Cheeks, TestContext.TestName);
            Assert.AreEqual(40, (int)AvatarItemCategoryEnum.HairFront, TestContext.TestName);
            Assert.AreEqual(60, (int)AvatarItemCategoryEnum.Accessory, TestContext.TestName);
            Assert.AreEqual(70, (int)AvatarItemCategoryEnum.ShirtFull, TestContext.TestName);
            Assert.AreEqual(80, (int)AvatarItemCategoryEnum.Pants, TestContext.TestName);
        }
        #endregion Instantiate

        #region DisplayName
        
        [TestMethod]
        public void Models_AvatarItemCategoryEnumUnitTests_DisplayName_Values_Should_Pass()
        {
            // Arrange

            // Act
            // Make sure there are no additional values
            var enumCount = AvatarItemCategoryEnum.GetNames(typeof(AvatarItemCategoryEnum)).Length;
           
            // Reset
        

            // Assert

            // Check each value against their expected value.
            Assert.AreEqual("Unknown", AvatarItemCategoryEnum.Unknown.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Hair", AvatarItemCategoryEnum.HairBack.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Head", AvatarItemCategoryEnum.Head.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Expression", AvatarItemCategoryEnum.Expression.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Cheeks", AvatarItemCategoryEnum.Cheeks.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Bangs", AvatarItemCategoryEnum.HairFront.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Accessory", AvatarItemCategoryEnum.Accessory.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Shirt", AvatarItemCategoryEnum.ShirtFull.GetDisplayName(), TestContext.TestName);
            Assert.AreEqual("Pants", AvatarItemCategoryEnum.Pants.GetDisplayName(), TestContext.TestName);
        }
        #endregion
    }
}
