using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library.Bonuses {
    internal class ReduceCooldownBonus : Bonus {
        public ReduceCooldownBonus(double x, double y) : base(x, y) {
            texture = Texture.LoadTexture(Constants.REDUCE_COOLDOWN_BONUS_TEXTURE_PATH);
        }
    }
}
