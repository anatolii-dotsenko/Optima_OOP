using GameModel.Characters;
using GameModel.Abilities;

namespace GameModel.Items
{
    /// <summary>
    /// Grants additional max HP and a bonus ability.
    /// </summary>
    public class MagicAmulet : Item
    {
        public MagicAmulet()
            : base(name: "Amulet of Flames")
        {
            Modifiers.Add(new HealthBonus(20));
            GrantedAbility = new Fireball();
        }
    }
}
