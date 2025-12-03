using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Core.Contracts
{
    public interface ICombatSystem
    {
        AttackResult Attack(ICombatEntity attacker, ICombatEntity defender);
        AbilityResult UseAbility(ICombatEntity user, ICombatEntity target, Ability ability);
        HealResult Heal(ICombatEntity healer, int amount);
    }
}