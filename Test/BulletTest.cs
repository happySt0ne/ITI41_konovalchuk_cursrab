using Game_Engine_Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK;
using System;

namespace Test {
    
    namespace Game_Engine_Library_Tests {
        [TestClass]
        public class BulletTests {
            private GLControl glControl;

            [TestInitialize]
            public void Init() {
                glControl = new GLControl();
                glControl.MakeCurrent();
            }

            [TestMethod]
            public void Constructor_Test() {
                double x = 10;
                double y = 20;
                int angle = 30;
                string side = "left";

                Bullet bullet = new Bullet(x, y, angle, side);
                PrivateObject privateObject = new PrivateObject(bullet);
               
                Assert.AreEqual(x, bullet.Points[0].Item1);
                Assert.AreEqual(y, bullet.Points[0].Item2);
                Assert.AreEqual(Constants.BULLETS_WIDTH, privateObject.GetField("width"));
                Assert.AreEqual(Constants.BULLETS_HEIGHT, privateObject.GetField("height"));
            }

            [TestMethod]
            public void MoveBullet_Test() {
                double x = 10;
                double y = 20;
                int angle = 30;
                string side = "left";
                Bullet bullet = new Bullet(x, y, angle, side);
                double originalX = bullet.Points[0].Item1;
                double originalY = bullet.Points[0].Item2;

                bullet.MoveBullet();

                Assert.AreNotEqual(originalX, bullet.Points[0].Item1);
                Assert.AreNotEqual(originalY, bullet.Points[0].Item2);
            }
        }
    }
}
