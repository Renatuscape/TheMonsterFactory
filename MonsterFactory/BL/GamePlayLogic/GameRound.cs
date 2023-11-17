using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.Heroes;
using TheMonsterFactory.BL.Monsters;

namespace TheMonsterFactory.BL.GamePlay
{
    public class GameRound
    {
        public List<Hero> HeroList;
        public List<Monster> MonsterList;
        public GameRound()
        {
            HeroList = new();
            MonsterList = new();
            Console.WriteLine(" *** WELCOME TO THE MONSTER FACTORY *** ");
            HeroFactory.HeroNumerCheck(ref HeroList);
            MonsterFactory.MonsterNumberCheck(ref MonsterList);
        }

        public void Round()
        {
            while (HeroList.Count > 0)
            {
                HeroFactory.HeroNumerCheck(ref HeroList);
                MonsterFactory.MonsterNumberCheck(ref MonsterList);
            }
        }
        
    }
}
