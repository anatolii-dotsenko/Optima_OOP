using GameModel.Core.ValueObjects;

namespace GameModel.Core.Entities
{
    /// <summary>
    /// Pattern: Template Method.
    /// Defines the skeleton of the algorithm in the Apply method, 
    /// deferring specific steps (CalculateDamage, OnAbilityUsed) to subclasses.
    /// </summary>
    public abstract class Ability
    {
        public string Name { get; }

        protected Ability(string name)
        {
            Name = name;
        }

        // The Template Method
        public int Apply(CharacterStats userStats, CharacterStats targetStats)
        {
            // 1. Hook: Pre-calculation logic (e.g., consume mana - optionally implemented by subclasses)
            if (!CanUse(userStats))
            {
                return 0;
            }

            // 2. Abstract Step: Calculate logic (must be implemented)
            // This is where specific logic (like Magic Damage formulas) will be executed by subclasses.
            int damage = CalculateDamage(userStats, targetStats);

            // 3. Hook: Post-calculation effects
            OnAbilityUsed(damage);

            return damage;
        }

        // Primitive operation (Abstract)
        protected abstract int CalculateDamage(CharacterStats userStats, CharacterStats targetStats);

        // Hook (Virtual - optional override)
        protected virtual bool CanUse(CharacterStats userStats) => true;
        protected virtual void OnAbilityUsed(int damageDealt) { }
    }
}