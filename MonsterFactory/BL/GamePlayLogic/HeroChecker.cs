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

            while (gameData.HeroList.Count < 4)
            {
                bool hasCleric = false;

                foreach (Hero hero in gameData.HeroList)
                {
                    if (hero is Cleric)
                    {
                        hasCleric = true;
                    }
                }

                gameData.TextManager.Write("\nCall on a hero by name: ");
                string input = gameData.TextManager.ReadLine() ?? "John";
                Hero newHero;

                if (hasCleric) { newHero = heroMaker.Create(gameData.PlayerLevel, input); }
                else { newHero = heroMaker.CreateCleric(gameData.PlayerLevel, input); }

                gameData.HeroList.Add(newHero);
                gameData.TextManager.WriteLine($"A new {newHero.GetType().Name} has joined the party.");
                gameData.TextManager.WriteLine(newHero.ShortStats());
                gameData.TextManager.ContinueAfterAnyKey();
            }

            gameData.TextManager.WriteLine("\nPARTY ROSTER:\n");

            foreach (Hero hero in gameData.HeroList)
            {
                gameData.TextManager.WriteLine(hero.FullStats());
            }
            gameData.TextManager.ContinueAfterAnyKey();
        }
    }
}
