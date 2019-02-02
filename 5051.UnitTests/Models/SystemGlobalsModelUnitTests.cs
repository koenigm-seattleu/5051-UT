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
    public class SystemGlobalsModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_SystemGlobals_Default_Instantiate_Should_Pass()
        {
            // Arrange
            var myModel = SystemGlobalsModel.Instance;

            // Act
            var result = myModel.DataSourceValue;

            // Assert
            Assert.AreEqual(result, DataSourceEnum.Mock, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SystemGlobals_Default_Existing_Should_Pass()
        {
            // Calls for the instance two times, the first time creates it, the second time uses the existing

            // Arrange
            var myFirstTime = SystemGlobalsModel.Instance;
            var myModel = SystemGlobalsModel.Instance;

            // Act
            var result = myModel.DataSourceValue;

            // Assert
            Assert.AreEqual(result, DataSourceEnum.Mock, TestContext.TestName);
        }

        #endregion Instantiate

        #region SelectDataSourceEnum
        [TestMethod]
        public void Models_SystemGlobals_SelectDataSourceEnum_Null_Should_Pass()
        {
            // Arrange

            // Act
            var result = SystemGlobalsModel.SelectDataSourceEnum(null);

            // Assert
            Assert.AreEqual(result, DataSourceEnum.Mock, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SystemGlobals_SelectDataSourceEnum_Other_Should_Pass()
        {
            // Arrange

            // Act
            var result = SystemGlobalsModel.SelectDataSourceEnum("other");

            // Assert
            Assert.AreEqual(result, DataSourceEnum.Mock, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SystemGlobals_SelectDataSourceEnum_LiveSchool_Should_Pass()
        {
            // Arrange

            // Act
            var result = SystemGlobalsModel.SelectDataSourceEnum("mchs.azurewebsites.net");

            // Assert
            Assert.AreEqual(result, DataSourceEnum.ServerLive, TestContext.TestName);
        }

        [TestMethod]
        public void Models_SystemGlobals_SelectDataSourceEnum_LiveTest_Should_Pass()
        {
            // Arrange

            // Act
            var result = SystemGlobalsModel.SelectDataSourceEnum("azurewebsites.net");

            // Assert
            Assert.AreEqual(result, DataSourceEnum.ServerTest, TestContext.TestName);
        }
        #endregion SelectDataSourceEnum
    }
}
