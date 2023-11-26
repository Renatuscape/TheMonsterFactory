﻿using TheMonsterFactory.BL.CombatMoves;

namespace TheMonsterFactory.BL.Heroes
{
    public class Hero : Creature, IAttack
    {
        public Hero(string name, int level) : base(name, level)
        {
            Description = "A simple adventurer.";
            Moves.Find("Iron Sword", MoveList);
        }

        public virtual int Attack(out string description)
        {
            int amount = Die.Roll(Level) * 2;
            description = $"{Name} swings their sword for {amount} damage!";
            return amount;
        }
    }
}