﻿using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.BL.Heroes;
using TheMonsterFactory.BL.Monsters;

namespace TheMonsterFactory.BL.CombatMoves
{
    public static class HeroMoveTargeting
    {
        public static List<Creature> ChooseEnemyTargets(GameData gameData, Move move)
        {
            List<Creature> targetList = new();
            for (int i = 0; i < move.MaxTargets; i++)
            {
                if (i >= gameData.MonsterList.Count)
                {
                    gameData.TextManager.WriteLine("No more valid targets available.");
                    break;
                }
                else
                {
                    int target = ChooseEnemy(gameData);
                    targetList.Add(gameData.MonsterList[target]);
                }
            }
            return targetList;
        }

        public static List<Creature> ChooseAllyTargets(GameData gameData, Creature activeCreature, Move move)
        {
            List<Creature> targetList = new();

            for (int i = 0; i < move.MaxTargets; i++)
            {
                if (i >= gameData.MonsterList.Count)
                {
                    gameData.TextManager.WriteLine("No more valid targets available.");
                    break;
                }
                else
                {
                    int target = ChooseAlly(gameData, activeCreature, move);
                    targetList.Add(gameData.MonsterList[target]);
                }
            }
            return targetList;
        }

        public static List<Creature> ChooseRandomAllies(GameData gameData, Creature activeCreature, Move move)
        {
            List<Creature> targetList;

            targetList = new(gameData.HeroList);

            if (move.CanTargetSelf)
            {
                targetList.Remove(activeCreature);
            }
            while (targetList.Count > move.MaxTargets)
            {
                int randomIndex = gameData.randomiser.Next(0, targetList.Count);
                targetList.RemoveAt(randomIndex);
            }
            return targetList;
        }

        public static List<Creature> ChooseRandomCreature(GameData gameData, Creature activeCreature, Move move)
        {
            List<Creature> targetList;

            targetList = new(gameData.HeroList);
            targetList.AddRange(gameData.MonsterList);

            if (move.CanTargetSelf)
            {
                targetList.Remove(activeCreature);
            }
            while (targetList.Count > move.MaxTargets)
            {
                int randomIndex = gameData.randomiser.Next(0, targetList.Count);
                targetList.RemoveAt(randomIndex);
            }
            return targetList;
        }

        public static List<Creature> ChooseRandomEnemies(GameData gameData, Move move)
        {
            List<Creature> targetList;

            targetList = new(gameData.MonsterList);

            while (targetList.Count > move.MaxTargets)
            {
                int randomIndex = gameData.randomiser.Next(0, targetList.Count);
                targetList.RemoveAt(randomIndex);
            }
            return targetList;
        }

        static int ChooseAlly(GameData gameData, Creature activeCreature, Move move)
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

                if (!move.CanTargetSelf && gameData.HeroList[target] == activeCreature)
                {
                    gameData.TextManager.WriteLine($"Cannot target self with this action.");
                    target = -1;
                    gameData.TextManager.ContinueAfterAnyKey();
                }
            }
            return target;
        }

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