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
    public class AvatarModelUnitTests
    {
        public TestContext TestContext { get; set; }

        #region Instantiate
        [TestMethod]
        public void Models_AvatarModel_Default_Instantiate_Should_Pass()
        {

            // Act
            var result = new AvatarModel();

            // Assert
            Assert.IsNotNull(result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_AvatarModel_Default_Instantiate_With_Data_Should_Pass()
        {
            // Arrange
            string uri = "uri";
            string name = "name";
            string description = "description";
            int level = 1;

            var expect = uri;

            // Act
            var returned = new AvatarModel(uri, name, description, level);
            var result = returned.Uri;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_AvatarModel_Default_Instantiate_With_Invalid_Data_URI_Null_Should_Fail()
        {
            // Arrange
            string uri = null;
            string name = "name";
            string description = "description";
            int level = 1;

            var expect = uri;

            // Act
            var returned = new AvatarModel(uri, name, description, level);
            var result = returned.Uri;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }


        #endregion Instantiate

        #region Update

        [TestMethod]
        public void Models_AvatarModel_Update_With_Invalid_Data_Should_Fail()
        {
            // Arrange

            var expect = "test";

            var data = new AvatarModel
            {
                Uri = "bogus"
            };

            var test = new AvatarModel
            {
                Uri = "test"
            };

            // Act
            data.Update(test);
            var result = data.Uri;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        [TestMethod]
        public void Models_AvatarModel_Update_With_Invalid_Data_Null_Should_Fail()
        {
            // Arrange

            var expect = "test";

            var data = new AvatarModel
            {
                Id = "test"
            };

            // Act
            data.Update(null);
            var result = data.Id;

            // Assert
            Assert.AreEqual(expect, result, TestContext.TestName);
        }

        #endregion Update
    }
}
