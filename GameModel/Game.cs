using System;
using System.Linq;
using GameModel.Characters;
using GameModel.Items;
using GameModel.Combat;
using GameModel.Logging;
using GameModel.Text;
using GameModel.Abilities;
using GameModel.Commands;

namespace GameModel
{
    /// <summary>
    /// The entry point of the application.
    /// Orchestrates the combat simulation and demonstrates the text generation system.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main execution method.
        /// </summary>
        /// <param name="args">Command line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Initialize game state singleton
            var gameState = GameState.Instance;
            gameState.CommandRegistry.RegisterCommands();

            // Create characters
            Warrior thorin = new Warrior("Thorin");
            // Inject the starting ability via constructor (DIP)
            Mage elira = new Mage("Elira", new Fireball());

            thorin.EquipItem(new Sword());
            thorin.EquipItem(new Shield());
            elira.EquipItem(new MagicAmulet(new Fireball()));

            gameState.AddCharacter(thorin);
            gameState.AddCharacter(elira);

            // Display initial state
            Console.WriteLine("=== PRE-BATTLE STATS ===");
            PrintCharacterInfo(thorin);
            PrintCharacterInfo(elira);
            Console.WriteLine();

            // Execute turn-based battle via hardcoded sequence (can be replaced with interactive loop)
            Console.WriteLine("=== BATTLE STARTS ===");
            ExecuteBattle(gameState, thorin, elira);

            Console.WriteLine("=== BATTLE ENDS ===\n");

            // Display final state
            Console.WriteLine("=== POST-BATTLE STATS ===");
            PrintCharacterInfo(thorin);
            PrintCharacterInfo(elira);
            Console.WriteLine(new string('=', 50));

            // Text abstraction demo
            DemoTextFactory();
        }

        static void ExecuteBattle(GameState gameState, Character thorin, Character elira)
        {
            // Sequence of commands (can be replaced with interactive CLI loop)
            ExecuteCommand(gameState, "attack Thorin Elira");
            if (CheckIfDead(thorin, elira)) return;

            ExecuteCommand(gameState, "ability Elira Thorin Fireball");
            if (CheckIfDead(thorin, elira)) return;

            ExecuteCommand(gameState, "ability Thorin Elira Power Strike");
            if (CheckIfDead(thorin, elira)) return;

            ExecuteCommand(gameState, "heal Elira 10");
            if (CheckIfDead(thorin, elira)) return;

            ExecuteCommand(gameState, "attack Thorin Elira");
            if (CheckIfDead(thorin, elira)) return;

            ExecuteCommand(gameState, "attack Elira Thorin");
            CheckIfDead(thorin, elira);
        }

        static void ExecuteCommand(GameState gameState, string input)
        {
            var parts = input.Split(' ');
            var commandName = parts[0];
            var commandArgs = parts.Skip(1).ToArray();

            if (gameState.CommandRegistry.TryGetCommand(commandName, out var command))
            {
                command.Execute(commandArgs);
            }
            else
            {
                Console.WriteLine($"Unknown command: {commandName}");
            }
        }

        /// <summary>
        /// Checks if either character has reached 0 health.
        /// Logs a defeat message if a character has died.
        /// </summary>
        /// <param name="c1">The first character to check.</param>
        /// <param name="c2">The second character to check.</param>
        /// <returns>True if any character is dead; otherwise, false.</returns>
        static bool CheckIfDead(Character c1, Character c2)
        {
            if (c1.Health <= 0)
            {
                Console.WriteLine($"\n*** {c1.Name} has been defeated! ***");
                return true;
            }
            if (c2.Health <= 0)
            {
                Console.WriteLine($"\n*** {c2.Name} has been defeated! ***");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Prints formatted statistics for a specific character to the console.
        /// Calculates final stats including item bonuses before display.
        /// </summary>
        /// <param name="c">The character whose info should be displayed.</param>
        static void PrintCharacterInfo(Character c)
        {
            // Fetch calculated stats (Base + Equipment)
            var (atk, arm, maxHp) = c.GetFinalStats();
            
            // Format equipment list
            string items = c.Equipment.Any() 
                ? string.Join(", ", c.Equipment.Select(i => i.Name)) 
                : "None";
            
            string status = c.Health > 0 ? "Alive" : "Dead";

            Console.WriteLine($"Name:   {c.Name} ({c.GetType().Name})");
            Console.WriteLine($"Status: {status}");
            Console.WriteLine($"HP:     {System.Math.Max(0, c.Health)}/{maxHp}");
            Console.WriteLine($"Atk:    {atk}");
            Console.WriteLine($"Arm:    {arm}");
            Console.WriteLine($"Items:  {items}");
            Console.WriteLine(new string('-', 20));
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