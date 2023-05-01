using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    internal class PanzarTurret : GameObject {
        public List<(double, double)> TurretPoints { get; private set; }

        public PanzarTurret(double x, double y, double width, double height) : base(x, y, width, height) {
            TurretPoints = new List<(double, double)> { (x + width / 3, y),
                                                        (x + width / 3 * 2, y),
                                                        (x + width / 3 * 2, y - height / 2),
                                                        (x + width / 3, y - height / 2) };
        }

        public override void Draw() {
            GL.Begin(PrimitiveType.Quads);
            TurretPoints.ForEach(x => GL.Vertex2(x.Item1, x.Item2));
            GL.End();
        }

        public override void Update() { }
    }
}
