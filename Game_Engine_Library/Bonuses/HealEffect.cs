using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library.Bonuses {
    public class HealBonus : Bonus {
        public HealBonus(Panzar panzar) : base(panzar) {
            panzar.Health += 20;
            if (panzar.Health > 100) panzar.Health = 100;
        }
    }
}
