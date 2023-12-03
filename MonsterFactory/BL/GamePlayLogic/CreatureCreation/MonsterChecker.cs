using TheMonsterFactory.BL.GamePlay;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation
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
