using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Tests
{
    [TestClass()]
    public class ApiModelTests
    {
        [TestMethod]
        public void StworzApiModelTest()
        {
            ApiModel log = ApiModel.StworzModelApi();
            Assert.IsNotNull(log);
        }
    }
}