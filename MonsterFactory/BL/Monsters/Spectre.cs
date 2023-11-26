using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.GamePlayLogic.MonsterAI;

namespace TheMonsterFactory.BL.Monsters
{
    internal class Spectre : Monster, IHeal
    {
        public Spectre(string name, int level) : base(name, level)
        {
            MonsterLogic.defendRate = 25;
            MonsterLogic.logicType = MonsterLogicType.HealLowest;
            MonsterLogic.selfishness = 10;
            Name = "Ra-" + Name;
            BaseCost = 15;

            Moves.Find("Cure", MoveList);
            Moves.Find("Cure All", MoveList);
        }

        public int HealOthers(out string description)
        {
            int amount = Die.Roll(1 * (Level / 3)) + Level;
            description = $"{Name} heals each of the other monsters for {amount} points!";

            return amount;
        }

        public int Heal(out string description)
        {
            int amount = Die.Roll(Level);
            description = $"{Name} heals itself for {amount} points!";

            return amount;
        }

        public override int Attack(out string description)
        {
            description = $"{Name} extends a ghostly claw.";

            return 1 * Level;
        }

        public override string Defend()
        {
            return $"{Name} becomes translucent and hovers ominously.";
        }
    }
}
