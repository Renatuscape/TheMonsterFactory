namespace TheMonsterFactory.BL.Monsters
{
    public static class MonsterFactory
    {
        public static void MonsterNumberCheck(ref List<Monster> monsterList)
        {
            while (monsterList.Count < 4)
            {
                monsterList.Add(new Goblin("Goblin"));
            }
            Console.WriteLine("\nENEMY ATTACKERS\n");
            foreach (var monster in monsterList)
            {
                Console.WriteLine(monster);
            }
            Console.ReadKey();
        }
    }
}
