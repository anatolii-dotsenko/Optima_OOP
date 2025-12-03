using GameModel.Abilities;
using GameModel.Characters;

namespace GameModel.Items
{
    /// <summary>
    /// Defensive shield increasing armor.
    /// Dual interface: explicit property (TS) + modifier (extensibility).
    /// </summary>
    public class Shield : Item
    {
        public override int ArmorBonus => 4;

        public Shield()
            : base(name: "Knight Shield")
        {
            Modifiers.Add(new ArmorBonus(4));
        }
    }
}
