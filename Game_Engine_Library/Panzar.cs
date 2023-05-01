using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Game_Engine_Library {
    public class Panzar : GameObject {
        private PanzarTrack _panzarTrack;
        private PanzarMuzzle _panzarMuzzle;
        private PanzarTurret _panzarTurret;

        /// <summary>
        /// Здоровье танка.
        /// </summary>
        public double Health { get; set; } = 100;

        /// <summary>
        /// Сторона игрока.
        /// </summary>
        public String Side { get; private set; }
        public int Ammo { get => _panzarMuzzle.Ammo; }
        public double Cooldown { get => _panzarMuzzle.Cooldown; }
        public bool Touched { get => _panzarTrack.touched; set => _panzarTrack.touched = value; }
        public bool Shooted { get => _panzarMuzzle.Shooted; }
        public (double, double) BulletPosition { get => _panzarMuzzle.BulletPosition; }
        public int MuzzleDirection { get => _panzarMuzzle.MuzzleDirection; }

        /// <summary>
        /// Создаёт танк.
        /// </summary>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координата Y</param>
        /// <param name="width">Ширина танка</param>
        /// <param name="height">Высота танка</param>
        /// <param name="side">Сторона сил</param>
        public Panzar(double x, double y, string side, double width = 0.2, double height = 0.2, double speed = 0.005)
                                                                                             : base(x, y, width, height) {
            _panzarMuzzle = new PanzarMuzzle(x + width / 3 * 2, y - height / 6, width / 3, height / 6, side);
            _panzarTrack = new PanzarTrack(x, y - height / 2, width, height / 2, speed, side);
            _panzarTurret = new PanzarTurret(x, y, width, height);
            Side = side;
        }

        /// <summary>
        /// Отлавливание и реакция на действия игрока.
        /// </summary>
        private void Action() {
            KeyboardState keyboard = Keyboard.GetState();
            
            Move(keyboard);
            _panzarMuzzle.RotateMuzzle(keyboard);
            _panzarMuzzle.Shoot(keyboard);
        }

        private void Move(KeyboardState keyboard) {
            _panzarTrack.Move(keyboard, _panzarTrack.TrackPoints);
            _panzarTrack.Move(keyboard, _panzarMuzzle.MuzzlePoints);
            _panzarTrack.Move(keyboard, _panzarTurret.TurretPoints);
        }
        
        /// <summary>
        /// Отрисовка конкретного танка.
        /// </summary>
        public override void Draw() {
            _panzarTrack.Draw();
            _panzarTurret.Draw();
            _panzarMuzzle.Draw();
        }

        /// <summary>
        /// Обновление логики и перересовка танка.
        /// </summary>
        public override void Update() {
            Action();
            Draw();
            _panzarTrack.Update();
            _panzarMuzzle.Update();
            _panzarTurret.Update();
        }
    }
}
