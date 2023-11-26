using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.GamePlayLogic.MonsterAI;

namespace TheMonsterFactory.BL.Monsters
{
    public class Bolethan : Monster
    {
        public Bolethan(string name, int level) : base(name, level)
        {
            Description = "A slimy, floating ball with dark powers.";
            Name = "Bo-" + Name + "'Th";
            MonsterLogic.defendRate = 5;
            MonsterLogic.selfishness = 100;
            MonsterLogic.logicType = MonsterLogicType.AttackHighestLevel;

            Moves.Find("SweepingTentacle", MoveList);
            Moves.Find("Cure", MoveList);
        }
    }
}
