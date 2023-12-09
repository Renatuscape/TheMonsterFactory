using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.DiceLogic;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation
{
    public abstract class Creature
    {
        public int BaseCost { get; set; } = 10;
        public string Name { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public int HealthMultiplier { get; set; } = 1;
        public int Level { get; set; } = 1;
        public int Experience { get; set; }
        public Die ActionDie { get; set; }
        public Die HealthDie { get; set; }
        public bool IsDefending { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Move> MoveList { get; set; } = new();

        public int Evasiveness { get; set; } = 5;

        public Creature(string name, int level)
        {
            Name = name;
            Level = 0;
            ActionDie = new D4();
            HealthDie = new D4();

            UpdateHealth();
            Moves.Find("Defend", MoveList);

            for (int i = 0; i < level; i++)
            {
                LevelUp();
            }
        }

        public virtual void UpdateHealth()
        {
            MaxHealth = HealthDie.Roll(HealthMultiplier) + Level;
            CurrentHealth = MaxHealth;
        }

        public virtual void AddXP(int xp, out string xpText, out string? levelUpText)
        {
            levelUpText = null;
            xpText = $"{this} gained [{xp} xp].";
            Experience += xp;

            if (Experience > 10 * Level * Level)
            {
                LevelUp();
                Experience = 0;
                levelUpText = $"{this} reached [level {Level}]!";
            }
        }
        public virtual void LevelUp()
        {
            if (Level < 20)
            {
                Level++;

                if (Level >= 18)
                {
                    HealthMultiplier = 7;
                }
                else if (Level >= 14)
                {
                    HealthMultiplier = 6;
                }
                else if (Level >= 10)
                {
                    HealthMultiplier = 5;
                }
                else if (Level >= 6)
                {
                    HealthMultiplier = 4;
                }
                else if (Level >= 4)
                {
                    HealthMultiplier = 3;
                }
                else if (Level >= 2)
                {
                    HealthMultiplier = 2;
                }
            }
            UpdateHealth();
        }

        public virtual string FullStats()
        {
            return $"Name: {$"[{Name} ({GetType().Name})]",-20}\n" +
                $"| Level: {Level,-20}\n" +
                $"| Health: {$"{CurrentHealth}/{MaxHealth} ({HealthDie.GetType().Name})",-20}\n" +
                $"| Action Die: {ActionDie.GetType().Name,-20}\n" +
                $"| Desciption: {Description,-20}\n";
        }
        public virtual string ShortStats()
        {
            return $"[{Name} (Lv.{Level} {GetType().Name})] HP: {CurrentHealth}/{MaxHealth}{(IsDefending == false ? null : " [(S)]")}";
        }
        public override string ToString()
        {
            return $"[{Name} ({GetType().Name})]";
        }
    }
}