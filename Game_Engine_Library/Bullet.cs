using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class Bullet : GameObject {
        List<(double, double)> bulletPoints = new List<(double, double)>();
        double speed;
        double flightAngle;
        double g = 0.0001;

        public int Damage { get; private set; }

        public Bullet(double x, double y, int angle, string side, int damage, double width = 0.04, double height = 0.04, double speed = 0.07) 
                                                                                                   : base(x, y, width, height) {
            bulletPoints.Add((x, y));
            bulletPoints.Add((x + width, y));
            bulletPoints.Add((x + width, y - height));
            bulletPoints.Add((x, y - height));

            this.speed = speed;
            flightAngle = side == "left" ? angle * Math.PI / 180 : -angle * Math.PI / 180;
            Damage = damage;
        }

        /// <summary>
        /// Отрисовка пули.
        /// </summary>
        public override void Draw() {
            GL.PointSize(1);
            GL.Color3(0.2, 0.2, 0.2);
            GL.Begin(PrimitiveType.Quads);
            bulletPoints.ForEach(x => GL.Vertex2(x.Item1, x.Item2));
            GL.End();
        }

        /// <summary>
        /// Обновление логики и перерисовка пули.
        /// </summary>
        public override void Update() {
            MoveBullet();
            Draw();
        }

        public void Explode() {

        }

        /// <summary>
        /// Передвижение пули.
        /// </summary>
        public void MoveBullet () {
            double x;
            double y;

            for (int i = 0; i < bulletPoints.Count; i++) {
                x = bulletPoints[i].Item1;
                y = bulletPoints[i].Item2;

                y += (speed + g) * Math.Sin(flightAngle);
                x += speed * Math.Cos(flightAngle);
                bulletPoints[i] = (x, y);
            }

            g -= 0.006;
            Collision.MoveCollisionBoxTo(bulletPoints[0].Item1, bulletPoints[0].Item2);
        }
    }
}
