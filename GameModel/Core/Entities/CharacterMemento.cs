using System.Collections.Generic;
using GameModel.Core.Contracts;
using GameModel.Core.ValueObjects;

namespace GameModel.Core.Entities
{
    /// <summary>
    /// Pattern: Memento (Concrete Memento).
    /// Stores the internal state of the Character immutable.
    /// </summary>
    public class CharacterMemento : IMemento
    {
        public string Name { get; }
        public string ClassType { get; }
        public int CurrentHealth { get; }
        public Dictionary<StatType, int> BaseStats { get; }
        public List<string> InventoryItems { get; }

        public CharacterMemento(string name, string classType, int hp, Dictionary<StatType, int> stats, List<string> items)
        {
            Name = name;
            ClassType = classType;
            CurrentHealth = hp;
            BaseStats = new Dictionary<StatType, int>(stats); // Deep copy
            InventoryItems = new List<string>(items);
        }
    }
}