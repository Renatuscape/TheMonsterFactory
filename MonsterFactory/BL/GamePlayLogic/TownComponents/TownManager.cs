using MonsterFactory.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.GamePlay;

namespace TheMonsterFactory.BL.GamePlayLogic.ShopComponents
{
    public static class TownManager
    {
        static List<Action<GameData>> menuChoices = new()
        {
            Tavern.VisitTavern,
            Alchemist.VisitAlchemist,
            Academy.VisitAcademy
        };

        public static void OpenMenu(GameData gameData)
        {
            string choice = string.Empty;

            while (choice != "y" && choice != "n")
            {
                gameData.TextManager.WriteColour("Return to town and replenish your supplies? [(Y/N)]", ColourTag.Information);
                choice = gameData.TextManager.ReadKey().ToLower();
                if (choice != "y" && choice != "n")
                {
                    gameData.TextManager.WriteColour("[Invalid choice].", ColourTag.Subtle);
                    gameData.TextManager.ContinueAfterAnyKey();
                }
            }
            if (choice == "n")
            {
                gameData.TextManager.WriteColour("You charge towards the next wave of enemies!", ColourTag.Information);
            }
            else if (choice == "y")
            {
                gameData.TextManager.WriteColour("", ColourTag.Information);
                PrintMenu();
            }

            void PrintMenu()
            {
                while (true)
                {
                    for (int menuIndex = 0; menuIndex < menuChoices.Count; menuIndex++)
                    {
                        gameData.TextManager.WriteColour($"[{menuIndex}] {menuChoices[menuIndex].Method.Name}", ColourTag.Information);
                    }
                    gameData.TextManager.WriteColour($"[x] Leave", ColourTag.Alert);

                    choice = gameData.TextManager.ReadKey().ToLower();

                    if (choice == "x")
                    {
                        break;
                    }
                    else if (int.TryParse(choice, out int parsedIndex) && parsedIndex >= 0 && parsedIndex < menuChoices.Count)
                    {
                        menuChoices[parsedIndex](gameData);
                    }
                    else
                    {
                        gameData.TextManager.WriteColour("[Invalid choice].", ColourTag.Subtle);
                        gameData.TextManager.ContinueAfterAnyKey();
                    }
                }
                gameData.TextManager.WriteColour($"You return to the battlefield, feeling refreshed.", ColourTag.Default);
                gameData.TextManager.ContinueAfterAnyKey();
            }
        }
    }
}
