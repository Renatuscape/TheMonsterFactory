using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.BL.Heroes;
using TheMonsterFactory.BL.Monsters;
using static System.Net.Mime.MediaTypeNames;

namespace TheMonsterFactory.BL.CombatMoves
{
    public static class MoveManager
    {
        public static void Parse(GameData gameData, Creature activeCreature, Move move)
        {
            if (move.MoveType == MoveType.DamagePhysical || move.MoveType == MoveType.DamageMagical)
            {
                AttackParser(gameData, activeCreature, move);
            }
            else if (move.MoveType == MoveType.Heal)
            {
                HealParser(gameData, activeCreature, move);
            }
            else if (move.MoveType == MoveType.Buff)
            {
                BuffParser(gameData, activeCreature, move);
            }

            DeathChecker(gameData);
        }

        static void HealParser(GameData gameData, Creature activeCreature, Move move)
        {
            List<Creature> targetList;

            if (activeCreature is Hero)
            {
                if (move.IsRandomTarget)
                {
                    targetList = HeroMoveTargeting.ChooseRandomAllies(gameData, activeCreature, move);
                }
                else
                {
                    targetList = HeroMoveTargeting.ChooseAllyTargets(gameData, activeCreature, move);
                }

                foreach (Creature ally in targetList)
                {
                    int damage = DamageCalculator(move, activeCreature);
                    ally.Health += damage;
                    Write(gameData, $"{ally} regains {damage} points of health!");
                }

                DistributeXP(gameData, 2 * activeCreature.Level, activeCreature);
            }
        }
        static void AttackParser(GameData gameData, Creature activeCreature, Move move)
        {
            List<Creature> targetList;
            int averageTargetLevel = 0;

            if (activeCreature is Hero)
            {
                if (!move.IsRandomTarget)
                {
                    targetList = HeroMoveTargeting.ChooseEnemyTargets(gameData, move);
                }
                else
                {
                    targetList = HeroMoveTargeting.ChooseRandomEnemies(gameData, move);
                }

                Write(gameData, $"{activeCreature} {move.Description.ToLower()}");

                foreach (Creature target in targetList)
                {
                    if (target.IsDefending)
                    {
                        string defenceText = $"{target} shielded themselves and took no damage.";
                        if (move.MoveType == MoveType.DamagePhysical)
                        {
                            defenceText +=" Their defence broke!";
                            target.IsDefending = false;
                        }
                        Write(gameData, defenceText);
                    }
                    else
                    {
                        int damage = DamageCalculator(move, activeCreature);
                        target.Health += -damage;
                        Write(gameData, $"{target} took {damage} damage!");
                        averageTargetLevel += target.Level;
                    }
                }

                averageTargetLevel = averageTargetLevel / targetList.Count;

                if (averageTargetLevel < 1)
                {
                    averageTargetLevel = 1;
                }
                DistributeXP(gameData, 2 * averageTargetLevel, activeCreature);
            }
        }
        static int DamageCalculator(Move move, Creature attacker)
        {
            int damage = 1;
            damage += Convert.ToInt32(attacker.Die.Roll(attacker.Level) * move.DiceMultiplier);
            return damage;
        }
        static void BuffParser(GameData gameData, Creature activeCreature, Move move)
        {
            if (move.BuffType == BuffType.Shield)
            {
                activeCreature.IsDefending = true;
                Write(gameData, $"{activeCreature} {move.Description.ToLower()}");
            }
        }

        static void DeathChecker(GameData gameData)
        {
            for (int i = 0; i < gameData.HeroList.Count; i++)
            {
                var hero = gameData.HeroList[i];
                if (hero.Health <= 0)
                {
                    Write(gameData, $"{hero} died.");
                    gameData.HeroList.Remove(hero);
                }
            }

            for (int i = 0; i < gameData.MonsterList.Count; i++)
            {
                var monster = gameData.MonsterList[i];

                if (monster.Health <= 0)
                {
                    int prize = monster.BaseCost * monster.Level;
                    Write(gameData, $"{monster} was killed and dropped {prize} gold.");
                    foreach (Hero hero in gameData.HeroList)
                    {
                        DistributeXP(gameData, 4 * monster.Level, hero);
                    }
                    gameData.Gold += prize;
                    gameData.MonsterList.Remove(monster);
                }
            }
        }

        static void DistributeXP(GameData gameData, int xp, Creature hero)
        {
            hero.AddXP(xp, out var description);
            Write(gameData, description);
        }

        static void Write(GameData gameData, string text)
        {
            gameData.TextManager.WriteLine(text);
        }
    }
}
