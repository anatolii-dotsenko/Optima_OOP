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
        public ICombatLogger CombatLogger { get; }
        public ILogger Logger { get; }
        public CommandRegistry CommandRegistry { get; }
        public List<Character> Characters { get; } = new();

        private GameState()
        {
            // Create composite logger for flexibility
            var compositeLogger = new CompositeLogger();
            
            // Add combat logger
            var combatLogger = new ConsoleLogger();
            compositeLogger.AddCombatLogger(combatLogger);
            CombatLogger = compositeLogger;
            
            // Add generic logger
            compositeLogger.AddGenericLogger(new ConsoleLoggerGeneric());

            Logger = compositeLogger;
            CombatSystem = new CombatSystem(compositeLogger);
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
