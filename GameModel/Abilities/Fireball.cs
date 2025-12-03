using GameModel.Characters;
using GameModel.Combat;

namespace GameModel.Abilities
{
    /// <summary>
    /// Fireball ability that deals fixed magical damage, ignoring armor.
    /// </summary>
    public class Fireball : Ability
    {
        public Fireball()
        {
            Name = "Fireball";
        }

        public override CombatAction CalculateEffect(Character user, Character target)
        {
            return new CombatAction
            {
                Type = CombatAction.ActionType.MagicalDamage,
                Amount = 75,
                Target = target,
                Description = $"{user.Name} casts Fireball on {target.Name}!"
            };
        }

        public override int Apply(Character user, Character target)
        {
            var action = CalculateEffect(user, target);
            target.TakeDamage(action.Amount);
            return action.Amount;
        }
    }
}