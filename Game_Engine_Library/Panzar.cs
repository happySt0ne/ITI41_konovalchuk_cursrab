using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Game_Engine_Library {
    public class Panzar : GameObject {
        protected PanzarTrack _panzarTrack;
        public PanzarMuzzle _panzarMuzzle;
        protected PanzarTurret _panzarTurret;
        
        #region Properties
        /// <summary>
        /// Здоровье танка.
        /// </summary>
        public double Health { get; set; } = 100;

        /// <summary>
        /// Сторона игрока.
        /// </summary>
        public String Side { get; private set; }

        /// <summary>
        /// Боезапас танка.
        /// </summary>
        public int Ammo { get => _panzarMuzzle.Ammo; set => _panzarMuzzle.Ammo = value;  }

        /// <summary>
        /// Текущая перезарядка до следующего выстрела.
        /// </summary>
        public double Cooldown { get => _panzarMuzzle.Cooldown; }
        
        /// <summary>
        /// Нужно ли запретить танку движение в том же направлении.
        /// </summary>
        public bool Touched { get => _panzarTrack.touched; set => _panzarTrack.touched = value; }

        /// <summary>
        /// Сделал ли танк выстрел.
        /// </summary>
        public bool Shooted { get => _panzarMuzzle.Shooted; }

        /// <summary>
        /// Точка для создания пули при выстреле.
        /// </summary>
        public (double, double) BulletPosition { get => _panzarMuzzle.BulletPosition; }

        /// <summary>
        /// Угол наклона дула танка.
        /// </summary>
        public int MuzzleDirection { get => _panzarMuzzle.MuzzleDirection; }
        #endregion

        /// <summary>
        /// Создаёт танк.
        /// </summary>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координата Y</param>
        /// <param name="width">Ширина танка</param>
        /// <param name="height">Высота танка</param>
        /// <param name="side">Сторона сил</param>
        public Panzar(string side) : base(side == "left" ? -Constants.PANZAR_X_COORDINATE_SPAWNPOINT - Constants.PANZARS_WIDTH
                                                         : Constants.PANZAR_X_COORDINATE_SPAWNPOINT, 
                                                           Constants.HEIGHT_TO_CREATE_PANZARS, 
                                                           Constants.PANZARS_WIDTH, 
                                                           Constants.PANZARS_HEIGHT) {
            _panzarMuzzle = new PanzarMuzzle(x + width / 3 * 2, y - height / 4, width / 3, height / 6, side);
            _panzarTrack = new PanzarTrack(x, y - height / 2, width, height / 2, side);
            _panzarTurret = new PanzarTurret(x + width / 3, y, width / 3, height / 2, side);
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

        /// <summary>
        /// Передвижение всех частей танка.
        /// </summary>
        /// <param name="keyboard"></param>
        private void Move(KeyboardState keyboard) {
            _panzarTrack.Move(keyboard, _panzarTrack.Points);
            _panzarTrack.Move(keyboard, _panzarMuzzle.Points);
            _panzarTrack.Move(keyboard, _panzarTurret.Points);

            Collision.MoveCollisionBoxTo(_panzarTrack.Points[0].Item1, y);
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
