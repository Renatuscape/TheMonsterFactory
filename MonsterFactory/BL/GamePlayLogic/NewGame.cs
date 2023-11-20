using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public NewGame(ITextManagement textManager)
        {
            HeroList = new();
            MonsterList = new();
            textManager.WriteLine(" *** WELCOME TO THE MONSTER FACTORY *** ");
            HeroChecker.HeroNumerCheck(ref HeroList, textManager);
            MonsterChecker.MonsterNumberCheck(ref MonsterList, textManager);
        }

        public void Round(ITextManagement textManager)
        {
            while (HeroList.Count > 0)
            {
                HeroChecker.HeroNumerCheck(ref HeroList, textManager);
                MonsterChecker.MonsterNumberCheck(ref MonsterList, textManager);
            }
        }
        
    }
}
