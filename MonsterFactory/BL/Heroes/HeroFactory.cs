namespace TheMonsterFactory.BL.Heroes
{
    public static class HeroFactory
    {
        public static void HeroNumerCheck(ref List<Hero> heroList)
        {
            while (heroList.Count < 4)
            {
                var hasCleric = false;
                foreach (Hero hero in heroList)
                {
                    if (hero is Cleric)
                    {
                        hasCleric = true;
                    }
                }

                if (hasCleric)
                {
                    Console.WriteLine("\nPlease enter new Hero name\n");
                    var input = Console.ReadLine() ?? "John";
                    heroList.Add(new Hero(input));
                    Console.WriteLine("Fighter has joined the party.");
                }
                else
                {
                    Console.WriteLine("\nPlease enter new Cleric name\n");
                    var input = Console.ReadLine() ?? "Finn";
                    heroList.Add(new Cleric(input));
                    Console.WriteLine("Cleric has joined the party.");
                }
            }
            Console.WriteLine();
            Console.WriteLine("PARTY ROSTER:");
            Console.WriteLine();

            foreach (Hero hero in heroList)
            {
                Console.WriteLine(hero);
            }
            Console.ReadKey();
            Console.Clear();
        }
    }
}
