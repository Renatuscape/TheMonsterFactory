using TheMonsterFactory.BL.Monsters;

namespace TheMonsterFactory.BL.GamePlayLogic
{
    public static class MonsterChecker
    {
        static MonsterMaker monsterMaker = new MonsterMaker();
        public static void MonsterNumberCheck(ref List<Monster> monsterList, int monsterLevel, ITextManagement textManager)
        {

            while (monsterList.Count < 4)
            {
                monsterList.Add(monsterMaker.Create(monsterLevel));
            }
            textManager.WriteLine("\nENEMY ATTACKERS\n");

            foreach (var monster in monsterList)
            {
                textManager.WriteLine(monster.FullStats());
            }
            textManager.ContinueAfterAnyKey();
        }
    }
}
