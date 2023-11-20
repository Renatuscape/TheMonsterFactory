using TheMonsterFactory.BL.DiceLogic;

namespace TheMonsterFactory.BL.Heroes
{
    public class Cleric : Hero, IHeal
    {
        public Cleric(string name) : base(name)
        {
            Description = "A loyal friend with healing magic.";
        }

        public int HealOthers(out string description)
        {
            description = $"{Name} heals their friends!";

            return Dice[Dice.Count - 1].Roll(1 * (Level / 6));
        }

        public int HealSelf(out string description)
        {
            description = $"{Name} heals themselves!";

            return Dice[Dice.Count - 1].Roll(Level);
        }

        public override int Attack(out string description)
        {
            description = $"{Name} pokes the enemy.";

            return 1 * Level;
        }

        public override string Move()
        {
            return $"{Name} stays at a safe distance.";
        }
        public override void UpdateHealth()
        {
            int healthBoost = 0;
            foreach (Die die in Dice)
            {
                healthBoost += die.Roll(Level);
            }
            Health += Convert.ToInt32(healthBoost * 0.5f);

            if (Health < 2)
            {
                Health = 2;
            }
        }
    }
}