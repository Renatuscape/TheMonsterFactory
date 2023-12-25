using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.GamePlay;

namespace TheMonsterFactory.Rendering
{
    public interface IRender
    {
        public void Roster(GameData gameData);
        public void HeroRoster(GameData gameData);
        public void IndexedHeroList(GameData gameData);
        public void MonsterRoster(GameData gameData);
        public void IndexedMonsterList(GameData gameData);
    }
}
