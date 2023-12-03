using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes
{
    public class Scribe : Hero, ICaster
    {
        public Scribe(string name, int level) : base(name, level)
        {
            BaseCost = 20;

            Description = "A fragile scholar with arcane powers.";
            Moves.Find("Magic Missile", MoveList);
        }

        public override void UpdateHealth()
        {
            int healthBoost = 0;
            healthBoost += Die.Roll(Level);
            Health += Convert.ToInt32(healthBoost * 0.5f);

            if (Health < 2)
            {
                Health = 2;
            }
        }
    }
}
