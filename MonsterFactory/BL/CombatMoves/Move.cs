using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMonsterFactory.BL.CombatMoves
{
    public class Move
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TargetType Target { get; set; } = TargetType.Enemy;
        public MoveType MoveType { get; set; } = MoveType.DamagePhysical;
        public BuffType BuffType { get; set; } = BuffType.None;
        public int MaxTargets { get; set; } = 1;
        public int MissChance { get; set; } = 10;
        public float DiceMultiplier { get; set; } = 1;
        public bool CanTargetSelf { get; set; } = false;
        public bool CanTargetSelfOnly { get; set; } = false;
        public bool IsRandomTarget { get; set; } = false;

        public Move(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
