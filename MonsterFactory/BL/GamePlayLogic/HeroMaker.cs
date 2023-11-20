using TheMonsterFactory.BL.Heroes;

namespace TheMonsterFactory.BL.GamePlayLogic
{
    public class HeroMaker : CreatureFactory
    {
        public override Hero Create(int level, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = nameGenerator.GetRandomName(true);
            }
            return new Hero(name, level);
        }

        public Cleric CreateCleric(int level, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = nameGenerator.GetRandomName(true);
            }
            return new Cleric(name, level);
        }
    }
}
