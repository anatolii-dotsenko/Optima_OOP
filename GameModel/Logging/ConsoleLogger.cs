using System;
using GameModel.Combat.Results;

namespace GameModel.Logging
{
    /// <summary>
    /// Simple implementation that logs to the console.
    /// Delegates formatting to CombatFormatter (SRP).
    /// </summary>
    public class ConsoleLogger : ICombatLogger
    {
        private readonly CombatFormatter _formatter;

        public ConsoleLogger()
        {
            _formatter = new CombatFormatter();
        }

        public void LogAttack(AttackResult result)
        {
            Console.WriteLine(_formatter.FormatAttack(result));
        }

        public void LogAbility(AbilityResult result)
        {
            Console.WriteLine(_formatter.FormatAbility(result));
        }

        public void LogAbilityNonDamage(string userName, string abilityName, string targetName)
        {
            Console.WriteLine(_formatter.FormatAbilityNonDamage(userName, abilityName, targetName));
        }

        public void LogAbilityNotFound(string userName, string abilityName)
        {
            Console.WriteLine(_formatter.FormatAbilityNotFound(userName, abilityName));
        }

        public void LogHeal(HealResult result)
        {
            Console.WriteLine(_formatter.FormatHeal(result));
        }
    }
}
