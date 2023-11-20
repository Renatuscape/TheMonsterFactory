using MonsterFactory.UI;
using TheMonsterFactory.BL.GamePlay;

namespace TheMonsterFactory.BL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleTextManager textManager = new();
            NewGame gameplay = new(textManager);
        }
    }
}