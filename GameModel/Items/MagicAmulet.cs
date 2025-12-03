using GameModel.Characters;
using GameModel.Abilities;

namespace GameModel.Items
{
    /// <summary>
    /// Grants additional max HP and a bonus ability.
    /// </summary>
    public class MagicAmulet : Item
    {
        // Dependency-injected ability; caller decides which concrete Ability to provide.
        public MagicAmulet(Ability ability)
            : base(name: "Amulet of Flames")
        {
            Modifiers.Add(new HealthBonus(20));
            GrantedAbility = ability;
        }
    }
}
