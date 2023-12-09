using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheMonsterFactory.BL.GamePlay;
using MonsterFactory.UI;
using static System.Net.Mime.MediaTypeNames;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes;

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
                    int potency = DamageCalculator(move, activeCreature);

                    if (ally.CurrentHealth + potency > ally.MaxHealth)
                    {
                        potency = ally.MaxHealth - ally.CurrentHealth;
                    }
                    if (potency > 0)
                    {
                        ally.CurrentHealth += potency;
                        gameData.TextManager.WriteColour($"{ally} regains [{potency} points of health!]", ColourTag.Emphasis);
                    }
                    else
                    {
                        gameData.TextManager.WriteColour($"{move.Name} [did nothing] to {ally}", ColourTag.Emphasis);
                    }
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

                gameData.TextManager.WriteColour($"{activeCreature} {move.Description.ToLower()}", ColourTag.Default);

                foreach (Creature target in targetList)
                {
                    if (target.IsDefending)
                    {
                        string defenceText = $"{target} shielded themselves and [took no damage].";
                        if (move.MoveType == MoveType.DamagePhysical)
                        {
                            defenceText +=" Their [defence broke]!";
                            target.IsDefending = false;
                        }
                        gameData.TextManager.WriteColour(defenceText, ColourTag.Alert);
                    }
                    else
                    {
                        if (move.Accuracy - target.Evasiveness > gameData.randomiser.Next(0, 100))
                        {
                            int damage = DamageCalculator(move, activeCreature);
                            target.CurrentHealth += -damage;
                            gameData.TextManager.WriteColour($"{target} took [{damage} damage]!", ColourTag.Critical);
                            averageTargetLevel += target.Level;
                        }
                        else
                        {
                            gameData.TextManager.WriteColour($"{target} [evaded] the attack!", ColourTag.Alert);
                        }
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
        public static int DamageCalculator(Move move, Creature attacker)
        {
            int damage = 1;
            damage += attacker.ActionDie.Roll(move.DiceMultiplier) + move.DiceBonus;
            return damage;
        }
        static void BuffParser(GameData gameData, Creature activeCreature, Move move)
        {
            if (move.BuffType == BuffType.Shield)
            {
                activeCreature.IsDefending = true;
                gameData.TextManager.WriteColour($"{activeCreature} {move.Description.ToLower()}", ColourTag.Default);
            }
        }

        static void DeathChecker(GameData gameData)
        {
            for (int i = gameData.HeroList.Count - 1; i >= 0; i--)
            {
                var hero = gameData.HeroList[i];
                if (hero.CurrentHealth <= 0)
                {
                    gameData.TextManager.WriteColour($"{hero} [died].", ColourTag.Critical);
                    gameData.HeroList.Remove(hero);
                }
            }

            for (int i = gameData.MonsterList.Count - 1; i >= 0; i--)
            {
                var monster = gameData.MonsterList[i];

                if (monster.CurrentHealth <= 0)
                {
                    int prize = monster.BaseCost * monster.Level;
                    gameData.TextManager.WriteColour($"{monster} was [killed] ", ColourTag.Critical, false);
                    gameData.TextManager.WriteColour($"and dropped [{prize} gold].", ColourTag.Success, true, false);
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
            hero.AddXP(xp, out string xpText, out string? levelUpText);

            gameData.TextManager.WriteColour(xpText, ColourTag.Information);
            if (levelUpText != null)
            {
                gameData.TextManager.WriteColour(levelUpText, ColourTag.Success);
            }
        }
    }
}
