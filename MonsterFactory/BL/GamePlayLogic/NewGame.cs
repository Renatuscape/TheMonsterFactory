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
        public List<Hero> HeroList;
        public List<Monster> MonsterList;
        public int monsterLevel = 1;
        public int playerLevel = 1;
        public NewGame(ITextManagement textManager)
        {
            HeroList = new();
            MonsterList = new();
            textManager.WriteLine(" *** WELCOME TO THE MONSTER FACTORY *** ");
            HeroChecker.HeroNumerCheck(ref HeroList, playerLevel, textManager);
            MonsterChecker.MonsterNumberCheck(ref MonsterList, monsterLevel, textManager);
            Round(textManager);
        }

        public void Round(ITextManagement textManager)
        {
            Random random = new();

            while (HeroList.Count > 0)
            {
                textManager.WriteLine("Combat phase!");

                foreach (Hero hero in HeroList)
                {
                    if (MonsterList.Count <= 0)
                    {
                        break;
                    }

                    textManager.WriteLine($"{hero}: Choose your action");

                    textManager.WriteLine(" - attack"); //only attack is supported as of now
                    /*foreach (string action in hero.ActionList)
                    {
                        textManager.WriteLine(" - " + action);
                    }*/
                    string choice = textManager.ReadLine();

                    if (choice.ToLower() == "attack")
                    {
                        Monster target = MonsterList[0];

                        textManager.WriteLine(GameController.Attack(hero, MonsterList[0]));

                        if (target.Health <= 0)
                        {
                            textManager.WriteLine($"{target} was killed!");
                            MonsterList.Remove(target);

                            if (random.Next(0, 100) > 25)
                            {
                                hero.LevelUp();
                                textManager.WriteLine($"{hero} levelled up!");
                            }
                        }
                    }
                    textManager.ContinueAfterAnyKey();
                }
                foreach (Monster monster in MonsterList)
                {
                    if (HeroList.Count < 1)
                    {
                        break;
                    }
                    Hero target = HeroList[random.Next(0, HeroList.Count)];
                    textManager.WriteLine($"{monster} targets {target}.");
                    textManager.WriteLine(GameController.Attack(monster, target));

                    if (target.Health <= 0)
                    {
                        textManager.WriteLine($"{target} died!");
                        HeroList.Remove(target);
                    }
                    textManager.ContinueAfterAnyKey();
                }

                if (HeroList.Count > 0)
                {
                    monsterLevel++;
                    playerLevel = random.Next(1, monsterLevel < 4 ? monsterLevel : monsterLevel-3);
                    HeroChecker.HeroNumerCheck(ref HeroList, playerLevel, textManager);
                    MonsterChecker.MonsterNumberCheck(ref MonsterList, monsterLevel, textManager);
                }
            }

            if (HeroList.Count <= 0)
            {
                textManager.WriteLine("Your heroes are all dead. GAME OVER.");
                textManager.ContinueAfterAnyKey();
            }
        }

    }
}
