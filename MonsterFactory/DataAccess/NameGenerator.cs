namespace TheMonsterFactory.DataAccess
{
    public class NameGenerator
    {
        public Random random = new Random();
        static List<string> PeopleNames { get; } = new()
        {
            "John","Jim", "Finn", "Carl", "Raymond", "Anna", "Michael", "Joanne", "Morag", "Charlie"
        };

        static List<string> MonsterNames { get; } = new()
        {
            "Grok", "Murmank", "Fronk", "Bralg", "Jort", "Smerdok", "Churl", "Arbadonk", "Urloz", "Wurtapek"
        };

        public string GetRandomName(bool isPerson = false)
        {
            if (isPerson)
            {
                return PeopleNames[random.Next(PeopleNames.Count)];
            }
            else
            {
                return MonsterNames[random.Next(MonsterNames.Count)];
            }
        }
    }
}
