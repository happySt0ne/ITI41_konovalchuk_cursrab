using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library.Bonuses {
    public abstract class Bonus : Panzar {
        private Panzar panzar;

        protected Bonus(Panzar panzar) : base(panzar.Side) {
        }
    }
}
