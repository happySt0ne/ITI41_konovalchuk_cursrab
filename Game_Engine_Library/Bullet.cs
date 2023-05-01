using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class Bullet : GameObject {
        private List<(double, double)> bulletPoints = new List<(double, double)>();
        private double xSpeed = Constants.BULLETS_X_START_SPEED;
        private double ySpeed = Constants.BULLETS_Y_START_SPEED;
        private double flightAngle;

        public int Damage { get; private set; }

        public Bullet(double x, double y, int angle, string side) : base(x, y, Constants.BULLETS_WIDTH, Constants.BULLETS_HEIGHT) {
            bulletPoints.Add((x, y));
            bulletPoints.Add((x + width, y));
            bulletPoints.Add((x + width, y - height));
            bulletPoints.Add((x, y - height));
            
            xSpeed = Constants.BULLETS_X_START_SPEED;
            flightAngle = side == "left" ? angle * Math.PI / 180 : -angle * Math.PI / 180;
            Damage = Constants.BULLET_DAMAGE;
            texture = Texture.LoadTexture(Constants.BULLET_TEXTURE_PATH);
        }

        /// <summary>
        /// Отрисовка пули.
        /// </summary>
        public override void Draw() {
            GL.BindTexture(TextureTarget.Texture2D, texture.ID);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0); GL.Vertex2(bulletPoints[0].Item1, bulletPoints[0].Item2);
            GL.TexCoord2(1, 0); GL.Vertex2(bulletPoints[1].Item1, bulletPoints[1].Item2);
            GL.TexCoord2(1, 1); GL.Vertex2(bulletPoints[2].Item1, bulletPoints[2].Item2);
            GL.TexCoord2(0, 1); GL.Vertex2(bulletPoints[3].Item1, bulletPoints[3].Item2);

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

                y += ySpeed * Math.Sin(flightAngle); 
                x += xSpeed * Math.Cos(flightAngle);
                bulletPoints[i] = (x, y);
            }

            ySpeed += Constants.GRAVITY_SCALE;
            Collision.MoveCollisionBoxTo(bulletPoints[0].Item1, bulletPoints[0].Item2);
        }
    }
}
