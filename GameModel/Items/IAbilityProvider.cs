using GameModel.Abilities;

namespace GameModel.Items
{
    /// <summary>
    /// Interface for items that grant abilities.
    /// Only items that actually grant abilities implement this (ISP).
    /// </summary>
    public interface IAbilityProvider
    {
        Ability GetAbility();
    }
}
