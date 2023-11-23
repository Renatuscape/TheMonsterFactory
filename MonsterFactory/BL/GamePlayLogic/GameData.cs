using TheMonsterFactory.BL.Heroes;
using TheMonsterFactory.BL.Monsters;

namespace TheMonsterFactory.BL.GamePlay
{
    public class GameData
    {
        public ITextManagement TextManager { get; set; }
        public List<Hero> HeroList { get; set; }
        public List<Monster> MonsterList { get; set; }
        public int MonsterLevel { get; set; } = 1;
        public int PlayerLevel { get; set; } = 1;
        public int GameRound { get; set; } = 0;
        public GameData(ITextManagement textManager) { 
            TextManager = textManager;
            HeroList = new List<Hero>();
            MonsterList = new List<Monster>();
        }
    }
}
