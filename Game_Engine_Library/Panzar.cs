using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Game_Engine_Library {
    public class Panzar : GameObject, IMovable {
        private double _speed;

        /// <summary>
        /// Список, который будет хранить координаты вершин частей танка.
        /// </summary>
        private List<(double, double)> _partsOfPanzar;

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
        public Panzar(double x, double y, double width, double height, string side, double speed = 0.005)
                                                          /* Координаты кузова. */  : base(x, y, width, height) {
            _partsOfPanzar = new List<(double, double)> { (x, y - height/2),
                                                          (x + width, y - height/2),
                                                          (x + width, y - height),
                                                          (x, y - height),

                                                          // Координаты точек башни.
                                                          (x + width/3, y),
                                                          (x + width/3*2, y),
                                                          (x + width/3*2, y - height/2),
                                                          (x + width/3, y - height/2),

                                                          // Координаты точек дула.
                                                          (x + width/3*2, y - height/6),
                                                          (x + width, y - height/6),
                                                          (x + width, y - height/3),
                                                          (x + width/3*2, y - height/3) };
            Side = side;
            _speed = speed;
        }

        /// <summary>
        /// Отлавливание и реакция на действия игрока.
        /// </summary>
        public void Action() {
            KeyboardState keyboard = Keyboard.GetState();
            
            Move(keyboard);
            RotateMuzzle(keyboard);
        }

        /// <summary>
        /// Реализация поворота дула танка.
        /// </summary>
        /// <param name="keyboard"></param>
        private void RotateMuzzle(KeyboardState keyboard) {
            if (keyboard.IsKeyDown(Key.W) && Side == "left" ||
                keyboard.IsKeyDown(Key.Up) && Side == "right") {
                GameMath.Rotate(_partsOfPanzar, 8, 11, 20, (x + width / 2, y - height / 4));
            }

            if (keyboard.IsKeyDown(Key.S) && Side == "left" ||
                keyboard.IsKeyDown(Key.Down) && Side == "right") {
                GameMath.Rotate(_partsOfPanzar, 8, 11, -20, (x + width / 2, y - height / 4));
            }
        }

        /// <summary>
        /// Реализация движения танка.
        /// </summary>
        private void Move(KeyboardState keyboard) {
            if ((keyboard.IsKeyDown(Key.A) && Side == "left") ||
                 (keyboard.IsKeyDown(Key.Left) && Side == "right")) {
                for (int i = 0; i < _partsOfPanzar.Count; i++) {
                    _partsOfPanzar[i] = (_partsOfPanzar[i].Item1 - _speed, _partsOfPanzar[i].Item2);
                }

                x -= _speed;
            }

            if ((keyboard.IsKeyDown(Key.D) && Side == "left") ||
                 (keyboard.IsKeyDown(Key.Right) && Side == "right")) {
                for (int i = 0; i < _partsOfPanzar.Count; i++) {
                    _partsOfPanzar[i] = (_partsOfPanzar[i].Item1 + _speed, _partsOfPanzar[i].Item2);
                }

                x += _speed;
            }
        }

        /// <summary>
        /// Отрисовка конкретного танка.
        /// </summary>
        public override void Draw() {
            GL.PointSize(5);
            
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(0.255, 0, 0);

            foreach ((double, double) point in _partsOfPanzar) {
                GL.Vertex2(point.Item1, point.Item2);
            }

            GL.End();

            // Точка, вокруг которой должно вращатся дуло танка.
            GL.Color3(0, 0.94, 0.255);
            GL.Begin(PrimitiveType.Points);
            GL.Vertex2(x + width / 2, y - height / 4);
            GL.End();

        }

        /// <summary>
        /// Обновление логики и перересовка танка.
        /// </summary>
        public override void Update() {
            Action();
            Draw();
        }
    }
}
