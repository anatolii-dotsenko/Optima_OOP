using System.Collections.Generic;
using GameModel.Items;

namespace GameModel.Characters
{
    /// <summary>
    /// Base class for all playable and non-playable characters.
    /// Responsible only for storing stats and abilities.
    /// Combat logic is handled by CombatSystem.
    /// </summary>
    public abstract class Character
    {
        public string Name { get; }
        
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Armor { get; protected set; }
        public int AttackPower { get; protected set; }

        /// <summary>
        /// Holds all abilities that the character can use.
        /// </summary>
        public List<Abilities.Ability> Abilities { get; } = new();

        /// <summary>
        /// Holds currently equipped items.
        /// Full stat calculation is performed through GetFinalStats().
        /// </summary>
        public List<Items.Item> Equipment { get; } = new();

        protected Character(string name, int maxHealth, int armor, int attack)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = maxHealth;
            Armor = armor;
            AttackPower = attack;
        }

        /// <summary>
        /// Restores health without exceeding maximum allowed health.
        /// </summary>
        public void Heal(int amount)
        {
            Health = System.Math.Min(MaxHealth, Health + amount);
        }

        /// <summary>
        /// Reduces current health.
        /// </summary>
        public void TakeDamage(int amount)
        {
            Health = System.Math.Max(0, Health - amount);
        }

        /// <summary>
        /// Equips an item and grants its benefits (stat bonuses, abilities).
        /// </summary>
        public virtual void EquipItem(Items.Item item)
        {
            Equipment.Add(item);

            if (item.GrantedAbility != null)
                Abilities.Add(item.GrantedAbility);
        }

        /// <summary>
        /// Calculates final stats by applying all equipped item modifiers to base stats.
        /// </summary>
        public (int attack, int armor, int health) GetFinalStats()
        {
            var stats = _baseStats.Copy();

            foreach (var item in EquippedItems)
            {
                item.ApplyModifiers(stats);
            }

            return (stats.Attack, stats.Armor, stats.Health);
        }
    }
}
