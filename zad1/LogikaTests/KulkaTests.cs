using Logika;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika.Tests
{
    [TestClass()]
    public class KulkaTests
    {
        [TestMethod()]
        public void CzyWZasieguTest()
        {
            Vector2 leg = new Vector2(4, 6);
            Vector2 speed = new Vector2(0, 0);
            Kulka ball1 = new Kulka(18, speed, leg, null, null);

            Vector2 leg2 = new Vector2(3, 4);
            Vector2 speed2 = new Vector2(0, 0);
            Kulka ball2 = new Kulka(12, speed2, leg2, null, null);

            Assert.IsTrue(ball1.CzyWZasiegu(ball2));
        }
    }
}