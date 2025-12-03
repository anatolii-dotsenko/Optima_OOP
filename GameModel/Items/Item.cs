using System.Collections.Generic;
using GameModel.Characters;

namespace GameModel.Items
{
    /// <summary>
    /// Base class for all equippable items.
    /// Provides explicit stat bonus properties (TS compliance) and extensible modifier system (OCP).
    /// Exposes AttackBonus, ArmorBonus, and HealthBonus as required by the Technical Specification.
    /// </summary>
    public class Item
    {
        public string Name { get; }
        public List<StatModifier> Modifiers { get; } = new();

        // TS-compliant explicit properties for direct stat bonuses
        // Derived classes override these to provide specific values
        public virtual int AttackBonus { get; protected set; } = 0;
        public virtual int ArmorBonus { get; protected set; } = 0;
        public virtual int HealthBonus { get; protected set; } = 0;

        public Item(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Applies all modifiers to the given stats.
        /// Combines both explicit properties (TS) and dynamic modifiers (OCP).
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
