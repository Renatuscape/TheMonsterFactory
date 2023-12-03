using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.BL.Monsters;
using MonsterFactory.UI;
using System.Threading;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;

namespace TheMonsterFactory.BL.GamePlayLogic.MonsterAI
{
    public static class MonsterCombatManager
    {
        public static void Act(GameData gameData, Monster activeCreature)
        {
            var chosenMove = MovePicker(gameData, activeCreature);

            if (chosenMove.MoveType == MoveType.DamageMagical || chosenMove.MoveType == MoveType.DamagePhysical)
            {
                AttackParser(gameData, activeCreature, chosenMove);
            }
            else if (chosenMove.MoveType == MoveType.Heal)
            {
                HealParser(gameData, activeCreature, chosenMove);
            }
            else if (chosenMove.Name == "Defend")
            {
                gameData.TextManager.WriteLine($"{activeCreature} {chosenMove.Description.ToLower()}");
                activeCreature.IsDefending = true;
            }
            DeathChecker(gameData);
        }

        static Move MovePicker(GameData gameData, Monster activeCreature)
        {
            Move chosenMove = activeCreature.MoveList[0];

            if (!activeCreature.IsDefending && gameData.randomiser.Next(0, 100) < activeCreature.MonsterLogic.defendRate)
            {
                return chosenMove;
            }

            //PICK HEALER MOVE
            else if (gameData.randomiser.Next(0, 100) > activeCreature.MonsterLogic.selfishness && activeCreature is IHeal)
            {
                List<Move> healerMoves = new(activeCreature.MoveList);

                for (int i = healerMoves.Count - 1; i >= 0; i--)
                {
                    if (healerMoves[i].MoveType != MoveType.Heal)
                    {
                        healerMoves.Remove(healerMoves[i]);
                    }
                }

                if (healerMoves.Count > 0)
                {
                    chosenMove = healerMoves[gameData.randomiser.Next(0, healerMoves.Count)];
                }
            }
            //PICK CASTER MOVE
            else if (activeCreature is ICaster)
            {
                List<Move> casterMoves = new(activeCreature.MoveList);

                for (int i = casterMoves.Count - 1; i >= 0; i--)
                {
                    if (casterMoves[i].MoveType != MoveType.DamageMagical)
                    {
                        casterMoves.Remove(casterMoves[i]);
                    }
                }

                if (casterMoves.Count > 0)
                {
                    chosenMove = casterMoves[gameData.randomiser.Next(0, casterMoves.Count)];
                }
            }
            //PICK ANY MOVE
            else
            {
                List<Move> damageMoves = new(activeCreature.MoveList);

                for (int i = damageMoves.Count-1; i >= 0; i--)
                {
                    if (damageMoves[i].MoveType != MoveType.DamageMagical && damageMoves[i].MoveType != MoveType.DamagePhysical)
                    {
                        damageMoves.Remove(damageMoves[i]);
                    }
                }

                if (damageMoves.Count > 0)
                {
                    chosenMove = damageMoves[gameData.randomiser.Next(0, damageMoves.Count)];
                }
            }

            return chosenMove;
        }

        static void AttackParser(GameData gameData, Monster activeCreature, Move move)
        {
            List<Creature> targetList = MonsterMoveTargeting.ChooseTargets(gameData, activeCreature, move);

            gameData.TextManager.WriteLine($"{activeCreature} {move.Description.ToLower()}");

            foreach (Creature target in targetList)
            {
                if (target.IsDefending)
                {
                    string defenceText = $"{target} shielded themselves and [took no damage].";
                    if (move.MoveType == MoveType.DamagePhysical)
                    {
                        defenceText += " Their defence broke!";
                        target.IsDefending = false;
                    }
                    gameData.TextManager.WriteColour(defenceText, ColourTag.Alert);
                }
                else
                {
                    int damage = MoveManager.DamageCalculator(move, activeCreature);
                    target.Health += -damage;
                    gameData.TextManager.WriteColour($"{target} took [{damage} damage]!", ColourTag.Critical);
                }
            }
        }

        static void HealParser(GameData gameData, Monster activeCreature, Move move)
        {
            List<Creature> targetList = MonsterMoveTargeting.ChooseTargets(gameData, activeCreature, move);

            gameData.TextManager.WriteLine($"{activeCreature} {move.Description.ToLower()}");

            foreach (Creature ally in targetList)
            {
                int damage = MoveManager.DamageCalculator(move, activeCreature);
                ally.Health += damage;
                gameData.TextManager.WriteColour($"{ally} regains [{damage} points of health]!", ColourTag.Emphasis);
            }
        }

        static void DeathChecker(GameData gameData)
        {
            for (int i = gameData.HeroList.Count - 1; i >= 0; i--)
            {
                var hero = gameData.HeroList[i];
                if (hero.Health <= 0)
                {
                    gameData.TextManager.WriteColour($"{hero} [died].", ColourTag.Critical);
                    gameData.HeroList.Remove(hero);
                }
            }

            for (int i = gameData.MonsterList.Count - 1; i >= 0; i--)
            {
                var monster = gameData.MonsterList[i];
                if (monster.Health <= 0)
                {
                    gameData.TextManager.WriteColour($"{monster} was [killed].", ColourTag.Critical);
                    gameData.MonsterList.Remove(monster);
                }
            }
        }
    }
}
