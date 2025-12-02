using System;
using System.Linq;
using GameModel.Characters;
using GameModel.Items;
using GameModel.Combat;
using GameModel.Logging;

namespace GameModel
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Initialize System
            ICombatLogger logger = new ConsoleLogger();
            CombatSystem combat = new CombatSystem(logger);

            // 2. Create Characters
            Warrior thorin = new Warrior("Thorin");
            Mage elira = new Mage("Elira");

            // 3. Equip Items
            thorin.EquipItem(new Sword());
            thorin.EquipItem(new Shield());
            elira.EquipItem(new MagicAmulet());

            // 4. Pre-Battle Stats
            Console.WriteLine("=== PRE-BATTLE STATS ===");
            PrintCharacterInfo(thorin);
            PrintCharacterInfo(elira);
            Console.WriteLine();

            // 5. Battle Simulation
            Console.WriteLine("=== BATTLE STARTS ===");

            // Action 1: Thorin attacks
            combat.Attack(thorin, elira);
            if (CheckIfDead(thorin, elira)) goto PostBattle;

            // Action 2: Elira uses Fireball
            combat.UseAbility(elira, thorin, "Fireball");
            if (CheckIfDead(thorin, elira)) goto PostBattle;

            // Action 3: Thorin uses Power Strike
            combat.UseAbility(thorin, elira, "Power Strike");
            if (CheckIfDead(thorin, elira)) goto PostBattle;

            // Action 4: Elira heals
            combat.Heal(elira, 10);
            if (CheckIfDead(thorin, elira)) goto PostBattle;

            // Action 5: Thorin attacks again
            combat.Attack(thorin, elira);
            if (CheckIfDead(thorin, elira)) goto PostBattle;

            // Action 6: Elira attacks
            combat.Attack(elira, thorin);
            if (CheckIfDead(thorin, elira)) goto PostBattle;

            PostBattle:
            Console.WriteLine("=== BATTLE ENDS ===");
            Console.WriteLine();

            // 6. Post-Battle Stats
            Console.WriteLine("=== POST-BATTLE STATS ===");
            PrintCharacterInfo(thorin);
            PrintCharacterInfo(elira);
        }

        // Helper method to check health and print defeat message
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