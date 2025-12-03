using GameModel.Core.Contracts;
using GameModel.Core.ValueObjects;

namespace GameModel.Infrastructure.Logging
{
    public class CompositeLogger : ICombatLogger
    {
        private readonly List<ICombatLogger> _loggers = new();

        public void Add(ICombatLogger logger) => _loggers.Add(logger);

        public void LogAttack(AttackResult result) => _loggers.ForEach(l => l.LogAttack(result));
        public void LogAbility(AbilityResult result) => _loggers.ForEach(l => l.LogAbility(result));
        public void LogHeal(HealResult result) => _loggers.ForEach(l => l.LogHeal(result));
        public void LogMessage(string message) => _loggers.ForEach(l => l.LogMessage(message));
    }
}