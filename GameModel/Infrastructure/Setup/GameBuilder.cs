using System.Collections.Generic;
using GameModel.Content.Characters;
using GameModel.Content.Items;
using GameModel.Core.Contracts;
using GameModel.Core.State;
using GameModel.Infrastructure.CLI;
using GameModel.Infrastructure.CLI.Commands;
using GameModel.Infrastructure.Logging;
using GameModel.Systems.Combat;
using GameModel.Text;

namespace GameModel.Infrastructure.Setup
{
    public class GameBuilder
    {
        /// <summary>
        /// Builds the game environment and returns the necessary contexts and registry.
        /// </summary>
        public (CommandRegistry, WorldContext, DocumentContext) Build()
        {
            // 1. Shared Infrastructure
            var registry = new CommandRegistry();
            var logger = new CompositeLogger();
            logger.Add(new ConsoleLogger());
            
            // 2. RPG System & State
            var worldContext = new WorldContext();
            ICombatSystem combatSystem = new CombatSystem(logger);
            
            // Seed Data
            var warrior = new Warrior("Thorin");
            worldContext.Characters.Add(warrior);
            worldContext.Characters.Add(new Mage("Elira"));
            worldContext.ItemPool.Add(new Sword());
            
            // Register RPG Commands
            registry.Register(new HelpCommand(registry));
            registry.Register(new CreateCommand(worldContext));
            registry.Register(new EquipCommand(worldContext));
            registry.Register(new LsCommand(worldContext));
            registry.Register(new ActCommand(combatSystem, worldContext));

            // 3. Text System & State
            var docContext = new DocumentContext();
            var textFactory = new TextFactory();

            // Register Text Commands
            registry.Register(new AddTextCommand(docContext, textFactory));
            registry.Register(new PrintCommand(docContext));
            registry.Register(new PwdCommand(docContext));
            registry.Register(new ChangeDirCommand(docContext));
            // Add other commands (Up, Rm) similarly...

            return (registry, worldContext, docContext);
        }
    }
}