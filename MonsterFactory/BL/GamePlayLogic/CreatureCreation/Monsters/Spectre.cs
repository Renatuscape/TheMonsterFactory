using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;
using TheMonsterFactory.BL.GamePlayLogic.MonsterAI;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Monsters
{
    internal class Spectre : Monster, IHeal
    {
        public Spectre(string name, int level) : base(name, level)
        {
            Description = "A vaporous sentinel.";
            MonsterLogic.defendRate = 25;
            MonsterLogic.logicType = MonsterLogicType.AidLowestHealth;
            MonsterLogic.selfishness = 10;
            Name = "Ra-" + Name;
            BaseCost = 15;

            Moves.Find("Cure", MoveList);
            Moves.Find("Cure All", MoveList);
            Moves.Find("Claws", MoveList);
        }
    }
}
