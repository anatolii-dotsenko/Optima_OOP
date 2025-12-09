// defines the template method for ability execution and damage calculation.
using GameModel.Core.ValueObjects;

namespace GameModel.Core.Entities
{

    public abstract class Ability
    {
        public string Name { get; }

        protected Ability(string name)
        {
            Name = name;
        }

        // template method
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

        // primitive operation (Abstract)
        protected abstract int CalculateDamage(CharacterStats userStats, CharacterStats targetStats);

        // hooks (optional overrides)
        protected virtual bool CanUse(CharacterStats userStats) => true;
        protected virtual void OnAbilityUsed(int damageDealt) { }
    }
}