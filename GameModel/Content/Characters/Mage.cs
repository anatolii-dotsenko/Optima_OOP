// represents a character preset with high magic stats and low armor
using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Content.Characters
{
    public class Mage : Character
    {
        public Mage(string name) : base(name)
        {
            SetBaseStat(StatType.MaxHealth, 90);
            SetBaseStat(StatType.Health, 90);
            SetBaseStat(StatType.Attack, 5);
            SetBaseStat(StatType.Armor, 2);
        }
    }
}