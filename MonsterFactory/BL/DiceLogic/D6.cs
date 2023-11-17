using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMonsterFactory.BL.DiceLogic
{
    internal class D6 : Die
    {
        public override int Roll(int level)
        {
            return random.Next(1, 7) * level;
        }
    }
}
