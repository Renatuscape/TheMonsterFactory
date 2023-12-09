using MonsterFactory.UI;
using TheMonsterFactory.BL.GamePlay;
using TheMonsterFactory.Rendering;

namespace TheMonsterFactory.BL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleTextManager textManager = new();
            ConsoleRenderer render = new();
            NewGame gameplay = new(textManager, render);
        }
    }
}