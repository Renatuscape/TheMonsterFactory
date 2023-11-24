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
        public Random random = new();
        public NameGenerator nameGenerator = new();
        public abstract Creature Create(int level, string name);
    }
}
