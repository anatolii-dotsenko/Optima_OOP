using System.Collections.Generic;
using GameModel.Characters;
using GameModel.Combat;
using GameModel.Commands;
using GameModel.Logging;
using GameModel.Core.Contracts;

namespace GameModel
{
    /// <summary>
    /// Holds the current game state and provides access to game systems.
    /// Now accepts injected dependencies (DIP) instead of creating them internally.
    /// </summary>
    public class GameState
    {
        private static GameState _instance;
        public static GameState Instance => _instance ??= Infrastructure.Setup.GameBuilder.Build();

        public ICombatSystem CombatSystem { get; }
        public ICombatLogger CombatLogger { get; }
        public ILogger Logger { get; }
        public CommandRegistry CommandRegistry { get; }
        public List<Character> Characters { get; } = new();

        /// <summary>
        /// Constructor that accepts all dependencies (DIP compliance).
        /// </summary>
        public GameState(ICombatSystem combatSystem, CompositeLogger compositeLogger)
        {
            CombatSystem = combatSystem;
            CombatLogger = compositeLogger;
            Logger = compositeLogger;
            CommandRegistry = new CommandRegistry();
        }

        /// <summary>
        /// Retrieve a character by name (case-insensitive).
        /// </summary>
        public Character GetCharacter(string name)
        {
            return Characters.Find(c => c.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Add a character to the game state.
        /// </summary>
        public void AddCharacter(Character character)
        {
            Characters.Add(character);
        }
    }
}
