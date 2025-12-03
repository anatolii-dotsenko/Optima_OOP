using GameModel.Core.Contracts;
using GameModel.Core.ValueObjects;

namespace GameModel.Infrastructure.Logging
{
    public class ConsoleLogger : ICombatLogger
    {
        private readonly CombatFormatter _formatter = new();
        private readonly IDisplayer _displayer;

        public ConsoleLogger(IDisplayer displayer)
        {
            _displayer = displayer;
        }

        public void LogAttack(AttackResult result) => _displayer.WriteLine(_formatter.Format(result));
        public void LogAbility(AbilityResult result) => _displayer.WriteLine(_formatter.Format(result));
        public void LogHeal(HealResult result) => _displayer.WriteLine(_formatter.Format(result));
        public void LogMessage(string message) => _displayer.WriteLine(message);
    }
}