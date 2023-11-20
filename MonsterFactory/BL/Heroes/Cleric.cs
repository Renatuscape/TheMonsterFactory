using TheMonsterFactory.BL.DiceLogic;

namespace TheMonsterFactory.BL.Heroes
{
    public class Cleric : Hero, IHeal
    {
        public Cleric(string name, int level) : base(name, level)
        {
            Description = "A loyal friend with healing magic.";
            ActionList.Add("Heal self");
            ActionList.Add("Heal others");
        }

        public int HealOthers(out string description)
        {
            int amount = Dice[Dice.Count - 1].Roll(1 * (Level / 6));
            description = $"{Name} heals each of their friends for {amount} points!";

            return amount;
        }

        public int HealSelf(out string description)
        {
            int amount = Dice[Dice.Count - 1].Roll(Level);
            description = $"{Name} heals themselves for {amount} points!";

            return amount;
        }

        public override int Attack(out string description)
        {
            int amount = 1 * Level;
            description = $"{Name} pokes the enemy for {amount} damage.";

            return amount;
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