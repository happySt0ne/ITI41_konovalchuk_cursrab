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

        /// <summary>
        /// Создаёт танк.
        /// </summary>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координата Y</param>
        /// <param name="width">Ширина танка</param>
        /// <param name="height">Высота танка</param>
        /// <param name="side">Сторона сил</param>
        public Panzar(double x, double y, double width, double height, string side) : base(x, y, width, height) {
            _partsOfPanzar = new List<(double, double)> { (x, y),
                                                          (x + width, y),
                                                          (x + width, y - height),
                                                          (x, y - height) };
            _random = new Random();
            Side = side;
        }

        /// <summary>
        /// Реализация движения танка.
        /// </summary>
        public void Move(Keys key) { 
            switch (key) {
                case Keys.J: case Keys.A:
                    for (int i = 0; i < _partsOfPanzar.Count; i++) {
                        _partsOfPanzar[i] = (_partsOfPanzar[i].Item1 - 0.006, _partsOfPanzar[i].Item2);
                    }
                    break;
                
                case Keys.D: case Keys.L:
                    for (int i = 0; i < _partsOfPanzar.Count; i++) {
                        _partsOfPanzar[i] = (_partsOfPanzar[i].Item1 + 0.006, _partsOfPanzar[i].Item2);
                    }
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
