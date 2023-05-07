using Game_Engine_Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test {
    [TestClass]
    public class CollisionTests {
        [TestMethod]
        [DataRow(0, 0, 10, 10, 5, 5, true)]
        [DataRow(0, 0, 10, 10, 20, 20, false)]
        [DataRow(0, 0, 10, 10, 5, 0, true)]
        [DataRow(0, 0, 10, 10, 20, 0, false)]
        [DataRow(0, 0, 10, 10, 0, 5, true)]
        [DataRow(0, 0, 10, 10, 0, 20, false)]
        [DataRow(0, 0, 10, 10, 5, 5, true)]
        [DataRow(0, 0, 10, 10, 5, 5, true)]
        public void IntersectWith_Test (double x1, double y1, double width1, double height1, double x2, double y2, bool expected) {
            var box1 = new Collision(x1, y1, width1, height1);
            var box2 = new Collision(x2, y2, width1, height1);

            bool result = box1.IntersectsWith(box2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(0, 0, 10, 10, 5, 5)]
        [DataRow(0, 0, 10, 10, 20, 20)]
        [DataRow(0, 0, 10, 10, 5, 0)]
        [DataRow(0, 0, 10, 10, 20, 0)]
        [DataRow(0, 0, 10, 10, 0, 5)]
        [DataRow(0, 0, 10, 10, 0, 20)]
        public void MoveCollisionBoxTo_Test(double x1, double y1, double width1, double height1, double x2, double y2) {
            var box = new Collision(x1, y1, width1, height1);

            box.MoveCollisionBoxTo(x2, y2);

            Assert.AreEqual(x2, box.x);
            Assert.AreEqual(y2, box.y);
        }
    }
}
