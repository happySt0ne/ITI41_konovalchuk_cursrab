using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class Bullet : GameObject {
        private double xSpeed = Constants.BULLETS_X_START_SPEED;
        private double ySpeed = Constants.BULLETS_Y_START_SPEED;
        private double flightAngle;

        public int Damage { get; private set; } = Constants.BULLET_DAMAGE;

        public Bullet(double x, double y, int angle, string side) : base(x, y, Constants.BULLETS_WIDTH, Constants.BULLETS_HEIGHT) {
            texture = Texture.LoadTexture(Constants.BULLET_TEXTURE_PATH);
            flightAngle = side == "left" ? angle * Math.PI / 180 : -angle * Math.PI / 180;
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

            for (int i = 0; i < Points.Count; i++) {
                x = Points[i].Item1;
                y = Points[i].Item2;

                y += ySpeed * Math.Sin(flightAngle); 
                x += xSpeed * Math.Cos(flightAngle);
                Points[i] = (x, y);
            }

            ySpeed += Constants.GRAVITY_SCALE;
            Collision.MoveCollisionBoxTo(Points[0].Item1, Points[0].Item2);
        }
    }
}
