using System;
using System.Linq;
using GameModel.Characters;
using GameModel.Items;
using GameModel.Combat;
using GameModel.Logging;
using GameModel.Text; // Додано namespace

namespace GameModel
{
    class Program
    {
        static void Main(string[] args)
        {
            // ---------------------------------------------------------
            // 1. Існуюча симуляція бою (залишено без змін)
            // ---------------------------------------------------------
            ICombatLogger logger = new ConsoleLogger();
            CombatSystem combat = new CombatSystem(logger);

            Warrior thorin = new Warrior("Thorin");
            Mage elira = new Mage("Elira");

            thorin.EquipItem(new Sword());
            thorin.EquipItem(new Shield());
            elira.EquipItem(new MagicAmulet());

            Console.WriteLine("=== PRE-BATTLE STATS ===");
            PrintCharacterInfo(thorin);
            PrintCharacterInfo(elira);
            Console.WriteLine();

            Console.WriteLine("=== BATTLE STARTS ===");
            
            combat.Attack(thorin, elira);
            if (!CheckIfDead(thorin, elira))
            {
                combat.UseAbility(elira, thorin, "Fireball");
                if (!CheckIfDead(thorin, elira))
                {
                    combat.UseAbility(thorin, elira, "Power Strike");
                    if (!CheckIfDead(thorin, elira))
                    {
                        combat.Heal(elira, 10);
                        if (!CheckIfDead(thorin, elira))
                        {
                            combat.Attack(thorin, elira);
                            if (!CheckIfDead(thorin, elira))
                            {
                                combat.Attack(elira, thorin);
                                CheckIfDead(thorin, elira);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("=== BATTLE ENDS ===");
            Console.WriteLine();

            Console.WriteLine("=== POST-BATTLE STATS ===");
            PrintCharacterInfo(thorin);
            PrintCharacterInfo(elira);
            Console.WriteLine(new string('=', 50));

            // ---------------------------------------------------------
            // 2. Новий приклад використання TextFactory (Example Usage)
            // ---------------------------------------------------------
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

        static void PrintCharacterInfo(Character c)
        {
            var stats = c.GetFinalStats();
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