using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMonsterFactory.BL.DiceLogic
{
    internal class D12 : Die
    {
        public override int Roll(int level)
        {
            return random.Next(1, 13) * level;
        }
    }
}
