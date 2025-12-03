namespace GameModel.Combat
{
    public class AttackResult
    {
        public required string AttackerName { get; set; }
        public required string TargetName { get; set; }
        public int Damage { get; set; }

        public AttackResult(string attackerName, string targetName, int damage)
        {
            AttackerName = attackerName;
            TargetName = targetName;
            Damage = damage;
        }
    }

    public class AbilityResult
    {
        public required string UserName { get; set; }
        public required string AbilityName { get; set; }
        public required string TargetName { get; set; }
        public int DamageDealt { get; set; }

        public AbilityResult(string userName, string abilityName, string targetName, int damageDealt)
        {
            UserName = userName;
            AbilityName = abilityName;
            TargetName = targetName;
            DamageDealt = damageDealt;
        }
    }

    public class HealResult
    {
        public required string HealerName { get; set; }
        public int Amount { get; set; }

        public HealResult(string healerName, int amount)
        {
            HealerName = healerName;
            Amount = amount;
        }
    }
}
