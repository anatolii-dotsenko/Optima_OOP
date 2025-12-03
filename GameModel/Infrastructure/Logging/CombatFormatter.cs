using GameModel.Core.ValueObjects;

namespace GameModel.Infrastructure.Logging
{
    public class CombatFormatter
    {
        public string Format(AttackResult r) => $"{r.Attacker} attacks {r.Target} for {r.Damage} damage.";
        public string Format(AbilityResult r) => $"{r.User} casts {r.AbilityName} on {r.Target} for {r.Damage} damage.";
        public string Format(HealResult r) => $"{r.Healer} heals for {r.Amount}.";
    }
}