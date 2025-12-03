namespace GameModel.Combat
{
    public class AttackResult
    {
        public string AttackerName { get; set; }
        public string TargetName { get; set; }
        public int Damage { get; set; }
    }

    public class AbilityResult
    {
        public string UserName { get; set; }
        public string AbilityName { get; set; }
        public string TargetName { get; set; }
        public int DamageDealt { get; set; }
    }

    public class HealResult
    {
        public string HealerName { get; set; }
        public int Amount { get; set; }
    }
}
