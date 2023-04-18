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
            DaneApi api2 = new DaneApi();

            if(api2.WysokoscPlanszy != api.WysokoscPlanszy) 
            { 
                Assert.Fail();
            }
            else if(api2.SzerokoscPlanszy != api.SzerokoscPlanszy)
            {
                Assert.Fail();
            }
            else if (api2.SrednicaKuli != api.SrednicaKuli)
            {
                Assert.Fail();
            }
            else
            {
                Assert.IsTrue(true);
            }

        }
    }
}