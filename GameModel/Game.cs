using System;
using GameModel.Characters;
using GameModel.Items;
using GameModel.Combat;
using GameModel.Text;
using GameModel.Abilities;
using GameModel.Presentation;
using GameModel.Infrastructure.Setup;

namespace GameModel
{
    /// <summary>
    /// The entry point of the application.
    /// Orchestrates the combat simulation via BattleManager and demonstrates the text generation system.
    /// Uses GameBuilder for dependency injection (DIP compliance).
    /// Uses ConsolePresenter for output rendering (SRP compliance).
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Build game with all dependencies injected (DIP)
            var gameState = GameBuilder.Build();

            // Log system startup
            gameState.Logger.LogInfo("Game initialized.");

            // Create characters
            Warrior thorin = new Warrior("Thorin");
            Mage elira = new Mage("Elira", new Fireball());

            // Equip items
            thorin.EquipItem(new Sword());
            thorin.EquipItem(new Shield());
            elira.EquipItem(new MagicAmulet(new Fireball()));

            gameState.AddCharacter(thorin);
            gameState.AddCharacter(elira);

            // Display initial state using dedicated presenter (SRP)
            ConsolePresenter.DisplayPreBattleStats(thorin, elira);

            // Initialize and run battle using BattleManager with injected ICombatSystem (DIP)
            var battleManager = new BattleManager(gameState.CombatSystem, gameState.CombatLogger);
            battleManager.AddParticipant(thorin);
            battleManager.AddParticipant(elira);
            battleManager.Start();

            // Display final state using dedicated presenter (SRP)
            ConsolePresenter.DisplayPostBattleStats(thorin, elira);

            // Text abstraction demo
            DemoTextFactory();
        }

        static void DemoTextFactory()
        {
            Console.WriteLine("\n=== TEXT ABSTRACTION DEMO ===");
            var factory = new TextFactory("Document Root");
            factory.AddHeading("Top Section");
            factory.AddParagraph("This is a line.");
            factory.AddParagraph("Another line.");
            factory.AddHeading("Inner Section", 1);
            factory.AddParagraph("Inner text.");
            factory.Up();
            factory.AddParagraph("Back to top level.");
            Console.WriteLine(factory.ToString());
        }
    }
}