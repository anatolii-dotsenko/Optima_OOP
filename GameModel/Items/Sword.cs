using GameModel.Abilities;
using GameModel.Characters;

namespace GameModel.Items
{
    /// <summary>
    /// Basic weapon increasing attack power.
    /// Exposes AttackBonus as explicit property (TS compliance).
    /// Uses modifier list for extensibility (OCP compliance).
    /// </summary>
    public class Sword : Item
    {
        // TS-compliant explicit property
        public override int AttackBonus => 12;

        public Sword()
            : base(name: "Iron Sword")
        {
            // Also add to modifiers for consistency
            Modifiers.Add(new AttackBonus(12));
        }
    }
}
