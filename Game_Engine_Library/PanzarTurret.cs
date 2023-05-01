using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    internal class PanzarTurret : GameObject {
        public List<(double, double)> TurretPoints { get; private set; }
        private (byte, byte)[] _texCoords;

        public PanzarTurret(double x, double y, double width, double height, string side) : base(x, y, width, height) {
            texture = Texture.LoadTexture(@"../../../Game_Engine_Library/Resources/PanzarTurret.bmp");
            
            _texCoords = side == "left" ? new (byte, byte)[4] { (0, 0), (1, 0), (1, 1), (0, 1) }  
                                        : new (byte, byte)[4] { (1, 0), (0, 0), (0, 1), (1, 1) };

            TurretPoints = new List<(double, double)> { (x, y), (x + width, y), (x + width, y - height), (x, y - height) };
        }

        /// <summary>
        /// Отрисовка башни танка.
        /// </summary>
        public override void Draw() {
            GL.BindTexture(TextureTarget.Texture2D, texture.ID);
            GL.Begin(PrimitiveType.Quads);
            
            for (int i = 0; i < _texCoords.Length; i++) {
                GL.TexCoord2(_texCoords[i].Item1, _texCoords[i].Item2);
                GL.Vertex2(TurretPoints[i].Item1, TurretPoints[i].Item2);
            }
            
            GL.End();
        }

        public override void Update() { }
    }
}
