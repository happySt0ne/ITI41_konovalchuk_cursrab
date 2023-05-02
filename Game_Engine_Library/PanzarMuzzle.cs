using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    internal class PanzarMuzzle : GameObject {
        
        private string _side;
        private (double, double) _rotateBazePoint;
        
        /// <summary>
        /// Перезарядка танка.
        /// </summary>
        public double Cooldown { get; private set; } = 0;

        /// <summary>
        /// Боезапас танка.
        /// </summary>
        public int Ammo { get; set; } = Constants.START_AMMO;

        /// <summary>
        /// Угол между дулом и осью Ох.
        /// </summary>
        public int MuzzleDirection { get; private set; } = 0;

        /// <summary>
        /// Начальная позиция полёта пули.
        /// </summary>
        public (double, double) BulletPosition {
            get {
                var bulletDiagonalLength = GameMath.FindHypotenuse(Constants.BULLETS_WIDTH, Constants.BULLETS_HEIGHT);
                return _side == "left" ? (Points[1].Item1 + bulletDiagonalLength, Points[1].Item2 + bulletDiagonalLength)
                                       : (Points[3].Item1 - bulletDiagonalLength, Points[3].Item2 + bulletDiagonalLength); ;
            }
        }

        /// <summary>
        /// Сделан ли выстрел танком.
        /// </summary>
        public bool Shooted { get; private set; } = false;

        public PanzarMuzzle(double x, double y, double width, double height, string side) : base(x, y, width, height) {
            _rotateBazePoint = (x - width / 2 , y - height / 4);
            texture = Texture.LoadTexture(Constants.PANZAR_MUZZLE_TEXTURE_PATH);

            _side = side;

            if (_side == "right") {
                Points = new List<(double, double)> { (x - 2 * width, y),
                                                            (x - width, y),
                                                            (x - width, y - height),
                                                            (x - 2 * width, y - height) };
                TextureHorizontalReflection();
                MuzzleDirection = -180;
            }

            Points.Add(_rotateBazePoint);
        }

        /// <summary>
        /// Реализация логики выстрела.
        /// </summary>
        /// <param name="keyboard"></param>
        public void Shoot(KeyboardState keyboard) {
            if ((keyboard.IsKeyDown(Key.Space) && _side == "left" || keyboard.IsKeyDown(Key.Enter) && _side == "right")
                                                                                        && Cooldown <= 0 && Ammo > 0) {
                Shooted = true;
                Cooldown = Constants.MAX_COOLDOWN;
                Ammo--;
            } else Shooted = false;
        }

        /// <summary>
        /// Реализация поворота дула танка.
        /// </summary>
        /// <param name="keyboard"></param>
        public void RotateMuzzle(KeyboardState keyboard) {
            if (keyboard.IsKeyDown(Key.W) && _side == "left" && MuzzleDirection < 90 ||
                keyboard.IsKeyDown(Key.Down) && _side == "right" && MuzzleDirection > -180) {
                GameMath.Rotate(Points, 0, 4, Constants.MUZZLE_ROTATION_SPEED, _rotateBazePoint);
                MuzzleDirection += _side == "left" ? Constants.MUZZLE_ROTATION_SPEED : -Constants.MUZZLE_ROTATION_SPEED;
            }

            if (keyboard.IsKeyDown(Key.S) && _side == "left" && MuzzleDirection > 0 ||
                keyboard.IsKeyDown(Key.Up) && _side == "right" && MuzzleDirection < -90) {
                GameMath.Rotate(Points, 0, 4, -Constants.MUZZLE_ROTATION_SPEED, _rotateBazePoint);
                MuzzleDirection += _side == "left" ? -Constants.MUZZLE_ROTATION_SPEED : Constants.MUZZLE_ROTATION_SPEED;
            }
        }

        /// <summary>
        /// Осуществление тика таймера кулдауна стрельбы.
        /// </summary>
        private void ReduceCooldown() {
            Cooldown = Math.Round(Cooldown, 3);

            if (Cooldown >= 0.025) {
                Cooldown -= 0.025;
            }
        }

        /// <summary>
        /// Обновление логики дула танка.
        /// </summary>
        public override void Update() {
            _rotateBazePoint = Points[4];
            ReduceCooldown();
        }
    }
}
