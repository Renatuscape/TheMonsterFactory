using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.DiceLogic;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes.AdvancedClasses;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes
{
    public class Fighter : Hero
    {
        public Fighter(string name, int level) : base(name, level)
        {
            BaseCost = 15;
            HealthDie = new D6();
            Evasiveness = 0;
            Description = "A brave, bright-eyed adventurer.";
            Moves.Find("Iron Sword", MoveList);

            UpgradeTypes.Add(new Knight(Name, Level));
        }
    }
}
