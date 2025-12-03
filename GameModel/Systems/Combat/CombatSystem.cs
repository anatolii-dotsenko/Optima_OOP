using GameModel.Abilities;
using GameModel.Characters;
using GameModel.Logging;
using GameModel.Combat.Actions;
using GameModel.Combat.Results;
using GameModel.Core.Contracts;

namespace GameModel.Combat
{
    /// <summary>
    /// Pure Rules Engine for combat calculations and state mutations.
    /// Handles all combat logic: attacks, defenses, ability usage.
    /// Character class stores only data, while this class performs actions.
    /// </summary>
    public class CombatSystem : ICombatSystem
    {
        private readonly ICombatLogger _logger;

        public CombatSystem(ICombatLogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Performs a basic attack from attacker to defender.
        /// </summary>
        public AttackResult Attack(Character attacker, Character defender)
        {
            var (_, arm, _) = defender.GetFinalStats();
            var (totalAtk, _, _) = attacker.GetFinalStats();

            int damage = System.Math.Max(0, totalAtk - arm);
            defender.TakeDamage(damage);

            var result = new AttackResult 
            { 
                AttackerName = attacker.Name, 
                TargetName = defender.Name, 
                Damage = damage 
            };
            _logger.LogAttack(result);
            return result;
        }

        /// <summary>
        /// Executes an ability from user onto target by name (legacy).
        /// </summary>
        public AbilityResult UseAbility(Character user, Character target, string abilityName)
        {
            var ability = user.Abilities.Find(a => a.Name == abilityName);

            if (ability == null)
            {
                _logger.LogAbilityNotFound(user.Name, abilityName);
                return new AbilityResult
                {
                    UserName = user.Name,
                    AbilityName = abilityName,
                    TargetName = target.Name,
                    DamageDealt = 0
                };
            }

            int damageDealt = ability.Apply(user, target);

            var result = new AbilityResult
            {
                UserName = user.Name,
                AbilityName = ability.Name,
                TargetName = target.Name,
                DamageDealt = damageDealt
            };

            if (damageDealt <= 0)
            {
                _logger.LogAbilityNonDamage(user.Name, ability.Name, target.Name);
            }
            else
            {
                _logger.LogAbility(result);
            }

            return result;
        }

        /// <summary>
        /// Executes an ability by reference (type-safe, no magic strings).
        /// </summary>
        public AbilityResult UseAbility(Character user, Character target, Ability ability)
        {
            if (ability == null)
            {
                _logger.LogAbilityNotFound(user.Name, "Unknown");
                return new AbilityResult
                {
                    UserName = user.Name,
                    AbilityName = "Unknown",
                    TargetName = target.Name,
                    DamageDealt = 0
                };
            }

            int damageDealt = ability.Apply(user, target);
            var result = new AbilityResult
            {
                UserName = user.Name,
                AbilityName = ability.Name,
                TargetName = target.Name,
                DamageDealt = damageDealt
            };

            if (damageDealt <= 0)
            {
                _logger.LogAbilityNonDamage(user.Name, ability.Name, target.Name);
            }
            else
            {
                _logger.LogAbility(result);
            }

            return result;
        }

        /// <summary>
        /// Performs a healing action on the specified character.
        /// </summary>
        public HealResult Heal(Character healer, int amount)
        {
            int actualAmount = amount < 0 ? 0 : amount;
            healer.Heal(actualAmount);
            var result = new HealResult 
            { 
                HealerName = healer.Name, 
                Amount = actualAmount 
            };
            _logger.LogHeal(result);
            return result;
        }
    }
}