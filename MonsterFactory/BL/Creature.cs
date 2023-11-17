using TheMonsterFactory.BL.DiceLogic;

namespace TheMonsterFactory.BL
{
    public abstract class Creature
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Level { get; set; } = 1;
        public List<Die> Dice { get; set; } = new();
        public string Description { get; set; } = string.Empty;
        public Creature(string name)
        {
            Name = name;
            Dice.Add(new D4());
            UpdateHealth();
        }

        public virtual void UpdateHealth()
        {
            foreach (Die die in Dice)
            {
                Health += die.Roll(Level) + Level;
            }
        }

        public virtual void LevelUp()
        {
            if (Level < 20)
            {
                Level++;

                if (Level >= 18)
                {
                    Dice.Add(new D12());
                }
                else if (Level >= 14)
                {
                    Dice.Add(new D10());
                }
                else if (Level >= 10)
                {
                    Dice.Add(new D8());
                }
                else if (Level >= 6)
                {
                    Dice.Add(new D6());
                }
            }
            UpdateHealth();
        }

        public override string ToString()
        {
            return $"Name: {Name} ({GetType().Name})\n" +
                 $"| Level: {string.Format("{0:d2}", Level)}" +
                 $"| Health: {string.Format("{0:d3}", Health)}" +
                 $"| Action Die: {Dice[Dice.Count-1].GetType().Name}| Desciption: {Description}\n";
        }
    }
}