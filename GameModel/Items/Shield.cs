using GameModel.Abilities;
using GameModel.Characters;

namespace GameModel.Items
{
    /// <summary>
    /// Defensive shield increasing armor.
    /// </summary>
    public class Shield : Item
    {
        public Shield()
            : base(name: "Knight Shield")
        {
            Modifiers.Add(new ArmorBonus(4));
        }
    }
}
