using TheMonsterFactory.BL.DiceLogic;

namespace TheMonsterFactory.BL.Heroes
{
    public class Cleric : Hero, IHeal
    {
        public Cleric(string name, int level) : base(name, level)
        {
            Description = "A loyal friend with healing magic.";
            ActionList.Add("Heal");
            ActionList.Add("Heal Many");
        }

        public int HealOthers(out string description)
        {
            int amount = Die.Roll(1 * (Level / 3)) + Level;
            description = $"{Name} heals each of their friends for {amount} points!";

            return amount;
        }

        public int Heal(out string description)
        {
            int amount = Die.Roll(Level);
            description = $"{Name} heals the target for {amount} points!";

            return amount;
        }

        public override int Attack(out string description)
        {
            int amount = 1 * Level;
            description = $"{Name} pokes the enemy for {amount} damage.";

            return amount;
        }

        public override string Defend()
        {
            return $"{Name} stays at a safe distance.";
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