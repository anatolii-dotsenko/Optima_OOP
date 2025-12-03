using GameModel.Combat;

namespace GameModel.Logging
{
    /// <summary>
    /// Basic logging interface for combat information.
    /// Enables switching between console, file, UI, etc.
    /// </summary>
    public interface ICombatLogger
    {
        void LogAttack(AttackResult result);
        void LogAbility(AbilityResult result);
        void LogAbilityNonDamage(string userName, string abilityName, string targetName);
        void LogAbilityNotFound(string userName, string abilityName);
        void LogHeal(HealResult result);
    }
}
