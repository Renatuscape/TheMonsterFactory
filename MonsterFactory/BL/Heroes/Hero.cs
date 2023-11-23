namespace TheMonsterFactory.BL.Heroes
{
    public class Hero : Creature, IAttack
    {
        public Hero(string name, int level) : base(name, level)
        {
            Description = "A simple adventurer.";
        }

        public virtual int Attack(out string description)
        {
            int amount = Die.Roll(Level);
            description = $"{Name} swings their sword for {amount} damage!";
            return amount;
        }
    }
}