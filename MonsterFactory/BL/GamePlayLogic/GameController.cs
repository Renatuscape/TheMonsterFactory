using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMonsterFactory.BL.GamePlayLogic
{
    public static class GameController
    {
        public static string Attack(IAttack attacker, Creature defender)
        {
            string attackDescription;
            int damage = attacker.Attack(out attackDescription);
            defender.Health += -damage;

            if (defender.Health < 0)
            {
                defender.Health = 0;
            }

            return attackDescription;
        }

        public static string HealSelf(IHeal healer, Creature creature)
        {
            string healingDescription;
            int healingAmount = healer.HealSelf(out healingDescription);
            creature.Health += healingAmount;

            return healingDescription;
        }

        public static string HealOthers(IHeal healer, List<Creature> party)
        {
            string healingDescription;
            int healingAmount = healer.HealOthers(out healingDescription);

            foreach (Creature creature in party)
            {
                if (creature is not IHeal)
                {
                    creature.Health += healingAmount;
                }
            }

            return healingDescription;
        }
    }
}
