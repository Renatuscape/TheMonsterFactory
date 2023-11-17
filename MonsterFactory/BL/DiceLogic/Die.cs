using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TheMonsterFactory.BL.DiceLogic
{
    public abstract class Die
    {
        public static Random random = new Random();

        public abstract int Roll(int level);
    }
}
