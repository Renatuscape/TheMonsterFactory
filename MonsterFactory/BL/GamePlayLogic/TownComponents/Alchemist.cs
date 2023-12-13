using MonsterFactory.UI;
using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes;

namespace TheMonsterFactory.BL.GamePlayLogic.ShopComponents
{
    public static class Alchemist
    {
        public static void VisitAlchemist(GameData gameData)
        {
            gameData.TextManager.WriteColour($"The alchemist sells useful items and upgrades.", ColourTag.Default);
            gameData.TextManager.ContinueAfterAnyKey();
        }

        static void IncreaseMaxHealth(Hero hero)
        {
            hero.MaxHealth++;
            hero.CurrentHealth = hero.MaxHealth;
        }
    }
}
