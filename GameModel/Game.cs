using System;
using System.Linq;
using GameModel.Characters;
using GameModel.Items;
using GameModel.Combat;
using GameModel.Logging;
using GameModel.Text;

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
            // ---------------------------------------------------------
            // 1. Combat Simulation
            // ---------------------------------------------------------
            
            // Initialize infrastructure
            ICombatLogger logger = new ConsoleLogger();
            CombatSystem combat = new CombatSystem(logger);

            // Create characters
            Warrior thorin = new Warrior("Thorin");
            Mage elira = new Mage("Elira");

            // Equip items (Composition)
            thorin.EquipItem(new Sword());
            thorin.EquipItem(new Shield());
            elira.EquipItem(new MagicAmulet());

            // Display initial state
            Console.WriteLine("=== PRE-BATTLE STATS ===");
            PrintCharacterInfo(thorin);
            PrintCharacterInfo(elira);
            Console.WriteLine();

            // Execute Turn-Based Battle Logic
            Console.WriteLine("=== BATTLE STARTS ===");
            
            // Turn 1: Thorin attacks
            combat.Attack(thorin, elira);
            if (!CheckIfDead(thorin, elira))
            {
                // Turn 2: Elira responds with magic
                combat.UseAbility(elira, thorin, "Fireball");
                if (!CheckIfDead(thorin, elira))
                {
                    // Turn 3: Thorin uses special ability
                    combat.UseAbility(thorin, elira, "Power Strike");
                    if (!CheckIfDead(thorin, elira))
                    {
                        // Turn 4: Elira heals
                        combat.Heal(elira, 10);
                        if (!CheckIfDead(thorin, elira))
                        {
                            // Turn 5: Thorin attacks again
                            combat.Attack(thorin, elira);
                            if (!CheckIfDead(thorin, elira))
                            {
                                // Turn 6: Elira attacks
                                combat.Attack(elira, thorin);
                                CheckIfDead(thorin, elira);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("=== BATTLE ENDS ===");
            Console.WriteLine();

            // Display final state
            Console.WriteLine("=== POST-BATTLE STATS ===");
            PrintCharacterInfo(thorin);
            PrintCharacterInfo(elira);
            Console.WriteLine(new string('=', 50));

            // ---------------------------------------------------------
            // 2. Text Abstraction Demo (Composite & Builder Patterns)
            // ---------------------------------------------------------
            Console.WriteLine("\n=== TEXT ABSTRACTION DEMO ===");
            
            // Initialize the factory (Builder) with a Root node
            var factory = new TextFactory("Document Root");

            // Build structure: Add a top-level heading
            factory.AddHeading("Top Section");
            factory.AddParagraph("This is a line.");
            factory.AddParagraph("Another line.");

            // Create nested structure: Add a sub-heading (Rank 1)
            // The factory automatically sets this new heading as the current context
            factory.AddHeading("Inner Section", 1);
            factory.AddParagraph("Inner text.");

            // Navigate back up the tree to the parent context
            factory.Up();

            // Add content to the parent level again
            factory.AddParagraph("Back to top level.");

            // Render the entire Composite tree to string
            Console.WriteLine(factory.ToString());
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
            var stats = c.GetFinalStats();
            
            // Format equipment list
            string items = c.Equipment.Any() 
                ? string.Join(", ", c.Equipment.Select(i => i.Name)) 
                : "None";
            
            string status = c.Health > 0 ? "Alive" : "Dead";

            Console.WriteLine($"Name:   {c.Name} ({c.GetType().Name})");
            Console.WriteLine($"Status: {status}");
            Console.WriteLine($"HP:     {Math.Max(0, c.Health)}/{stats.TotalMaxHealth}");
            Console.WriteLine($"Atk:    {stats.TotalAttack}");
            Console.WriteLine($"Arm:    {stats.TotalArmor}");
            Console.WriteLine($"Items:  {items}");
            Console.WriteLine(new string('-', 20));
        }
    }
}