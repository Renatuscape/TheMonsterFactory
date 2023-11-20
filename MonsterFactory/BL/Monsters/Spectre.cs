using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMonsterFactory.BL.Monsters
{
    internal class Spectre : Monster, IHeal
    {
        public Spectre(string name, int level) : base(name, level)
        {
        }

        public int HealOthers(out string description)
        {
            int amount = Dice[Dice.Count - 1].Roll(1 * (Level / 6));
            description = $"{Name} heals each of the other monsters for {amount} points!";

            return amount;
        }

        public int HealSelf(out string description)
        {
            int amount = Dice[Dice.Count - 1].Roll(Level);
            description = $"{Name} heals itself for {amount} points!";

            return amount;
        }

        public override int Attack(out string description)
        {
            description = $"{Name} extends a ghostly claw.";

            return 1 * Level;
        }

        public override string Move()
        {
            return $"{Name} hovers ominously.";
        }
    }
}
