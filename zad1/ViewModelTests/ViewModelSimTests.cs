using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ViewModel.Tests
{
    [TestClass()]
    public class ViewModelSimTests
    {
        [TestMethod]
        public void StartSimTest()
        {
            ViewModelSim log = new ViewModelSim();

            log.SimStart();

            Assert.IsTrue(log.getSetFlag);

        }

        [TestMethod]
        public void StopSimTest()
        {
            ViewModelSim log = new ViewModelSim();

            log.SimStop();

            Assert.IsFalse(log.getSetFlag);

        }

        [TestMethod]
        public void LiczbaKulekZmn()
        {

            bool chg = false;

            ViewModelSim log = new ViewModelSim();

            log.PropertyChanged += (object? sender, PropertyChangedEventArgs a) => chg = true;

            Assert.IsTrue(!chg);

            //tera zmiana wartosci
            log.LiczbaKulek = 7;
            Assert.IsTrue(chg);

        }


    }
}