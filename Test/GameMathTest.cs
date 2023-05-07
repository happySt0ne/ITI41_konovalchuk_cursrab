using Game_Engine_Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Test {
    [TestClass]
    public class GameMathTests {
        [TestMethod]
        public void Rotate_List_Test() {
            var array = new List<(double, double)> { (0, 0), (2, 0), (2, 2), (0, 2) };
            var expectedArray = new List<(double, double)> { (0, 0), (0, 2), (-2, 2), (-2, 0) };
            var startIndex = 1;
            var endIndex = 4;
            var angle = 90.0;
            var bazePoint = (0, 0);

            GameMath.Rotate(array, startIndex, endIndex, angle, bazePoint);

            CollectionAssert.AreEqual(expectedArray, array);
        }

        [TestMethod]
        public void Rotate_Point_Test() {
            var bazePoint = (0d, 0d);
            var rotatePoint = (1d, 0d);
            var angle = 90.0;
            var expectedPoint = (0d, 1d);

            var result = GameMath.Rotate(bazePoint, rotatePoint, angle);

            Assert.AreEqual(expectedPoint, result);
        }

        [TestMethod]
        public void FindHypotenuse_Test() {
            var firstCathet = 3.0;
            var secondCathet = 4.0;
            var expectedHypotenuse = 5.0;

            var result = GameMath.FindHypotenuse(firstCathet, secondCathet);

            Assert.AreEqual(expectedHypotenuse, result);
        }
    }
}
