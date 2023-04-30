using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using static OpenTK.Graphics.OpenGL.GL;

namespace Game_Engine_Library {
    public class Panzar : GameObject, IMovable {
        private double _speed;
        public bool touched;

        /// <summary>
        /// Направление, куда двигается танк. false - влево, true - вправо.
        /// </summary>
        private sbyte _direction;

        /// <summary>
        /// Список, который будет хранить координаты вершин частей танка.
        /// </summary>
        private List<(double, double)> _partsOfPanzar;

        public int _muzzleDirection { get; private set; }

        public (double, double) bulletPosition {
            get {
                var muzzleStartPoint = (x + width, y - height / 6);
                var bazePoint = (x + width / 2, y - height / 4);
                (double, double) pointToReturn;

                switch (Side) {
                    case "left":
                        pointToReturn = GameMath.Rotate(bazePoint, muzzleStartPoint, _muzzleDirection);
                        return (pointToReturn.Item1 + 0.02, pointToReturn.Item2 + 0.01);
                    case "right":
                        pointToReturn = GameMath.Rotate(bazePoint, muzzleStartPoint, -_muzzleDirection);
                        return (pointToReturn.Item1 - 0.03, pointToReturn.Item2 + 0.02);
                    default:
                        return (0, 0);
                }
            }
        }

        public bool Shooted { get; private set; }

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
        public Panzar(double x, double y, string side, double width = 0.2, double height = 0.2, double speed = 0.005)
                                                          /* Координаты кузова. */      : base(x, y, width, height) {
            _partsOfPanzar = new List<(double, double)> { (x, y - height / 2),
                                                          (x + width, y - height / 2),
                                                          (x + width, y - height),
                                                          (x, y - height),

                                                          // Координаты точек башни.
                                                          (x + width / 3, y),
                                                          (x + width / 3 * 2, y),
                                                          (x + width / 3 * 2, y - height / 2),
                                                          (x + width / 3, y - height / 2),

                                                          // Координаты точек дула.
                                                          (x + width / 3 * 2, y - height / 6),
                                                          (x + width, y - height / 6),
                                                          (x + width, y - height / 3),
                                                          (x + width / 3 * 2, y - height / 3) };
            Side = side;
            _speed = speed;
            touched = false;
            Shooted = false;
            _muzzleDirection = 0;

            if (Side == "right") {
                GameMath.Rotate(_partsOfPanzar, 8, 11, 180, (x + width / 2, y - height / 4));
                _muzzleDirection = 180;
            }
        }

        /// <summary>
        /// Отлавливание и реакция на действия игрока.
        /// </summary>
        public void Action() {
            KeyboardState keyboard = Keyboard.GetState();
            
            Move(keyboard);
            RotateMuzzle(keyboard);
            Shoot(keyboard);
        }

        public void Shoot(KeyboardState keyboard) {
            if (keyboard.IsKeyDown(Key.Space) && Side == "left" || 
                keyboard.IsKeyDown(Key.Enter) && Side == "right") {
                Shooted = true;
                // Сюда ещё докинешь проверку перезарядки, отнятие патрона.
            } else Shooted = false;
        }

        /// <summary>
        /// Реализация поворота дула танка.
        /// </summary>
        /// <param name="keyboard"></param>
        private void RotateMuzzle(KeyboardState keyboard) {
            if (keyboard.IsKeyDown(Key.W) && Side == "left" && _muzzleDirection < 90 ||
                keyboard.IsKeyDown(Key.Down) && Side == "right" && _muzzleDirection > 180) {
                GameMath.Rotate(_partsOfPanzar, 8, 11, 5, (x + width / 2, y - height / 4));
                _muzzleDirection += Side == "left" ? 5 : -5;
            }

            if (keyboard.IsKeyDown(Key.S) && Side == "left" && _muzzleDirection > 0 ||
                keyboard.IsKeyDown(Key.Up) && Side == "right" && _muzzleDirection < 270) {
                GameMath.Rotate(_partsOfPanzar, 8, 11, -5, (x + width / 2, y - height / 4));
                _muzzleDirection += Side == "left" ? -5 : 5;
            }
        }

        /// <summary>
        /// Реализация движения танка.
        /// </summary>
        private void Move(KeyboardState keyboard) {
            if (((keyboard.IsKeyDown(Key.A) && Side == "left") ||
                (keyboard.IsKeyDown(Key.Left) && Side == "right")) && 
                !(_direction == -1 && touched)) {
                for (int i = 0; i < _partsOfPanzar.Count; i++) {
                    _partsOfPanzar[i] = (_partsOfPanzar[i].Item1 - _speed, _partsOfPanzar[i].Item2);
                }

                touched = false;
                _direction = -1;
                x -= _speed;
            }

            if (((keyboard.IsKeyDown(Key.D) && Side == "left") ||
                 (keyboard.IsKeyDown(Key.Right) && Side == "right")) &&
                 !(_direction == 1 && touched)) {
                for (int i = 0; i < _partsOfPanzar.Count; i++) {
                    _partsOfPanzar[i] = (_partsOfPanzar[i].Item1 + _speed, _partsOfPanzar[i].Item2);
                }

                touched = false;
                _direction = 1;
                x += _speed;
            }

            // После того, как танк подвинулся, следует подвинуть и его collision box.
            Collision.MoveCollisionBoxTo(x, y);
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
