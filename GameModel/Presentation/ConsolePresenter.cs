using System;
using System.Linq;
using GameModel.Characters;

namespace GameModel.Presentation
{
    /// <summary>
    /// Responsible for rendering character information to the console.
    /// Separates presentation logic from game orchestration (SRP).
    /// </summary>
    public class ConsolePresenter
    {
        /// <summary>
        /// Prints formatted statistics for a specific character to the console.
        /// </summary>
        public static void PrintCharacterInfo(Character c)
        {
            var (atk, arm, maxHp) = c.GetFinalStats();
            
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

        /// <summary>
        /// Displays the pre-battle character stats.
        /// </summary>
        public static void DisplayPreBattleStats(Character c1, Character c2)
        {
            Console.WriteLine("=== PRE-BATTLE STATS ===");
            PrintCharacterInfo(c1);
            PrintCharacterInfo(c2);
            Console.WriteLine();
        }

        /// <summary>
        /// Displays the post-battle character stats.
        /// </summary>
        public static void DisplayPostBattleStats(Character c1, Character c2)
        {
            Console.WriteLine("=== POST-BATTLE STATS ===");
            PrintCharacterInfo(c1);
            PrintCharacterInfo(c2);
            Console.WriteLine(new string('=', 50));
        }
    }
}
