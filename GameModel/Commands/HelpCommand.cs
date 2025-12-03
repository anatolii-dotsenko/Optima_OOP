using System;
using System.Linq;

namespace GameModel.Commands
{
    /// <summary>
    /// Command to display all available commands.
    /// </summary>
    public class HelpCommand : ICommand
    {
        public string Keyword => "help";
        public string Description => "Display all available commands.";

        private readonly CommandRegistry _registry;

        public HelpCommand()
        {
            _registry = GameState.Instance.CommandRegistry;
        }

        public void Execute(string[] args)
        {
            Console.WriteLine("\n=== Available Commands ===");
            foreach (var cmd in _registry.GetAllCommands().OrderBy(c => c.Keyword))
            {
                Console.WriteLine($"  {cmd.Keyword,-12} {cmd.Description}");
            }
            Console.WriteLine();
        }
    }
}
