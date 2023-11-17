using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMonsterFactory.BL.Monsters
{
    internal class Goblin : Monster
    {
        public Goblin(string name) : base(name)
        {

        }

        public override int Attack(out string description)
        {
            description = $"{Name} swipes with its claws!";
            return Dice[Dice.Count - 1].Roll(Level);
        }
    }
}
