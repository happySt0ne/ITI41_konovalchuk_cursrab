using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Engine_Library.Bonuses {
    /// <summary>
    /// Класс, создающий бонусы.
    /// </summary>
    public static class BonusCreator {
        delegate Bonus bonusDelegate(double x, double y);
        private static Random s_random;
        private static List<bonusDelegate> s_createBonuseDelegateList;

        static BonusCreator() {
            s_random = new Random(Guid.NewGuid().GetHashCode());
            s_createBonuseDelegateList = new List<bonusDelegate> { CreateHealBonus, CreateAmmoBonus, CreateReduceCooldownBonus };
        }

        private static HealBonus CreateHealBonus(double x, double y) => new HealBonus(x, y);
        private static AmmoBonus CreateAmmoBonus(double x, double y) => new AmmoBonus(x, y);
        private static ReduceCooldownBonus CreateReduceCooldownBonus(double x, double y) => new ReduceCooldownBonus(x, y);

        /// <summary>
        /// Возвращает случайный бонус.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Bonus CreateRandomBonus(double x, double y) =>
            s_createBonuseDelegateList[s_random.Next(s_createBonuseDelegateList.Count)](x, y);

    }
}
