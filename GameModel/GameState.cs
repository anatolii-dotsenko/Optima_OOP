using System.Collections.Generic;
using GameModel.Characters;
using GameModel.Combat;
using GameModel.Commands;
using GameModel.Logging;

namespace GameModel
{
    /// <summary>
    /// Singleton holding the current game state.
    /// Provides access to characters, combat system, and command registry.
    /// </summary>
    public class GameState
    {
        private static GameState _instance;
        public static GameState Instance => _instance ??= new GameState();

        public CombatSystem CombatSystem { get; }
        public CommandRegistry CommandRegistry { get; }
        public List<Character> Characters { get; } = new();

        private GameState()
        {
            ICombatLogger logger = new ConsoleLogger();
            CombatSystem = new CombatSystem(logger);
            CommandRegistry = new CommandRegistry();
        }

        /// <summary>
        /// Retrieve a character by name.
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
