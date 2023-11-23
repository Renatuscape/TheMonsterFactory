using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheMonsterFactory.BL.GamePlayLogic;
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

            while (gameData.HeroList.Count > 0)
            {
                HeroRound(gameData);
                MonsterRound(gameData);
                EndRound(gameData);
            }

            textManager.WriteLine(" *** Goodbye! *** ");
        }

        public void HeroRound(GameData gameData)
        {
            Random random = new();
            gameData.TextManager.WriteLine("Heroes' Phase!");

            foreach (Hero hero in gameData.HeroList)
            {
                if (gameData.MonsterList.Count <= 0)
                {
                    break;
                }

                gameData.TextManager.WriteLine($"{hero}: Choose your action");

                foreach (string action in hero.ActionList)
                {
                    gameData.TextManager.WriteLine(" - " + action);
                }
                string choice = gameData.TextManager.ReadLine();

                if (choice.ToLower() == "attack")
                {
                    int target = Targeting.EnemyPicker(gameData);
                    HeroAttack(hero, gameData.MonsterList, target, gameData.TextManager);
                }
                else if (choice.ToLower() == "defend")
                {
                    gameData.TextManager.WriteLine(GameController.Defend(hero));
                }
                else if (choice.ToLower() == "heal")
                {
                    IHeal healer = (IHeal)hero;
                    Hero target = gameData.HeroList[Targeting.AllyPicker(gameData)];

                    gameData.TextManager.WriteLine(GameController.Heal(healer, target));

                    if (random.Next(0, 100) > 40)
                    {
                        hero.LevelUp();
                        gameData.TextManager.WriteLine($"{hero} levelled up!");
                    }
                }
                else if (choice.ToLower() == "heal many" || choice.ToLower() == "heal others")
                {
                    IHeal healer = (IHeal)hero;
                    List<Creature> targetList = new();

                    foreach (Hero partyMember in gameData.HeroList)
                    {
                        if (partyMember is not IHeal)
                        {
                            targetList.Add(partyMember);
                        }
                    }

                    gameData.TextManager.WriteLine(GameController.HealOthers(healer, targetList));

                    if (random.Next(0, 100) > 25)
                    {
                        hero.LevelUp();
                        gameData.TextManager.WriteLine($"{hero} levelled up!");
                    }
                }
                else
                {
                    gameData.TextManager.WriteLine($"{hero} does not understand the command.");
                }
                gameData.TextManager.ContinueAfterAnyKey();
            }

        }

        public void MonsterRound(GameData gameData)
        {
            Random random = new();
            gameData.TextManager.WriteLine("Monsters' Phase!");
            foreach (Monster monster in gameData.MonsterList)
            {
                if (gameData.HeroList.Count < 1)
                {
                    break;
                }
                Hero target = gameData.HeroList[random.Next(0, gameData.HeroList.Count)];
                gameData.TextManager.WriteLine($"{monster} targets {target}.");
                gameData.TextManager.WriteLine(GameController.Attack(monster, target));

                if (target.Health <= 0)
                {
                    gameData.TextManager.WriteLine($"{target} died!");
                    gameData.HeroList.Remove(target);
                }
                gameData.TextManager.ContinueAfterAnyKey();
            }
        }

        public void EndRound(GameData gameData)
        {
            Random random = new();
            if (gameData.HeroList.Count <= 0)
            {
                gameData.TextManager.WriteLine("Your heroes are all dead. GAME OVER.");
                gameData.TextManager.ContinueAfterAnyKey();
            }
            else if (gameData.HeroList.Count > 0)
            {
                gameData.MonsterLevel++;
                gameData.PlayerLevel = random.Next(1, gameData.MonsterLevel < 4 ? gameData.MonsterLevel : gameData.MonsterLevel - 3);
                HeroChecker.HeroNumerCheck(gameData);
                MonsterChecker.MonsterNumberCheck(gameData);
            }
        }

        public static void HeroAttack(Hero hero, List<Monster> targetList, int targetNumber, ITextManagement textManager)
        {
            Random random = new();
            Monster target = targetList[targetNumber];

            textManager.WriteLine(GameController.Attack(hero, target));

            if (target.Health <= 0)
            {
                textManager.WriteLine($"{target} was killed!");
                targetList.Remove(target);

                if (random.Next(0, 100) > 25)
                {
                    hero.LevelUp();
                    textManager.WriteLine($"{hero} levelled up!");
                }
            }
        }

    }
}
