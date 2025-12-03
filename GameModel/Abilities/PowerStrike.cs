using GameModel.Characters;
using GameModel.Combat;

namespace GameModel.Abilities
{
    /// <summary>
    /// Power Strike ability that scales with the user's attack stat.
    /// Deals physical damage that can be reduced by armor.
    /// </summary>
    public class PowerStrike : Ability
    {
        public PowerStrike()
        {
            Name = "Power Strike";
        }

        public override CombatAction CalculateEffect(Character user, Character target)
        {
            var (userAtk, _, _) = user.GetFinalStats();
            int damage = System.Math.Max(0, userAtk * 2);
            
            return new CombatAction
            {
                Type = CombatAction.ActionType.PhysicalDamage,
                Amount = damage,
                Target = target,
                Description = $"{user.Name} unleashes a Power Strike on {target.Name}!"
            };
        }

        public override int Apply(Character user, Character target)
        {
            var (userAtk, targetArm, _) = user.GetFinalStats();
            int baseDamage = userAtk * 2;
            int damage = System.Math.Max(0, baseDamage - targetArm);
            
            target.TakeDamage(damage);
            return damage;
        }
    }
}