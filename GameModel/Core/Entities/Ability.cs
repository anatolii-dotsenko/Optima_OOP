using GameModel.Characters;
using GameModel.Combat;

namespace GameModel.Abilities
{
    /// <summary>
    /// Abstract base class for all character abilities.
    /// Refactored: Abilities now calculate effects without mutating state directly.
    /// CombatSystem applies the result.
    /// </summary>
    public abstract class Ability
    {
        public string Name { get; protected set; }

        /// <summary>
        /// Calculate the effect without applying it.
        /// Returns a CombatAction describing what should happen.
        /// </summary>
        public abstract CombatAction CalculateEffect(Character user, Character target);

        /// <summary>
        /// Apply the ability effect. Can be overridden by subclasses.
        /// Default implementation calls CalculateEffect then applies the result.
        /// </summary>
        public virtual int Apply(Character user, Character target)
        {
            var action = CalculateEffect(user, target);
            if (action.Type == CombatAction.ActionType.PhysicalDamage || 
                action.Type == CombatAction.ActionType.MagicalDamage)
            {
                target.TakeDamage(action.Amount);
                return action.Amount;
            }
            return 0;
        }
    }
}