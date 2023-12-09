using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.DiceLogic;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes.AdvancedClasses
{
    public abstract class AdvancedHero : Hero
    {
        public AdvancedHero(string name, int level) : base(name, level)
        {
            AdvanceLevel = 3;
            BaseCost = BaseCost * 3;
            ActionDie = new D8();
            HealthDie = new D8();
        }
    }
}
