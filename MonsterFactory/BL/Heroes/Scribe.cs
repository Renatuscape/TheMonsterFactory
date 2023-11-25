using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMonsterFactory.BL.Heroes
{
    public class Scribe : Hero, IMagicMissile
    {
        public Scribe(string name, int level) : base(name, level)
        {
            Description = "A fragile scholar with arcane powers.";
            ActionList.Add("Magic Missile");
        }

        public override int Attack(out string description)
        {
            int amount = Die.Roll(Level) * 1;
            description = $"{Name} shoves the enemy for {amount} damage.";

            return amount;
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
