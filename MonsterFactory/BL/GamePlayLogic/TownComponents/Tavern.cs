using MonsterFactory.UI;
using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes;

namespace TheMonsterFactory.BL.GamePlayLogic.ShopComponents
{
    public static class Tavern
    {
        public static void VisitTavern(GameData gameData)
        {
            string choice = string.Empty;
            int restPrice = 20 * gameData.PlayerLevel * gameData.HeroList.Count;
            int restBonus = 10 * gameData.PlayerLevel;

            while (choice != "0" && choice != "1")
            {
                gameData.TextManager.WriteColour($"The tavern is run-down and dusty, but full of promising heroes.", ColourTag.Default);
                gameData.TextManager.WriteColour($"[0] Rest and restore your party's health ({restPrice}g).", ColourTag.Information);
                gameData.TextManager.WriteColour($"[1] Replenish your ranks with new heroes", ColourTag.Information);
                gameData.TextManager.WriteColour($"You have [{gameData.Gold} gold]", ColourTag.Alert);
                choice = gameData.TextManager.ReadKey().ToLower();

                if (choice != "0" && choice != "1")
                {
                    gameData.TextManager.WriteColour("[Invalid choice].", ColourTag.Subtle);
                    gameData.TextManager.ContinueAfterAnyKey();
                }
            }
            if (choice == "0")
            {
                if (gameData.Gold < restPrice)
                {
                    gameData.TextManager.WriteColour("[You cannot afford beds for your whole party.]", ColourTag.Subtle);
                    gameData.TextManager.ContinueAfterAnyKey();
                }
                else
                {
                    gameData.Gold += -restPrice;
                    foreach (Hero hero in gameData.HeroList)
                    {
                        hero.CurrentHealth += restBonus;
                    }
                    gameData.TextManager.WriteColour($"Everyone enjoys a lovely rest and wakes up with [{restBonus}] health restored.", ColourTag.SmallSuccess);
                    gameData.TextManager.ContinueAfterAnyKey();
                }
            }
            else if (choice == "1")
            {
                if (gameData.HeroList.Count < 4)
                {
                    HeroChecker.HeroNumerCheck(gameData);
                }
                else
                {
                    gameData.TextManager.WriteColour("[Your party is already full.]", ColourTag.Subtle);
                }
            }
            gameData.TextManager.ContinueAfterAnyKey();
        }
    }
}
