using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class Plane : GameObject {
        private static Random random = new Random(Guid.NewGuid().GetHashCode());
        private static double _maxPlaneLifetime = (Constants.PLANE_WIDTH * 2 + 2) / (Constants.PLANE_X_SPEED / 0.025);
        private static int s_moveDirection;
        private static double _currentPlaneLifetime;
        
        public static bool IsAlive {
            get {
                return _currentPlaneLifetime >= _maxPlaneLifetime ? false : true;
            }
        } 

        public Plane() : base(ChooseDirection() * Constants.PLANE_X_COORDINATE_SPAWN, Constants.PLANE_Y_COORDINATE_SPAWN, 
                                                                         Constants.PLANE_WIDTH, Constants.PLANE_HEIGHT) {
            if (s_moveDirection == -1) TextureHorizontalReflection();
            texture = Texture.LoadTexture(Constants.PLANE_TEXTURE_PATH);
            _currentPlaneLifetime = 0;
        }

        public static int ChooseDirection() {
            if (random.Next(0, 2) == 0) {
                s_moveDirection = -1;
                return -1;
            } else {
                s_moveDirection = 1;
                return 1;
            }
        }

        public void Move() {
            for (int i = 0; i < Points.Count; i++) {
                Points[i] = (Points[i].Item1 + Constants.PLANE_X_SPEED * s_moveDirection, Points[i].Item2);
            }
        }

        public override void Update() {
            _currentPlaneLifetime += 0.025;
            Move();
            Draw();
        }
    }
}
