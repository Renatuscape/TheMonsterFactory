using TheMonsterFactory.BL.Heroes;

namespace TheMonsterFactory.BL.GamePlayLogic
{
    public class HeroMaker : CreatureFactory
    {
        public override Hero Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = nameGenerator.GetRandomName(true);
            }
            return new Hero(name);
        }

        public Cleric CreateCleric(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = nameGenerator.GetRandomName();
            }
            return new Cleric(name);
        }
    }
}
