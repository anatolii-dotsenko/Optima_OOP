using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Content.Items
{
    public class Shield : Item
    {
        public Shield() : base("Wooden Shield")
        {
            AddModifier(StatType.Armor, 5);
        }
    }
}