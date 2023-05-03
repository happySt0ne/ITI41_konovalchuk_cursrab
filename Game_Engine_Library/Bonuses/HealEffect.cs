using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library.Bonuses {
    public class HealEffect : Effect {
        public HealEffect(Panzar panzar) : base(panzar) {
            Health += Constants.HEAL_EFFECT_GIVEN_HEALTH;
            if (Health > Constants.PANZAR_MAX_HP) Health = Constants.PANZAR_MAX_HP; 
        }
    }
}
