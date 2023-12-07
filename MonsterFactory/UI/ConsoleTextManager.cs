using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            WriteColour(text, ColourTag.Information, false);
        }

        public void WriteLine(string text)
        {
            WriteColour(text, ColourTag.Information);
        }

        public string Tabulate(string text)
        {
            return text = "\t" + text.Replace("\n", "\n\t");
        }

        public void ClearScreen()
        {
            Console.Clear();
        }
        public void WriteColour(string text, ColourTag colourTag, bool isNewLine = true, bool isTabulated = true)
        {
            if (isTabulated)
            {
                text = Tabulate(text);
            }

            ConsoleColor originalColor = Console.ForegroundColor;
            MatchCollection matches = Regex.Matches(text, @"\[(.*?)\]");
            int lastIndex = 0;

            foreach (Match match in matches)
            {
                // Print the text before the match
                Console.Write(text.Substring(lastIndex, match.Index - lastIndex));

                // Set color for text within square brackets
                Console.ForegroundColor = ColourTagToConsole(colourTag);

                // Write the text within square brackets
                Console.Write(match.Groups[1].Value);

                // Reset color to the original
                Console.ForegroundColor = originalColor;

                lastIndex = match.Index + match.Length;
            }

            // Print any remaining text after the last match
            if (lastIndex < text.Length)
            {
                Console.Write(text.Substring(lastIndex));
            }

            if (isNewLine)
            {
                Console.WriteLine();
            }
        }

        static ConsoleColor ColourTagToConsole(ColourTag colourTag)
        {
            if (colourTag == ColourTag.Information)
            {
                return ConsoleColor.Cyan;
            }
            else if (colourTag == ColourTag.Alert)
            {
                return ConsoleColor.Yellow;
            }
            else if (colourTag == ColourTag.Critical)
            {
                return ConsoleColor.Red;
            }
            else if (colourTag == ColourTag.Success)
            {
                return ConsoleColor.Green;
            }
            else if (colourTag == ColourTag.SmallSuccess)
            {
                return ConsoleColor.DarkGreen;
            }
            else if (colourTag == ColourTag.Emphasis)
            {
                return ConsoleColor.Magenta;
            }
            else if (colourTag == ColourTag.Subtle)
            {
                return ConsoleColor.Gray;
            }
            else if (colourTag == ColourTag.Disabled)
            {
                return ConsoleColor.DarkGray;
            }
            return ConsoleColor.White;
        }
    }
}
