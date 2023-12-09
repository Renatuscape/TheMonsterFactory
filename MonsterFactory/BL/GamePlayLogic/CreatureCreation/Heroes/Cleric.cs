using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.DiceLogic;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes.AdvancedClasses;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes
{
    public class Cleric : Hero, IHeal
    {
        public Cleric(string name, int level) : base(name, level)
        {
            BaseCost = 15;
            ActionDie = new D6();

            Description = "A loyal friend with healing magic.";

            Moves.Find("Staff", MoveList);
            Moves.Find("Cure", MoveList);
            Moves.Find("Cure All", MoveList);

            UpgradeTypes.Add(new Sorcerer(Name, Level));
        }
    }
}