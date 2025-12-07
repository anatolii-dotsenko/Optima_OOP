using System;
using GameModel.Core.Contracts;
using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Systems.Combat
{
    public class CombatSystem : ICombatSystem
    {
        // Observer: Events for subscribers
        public event Action<AttackResult>? OnAttackPerformed;
        public event Action<AbilityResult>? OnAbilityUsed;
        public event Action<HealResult>? OnHealed;

        // Note: Removed direct ICombatLogger dependency to decouple logic from IO.

        public void Attack(ICombatEntity attacker, ICombatEntity defender)
        {
            var attStats = attacker.GetStats();
            var defStats = defender.GetStats();

            int atk = attStats.GetStat(StatType.Attack);
            int arm = defStats.GetStat(StatType.Armor);
            int damage = Math.Max(0, atk - arm);

            defender.TakeDamage(damage);

            var result = new AttackResult(attacker.Name, defender.Name, damage);
            
            // Notify Observers
            OnAttackPerformed?.Invoke(result);
        }

        public void UseAbility(ICombatEntity user, ICombatEntity target, Ability ability)
        {
            var userStats = user.GetStats();
            var targetStats = target.GetStats();

            // Ability uses Template Method internally
            int damage = ability.Apply(userStats, targetStats);
            target.TakeDamage(damage);

            var result = new AbilityResult(user.Name, target.Name, ability.Name, damage);
            
            // Notify Observers
            OnAbilityUsed?.Invoke(result);
        }

        public void Heal(ICombatEntity healer, int amount)
        {
            healer.Heal(amount);
            var result = new HealResult(healer.Name, amount);
            
            // Notify Observers
            OnHealed?.Invoke(result);
        }
    }
}