using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMonsterFactory.BL.Monsters
{
    internal class Spectre : Monster, IHeal
    {
        public Spectre(string name) : base(name)
        {
        }

        public int HealOthers(out string description)
        {
            description = $"{Name} heals the other monsters!";

            return Dice[Dice.Count - 1].Roll(1 * (Level / 6));
        }

        public int HealSelf(out string description)
        {
            description = $"{Name} heals itself!";

            return Dice[Dice.Count - 1].Roll(Level);
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
