using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class PanzarMuzzle : GameObject {
        private string _side;
        private (double, double) _rotateBazePoint;
        public double refreshCooldown;

        /// <summary>
        /// Перезарядка танка.
        /// </summary>
        public double Cooldown { get; set; } = 0;

        /// <summary>
        /// Боезапас танка.
        /// </summary>
        public int Ammo { get; set; } 

        /// <summary>
        /// Угол между дулом и осью Ох.
        /// </summary>
        public int MuzzleDirection { get; private set; } = 0;

        /// <summary>
        /// Сделан ли выстрел танком.
        /// </summary>
        public bool Shooted { get; private set; } = false;

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

        public PanzarMuzzle(double x, double y, double width, double height, string side) : base(x, y, width, height) {
            texture = Texture.LoadTexture(Constants.PANZAR_MUZZLE_TEXTURE_PATH);
            Ammo = Constants.START_AMMO;
            refreshCooldown = Constants.MAX_COOLDOWN;

            _rotateBazePoint = (x - width / 2 , y - height / 4);
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
                Cooldown = refreshCooldown;
                Ammo--;
            } else Shooted = false;
        }

        /// <summary>
        /// Реализация поворота дула танка.
        /// </summary>
        /// <param name="keyboard"></param>
        public void RotateMuzzle(KeyboardState keyboard) {
            if (keyboard.IsKeyDown(Key.W) && _side == "left" && MuzzleDirection < Constants.MUZZLE_MAX_ROTATE ||
                keyboard.IsKeyDown(Key.Down) && _side == "right" && MuzzleDirection > Constants.MUZZLE_MIN_ROTATE - 180) {
                GameMath.Rotate(Points, 0, 4, Constants.MUZZLE_ROTATION_SPEED, _rotateBazePoint);
                MuzzleDirection += _side == "left" ? Constants.MUZZLE_ROTATION_SPEED : -Constants.MUZZLE_ROTATION_SPEED;
            }

            if (keyboard.IsKeyDown(Key.S) && _side == "left" && MuzzleDirection > Constants.MUZZLE_MIN_ROTATE ||
                keyboard.IsKeyDown(Key.Up) && _side == "right" && MuzzleDirection < Constants.MUZZLE_MAX_ROTATE - 180) {
                GameMath.Rotate(Points, 0, 4, -Constants.MUZZLE_ROTATION_SPEED, _rotateBazePoint);
                MuzzleDirection += _side == "left" ? -Constants.MUZZLE_ROTATION_SPEED : Constants.MUZZLE_ROTATION_SPEED;
            }
        }

        /// <summary>
        /// Осуществление тика таймера кулдауна стрельбы.
        /// </summary>
        protected void ReduceCooldown() {
            Cooldown = Math.Round(Cooldown, 3);

            if (Cooldown >= Constants.TIMER_INTERVAL_SECONDS) {
                Cooldown -= Constants.TIMER_INTERVAL_SECONDS;
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
