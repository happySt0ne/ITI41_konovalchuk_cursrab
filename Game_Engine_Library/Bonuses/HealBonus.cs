using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library.Bonuses {
    public class HealBonus : Bonus {
        public HealBonus(double x, double y) : base(x, y) {
            texture = Texture.LoadTexture(Constants.HEAL_BONUS_TEXTURE_PATH);
        }
    }
}
