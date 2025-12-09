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

            // 1. Physical Damage Calculation
            int attack = attStats.GetStat(StatType.Attack);
            int armor = defStats.GetStat(StatType.Armor);

            // Resistance is stored as Int (e.g., 20 = 20%). Converted to decimal for math.
            double resistance = defStats.GetStat(StatType.Resistance) / 100.0;

            // Formula: (Atk - Armor) * (1 - Res)
            int flatDamage = Math.Max(0, attack - armor);
            int finalDamage = (int)(flatDamage * (1.0 - resistance));

            defender.TakeDamage(finalDamage);

            OnAttackPerformed?.Invoke(new AttackResult(attacker.Name, defender.Name, finalDamage));
        }

        public void UseAbility(ICombatEntity user, ICombatEntity target, Ability ability)
        {
            var userStats = user.GetStats();
            var targetStats = target.GetStats();

            // Delegate calculation to Ability (Template Method), then apply results
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