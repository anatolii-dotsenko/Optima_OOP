using System.Collections.Generic;
using GameModel.Abilities;
using GameModel.Characters;

namespace GameModel.Items
{
    /// <summary>
    /// Base class for all items.
    /// For advanced systems you can derive Weapon, Armor, Amulet, etc.
    /// </summary>
    public abstract class Item
    {
        public string Name { get; }
        public List<StatModifier> Modifiers { get; } = new();

        public Ability? GrantedAbility { get; protected set; }

        protected Item(
            string name, 
            Ability? ability = null)
        {
            Name = name;
            GrantedAbility = ability;
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
