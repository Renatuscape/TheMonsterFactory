namespace TheMonsterFactory.BL.Heroes
{
    public class Hero : Creature, IAttack
    {
        public Hero(string name) : base(name)
        {
            Description = "A simple adventurer.";
        }

        public virtual int Attack(out string description)
        {
            description = $"{Name} swings their sword!";
            return Dice[Dice.Count - 1].Roll(Level);
        }

        public override string Move()
        {
            return $"{Name} rushes forwards!";
        }
    }
}