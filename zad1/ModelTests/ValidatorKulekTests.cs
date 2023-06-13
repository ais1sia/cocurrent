using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Tests
{
    [TestClass()]
    public class ValidatorKulekTests
    {
        [TestMethod()]
        public void ValidatorKulekTest()
        {
            int min = 1;
            int max = 20;

            InterfaceValidator<int> log = new ValidatorKulek(min, max);

            Assert.IsTrue(log.IsValid(min + 1));
            Assert.IsTrue(log.IsValid(max - 1));

            Assert.IsFalse(log.IsValid(max + 1));
            Assert.IsFalse(log.IsValid(min - 1));
        }
    }
}