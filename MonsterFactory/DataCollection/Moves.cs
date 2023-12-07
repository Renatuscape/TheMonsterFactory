namespace TheMonsterFactory.BL.CombatMoves
{
    public static class Moves
    {
        public static List<Move> All { get; set; } = new();

        public static void Find(string searchName, List<Move> moveList)
        {
            Move? foundMove = null;
            foreach (Move move in All)
            {
                if (move.Name.ToLower() == searchName.ToLower())
                {
                    foundMove = move;
                }
            }
            if (foundMove != null)
            {
                moveList.Add(foundMove);
            }
            else
            {
                Console.WriteLine("No move found contianing this phrase: " + searchName);
            }
        }

        public static void PopulateList()
        {
            Move Defend = new("Defend", "Takes a defensive stance.");
            Defend.MoveType = MoveType.Buff;
            Defend.BuffType = BuffType.Shield;
            Defend.CanTargetSelfOnly = true;
            All.Add(Defend);

            Move Staff = new("Staff", "Jabs with a staff.");
            Staff.DiceMultiplier = 0.8f;
            All.Add(Staff);

            Move Claws = new("Claws", "Swipes their claws.");
            Claws.Accuracy = 90;
            All.Add(Claws);

            Move IronSword = new("Iron Sword", "Swings their iron sword.");
            IronSword.DiceMultiplier = 1.5f;
            All.Add(IronSword);

            Move SteelSword = new("Steel Sword", "Swings their steel sword.");
            SteelSword.DiceMultiplier = 1.8f;
            All.Add(SteelSword);

            Move Potion = new ("Potion", "Drinks a potion to restore their own health");
            Potion.MoveType = MoveType.Heal;
            Potion.DiceMultiplier = 0.5f;
            Potion.CanTargetSelfOnly = true;
            All.Add(Potion);

            Move Cure = new("Cure", "Weaves a spell to restore health.");
            Cure.MoveType = MoveType.Heal;
            Cure.Target = TargetType.Ally;
            Cure.CanTargetSelf = true;
            All.Add(Cure);

            Move CureAll = new("Cure All", "Calls on a soothing spell to restore their companions.");
            CureAll.MoveType = MoveType.Heal;
            CureAll.Target = TargetType.Ally;
            CureAll.MaxTargets = 3;
            CureAll.IsRandomTarget = true;
            CureAll.DiceMultiplier = 0.35f;
            All.Add(CureAll);

            Move MagicMissile = new("Magic Missile", "Strikes the targets with magic missiles.");
            MagicMissile.MoveType = MoveType.DamageMagical;
            MagicMissile.MaxTargets = 4;
            MagicMissile.IsRandomTarget = true;
            MagicMissile.DiceMultiplier = 0.35f;
            MagicMissile.Accuracy = 200;
            All.Add(MagicMissile);

            Move SweepingTentacle = new("Sweeping Tentacle", "Sweeps across the field with a long tentacle.");
            SweepingTentacle.MoveType = MoveType.DamageMagical;
            SweepingTentacle.MaxTargets = 3;
            SweepingTentacle.IsRandomTarget = true;
            SweepingTentacle.Accuracy = 80;
            SweepingTentacle.DiceMultiplier = 0.5f;
            All.Add(SweepingTentacle);

            Move Witchbolt = new("Witchbolt", "Strikes two targets with purple bolts.");
            Witchbolt.MoveType = MoveType.DamageMagical;
            Witchbolt.MaxTargets = 2;
            Witchbolt.DiceMultiplier = 0.7f;
            All.Add(Witchbolt);

            Move VileBlade = new("Vile Blade", "Cuts the target with a vile blade.");
            VileBlade.DiceMultiplier = 2.0f;
            All.Add(VileBlade);
        }
    }
}
