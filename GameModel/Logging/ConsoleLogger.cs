using System;
using GameModel.Combat;

namespace GameModel.Logging
{
    /// <summary>
    /// Simple implementation that logs to the console.
    /// </summary>
    public class ConsoleLogger : ICombatLogger
    {
        public void LogAttack(AttackResult result)
        {
            Console.WriteLine($"{result.AttackerName} attacks {result.TargetName} for {result.Damage} damage.");
        }

        public void LogAbility(AbilityResult result)
        {
            Console.WriteLine($"{result.UserName} uses {result.AbilityName} on {result.TargetName} dealing {result.DamageDealt} damage.");
        }

        public void LogAbilityNonDamage(string userName, string abilityName, string targetName)
        {
            Console.WriteLine($"{userName} uses {abilityName} on {targetName}.");
        }

        public void LogAbilityNotFound(string userName, string abilityName)
        {
            Console.WriteLine($"{userName} does not know {abilityName}.");
        }

        public void LogHeal(HealResult result)
        {
            Console.WriteLine($"{result.HealerName} heals for {result.Amount}.");
        }
    }
}
