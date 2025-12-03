using System.Reflection;
using GameModel.Core.Contracts;

namespace GameModel.Infrastructure.CLI
{
    public class CommandRegistry
    {
        private readonly Dictionary<string, ICommand> _commands = new();

        /// <summary>
        /// Registers a command instance.
        /// </summary>
        /// <param name="command">The command implementation to register.</param>
        public void Register(ICommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            _commands[command.Keyword.ToLower()] = command;
        }

        /// <summary>
        /// Retrieves a command by its keyword.
        /// </summary>
        /// <param name="keyword">The command keyword (case-insensitive).</param>
        /// <returns>The command instance, or null if not found.</returns>
        public ICommand? GetCommand(string keyword)
        {
            return _commands.GetValueOrDefault(keyword.ToLower());
        }

        /// <summary>
        /// Retrieves all registered commands.
        /// </summary>
        public IEnumerable<ICommand> GetAll()
        {
            return _commands.Values;
        }
        
        /// <summary>
        /// Optional: helper to scan assembly for ICommand types.
        /// In this architecture, we prefer manual registration in GameBuilder 
        /// to handle Dependency Injection cleanly without a container.
        /// </summary>
        public void RegisterFromAssembly(Assembly assembly)
        {
            var commandTypes = assembly.GetTypes()
                .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            // Logic to instantiate would go here, but requires solving DI.
            // For now, we rely on Register() being called by the Composition Root (GameBuilder).
        }
    }
}