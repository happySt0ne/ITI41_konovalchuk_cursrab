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
        const double MAX_COOLDOWN = 1;
        const int START_AMMO = 40;
        const int MUZZLE_ROTATION_SPEED = 5;
        private string _side;
        private (double, double) _rotateBazePoint;

        public List<(double, double)> MuzzlePoints { get; private set; }

        /// <summary>
        /// Перезарядка танка.
        /// </summary>
        public double Cooldown { get; private set; } = 0;

        /// <summary>
        /// Боезапас танка.
        /// </summary>
        public int Ammo { get; private set; } = START_AMMO;

        /// <summary>
        /// Угол между дулом и осью Ох.
        /// </summary>
        public int MuzzleDirection { get; private set; } = 0;

        /// <summary>
        /// Начальная позиция полёта пули.
        /// </summary>
        public (double, double) BulletPosition {
            get {
                (double, double) muzzleStartPoint = (MuzzlePoints[0].Item1 + width, MuzzlePoints[0].Item2 - height);
                (double, double) pointToReturn;

                switch (_side) {
                    case "left":
                        //muzzleStartPoint = ;
                        pointToReturn = GameMath.Rotate(_rotateBazePoint, muzzleStartPoint, MuzzleDirection);
                        return (pointToReturn.Item1 + 0.04, pointToReturn.Item2 + 0.04);
                    case "right":
                        //muzzleStartPoint = (x - width, y - height);
                        pointToReturn = GameMath.Rotate(_rotateBazePoint, muzzleStartPoint, -MuzzleDirection);
                        return (pointToReturn.Item1 - 0.06, pointToReturn.Item2 + 0.045);
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
            _rotateBazePoint = (x - width / 2 , y - height / 4);

            MuzzlePoints = new List<(double, double)> { (x, y),
                                                        (x + width, y),
                                                        (x + width, y - height),
                                                        (x , y - height), 
                                                        _rotateBazePoint };

            _side = side;

            if (_side == "right") {
                GameMath.Rotate(MuzzlePoints, 0, 4, 180, _rotateBazePoint);
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
                GameMath.Rotate(MuzzlePoints, 0, 4, MUZZLE_ROTATION_SPEED, _rotateBazePoint);
                MuzzleDirection += _side == "left" ? MUZZLE_ROTATION_SPEED : -MUZZLE_ROTATION_SPEED;
            }

            if (keyboard.IsKeyDown(Key.S) && _side == "left" && MuzzleDirection > 0 ||
                keyboard.IsKeyDown(Key.Up) && _side == "right" && MuzzleDirection < 270) {
                GameMath.Rotate(MuzzlePoints, 0, 4, -MUZZLE_ROTATION_SPEED, _rotateBazePoint);
                MuzzleDirection += _side == "left" ? -MUZZLE_ROTATION_SPEED : MUZZLE_ROTATION_SPEED;
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

        public override void Draw() {
            GL.Begin(PrimitiveType.Quads);
            MuzzlePoints.ForEach(x => GL.Vertex2(x.Item1, x.Item2));
            GL.End();
        }

        public override void Update() {
            _rotateBazePoint = MuzzlePoints[4];
            ReduceCooldown();
        }
    }
}
