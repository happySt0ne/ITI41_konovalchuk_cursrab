using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class Wall : GameObject {
        public Wall(double x, double y, double width, double height) : base(x, y, width, height) {
            texture = Texture.LoadTexture(@"C:/labs/4sem/konovalchuk_cursrab/photo_2022-05-31_21-54-02.bmp");
        }
        
        public override void Draw() {
            GL.BindTexture(TextureTarget.Texture2D, texture.ID);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0);
            GL.Vertex2(x, y);
            GL.TexCoord2(1, 0);
            GL.Vertex2(x + width, y);
            GL.TexCoord2(1, 1);
            GL.Vertex2(x + width, y - height);
            GL.TexCoord2(0, 1);
            GL.Vertex2(x, y - height);
            GL.End();
        }

        public override void Update() {
            Draw();
        }
    }
}
