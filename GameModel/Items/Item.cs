using System.Collections.Generic;
using GameModel.Abilities;
using GameModel.Characters;

namespace GameModel.Items
{
    /// <summary>
    /// Represents an equippable item with extensible stat modifiers.
    /// New stat types can be added via StatModifier subclasses without modifying this class.
    /// </summary>
    public class Item
    {
        public string Name { get; }
        public List<StatModifier> Modifiers { get; } = new();
        public Ability? GrantedAbility { get; protected set; }

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
