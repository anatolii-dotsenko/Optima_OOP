using System;
using System.Collections.Generic;
using System.Linq;
using GameModel.Core.Contracts;
using GameModel.Core.Data;
using GameModel.Core.ValueObjects;

namespace GameModel.Core.Entities
{
    public abstract class Character : ICombatEntity
    {
        public string Name { get; }
        private readonly CharacterStats _baseStats;
        private readonly List<Item> _equipment = new();
        private readonly List<Ability> _abilities = new();

        protected Character(string name)
        {
            Name = name;
            _baseStats = new CharacterStats();
        }

        // Helper to set initial stats in constructor
        protected void SetBaseStat(StatType type, int value) => _baseStats.SetStat(type, value);

        public bool IsAlive => GetCurrentHealth() > 0;

        private int GetCurrentHealth() => _baseStats.GetStat(StatType.Health);

        public void TakeDamage(int amount)
        {
            int current = GetCurrentHealth();
            _baseStats.SetStat(StatType.Health, Math.Max(0, current - amount));
        }

        public void Heal(int amount)
        {
            int current = GetCurrentHealth();
            int max = _baseStats.GetStat(StatType.MaxHealth); 
            _baseStats.SetStat(StatType.Health, Math.Min(max, current + amount));
        }

        public void EquipItem(Item item)
        {
            _equipment.Add(item);
            if (item.GrantedAbility != null)
            {
                _abilities.Add(item.GrantedAbility);
            }
        }

        public void LearnAbility(Ability ability) => _abilities.Add(ability);

        public CharacterStats GetStats()
        {
            // Clone base stats
            var finalStats = new CharacterStats(_baseStats);

            // Apply item modifiers
            foreach (var item in _equipment)
            {
                foreach (var mod in item.Modifiers)
                {
                    finalStats.ModifyStat(mod.Key, mod.Value);
                }
            }
            return finalStats;
        }

        public IEnumerable<Ability> GetAbilities() => _abilities;

        // --- NEW: Export to DTO ---
        public virtual CharacterData ToData()
        {
            return new CharacterData
            {
                Name = this.Name,
                ClassType = this.GetType().Name,
                CurrentHealth = this.GetCurrentHealth(),
                BaseStats = _baseStats.ToDictionary(),
                InventoryItems = _equipment.Select(i => i.Name).ToList()
            };
        }
    }
}