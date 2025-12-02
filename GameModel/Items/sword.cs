using GameModel.Abilities;

namespace GameModel.Items
{
    /// <summary>
    /// Basic weapon increasing attack power.
    /// </summary>
    public class Sword : Item
    {
        public Sword()
            : base(name: "Iron Sword", attack: 4)
        {
        }
    }
}
