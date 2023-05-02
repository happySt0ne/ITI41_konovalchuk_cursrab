using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class Plane : GameObject {
        public static double PlaneLifetime { get; private set; }

        public Plane() : base(Constants.PLANE_X_COORDINATE_SPAWN, Constants.PLANE_Y_COORDINATE_SPAWN, 
                                                       Constants.PLANE_WIDTH, Constants.PLANE_HEIGHT) {

            Points = new List<(double, double)> { (x, y), (x + width, y), (x + width, y - height), (x, y - height) };
            texture = Texture.LoadTexture(Constants.PLANE_TEXTURE_PATH);
        }

        public void Move() {
            for (int i = 0; i < Points.Count; i++) {
                Points[i] = (Points[i].Item1 + Constants.PLANE_X_SPEED, Points[i].Item2);
            }
        }

        public override void Update() {
            PlaneLifetime += 0.025;
            Move();
            Draw();
        }
    }
}
