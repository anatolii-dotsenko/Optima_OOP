using System;
using System.Collections.Generic;
using System.Linq;
using GameModel.Content.Abilities;
using GameModel.Content.Characters;
using GameModel.Content.Items;
using GameModel.Core.Contracts;
using GameModel.Core.Entities;
using GameModel.Core.State;
using GameModel.Core.ValueObjects;

namespace GameModel.Infrastructure.CLI.Commands
{
    public class CreateCommand : ICommand
    {
        private readonly WorldContext _context;
        
        // Dictionary to map character class names (strings) to factory functions.
        // This replaces the open/closed principle violation found in the previous if/else logic.
        private readonly Dictionary<string, Func<string, Character>> _characterFactories;

        // Dictionary to map item type names to factory functions.
        private readonly Dictionary<string, Func<Item>> _itemFactories;

        public string Keyword => "create";
        public string Description => "Usage: create <char|item|ability>";

        public CreateCommand(WorldContext context)
        {
            _context = context;

            // Initialize character factories. 
            // New characters can be added here without modifying the Execute method logic.
            _characterFactories = new Dictionary<string, Func<string, Character>>(StringComparer.OrdinalIgnoreCase)
            {
                { "warrior", name => new Warrior(name) },
                { "mage", name => new Mage(name) }
                // { "archer", name => new Archer(name) } // Example of easy extension
            };

            // Initialize item factories.
            _itemFactories = new Dictionary<string, Func<Item>>(StringComparer.OrdinalIgnoreCase)
            {
                { "sword", () => new Sword() },
                { "shield", () => new Shield() }
            };
        }

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            if (args.Length == 0) { Console.WriteLine(Description); return; }

            string type = args[0].ToLower();
            
            if (type == "char")
            {
                Console.Write("Class (warrior/mage): ");
                string? cls = Console.ReadLine()?.Trim();
                
                // Validate input
                if (string.IsNullOrEmpty(cls) || !_characterFactories.ContainsKey(cls))
                {
                    Console.WriteLine($"Unknown class. Available: {string.Join(", ", _characterFactories.Keys)}");
                    return;
                }

                Console.Write("Name: ");
                string name = Console.ReadLine() ?? "Unnamed";

                // Use the factory to create the character instance
                var character = _characterFactories[cls](name);
                _context.Characters.Add(character);
                
                Console.WriteLine($"Character {name} created.");
            }
            else if (type == "item")
            {
                Console.Write("Type (sword/shield): ");
                string? it = Console.ReadLine()?.Trim();

                // Validate input using the dictionary keys
                if (string.IsNullOrEmpty(it) || !_itemFactories.ContainsKey(it))
                {
                    Console.WriteLine($"Unknown item type. Available: {string.Join(", ", _itemFactories.Keys)}");
                    return;
                }

                // Create and add the item
                var item = _itemFactories[it]();
                _context.ItemPool.Add(item);
                
                Console.WriteLine("Item created.");
            }
            // New Ability logic
            else if (type == "ability")
            {
                Console.WriteLine("Creating ability (mock implementation: adds to pool if pool existed, or just logs).");
                Console.WriteLine("Ability created.");
            }
            else
            {
                Console.WriteLine($"Unknown creation type '{type}'. Use char, item, or ability.");
            }
        }
    }

    public class EquipCommand : ICommand
    {
        private readonly WorldContext _context;
        public string Keyword => "add";
        public string Description => "Usage: add [--char_id <name> --id <item_name>]";

        public EquipCommand(WorldContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            // Strict flag usage per requirements
            string? charName = options.GetValueOrDefault("char_id");
            string? itemName = options.GetValueOrDefault("id");

            if (charName == null || itemName == null)
            {
                Console.WriteLine("Error: Must provide --char_id and --id");
                return;
            }

            var character = _context.Characters.FirstOrDefault(c => c.Name.Equals(charName, StringComparison.OrdinalIgnoreCase));
            var item = _context.ItemPool.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (character != null && item != null)
            {
                character.EquipItem(item);
                Console.WriteLine($"Equipped '{item.Name}' to '{character.Name}'.");
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
        public string Description => "Usage: ls <char|item|ability> [--id <name>]";

        public LsCommand(WorldContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            string category = args.Length > 0 ? args[0].ToLower() : "all";
            string? idFilter = options.GetValueOrDefault("id");

            if (category == "char" || category == "all")
            {
                Console.WriteLine("--- Characters ---");
                foreach (var c in _context.Characters)
                {
                    if (idFilter != null && !c.Name.Equals(idFilter, StringComparison.OrdinalIgnoreCase)) continue;
                    
                    Console.WriteLine($"Name: {c.Name}, HP: {c.GetStats().GetStat(StatType.Health)}/{c.GetStats().GetStat(StatType.MaxHealth)}");
                    if (idFilter != null) // Detailed view
                    {
                        Console.WriteLine($"  Armor: {c.GetStats().GetStat(StatType.Armor)}");
                        Console.WriteLine($"  Attack: {c.GetStats().GetStat(StatType.Attack)}");
                    }
                }
            }
            if (category == "item" || category == "all")
            {
                Console.WriteLine("--- Items Pool ---");
                foreach (var i in _context.ItemPool)
                {
                    if (idFilter != null && !i.Name.Equals(idFilter, StringComparison.OrdinalIgnoreCase)) continue;
                    Console.WriteLine($"- {i.Name}");
                }
            }
        }
    }
}