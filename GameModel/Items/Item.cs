using System.Collections.Generic;
using GameModel.Characters;

namespace GameModel.Items
{
    /// <summary>
    /// Base class for all equippable items.
    /// Responsible for stat modifiers and ability granting.
    /// Provides both explicit properties (TS compliance) and extensible modifier system (OCP).
    /// </summary>
    public class Item
    {
        public string Name { get; }
        public List<StatModifier> Modifiers { get; } = new();

        // TS-compliant explicit properties for direct stat bonuses
        public virtual int AttackBonus { get; protected set; } = 0;
        public virtual int ArmorBonus { get; protected set; } = 0;
        public virtual int HealthBonus { get; protected set; } = 0;

        public Item(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Applies all modifiers to the given stats.
        /// Combines both explicit properties and dynamic modifiers.
        /// </summary>
        public void ApplyModifiers(CharacterStats stats)
        {
            // Apply explicit properties
            stats.Attack += AttackBonus;
            stats.Armor += ArmorBonus;
            stats.Health += HealthBonus;

            // Apply dynamic modifiers
            foreach (var modifier in Modifiers)
            {
                modifier.Apply(stats);
            }
        }
    }
}
