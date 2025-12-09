// constructs the application graph and registers dependencies.
using GameModel.Content.Characters;
using GameModel.Content.Items;
using GameModel.Core.Contracts;
using GameModel.Core.State;
using GameModel.Infrastructure.CLI;
using GameModel.Infrastructure.CLI.Commands;
using GameModel.Infrastructure.CLI.Rendering;
using GameModel.Infrastructure.CLI.Strategies;
using GameModel.Infrastructure.IO;
using GameModel.Infrastructure.Logging;
using GameModel.Infrastructure.Network;
using GameModel.Infrastructure.Persistence;
using GameModel.Systems.Combat;
using GameModel.Text;

namespace GameModel.Infrastructure.Setup
{
    public class GameBuilder
    {
        /// <summary>
        /// Builds the CLI application with all dependencies wired up.
        /// </summary>
        /// <param name="args">Command line arguments to determine initial mode.</param>
        /// <returns>The configured CLI Facade.</returns>
        public Cli Build(string[] args)
        {
            // --- 1. Infrastructure Layer ---
            // Renderer for the new CLI Facade
            var renderer = new ConsoleRenderer();
            var imageCache = new ImageCacheService(); // New Service
            IGameDataService apiService = new GenshinApiService(imageCache); // inject dependency
            // Displayer for legacy components (Logger, Persistence)
            // Ideally, everything would move to IRenderer, but we keep this for compatibility
            IDisplayer legacyDisplayer = new ConsoleDisplayer();

            var repository = new JsonFileRepository("savegame.json");

            // Logging Setup (Observer Pattern)
            var logger = new CompositeLogger();
            logger.Add(new ConsoleLogger(legacyDisplayer));

            // --- 2. Core & Systems Layer ---
            var worldContext = new WorldContext();

            // Combat System (Observer Pattern: No logger in constructor)
            ICombatSystem combatSystem = new CombatSystem();

            // Link Combat System events to Logger
            var combatObserver = new CombatEventObserver(logger, combatSystem);

            // Seed Data (Default World State)
            SeedWorldData(worldContext);

            // --- 3. Strategies Setup (Command Pattern) ---

            // Strategy A: RPG Mode
            // We can create a generic strategy or a specific RpgStrategy class
            var rpgStrategy = new RpgStrategy();
            RegisterRpgCommands(rpgStrategy, worldContext, combatSystem, apiService, repository, legacyDisplayer);

            // Strategy B: Text Editor Mode
            var docContext = new DocumentContext();
            var textFactory = new TextFactory();
            var textStrategy = new TextStrategy(); // Assumes TextStrategy class exists similar to RpgStrategy
            RegisterTextCommands(textStrategy, docContext, textFactory);

            // --- 4. CLI Facade Construction ---
            var cli = new Cli(renderer);

            // --- 5. Initial Strategy Selection ---
            if (args.Length > 0 && args[0] == "--text")
            {
                cli.UseStrategy(textStrategy);
            }
            else
            {
                // Default to RPG mode
                cli.UseStrategy(rpgStrategy);
            }

            return cli;
        }

        private void SeedWorldData(WorldContext context)
        {
            context.Characters.Add(new Warrior("Thorin"));
            context.Characters.Add(new Mage("Elira"));

            context.ItemPool.Add(new Sword());
            context.ItemPool.Add(new LightningWand());
            context.ItemPool.Add(new MagicAmulet());
            context.ItemPool.Add(new Shield());
        }

        private void RegisterRpgCommands(
            ICommandStrategy strategy,
            WorldContext worldContext,
            ICombatSystem combatSystem,
            IGameDataService apiService,
            JsonFileRepository repository,
            IDisplayer displayer)
        {
            // Gameplay Commands
            strategy.RegisterCommand(new HelpCommand(new CommandRegistry())); // Note: HelpCommand might need refactoring to accept Strategy info instead of Registry
            strategy.RegisterCommand(new CreateCommand(worldContext));
            strategy.RegisterCommand(new EquipCommand(worldContext));
            strategy.RegisterCommand(new LsCommand(worldContext));
            strategy.RegisterCommand(new ActCommand(combatSystem, worldContext));

            // Network Commands
            // We pass null for text-related dependencies as they aren't needed in RPG mode usually, 
            // or we create separate instances if NetCommand requires them.
            strategy.RegisterCommand(new NetCommand(apiService, worldContext, new TextFactory(), new DocumentContext()));

            // System Commands
            strategy.RegisterCommand(new SaveCommand(worldContext, repository, displayer));
            strategy.RegisterCommand(new LoadCommand(worldContext, repository, displayer));
        }

        private void RegisterTextCommands(
            ICommandStrategy strategy,
            DocumentContext docContext,
            TextFactory textFactory)
        {
            strategy.RegisterCommand(new AddTextCommand(docContext, textFactory));
            strategy.RegisterCommand(new PrintCommand(docContext));
        }
    }
}