using GameModel.Abilities;

namespace GameModel.Characters
{
    /// <summary>
    /// Weak in armor but powerful with destructive magic.
    /// </summary>
    public class Mage : Character
    {
        public Mage(string name)
            : base(name, maxHealth: 90, armor: 3, attack: 15)
        {
            Abilities.Add(new Fireball());
        }
    }
}
