using GameModel.Abilities;
using GameModel.Characters;

namespace GameModel.Items
{
    /// <summary>
    /// Basic weapon increasing attack power.
    /// </summary>
    public class Sword : Item
    {
        public Sword()
            : base(name: "Iron Sword")
        {
            Modifiers.Add(new AttackBonus(12));
        }
    }
}
