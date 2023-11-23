using TheMonsterFactory.BL.Heroes;
using TheMonsterFactory.BL.Monsters;

namespace TheMonsterFactory.BL.GamePlay
{
    public partial class NewGame
    {
        public static class Targeting
        {
            public static int AllyPicker(ITextManagement textManagement, List<Hero> heroList)
            {
                int target = -1;
                for (int i = 0; i < heroList.Count; i++)
                {
                    textManagement.WriteLine($"{i}: {heroList[i].ShortStats()}");
                }

                string input = textManagement.ReadKey();

                if (int.TryParse(input, out var choice))
                {
                    target = choice;
                }

                if (target >= heroList.Count)
                {
                    target = -1;
                }

                if (target <= -1)
                {
                    textManagement.WriteLine($"Please choose a valid target.");
                    textManagement.ContinueAfterAnyKey();
                }
                return target;
            }
            public static int EnemyPicker(ITextManagement textManagement, List<Monster>? monsterList)
            {
                int target = -1;

                if (monsterList != null)
                {
                    for (int i = 0; i < monsterList.Count; i++)
                    {
                        textManagement.WriteLine($"{i}: {monsterList[i].ShortStats()}");
                    }

                    string input = textManagement.ReadKey();

                    if (int.TryParse(input, out var choice))
                    {
                        target = choice;
                    }
                    if (target >= monsterList.Count)
                    {
                        target = -1;
                    }

                    if (target <= -1)
                    {
                        textManagement.WriteLine($"Please choose a valid target.");
                        textManagement.ContinueAfterAnyKey();
                    }
                }
                return target;
            }
        }

    }
}
