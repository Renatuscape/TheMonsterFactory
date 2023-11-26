using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;

namespace TheMonsterFactory.BL.Monsters
{
    internal class Goblin : Monster
    {
        public Goblin(string name, int level) : base(name, level)
        {
            Moves.Find("Claws", MoveList);
        }

        public override int Attack(out string description)
        {
            int amount = Die.Roll(Level);
            description = $"{Name} swipes with its claws for {amount} damage!";
            return amount;
        }
    }
}
