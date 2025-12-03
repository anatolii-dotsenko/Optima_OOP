using System.Collections.Generic;
using GameModel.Characters;

namespace GameModel.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for objects that provide stat modifiers.
    /// Allows Items, Buffs, and other effects to modify character stats.
    /// </summary>
    public interface IStatProvider
    {
        IEnumerable<StatModifier> GetModifiers();
    }
}
