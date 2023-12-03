using TheMonsterFactory.BL.CombatMoves;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation;

namespace TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Heroes
{
    public abstract class Hero : Creature
    {
        public Hero(string name, int level) : base(name, level)
        {

        }
    }
}