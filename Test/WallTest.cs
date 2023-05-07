using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Game_Engine_Library;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Test {
    [TestClass]
    public class WallTest {
        private GLControl glControl;

        [TestInitialize]
        public void Init() {
            glControl = new GLControl();
            glControl.MakeCurrent();
        }

        [TestMethod]
        public void Wall_Test() {
            var x = 10.0;
            var y = 20.0;
            var width = 30.0;
            var height = 40.0;

            var wall = new Wall(x, y, width, height);
            var privateObject = new PrivateObject(wall);

            Assert.AreEqual(x, privateObject.GetField("x"));
            Assert.AreEqual(y, privateObject.GetField("y"));
            Assert.AreEqual(width, privateObject.GetField("width"));
            Assert.AreEqual(height, privateObject.GetField("height"));
        }
    }
}
