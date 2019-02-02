using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using _5051.Backend;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Reflection;

namespace _5051.UnitTests.Backend
{
    [TestClass]
    public class UTCConversionsBackendUnitTests
    {
        [TestInitialize]
        public void TestSetup()
        {
            // We need to setup the Current HTTP Context as follows:            

            // Step 1: Setup the HTTP Request
            var httpRequest = new HttpRequest("", "http://5051.azurewebsites.net/", "");

            // Step 2: Setup the HTTP Response
            var httpResponce = new HttpResponse(new StringWriter());

            // Step 3: Setup the Http Context
            var httpContext = new HttpContext(httpRequest, httpResponce);
            var sessionContainer =
                new HttpSessionStateContainer("id",
                                               new SessionStateItemCollection(),
                                               new HttpStaticObjectsCollection(),
                                               10,
                                               true,
                                               HttpCookieMode.AutoDetect,
                                               SessionStateMode.InProc,
                                               false);
            httpContext.Items["AspSession"] =
                typeof(HttpSessionState)
                .GetConstructor(
                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                    null,
                                    CallingConventions.Standard,
                                    new[] { typeof(HttpSessionStateContainer) },
                                    null)
                .Invoke(new object[] { sessionContainer });

            // Step 4: Assign the Context
            HttpContext.Current = httpContext;
        }

        //[TestMethod]
        //public void BasicTest_Push_Item_Into_Session()
        //{
        //    // Arrange
        //    var itemValue = "RandomItemValue";
        //    var itemKey = "RandomItemKey";

        //    // Act
        //    HttpContext.Current.Session.Add(itemKey, itemValue);

        //    // Assert
        //    Assert.AreEqual(HttpContext.Current.Session[itemKey], itemValue);
        //}


        public TestContext TestContext { get; set; }

        #region FromClientTime
        [TestMethod]
        public void Backend_UTCConversionsBackend_FromClientTime_Valid_Date_TimeOffSet_Is_Null_Should_Pass()
        {
            //arrange
            var inputLocalDateTime = DateTime.Now;
            var expectDateTime = DateTime.Now.ToUniversalTime();

            //act
            var result = _5051.Backend.UTCConversionsBackend.FromClientTime(inputLocalDateTime);

            //assert
            Assert.AreEqual(expectDateTime.Minute, result.Minute, TestContext.TestName);
            Assert.AreEqual(expectDateTime.Hour, result.Hour, TestContext.TestName);
        }

        #endregion

        #region ToClientTime
        [TestMethod]
        public void Backend_UTCConvensionsBackend_ToClientTime_Valid_Date_Already_Local_Should_Pass()
        {
            //arrange
            var inputDateTime = DateTime.Now;
            var expectDateTime = DateTime.Now;

            //act
            var result = UTCConversionsBackend.ToClientTime(inputDateTime);

            //assert
            Assert.AreEqual(expectDateTime.Minute, result.Minute, TestContext.TestName);
            Assert.AreEqual(expectDateTime.Hour, result.Hour, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_UTCConvensionsBackend_ToClientTime_Valid_Date_UTC_timeOffSet_Is_Null_Should_Pass()
        {
            //arrange
            var inputDateTime = DateTime.Now;
            inputDateTime = DateTime.SpecifyKind(inputDateTime, DateTimeKind.Unspecified);
            var expectDateTime = DateTime.UtcNow;

            //act
            var result = UTCConversionsBackend.ToClientTime(inputDateTime);

            //assert
            Assert.AreEqual(expectDateTime.Minute, result.Minute, TestContext.TestName);
            Assert.AreEqual(expectDateTime.Hour, result.Hour, TestContext.TestName);
        }

        [TestMethod]
        public void Backend_UTCConvensionsBackend_ToClientTime_Valid_Date_UTC_HttpContext_Current_Is_Null_Should_Pass()
        {
            //arrange
            var inputDateTime = DateTime.Now;
            inputDateTime = DateTime.SpecifyKind(inputDateTime, DateTimeKind.Unspecified);
            var expectDateTime = DateTime.UtcNow;
            HttpContext.Current = null;

            //act
            var result = UTCConversionsBackend.ToClientTime(inputDateTime);

            //assert
            Assert.AreEqual(expectDateTime.Minute, result.Minute, TestContext.TestName);
            Assert.AreEqual(expectDateTime.Hour, result.Hour, TestContext.TestName);
        }
        #endregion


    }
}