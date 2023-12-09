using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.DiceLogic;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes.AdvancedClasses;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes
{
    public class Scribe : Hero, ICaster
    {
        public Scribe(string name, int level) : base(name, level)
        {
            BaseCost = 20;
            ActionDie = new D6();

            Description = "A fragile scholar with arcane powers.";
            Moves.Find("Staff", MoveList);
            Moves.Find("Magic Missile", MoveList);

            UpgradeTypes.Add(new Sorcerer(Name, Level));
        }
    }
}
