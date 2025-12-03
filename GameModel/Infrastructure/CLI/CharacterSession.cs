using System;
using System.Collections.Generic;
using System.Linq;
using GameModel.Core.Entities;          // Fixed: Character, Item base classes
using GameModel.Content.Characters;     // Fixed: Warrior, Mage
using GameModel.Content.Items;          // Fixed: Sword, Shield
using GameModel.Systems.Combat;         // Fixed: CombatSystem
using GameModel.Infrastructure.Logging; // Fixed: ConsoleLogger

namespace GameModel.Infrastructure.CLI
{
    public class CharacterSession : ICliSession
    {
        public string ModeName => "Characters";

        // In Ideal Architecture, Character is ICombatEntity, but for this session we cast to concrete Character
        // or change the list to ICombatEntity if you prefer strict abstraction.
        // For simplicity with the provided "Warrior/Mage" classes, we use Character base class.
        private List<Character> _characters = new();
        private List<Item> _itemPool = new(); 
        private CombatSystem _combatSystem;

        public CharacterSession()
        {
            _combatSystem = new CombatSystem(new ConsoleLogger());
            
            // Seed some data
            _characters.Add(new Warrior("Thorin"));
            _characters.Add(new Mage("Elira"));
            _itemPool.Add(new Sword());
        }

        public void PrintHelp()
        {
            Console.WriteLine("Commands: create, add, act, ls");
        }

        public void ExecuteCommand(string command, string[] args, Dictionary<string, string> options)
        {
            switch (command)
            {
                case "create":
                    CreateDialog(args);
                    break;
                case "ls":
                    ListObjects(args, options);
                    break;
                case "add": 
                    AddToCharacter(args, options);
                    break;
                case "act":
                    PerformAction(args, options);
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }

        private void CreateDialog(string[] args)
        {
            string type = args.Length > 0 ? args[0] : Ask("Create what? (char/item):");
            
            if (type == "char")
            {
                string cls = Ask("Class (warrior/mage):");
                string name = Ask("Name:");
                if (cls.ToLower() == "warrior") _characters.Add(new Warrior(name));
                else if (cls.ToLower() == "mage") _characters.Add(new Mage(name));
                Console.WriteLine($"Character {name} created.");
            }
            else if (type == "item")
            {
                string it = Ask("Item type (sword/shield):");
                if (it.ToLower() == "sword") _itemPool.Add(new Sword());
                else if (it.ToLower() == "shield") _itemPool.Add(new Shield());
                Console.WriteLine("Item created in pool.");
            }
        }

        private void ListObjects(string[] args, Dictionary<string, string> options)
        {
            string category = args.Length > 0 ? args[0] : "all";

            if (category == "char" || category == "all")
            {
                Console.WriteLine("--- Characters ---");
                foreach (var c in _characters)
                {
                    // Note: In Ideal Arch, access stats via GetStats() or Properties depending on implementation
                    var stats = c.GetStats(); 
                    // Assuming GetCurrentHealth() is exposed or using IsAlive check
                    Console.WriteLine($"- {c.Name} (Alive: {c.IsAlive})");
                }
            }
            if (category == "item" || category == "all")
            {
                Console.WriteLine("--- Items Pool ---");
                foreach (var i in _itemPool) Console.WriteLine($"- {i.Name}");
            }
        }

        private void AddToCharacter(string[] args, Dictionary<string, string> options)
        {
            string charName = options.ContainsKey("char_id") ? options["char_id"] : Ask("Character Name:");
            string itemName = options.ContainsKey("id") ? options["id"] : Ask("Item Name:");

            var character = _characters.FirstOrDefault(c => c.Name == charName);
            var item = _itemPool.FirstOrDefault(i => i.Name == itemName);

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

        private void PerformAction(string[] args, Dictionary<string, string> options)
        {
            if (args.Length < 3) { Console.WriteLine("Usage: act <type> <actor> <target>"); return; }
            
            string type = args[0];
            var actor = _characters.FirstOrDefault(c => c.Name == args[1]);
            var target = _characters.FirstOrDefault(c => c.Name == args[2]);

            if (actor == null || target == null) { Console.WriteLine("Participants not found."); return; }

            if (type == "attack")
            {
                _combatSystem.Attack(actor, target);
            }
            else if (type == "heal")
            {
                 _combatSystem.Heal(actor, 10);
            }
            else if (type == "ability")
            {
                string abName = options.ContainsKey("id") ? options["id"] : "Fireball";
                
                // In Ideal Arch, UseAbility expects the Ability object, not string name
                // We need to find the ability in the actor's list
                var abilities = actor.GetAbilities(); 
                var abilityToUse = abilities.FirstOrDefault(a => a.Name.Equals(abName, StringComparison.OrdinalIgnoreCase));

                if (abilityToUse != null)
                {
                    _combatSystem.UseAbility(actor, target, abilityToUse);
                }
                else
                {
                    Console.WriteLine($"Ability '{abName}' not found on {actor.Name}");
                }
            }
        }

        private string Ask(string q)
        {
            Console.Write(q + " ");
            return Console.ReadLine() ?? "";
        }
    }
}