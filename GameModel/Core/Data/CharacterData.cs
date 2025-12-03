using System.Collections.Generic;
using GameModel.Core.ValueObjects;

namespace GameModel.Core.Data
{
    public class CharacterData
    {
        public string Name { get; set; } = string.Empty;
        public string ClassType { get; set; } = string.Empty; // e.g., "Warrior", "Mage"
        public int CurrentHealth { get; set; }
        public Dictionary<StatType, int> BaseStats { get; set; } = new();
        public List<string> InventoryItems { get; set; } = new();
    }
}