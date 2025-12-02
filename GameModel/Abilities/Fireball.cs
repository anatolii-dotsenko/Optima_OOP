using GameModel.Characters;

namespace GameModel.Abilities
{
    /// <summary>
    /// Deals fixed magical damage ignoring armor.
    /// </summary>
    public class Fireball : Ability
    {
        public Fireball() : base("Fireball") { }

        public override int Apply(Character user, Character target)
        {
            int damage = 75;
            target.TakeDamage(damage);
            return damage;
        }
    }
}