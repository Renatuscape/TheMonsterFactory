using MonsterFactory.UI;
using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes.AdvancedClasses;

namespace TheMonsterFactory.BL.GamePlayLogic.ShopComponents
{
    public static class Academy
    {
        public static void VisitAcademy(GameData gameData)
        {
            gameData.TextManager.WriteColour($"The academy will allow you to upgrade and promote your heroes.", ColourTag.Default);
            List<Hero> promotionList = new();
            foreach (Hero hero in gameData.HeroList)
            {
                if (hero.Level >= hero.AdvanceLevel && hero is not AdvancedHero)
                {
                    promotionList.Add(hero);
                }
            }

            if (promotionList.Count < 1)
            {
                gameData.TextManager.WriteColour($"There are no heroes ready for promotion yet.", ColourTag.Default);
            }
            else
            {
                gameData.TextManager.WriteColour($"Heroes ready for promotion:", ColourTag.Default);

                for (int i = 0; i < promotionList.Count; i++)
                {
                    gameData.TextManager.WriteColour($"[{i}] {promotionList[i].ShortStats()}", ColourTag.Information);
                }
                string choice = gameData.TextManager.ReadKey().ToLower();

                if (int.TryParse(choice, out int iChoice) && iChoice >= 0 && iChoice < promotionList.Count)
                {
                    if (promotionList[iChoice] is Cleric || promotionList[iChoice] is Scribe)
                    {
                        int cost = 40 * promotionList[iChoice].Level;
                        gameData.TextManager.WriteColour($"Promote {promotionList[iChoice]} to [Sorcerer] for [{cost} gold]? (Y/N)", ColourTag.Alert);
                        choice = gameData.TextManager.ReadKey().ToLower();
                        if (choice == "y")
                        {
                            if (gameData.Gold >= cost)
                            {
                                if (PromoteToSorcerer(gameData, promotionList[iChoice]))
                                {
                                    gameData.TextManager.WriteColour($"[Successfully promoted!].", ColourTag.SmallSuccess);
                                }
                            }
                            else
                            {
                                gameData.TextManager.WriteColour($"Not enough gold.", ColourTag.Subtle);
                            }
                        }
                    }
                    else if (promotionList[iChoice] is Fighter)
                    {
                        int cost = 30 * promotionList[iChoice].Level;
                        gameData.TextManager.WriteColour($"Promote {promotionList[iChoice]} to [Knight] for [{cost} gold]? (Y/N)", ColourTag.Alert);
                        choice = gameData.TextManager.ReadKey().ToLower();
                        if (choice == "y")
                        {
                            if (gameData.Gold >= cost)
                            {
                                if (PromoteToKnight(gameData, promotionList[iChoice]))
                                {
                                    gameData.TextManager.WriteColour($"[Successfully promoted!].", ColourTag.SmallSuccess);
                                }
                            }
                            else
                            {
                                gameData.TextManager.WriteColour($"Not enough gold.", ColourTag.Subtle);
                            }
                        }
                    }
                }
            }
            gameData.TextManager.ContinueAfterAnyKey();
        }

        static bool PromoteToSorcerer(GameData gameData, Hero hero)
        {
            if (hero is Cleric || hero is Scribe)
            {
                Sorcerer sorcerer = new(hero.Name, hero.Level);
                gameData.HeroList.Remove(hero);
                gameData.HeroList.Add(sorcerer);
                return true;
            }
            return false;
        }

        static bool PromoteToKnight(GameData gameData, Hero hero)
        {
            if (hero is Fighter)
            {
                Knight knight = new(hero.Name, hero.Level);
                gameData.HeroList.Remove(hero);
                gameData.HeroList.Add(knight);
                return true;
            }
            return false;
        }
    }
}
