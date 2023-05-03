using Game_Engine_Library.Bonuses;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class Plane : GameObject {
        private static Random s_random = new Random(Guid.NewGuid().GetHashCode());
        private static double s_maxPlaneLifetime;
        private static int s_moveDirection;
        private static int s_randomNumber;
        public static double s_currentPlaneLifetime;

        public static bool Dropped { get; private set; }

        public override bool OutsideTheWindow {
            get {
                if (s_moveDirection == 1 && Points[0].Item1 > 1) return true;
                if (s_moveDirection == -1 && Points[1].Item1 < -1) return true;
                return false;
            }
        }

        public Plane() : base(ChooseDirection() == 1 ? Constants.PLANE_X_COORDINATE_SPAWN
                                                   : -(Constants.PLANE_X_COORDINATE_SPAWN + Constants.PLANE_WIDTH), 
                                                       Constants.PLANE_Y_COORDINATE_SPAWN, 
                                                       Constants.PLANE_WIDTH, Constants.PLANE_HEIGHT) {
            if (s_moveDirection == -1) TextureHorizontalReflection();
            texture = Texture.LoadTexture(Constants.PLANE_TEXTURE_PATH);
            s_currentPlaneLifetime = 0;
            Dropped = false;
            s_randomNumber = s_random.Next(Constants.CHANCE_TO_CREATE_BONUS_PER_FRAME);
            s_maxPlaneLifetime = (Constants.PLANE_WIDTH * 2 + 2) / (Constants.PLANE_X_SPEED / Constants.TIMER_INTERVAL_SECONDS);
        }

        static Plane() {
            s_maxPlaneLifetime = (Constants.PLANE_WIDTH * 2 + 2) / (Constants.PLANE_X_SPEED / Constants.TIMER_INTERVAL_SECONDS);
            s_currentPlaneLifetime = s_maxPlaneLifetime;
            Dropped = false;
        }

        /// <summary>
        /// Случайным образом выбирает с какой стороны появится.
        /// </summary>
        /// <returns> -1 если появляется справа и двигается влево, 1 в ином случае.</returns>
        public static int ChooseDirection() {
            if (s_random.Next(0, 2) == 0) {
                s_moveDirection = -1;
                return -1;
            } else {
                s_moveDirection = 1;
                return 1;
            }
        }

        /// <summary>
        /// Передвигает самолёт
        /// </summary>
        public void Move() {
            for (int i = 0; i < Points.Count; i++) {
                Points[i] = (Points[i].Item1 + Constants.PLANE_X_SPEED * s_moveDirection, Points[i].Item2);
            }
        }

        public static Bonus DropBonus(double x, double y) {
            if (s_random.Next(Constants.CHANCE_TO_CREATE_BONUS_PER_FRAME) == s_randomNumber) {
                Dropped = true;
                return BonusCreator.CreateRandomBonus(x, y);
            }
            return null;
        }

        /// <summary>
        /// Обновляет логику самолёта и перерисовывает его.
        /// </summary>
        public override void Update() {
            s_currentPlaneLifetime += Constants.TIMER_INTERVAL_SECONDS;
            Move();
            Draw();
        }
    }
}
