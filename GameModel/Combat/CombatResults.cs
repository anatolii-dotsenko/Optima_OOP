namespace GameModel.Combat
{
    public class AttackResult
    {
        public string AttackerName { get; set; } = string.Empty;
        public string TargetName { get; set; } = string.Empty;
        public int Damage { get; set; }
    }

    public class AbilityResult
    {
        public string UserName { get; set; } = string.Empty;
        public string AbilityName { get; set; } = string.Empty;
        public string TargetName { get; set; } = string.Empty;
        public int DamageDealt { get; set; }
    }

    public class HealResult
    {
        public string HealerName { get; set; } = string.Empty;
        public int Amount { get; set; }
    }
}
