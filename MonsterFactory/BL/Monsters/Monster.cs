﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMonsterFactory.BL.Monsters
{
    public abstract class Monster : Creature, IAttack
    {
        public Monster(string name) : base(name)
        {
            Description = "A nasty little trickster.";
        }

        public virtual int Attack(out string description)
        {
            description = $"{Name} attacks!";
            return Dice[Dice.Count - 1].Roll(Level);
        }
    }
}
