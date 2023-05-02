using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public abstract class GameObject : IDrawable {
        protected double x, y, width, height;
        private (byte, byte)[] _texCoords = new (byte, byte)[4] { (0, 0), (1, 0), (1, 1), (0, 1) };
        protected Texture texture;
        public List<(double, double)> Points { get; protected set; }

        public Collision Collision { get; protected set; }

        public GameObject(double x, double y, double width, double height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            Collision = new Collision(x, y, width, height);

            Points = new List<(double, double)> { (x, y), (x + width, y), (x + width, y - height), (x, y - height) };
        }

        public GameObject() { }

        public void TextureHorizontalReflection() => 
            _texCoords = new (byte, byte)[4] { (1, 0), (0, 0), (0, 1), (1, 1) };
    
        public virtual void Draw() {
            GL.BindTexture(TextureTarget.Texture2D, texture.ID);
            GL.Begin(PrimitiveType.Quads);

            for (int i = 0; i < _texCoords.Length; i++) {
                GL.TexCoord2(_texCoords[i].Item1, _texCoords[i].Item2);
                GL.Vertex2(Points[i].Item1, Points[i].Item2);
            }

            GL.End();
        }

        public abstract void Update();
    }
}
