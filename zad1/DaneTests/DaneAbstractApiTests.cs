using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane.Tests
{
    [TestClass()]
    public class DaneAbstractApiTests
    {
        [TestMethod()]
        public void StworzDaneApiTest()
        {
            DaneAbstractApi api = DaneAbstractApi.StworzDaneApi();
            Assert.IsNotNull(api);
        }

        [TestMethod()]
        public void RandomTest()
        {
            DaneAbstractApi dataApi = DaneAbstractApi.StworzDaneApi();
            Assert.AreNotEqual(dataApi.WysokoscPlanszy, default);
            Assert.AreNotEqual(dataApi.SzerokoscPlanszy, default);
            Assert.AreNotEqual(dataApi.maxSrednicaKuli, default);
        }

    }
}