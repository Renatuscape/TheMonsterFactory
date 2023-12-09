using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.DiceLogic;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes.AdvancedClasses
{
    public class Priest : AdvancedHero
    {
        public Priest(string name, int level) : base(name, level)
        {
            Evasiveness = 0;
            Description = "A versatile healer.";
            HealthDie = new D6();
            Moves.Find("Staff", MoveList);
            Moves.Find("Cure", MoveList);
            Moves.Find("Cure All", MoveList);
            Moves.Find("Ward", MoveList);
        }
    }
}
