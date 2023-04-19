using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Game_Engine_Library {
    public class Panzar : GameObject, IMovable {
        /// <summary>
        /// Список, который будет хранить координаты вершин частей танка.
        /// </summary>
        private List<(double, double)> _partsOfPanzar; 
        private Random _random;

        /// <summary>
        /// Сторона игрока.
        /// </summary>
        public String Side { get; private set; }

        public Panzar(double x, double y, double w, double h, string side) : base(x, y, w, h) {
            _partsOfPanzar = new List<(double, double)> { (0.0, 0.0),
                                                          (0.1, 0.0),
                                                          (0.1, 0.1),
                                                          (0.0, 0.1) };
            _random = new Random();
            Side = side;
        }

        /// <summary>
        /// Реализация движения танка.
        /// </summary>
        public void Move(Keys key) { 
            switch (key) {
                case Keys.Left:
                case Keys.A:
                    _partsOfPanzar.ForEach(x => x.Item1 -= 0.1);
                    break;
            }
        }

        /// <summary>
        /// Отрисовка конкретного танка.
        /// </summary>
        public override void Draw() {
            GL.PointSize(10);

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(_random.NextDouble(), _random.NextDouble(), _random.NextDouble());

            foreach ((double, double) point in _partsOfPanzar) {
                GL.Vertex2(point.Item1, point.Item2);
            }

            GL.End();
        }

        public override void Dispose() {
            throw new NotImplementedException();
        }
    }
}
