namespace TheMonsterFactory.BL
{
    public interface IHeal
    {
        public int HealOthers(out string description);
        public int Heal(out string description);
    }
}