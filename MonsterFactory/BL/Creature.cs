using TheMonsterFactory.BL.DiceLogic;

namespace TheMonsterFactory.BL
{
    public abstract class Creature
    {
        public string Name { get; }
        public int Health { get; set; }
        public int Level { get; set; } = 1;
        public Die Die { get; set; }
        public bool IsDefending { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<string> ActionList { get; set; } = new()
        {
            "Attack",
            "Defend"
        };
        public Creature(string name, int level)
        {
            Name = name;
            Level = 0;
            Die = new D4();
            UpdateHealth();

            for (int i = 0; i < level; i++)
            {
                LevelUp();
            }
        }

        public virtual void UpdateHealth()
        {
            Health += Die.Roll(Level) + Level;
        }

        public virtual void LevelUp()
        {
            if (Level < 20)
            {
                Level++;

                if (Level >= 18)
                {
                    Die = new D12();
                }
                else if (Level >= 14)
                {
                    Die = new D10();
                }
                else if (Level >= 10)
                {
                    Die = new D8();
                }
                else if (Level >= 6)
                {
                    Die = new D6();
                }
            }
            UpdateHealth();
        }

        public virtual string FullStats()
        {
            return $"Name: {Name} ({GetType().Name})\n" +
                $"| Level: {string.Format("{0:d2}", Level)}\n" +
                $"| Health: {string.Format("{0:d3}", Health)}\n" +
                $"| Action Die: {Die.GetType().Name}\n" +
                $"| Desciption: {Description}\n";
        }
        public virtual string ShortStats()
        {
            return $"Name: {Name} (Lv.{Level} {GetType().Name}) HP: {Health}";
        }
        public override string ToString()
        {
            return $"{Name} ({GetType().Name})";
        }

        public virtual string Defend()
        {
            IsDefending = true;
            return $"{Name} takes a defensive stance.";
        }

        public virtual List<string> GetActionList()
        {
            return ActionList;
        }
    }
}