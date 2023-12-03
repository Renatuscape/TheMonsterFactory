using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.DiceLogic;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes
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