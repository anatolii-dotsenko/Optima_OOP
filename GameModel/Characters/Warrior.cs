using GameModel.Abilities;

namespace GameModel.Characters
{
    /// <summary>
    /// Sturdy melee fighter with strong armor and a signature strike.
    /// </summary>
    public class Warrior : Character
    {
        public Warrior(string name)
            : base(name, maxHealth: 150, armor: 10, attack: 15)
        {
            // Unique class ability
            Abilities.Add(new PowerStrike());
        }
    }
}
