using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.BL.Heroes;

namespace TheMonsterFactory.BL.GamePlayLogic
{
    public static class HeroChecker
    {
        static HeroMaker heroMaker = new HeroMaker();
        public static void HeroNumerCheck(GameData gameData)
        {
            gameData.TextManager.WriteLine("\nSummoning phase!");
            gameData.TextManager.ContinueAfterAnyKey();

            Hero newHero;
            string choice;
            Fighter fighter = new("", 1);
            Cleric cleric = new("", 1);
            Scribe scribe = new("", 1);

            while (gameData.HeroList.Count < 4)
            {
                bool hasEnoughGold = false;

                if (gameData.HeroList.Count > 0)
                {
                    gameData.TextManager.WriteLine($"You have {gameData.Gold} gold. Would you like to add a hero to your party?");
                    gameData.TextManager.WriteLine($"[0] Fighter ({fighter.BaseCost}x Level gold)");
                    gameData.TextManager.WriteLine($"[1] Cleric ({cleric.BaseCost}x Level gold)");
                    gameData.TextManager.WriteLine($"[2] Scribe ({scribe.BaseCost}x Level gold)");
                    gameData.TextManager.WriteLine($"[X] No");

                    choice = gameData.TextManager.ReadKey();
                }
                else
                {
                    choice = "0";
                }

                if (choice == "0")
                {
                    gameData.TextManager.Write("\nCall your fighter by name: ");
                    string input = gameData.TextManager.ReadLine() ?? "";
                    newHero = heroMaker.CreateFighter(gameData.PlayerLevel, input);
                }
                else if (choice == "1")
                {
                    gameData.TextManager.Write("\nCall your cleric by name: ");
                    string input = gameData.TextManager.ReadLine() ?? "";
                    newHero = heroMaker.CreateCleric(gameData.PlayerLevel, input);
                }
                else if (choice == "2")
                {
                    gameData.TextManager.Write("\nCall your scribe by name: ");
                    string input = gameData.TextManager.ReadLine() ?? "";
                    newHero = heroMaker.CreateScribe(gameData.PlayerLevel, input);
                }
                else
                {
                    break;
                }

                hasEnoughGold = PriceChecker(newHero);

                if (hasEnoughGold)
                {
                    gameData.Gold += -newHero.BaseCost;
                    gameData.HeroList.Add(newHero);
                    gameData.TextManager.WriteLine($"A new {newHero.GetType().Name} has joined the party.");
                    gameData.TextManager.WriteLine(newHero.ShortStats());
                }
                else
                {
                    gameData.TextManager.WriteLine("You do not have enough gold to buy this hero.");
                }

                gameData.TextManager.ContinueAfterAnyKey();
            }

            gameData.TextManager.WriteLine("\nPARTY ROSTER:\n");

            foreach (Hero hero in gameData.HeroList)
            {
                gameData.TextManager.WriteLine(hero.FullStats());
            }
            gameData.TextManager.WriteLine($"Gold: {gameData.Gold}");
            gameData.TextManager.ContinueAfterAnyKey();

            bool PriceChecker(Creature hero)
            {
                if (gameData.Gold >= hero.BaseCost)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
