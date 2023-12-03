using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Monsters
{
    internal class Goblin : Monster
    {
        public Goblin(string name, int level) : base(name, level)
        {
            Moves.Find("Claws", MoveList);
        }
    }
}
