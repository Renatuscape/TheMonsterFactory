using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Monsters;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation
{
    public class MonsterMaker : CreatureFactory
    {
        public override Monster CreateFighter(int level, string name = "")
        {
            name = nameGenerator.GetRandomName();
            int randomRoll = random.Next(0, 100);

            if (randomRoll > 95)
            {
                return new Vileblade(name, level);
            }
            else if (randomRoll > 80)
            {
                return new Spectre(name, level);
            }
            else if (randomRoll > 65)
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
