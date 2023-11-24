using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.GamePlayLogic.MonsterAI;

namespace TheMonsterFactory.BL.Monsters
{
    public abstract class Monster : Creature, IAttack
    {
        public MonsterLogic MonsterLogic { get; set; } = new();
        public Monster(string name, int level) : base(name, level)
        {
            Description = "A nasty little trickster.";
            MonsterLogic.defendRate = 10;
            MonsterLogic.logicType = MonsterLogicType.TargetHighest;
        }

        public virtual int Attack(out string description)
        {
            description = $"{Name} attacks!";
            return Die.Roll(Level);
        }
    }
}
