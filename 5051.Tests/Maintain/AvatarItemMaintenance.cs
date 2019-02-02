using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using _5051;
using _5051.Maintain;
using _5051.Backend;
using _5051.Models;

namespace _5051.Tests.Maintenance
{
    [TestClass]
    public class AvatarItemMaintenanceTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Maintenance_AvatarItem_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var Maintenance = new AvatarItemMaintenance();

            // Act
            var result = Maintenance.GetType();

            // Assert
            Assert.AreEqual(result, new AvatarItemMaintenance().GetType(), TestContext.TestName);
        }

        [TestMethod]
        public void Maintenance_AvatarItem_RefreshAvatars_Default_Should_Pass()
        {
            // Arrange
            var Maintenance = new AvatarItemMaintenance();

            // Act
            var result = Maintenance.RefreshAvatars();

            // Assert
            Assert.AreEqual(true, result, TestContext.TestName);
        }
        #endregion Instantiate
    }
}