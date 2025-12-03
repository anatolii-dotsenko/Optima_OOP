using System;
using System.Collections.Generic;
using System.Linq;
using GameModel.Content.Characters;
using GameModel.Content.Items;
using GameModel.Core.Contracts;
using GameModel.Core.State;

namespace GameModel.Infrastructure.CLI.Commands
{
    public class CreateCommand : ICommand
    {
        private readonly WorldContext _context;
        public string Keyword => "create";
        public string Description => "Usage: create <char|item>";

        public CreateCommand(WorldContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            if (args.Length == 0) { Console.WriteLine("Specify 'char' or 'item'."); return; }

            string type = args[0].ToLower();
            if (type == "char")
            {
                // Simple interactive prompts allowed inside the command if args missing
                Console.Write("Class (warrior/mage): ");
                string cls = Console.ReadLine()?.ToLower() ?? "";
                Console.Write("Name: ");
                string name = Console.ReadLine() ?? "Unknown";

                if (cls == "warrior") _context.Characters.Add(new Warrior(name));
                else if (cls == "mage") _context.Characters.Add(new Mage(name));
                else Console.WriteLine("Unknown class.");
                
                Console.WriteLine($"Created {name}.");
            }
            else if (type == "item")
            {
                Console.Write("Item (sword/shield): ");
                string it = Console.ReadLine()?.ToLower() ?? "";
                if (it == "sword") _context.ItemPool.Add(new Sword());
                else if (it == "shield") _context.ItemPool.Add(new Shield());
                Console.WriteLine("Item added to pool.");
            }
        }
    }

    public class EquipCommand : ICommand
    {
        private readonly WorldContext _context;
        public string Keyword => "add"; // Matches "add" requirement from 'Characters' system
        public string Description => "Usage: add --char_id <Name> --id <ItemName>";

        public EquipCommand(WorldContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            string charName = options.GetValueOrDefault("char_id");
            string itemName = options.GetValueOrDefault("id");

            if (string.IsNullOrEmpty(charName) || string.IsNullOrEmpty(itemName))
            {
                Console.WriteLine("Missing flags. Use --char_id and --id.");
                return;
            }

            var character = _context.Characters.FirstOrDefault(c => c.Name.Equals(charName, StringComparison.OrdinalIgnoreCase));
            var item = _context.ItemPool.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (character != null && item != null)
            {
                character.EquipItem(item);
                Console.WriteLine($"Equipped {item.Name} to {character.Name}");
            }
            else
            {
                Console.WriteLine("Character or Item not found.");
            }
        }
    }

    public class LsCommand : ICommand
    {
        private readonly WorldContext _context;
        public string Keyword => "ls";
        public string Description => "Usage: ls <char|item>";

        public LsCommand(WorldContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            string type = args.Length > 0 ? args[0] : "all";

            if (type == "char" || type == "all")
            {
                Console.WriteLine("--- Characters ---");
                foreach (var c in _context.Characters)
                    Console.WriteLine($"- {c.Name} (HP: {c.GetStats().GetStat(GameModel.Core.ValueObjects.StatType.Health)})");
            }
            if (type == "item" || type == "all")
            {
                Console.WriteLine("--- Items Pool ---");
                foreach (var i in _context.ItemPool)
                    Console.WriteLine($"- {i.Name}");
            }
        }
    }
}