using GameModel.Characters;
using GameModel.Combat.Results;

namespace GameModel.Core.Contracts
{
    /// <summary>
    /// Interface for combat system operations.
    /// Allows dependency injection and decoupling from concrete implementation.
    /// </summary>
    public interface ICombatSystem
    {
        AttackResult Attack(Character attacker, Character defender);
        AbilityResult UseAbility(Character user, Character target, string abilityName);
        AbilityResult UseAbility(Character user, Character target, GameModel.Abilities.Ability ability);
        HealResult Heal(Character healer, int amount);
    }
}
