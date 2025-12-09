// defines the structure for executable CLI commands
namespace GameModel.Core.Contracts
{
    public interface ICommand
    {
        string Keyword { get; }
        string Description { get; }
        // options support (like --id 5)
        void Execute(string[] args, Dictionary<string, string> options);
    }
}