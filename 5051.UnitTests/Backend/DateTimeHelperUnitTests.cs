using _5051.Backend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class DateTimeHelperUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Backend_DateTimeHelper_Default_Instantiate_Should_Pass()
        {
            // Arrange

            // Act
            var result = DateTimeHelper.Instance;

            //Reset
            DataSourceBackend.Instance.Reset();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }
        #endregion Instantiate

        #region SetForced
        [TestMethod]
        public void Backend_DateTimeHelper_SetForced_Should_Pass()
        {
            // Arrange
            var backend = DateTimeHelper.Instance;
            var expect = DateTime.Parse("01/23/2018");

            DateTimeHelper.Instance.SetForced(expect);
            DateTimeHelper.Instance.EnableForced(true);

            // Act
            var result = DateTimeHelper.Instance.GetDateTimeNowUTC();

            //Reset
            DataSourceBackend.Instance.Reset();
            DateTimeHelper.Instance.EnableForced(false);

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }
        #endregion SetForced

        #region GetDateTime
        [TestMethod]
        public void Backend_DateTimeHelper_GetDateTime_Data_Is_Valid_Should_Pass()
        {
            // Arrange
            var backend = DateTimeHelper.Instance;

            // Act
            var expect = DateTime.Parse("01/23/2018");

            DateTimeHelper.Instance.SetForced(expect);
            DateTimeHelper.Instance.EnableForced(true);

            var result = DateTimeHelper.Instance.GetDateTimeNowUTC();

            //Reset
            DataSourceBackend.Instance.Reset();
            DateTimeHelper.Instance.EnableForced(false);

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_DateTimeHelper_GetDateTime_IsForced_False_Should_Pass()
        {
            // Arrange
            var backend = DateTimeHelper.Instance;

            // Act
            var expect = DateTime.Parse("01/23/2018");
            DateTimeHelper.Instance.SetForced(expect);
            DateTimeHelper.Instance.EnableForced(false);

            var result = DateTimeHelper.Instance.GetDateTimeNowUTC();

            //Reset
            DataSourceBackend.Instance.Reset();
            DateTimeHelper.Instance.EnableForced(false);

            // Assert
            Assert.AreNotEqual(expect, result, TestContext.TestName);
        }
        #endregion GetDateTime
    }
}
