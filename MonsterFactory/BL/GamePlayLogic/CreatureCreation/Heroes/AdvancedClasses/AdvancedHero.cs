using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes.AdvancedClasses
{
    public abstract class AdvancedHero : Hero
    {
        protected AdvancedHero(string name, int level) : base(name, level)
        {
            BaseCost = BaseCost * 2;
        }
    }
}
