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
        public (CommandRegistry textReg, CommandRegistry charReg, WorldContext, DocumentContext) Build()
        {
            // --- Shared Infrastructure ---
            var logger = new CompositeLogger();
            logger.Add(new ConsoleLogger());
            
            // --- 1. RPG Setup ---
            var worldContext = new WorldContext();
            ICombatSystem combatSystem = new CombatSystem(logger);
            
            // Seed Data
            worldContext.Characters.Add(new Warrior("Thorin"));
            worldContext.Characters.Add(new Mage("Elira"));
            worldContext.ItemPool.Add(new Sword());

            // RPG Registry
            var charRegistry = new CommandRegistry();
            charRegistry.Register(new HelpCommand(charRegistry));
            charRegistry.Register(new CreateCommand(worldContext));
            charRegistry.Register(new EquipCommand(worldContext)); // "add" for chars
            charRegistry.Register(new LsCommand(worldContext));
            charRegistry.Register(new ActCommand(combatSystem, worldContext));

            // --- 2. Text Setup ---
            var docContext = new DocumentContext();
            var textFactory = new TextFactory();

            // Text Registry
            var textRegistry = new CommandRegistry();
            textRegistry.Register(new HelpCommand(textRegistry));
            textRegistry.Register(new AddTextCommand(docContext, textFactory));
            textRegistry.Register(new PrintCommand(docContext));
            textRegistry.Register(new PwdCommand(docContext));
            textRegistry.Register(new ChangeDirCommand(docContext));
            textRegistry.Register(new UpCommand(docContext)); // New
            textRegistry.Register(new RmCommand(docContext)); // New

            return (textRegistry, charRegistry, worldContext, docContext);
        }
    }
}