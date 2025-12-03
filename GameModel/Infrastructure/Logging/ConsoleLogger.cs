using GameModel.Core.Contracts;
using GameModel.Core.ValueObjects;

namespace GameModel.Infrastructure.Logging
{
    public class ConsoleLogger : ICombatLogger
    {
        private readonly CombatFormatter _formatter = new();

        public void LogAttack(AttackResult result) => Console.WriteLine(_formatter.Format(result));
        public void LogAbility(AbilityResult result) => Console.WriteLine(_formatter.Format(result));
        public void LogHeal(HealResult result) => Console.WriteLine(_formatter.Format(result));
        public void LogMessage(string message) => Console.WriteLine(message);
    }
}