namespace GameModel.Abilities
{
    /// <summary>
    /// Base class for all abilities.
    /// Concrete abilities implement custom logic in Apply().
    /// </summary>
    public abstract class Ability
    {
        public string Name { get; }

        protected Ability(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Applies the ability effect.
        /// </summary>
        public abstract void Apply(
            Characters.Character user, 
            Characters.Character target
        );
    }
}
