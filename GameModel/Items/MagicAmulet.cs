using GameModel.Characters;
using GameModel.Abilities;

namespace GameModel.Items
{
    /// <summary>
    /// Magical amulet that grants both stat bonuses and an ability.
    /// Implements IAbilityProvider to separate ability-granting concern (ISP).
    /// Exposes HealthBonus as explicit property (TS compliance).
    /// </summary>
    public class MagicAmulet : Item, IAbilityProvider
    {
        private readonly Ability _ability;

        public override int HealthBonus => 20;

        // Dependency-injected ability; caller decides which concrete Ability to provide (DIP).
        public MagicAmulet(Ability ability)
            : base(name: "Amulet of Flames")
        {
            _ability = ability;
            Modifiers.Add(new HealthBonus(20));
        }

        public Ability GetAbility()
        {
            return _ability;
        }
    }
}
