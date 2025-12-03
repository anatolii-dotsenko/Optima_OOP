using GameModel.Combat;
using GameModel.Logging;
using GameModel.Core.Contracts;
using GameModel.Commands;

namespace GameModel.Infrastructure.Setup
{
    /// <summary>
    /// Builds and wires the game dependencies.
    /// Replaces the ServiceLocator/Singleton pattern with explicit dependency injection.
    /// This is the Composition Root of the application.
    /// </summary>
    public class GameBuilder
    {
        /// <summary>
        /// Builds a fully configured GameState with all dependencies injected.
        /// </summary>
        public static GameState Build()
        {
            // Setup logging infrastructure
            var compositeLogger = new CompositeLogger();
            compositeLogger.AddCombatLogger(new ConsoleLogger());
            compositeLogger.AddGenericLogger(new ConsoleLoggerGeneric());

            // Create combat system with injected logger
            ICombatSystem combatSystem = new CombatSystem(compositeLogger);

            // Initialize GameState with dependencies
            var gameState = new GameState(combatSystem, compositeLogger);
            gameState.CommandRegistry.RegisterCommands();

            return gameState;
        }
    }
}
