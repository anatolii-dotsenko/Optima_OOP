using System.Collections.Generic;
using GameModel.Core.Contracts; // Fixed: Missing using for ICommand
using GameModel.Infrastructure.CLI.Commands;

namespace GameModel.Infrastructure.CLI.Strategies
{
    public class RpgStrategy : BaseStrategy
    {
        public override string Name => "RPG Mode";

        public RpgStrategy() 
        {
            // Constructor is now parameterless to match GameBuilder usage.
            // Commands are registered externally via RegisterCommand.
        }
        
        // Helper to batch register commands if needed
        public void AddRpgCommands(IEnumerable<ICommand> commands)
        {
            foreach(var c in commands) RegisterCommand(c);
        }
    }
}