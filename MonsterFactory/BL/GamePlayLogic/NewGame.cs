using System;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.GamePlayLogic;
using TheMonsterFactory.BL.GamePlayLogic.MonsterAI;
using TheMonsterFactory.BL.Heroes;
using TheMonsterFactory.BL.Monsters;
using MonsterFactory.UI;

namespace TheMonsterFactory.BL.GamePlay
{
    public class NewGame
    {
        public NewGame(ITextManagement textManager)
        {
            GameData gameData = new(textManager);

            textManager.WriteLine(" *** WELCOME TO THE MONSTER FACTORY *** ");
            HeroChecker.HeroNumerCheck(gameData);
            gameData.MonsterList = ChapterManager.GetWave(gameData.CurrentChapter);//MonsterChecker.MonsterNumberCheck(gameData);
            PrintMonsters(gameData);

            while (gameData.HeroList.Count > 0 && gameData.MonsterList.Count > 0)
            {
                textManager.WriteColour($"[CHAPTER {gameData.CurrentChapter}]: [WAVE {ChapterManager.WaveCounter}]", ColourTag.Information);
                textManager.WriteLine("");
                HeroRound(gameData);
                if (gameData.MonsterList.Count > 0)
                {
                    MonsterRound(gameData);
                }
                EndRound(gameData);
            }

            textManager.WriteLine(" *** Goodbye! *** ");
        }

        public void HeroRound(GameData gameData)
        {
            gameData.TextManager.WriteLine("Hero Phase!");

            foreach (Hero hero in gameData.HeroList)
            {
                if (gameData.MonsterList.Count <= 0)
                {
                    break;
                }

                while (true)
                {
                    gameData.TextManager.WriteLine($"{hero.ShortStats()}\nChoose your action");
                    int i = 0;

                    foreach (Move move in hero.MoveList)
                    {
                        gameData.TextManager.WriteColour($"[{i}] {move.Name}", ColourTag.Information);
                        i++;
                    }

                    string choice = gameData.TextManager.ReadKey();
                    gameData.TextManager.WriteLine("");

                    if (int.TryParse(choice, out int index))
                    {
                        if (index >= 0 && index < hero.MoveList.Count)
                        {
                            MoveManager.Parse(gameData, hero, hero.MoveList[index]);
                            break;
                        }
                    }
                    gameData.TextManager.WriteColour($"{hero} does not understand the command. Try again.", ColourTag.Alert);
                    gameData.TextManager.ContinueAfterAnyKey();
                }
                gameData.TextManager.ContinueAfterAnyKey();
            }
        }

        public void MonsterRound(GameData gameData)
        {
            gameData.TextManager.WriteLine("Monster Phase!");
            foreach (Monster monster in gameData.MonsterList)
            {
                if (gameData.HeroList.Count < 1)
                {
                    break;
                }
                MonsterCombatManager.Act(gameData, monster);
                gameData.TextManager.ContinueAfterAnyKey();
            }
        }

        public void EndRound(GameData gameData)
        {
            if (gameData.HeroList.Count <= 0)
            {
                gameData.TextManager.WriteColour("Your heroes are all dead. [GAME OVER].", ColourTag.Critical);
                gameData.TextManager.ContinueAfterAnyKey();
            }
            else if (gameData.MonsterList.Count <= 0)
            {
                gameData.TextManager.WriteColour("[Wave cleared!]", ColourTag.SmallSuccess);
                gameData.TextManager.ContinueAfterAnyKey();

                if (ChapterManager.WaveCounter < ChapterManager.GetWavesInChapter(gameData.CurrentChapter))
                {
                    gameData.MonsterList = ChapterManager.GetWave(gameData.CurrentChapter);
                    PrintMonsters(gameData);
                }
                else
                {
                    gameData.TextManager.WriteColour(" [You vanquished the enemy! Chapter cleared.] ", ColourTag.Success);
                    gameData.TextManager.ContinueAfterAnyKey();
                    gameData.CurrentChapter++;

                    if (gameData.CurrentChapter < ChapterManager.GetChapterCount())
                    {
                        ChapterManager.WaveCounter = 0;
                        gameData.PlayerLevel++;

                        gameData.MonsterList = ChapterManager.GetWave(gameData.CurrentChapter);
                        PrintMonsters(gameData);
                        HeroChecker.HeroNumerCheck(gameData);
                        //CALL SHOP LOGIC HERE
                    }
                    else
                    {
                        gameData.TextManager.WriteColour(" [LAST CHAPTER CLEARED - CONGRATULATIONS!] ", ColourTag.Success);
                    }
                }
            }
        }

        public static void PrintMonsters(GameData gameData)
        {
            gameData.TextManager.WriteLine("\nENEMY ATTACKERS\n");

            foreach (var monster in gameData.MonsterList)
            {
                gameData.TextManager.WriteLine(monster.FullStats());
            }
            gameData.TextManager.ContinueAfterAnyKey();
        }
    }
}
