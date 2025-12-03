using System;
using System.Collections.Generic;
using System.Linq;
using GameModel.Core.Contracts;
using GameModel.Core.State;

namespace GameModel.Infrastructure.CLI.Commands
{
    public class ActCommand : ICommand
    {
        private readonly ICombatSystem _system;
        private readonly WorldContext _context;

        public string Keyword => "act";
        public string Description => "Usage: act <attack|heal|ability> <actor> <target> [--id AbilityName]";

        public ActCommand(ICombatSystem system, WorldContext context)
        {
            _system = system;
            _context = context;
        }

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            if (args.Length < 3) 
            {
                Console.WriteLine(Description); 
                return;
            }

            string actionType = args[0].ToLower();
            string actorName = args[1];
            string targetName = args[2];

            var actor = _context.Characters.FirstOrDefault(c => c.Name.Equals(actorName, StringComparison.OrdinalIgnoreCase));
            var target = _context.Characters.FirstOrDefault(c => c.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase));

            if (actor == null || target == null)
            {
                Console.WriteLine("Actor or Target not found.");
                return;
            }

            switch (actionType)
            {
                case "attack":
                    _system.Attack(actor, target);
                    break;
                
                case "heal":
                    // Simple logic: Healer heals target (or self) for fixed amount for now
                    _system.Heal(target, 10); 
                    break;

                case "ability":
                    string abilityName = options.GetValueOrDefault("id");
                    if (string.IsNullOrEmpty(abilityName))
                    {
                        Console.WriteLine("Specify ability name with --id.");
                        return;
                    }
                    var ability = actor.GetAbilities().FirstOrDefault(a => a.Name.Equals(abilityName, StringComparison.OrdinalIgnoreCase));
                    if (ability != null)
                    {
                        _system.UseAbility(actor, target, ability);
                    }
                    else
                    {
                        Console.WriteLine($"Ability '{abilityName}' not found on {actor.Name}");
                    }
                    break;

                default:
                    Console.WriteLine("Unknown action type. Use attack, heal, or ability.");
                    break;
            }
        }
    }
}