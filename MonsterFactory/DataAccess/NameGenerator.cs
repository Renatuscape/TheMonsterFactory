namespace TheMonsterFactory.DataAccess
{
    public class NameGenerator
    {
        public Random random = new Random();
        static List<string> PeopleNames { get; } = new()
        {
            "John","Jim", "Finn", "Carl", "Raymond", "Anna", "Michael", "Joanne", "Morag", "Charlie", "Whitney","Wilfred", "Earl", "Winston", "George", "Emma", "Rachel", "Harlan", "Terry", "Neil", "Ronald", "Raoul", "Ursula", "Sarah", "Lana", "Pam", "Salman", "Ibrahim", "Mo", "Al", "Sterling", "Tom", "Kim", "Alex", "Robin", "Luna", "Ida", "Georgina", "Olive", "Ophelia", "Ruben", "Sloane", "Siobhan", "Nieve", "Calem", "Isaac", "Arman", "Iman", "Christopher", "Sadie", "Hector" 
        };

        static List<string> MonsterNames { get; } = new()
        {
            "Grok", "Murmank", "Fronk", "Bralg", "Jort", "Smerdok", "Churl", "Arbadonk", "Urloz", "Wurtapek", "Mrug", "Johnathorn", "Nukbok", "Ybrodak", "Vopmin", "Pertunk", "Flobnug", "Rublik", "Nebrokum", "Narnagak", "Snorblin", "Rahactor", "Parlunk", "Qork", "Wruggle", "Ertibonk", "Rymput", "Torgodork", "Yflan", "Uwurubu", "Ixopax", "Olugwar", "Plagrot", "Azlurunt", "Swunq", "Drublock", "Fizfax", "Glunt", "Hargyle", "Judnop", "Kreequin", "Lonkmagok", "Zleep", "Xanathandos", "Cruplot", "Vubrog", "Bronkenbork", "Nublonib", "Munklonk"
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
