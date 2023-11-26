using TheMonsterFactory.BL.CombatMoves;

namespace TheMonsterFactory.BL.Heroes
{
    public abstract class Hero : Creature
    {
        public Hero(string name, int level) : base(name, level)
        {

        }
    }
}