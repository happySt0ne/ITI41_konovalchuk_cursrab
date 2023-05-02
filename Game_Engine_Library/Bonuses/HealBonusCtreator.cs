using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library.Bonuses {
    public class HealBonusCtreator : BonusCreator {
        public override Bonus CreateBonus(double x, double y) => new HealBonus(x, y);
    }
}
