using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using System.Linq;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class EmotionStatusEnumUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_EmotionStatusEnumUnitTests_Values_Should_Pass()
        {
            // Assert

            // Make sure there are no additional values
            var enumCount = EmotionStatusEnum.GetNames(typeof(EmotionStatusEnum)).Length;
            Assert.AreEqual(5, enumCount, TestContext.TestName);

            // Check each value against their expected value.
            Assert.AreEqual(5, (int)EmotionStatusEnum.VeryHappy, TestContext.TestName);
            Assert.AreEqual(4, (int)EmotionStatusEnum.Happy, TestContext.TestName);
            Assert.AreEqual(3, (int)EmotionStatusEnum.Neutral, TestContext.TestName);
            Assert.AreEqual(2, (int)EmotionStatusEnum.Sad, TestContext.TestName);
            Assert.AreEqual(1, (int)EmotionStatusEnum.VerySad, TestContext.TestName);
        }
        #endregion Instantiate

        #region GetEmotionUri
        [TestMethod]
        public void Models_Emotion_GetEmotionUri_Values_Should_Pass()
        {
            // Assert

            // Make sure there are no additional values
            var enumValues = EmotionStatusEnum.GetValues(typeof(EmotionStatusEnum)).Cast< EmotionStatusEnum>();

            foreach (var item in enumValues)
            {
                var expect = Emotion.GetEmotionURI(item);
                Assert.IsNotNull(expect, TestContext.TestName);
            }

        }
        #endregion GetEmotionUri
    }
}
