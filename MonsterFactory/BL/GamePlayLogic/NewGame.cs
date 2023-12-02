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
            MonsterChecker.MonsterNumberCheck(gameData);

            while (gameData.HeroList.Count > 0 && gameData.MonsterList.Count > 0)
            {
                HeroRound(gameData);
                MonsterRound(gameData);
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
                gameData.TextManager.WriteColour(" [You vanquished the enemy! Congratulations are in order.] ", ColourTag.Success);
            }
            else if (gameData.HeroList.Count > 0 && gameData.MonsterList.Count > 0)
            {
                if (gameData.randomiser.Next(0, 100) > 30)
                {
                    gameData.GameRound++;
                    if (gameData.GameRound % 3 == 0)
                    {
                        gameData.MonsterLevel++;
                    }
                }

                gameData.PlayerLevel = gameData.randomiser.Next(1, gameData.MonsterLevel < 4 ? gameData.MonsterLevel : gameData.MonsterLevel - 3);
                HeroChecker.HeroNumerCheck(gameData);
                MonsterChecker.MonsterNumberCheck(gameData);
            }
        }
    }
}
