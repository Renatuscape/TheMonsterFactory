using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.BL.Monsters;

namespace TheMonsterFactory.BL.GamePlayLogic
{
    public static class MonsterChecker
    {
        static MonsterMaker monsterMaker = new MonsterMaker();
        public static void MonsterNumberCheck(GameData gameData)
        {

            while (gameData.MonsterList.Count < 4)
            {
                gameData.MonsterList.Add(monsterMaker.CreateFighter(gameData.MonsterLevel));
            }
            gameData.TextManager.WriteLine("\nENEMY ATTACKERS\n");

            foreach (var monster in gameData.MonsterList)
            {
                gameData.TextManager.WriteLine(monster.FullStats());
            }
            gameData.TextManager.ContinueAfterAnyKey();
        }
    }
}
