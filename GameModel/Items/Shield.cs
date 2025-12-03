using GameModel.Abilities;
using GameModel.Characters;

namespace GameModel.Items
{
    /// <summary>
    /// Defensive shield increasing armor.
    /// Exposes ArmorBonus as explicit property (TS compliance).
    /// Uses modifier list for extensibility (OCP compliance).
    /// </summary>
    public class Shield : Item
    {
        // TS-compliant explicit property
        public override int ArmorBonus => 4;

        public Shield()
            : base(name: "Knight Shield")
        {
            // Also add to modifiers for consistency
            Modifiers.Add(new ArmorBonus(4));
        }
    }
}
