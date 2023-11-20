using TheMonsterFactory.BL.Heroes;

namespace TheMonsterFactory.BL.GamePlayLogic
{
    public static class HeroChecker
    {
        static HeroMaker heroMaker = new HeroMaker();
        public static void HeroNumerCheck(ref List<Hero> heroList, int playerLevel, ITextManagement textManager)
        {
            textManager.WriteLine("\nSummoning phase!");
            textManager.ContinueAfterAnyKey();

            while (heroList.Count < 4)
            {
                bool hasCleric = false;

                foreach (Hero hero in heroList)
                {
                    if (hero is Cleric)
                    {
                        hasCleric = true;
                    }
                }

                textManager.Write("\nCall on a hero by name: ");
                string input = textManager.ReadLine() ?? "John";
                Hero newHero;

                if (hasCleric) { newHero = heroMaker.Create(playerLevel, input); }
                else { newHero = heroMaker.CreateCleric(playerLevel, input); }

                heroList.Add(newHero);
                textManager.WriteLine($"{input} ({newHero.GetType().Name}) has joined the party.");
                textManager.ContinueAfterAnyKey();
            }

            textManager.WriteLine("\nPARTY ROSTER:\n");

            foreach (Hero hero in heroList)
            {
                textManager.WriteLine(hero.FullStats());
            }
            textManager.ContinueAfterAnyKey();
        }
    }
}
