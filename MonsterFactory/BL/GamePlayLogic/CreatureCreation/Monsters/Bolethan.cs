using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.DiceLogic;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;
using TheMonsterFactory.BL.GamePlayLogic.MonsterAI;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Monsters
{
    public class Bolethan : Monster, ICaster
    {
        public Bolethan(string name, int level) : base(name, level)
        {
            ActionDie = new D6();
            Description = "A slimy, floating ball with dark powers.";
            Name = "Bo-" + Name + "'ul";
            MonsterLogic.defendRate = 5;
            MonsterLogic.selfishness = 100;
            MonsterLogic.logicType = MonsterLogicType.AttackHighestLevel;

            Moves.Find("Sweeping Tentacle", MoveList);
            Moves.Find("Cure", MoveList);
        }
    }
}
