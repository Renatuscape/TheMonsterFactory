using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.DiceLogic;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes.AdvancedClasses
{
    public class Knight : AdvancedHero
    {
        public Knight(string name, int level) : base(name, level)
        {
            HealthDie = new D10();
            Evasiveness = 0;
            Description = "A confident and sturdy warrior.";
            Moves.Find("Steel Sword", MoveList);
        }
    }
}
