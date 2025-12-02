using GameModel.Characters;

namespace GameModel.Abilities
{
    /// <summary>
    /// Deals fixed magical damage ignoring armor.
    /// </summary>
    public class Fireball : Ability
    {
        public Fireball() : base("Fireball") { }

        public override void Apply(Character user, Character target)
        {
            target.TakeDamage(25);
        }
    }
}
