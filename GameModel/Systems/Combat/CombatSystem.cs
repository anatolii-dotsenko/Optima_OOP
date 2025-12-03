using GameModel.Core.Contracts;
using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Systems.Combat
{
    public class CombatSystem : ICombatSystem
    {
        private readonly ICombatLogger _logger;

        public CombatSystem(ICombatLogger logger)
        {
            _logger = logger;
        }

        public AttackResult Attack(ICombatEntity attacker, ICombatEntity defender)
        {
            var attStats = attacker.GetStats();
            var defStats = defender.GetStats();

            int atk = attStats.GetStat(StatType.Attack);
            int arm = defStats.GetStat(StatType.Armor);
            int damage = Math.Max(0, atk - arm);

            defender.TakeDamage(damage);

            var result = new AttackResult(attacker.Name, defender.Name, damage);
            _logger.LogAttack(result);
            return result;
        }

        public AbilityResult UseAbility(ICombatEntity user, ICombatEntity target, Ability ability)
        {
            var userStats = user.GetStats();
            var targetStats = target.GetStats();

            int damage = ability.CalculateDamage(userStats, targetStats);
            target.TakeDamage(damage);

            var result = new AbilityResult(user.Name, target.Name, ability.Name, damage);
            _logger.LogAbility(result);
            return result;
        }

        public HealResult Heal(ICombatEntity healer, int amount)
        {
            healer.Heal(amount);
            var result = new HealResult(healer.Name, amount);
            _logger.LogHeal(result);
            return result;
        }
    }
}