using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aplikacja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        [TestMethod()]
        public void MainWindowTest()
        {
            Assert.AreEqual((1+1), 2);
            Assert.IsTrue((1+1) == 2);
        }
    }
}