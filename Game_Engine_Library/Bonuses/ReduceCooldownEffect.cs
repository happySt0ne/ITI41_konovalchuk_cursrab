using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library.Bonuses {
    public class ReduceCooldownEffect : Effect {
        private double _effectionTime = 3;

        public ReduceCooldownEffect(Panzar panzar) : base(panzar) {
             base.panzar._panzarMuzzle.refreshCooldown = Constants.MAX_COOLDOWN / 2;
        }

        public override void Update() {
            _effectionTime -= Constants.TIMER_INTERVAL_SECONDS; 
            if (_effectionTime <= 0) {
                panzar._panzarMuzzle.refreshCooldown = Constants.MAX_COOLDOWN;
            } 
                

            base.Update(); 
        }
    }
}
