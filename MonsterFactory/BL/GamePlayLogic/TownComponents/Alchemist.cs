using MonsterFactory.UI;
using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes;

namespace TheMonsterFactory.BL.GamePlayLogic.ShopComponents
{
    public static class Alchemist
    {
        public static void VisitAlchemist(GameData gameData)
        {
            gameData.TextManager.WriteColour($"The alchemist greets you and lists his available services.", ColourTag.Default);
            gameData.TextManager.WriteColour($"[0] Elixir of Life", ColourTag.Information);
            string choice = gameData.TextManager.ReadKey();
            gameData.TextManager.WriteLine("");

            if (int.TryParse(choice, out var choiceInt) && choiceInt >= 0)//&& choiceInt < menuChoices.Count)
            {
                if (choiceInt == 0)
                {
                    ElixirMenu(gameData);
                }
            }

            gameData.TextManager.ContinueAfterAnyKey();
        }

        static void ElixirMenu(GameData gameData)
        {
            int basePrice = 15 * gameData.PlayerLevel;
            int index = 0;
            foreach (Hero hero in gameData.HeroList)
            {
                gameData.TextManager.WriteColour($"[{index}] {hero.ShortStats()}\t [{basePrice * hero.BonusHealth} gold]", ColourTag.Alert);
                index++;
            }
            gameData.TextManager.WriteColour($"[x] Cancel", ColourTag.Alert);

            string choice = gameData.TextManager.ReadKey();
            gameData.TextManager.WriteLine("");

            if (int.TryParse(choice, out var choiceInt) && choiceInt >= 0 && choiceInt < gameData.HeroList.Count)
            {
                var hero = gameData.HeroList[choiceInt];
                if (gameData.Gold >= basePrice * hero.BonusHealth)
                {
                    gameData.Gold -= basePrice * hero.BonusHealth;
                    gameData.TextManager.WriteColour($"{hero.Name}'s [max health increased]!", ColourTag.Success);
                    IncreaseMaxHealth(hero);
                }
                else
                {
                    gameData.TextManager.WriteColour("[Not enough gold.]", ColourTag.Alert);
                }
            }

            gameData.TextManager.ContinueAfterAnyKey();
        }
        static void IncreaseMaxHealth(Hero hero)
        {
            hero.MaxHealth++;
            hero.CurrentHealth = hero.MaxHealth;
        }
    }
}
