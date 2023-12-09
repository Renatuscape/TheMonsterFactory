using MonsterFactory.UI;
using System;
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
            gameData.TextManager.WriteColour($"Your gold: [{gameData.Gold}]", ColourTag.Alert);
            List<Hero> promotionList = new();
            foreach (Hero hero in gameData.HeroList)
            {
                if (hero.UpgradeTypes.Count > 0)
                {
                    promotionList.Add(hero);
                }
            }

            if (promotionList.Count < 1)
            {
                gameData.TextManager.WriteColour($"There are no heroes suitable for promotion.", ColourTag.Default);
            }
            else
            {
                gameData.TextManager.WriteColour($"Heroes with available promotions:", ColourTag.Default);

                for (int i = 0; i < promotionList.Count; i++)
                {
                    gameData.TextManager.WriteColour($"[{i}] {promotionList[i].ShortStats()}", ColourTag.Information);
                    PrintPromotionInfo(gameData, promotionList[i]);
                }
                string choice = gameData.TextManager.ReadKey().ToLower();

                if (int.TryParse(choice, out int iChoice) && iChoice >= 0 && iChoice < promotionList.Count)
                {
                    gameData.TextManager.ClearScreen();
                    gameData.TextManager.WriteColour($"Your gold: [{gameData.Gold}]", ColourTag.Alert);
                    PromoteHero(gameData, promotionList[iChoice]);
                }
                else
                {
                    gameData.TextManager.WriteColour("Returning to town.", ColourTag.Success);
                }
            }
            gameData.TextManager.ContinueAfterAnyKey();
        }

        internal static void PrintPromotionInfo(GameData gameData, Hero hero)
        {
            hero.UpdateUpgrades();

            int index = 0;
            foreach (Hero aHero in hero.UpgradeTypes)
            {
                gameData.TextManager.WriteColour($"\t- Can become [{aHero.GetType().Name}] for {aHero.BaseCost * hero.Level}g at lvl{aHero.AdvanceLevel}.", ColourTag.Emphasis);
                index++;
            }
        }
        internal static void PromoteHero(GameData gameData, Hero hero)
        {
            gameData.TextManager.WriteColour($"Choose promotion for {hero}", ColourTag.Information);
            int index = 0;
            foreach (Hero upgradeType in hero.UpgradeTypes)
            {
                gameData.TextManager.WriteColour($"{index} [{upgradeType.GetType().Name}]\tPrice: {upgradeType.BaseCost * hero.Level}g\t Required lvl: {upgradeType.AdvanceLevel}", ColourTag.Emphasis);
                index++;
            }
            string choice = gameData.TextManager.ReadKey();

            if (int.TryParse(choice, out int iChoice) && iChoice >= 0 && iChoice < hero.UpgradeTypes.Count)
            {
                //Hero upgradedHero = hero.UpgradeTypes[iChoice];

                //if (gameData.Gold >= upgradedHero.BaseCost * hero.Level && hero.Level >= upgradedHero.AdvanceLevel)
                //{
                //    find out how to get the correct upgrade type!
                //}
            }
        }
    }
}
