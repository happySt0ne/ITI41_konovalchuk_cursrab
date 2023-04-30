using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    internal class Background : GameObject {
        public Background(double x, double y, double width, double height) : base(x, y, width, height) {
            texture = Texture.LoadTexture(@"../../../Game_Engine_Library/Resources/photo_2022-05-31_21-54-02.bmp");
        }

        public override void Draw() {
            GL.BindTexture(TextureTarget.Texture2D, texture.ID);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(-1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1.0f, -1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1.0f, 1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-1.0f, 1.0f);
            GL.End();
        }

        public override void Update() {
            Draw();
        }
    }
}
