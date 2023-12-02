using TheMonsterFactory.BL.Monsters;

namespace TheMonsterFactory.BL.GamePlayLogic
{
    public class MonsterMaker : CreatureFactory
    {
        public override Monster CreateFighter(int level, string name = "")
        {
            name = nameGenerator.GetRandomName();
            if (random.Next(0, 100) > 80)
            {
                return new Spectre(name, level);
            }
            else if (random.Next(0, 100) > 70)
            {
                return new Bolethan(name, level);
            }
            else
            {
                return new Goblin(name, level);
            }
        }
    }
}
