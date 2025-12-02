using GameModel.Characters;

namespace GameModel.Abilities
{
    /// <summary>
    /// A powerful melee blow dealing double attack damage.
    /// </summary>
    public class PowerStrike : Ability
    {
        public PowerStrike() : base("Power Strike") { }

        public override void Apply(Character user, Character target)
        {
            var (atk, _, _) = user.GetFinalStats();
            int dmg = atk * 2;
            target.TakeDamage(dmg);
        }
    }
}
