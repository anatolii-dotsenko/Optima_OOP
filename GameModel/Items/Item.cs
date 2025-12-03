using System.Collections.Generic;
using GameModel.Characters;

namespace GameModel.Items
{
    /// <summary>
    /// Base class for all equippable items.
    /// Responsible only for stat modifiers, not ability granting.
    /// </summary>
    public class Item
    {
        public string Name { get; }
        public List<StatModifier> Modifiers { get; } = new();

        public Item(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Applies all modifiers to the given stats.
        /// </summary>
        public void ApplyModifiers(CharacterStats stats)
        {
            foreach (var modifier in Modifiers)
            {
                modifier.Apply(stats);
            }
        }
    }
}
