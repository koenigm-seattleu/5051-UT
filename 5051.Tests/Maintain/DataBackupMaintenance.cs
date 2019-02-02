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
    public class DataBackupMaintenanceTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

        #region Instantiate
        [TestMethod]
        public void Maintenance_DataBackup_Instantiate_Default_Should_Pass()
        {
            // Arrange
            var Maintenance = new DataBackupMaintenance();

            // Act
            var result = Maintenance.GetType();

            // Assert
            Assert.AreEqual(result, new DataBackupMaintenance().GetType(), TestContext.TestName);
        }
        #endregion Instantiate

        #region DataBackup
        [TestMethod]
        public void Maintenance_DataBackup_Source_Match_Destination_Should_Fail()
        {
            // Arrange
            var Maintenance = new DataBackupMaintenance();

            // Act
            var result = Maintenance.DataBackup(DataSourceEnum.Mock, DataSourceEnum.Mock);

            // Assert
            Assert.AreEqual(false, result, TestContext.TestName);
        }

        [TestMethod]
        public void Maintenance_DataBackup_Source_Mock_Destination_Should_Fail()
        {
            // Arrange
            var Maintenance = new DataBackupMaintenance();

            // Act
            var result = Maintenance.DataBackup(DataSourceEnum.Mock, DataSourceEnum.Local);

            // Assert
            Assert.AreEqual(false, result, TestContext.TestName);
        }

        [TestMethod]
        public void Maintenance_DataBackup_Default_Should_Fail()
        {
            // Arrange
            var Maintenance = new DataBackupMaintenance();

            // Act
            var result = Maintenance.DataBackup(DataSourceEnum.ServerTest, DataSourceEnum.Local);

            // Assert
            Assert.AreEqual(true, result, TestContext.TestName);
        }
        #endregion DataBackup
    }
}