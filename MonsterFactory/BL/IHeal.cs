namespace TheMonsterFactory.BL
{
    public interface IHeal
    {
        public int HealOthers(out string description);
        public int HealSelf(out string description);
    }
}