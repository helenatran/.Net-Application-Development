using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Week5LabProgramQuestion;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCircle()
        {
            var circle = new Shapes(4.0);
            Assert.AreEqual(circle.CircleArea(), 50.272);
        }

        [TestMethod]
        public void testSquare()
        {
            var square = new Shapes(5.0, 4.0);
            Assert.AreEqual(square.RectangleArea(), 20.0);
        }
    }
}
