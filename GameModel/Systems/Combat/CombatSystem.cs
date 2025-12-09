// implements the core math and logic for attacks and abilities
using GameModel.Core.Contracts;
using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Systems.Combat
{
    public class CombatSystem : ICombatSystem
    {
        public event Action<AttackResult>? OnAttackPerformed;
        public event Action<AbilityResult>? OnAbilityUsed;
        public event Action<HealResult>? OnHealed;

        public void Attack(ICombatEntity attacker, ICombatEntity defender)
        {
            var attStats = attacker.GetStats();
            var defStats = defender.GetStats();

            // physical damage calculation
            int attack = attStats.GetStat(StatType.Attack);
            int armor = defStats.GetStat(StatType.Armor);

            // resistance as a percentage
            double resistance = defStats.GetStat(StatType.Resistance) / 100.0;

            // formula: damage = (attack - armor) * (1 - resistance)
            int flatDamage = Math.Max(0, attack - armor);
            int finalDamage = (int)(flatDamage * (1.0 - resistance));

            defender.TakeDamage(finalDamage);

            OnAttackPerformed?.Invoke(new AttackResult(attacker.Name, defender.Name, finalDamage));
        }

        public void UseAbility(ICombatEntity user, ICombatEntity target, Ability ability)
        {
            var userStats = user.GetStats();
            var targetStats = target.GetStats();

            // template method usage delegation to Ability
            int damage = ability.Apply(userStats, targetStats);
            target.TakeDamage(damage);

            OnAbilityUsed?.Invoke(new AbilityResult(user.Name, target.Name, ability.Name, damage));
        }

        public void Heal(ICombatEntity healer, int amount)
        {
            healer.Heal(amount);
            OnHealed?.Invoke(new HealResult(healer.Name, amount));
        }
    }
}