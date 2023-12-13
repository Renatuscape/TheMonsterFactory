using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes.AdvancedClasses;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes
{
    public abstract class Hero : Creature
    {
        public int AdvanceLevel { get; set; } = 0;
        public List<Hero> UpgradeTypes { get; set; } = new();
        public Hero(string name, int level) : base(name, level)
        {

        }

        public void UpdateUpgrades()
        {
            foreach (Hero hero in UpgradeTypes)
            {
                hero.Name = Name;
                hero.Level = Level;
                hero.RollMaxHealth();
            }
        }
    }
}