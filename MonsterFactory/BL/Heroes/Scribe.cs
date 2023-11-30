using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;

namespace TheMonsterFactory.BL.Heroes
{
    public class Scribe : Hero, IMagicMissile, ICaster
    {
        public Scribe(string name, int level) : base(name, level)
        {
            BaseCost = 20;

            Description = "A fragile scholar with arcane powers.";
            Moves.Find("Magic Missile", MoveList);
        }

        public override string Defend()
        {
            return $"{Name} creates a shimmering shield of magic around themselves.";
        }

        public int MagicMissile(out string description)
        {
            int amount = Convert.ToInt32(Die.Roll(Level) * 0.5f);
            if (amount < 1)
            {
                amount = 1;
            }
            description = $"{Name} strikes the enemies with magic missiles for {amount} damage each.";
            return amount;
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
