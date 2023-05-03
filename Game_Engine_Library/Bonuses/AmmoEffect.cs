using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library.Bonuses {
    internal class AmmoEffect : Effect {
        public AmmoEffect(Panzar panzar) : base(panzar) {
            Ammo += 5;
        }
    }
}
