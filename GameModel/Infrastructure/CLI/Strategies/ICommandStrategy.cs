using GameModel.Core.Contracts;

namespace GameModel.Infrastructure.CLI.Strategies
{
    public interface ICommandStrategy
    {
        string Name { get; }
        void RegisterCommand(ICommand command);
        void ExecCommand(string commandName, string[] args, Dictionary<string, string> options);
        IEnumerable<ICommand> GetAvailableCommands();
    }
}