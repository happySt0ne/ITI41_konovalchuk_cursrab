using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class Wall : GameObject {
        public Wall(double x, double y, double width, double height) : base(x, y, width, height) {
            texture = Texture.LoadTexture(Constants.WALL_TEXTURE_PATH);
        }

        public override void Update() {
            Draw();
        }
    }
}
