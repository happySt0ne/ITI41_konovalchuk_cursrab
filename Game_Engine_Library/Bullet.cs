using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class Bullet : GameObject, IDisposable {
        List<(double, double)> bulletPoints = new List<(double, double)>();
        double speed;
        double flightAngle;
        double g = 0.0001;

        public Bullet(double x, double y, int angle, double width = 0.02, double height = 0.02, double speed = 0.09) 
                                                                            : base(x, y, width, height) {
            bulletPoints.Add((x, y));
            bulletPoints.Add((x + width, y));
            bulletPoints.Add((x + width, y - height));
            bulletPoints.Add((x, y - height));

            this.speed = speed;
            flightAngle = angle * Math.PI / 180;
        }

        public override void Draw() {
            GL.PointSize(1);
            GL.Color3(0.2, 0.2, 0.2);
            GL.Begin(PrimitiveType.Quads);
            bulletPoints.ForEach(x => GL.Vertex2(x.Item1, x.Item2));
            GL.End();
        }

        public override void Update() {
            MoveBullet();
            Draw();
        }

        public void MoveBullet () {
            double x;
            double y;
            double delta_x;
            double delta_y;
            for (int i = 0; i < bulletPoints.Count; i++) {
                x = bulletPoints[i].Item1;
                y = bulletPoints[i].Item2;

                y += (speed + g) * Math.Sin(flightAngle);
                x += delta_x = speed * Math.Cos(flightAngle);
                bulletPoints[i] = (x, y);
            }

            g -= 0.008;
            Collision.MoveCollisionBoxTo(bulletPoints[0].Item1, bulletPoints[0].Item2);
        }

        public void Dispose() {
            throw new Exception();
        }
    }
}
