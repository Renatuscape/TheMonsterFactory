using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMonsterFactory.BL.GamePlayLogic
{
    public static class Actions
    {
        public static string Attack(IAttack attacker, Creature defender)
        {
            string attackDescription;
            if (!defender.IsDefending)
            {
                int damage = attacker.Attack(out attackDescription);
                defender.Health += -damage;

                if (defender.Health < 0)
                {
                    defender.Health = 0;
                }
            }
            else
            {
                attackDescription = $"{defender} shields all damage!";
                defender.IsDefending = false;
            }

            return attackDescription;
        }

        public static string Defend(Creature defender)
        {
            return defender.Defend();
        }

        public static string Heal(IHeal healer, Creature creature)
        {
            string healingDescription;
            int healingAmount = healer.Heal(out healingDescription);
            creature.Health += healingAmount;

            return healingDescription;
        }

        public static string HealMany(IHeal healer, List<Creature> party)
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

        public static string MagicMissile(IMagicMissile mage, List<Creature> enemies)
        {
            string attackDescription;
            int damageAmount = mage.MagicMissile(out attackDescription);

            foreach (Creature creature in enemies)
            {
                if (!creature.IsDefending)
                {
                    creature.Health += -damageAmount;

                    if (creature.Health < 0)
                    {
                        creature.Health = 0;
                    }
                }
                else
                {
                    attackDescription += $"\n{creature} shielded itself against all damage.";
                }

            }

            return attackDescription;
        }
    }
}
