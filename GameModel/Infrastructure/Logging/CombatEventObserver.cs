using GameModel.Core.Contracts;

namespace GameModel.Infrastructure.Logging
{
    /// <summary>
    /// Pattern: Observer (Concrete Observer).
    /// Listens to combat events and directs them to the logger.
    /// </summary>
    public class CombatEventObserver
    {
        private readonly ICombatLogger _logger;

        public CombatEventObserver(ICombatLogger logger, ICombatSystem system)
        {
            _logger = logger;
            // Subscribe to events
            system.OnAttackPerformed += result => _logger.LogAttack(result);
            system.OnAbilityUsed += result => _logger.LogAbility(result);
            system.OnHealed += result => _logger.LogHeal(result);
        }
    }
}