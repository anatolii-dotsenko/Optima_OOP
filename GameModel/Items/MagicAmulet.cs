using GameModel.Abilities;

namespace GameModel.Items
{
    /// <summary>
    /// Grants additional max HP and a bonus ability.
    /// </summary>
    public class MagicAmulet : Item
    {
        public MagicAmulet()
            : base(name: "Amulet of Flames", health: 20, ability: new Fireball())
        {
        }
    }
}
