using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.DiceLogic;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes.AdvancedClasses
{
    public class Sorcerer : AdvancedHero, ICaster
    {
        public Sorcerer(string name, int level) : base(name, level)
        {
            Evasiveness = 10;
            Description = "A single-target caster with offensive and defensive spells.";
            Moves.Find("Cure", MoveList);
            Moves.Find("Witchbolt", MoveList);
        }
    }
}
