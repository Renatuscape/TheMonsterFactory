using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.DataAccess;

namespace TheMonsterFactory.BL.GamePlayLogic
{
    public abstract class CreatureFactory
    {
        public NameGenerator nameGenerator = new();
        public abstract Creature Create(string name);
    }
}
