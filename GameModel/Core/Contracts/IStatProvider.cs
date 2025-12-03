using GameModel.Core.ValueObjects;

namespace GameModel.Core.Contracts
{
    /// <summary>
    /// Defines a contract for objects that provide stat modifiers.
    /// Used by Items, Buffs, or environmental effects to alter Character stats.
    /// </summary>
    public interface IStatProvider
    {
        /// <summary>
        /// Retrieves the collection of stat modifiers provided by this object.
        /// </summary>
        /// <returns>A dictionary mapping StatType to the modifier value.</returns>
        Dictionary<StatType, int> GetModifiers();
    }
}