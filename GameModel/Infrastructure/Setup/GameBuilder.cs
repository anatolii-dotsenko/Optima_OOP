using System.Collections.Generic;
using GameModel.Content.Characters;
using GameModel.Content.Items;
using GameModel.Core.Contracts;
using GameModel.Core.State;
using GameModel.Infrastructure.CLI;
using GameModel.Infrastructure.CLI.Commands;
using GameModel.Infrastructure.Logging;
using GameModel.Infrastructure.IO;
using GameModel.Infrastructure.Persistence;
using GameModel.Systems.Combat;
using GameModel.Text;
using GameModel.Infrastructure.Network; // New namespace

namespace GameModel.Infrastructure.Setup
{
    public class GameBuilder
    {
        // Now returns the Engine directly, fully constructed with dependencies
        public CliEngine BuildEngine(string[] args)
        {
            // --- 1. Infrastructure Layer ---
            IGameDataService apiService = new GenshinApiService();
            IDisplayer displayer = new ConsoleDisplayer();
            var repository = new JsonFileRepository("savegame.json");

            var logger = new CompositeLogger();
            logger.Add(new ConsoleLogger(displayer)); // Inject displayer
            
            // --- 2. Core & Systems Layer ---
            var worldContext = new WorldContext();
            ICombatSystem combatSystem = new CombatSystem(); // No logger in constructor now
            // Connect Observer
            var combatObserver = new CombatEventObserver(logger, combatSystem);
            
            // Seed Data (Default State)
            worldContext.Characters.Add(new Warrior("Thorin"));
            worldContext.Characters.Add(new Mage("Elira"));
            
            worldContext.ItemPool.Add(new Sword());
            worldContext.ItemPool.Add(new LightningWand());
            worldContext.ItemPool.Add(new MagicAmulet());
            worldContext.ItemPool.Add(new Shield());

            // --- 3. Commands Setup ---
            var charRegistry = new CommandRegistry();
            charRegistry.Register(new HelpCommand(charRegistry));
            charRegistry.Register(new CreateCommand(worldContext));
            charRegistry.Register(new EquipCommand(worldContext));
            charRegistry.Register(new LsCommand(worldContext));
            charRegistry.Register(new ActCommand(combatSystem, worldContext));

            // Text Mode Setup
            var docContext = new DocumentContext();
            var textFactory = new TextFactory();

            charRegistry.Register(new NetCommand(apiService, worldContext, textFactory, docContext));

            // NEW: System Commands
            charRegistry.Register(new SaveCommand(worldContext, repository, displayer));
            charRegistry.Register(new LoadCommand(worldContext, repository, displayer));


            var textRegistry = new CommandRegistry();
            textRegistry.Register(new HelpCommand(textRegistry));
            textRegistry.Register(new AddTextCommand(docContext, textFactory));
            textRegistry.Register(new PrintCommand(docContext));
            textRegistry.Register(new PwdCommand(docContext));
            textRegistry.Register(new ChangeDirCommand(docContext));
            textRegistry.Register(new UpCommand(docContext));
            textRegistry.Register(new RmCommand(docContext));

            // --- 4. Session Selection ---
            ICliSession session;

            if (args.Length > 0 && args[0] == "--text")
            {
                session = new TextSession(textRegistry);
            }
            else if (args.Length > 0 && args[0] == "--chars")
            {
                session = new CharacterSession(charRegistry);
            }
            else
            {
                displayer.WriteLine("Select Mode: 1. Text, 2. Characters");
                var choice = displayer.ReadLine();
                if (choice == "1") session = new TextSession(textRegistry);
                else session = new CharacterSession(charRegistry);
            }

            // --- 5. Return configured engine ---
            return new CliEngine(session, displayer);
        }
    }
}