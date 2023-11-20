using TheMonsterFactory.BL.Monsters;

namespace TheMonsterFactory.BL.GamePlayLogic
{
    public class MonsterMaker : CreatureFactory
    {
        public override Monster Create(int level, string name = "")
        {
            name = nameGenerator.GetRandomName();
            return new Goblin(name, level);
        }
    }
}
