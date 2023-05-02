using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    internal class PanzarTrack : GameObject{
        public bool touched = false;
        private double _speed;
        private sbyte _moveDirection;
        private string _side;
        //private (byte, byte)[] _texCoords;

        //public List<(double, double)> TrackPoints { get; private set; }

        public PanzarTrack(double x, double y, double width, double height, string side) : base(x, y, width, height) {
            _speed = Constants.PANZARS_SPEED;
            _side = side;
            texture = Texture.LoadTexture(Constants.PANZAR_TRACK_TEXTURE_PATH);
            Points = new List<(double, double)> { (x, y), (x + width, y), (x + width, y - height), (x, y - height) };

            if (side == "right") TextureHorizontalReflectoin();
        }

        /// <summary>
        /// Реализация движения танка.
        /// </summary>
        public void Move(KeyboardState keyboard, List<(double, double)> parts) {
            if (((keyboard.IsKeyDown(Key.A) && _side == "left") ||
                (keyboard.IsKeyDown(Key.Left) && _side == "right")) &&
                !(_moveDirection == -1 && touched)) {
                for (int i = 0; i < parts.Count; i++) {
                    parts[i] = (parts[i].Item1 - _speed, parts[i].Item2);
                }

                touched = false;
                _moveDirection = -1;
            }

            if (((keyboard.IsKeyDown(Key.D) && _side == "left") ||
                 (keyboard.IsKeyDown(Key.Right) && _side == "right")) &&
                 !(_moveDirection == 1 && touched)) {
                for (int i = 0; i < parts.Count; i++) {
                    parts[i] = (parts[i].Item1 + _speed, parts[i].Item2);
                }

                touched = false;
                _moveDirection = 1;
            }
        }

        public override void Update() { }
    }
}
