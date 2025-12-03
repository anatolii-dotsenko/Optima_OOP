using GameModel.Abilities;
using GameModel.Characters;

namespace GameModel.Items
{
    /// <summary>
    /// Basic weapon increasing attack power.
    /// Dual interface: explicit property (TS) + modifier (extensibility).
    /// </summary>
    public class Sword : Item
    {
        public override int AttackBonus => 12;

        public Sword()
            : base(name: "Iron Sword")
        {
            Modifiers.Add(new AttackBonus(12));
        }
    }
}
