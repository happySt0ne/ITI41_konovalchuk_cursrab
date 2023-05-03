using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class PanzarTurret : GameObject {
        public PanzarTurret(double x, double y, double width, double height, string side) : base(x, y, width, height) {
            texture = Texture.LoadTexture(Constants.PANZAR_TURRET_TEXTURE_PATH);

            if (side == "right") TextureHorizontalReflection();
        }

        public override void Update() { }
    }
}
