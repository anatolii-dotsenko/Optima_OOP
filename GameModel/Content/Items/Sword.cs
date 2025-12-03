using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Content.Items
{
    public class Sword : Item
    {
        public Sword() : base("Iron Sword")
        {
            AddModifier(StatType.Attack, 12);
        }
    }
}