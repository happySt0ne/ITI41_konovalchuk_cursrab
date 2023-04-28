using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class Wall : GameObject {
        public Wall(double x, double y, double width, double height) : base(x, y, width, height) { }
        
        public override void Draw() {
            GL.PointSize(5);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(x, y);
            GL.Vertex2(x + width, y);
            GL.Vertex2(x + width, y - height);
            GL.Vertex2(x, y - height);
            GL.End();
        }

        public override void Update() {
            Draw();
        }
    }
}
