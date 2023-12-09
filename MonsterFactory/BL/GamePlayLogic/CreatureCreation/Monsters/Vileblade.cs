using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.DiceLogic;
using TheMonsterFactory.BL.GamePlayLogic.MonsterAI;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Monsters
{
    public class Vileblade : Monster
    {
        public Vileblade(string name, int level) : base(name, level)
        {
            HealthDie = new D10();
            ActionDie = new D6();
            Description = "A foul knight raised from the grave by evil powers.";
            Name = "Ser " + Name;

            MonsterLogic.defendRate = 15;
            MonsterLogic.logicType = MonsterLogicType.AttackCaster;
            MonsterLogic.selfishness = 100;
            BaseCost = 25;

            Moves.Find("Witchbolt", MoveList);
            Moves.Find("Vile Blade", MoveList);
        }
    }
}
