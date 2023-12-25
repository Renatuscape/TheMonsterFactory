using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Monsters;

namespace TheMonsterFactory.Rendering
{
    public class ConsoleRenderer : IRender
    {
        public void HeroRoster(GameData gameData)
        {
            string heroString = string.Empty;
            foreach (Hero hero in gameData.HeroList)
            {
                heroString += $"|{hero} {hero.CurrentHealth}/{hero.MaxHealth} ";
            }
            gameData.TextManager.WriteLine(heroString + "\n");
        }

        public void IndexedHeroList(GameData gameData)
        {
            string heroString = string.Empty;
            int i = 0;
            foreach (Hero hero in gameData.HeroList)
            {
                heroString += $"[{i}] {hero.ShortStats()}\n";
                i++;
            }
            gameData.TextManager.WriteColour(heroString + "\n", MonsterFactory.UI.ColourTag.Information);
        }

        public void IndexedMonsterList(GameData gameData)
        {
            string monsterString = string.Empty;
            int i = 0;
            foreach (Monster monster in gameData.MonsterList)
            {
                monsterString += $"[{i}] {monster.ShortStats()}\n";
                i++;
            }
            gameData.TextManager.WriteColour(monsterString + "\n", MonsterFactory.UI.ColourTag.Information);
        }

        public void MonsterRoster(GameData gameData)
        {
            string monsterString = string.Empty;
            foreach (Monster monster in gameData.MonsterList)
            {
                monsterString += $"|{monster} {monster.CurrentHealth}/{monster.MaxHealth} ";
            }
            gameData.TextManager.WriteLine(monsterString + "\n");
        }

        public void Roster(GameData gameData)
        {
            string heroString = string.Empty;
            string monsterString = string.Empty;
            foreach (Hero hero in gameData.HeroList)
            {
                heroString += $"|{hero} {hero.CurrentHealth}/{hero.MaxHealth} ";
            }
            foreach (Monster monster in gameData.MonsterList)
            {
                monsterString += $"|{monster} {monster.CurrentHealth}/{monster.MaxHealth} ";
            }
            gameData.TextManager.WriteLine(heroString + "\n" + monsterString + "\n");
        }
    }
}
