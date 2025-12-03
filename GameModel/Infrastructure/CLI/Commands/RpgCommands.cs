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
        private readonly Dictionary<string, Func<string, Character>> _characterFactories;
        private readonly Dictionary<string, Func<Item>> _itemFactories;

        public string Keyword => "create";
        public string Description => "Usage: create <char|item|ability>. Interactive dialog to create a new character, item, or ability.";

        public CreateCommand(WorldContext context)
        {
            _context = context;

            _characterFactories = new Dictionary<string, Func<string, Character>>(StringComparer.OrdinalIgnoreCase)
            {
                { "warrior", name => new Warrior(name) },
                { "mage", name => new Mage(name) }
            };

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
                // Interactive dialog for character creation
                Console.Write($"Enter character class ({string.Join("/", _characterFactories.Keys)}): ");
                string? cls = Console.ReadLine()?.Trim();
                
                if (string.IsNullOrEmpty(cls) || !_characterFactories.ContainsKey(cls))
                {
                    Console.WriteLine($"Unknown class. Available: {string.Join(", ", _characterFactories.Keys)}");
                    return;
                }

                Console.Write("Enter character name: ");
                string name = Console.ReadLine() ?? "Unnamed";

                var character = _characterFactories[cls](name);
                _context.Characters.Add(character);
                
                Console.WriteLine($"Character '{name}' ({cls}) created successfully.");
            }
            else if (type == "item")
            {
                // Interactive dialog for item creation
                Console.Write($"Enter item type ({string.Join("/", _itemFactories.Keys)}): ");
                string? it = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(it) || !_itemFactories.ContainsKey(it))
                {
                    Console.WriteLine($"Unknown item type. Available: {string.Join(", ", _itemFactories.Keys)}");
                    return;
                }

                var item = _itemFactories[it]();
                _context.ItemPool.Add(item);
                
                Console.WriteLine($"Item '{item.Name}' created successfully.");
            }
            else if (type == "ability")
            {
                Console.WriteLine("Creating ability (Mock: Ability system logic would go here).");
                Console.WriteLine("Ability created.");
            }
            else
            {
                Console.WriteLine($"Unknown creation type '{type}'. Use 'char', 'item', or 'ability'.");
            }
        }
    }

    public class EquipCommand : ICommand
    {
        private readonly WorldContext _context;
        public string Keyword => "add";
        public string Description => "Usage: add --char_id <name> --id <item_name>. Equips an item or ability to a character.";

        public EquipCommand(WorldContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            string? charName = options.GetValueOrDefault("char_id");
            string? itemName = options.GetValueOrDefault("id");

            if (charName == null || itemName == null)
            {
                Console.WriteLine("Error: Missing arguments. You must provide --char_id and --id.");
                return;
            }

            var character = _context.Characters.FirstOrDefault(c => c.Name.Equals(charName, StringComparison.OrdinalIgnoreCase));
            var item = _context.ItemPool.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (character != null && item != null)
            {
                character.EquipItem(item);
                Console.WriteLine($"Successfully equipped '{item.Name}' to '{character.Name}'.");
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
        public string Description => "Usage: ls <char|item|ability> [--id <name>]. Lists objects. Use --id to see full stats.";

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
                    // Filter if --id is provided
                    if (idFilter != null && !c.Name.Equals(idFilter, StringComparison.OrdinalIgnoreCase)) continue;
                    
                    Console.WriteLine($"Name: {c.Name}, HP: {c.GetStats().GetStat(StatType.Health)}/{c.GetStats().GetStat(StatType.MaxHealth)}");
                    if (idFilter != null) // Detailed view
                    {
                        Console.WriteLine($"  Armor: {c.GetStats().GetStat(StatType.Armor)}");
                        Console.WriteLine($"  Attack: {c.GetStats().GetStat(StatType.Attack)}");
                        Console.WriteLine("  Abilities: " + string.Join(", ", c.GetAbilities().Select(a => a.Name)));
                    }
                }
            }
            if (category == "item" || category == "all")
            {
                Console.WriteLine("--- Items Pool ---");
                foreach (var i in _context.ItemPool)
                {
                    if (idFilter != null && !i.Name.Equals(idFilter, StringComparison.OrdinalIgnoreCase)) continue;
                    Console.WriteLine($"- {i.Name} (Grants: {string.Join(", ", i.Modifiers.Select(m => $"{m.Key}+{m.Value}"))})");
                }
            }
        }
    }
}