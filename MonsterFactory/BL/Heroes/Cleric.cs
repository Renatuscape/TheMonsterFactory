using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.DiceLogic;

namespace TheMonsterFactory.BL.Heroes
{
    public class Cleric : Hero, IHeal
    {
        public Cleric(string name, int level) : base(name, level)
        {
            BaseCost = 15;

            Description = "A loyal friend with healing magic.";

            Moves.Find("Cure", MoveList);
            Moves.Find("Cure All", MoveList);
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

        public override string Defend()
        {
            return $"{Name} stays at a safe distance.";
        }
        public override void UpdateHealth()
        {
            int healthBoost = 0;
            healthBoost += Die.Roll(Level);
            Health += Convert.ToInt32(healthBoost * 0.6f);

            if (Health < 2)
            {
                Health = 2;
            }
        }
    }
}