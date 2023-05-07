using Game_Engine_Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK;
using System;
using System.Linq;
using System.Reflection;

namespace Test {
    [TestClass]
    public class GameObjectTests {
        [TestMethod]
        public void GameObject_CreatesCollisionBoxWithCorrectDimensions() {
            double x = 0;
            double y = 0;
            double width = 10;
            double height = 20;

            GameObject gameObject = new TestGameObject(x, y, width, height);

            Assert.AreEqual(x, gameObject.Collision.x);
            Assert.AreEqual(y, gameObject.Collision.y);
            Assert.AreEqual(width, gameObject.Collision.width);
            Assert.AreEqual(height, gameObject.Collision.height);
        }

        [TestMethod]
        public void GameObject_CreatesPointsListWithCorrectPoints() {
            double x = 0;
            double y = 0;
            double width = 10;
            double height = 20;

            GameObject gameObject = new TestGameObject(x, y, width, height);

            Assert.AreEqual(x, gameObject.Points[0].Item1);
            Assert.AreEqual(y, gameObject.Points[0].Item2);
            Assert.AreEqual(x + width, gameObject.Points[1].Item1);
            Assert.AreEqual(y, gameObject.Points[1].Item2);
            Assert.AreEqual(x + width, gameObject.Points[2].Item1);
            Assert.AreEqual(y - height, gameObject.Points[2].Item2);
            Assert.AreEqual(x, gameObject.Points[3].Item1);
            Assert.AreEqual(y - height, gameObject.Points[3].Item2);
        }

        [TestMethod]
        public void TextureHorizontalReflection_Test() {
            var gameObject = new TestGameObject();
            gameObject.TextureHorizontalReflection();
            var texCoords = gameObject.GetTexCoords();

            Assert.AreEqual((1, 0), (texCoords[0,0], texCoords[0,1]));
            Assert.AreEqual((0, 0), (texCoords[1, 0], texCoords[1, 1]));
            Assert.AreEqual((0, 1), (texCoords[2, 0], texCoords[2, 1]));
            Assert.AreEqual((1, 1), (texCoords[3, 0], texCoords[3, 1]));
        }

        [TestMethod]
        [DataRow(0, 0, 10, 20, false)]
        [DataRow(-100, -100, 200, 200, true)]
        [DataRow(50, 50, 100, 100, true)]
        [DataRow(-2, 0, 10, 20, true)]
        [DataRow(-100, -100, 50, 50, true)]
        [DataRow(500, 500, 50, 50, true)]
        public void OutsideTheWindow_Test(double x, double y, double width, double height, bool expected) {
            GameObject gameObject = new TestGameObject(x, y, width, height);

            bool result = gameObject.OutsideTheWindow;

            Assert.AreEqual(expected, result);
        }

        private class TestGameObject : GameObject {
            public TestGameObject() : base() { }

            public TestGameObject(double x, double y, double width, double height) : base(x, y, width, height) { }

            public byte[,] GetTexCoords() {
                var field = typeof(GameObject).GetField("_texCoords", BindingFlags.NonPublic | BindingFlags.Instance);
                var valueTuples = (ValueTuple<byte, byte>[])field.GetValue(this);
                var byteArrayProjections = valueTuples.Select(t => new byte[] { t.Item1, t.Item2 });
                var byteArrayArrays = byteArrayProjections.ToArray();
                var result = new byte[byteArrayArrays.Length, 2];

                for (int i = 0; i < byteArrayArrays.Length; i++) {
                    result[i, 0] = byteArrayArrays[i][0];
                    result[i, 1] = byteArrayArrays[i][1];
                }

                return result;
            }

            public override void Update() {
                throw new NotImplementedException();
            }
        }
    }
}
