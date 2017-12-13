using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.UI.Testing
{
    public class UITestBase
    {

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        public string BaseDir => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public string SutPath => Path.Combine(BaseDir, "AutoReservation.UI.exe");
    }
}
