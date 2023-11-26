using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;

namespace TheMonsterFactory.BL.Heroes
{
    public class Fighter : Hero
    {
        public Fighter(string name, int level) : base(name, level)
        {
            BaseCost = 15;

            Description = "A brave, bright-eyed adventurer.";
            Moves.Find("Iron Sword", MoveList);
        }
    }
}
