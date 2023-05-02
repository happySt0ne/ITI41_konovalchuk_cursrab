using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Engine_Library.Bonuses {
    public static class BonusCreator {
        delegate Bonus bonusDelegate(double x, double y);
        private static Random random = new Random(Guid.NewGuid().GetHashCode());
        private static List<bonusDelegate> _createBonuseDelegateList = new List<bonusDelegate>();

        static BonusCreator() {
            _createBonuseDelegateList.Add(CreateHealBonus);
        }

        private static HealBonus CreateHealBonus(double x, double y) => new HealBonus(x, y);

        public static Bonus CreateRandomBonus(double x, double y) =>
            _createBonuseDelegateList[random.Next(_createBonuseDelegateList.Count)](x, y);
        
    }
}
