using System.Collections.Generic;
using GameModel.Core.Entities;

namespace GameModel.Core.State
{
    /// <summary>
    /// Holds the shared state for the RPG world (Characters and Item Pool).
    /// </summary>
    public class WorldContext
    {
        public List<Character> Characters { get; } = new();
        public List<Item> ItemPool { get; } = new();
    }
}