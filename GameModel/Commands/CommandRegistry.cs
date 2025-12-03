using System;
using System.Collections.Generic;
using System.Linq;

namespace GameModel.Commands
{
    /// <summary>
    /// Registry that discovers and instantiates all ICommand implementations via reflection.
    /// Supports dynamic command registration without code modification (OCP).
    /// </summary>
    public class CommandRegistry
    {
        private readonly Dictionary<string, ICommand> _commands = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Scans all loaded assemblies for ICommand implementations and registers them.
        /// </summary>
        public void RegisterCommands()
        {
            var commandTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(ICommand).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

            foreach (var type in commandTypes)
            {
                try
                {
                    var command = (ICommand)Activator.CreateInstance(type);
                    _commands[command.Keyword] = command;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: Failed to register command {type.Name}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Attempts to retrieve a command by keyword.
        /// </summary>
        public bool TryGetCommand(string keyword, out ICommand command)
        {
            return _commands.TryGetValue(keyword, out command);
        }

        /// <summary>
        /// Returns all registered commands for help/listing.
        /// </summary>
        public IEnumerable<ICommand> GetAllCommands()
        {
            return _commands.Values;
        }
    }
}
