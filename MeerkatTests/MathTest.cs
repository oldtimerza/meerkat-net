using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeerkatTests
{
    [TestClass]
    public class MathTest
    {
        [TestMethod]
        public void ShouldHandleModuloForPositiveNumber()
        {
            int result = Meerkat.Utility.Math.Mod(4, 3);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ShouldHandleModuloForNegativeNumber()
        {
            int result = Meerkat.Utility.Math.Mod(-4, 3);
            Assert.AreEqual(2, result);
       }
    }
}
