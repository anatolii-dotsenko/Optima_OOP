using GameModel.Characters;
using GameModel.Combat;

namespace GameModel.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for any ability that can be used in combat.
    /// </summary>
    public interface IAbility
    {
        string Name { get; }
        CombatAction CalculateEffect(Character user, Character target);
    }
}
