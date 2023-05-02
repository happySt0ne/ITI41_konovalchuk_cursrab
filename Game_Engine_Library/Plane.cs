using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class Plane : GameObject {
        private List<(double, double)> _planePoints; 
        private (byte, byte)[] _texCoords;

        public static double PlaneLifetime { get; private set; }

        public Plane() : base(Constants.PLANE_X_COORDINATE_SPAWN, 
                              Constants.PLANE_Y_COORDINATE_SPAWN, 
                              Constants.PLANE_WIDTH, 
                              Constants.PLANE_HEIGHT) {

            _texCoords = new (byte, byte)[4] { (0, 0), (1, 0), (1, 1), (0, 1) };
            _planePoints = new List<(double, double)> { (x, y), (x + width, y), (x + width, y - height), (x, y - height) };
            texture = Texture.LoadTexture(Constants.PLANE_TEXTURE_PATH);
        }

        public override void Draw() {
            GL.BindTexture(TextureTarget.Texture2D, texture.ID);
            GL.Begin(PrimitiveType.Quads);

            for (int i = 0; i < _texCoords.Length; i++) {
                GL.TexCoord2(_texCoords[i].Item1, _texCoords[i].Item2);
                GL.Vertex2(_planePoints[i].Item1, _planePoints[i].Item2);
            }

            GL.End();
        }

        public void Move() {
            for (int i = 0; i < _planePoints.Count; i++) {
                _planePoints[i] = (_planePoints[i].Item1 + Constants.PLANE_X_SPEED, _planePoints[i].Item2);
            }
        }

        public override void Update() {
            PlaneLifetime += 0.025;
            Move();
            Draw();
        }
    }
}
