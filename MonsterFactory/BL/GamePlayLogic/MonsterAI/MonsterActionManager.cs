using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.BL.Heroes;
using TheMonsterFactory.BL.Monsters;

namespace TheMonsterFactory.BL.GamePlayLogic.MonsterAI
{
    public static class MonsterActionManager
    {
        public static void ChooseAction(GameData gameData, Monster monster,out string actionDescription, out Creature? target)
        {
            target = null;
            actionDescription = string.Empty;

            if (gameData.randomiser.Next(0, 100) > monster.MonsterLogic.defendRate)
            {
                monster.IsDefending = false;
            }
            else
            {
                actionDescription = monster.Defend();
            }
            if (!monster.IsDefending)
            {
                switch (monster.MonsterLogic.logicType)
                {
                    case MonsterLogicType.TargetHighest:
                        target = FindHighestLevelHero(gameData);
                        actionDescription = Actions.Attack(monster, target);
                        break;
                    case MonsterLogicType.TargetLowest:
                        target = FindLowestLevelHero(gameData);
                        actionDescription = Actions.Attack(monster, target);
                        break;
                    case MonsterLogicType.AttackHealer:
                        target = FindHealer(gameData);
                        actionDescription = Actions.Attack(monster, target);
                        break;
                    case MonsterLogicType.HealLowest:
                        if (gameData.randomiser.Next(0,100) > monster.MonsterLogic.selfishness)
                        {
                            actionDescription = Actions.HealOthers((IHeal)monster, HealMany(gameData, monster));
                        }
                        else
                        {
                            if (gameData.randomiser.Next(0,100) > 25)
                            {
                            actionDescription = Actions.Heal((IHeal)monster, HealLowest(gameData, monster));
                            }
                            else
                            {
                                target = FindLowestLevelHero(gameData);
                                actionDescription = Actions.Attack(monster, target);
                            }

                        }
                        break;
                }

            }
        }

        public static Creature HealLowest(GameData gameData, Monster healer)
        {
            Creature target = healer;
            if (gameData.randomiser.Next(0, 80) > healer.MonsterLogic.selfishness)
            {
                foreach (Monster monster in gameData.MonsterList)
                {
                    if (monster.Health > target.Health
                     && monster is not IHeal)
                    {
                        target = monster;
                    }
                }
            }
            return target;
        }

        public static List<Creature> HealMany(GameData gameData, Monster healer)
        {
            List<Creature> targets = new();
            foreach (Monster monster in gameData.MonsterList)
            {
                if (monster != healer)
                {
                    targets.Add(monster);
                }
            }
            return targets;
        }
        public static Hero FindHighestLevelHero(GameData gameData)
        {
            Hero target = gameData.HeroList[0];
            foreach (Hero hero in gameData.HeroList)
            {
                if (hero.Level > target.Level
                    || hero.Level == target.Level && hero is IHeal)
                {
                    target = hero;
                }
            }
            return target;
        }

        public static Hero FindLowestLevelHero(GameData gameData)
        {
            Hero target = gameData.HeroList[0];
            foreach (Hero hero in gameData.HeroList)
            {
                if (hero.Level < target.Level
                    || hero.Level == target.Level && hero is IHeal)
                {
                    target = hero;
                }
            }
            return target;
        }

        public static Hero FindHealer(GameData gameData)
        {
            Hero target = gameData.HeroList[0];
            foreach (Hero hero in gameData.HeroList)
            {
                if (hero is IHeal)
                {
                    target = hero;
                }
            }
            if (target is not IHeal)
            {
                target = FindHighestLevelHero(gameData);
            }
            return target;
        }
    }
}
