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
    public class BackupDataInputModelTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_BackupDataInputModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new BackupDataInputModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        #endregion Instantiate

        #region Get
        [TestMethod]
        public void Models_BackupDataInputModel_SetGet_Should_Pass()
        {

            var TempPassword = "Password";
            var TempDestination = DataSourceEnum.Local;
            var TempConfirmDestination = DataSourceEnum.Local;
            var TempSource = DataSourceEnum.Local;
            var TempConfirmSource = DataSourceEnum.Local;

            // Act
            var result = new BackupDataInputModel
            {
                Password = TempPassword,
                Destination = TempDestination,
                ConfirmDestination = TempConfirmDestination,
                Source = TempSource,
                ConfirmSource = TempConfirmSource
            };

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
            Assert.AreEqual(TempPassword, result.Password, TestContext.TestName);
            Assert.AreEqual(TempDestination, result.Destination, TestContext.TestName);
            Assert.AreEqual(TempConfirmDestination, result.ConfirmDestination, TestContext.TestName);
            Assert.AreEqual(TempSource, result.Source, TestContext.TestName);
            Assert.AreEqual(TempConfirmSource, result.ConfirmSource, TestContext.TestName);
        }

        #endregion Get

        #region Set
        #endregion Set
    }
}
