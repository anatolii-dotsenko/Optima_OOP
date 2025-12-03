using GameModel.Content.Characters;
using GameModel.Content.Items;
using GameModel.Core.Contracts;
using GameModel.Core.Entities;
using GameModel.Infrastructure.CLI;
using GameModel.Infrastructure.CLI.Commands;
using GameModel.Infrastructure.Logging;
using GameModel.Systems.Combat;

namespace GameModel.Infrastructure.Setup
{
    /// <summary>
    /// Constructs the game graph, wiring dependencies.
    /// Replaces the monolithic 'Game' class and Singleton GameState.
    /// </summary>
    public class GameBuilder
    {
        public (BattleManager, CommandRegistry, List<ICombatEntity>) Build()
        {
            // 1. Setup Logging
            var logger = new CompositeLogger();
            logger.Add(new ConsoleLogger());

            // 2. Setup Systems
            ICombatSystem combatSystem = new CombatSystem(logger);

            // 3. Setup Entities
            var warrior = new Warrior("Thorin");
            warrior.EquipItem(new Sword());
            warrior.EquipItem(new Shield());

            var mage = new Mage("Elira");
            mage.EquipItem(new MagicAmulet());

            var characters = new List<ICombatEntity> { warrior, mage };
            
            // Helper for commands to find characters
            Func<string, ICombatEntity?> lookup = name => 
                characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            // 4. Setup Manager
            var battleManager = new BattleManager(combatSystem, logger);
            foreach (var c in characters) battleManager.AddParticipant(c);

            // 5. Setup CLI
            var registry = new CommandRegistry();
            registry.Register(new HelpCommand(registry));
            registry.Register(new AttackCommand(combatSystem, lookup));
            // Add HealCommand, etc.

            return (battleManager, registry, characters);
        }
    }
}