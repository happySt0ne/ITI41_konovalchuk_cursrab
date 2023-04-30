using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Game_Engine_Library {
    public class Panzar : GameObject {
        private double _speed;
        public bool touched = false;
        private sbyte _moveDirection;
        private List<(double, double)> _partsOfPanzar;
        const int MAX_COOLDOWN = 4;
        private double cooldown = 0;

        /// <summary>
        /// Боезапас танка.
        /// </summary>
        public int Ammo { get; private set; } = 2;

        /// <summary>
        /// Здоровье танка.
        /// </summary>
        public double Health { get; set; } = 100;

        /// <summary>
        /// Угол между дулом и осью Ох.
        /// </summary>
        public int MuzzleDirection { get; private set; } = 0;

        /// <summary>
        /// Начальная позиция полёта пули.
        /// </summary>
        public (double, double) BulletPosition {
            get {
                var muzzleStartPoint = (x + width, y - height / 6);
                var bazePoint = (x + width / 2, y - height / 4);
                (double, double) pointToReturn;

                switch (Side) {
                    case "left":
                        pointToReturn = GameMath.Rotate(bazePoint, muzzleStartPoint, MuzzleDirection);
                        return (pointToReturn.Item1 + 0.04, pointToReturn.Item2 + 0.04);
                    case "right":
                        pointToReturn = GameMath.Rotate(bazePoint, muzzleStartPoint, -MuzzleDirection);
                        return (pointToReturn.Item1 - 0.045, pointToReturn.Item2 + 0.04);
                    default:
                        return (0, 0);
                }
            }
        }

        /// <summary>
        /// Сделан ли выстрел танком.
        /// </summary>
        public bool Shooted { get; private set; } = false;

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

            if (Side == "right") {
                GameMath.Rotate(_partsOfPanzar, 8, 11, 180, (x + width / 2, y - height / 4));
                MuzzleDirection = 180;
            }
        }

        /// <summary>
        /// Отлавливание и реакция на действия игрока.
        /// </summary>
        private void Action() {
            KeyboardState keyboard = Keyboard.GetState();
            
            Move(keyboard);
            RotateMuzzle(keyboard);
            Shoot(keyboard);
        }

        /// <summary>
        /// Реализация логики выстрела.
        /// </summary>
        /// <param name="keyboard"></param>
        private void Shoot(KeyboardState keyboard) {
            if ((keyboard.IsKeyDown(Key.Space) && Side == "left" || keyboard.IsKeyDown(Key.Enter) && Side == "right") 
                                                                                        && cooldown <= 0 && Ammo > 0) {
                Shooted = true;
                cooldown = MAX_COOLDOWN;
                Ammo--;
            } else Shooted = false;
        }

        /// <summary>
        /// Реализация поворота дула танка.
        /// </summary>
        /// <param name="keyboard"></param>
        private void RotateMuzzle(KeyboardState keyboard) {
            if (keyboard.IsKeyDown(Key.W) && Side == "left" && MuzzleDirection < 90 ||
                keyboard.IsKeyDown(Key.Down) && Side == "right" && MuzzleDirection > 180) {
                GameMath.Rotate(_partsOfPanzar, 8, 11, 5, (x + width / 2, y - height / 4));
                MuzzleDirection += Side == "left" ? 5 : -5;
            }

            if (keyboard.IsKeyDown(Key.S) && Side == "left" && MuzzleDirection > 0 ||
                keyboard.IsKeyDown(Key.Up) && Side == "right" && MuzzleDirection < 270) {
                GameMath.Rotate(_partsOfPanzar, 8, 11, -5, (x + width / 2, y - height / 4));
                MuzzleDirection += Side == "left" ? -5 : 5;
            }
        }

        /// <summary>
    /// Реализация движения танка.
    /// </summary>
        private void Move(KeyboardState keyboard) {
            if (((keyboard.IsKeyDown(Key.A) && Side == "left") ||
                (keyboard.IsKeyDown(Key.Left) && Side == "right")) &&
                !(_moveDirection == -1 && touched)) {
                for (int i = 0; i < _partsOfPanzar.Count; i++) {
                    _partsOfPanzar[i] = (_partsOfPanzar[i].Item1 - _speed, _partsOfPanzar[i].Item2);
                }

                touched = false;
                _moveDirection = -1;
                x -= _speed;
            } 

            if (((keyboard.IsKeyDown(Key.D) && Side == "left") ||
                 (keyboard.IsKeyDown(Key.Right) && Side == "right")) &&
                 !(_moveDirection == 1 && touched)) {
                for (int i = 0; i < _partsOfPanzar.Count; i++) {
                    _partsOfPanzar[i] = (_partsOfPanzar[i].Item1 + _speed, _partsOfPanzar[i].Item2);
                }

                touched = false;
                _moveDirection = 1;
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
        /// Осуществление тика таймера кулдауна стрельбы.
        /// </summary>
        private void ReduceCooldown() {
            cooldown = Math.Round(cooldown, 3);

            if (cooldown >= 0.025) {
                cooldown -= 0.025;
            }
        }

        /// <summary>
        /// Обновление логики и перересовка танка.
        /// </summary>
        public override void Update() {
            Action();
            Draw();
            ReduceCooldown();
        }
    }
}
