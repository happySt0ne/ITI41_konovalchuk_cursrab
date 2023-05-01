using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    internal class PanzarTrack : GameObject{
        private double _speed;
        public bool touched = false;
        private sbyte _moveDirection;
        private List<(double, double)> _trackPoints;
        private string _side;

        public PanzarTrack(double x, double y, double width, double height, double speed, string side) : base(x, y, width, height) {
            _speed = speed;
            _side = side;
            _trackPoints = new List<(double, double)> { (x, y),
                                                        (x + width, y),
                                                        (x + width, y - height),
                                                        (x, y - height) };
        }

        /// <summary>
        /// Реализация движения танка.
        /// </summary>
        public void Move(KeyboardState keyboard) {
            if (((keyboard.IsKeyDown(Key.A) && _side == "left") ||
                (keyboard.IsKeyDown(Key.Left) && _side == "right")) &&
                !(_moveDirection == -1 && touched)) {
                for (int i = 0; i < _trackPoints.Count; i++) {
                    _trackPoints[i] = (_trackPoints[i].Item1 - _speed, _trackPoints[i].Item2);
                }

                touched = false;
                _moveDirection = -1;
                x -= _speed;
            }

            if (((keyboard.IsKeyDown(Key.D) && _side == "left") ||
                 (keyboard.IsKeyDown(Key.Right) && _side == "right")) &&
                 !(_moveDirection == 1 && touched)) {
                for (int i = 0; i < _trackPoints.Count; i++) {
                    _trackPoints[i] = (_trackPoints[i].Item1 + _speed, _trackPoints[i].Item2);
                }

                touched = false;
                _moveDirection = 1;
                x += _speed;
            }

            // После того, как танк подвинулся, следует подвинуть и его collision box.
            Collision.MoveCollisionBoxTo(x, y);
        }


        public override void Draw() {
            GL.Begin(PrimitiveType.Quads);
            _trackPoints.ForEach(x => GL.Vertex2(x.Item1, x.Item2));
            GL.End();
        }

        public override void Update() {
            Draw();
        }
    }
}
