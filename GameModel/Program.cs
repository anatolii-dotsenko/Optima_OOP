// using GameModel.Characters;
// using GameModel.Items;
// using GameModel.Logging;
// using GameModel.Combat;

// class Program
// {
//     static void Main()
//     {
//         ICombatLogger logger = new ConsoleLogger();
//         CombatSystem combat = new CombatSystem(logger);

//         // Create characters
//         var warrior = new Warrior("Thorin");
//         var mage = new Mage("Elira");

//         // Equip items
//         warrior.EquipItem(new Sword());
//         warrior.EquipItem(new Shield());

//         mage.EquipItem(new MagicAmulet());

//         // Show start
//         logger.Write("=== BATTLE STARTS ===");

//         // Round 1
//         combat.Attack(warrior, mage);
//         combat.UseAbility(mage, warrior, "Fireball");

//         // Round 2
//         combat.UseAbility(warrior, mage, "Power Strike");
//         combat.Heal(mage, 10);

//         // Round 3
//         combat.Attack(warrior, mage);
//         combat.Attack(mage, warrior);

//         logger.Write("=== BATTLE ENDS ===");
//     }
// }


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
            ICombatLogger logger = new ConsoleLogger();
            CombatSystem combat = new CombatSystem(logger);

            // Create characters
            // Thorin: Warrior (Base: 150 HP, 10 Armor, 15 Attack)
            Warrior thorin = new Warrior("Thorin");
            // Elira: Mage (Base: 90 HP, 3 Armor, 12 Attack)
            Mage elira = new Mage("Elira");

            // Equip items
            // Thorin equips Sword (+5 Atk) та Shield (+4 Armor)
            // Total: Attack 20, Armor 14.
            thorin.EquipItem(new Sword());
            thorin.EquipItem(new Shield());

            // Elira equips MagicAmulet (+20 HP, Fireball)
            // Total: HP 110.
            elira.EquipItem(new MagicAmulet());

            // Starting stats
            Console.WriteLine("=== PRE-BATTLE STATS ===");
            PrintCharacterInfo(thorin);
            PrintCharacterInfo(elira);
            Console.WriteLine();

            // Battle simulation
            Console.WriteLine("=== BATTLE STARTS ===");

            // Thorin attacks Elira (20 Atk - 3 Armor = 17 Dmg)
            combat.Attack(thorin, elira);

            // Elira uses Fireball (Fixed 25 Dmg)
            combat.UseAbility(elira, thorin, "Fireball");

            // Thorin uses Power Strike (20 Atk * 2 = 40 Dmg)
            combat.UseAbility(thorin, elira, "Power Strike");

            // Elira heals (+10 HP)
            combat.Heal(elira, 10);

            // Thorin attacks Elira (17 Dmg)
            combat.Attack(thorin, elira);

            // Elira attacks Thorin (12 Atk - 14 Armor = 0 Dmg)
            combat.Attack(elira, thorin);

            Console.WriteLine("=== BATTLE ENDS ===");
            Console.WriteLine();

            // 6. Вивід кінцевих статів
            Console.WriteLine("=== POST-BATTLE STATS ===");
            PrintCharacterInfo(thorin);
            PrintCharacterInfo(elira);
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
            Console.WriteLine($"HP:     {c.Health}/{stats.TotalMaxHealth}");
            Console.WriteLine($"Atk:    {stats.TotalAttack}");
            Console.WriteLine($"Arm:    {stats.TotalArmor}");
            Console.WriteLine($"Items:  {items}");
            Console.WriteLine(new string('-', 20));
        }
    }
}