using System;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.GamePlayLogic.MonsterAI;
using MonsterFactory.UI;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;
using TheMonsterFactory.BL.GameStructure;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Monsters;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes;
using TheMonsterFactory.BL.GamePlayLogic.ShopComponents;
using TheMonsterFactory.Rendering;

namespace TheMonsterFactory.BL.GamePlay
{
    public class NewGame
    {
        public NewGame(ITextManagement textManager, IRender render)
        {
            GameData gameData = new(textManager, render);

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
                if (gameData.HeroList.Count > 0)
                {
                    CheckOverrun(gameData);
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
                gameData.Render.Roster(gameData);

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

                ChapterManager.PassRound(gameData.CurrentChapter);
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
                        
                        TownManager.OpenMenu(gameData);

                        gameData.MonsterList = ChapterManager.GetWave(gameData.CurrentChapter);
                        PrintMonsters(gameData);
                    }
                    else
                    {
                        gameData.TextManager.WriteColour(" [LAST CHAPTER CLEARED - CONGRATULATIONS!] ", ColourTag.Success);
                    }
                }
            }
        }

        public static void CheckOverrun(GameData gameData)
        {
            if (ChapterManager.GetRemainingRounds(gameData.CurrentChapter) > 0)
            {
                if (ChapterManager.GetRemainingRounds(gameData.CurrentChapter) < 3)
                {
                    gameData.TextManager.WriteColour($"[{ChapterManager.GetRemainingRounds(gameData.CurrentChapter)} rounds until the enemy reaches the city gates.]", ColourTag.Alert);
                }
            }
            else
            {
                gameData.TextManager.WriteColour("[THE ENEMY OVERWHELMS YOU]", ColourTag.Critical);
                List<Hero> killList = new();
                foreach (Hero hero in gameData.HeroList)
                {
                    int roundMultiplier = (ChapterManager.GetRemainingRounds(gameData.CurrentChapter) - 1) * -2;
                    int monsterModifier = gameData.MonsterList.Count > 0 ? gameData.MonsterList.Count : 1;
                    int maxDamage = Convert.ToInt32(hero.CurrentHealth * roundMultiplier + monsterModifier);
                    int damage = gameData.randomiser.Next(Convert.ToInt32(hero.CurrentHealth * 0.4f), maxDamage);

                    hero.CurrentHealth -= damage;
                    gameData.TextManager.WriteColour($"{hero} takes [{damage} damage.]", ColourTag.Critical);
                    if (hero.CurrentHealth <= 0)
                    {
                        gameData.TextManager.WriteColour($"{hero} [died.]", ColourTag.Critical);
                        killList.Add(hero);
                    }
                }
                foreach (Hero hero in killList)
                {
                    gameData.HeroList.Remove(hero);
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
