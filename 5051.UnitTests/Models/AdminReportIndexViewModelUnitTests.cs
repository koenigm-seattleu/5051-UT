using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5051.Models;
using Microsoft.AspNet.Identity;
using _5051.Backend;

namespace _5051.UnitTests.Models
{
    [TestClass]
    public class AdminReportIndexViewModelUnitTests
    {       
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }
    }
}
