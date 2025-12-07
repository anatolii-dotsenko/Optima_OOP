using System;
using System.Collections.Generic;
using GameModel.Core.Contracts;

namespace GameModel.Infrastructure.CLI.Strategies
{
    public abstract class BaseStrategy : ICommandStrategy
    {
        protected readonly Dictionary<string, ICommand> _commands = new();
        public abstract string Name { get; }

        public void RegisterCommand(ICommand command)
        {
            _commands[command.Keyword.ToLower()] = command;
        }

        public void ExecCommand(string commandName, string[] args, Dictionary<string, string> options)
        {
            if (_commands.TryGetValue(commandName.ToLower(), out var cmd))
            {
                cmd.Execute(args, options);
            }
            else
            {
                Console.WriteLine($"Unknown command '{commandName}' in mode {Name}. Type 'help'.");
            }
        }

        public IEnumerable<ICommand> GetAvailableCommands() => _commands.Values;
    }
}