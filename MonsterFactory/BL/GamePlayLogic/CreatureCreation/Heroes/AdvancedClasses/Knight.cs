using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes.AdvancedClasses
{
    public class Knight : AdvancedHero
    {
        public Knight(string name, int level) : base(name, level)
        {
            BaseCost = 30;

            Description = "A confident and sturdy warrior.";
            Moves.Find("Steel Sword", MoveList);
        }

        public override void UpdateHealth()
        {
            int healthBoost = 0;
            healthBoost += Die.Roll(Level);
            Health += Convert.ToInt32(healthBoost * 1.5f);

            if (Health < 2)
            {
                Health = 2;
            }
        }
    }
}
