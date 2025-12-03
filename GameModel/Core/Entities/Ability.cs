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

        /// <summary>
        /// Calculates the effect of the ability. 
        /// Pure function: does not mutate state directly.
        /// </summary>
        public abstract int CalculateDamage(CharacterStats userStats, CharacterStats targetStats);
    }
}