using GameModel.Core.Contracts;

namespace GameModel.Infrastructure.CLI.Commands
{
    public class HealCommand : ICommand
    {
        private readonly ICombatSystem _system;
        private readonly Func<string, ICombatEntity?> _characterLookup;

        public string Keyword => "heal";
        public string Description => "Usage: heal <character_name> <amount>";

        public HealCommand(ICombatSystem system, Func<string, ICombatEntity?> lookup)
        {
            _system = system;
            _characterLookup = lookup;
        }

        public void Execute(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine(Description);
                return;
            }

            string targetName = args[0];
            string amountStr = args[1];

            var target = _characterLookup(targetName);

            if (target == null)
            {
                Console.WriteLine($"Character '{targetName}' not found.");
                return;
            }

            if (!int.TryParse(amountStr, out int amount))
            {
                Console.WriteLine($"Invalid amount: '{amountStr}'. Must be an integer.");
                return;
            }

            // Execute the heal action via the combat system
            _system.Heal(target, amount);
        }
    }
}