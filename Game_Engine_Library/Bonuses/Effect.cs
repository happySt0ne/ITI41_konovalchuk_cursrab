﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library.Bonuses {
    public abstract class Effect : Panzar {
        private Panzar panzar;

        protected Effect(Panzar panzar) : base(panzar.Side) {
        }
    }
}
