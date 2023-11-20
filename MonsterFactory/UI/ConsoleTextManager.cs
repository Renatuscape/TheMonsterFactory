using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFactory.UI
{
    public class ConsoleTextManager : ITextManagement
    {
        public void ContinueAfterAnyKey()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            WriteLine(">> Press any key to continue");
            ReadKey();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public string ReadKey()
        {
            Console.Write("\t");
            return Console.ReadKey().KeyChar.ToString();
        }

        public string ReadLine()
        {
            Console.Write("\t");
            string input = Console.ReadLine() ?? "";
            return input;
        }

        public void Write(string text)
        {
            text = Tabulate(text);
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            text = Tabulate(text);
            Console.WriteLine(text);
        }

        public string Tabulate(string text)
        {
            return text = "\t" + text.Replace("\n", "\n\t");
        }
    }
}
