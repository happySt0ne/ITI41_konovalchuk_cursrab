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
        const int MAX_COOLDOWN = 3;
        private List<(double, double)> _muzzlePoints;
        private string _side;

        /// <summary>
        /// Перезарядка танка.
        /// </summary>
        public double Cooldown { get; private set; } = 0;

        /// <summary>
        /// Боезапас танка.
        /// </summary>
        public int Ammo { get; private set; } = 20;

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

                switch (_side) {
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

        public PanzarMuzzle(double x, double y, double width, double height, string side) : base(x, y, width, height) {
            _muzzlePoints = new List<(double, double)> { (x + width / 3 * 2, y - height / 6),
                                                         (x + width, y - height / 6),
                                                         (x + width, y - height / 3),
                                                         (x + width / 3 * 2, y - height / 3) };
            _side = side;
            if (_side == "right") {
                GameMath.Rotate(_muzzlePoints, 0, 4, 180, (x + width / 2, y - height / 4));
                MuzzleDirection = 180;
            }
        }

        /// <summary>
        /// Реализация логики выстрела.
        /// </summary>
        /// <param name="keyboard"></param>
        public void Shoot(KeyboardState keyboard) {
            if ((keyboard.IsKeyDown(Key.Space) && _side == "left" || keyboard.IsKeyDown(Key.Enter) && _side == "right")
                                                                                        && Cooldown <= 0 && Ammo > 0) {
                Shooted = true;
                Cooldown = MAX_COOLDOWN;
                Ammo--;
            } else Shooted = false;
        }

        /// <summary>
        /// Реализация поворота дула танка.
        /// </summary>
        /// <param name="keyboard"></param>
        public void RotateMuzzle(KeyboardState keyboard) {
            if (keyboard.IsKeyDown(Key.W) && _side == "left" && MuzzleDirection < 90 ||
                keyboard.IsKeyDown(Key.Down) && _side == "right" && MuzzleDirection > 180) {
                GameMath.Rotate(_muzzlePoints, 0, 4, 5, (x + width / 2, y - height / 4));
                MuzzleDirection += _side == "left" ? 5 : -5;
            }

            if (keyboard.IsKeyDown(Key.S) && _side == "left" && MuzzleDirection > 0 ||
                keyboard.IsKeyDown(Key.Up) && _side == "right" && MuzzleDirection < 270) {
                GameMath.Rotate(_muzzlePoints, 0, 4, -5, (x + width / 2, y - height / 4));
                MuzzleDirection += _side == "left" ? -5 : 5;
            }
        }

        /// <summary>
        /// Осуществление тика таймера кулдауна стрельбы.
        /// </summary>
        public void ReduceCooldown() {
            Cooldown = Math.Round(Cooldown, 3);

            if (Cooldown >= 0.025) {
                Cooldown -= 0.025;
            }
        }

        public override void Draw() {
            GL.Begin(PrimitiveType.Quads);
            _muzzlePoints.ForEach(x => GL.Vertex2(x.Item1, x.Item2));
            GL.End();
        }

        public override void Update() {
            Draw();
        }
    }
}
