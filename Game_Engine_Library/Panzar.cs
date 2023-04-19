using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Game_Engine_Library {
    public class Panzar : GameObject, IMovable {
        /// <summary>
        /// Список, который будет хранить координаты вершин частей танка.
        /// </summary>
        private List<(double, double)> _partsOfPanzar; 
        private Random _random;

        public Panzar(double x, double y, double w, double h) : base(x, y, w, h) {
            _partsOfPanzar = new List<(double, double)> { (0.0, 0.0),
                                                          (0.1, 0.0),
                                                          (0.1, 0.1),
                                                          (0.0, 0.1) };
            _random = new Random();
        }

        public void Draw() {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.PointSize(10);
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(_random.NextDouble(), _random.NextDouble(), _random.NextDouble());

            foreach ((double, double) point in _partsOfPanzar) {
                GL.Vertex2(point.Item1, point.Item2);
            }

            GL.End();
        } 
    }
}
