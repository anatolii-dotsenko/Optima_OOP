using GameModel.Core.Contracts;

namespace GameModel.Infrastructure.CLI.Commands
{
    public class HelpCommand : ICommand
    {
        private readonly CommandRegistry _registry;
        
        public string Keyword => "help";
        public string Description => "Lists all commands";

        public HelpCommand(CommandRegistry registry)
        {
            _registry = registry;
        }

        public void Execute(string[] args)
        {
            Console.WriteLine("Available Commands:");
            foreach(var cmd in _registry.GetAll())
            {
                Console.WriteLine($"- {cmd.Keyword}: {cmd.Description}");
            }
        }
    }
}