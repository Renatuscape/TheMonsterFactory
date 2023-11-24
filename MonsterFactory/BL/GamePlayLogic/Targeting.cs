using TheMonsterFactory.BL.Heroes;
using TheMonsterFactory.BL.Monsters;

namespace TheMonsterFactory.BL.GamePlay
{
    public static class Targeting
    {
        public static int ChooseAlly(GameData gameData)
        {
            int target = -1;
            while (target <= -1 || target >= gameData.HeroList.Count)
            {
                for (int i = 0; i < gameData.HeroList.Count; i++)
                {
                    gameData.TextManager.WriteLine($"{i}: {gameData.HeroList[i].ShortStats()}");
                }

                string input = gameData.TextManager.ReadKey();
                gameData.TextManager.WriteLine("");

                if (int.TryParse(input, out var choice))
                {
                    target = choice;
                }

                if (target >= gameData.HeroList.Count || target < 0)
                {
                    target = -1;
                }

                if (target <= -1)
                {
                    gameData.TextManager.WriteLine($"Please choose a valid target.");
                    gameData.TextManager.ContinueAfterAnyKey();
                }
            }
            return target;
        }
        public static int ChooseEnemy(GameData gameData)
        {
            int target = -1;

            while (target <= -1 || target >= gameData.HeroList.Count)
            {
                if (gameData.MonsterList != null)
                {
                    for (int i = 0; i < gameData.MonsterList.Count; i++)
                    {
                        gameData.TextManager.WriteLine($"{i}: {gameData.MonsterList[i].ShortStats()}");
                    }

                    string input = gameData.TextManager.ReadKey();
                    gameData.TextManager.WriteLine("");

                    if (int.TryParse(input, out var choice))
                    {
                        target = choice;
                    }
                    if (target >= gameData.MonsterList.Count || target < 0)
                    {
                        target = -1;
                    }

                    if (target <= -1)
                    {
                        gameData.TextManager.WriteLine($"Please choose a valid target.");
                        gameData.TextManager.ContinueAfterAnyKey();
                    }
                }
            }
            return target;
        }
    }

}
