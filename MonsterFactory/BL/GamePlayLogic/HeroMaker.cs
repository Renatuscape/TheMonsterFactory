using TheMonsterFactory.BL.Heroes;

namespace TheMonsterFactory.BL.GamePlayLogic
{
    public class HeroMaker : CreatureFactory
    {
        public override Hero CreateFighter(int level, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = nameGenerator.GetRandomName(true);
            }
            return new Hero(name, level);
        }

        public Scribe CreateScribe(int level, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = nameGenerator.GetRandomName(true);
            }
            return new Scribe(name, level);
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
