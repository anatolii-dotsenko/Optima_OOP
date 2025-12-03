namespace GameModel.Characters
{
    /// <summary>
    /// Immutable container for base character stats.
    /// </summary>
    public class CharacterStats
    {
        public int Attack { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }

        public CharacterStats(int attack, int armor, int health)
        {
            Attack = attack;
            Armor = armor;
            Health = health;
        }

        /// <summary>
        /// Creates a copy of these stats for calculation purposes.
        /// </summary>
        public CharacterStats Copy()
        {
            return new CharacterStats(Attack, Armor, Health);
        }
    }
}
