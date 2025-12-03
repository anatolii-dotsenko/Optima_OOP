
using System;
using System.Collections.Generic;
using GameModel.Characters;
using GameModel.Items;
using GameModel.Combat;
using GameModel.Logging;

namespace GameModel.Infrastructure.CLI
{
    public class AiBattleSession : ICliSession
    {
        public string ModeName => "AI Fight";

        public void PrintHelp()
        {
            Console.WriteLine("Commands: start (runs the battle), exit");
        }

        public void ExecuteCommand(string command, string[] args, Dictionary<string, string> options)
        {
            if (command == "start")
            {
                RunSimulation();
            }
            else
            {
                Console.WriteLine("Unknown command. Type 'start' to begin simulation.");
            }
        }

        private void RunSimulation()
        {
            // Initialize infrastructure
            ICombatLogger logger = new ConsoleLogger();
            CombatSystem combat = new CombatSystem(logger);

            // Create characters
            Warrior thorin = new Warrior("Thorin");
            Mage elira = new Mage("Elira");

            // Equip items
            thorin.EquipItem(new Sword());
            thorin.EquipItem(new Shield());
            elira.EquipItem(new MagicAmulet());
            // Giving Elira a wand too for the show
            elira.EquipItem(new LightningWand());

            // Display initial state
            Console.WriteLine("=== PRE-BATTLE STATS ===");
            PrintInfo(thorin);
            PrintInfo(elira);
            Console.WriteLine();

            Console.WriteLine("=== BATTLE STARTS ===");

            // Scripted Turn Sequence
            PerformTurn(combat, thorin, elira, "attack");
            if (IsBattleOver(thorin, elira)) return;

            PerformTurn(combat, elira, thorin, "ability", "Fireball");
            if (IsBattleOver(thorin, elira)) return;

            PerformTurn(combat, thorin, elira, "ability", "Power Strike");
            if (IsBattleOver(thorin, elira)) return;

            PerformTurn(combat, elira, thorin, "heal", "15");
            if (IsBattleOver(thorin, elira)) return;
            
            PerformTurn(combat, elira, thorin, "ability", "Lightning");
            if (IsBattleOver(thorin, elira)) return;

            Console.WriteLine("=== BATTLE ENDS ===");
            Console.WriteLine("Draw/Timeout reached.");
        }

        private void PerformTurn(CombatSystem sys, Character actor, Character target, string action, string? arg = null)
        {
            if (actor.Health <= 0) return;

            switch (action)
            {
                case "attack":
                    sys.Attack(actor, target);
                    break;
                case "ability":
                    sys.UseAbility(actor, target, arg!);
                    break;
                case "heal":
                    sys.Heal(actor, int.Parse(arg!));
                    break;
            }
        }

        private bool IsBattleOver(Character c1, Character c2)
        {
            if (c1.Health <= 0)
            {
                Console.WriteLine($"\n*** {c1.Name} was defeated! ***");
                return true;
            }
            if (c2.Health <= 0)
            {
                Console.WriteLine($"\n*** {c2.Name} was defeated! ***");
                return true;
            }
            return false;
        }

        private void PrintInfo(Character c)
        {
            var stats = c.GetFinalStats();
            Console.WriteLine($"{c.Name}: HP {c.Health}/{stats.TotalMaxHealth} | Atk {stats.TotalAttack} | Arm {stats.TotalArmor}");
        }
    }
}