using GameModel.Characters;
using GameModel.Abilities;

namespace GameModel.Items
{
    /// <summary>
    /// Magical amulet that grants both stat bonuses and an ability.
    /// Exposes HealthBonus as explicit property (TS compliance).
    /// Implements IAbilityProvider to segregate ability-granting (ISP compliance).
    /// Accepts dependency-injected ability (DIP compliance).
    /// </summary>
    public class MagicAmulet : Item, IAbilityProvider
    {
        private readonly Ability _ability;

        // TS-compliant explicit property
        public override int HealthBonus => 20;

        /// <summary>
        /// Dependency-injected ability; caller decides which concrete Ability to provide.
        /// This satisfies the Dependency Inversion Principle while remaining flexible.
        /// </summary>
        public MagicAmulet(Ability ability)
            : base(name: "Amulet of Flames")
        {
            _ability = ability;
            // Also add to modifiers for consistency
            Modifiers.Add(new HealthBonus(20));
        }

        public Ability GetAbility()
        {
            return _ability;
        }
    }
}
