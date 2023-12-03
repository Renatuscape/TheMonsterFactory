using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation
{
    public class HeroMaker : CreatureFactory
    {
        public override Fighter CreateFighter(int level, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = nameGenerator.GetRandomName(true);
            }
            return new Fighter(name, level);
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
