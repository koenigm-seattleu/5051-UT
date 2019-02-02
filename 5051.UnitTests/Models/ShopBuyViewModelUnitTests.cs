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
    public class ShopBuyViewModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_ShopBuyViewModel_Default_Instantiate_Get_Set_Should_Pass()
        {
            //arrange
            var result = new ShopBuyViewModel();
            var expectStudentId = "GoodStudentId1";
            var expectItemId = "GoodItemId1";

            // Act
            result.StudentId = expectStudentId;
            result.ItemId = expectItemId;

            // Assert
            Assert.AreEqual(expectStudentId, result.StudentId, TestContext.TestName);
            Assert.AreEqual(expectItemId, result.ItemId, TestContext.TestName);
        }
        #endregion Instantiate
    }
}
