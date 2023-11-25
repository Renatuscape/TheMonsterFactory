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
            else
            {
                return new Goblin(name, level);
            }
        }
    }
}
