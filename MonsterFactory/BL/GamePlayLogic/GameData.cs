using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Monsters;

namespace TheMonsterFactory.BL.GamePlay
{
    public class GameData
    {
        public Random randomiser = new Random();
        public ITextManagement TextManager { get; set; }
        public List<Hero> HeroList { get; set; }
        public List<Monster> MonsterList { get; set; }
        public int MonsterLevel { get; set; } = 1;
        public int PlayerLevel { get; set; } = 1;
        public int CurrentChapter { get; set; } = 0;
        public int Gold { get; set; } = 65;
        public int GameRound { get; set; } = 0;
        public GameData(ITextManagement textManager)
        { 
            TextManager = textManager;
            HeroList = new List<Hero>();
            MonsterList = new List<Monster>();
            Moves.PopulateList();
        }
    }
}
