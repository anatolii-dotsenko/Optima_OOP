using GameModel.Characters;
using GameModel.Logging;

namespace GameModel.Combat
{
    /// <summary>
    /// Handles all combat logic: attacks, defenses, ability usage.
    /// Character class stores only data, while this class performs actions.
    /// </summary>
    public class CombatSystem
    {
        private readonly ICombatLogger _logger;

        public CombatSystem(ICombatLogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Performs a basic attack from attacker to defender.
        /// </summary>
        public void Attack(Character attacker, Character defender)
        {
            var (atk, arm, _) = defender.GetFinalStats();
            var (totalAtk, _, _) = attacker.GetFinalStats();

            int damage = System.Math.Max(0, totalAtk - arm);

            _logger.Write($"{attacker.Name} attacks {defender.Name} for {damage} damage.");
            defender.TakeDamage(damage);
        }

        /// <summary>
        /// Executes an ability from user onto target.
        /// </summary>
        public void UseAbility(Character user, Character target, string abilityName)
        {
            var ability = user.Abilities.Find(a => a.Name == abilityName);

            if (ability == null)
            {
                _logger.Write($"{user.Name} does not know {abilityName}.");
                return;
            }

            _logger.Write($"{user.Name} uses {ability.Name}!");
            ability.Apply(user, target);
        }

        /// <summary>
        /// Performs a healing action.
        /// </summary>
        public void Heal(Character healer, int amount)
        {
            healer.Heal(amount);
            _logger.Write($"{healer.Name} heals for {amount}.");
        }
    }
}
