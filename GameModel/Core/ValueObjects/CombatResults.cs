namespace GameModel.Core.ValueObjects
{
    public record AttackResult(string Attacker, string Target, int Damage);
    public record AbilityResult(string User, string Target, string AbilityName, int Damage);
    public record HealResult(string Healer, int Amount);
}