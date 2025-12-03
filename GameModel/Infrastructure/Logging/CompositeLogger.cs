using System;
using System.Collections.Generic;
using GameModel.Combat.Results;

namespace GameModel.Logging
{
    /// <summary>
    /// Composite logger that delegates to multiple loggers simultaneously.
    /// Enables logging to console AND file without modifying calling code (OCP).
    /// </summary>
    public class CompositeLogger : ICombatLogger, ILogger
    {
        private readonly List<ICombatLogger> _combatLoggers;
        private readonly List<ILogger> _genericLoggers;

        public CompositeLogger()
        {
            _combatLoggers = new();
            _genericLoggers = new();
        }

        public void AddCombatLogger(ICombatLogger logger) => _combatLoggers.Add(logger);
        public void AddGenericLogger(ILogger logger) => _genericLoggers.Add(logger);

        // ICombatLogger implementation
        public void LogAttack(AttackResult result)
        {
            foreach (var logger in _combatLoggers)
                logger.LogAttack(result);
        }

        public void LogAbility(AbilityResult result)
        {
            foreach (var logger in _combatLoggers)
                logger.LogAbility(result);
        }

        public void LogAbilityNonDamage(string userName, string abilityName, string targetName)
        {
            foreach (var logger in _combatLoggers)
                logger.LogAbilityNonDamage(userName, abilityName, targetName);
        }

        public void LogAbilityNotFound(string userName, string abilityName)
        {
            foreach (var logger in _combatLoggers)
                logger.LogAbilityNotFound(userName, abilityName);
        }

        public void LogHeal(HealResult result)
        {
            foreach (var logger in _combatLoggers)
                logger.LogHeal(result);
        }

        // ILogger implementation
        public void LogInfo(string message)
        {
            foreach (var logger in _genericLoggers)
                logger.LogInfo(message);
        }

        public void LogWarning(string message)
        {
            foreach (var logger in _genericLoggers)
                logger.LogWarning(message);
        }

        public void LogError(string message)
        {
            foreach (var logger in _genericLoggers)
                logger.LogError(message);
        }

        public void LogDebug(string message)
        {
            foreach (var logger in _genericLoggers)
                logger.LogDebug(message);
        }
    }
}
