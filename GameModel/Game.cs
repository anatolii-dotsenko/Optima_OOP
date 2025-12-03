// using System;
// using System.Linq;
// using GameModel.Characters;
// using GameModel.Items;
// using GameModel.Combat;
// using GameModel.Logging;
// using GameModel.Text;
// using GameModel.Abilities;
// using GameModel.Commands;

// namespace GameModel
// {
//     /// <summary>
//     /// The entry point of the application.
//     /// Orchestrates the combat simulation via BattleManager and demonstrates the text generation system.
//     /// </summary>
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             // Initialize game state singleton
//             var gameState = GameState.Instance;
//             gameState.CommandRegistry.RegisterCommands();

//             // Log system startup
//             gameState.Logger.LogInfo("Game initialized.");

//             // Create characters
//             Warrior thorin = new Warrior("Thorin");
//             Mage elira = new Mage("Elira", new Fireball());

//             // Equip items
//             thorin.EquipItem(new Sword());
//             thorin.EquipItem(new Shield());
//             elira.EquipItem(new MagicAmulet(new Fireball()));

//             gameState.AddCharacter(thorin);
//             gameState.AddCharacter(elira);

//             // Display initial state
//             Console.WriteLine("=== PRE-BATTLE STATS ===");
//             PrintCharacterInfo(thorin);
//             PrintCharacterInfo(elira);
//             Console.WriteLine();

//             // Initialize and run battle using BattleManager (TS compliance: autonomous coordination)
//             var battleManager = new BattleManager(gameState.CombatSystem, gameState.CombatLogger);
//             battleManager.AddParticipant(thorin);
//             battleManager.AddParticipant(elira);
//             battleManager.Start();

//             // Display final state
//             Console.WriteLine("=== POST-BATTLE STATS ===");
//             PrintCharacterInfo(thorin);
//             PrintCharacterInfo(elira);
//             Console.WriteLine(new string('=', 50));

//             // Text abstraction demo
//             DemoTextFactory();
//         }

//         /// <summary>
//         /// Prints formatted statistics for a specific character to the console.
//         /// Calculates final stats including item bonuses before display.
//         /// </summary>
//         /// <param name="c">The character whose info should be displayed.</param>
//         static void PrintCharacterInfo(Character c)
//         {
//             // Fetch calculated stats (Base + Equipment)
//             var (atk, arm, maxHp) = c.GetFinalStats();
            
//             // Format equipment list
//             string items = c.Equipment.Any() 
//                 ? string.Join(", ", c.Equipment.Select(i => i.Name)) 
//                 : "None";
            
//             string status = c.Health > 0 ? "Alive" : "Dead";

//             Console.WriteLine($"Name:   {c.Name} ({c.GetType().Name})");
//             Console.WriteLine($"Status: {status}");
//             Console.WriteLine($"HP:     {System.Math.Max(0, c.Health)}/{maxHp}");
//             Console.WriteLine($"Atk:    {atk}");
//             Console.WriteLine($"Arm:    {arm}");
//             Console.WriteLine($"Items:  {items}");
//             Console.WriteLine(new string('-', 20));
//         }

//         static void DemoTextFactory()
//         {
//             Console.WriteLine("\n=== TEXT ABSTRACTION DEMO ===");
//             var factory = new TextFactory("Document Root");
//             factory.AddHeading("Top Section");
//             factory.AddParagraph("This is a line.");
//             factory.AddParagraph("Another line.");
//             factory.AddHeading("Inner Section", 1);
//             factory.AddParagraph("Inner text.");
//             factory.Up();
//             factory.AddParagraph("Back to top level.");
//             Console.WriteLine(factory.ToString());
//         }
//     }
// }