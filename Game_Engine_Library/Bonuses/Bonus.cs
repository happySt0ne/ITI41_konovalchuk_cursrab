using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library.Bonuses {
    public abstract class Bonus : GameObject {
        protected Bonus(double x, double y) : base(x, y, Constants.BONUS_WIDTH, Constants.BONUS_HEIGHT) {
        }

        private void Move() {
            for (int i = 0; i < Points.Count; i++) {
                Points[i] = (Points[i].Item1, Points[i].Item2 - Constants.BONUS_Y_SPEED);
            }

            Collision.MoveCollisionBoxTo(x, y);
        }

        public override void Update() {
            Move();
            Draw();
        }
    }
}
