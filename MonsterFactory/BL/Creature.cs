using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.DiceLogic;

namespace TheMonsterFactory.BL
{
    public abstract class Creature
    {
        public int BaseCost { get; set; } = 10;
        public string Name { get; set; }
        public int Health { get; set; }
        public int Level { get; set; } = 1;
        public int Experience { get; set; }
        public Die Die { get; set; }
        public bool IsDefending { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Move> MoveList { get; set; } = new();

        public Creature(string name, int level)
        {
            Name = name;
            Level = 0;
            Die = new D4();
            UpdateHealth();
            Moves.Find("Defend", MoveList);

            for (int i = 0; i < level; i++)
            {
                LevelUp();
            }
        }

        public virtual void UpdateHealth()
        {
            Health += Die.Roll(Level) + Level;
        }

        public virtual void AddXP(int xp, out string description)
        {
            description = $"{this} gained [{xp} xp].";
            Experience += xp;

            if (Experience > 10 * (Level * Level))
            {
                LevelUp();
                Experience = 0;
                description += $"\n{this} [levelled up to {Level}]!";
            }
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
            return $"Name: {$"[{Name} ({GetType().Name})]", -20}\n" +
                $"| Level: {Level, -20}\n" +
                $"| Health: {Health,-20}\n" +
                $"| Action Die: {Die.GetType().Name, -20}\n" +
                $"| Desciption: {Description,-20}\n";
        }
        public virtual string ShortStats()
        {
            return $"[{Name} (Lv.{Level} {GetType().Name})] HP: {Health}{(IsDefending == false ? null : " [(S)]")}";
        }
        public override string ToString()
        {
            return $"[{Name} ({GetType().Name})]";
        }

        public virtual string Defend()
        {
            IsDefending = true;
            return $"[{Name}] takes a defensive stance.";
        }
    }
}