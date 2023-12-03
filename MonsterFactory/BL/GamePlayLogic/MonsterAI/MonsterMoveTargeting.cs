using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Monsters;
using TheMonsterFactory.BL.Heroes;

namespace TheMonsterFactory.BL.GamePlayLogic.MonsterAI
{
    public static class MonsterMoveTargeting
    {
        public static List<Creature> ChooseTargets(GameData gameData, Monster activeCreature, Move move)
        {
            List<Creature> targetList;

            if (move.Target == TargetType.Ally)
            {
                if (activeCreature.MonsterLogic.logicType == MonsterLogicType.HealSelf && move.MaxTargets == 1)
                {
                    targetList = new() { activeCreature };
                }
                else
                {
                    targetList = ChooseAllies(gameData, activeCreature);
                }

            }
            else if (move.Target == TargetType.Enemy)
            {
                targetList = ChooseEnemies(gameData, activeCreature);
            }
            else
            {
                targetList = ChooseCreatures(gameData, activeCreature, move);
            }

            return CullList(gameData, targetList, activeCreature, move);
        }

        static List<Creature> CullList(GameData gameData, List<Creature> targetList, Monster activeCreature, Move move)
        {
            if (!move.CanTargetSelf)
            {
                targetList.Remove(activeCreature);
            }
            while (targetList.Count > move.MaxTargets)
            {
                if (move.IsRandomTarget)
                {
                    int randomIndex = gameData.randomiser.Next(0, targetList.Count);
                    targetList.RemoveAt(randomIndex);
                }
                else
                {
                    targetList.RemoveAt(targetList.Count - 1);
                }
            }
            return targetList;
        }

        static List<Creature> ChooseAllies(GameData gameData, Monster activeCreature)
        {
            List<Creature> targetList;

            targetList = new(gameData.MonsterList);

            if (activeCreature.MonsterLogic.logicType == MonsterLogicType.AidLowestHealth)
            {
                targetList.Sort((x, y) => x.Health.CompareTo(y.Health));
            }
            else if (activeCreature.MonsterLogic.logicType == MonsterLogicType.AidHighestHealth)
            {
                targetList.Sort((x, y) => x.Health.CompareTo(y.Health));
                targetList = targetList.OrderByDescending(o => o.Health).ToList();
            }
            else if (activeCreature.MonsterLogic.logicType == MonsterLogicType.AidLowestLevel)
            {
                targetList.Sort((x, y) => x.Level.CompareTo(y.Level));
            }
            else //aid highest level
            {
                targetList.Sort((x, y) => x.Level.CompareTo(y.Level));
                targetList = targetList.OrderByDescending(o => o.Level).ToList();
            }

            return targetList;
        }

        static List<Creature> ChooseEnemies(GameData gameData, Monster activeCreature)
        {
            List<Creature> targetList;

            targetList = new(gameData.HeroList);

            if (activeCreature.MonsterLogic.logicType == MonsterLogicType.AttackHighestLevel)
            {
                targetList.Sort((x, y) => x.Level.CompareTo(y.Level));
                targetList = targetList.OrderByDescending(o => o.Level).ToList();
            }
            else if (activeCreature.MonsterLogic.logicType == MonsterLogicType.AttackLowestLevel)
            {
                targetList.Sort((x, y) => x.Level.CompareTo(y.Level));
            }
            else if (activeCreature.MonsterLogic.logicType == MonsterLogicType.AttackHealer)
            {
                List<Creature> healerList = new List<Creature>();
                foreach (var creature in targetList)
                {
                    if (creature is IHeal)
                    {
                        healerList.Add(creature);
                    }
                }
                foreach (var character in targetList)
                {
                    if (!(character is IHeal))
                    {
                        healerList.Add(character);
                    }
                }

                targetList = healerList;
            }
            else //Get the caster
            {
                List<Creature> casterList = new List<Creature>();
                foreach (var creature in targetList)
                {
                    if (creature is ICaster)
                    {
                        casterList.Add(creature);
                    }
                }
                foreach (var character in targetList)
                {
                    if (!(character is ICaster))
                    {
                        casterList.Add(character);
                    }
                }

                targetList = casterList;
            }

            return targetList;
        }

        static List<Creature> ChooseCreatures(GameData gameData, Creature activeCreature, Move move)
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

    }
}
