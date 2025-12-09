using GameModel.Core.Contracts;
using GameModel.Core.Data;
using GameModel.Core.Entities.States;
using GameModel.Core.ValueObjects;
using GameModel.Infrastructure.CLI.Rendering;

namespace GameModel.Core.Entities
{
    public abstract class Character : ICombatEntity, IRenderable<CharacterData>
    {
        public string Name { get; }

        // Internal access for States to modify stats
        internal CharacterStats StatsInternal { get; }

        protected readonly List<Item> _equipment = new();
        protected readonly List<Ability> _abilities = new();

        // Pattern: State
        private ICharacterState _state;

        protected Character(string name)
        {
            Name = name;
            StatsInternal = new CharacterStats();
            _state = new AliveState(); // Default state
        }

        // State Management
        public void SetState(ICharacterState state) => _state = state;

        // Delegating actions to the current State
        public bool IsAlive => _state is AliveState;

        public void TakeDamage(int amount) => _state.TakeDamage(this, amount);

        public void Heal(int amount) => _state.Heal(this, amount);

        public void SetBaseStat(StatType type, int value) => StatsInternal.SetStat(type, value);

        public void EquipItem(Item item)
        {
            _equipment.Add(item);
            if (item.GrantedAbility != null) _abilities.Add(item.GrantedAbility);
        }

        public void LearnAbility(Ability ability) => _abilities.Add(ability);

        public IEnumerable<Ability> GetAbilities() => _abilities;


        public void UseRenderer(IRenderer<CharacterData> renderer)
        {
            renderer.Render(this.ToData());
        }
        public CharacterStats GetStats()
        {
            var finalStats = new CharacterStats(StatsInternal);
            foreach (var item in _equipment)
            {
                foreach (var mod in item.Modifiers)
                {
                    finalStats.ModifyStat(mod.Key, mod.Value);
                }
            }
            return finalStats;
        }

        // --- Pattern: Memento ---
        public IMemento Save()
        {
            return new CharacterMemento(
                Name,
                this.GetType().Name,
                StatsInternal.GetStat(StatType.Health),
                StatsInternal.ToDictionary(),
                _equipment.Select(i => i.Name).ToList()
            );
        }

        public void Restore(IMemento memento)
        {
            if (memento is not CharacterMemento cm) throw new ArgumentException("Invalid memento");

            StatsInternal.SetStat(StatType.Health, cm.CurrentHealth);
            // Logic to restore stats/items would go here
        }

        // --- RESTORED METHOD: Export to DTO for Save System ---
        public virtual CharacterData ToData()
        {
            return new CharacterData
            {
                Name = this.Name,
                ClassType = this.GetType().Name,
                CurrentHealth = StatsInternal.GetStat(StatType.Health),
                BaseStats = StatsInternal.ToDictionary(),
                InventoryItems = _equipment.Select(i => i.Name).ToList()
            };
        }
    }
}