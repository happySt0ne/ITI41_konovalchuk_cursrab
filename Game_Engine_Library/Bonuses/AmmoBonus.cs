using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library.Bonuses {
    internal class AmmoBonus : Bonus {
        public AmmoBonus(double x, double y) : base(x, y) {
            texture = Texture.LoadTexture(Constants.AMMO_BONUS_TEXTURE_PATH);
        }
    }
}
