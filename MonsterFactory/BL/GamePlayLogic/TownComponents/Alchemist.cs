using MonsterFactory.UI;
using TheMonsterFactory.BL.GamePlay;

namespace TheMonsterFactory.BL.GamePlayLogic.ShopComponents
{
    public static class Alchemist
    {
        public static void VisitAlchemist(GameData gameData)
        {
            gameData.TextManager.WriteColour($"The alchemist will sell useful items and upgrades.", ColourTag.Default);
            gameData.TextManager.ContinueAfterAnyKey();
        }
    }
}
