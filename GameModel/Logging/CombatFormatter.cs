using GameModel.Combat.Results;

namespace GameModel.Logging
{
    /// <summary>
    /// Responsible only for formatting combat results into human-readable strings.
    /// Separates formatting concern from output mechanism (Console, File, etc.).
    /// </summary>
    public class CombatFormatter
    {
        public string FormatAttack(AttackResult result)
        {
            return $"{result.AttackerName} attacks {result.TargetName} for {result.Damage} damage.";
        }

        public string FormatAbility(AbilityResult result)
        {
            return $"{result.UserName} uses {result.AbilityName} on {result.TargetName} dealing {result.DamageDealt} damage.";
        }

        public string FormatAbilityNonDamage(string userName, string abilityName, string targetName)
        {
            return $"{userName} uses {abilityName} on {targetName}.";
        }

        public string FormatAbilityNotFound(string userName, string abilityName)
        {
            return $"{userName} does not know {abilityName}.";
        }

        public string FormatHeal(HealResult result)
        {
            return $"{result.HealerName} heals for {result.Amount}.";
        }
    }
}
