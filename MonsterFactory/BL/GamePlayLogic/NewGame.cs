using System;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.GamePlayLogic;
using TheMonsterFactory.BL.GamePlayLogic.MonsterAI;
using TheMonsterFactory.BL.Heroes;
using TheMonsterFactory.BL.Monsters;

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
                        gameData.TextManager.WriteLine($"{i} {move.Name}");
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
                    gameData.TextManager.WriteLine($"{hero} does not understand the command. Try again.");
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
                Creature? target;

                if (gameData.HeroList.Count < 1)
                {
                    break;
                }
                MonsterActionManager.ChooseAction(gameData, monster, out var description, out target);

                if (!string.IsNullOrWhiteSpace(description))
                {
                    gameData.TextManager.WriteLine(description);
                }

                if (target != null)
                {
                    if (target.Health <= 0)
                    {
                        gameData.TextManager.WriteLine($"{target} died!");
                        gameData.HeroList.Remove((Hero)target);
                    }
                    else
                    {
                        gameData.TextManager.WriteLine($"{target} has {target.Health} HP remaining.");
                    }
                }
                gameData.TextManager.ContinueAfterAnyKey();
            }
        }

        public void EndRound(GameData gameData)
        {
            if (gameData.HeroList.Count <= 0)
            {
                gameData.TextManager.WriteLine("Your heroes are all dead. GAME OVER.");
                gameData.TextManager.ContinueAfterAnyKey();
            }
            else if (gameData.MonsterList.Count <= 0)
            {
                gameData.TextManager.WriteLine(" You vanquished the enemy! Congratulations are in order. ");
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

        public static void HeroSpell(Hero hero, GameData gameData)
        {
            List<Creature> targetList = new();

            foreach (Monster enemy in gameData.MonsterList)
            {
                targetList.Add(enemy);
            }

            IMagicMissile mage = (IMagicMissile)hero;
            gameData.TextManager.WriteLine(Actions.MagicMissile(mage, targetList));

            for (int i = 0; i < gameData.MonsterList.Count; i++)
            {
                if (gameData.MonsterList[i].Health <= 0)
                {
                    gameData.TextManager.WriteLine($"{gameData.MonsterList[i]} was killed!");
                    gameData.MonsterList.Remove(gameData.MonsterList[i]);

                    if (gameData.randomiser.Next(0, 100) > 50)
                    {
                        hero.LevelUp();
                        gameData.TextManager.WriteLine($"{hero} levelled up!");
                    }
                }
            }
        }

    }
}
