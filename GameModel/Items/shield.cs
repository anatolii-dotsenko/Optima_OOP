using GameModel.Abilities;

namespace GameModel.Items
{
    /// <summary>
    /// Defensive shield increasing armor.
    /// </summary>
    public class Shield : Item
    {
        public Shield()
            : base(name: "Knight Shield", armor: 4)
        {
        }
    }
}
