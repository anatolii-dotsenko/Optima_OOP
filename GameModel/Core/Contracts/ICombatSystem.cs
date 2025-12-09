// defines the contract for the core combat logic and event subscriptions
using GameModel.Core.ValueObjects;

namespace GameModel.Core.Contracts
{
    public interface ICombatSystem
    {
        // pattern observer (events)
        event Action<AttackResult> OnAttackPerformed;
        event Action<AbilityResult> OnAbilityUsed;
        event Action<HealResult> OnHealed;

        void Attack(ICombatEntity attacker, ICombatEntity defender);
        void UseAbility(ICombatEntity user, ICombatEntity target, Entities.Ability ability);
        void Heal(ICombatEntity healer, int amount);
    }
}