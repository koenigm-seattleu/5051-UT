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
    public class CalendarMaintenanceTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DataSourceBackend.SetTestingMode(true);
        }

    }
}