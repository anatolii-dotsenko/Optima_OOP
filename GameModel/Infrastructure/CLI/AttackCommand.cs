using GameModel.Core.Contracts;

namespace GameModel.Infrastructure.CLI.Commands
{
    public class AttackCommand : ICommand
    {
        private readonly ICombatSystem _system;
        private readonly Func<string, ICombatEntity?> _characterLookup;

        public string Keyword => "attack";
        public string Description => "Usage: attack <attacker> <target>";

        public AttackCommand(ICombatSystem system, Func<string, ICombatEntity?> lookup)
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

            var attacker = _characterLookup(args[0]);
            var target = _characterLookup(args[1]);

            if (attacker != null && target != null)
            {
                _system.Attack(attacker, target);
            }
            else
            {
                Console.WriteLine("Character not found.");
            }
        }
    }
}