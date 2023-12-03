using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Monsters;

namespace TheMonsterFactory.BL.GameStructure
{
    public class Wave
    {
        public List<Monster> WaveContent { get; set; } = new();

        public Wave()
        {

        }

        public Wave(GameData gameData, int enemyCount = 8, bool IsRandom = true, int levelAdjustment = 0)
        {
            MonsterMaker monsterMaker = new();

            if (IsRandom)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    WaveContent.Add(monsterMaker.CreateFighter(gameData.MonsterLevel + levelAdjustment));
                }
            }
        }
    }
}
