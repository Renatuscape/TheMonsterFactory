﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMonsterFactory.BL.Monsters
{
    internal class Spectre : Monster, IHeal
    {
        public Spectre(string name, int level) : base(name, level)
        {
        }

        public int HealOthers(out string description)
        {
            int amount = Die.Roll(1 * (Level / 3));
            description = $"{Name} heals each of the other monsters for {amount} points!";

            return amount;
        }

        public int Heal(out string description)
        {
            int amount = Die.Roll(Level);
            description = $"{Name} heals itself for {amount} points!";

            return amount;
        }

        public override int Attack(out string description)
        {
            description = $"{Name} extends a ghostly claw.";

            return 1 * Level;
        }

        public override string Defend()
        {
            return $"{Name} becomes translucent and hovers ominously.";
        }
    }
}
